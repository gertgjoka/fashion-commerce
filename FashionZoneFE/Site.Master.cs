using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
namespace FashionZone.FE
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public Literal CartNumber
        {
            get { return litCarrello; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void loggingOut(object sender, EventArgs e)
        {
            List<SessionCart> cart = Session["CartSession"] as List<SessionCart>;
            if (cart != null && cart.Count > 0)
            {
                ApplicationContext.Current.Carts.DeleteShoppingCart(cart.First().Id);
            }

            Session.Abandon();

            if (Session["accesstoken"] != null && !String.IsNullOrWhiteSpace(Session["accesstoken"].ToString()))
            {
                string url = String.Format("https://www.facebook.com/logout.php?next={0}&access_token={1}", Configuration.DeploymentURL, Session["accesstoken"].ToString());

                Response.Redirect(url);
            }
        }

        protected void languageLinkButton_Click(object sender, EventArgs e)
        {
            LinkButton languageButton = sender as LinkButton;

            Session["Culture"] = languageButton.CommandArgument;

            // Reload last requested page with new culture.
            Response.Redirect(Request.Path);
        }

        public void SetImgBackground(string imgPath, string cssClass)
        {
            if (!string.IsNullOrEmpty(imgPath))
                divMainContent.Attributes["style"] = "background-image:url('" + imgPath + "');";
            divMainContent.Attributes["class"] = cssClass;
        }
    }
}
