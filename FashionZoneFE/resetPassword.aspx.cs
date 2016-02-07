using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;

namespace FashionZone.FE
{
    public partial class resetPassword : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Response.Redirect("/home");
            }
            ((PublicMaster)Master).SetImgBackground("", "ContentIII");
        }

        protected void lnkPass_Click(object sender, EventArgs e)
        {
            string email = UserName.Text;

            try
            {
                CUSTOMER customer = ApplicationContext.Current.Customers.GetByEmail(email);

                if (customer == null)
                {
                    lblResult.Text = Resources.Lang.EmailNotExisting;
                    return;
                }

                string encEmail = Encryption.Encrypt(email);
                string encPass = Encryption.Encrypt(customer.Password);


                string sub = string.Format("{0}", Resources.Lang.PasswordMailSubject);
                string url = string.Format("<a href=\"http://www.fzone.al/password/{0}/{1}\">www.fzone.al</a>", encEmail, encPass);
                string body = string.Format("<b>{0}</b><br /> {1} {2} <br /> {3}",
                    Resources.Lang.PasswordMailSubject, customer.Name + " " + customer.Surname, Resources.Lang.PasswordLinkClick, url);
                BL.Util.Mailer.SendMailGeneric(email, sub, body);

                lblResult.Text = Resources.Lang.PasswordLinkSent;
            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "ResetPassword");
                lblResult.Text = Resources.Lang.ErrorVerifiedLabel;
            }
        }
    }
}