using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using FashionZone.BL.Util;
using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace FashionZone.FE.Secure
{
    public partial class product : Utils.BasePage
    {
        protected int CampaignID
        {
            get
            {
                int id;
                if (ViewState["CampaignID"] != null && Int32.TryParse(ViewState["CampaignID"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["CampaignID"] = value;
            }
        }

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

        // temporary session variable used during product selection in the popup
        public byte[] Version
        {
            get
            {
                if (Session["ProdAttrV"] != null)
                {
                    return (byte[])Session["ProdAttrV"];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                Session["ProdAttrV"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl divContent = (HtmlGenericControl)Master.FindControl("divMainContent");
            divContent.Attributes["class"] = "ContentIII";

            if (!IsPostBack)
            {
                try
                {
                    Version = null;
                    if (RouteData.Values["prod"] != null && !String.IsNullOrWhiteSpace(RouteData.Values["prod"].ToString()))
                    {
                        loadProductData(RouteData.Values["prod"].ToString());
                    }
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (Exception ex)
                {
                    Log(ex, ex.Message, ex.StackTrace, "Product");
                    Response.Redirect("/home/");
                }
            }
            base.Page_Load(sender, e);
        }

        private void loadProductData(string encProdID)
        {
            string productId = Encryption.Decrypt(encProdID);
            int id;

            if (Int32.TryParse(productId, out id))
            {

                PRODUCT product = ApplicationContext.Current.Products.GetById(id);
               
                if (product != null)
                {
                    CAMPAIGN campaign = null;
                    if (product.CampaignID.HasValue)
                    {
                        campaign = ApplicationContext.Current.Campaigns.GetById(product.CampaignID.Value);
                    }
                    if (campaign != null && (campaign.Active || (campaign.Approved.Value && campaign.StartDate > DateTime.Today)))
                    {
                        ProductID = product.ID;
                        this.Title = campaign.BrandName + " - " + product.Name + " - FZone.al";
                        litProdCode.Text = product.Code;
                        litOriginalPrice.Text = product.OriginalPrice.ToString("N2");
                        litPrice.Text = product.OurPrice.ToString("N2");
                        litDescription.Text = product.Description;
                        litProdName.Text = product.Name;
                        litProdName2.Text = product.Name;
                        loadImages(product);

                        breadcrumb.ProductID = ProductID;
                        breadcrumb.ProductName = product.Name;
                        breadcrumb.CampaignName = campaign.BrandName;
                        breadcrumb.CampaignID = product.CampaignID.Value;

                        CATEGORY cat = product.CATEGORY.Where(c => c.ParentID != null).FirstOrDefault();
                        if (cat != null)
                        {
                            breadcrumb.CategoryID = cat.ID;
                            breadcrumb.CategoryName = cat.Name;
                        }

                        setCampaignImg(product, campaign);

                        loadProductAttributes();
                        if (product.Remaining == 0)
                        {
                            divWhenAvailable.Visible = false;
                            litNotAvailable.Visible = true;
                        }
                    }
                    else
                    {
                        Response.Redirect("/home/");
                    }
                }
                else
                {
                    Response.Redirect("/");
                }
            }
            else
            {
                Response.Redirect("/");
            }
        }

      private void setCampaignImg(PRODUCT product, CAMPAIGN campaign)
        {
            if (product.CampaignID.HasValue)
            {
                int campaignId = product.CampaignID.Value;
                if (campaign != null)
                {
                    CampaignID = campaign.ID;
                    imgBrandLogo.ImageUrl = Configuration.ImagesUploadPath + campaign.Logo;
                    imgBrandLogo.AlternateText = campaign.Name;

                    if (campaign.BRAND != null && campaign.BRAND.Shop.HasValue && campaign.BRAND.Shop.Value)
                    {
                        lblEstimatedDelivery.Text = Resources.Lang.ProductPickUpLabel + "<br />";
                        litEstimatedDates.Text = campaign.BRAND.Address;
                        lblInfo.Visible = true;
                        lblInfo.Text = "<br />" + Resources.Lang.ProductExclusivityLabel;
                    }
                    else
                    {
                        litEstimatedDates.Text = campaign.DeliveryStartDate.Value.ToString("dd/MM/yyyy") + " - " +
                            campaign.DeliveryEndDate.Value.ToString("dd/MM/yyyy");
                    }
                }
            }
        }

        private void loadImages(PRODUCT product)
        {
            if (product.PROD_IMAGES != null && product.PROD_IMAGES.Count > 0)
            {
                PROD_IMAGES principalImage = product.PROD_IMAGES.Where(i => i.Principal.HasValue && i.Principal.Value).FirstOrDefault();
                imgProdBig.ImageUrl = Configuration.ImagesUploadPath + principalImage.Image;
                lnkImage.HRef = Configuration.ImagesUploadPath + principalImage.LargeImage;
                hdnPreviousImage.Value = principalImage.ID.ToString();
                rptImages.DataSource = product.PROD_IMAGES.OrderByDescending(i => i.Principal);
                rptImages.DataBind();
            }
        }

        private void loadProductAttributes()
        {
            List<FZAttributeAvailability> list = ApplicationContext.Current.Products.GetProductAttributeValues(ProductID);
            list.Insert(0, new FZAttributeAvailability() { Id = 0, Value = Resources.Lang.SelectSizeLabel, Availability = 0 });
            ddlSize.DataSource = list;
            ddlSize.DataBind();
            ddlSize.SelectedValue = "0";
            anotateSizeItems(list, ddlSize);
        }

        /// <summary>
        /// Anotates items of the size list with availability information and disables the items with no availability
        /// </summary>
        /// <param name="items"></param>
        /// <param name="ddlSize"></param>
        private void anotateSizeItems(List<FZAttributeAvailability> items, DropDownList ddlSize)
        {
            var lessThanItems = items.Where(x => x.Availability < FashionZone.BL.Configuration.MaxOrderQuantityPerProduct);
            foreach (var item in lessThanItems)
            {
                ListItem lItem = ddlSize.Items.FindByValue(item.Id.ToString());
                if (item.Availability > 0)
                {
                    lItem.Text = lItem.Text + " - " + Resources.Lang.OnlyLabel + " " + item.Availability;
                }
                else
                {
                    lItem.Attributes.Add("style", "color:gray;");
                    lItem.Attributes.Add("disabled", "true");
                    if (item.Value != Resources.Lang.SelectSizeLabel)
                    {
                        lItem.Text = lItem.Text + " - " + Resources.Lang.TerminatedLabel;
                    }
                    lItem.Value = "-1";
                }
            }
        }

        protected void ddlSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            int prodAttrId;
            if (ddl.SelectedValue != "-1")
            {
                if (Int32.TryParse(ddl.SelectedValue, out prodAttrId))
                {
                    List<int> qtyList;
                    PRODUCT_ATTRIBUTE prodAttr = ApplicationContext.Current.Products.GetProductAvailability(prodAttrId, out qtyList);

                    Version = prodAttr.Version;
                    ddlQuantity.Enabled = true;
                    ddlQuantity.DataSource = qtyList;
                    ddlQuantity.DataBind();

                    lnkAddToBasket.Enabled = true;
                    updPanelDDL.Update();
                }
            }
        }

        protected void lnkAddToBasket_Click(object sender, EventArgs e)
        {
            string CartID = String.Empty;
            
            Guid g = Guid.NewGuid();
            UniqueIdGenerator unique = UniqueIdGenerator.GetInstance();
            string cartId = unique.GetBase32UniqueId(g.ToByteArray(), 20).ToLower();
            if (CartSession == null || CartSession.Count == 0)
            {
                
                CartID = cartId;
                CartSession = new List<SessionCart>();
            }
            else
            {
                List<SessionCart> cSession = CartSession.OrderByDescending(c => c.DateAdded).ToList();
                SessionCart sessionCart = cSession.First();
                if (sessionCart.DateAdded.AddMinutes(Configuration.CartExpirationValue) < DateTime.Now)
                {
                    RefreshCart();
                    CartID = cartId;
                }
                else
                {
                    CartID = CartSession.First().Id;
                }
            }

            SHOPPING_CART cart = new SHOPPING_CART();
            cart.ID = CartID;
            cart.FrontEnd = true;
            cart.CampaignID = CampaignID;
            cart.CustomerID = CurrentCustomer.Id;
            cart.DateAdded = DateTime.Now;
            cart.ProductID = ProductID;
            int num = 0;

            if (!Int32.TryParse(ddlSize.SelectedValue, out num))
            {
                return;
            }
            cart.ProdAttrID = num;

            num = 0;
            if (!Int32.TryParse(ddlQuantity.SelectedValue, out num) || num == 0)
            {
                return;
            }
            cart.Quantity = num;

            
            
            // the versions list of lists is created each time the product popup is shown, and destroyed each time it is closed
            if (Version != null)
            {
                cart.ProductAttributeVersion = Version;
            }
            else
            {
                throw new ApplicationException("Session is compromised! Cannot proceed.");
            }

            SessionCart sC;
            try
            {
                // already in the cart
                if (CartSession != null && CartSession.Count > 0 && (sC = CartSession.Find(c => c.ProductAttributeId == cart.ProdAttrID)) != null)
                {
                    // sum with old quantity
                    cart.Quantity += sC.Quantity;
                    ApplicationContext.Current.Carts.Update(cart, sC.Quantity);

                    // updating session with last quantity and last prod-attr version
                    sC.Quantity = cart.Quantity;
                    sC.ProductAttributeVersion = cart.ProductAttributeVersion;
                }
                else
                {
                    ApplicationContext.Current.Carts.Insert(cart);
                    sC = new SessionCart(cart);
                    CartSession.Add(sC);
                }
                TotalAmount = ApplicationContext.Current.Carts.GetShoppingCartTotalAmount(CartID);
            }
            catch (Exception ex)
            {
                //TODO log error
                Log(ex, ex.Message, ex.StackTrace, "Product.AddToCart");

                List<int> qtyList;
                PRODUCT_ATTRIBUTE prodAttr = ApplicationContext.Current.Products.GetProductAvailability(cart.ProdAttrID, out qtyList);

                Version = prodAttr.Version;
                
                ddlQuantity.DataSource = qtyList;
                ddlQuantity.DataBind();
                if (!qtyList.Contains(cart.Quantity))
                {
                    lblMessage.Text = Resources.Lang.InsufficientAvailabilityMessage;
                }
                if (qtyList.Count == 0)
                {
                    ddlQuantity.Enabled = false;
                    lnkAddToBasket.Enabled = false;
                    loadProductAttributes();
                }
                //refreshing the size ddl
                loadProductAttributes();
                updPanelDDL.Update();
                return;
            }

            
            Version = null;
            Response.Redirect("/cart/mycart/");
        }
    }
}