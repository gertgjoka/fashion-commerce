using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.BL.Util;
using FashionZone.DataLayer.Model;
using Resources;
using System.Text;

namespace FashionZone.FE.Secure.Personal
{
    public partial class inviteFriend : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //MasterPage set cssClass
            ((SiteMaster)Master).SetImgBackground("", "ContentIII");
            base.Page_Load(sender, e);
        }

        protected void lnkRegister_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (IsValid)
                Save();
        }

        private void Save()
        {
            string[] mails = txtInvitedMail.Text.Split(',');
            int count = 0;
            List<string> alreadyInvitedMails = new List<string>();
            List<string> alreadyRegisteredMails = new List<string>();
            try
            {
                foreach (var mail in mails)
                {
                    var newInvt = new INVITATION();
                    if (this.CurrentCustomer != null)
                    {
                        int idC = CurrentCustomer.Id;
                        newInvt.CustomerID = idC;
                        newInvt.Registered = false;
                        //newInvt.RegistrationDate = DateTime.Now;
                        newInvt.InvitedMail = mail;

                        if (ApplicationContext.Current.Customers.GetByEmail(mail) != null)
                        {
                            alreadyRegisteredMails.Add(mail);
                            continue;
                        }

                        List<INVITATION> invites = ApplicationContext.Current.Invitations.GetAllInvitationOfCustomer(CurrentCustomer.Id);

                        if (invites.Where(i => i.InvitedMail == mail).FirstOrDefault() != null)
                        {
                            alreadyInvitedMails.Add(mail);
                            continue;
                        }

                        ApplicationContext.Current.Invitations.Insert(newInvt);

                        count++;

                        SendInvite(this.CurrentCustomer, newInvt.ID, mail, CurrentCustomer.FullName);
                    }
                }
                StringBuilder builder = new StringBuilder();
                if (count > 0)
                {
                    builder.Append(Lang.InviteSuccessful);
                }
                if (alreadyRegisteredMails.Count > 0)
                {
                    builder.Append( "<br />" + Resources.Lang.AlreadyRegisteredMailLabel + "<br />");

                    foreach (string email in alreadyRegisteredMails)
                    {
                        builder.Append(email + " ");
                    }
                }

                if (alreadyInvitedMails.Count > 0)
                {
                    builder.Append("<br />" + Resources.Lang.EmailAlreadyInvited + "<br />");

                    foreach (string email in alreadyInvitedMails)
                    {
                        builder.Append(email + " ");
                    }
                }

                litError.Text = builder.ToString();
            }
            catch (Exception ex)
            {
                //TODO log
                Log(ex, ex.Message, ex.StackTrace, "Invite");
                litError.Text = Lang.ErrorVerifiedLabel;
            }
        }

        private void SendInvite(SessionCustomer customer, int idInvi, string mailTo, string fullName)
        {
            string custEncId = Encryption.Encrypt(customer.Id.ToString());
            string invEncId = Encryption.Encrypt(idInvi.ToString());
            string sub = string.Format("{0} {1}", fullName, Lang.InviteSubjectMailLabel);
            string url = string.Format("{0}/register/{1}/{2}", Configuration.DeploymentURL, custEncId, invEncId);
            
            BL.Util.Mailer.SendInvite(customer, url, mailTo, sub, Lang.InviteBody2MailLabel);

        }
    }
}