using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace FashionZone.FE.Public
{
    public partial class contactUs : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((PublicMaster)Master).SetImgBackground("", "ContentIII");
        }

        protected void lnkRegister_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    string mailTo = ConfigurationManager.AppSettings["MailContact"];
                    string body = string.Format("Motivi: {0}<br />Teksti: {1}<br /><br />Kerkesa e shkruar nga: {2}", ddlMotiv.SelectedValue, txtText.Text, txtEmail.Text);
                    FashionZone.BL.Util.Mailer.SendMailGenericMailFrom(mailTo, txtEmail.Text, "Kerkese kontakti!", body, MailPriority.High);
                    litError.Text = Resources.Lang.ContactRequestSent;
                }
                catch (Exception ex)
                {
                    Log(ex, ex.Message, ex.StackTrace, "Contact");
                    litError.Text = Resources.Lang.ErrorVerifiedLabel;
                }

            }
        }
    }
}