using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using FashionZone.FE.Utils;
using System.Data;

namespace FashionZone.FE.CustomControl
{
    public partial class cart : System.Web.UI.UserControl
    {
        public event EventHandler NeedRefresh;

        public List<SessionCart> CartSession
        {
            get
            {
                return ((Secure.cart.mycart)Page).CartSession;
            }
            set
            {
                ((Secure.cart.mycart)Page).CartSession = value;
            }
        }

        public SessionCustomer CurrentCustomer
        {
            get
            {
                return ((Secure.cart.mycart)Page).CurrentCustomer;
            }
            set
            {
                ((Secure.cart.mycart)Page).CurrentCustomer = value;
            }
        }

        /// <summary>
        /// Empties the cart and creates a new Guid
        /// </summary>
        public void Destroy()
        {
            //CartSession = null;
            //Versions = null;
            //CartID = Guid.NewGuid().ToString();
            //DataBind(null);
            //NeedRefresh(null, null);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // refreshing session
                if (CartSession != null && CartSession.Count > 0)
                {
                    DataBind();
                }
                else
                {
                    lblMessage.Text = Resources.Lang.EmptyCartLabel;
                }
            }
        }

        public void DataBind()
        {
            List<SHOPPING_CART> carts = ApplicationContext.Current.Carts.GetShoppingCartItems(CartSession.First().Id);

            decimal total = 0;
            int i = 0;
            CartSession = new List<SessionCart>();
            if (carts != null)
            {
                foreach (SHOPPING_CART cart in carts)
                {
                    total += cart.Amount.Value;
                    i += cart.Quantity;
                    SessionCart sC = new SessionCart(cart);
                    CartSession.Add(sC);
                }
            }
            TotalAmount = total;
            rptDetails.DataSource = carts;
            rptDetails.DataBind();

            ((Secure.cart.mycart)Page).RefreshCartNumber();

            if (NeedRefresh != null)
            {
                NeedRefresh(null, null);
            }
        }

        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            // we need only items, footer and header aren't considered
            if (e.Item.ItemType == ListItemType.Footer)
            {
                // total amount in footer
                Label lblSubTotal = (Label)item.FindControl("lblSubTotal");
                lblSubTotal.Text = TotalAmount.Value.ToString("N2");

            }
        }

        public decimal? TotalAmount
        {
            get
            {
                if (Session["TotalAmount"] != null)
                {
                    return (decimal)Session["TotalAmount"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                Session["TotalAmount"] = value;
            }
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
                            ddlQty.Visible = true;
                            lnk.Visible = false;

                            List<int> qtyList;
                            PRODUCT_ATTRIBUTE prodAttr = ApplicationContext.Current.Products.GetProductAvailability(prodAttrID, out qtyList, Int32.Parse(lnk.Text));

                            // updating version
                            SessionCart cart = CartSession.Find(c => c.ProductAttributeId == prodAttrID);
                            cart.ProductAttributeVersion = prodAttr.Version;
                            ddlQty.DataSource = qtyList;
                            ddlQty.DataBind();
                            ddlQty.SelectedValue = lnk.Text;
                        }
                        else if (lnk.ID == "lnkRemove")
                        {

                            ApplicationContext.Current.Carts.DeleteById(CartSession.First().Id, prodAttrID);
                            DataBind();
                        }
                        if (CartSession == null || CartSession.Count == 0)
                        {
                            lblMessage.Text = Resources.Lang.EmptyCartLabel;
                            rptDetails.Visible = false;
                        }
                        else
                        {
                            lblMessage.Text = String.Empty;
                        }
                    }
                }
            }
        }

        private void updAvailListInOptimisticScenario(DropDownList ddl, int prodAttrId, int alreadyInCart, int newQuantity, LinkButton lnk)
        {
            List<int> qtyList;
            PRODUCT_ATTRIBUTE prodAttr = ApplicationContext.Current.Products.GetProductAvailability(prodAttrId, out qtyList, alreadyInCart);

            if (!qtyList.Contains(newQuantity))
            {
                ddl.SelectedValue = alreadyInCart.ToString();
                lnk.Text = alreadyInCart.ToString();
                lblMessage.Text = Resources.Lang.InsufficientAvailabilityMessage;
                CartSession.Find(c => c.ProductAttributeId == prodAttrId).Quantity = alreadyInCart;
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
                    SessionCart cart = CartSession.Find(c => c.ProductAttributeId == prodAttrId);
                    if (cart != null)
                    {
                        // we already have the right version in the session variable cartsession
                        int oldQuantity = cart.Quantity;
                        SHOPPING_CART shopping = new SHOPPING_CART() { ID = cart.Id, ProdAttrID = prodAttrId };
                        // session var
                        cart.Quantity = selectedQty;
                        cart.DateAdded = DateTime.Now;

                        shopping.Quantity = selectedQty;
                        shopping.DateAdded = DateTime.Now;
                        shopping.ProductAttributeVersion = cart.ProductAttributeVersion;

                        LinkButton lnk = (LinkButton)item.FindControl("lnkEdit");

                        try
                        {
                            ApplicationContext.Current.Carts.Update(shopping, oldQuantity);
                            lblMessage.Text = String.Empty;
                        }
                        catch (Exception ex)
                        {
                            BasePage.Log(ex, ex.Message, ex.StackTrace, "Cart Control");
                            updAvailListInOptimisticScenario(ddl, prodAttrId, oldQuantity, selectedQty, lnk);
                        }
                        finally
                        {
                            DataBind();
                            ddl.Visible = false;
                            lnk.Visible = true;
                        }
                    }
                }
            }
        }
    }
}