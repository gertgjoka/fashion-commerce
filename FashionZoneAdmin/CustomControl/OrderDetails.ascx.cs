using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using FashionZone.Admin.Secure.Order;
using FashionZone.BL.Util;

namespace FashionZone.Admin.CustomControl
{
    public partial class OrderDetails : System.Web.UI.UserControl
    {
        public event EventHandler NeedRefresh;

        #region Properties in session

        // temporary session variable used during product selection in the popup
        public SortedList<int, SortedList<int, byte[]>> Versions
        {
            get
            {
                if (Session["ProductAttributeVersions"] != null)
                {
                    return (SortedList<int, SortedList<int, byte[]>>)Session["ProductAttributeVersions"];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                Session["ProductAttributeVersions"] = value;
            }
        }

        public List<SHOPPING_CART> CartSession
        {
            get
            {
                if (Session["CartSession"] != null)
                {
                    return (List<SHOPPING_CART>)Session["CartSession"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                Session["CartSession"] = value;
            }
        }

        #endregion

        public bool ReadOnly
        {
            get
            {
                bool readOnly;
                if (ViewState["ReadOnly"] != null && Boolean.TryParse(ViewState["ReadOnly"].ToString(), out readOnly))
                    return readOnly;
                else
                    return false;
            }
            set
            {
                ViewState["ReadOnly"] = value;
            }
        }

        public String CartID
        {
            get
            {
                if (ViewState["CartID"] != null)
                    return ViewState["CartID"].ToString();
                else
                    return null;
            }
            set
            {
                ViewState["CartID"] = value;
            }
        }

        /// <summary>
        /// Empties the cart and creates a new Guid
        /// </summary>
        public void Destroy()
        {
            CartSession = null;
            Versions = null;

            // deletes shopping cart from db and returns items to warehouse
            ApplicationContext.Current.Carts.DeleteShoppingCart(CartID);

            // generates a new id
            Guid g = Guid.NewGuid();
            UniqueIdGenerator unique = UniqueIdGenerator.GetInstance();
            string cartId = unique.GetBase32UniqueId(g.ToByteArray(), 20).ToLower();
            CartID = cartId;
            DataBind(null);
            NeedRefresh(null, null);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CartSession = null;
            }
        }

        /// <summary>
        /// Adds a new product to this session cart
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="CampaignID"></param>
        /// <param name="ProductId"></param>
        /// <param name="ProdAttrId"></param>
        /// <param name="Quantity"></param>
        public void AddProductToCart(int CustomerID, int CampaignID, int ProductId, int ProdAttrId, int Quantity, string BrandName)
        {
            if (CartID == null)
            {
                Guid g = Guid.NewGuid();
                UniqueIdGenerator unique = UniqueIdGenerator.GetInstance();
                string cartId = unique.GetBase32UniqueId(g.ToByteArray(), 20).ToLower();
                CartID = cartId;
            }

            SHOPPING_CART cart = new SHOPPING_CART();
            cart.ID = CartID;
            cart.FrontEnd = false;
            cart.CampaignID = CampaignID;
            cart.CustomerID = CustomerID;
            cart.DateAdded = DateTime.Now;
            cart.ProductID = ProductId;
            cart.ProdAttrID = ProdAttrId;
            cart.Quantity = Quantity;
            cart.BrandName = BrandName;

            // the versions list of lists is created each time the product popup is shown, and destroyed each time it is closed
            if (Versions != null)
            {
                cart.ProductAttributeVersion = Versions.Where(x => x.Key == ProductId).FirstOrDefault().Value.Where(y => y.Key == ProdAttrId).FirstOrDefault().Value;
            }
            else
            {
                throw new ApplicationException("Session is compromised! Cannot proceed.");
            }

            if (CartSession == null)
            {
                CartSession = new List<SHOPPING_CART>();
            }

            List<SHOPPING_CART> carts = CartSession;
            SHOPPING_CART sc;

            // already in the cart
            if (carts.Count > 0 && (sc = carts.Where(c => c.ID == cart.ID && c.ProdAttrID == cart.ProdAttrID).FirstOrDefault()) != null)
            {
                cart.Quantity += sc.Quantity;
                ApplicationContext.Current.Carts.Update(cart, sc.Quantity);

                // updating session with last quantity and last prod-attr version
                sc.Quantity = cart.Quantity;
                sc.ProductAttributeVersion = cart.ProductAttributeVersion;
                //sc = cart;
            }
            else
            {
                ApplicationContext.Current.Carts.Insert(cart);

                // already has the last version set from the context saving
                CartSession.Add(cart);
            }

            // refreshing session, optional
            //ApplicationContext.Current.Carts.GetShoppingCartItems(CartID);
            DataBind(CartSession);
        }

        public void DataBind(List<SHOPPING_CART> cart)
        {
            CartSession = cart;
            rptDetails.DataSource = cart;
            rptDetails.DataBind();
        }

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            // we need only items, footer and header aren't considered
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlSize = (DropDownList)item.FindControl("ddlSize");
                SHOPPING_CART cart = (SHOPPING_CART)item.DataItem;

                // loading size ddl, actually it's not possible to modify the choosen size
                List<FZAttributeAvailability> list; // = ApplicationContext.Current.Products.GetProductAttributeValues(cart.ProductID.Value);
                list = new List<FZAttributeAvailability>();
                list.Insert(0, new FZAttributeAvailability() { Id = cart.ProdAttrID, Value = cart.ProductAttribute });
                ddlSize.DataSource = list;
                ddlSize.DataBind();
                ddlSize.SelectedValue = cart.ProdAttrID.ToString();

                List<int> qtyList;
                PRODUCT_ATTRIBUTE prodAttr = ApplicationContext.Current.Products.GetProductAvailability(cart.ProdAttrID, out qtyList, cart.Quantity);

                DropDownList ddlQty = (DropDownList)item.FindControl("ddlQty");
                ddlQty.DataSource = qtyList;
                ddlQty.DataBind();
                ddlQty.SelectedValue = cart.Quantity.ToString();
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                // total amount in footer
                Label lblSubTotal = (Label)item.FindControl("lblSubTotal");
                lblSubTotal.Text = TotalAmount().ToString("N2");

            }
        }

        public decimal TotalAmount()
        {
            decimal total = 0;
            if (CartSession != null)
            {
                foreach (SHOPPING_CART cart in CartSession)
                {
                    total += cart.Amount.Value;
                }
            }
            return total;
        }

        protected void rptDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandSource is LinkButton)
            {
                LinkButton lnk = e.CommandSource as LinkButton;
                if (!String.IsNullOrWhiteSpace(lnk.CommandArgument))
                {
                    int prodAttrID;
                    if (Int32.TryParse(lnk.CommandArgument, out prodAttrID))
                    {

                        if (lnk.ID == "lnkEdit")
                        {
                            RepeaterItem item = (RepeaterItem)lnk.NamingContainer;
                            DropDownList ddlQty = (DropDownList)item.FindControl("ddlQty");
                            ddlQty.Enabled = true;
                            lnk.Enabled = false;
                        }
                        else if (lnk.ID == "lnkRemove")
                        {

                            ApplicationContext.Current.Carts.DeleteById(CartID, prodAttrID);

                            // refreshing session
                            CartSession = ApplicationContext.Current.Carts.GetShoppingCartItems(CartID);
                            DataBind(CartSession);
                        }
                        NeedRefresh(source, e);
                    }
                }
            }
        }

        protected void ddlQty_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            int selectedQty;
            if (Int32.TryParse(ddl.SelectedValue, out selectedQty))
            {
                RepeaterItem item = (RepeaterItem)ddl.NamingContainer;
                HiddenField field = (HiddenField)item.FindControl("ProdAttrID");
                int prodAttrId;
                if (Int32.TryParse(field.Value, out prodAttrId))
                {
                    SHOPPING_CART cart;
                    if ((cart = CartSession.Where(c => c.ID == CartID && c.ProdAttrID == prodAttrId).FirstOrDefault()) != null)
                    {
                        // we already have the right version in the session variable cartsession
                        int oldQuantity = cart.Quantity;
                        DateTime oldDate = cart.DateAdded;
                        cart.Quantity = selectedQty;
                        cart.DateAdded = DateTime.Now;
                        try
                        {
                            ApplicationContext.Current.Carts.Update(cart, oldQuantity);
                        }
                        catch (Exception ex)
                        {
                            OrderNew parent = this.Page as OrderNew;
                            parent.writeResult(ex.Message, true);

                            // reverse situation because of optimistic error
                            cart.Quantity = oldQuantity;
                            cart.DateAdded = oldDate;
                        }
                        // refresh session, optional
                        //CartSession = ApplicationContext.Current.Carts.GetShoppingCartItems(CartID);
                        DataBind(CartSession);
                        // notify parent for a partial refresh
                        NeedRefresh(sender, e);
                        ddl.Enabled = false;
                        LinkButton lnk = (LinkButton)item.FindControl("lnkEdit");
                        lnk.Enabled = false;
                    }
                }
            }
        }
    }
}