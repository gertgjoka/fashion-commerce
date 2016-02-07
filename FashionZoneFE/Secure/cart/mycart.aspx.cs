using FashionZone.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FashionZone.FE.Secure.cart
{
    public partial class mycart : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //MasterPage set cssClass
            ((SiteMaster)Master).SetImgBackground("", "ContentIII");
            if (!IsPostBack)
            {
                if (CartSession != null && CartSession.Count > 0)
                {
                    divAction.Visible = true;
                    divClock.Visible = true;
                    //updateTimer();
                }
            }
            base.Page_Load(sender, e);
        }

        protected void cart_NeedRefresh(object sender, EventArgs e)
        {
            if (CartSession != null && CartSession.Count > 0)
            {
                divAction.Visible = true;
                divClock.Visible = true;
                updateTimer();
            }
            else
            {
                divAction.Visible = false;
                divClock.Visible = false;
            }
        }

        protected void updateTimer()
        {
            int? exp = CartExpirationMinutes();
            if (exp.HasValue)
            {
                lblCartExp.Text = exp.Value.ToString();
                updMinutes.Update();
            }
        }

        protected void lnkCheckout_Click(object sender, EventArgs e)
        {
            RefreshCart();
            if (CartSession != null && CartSession.Count > 0)
            {
                Response.Redirect("/cart/checkout/");
            }
            else
            {
                //popup with message and redirect to home page.
                modalPopup.Show();
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            modalPopup.Hide();
            Response.Redirect("/home/");
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CartSession != null && CartSession.Count > 0)
                {
                    ApplicationContext.Current.Carts.DeleteShoppingCart(CartSession.First().Id);
                }
                CartSession = null;
                Response.Redirect("/home/");
            }
            catch (System.Threading.ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "Checkout - Cancel");
            }
        }
    }
}