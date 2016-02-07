using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FashionZone.FE
{
    public partial class PublicMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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