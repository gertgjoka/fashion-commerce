using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace FashionZone.FE
{
    public partial class _default : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //UnobtrusiveValidationMode 
            if (Page.User.Identity.IsAuthenticated || facebookAuthenticate())
            {
                Response.Redirect("/home/");
            }
        }

      
        protected void languageLinkButton_Click(object sender, EventArgs e)
        {
            ImageButton languageButton = sender as ImageButton;

            Session["Culture"] = languageButton.CommandArgument;

            // Reload last requested page with new culture.
            Response.Redirect(Request.Path);
        }
    }
}