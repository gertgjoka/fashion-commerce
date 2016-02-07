using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using FashionZone.FE.Utils;
using FashionZone.BL.Util;
using System.Web.Security;

namespace FashionZone.FE
{
    public partial class password : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Response.Redirect("/home");
            }

            ((PublicMaster)Master).SetImgBackground("", "ContentIII");
        }

        protected void lnkRegister_Click(object sender, EventArgs e)
        {
            if (RouteData.Values["uid"] != null && RouteData.Values["pid"] != null)
            {
                string user = RouteData.Values["uid"].ToString();
                string pass = RouteData.Values["pid"].ToString();
                if (!String.IsNullOrWhiteSpace(user) && !String.IsNullOrWhiteSpace(pass))
                {
                    try
                    {
                        string clearUser = Encryption.Decrypt(user);
                        string clearPass = Encryption.Decrypt(pass);

                        if (ApplicationContext.Current.Customers.Validate(clearUser, clearPass))
                        {
                            string newPass =  FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, Configuration.PasswordHashMethod).ToLower();
                            ApplicationContext.Current.Customers.ChangePassword(clearUser, clearPass, newPass);

                            CurrentCustomer = new SessionCustomer(ApplicationContext.Current.Customers.GetByEmail(clearUser));

                            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                            FormsAuthentication.SetAuthCookie(clearUser, false);

                            Response.Redirect("/home");
                        }
                    }
                    catch (System.Threading.ThreadAbortException ex)
                    {
                    }
                    catch (Exception ex)
                    {
                        Log(ex, ex.Message, ex.StackTrace, "Password");
                        lblResult.Text = Resources.Lang.ErrorVerifiedLabel;
                    }
                }
            }
        }
    }
}