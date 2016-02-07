using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FashionZone.FE
{
    public partial class login : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated || facebookAuthenticate())
            {
                if (!String.IsNullOrWhiteSpace(Request["ReturnUrl"]))
                {
                    Response.Redirect(Request["ReturnUrl"]);
                }
                else
                {
                    Response.Redirect("/home");
                }
            }
            ((PublicMaster)Master).SetImgBackground("", "ContentIII");
        }
    }
}