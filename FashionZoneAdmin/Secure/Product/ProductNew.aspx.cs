using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using FashionZone.Admin.CustomControl;
using System.Drawing;
using System.Text;

namespace FashionZone.Admin.Secure.Product
{
    public partial class ProductNew : System.Web.UI.Page
    {
        protected int ProductID
        {
            get
            {
                int id;
                if (ViewState["ProductID"] != null && Int32.TryParse(ViewState["ProductID"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["ProductID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Error += new EventHandler(Page_Error);
            string prodId = Request["ID"];
            int id;
            if (!IsPostBack)
            {
                if (!String.IsNullOrWhiteSpace(prodId) && Int32.TryParse(prodId, out id) && id != 0)
                {

                    if (!loadProduct(id))
                    {
                        writeError("Product was not found.");
                    }
                }
                else
                {
                    populateAttributes();
                }

                if (!String.IsNullOrEmpty(User.Identity.Name))
                {
                    USER user = ApplicationContext.Current.Users.GetByUserName(User.Identity.Name);
                    // not an administrator neither a moderator
                    if (user.RoleID > 2)
                    {
                        chkApproved.Enabled = false;
                    }
                }
                else
                {
                    chkApproved.Enabled = false;
                }
            }
            //lblErrors.Text = "";
            txtApprover.Attributes.Add("readonly", "readonly");
            txtDiscount.Attributes.Add("readonly", "readonly");
        }

        private bool loadProduct(int id)
        {
            PRODUCT prod = null;
            try
            {
                prod = ApplicationContext.Current.Products.GetById(id);

                if (prod != null)
                {
                    ProductID = id;

                    txtName.Text = prod.Name;
                    txtPrice.Text = prod.OurPrice.ToString("N2");
                    txtOriginalPrice.Text = prod.OriginalPrice.ToString("N2");
                    if (prod.SupplierPrice.HasValue)
                    {
                        txtSupplierPrice.Text = prod.SupplierPrice.Value.ToString("N2");
                    }
                    txtDescription.Content = prod.Description;
                    txtDiscount.Text = prod.Discount.Value.ToString();
                    txtCode.Text = prod.Code;

                    List<PRODUCT_ATTRIBUTE> attributes = new List<PRODUCT_ATTRIBUTE>();
                    if (ProductID != 0)
                    {
                        //we are retrieving attributes for an existing product
                        attributes = ApplicationContext.Current.Products.GetProductAttributes(ProductID);
                    }

                    if (prod.Approved.HasValue)
                    {
                        chkApproved.Checked = prod.Approved.Value;

                        if (prod.USER != null)
                        {
                            txtApprover.Text = prod.USER.Login;
                        }
                    }

                    if (attributes.Count > 0)
                    {
                        ddlAttributes.Visible = false;
                        populateAttributeValues(attributes.First().D_ATTRIBUTE_VALUE.AttributeID, attributes);
                    }
                    else
                    {
                        populateAttributes();
                    }

                    loadImages(false);
                    loadCategories(prod);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                writeError(ex.Message);
                return false;
            }
            return true;
        }

        private void populateAttributes()
        {
            ddlAttributes.DataSource = ApplicationContext.Current.Attributes.GetAll();
            ddlAttributes.DataBind();
            ddlAttributes.Visible = true;
            ddlAttributes_SelectedIndexChanged(null, null); // load for first available attribute
        }

        private void populateAttributeValues(int attributeId, List<PRODUCT_ATTRIBUTE> attributes)
        {
            List<D_ATTRIBUTE_VALUE> values = ApplicationContext.Current.Attributes.GetAttributeValues(attributeId);
            List<PRODUCT_ATTRIBUTE> showAttributes = new List<PRODUCT_ATTRIBUTE>();
            PRODUCT_ATTRIBUTE prod;
            foreach (D_ATTRIBUTE_VALUE value in values)
            {
                prod = null;
                if (attributes != null && attributes.Count > 0 && (prod = attributes.Where(a => a.D_ATTRIBUTE_VALUE.ID == value.ID).FirstOrDefault()) != null)
                {
                    showAttributes.Add(prod);
                }
                else
                {
                    // adding a fake attribute in order to have one for every value
                    showAttributes.Add(new PRODUCT_ATTRIBUTE() { ID = 0, Availability = 0, Quantity = 0, ProductID = ProductID, AttributeValueID = value.ID, ValueOrder = value.ShowOrder, Value = value.Value });
                }
            }

            lvAttributeValues.DataSource = showAttributes.OrderBy(a => a.ValueOrder);
            lvAttributeValues.DataBind();
        }

        private void loadCategories(PRODUCT product)
        {
            if (product.CATEGORY != null && product.CATEGORY.Count > 0)
            {
                populate(product.CampaignID.Value, product.Campaign, product.CATEGORY);
                chooseBrandCampPnl.Visible = false;
                tvCategories.Visible = true;
            }
        }

        private void loadImages(bool addFake)
        {
            List<PROD_IMAGES> images;
            if (ProductID != 0)
            {
                images = ApplicationContext.Current.Products.GetImages(ProductID);
            }
            else
            {
                images = new List<PROD_IMAGES>();
            }
            if (addFake)
            {
                images.Insert(images.Count, createEmptyImage());
            }
            bindRepeater(images);
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
        }

        private void bindRepeater(List<PROD_IMAGES> images)
        {
            repeater.DataSource = images;
            repeater.DataBind();
        }

        private bool checkPrincipalImage()
        {
            bool result = false;

            ProductImageUpload upl;
            if (repeater.Items.Count > 0)
            {
                foreach (RepeaterItem item in repeater.Items)
                {
                    if (item.HasControls() && item.Controls[1] is ProductImageUpload)
                    {
                        upl = item.Controls[1] as ProductImageUpload;
                        if (upl.Principal)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (tvCategories.Nodes.Count == 0 || tvCategories.CheckedNodes.Count == 0)
            {
                writeError("At least a category should be checked.");
                return;
            }

            if (!checkPrincipalImage() && chkApproved.Checked)
            {
                writeError("Approved product must contain a principal image.");
                return;
            }

            PRODUCT prod = new PRODUCT();
            prod.Name = txtName.Text;
            decimal our, original, suppl = 0;
            if (Decimal.TryParse(txtPrice.Text, out our))
            {
                prod.OurPrice = our;
            }
            if (Decimal.TryParse(txtOriginalPrice.Text, out original))
            {
                prod.OriginalPrice = original;
            }

            if (Decimal.TryParse(txtSupplierPrice.Text, out suppl))
            {
                prod.SupplierPrice = suppl;
            }

            prod.Description = txtDescription.Content;

            txtDiscount.Text = ((int)(((original - our) / original) * 100)).ToString();

            int id = 0;
            lblErrors.Visible = true;
            lblErrors.ForeColor = Color.Green;
            prod.Code = txtCode.Text;

            string operation = String.Empty;

            try
            {
                // sets checked category
                if (!setCategories(prod))
                {
                    writeError("At least one sub-category should be checked (ex. Woman->Jeans).");
                    return;
                }

                USER user = null;
                if (!String.IsNullOrEmpty(User.Identity.Name))
                {
                    user = ApplicationContext.Current.Users.GetByUserName(User.Identity.Name);
                }

                if (ProductID != 0)
                {
                    prod.ID = ProductID;
                    bool attached = false;

                    if (user != null)
                    {
                        bool? previousStatus = ApplicationContext.Current.Products.GetApprovalStatus(prod.ID);

                        if (chkApproved.Checked)
                        {
                            if (previousStatus == null || !previousStatus.Value)
                            {
                                if (user.RoleID <= 2)
                                {
                                    prod.Approved = true;
                                    prod.ApprovedBy = user.ID;
                                    ApplicationContext.Current.Products.ChangeApproval(prod);
                                    attached = true;
                                    txtApprover.Text = user.Login;
                                }
                            }
                        }
                        else
                        {
                            if (previousStatus.HasValue && previousStatus.Value)
                            {
                                if (user.RoleID <= 2)
                                {
                                    prod.Approved = false;
                                    ApplicationContext.Current.Products.ChangeApproval(prod);
                                    attached = true;
                                    txtApprover.Text = String.Empty;
                                }
                            }
                        }
                    }

                    ApplicationContext.Current.Products.Update(prod, !attached);
                    operation = "updated";
                }
                else
                {
                    if (user != null && chkApproved.Checked)
                    {
                        prod.Approved = true;
                        prod.ApprovedBy = user.ID;
                        txtApprover.Text = user.Login;
                    }
                    else
                    {
                        prod.Approved = false;
                    }

                    ApplicationContext.Current.Products.Insert(prod);
                    operation = "inserted";
                    ProductID = prod.ID;
                }

                updDiscountApprover.Update();
                saveAttributes(prod);

                ProductImageUpload upl;
                if (repeater.Items.Count > 0)
                {
                    foreach (RepeaterItem item in repeater.Items)
                    {
                        if (item.HasControls() && item.Controls[1] is ProductImageUpload)
                        {
                            upl = item.Controls[1] as ProductImageUpload;
                            if (upl.IsFormValid())
                            {
                                upl.ProdID = ProductID.ToString() ;
                                upl.Save();
                            }
                        }
                    }
                }

                lblErrors.Text = "Product " + operation + " correctly.";
                LinkButton lnkAddImage = lgnViewAddImage.FindControl("lnkAddImage") as LinkButton;
                if (lnkAddImage != null)
                {
                    lnkAddImage.Visible = true;
                }
                // deactivating campaign and brand ddls
                cddlBrand.Enabled = false;
                cddlCampain.Enabled = false;
                chooseBrandCampPnl.Visible = false;
                updPanelBrandCampaign.Update();

                UpdatePanel updPnlLnkAdd = lgnViewAddImage.FindControl("updPnlLnkAdd") as UpdatePanel;
                if (updPnlLnkAdd != null)
                {
                    updPnlLnkAdd.Update();
                }
            }
            // TODO handle situation with OptimisticConcurrencyException rethrown from BL
            catch (Exception ex)
            {
                // TODO log error
                writeError(ex.Message);
            }
        }

        private void saveAttributes(PRODUCT product)
        {
            string prodAttrID, attrValID, availability;
            PRODUCT_ATTRIBUTE prodAttr;
            int id;
            foreach (ListViewDataItem item in lvAttributeValues.Items)
            {
                prodAttrID = attrValID = availability = String.Empty;

                prodAttrID = ((HiddenField)item.FindControl("prodAttrID")).Value;
                attrValID = ((HiddenField)item.FindControl("attrValID")).Value;
                availability = ((TextBox)item.FindControl("txtAvailability")).Text;

                id = 0;

                prodAttr = new PRODUCT_ATTRIBUTE();
                if (!String.IsNullOrEmpty(prodAttrID) && !String.IsNullOrEmpty(attrValID) && Int32.TryParse(prodAttrID, out id))
                {
                    // if availability is 0 and the attribute is never associated to this product, value is not considered
                    if (availability == "0" && id == 0)
                        continue;

                    ddlAttributes.Visible = false;
                    prodAttr.Availability = Int32.Parse(availability);
                    prodAttr.AttributeValueID = Int32.Parse(attrValID);
                    prodAttr.ProductID = product.ID;
                    // old version from previous retrieval
                    prodAttr.Version = (byte[])lvAttributeValues.DataKeys[item.DataItemIndex].Value;
                    if (id != 0)
                    {
                        // not new
                        prodAttr.ID = id;
                        ApplicationContext.Current.Products.UpdateProductAttribute(prodAttr);
                    }
                    else
                    {
                        // new, quantity is set only this time
                        prodAttr.Quantity = prodAttr.Availability;
                        ApplicationContext.Current.Products.AddProductAttribute(prodAttr);
                        ((HiddenField)item.FindControl("prodAttrID")).Value = prodAttr.ID.ToString();
                        ((Label)item.FindControl("lblQuantity")).Text = prodAttr.Quantity.ToString();
                    }
                }
            }

            // the list view is updated to reflect new versions after update
            List<PRODUCT_ATTRIBUTE> attributes = ApplicationContext.Current.Products.GetProductAttributes(ProductID);
            if (attributes.Count > 0)
            {
                populateAttributeValues(attributes.First().D_ATTRIBUTE_VALUE.AttributeID, attributes);
                updPanelAttributes.Update();
            }
        }

        private bool setCategories(PRODUCT product)
        {
            CATEGORY cat;
            bool subCatChecked = false;
            foreach (TreeNode node in tvCategories.CheckedNodes)
            {
                if (node.Depth == 2)
                {
                    subCatChecked = true;
                }
                cat = new CATEGORY() { ID = Int32.Parse(node.Value) };
                product.CATEGORY.Add(cat);
            }
            return subCatChecked;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void imageDelete(object sender, EventArgs e)
        {
            ApplicationContext.Current.Products.DeleteImageById(Int32.Parse((sender as ProductImageUpload).ImgID));
            loadImages(false);
            LinkButton lnkAddImage = lgnViewAddImage.FindControl("lnkAddImage") as LinkButton;
            if (lnkAddImage != null)
            {
                lnkAddImage.Visible = true;
            }
            UpdatePanel updPnlLnkAdd = lgnViewAddImage.FindControl("updPnlLnkAdd") as UpdatePanel;
            if (updPnlLnkAdd != null)
            {
                updPnlLnkAdd.Update();
            }
        }

        protected void lnkAddImage_Click(object sender, EventArgs e)
        {
            addImage();
            LinkButton lnkAddImage = lgnViewAddImage.FindControl("lnkAddImage") as LinkButton;
            if (lnkAddImage != null)
            {
                lnkAddImage.Visible = false;
            }
            UpdatePanel updPnlLnkAdd = lgnViewAddImage.FindControl("updPnlLnkAdd") as UpdatePanel;
            if (updPnlLnkAdd != null)
            {
                updPnlLnkAdd.Update();
            }
        }

        private void addImage()
        {
            ProductImageUpload upl;
            if (repeater.Items.Count > 0 && repeater.Items[repeater.Items.Count - 1].Controls[1] is ProductImageUpload)
            {
                upl = (ProductImageUpload)repeater.Items[repeater.Items.Count - 1].Controls[1];
                if (upl.IsFormValid())
                {
                    loadImages(true);
                }
                else
                {
                    writeError("Images should be loaded before you can insert a new one.");
                }
            }
            else
            {
                loadImages(true);
            }
        }

        private void writeError(string errorMessage)
        {
            lblErrors.Visible = true;
            lblErrors.ForeColor = Color.Red;
            lblErrors.Text = "Error occurred: " + errorMessage;
        }

        private PROD_IMAGES createEmptyImage()
        {
            PROD_IMAGES fake = new PROD_IMAGES()
            {
                ID = 0,
                Image = String.Empty,
                LargeImage = String.Empty,
                Principal = false,
                ProductID = Int32.Parse(ID.Value),
                Thumbnail = String.Empty
            };

            return fake;
        }

        protected void ddlCampain_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idcampainSelectedVal = 0;
            if (Int32.TryParse(ddlCampaign.SelectedValue, out idcampainSelectedVal))
            {
                populate(idcampainSelectedVal, ddlCampaign.SelectedItem.Text, null);
            }
        }

        private void populate(int campaignID, string campaignName, ICollection<CATEGORY> cats)
        {
            TreeNode myNode;
            tvCategories.Nodes.Clear();
            myNode = new TreeNode(campaignName, campaignID.ToString());
            myNode.NavigateUrl = "CampaignNew.aspx?ID=" + ddlCampaign.SelectedValue;
            tvCategories.Nodes.Add(myNode);

            List<CATEGORY> catAllList = ApplicationContext.Current.Categories.GetCategoryListByCampaign(campaignID);
            if (catAllList != null && catAllList.Count != 0)
            {
                var catFatherL = catAllList.Where(a => a.ParentID == null);
                foreach (var singleC in catFatherL)
                {
                    populateTreeView(myNode, singleC, cats, catAllList);
                }
                tvCategories.Visible = true;
                lblCat.Visible = true;
                lblErrors.Text = String.Empty;
            }
            else
            {
                tvCategories.Visible = false;
                lblCat.Visible = true;
                lblErrors.Text = "No categories found for the selected campaign.";
            }
        }


        private static void populateTreeView(TreeNode myNode, CATEGORY singleC, ICollection<CATEGORY> cats, List<CATEGORY> allCats)
        {
            var catChChildren = allCats.Where(c => c.ParentID == singleC.ID).OrderBy(c2 => c2.Ordering).ToList();
            TreeNode nFather = addNodeToTree(myNode, singleC, cats);
            if (catChChildren.Count > 0)
            {
                foreach (var c in catChChildren)
                    populateTreeView(nFather, c, cats, allCats);
            }
            catChChildren.Clear();
        }

        private static TreeNode addNodeToTree(TreeNode myNode, CATEGORY singleC, ICollection<CATEGORY> cats)
        {
            var node = new TreeNode(singleC.Name, singleC.ID.ToString());
            node.ShowCheckBox = true;
            if (cats != null && cats.Count > 0 && cats.Any(cat => cat.ID == singleC.ID))
                node.Checked = true;
            myNode.ChildNodes.Add(node);
            return node;
        }

        protected void ddlAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idattrSelectedVal = 0;
            if (!String.IsNullOrWhiteSpace(ddlAttributes.SelectedValue) && Int32.TryParse(ddlAttributes.SelectedValue, out idattrSelectedVal))
            {
                populateAttributeValues(idattrSelectedVal, null);
            }
        }
    }
}