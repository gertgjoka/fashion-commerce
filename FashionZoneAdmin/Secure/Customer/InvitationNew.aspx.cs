using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace FashionZone.Admin.Secure.Customer
{
    public partial class InvitationNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idInvit = Request["ID"];
            if (!IsPostBack)
            {
                INVITATION inv = null;
                int id;
                if (!String.IsNullOrWhiteSpace(idInvit) && Int32.TryParse(idInvit, out id) && id != 0)
                {
                    getInvitation(id);
                }
            }

        }

        private void getInvitation(int id)
        {
            var invitiation = ApplicationContext.Current.Invitations.GetById(id);
            if (invitiation != null)
            {
                txtCustomer.Text = invitiation.CustomerFullName;
                txtInvitedMail.Text = invitiation.InvitedMail;
                if (invitiation.Registered.HasValue)
                    chkRegistered.Checked = invitiation.Registered.Value;
                if (invitiation.RegistrationDate.HasValue)
                    txtDate.Text = invitiation.RegistrationDate.Value.ToString("dd/MM/yyyy hh:mm");
                hdnIdInvt.Value = invitiation.ID.ToString();
                hdnCustomerId.Value = invitiation.CustomerID.ToString();
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    int numValue;
                    if (int.TryParse(hdnIdInvt.Value, out numValue))
                        Update(numValue);
                    else
                        Save();
                }
                catch (Exception ex)
                {
                    writeResult(ex.Message, true);
                }
            }
        }

        private void Update(int numValue)
        {
            var updIntit = new INVITATION { ID = numValue };
            int custId;
            if (Int32.TryParse(hdnCustomerId.Value, out custId))
            {
                updIntit.CustomerID = custId;
                updIntit.InvitedMail = txtInvitedMail.Text;
                updIntit.Registered = chkRegistered.Checked;
                DateTime date;
                if (DateTime.TryParse(txtDate.Text, out date))
                    updIntit.RegistrationDate = date;
                ApplicationContext.Current.Invitations.Update(updIntit);
                writeResult("Update successful!", false);
            }
            else
            {
                writeResult("Error successful! No Customer data.", true);
            }
        }

        private void Save()
        {
            string[] mails = txtInvitedMail.Text.Split(';');
            int count = 0;
            foreach (var mail in mails)
            {
                var newInvt = new INVITATION();
                if (!string.IsNullOrEmpty(txtCustomer.Text) && !string.IsNullOrWhiteSpace(txtCustomer.Text))
                {
                    string[] nameBrday = txtCustomer.Text.Split('-');
                    if (nameBrday.Length > 2)
                    {
                        int idC;
                        if (Int32.TryParse(nameBrday[2], out idC))
                        {
                            var customer = ApplicationContext.Current.Customers.GetById(idC);
                            if (customer != null)
                            {
                                newInvt.CUSTOMER = customer;
                                newInvt.CustomerID = customer.ID;
                            }
                        }
                    }
                }
                newInvt.InvitedMail = mail;
                newInvt.Registered = chkRegistered.Checked;
                DateTime dataReg;
                if (DateTime.TryParse(txtDate.Text, out dataReg))
                    newInvt.RegistrationDate = dataReg;
                ApplicationContext.Current.Invitations.Insert(newInvt);
                count++;
            }
            writeResult(count > 1 ? count + " Inserts successful!" : "Insert successful!", false);
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            txtCustomer.Text = txtInvitedMail.Text = txtDate.Text = "";
            chkRegistered.Checked = false;
        }

        private void writeResult(string message, bool isError)
        {
            lblErrors.Visible = true;
            if (isError)
            {
                lblErrors.ForeColor = Color.Red;
                message = "Error occurred: " + message;
            }
            else
            {
                lblErrors.ForeColor = Color.Green;
            }
            lblErrors.Text = message;
        }
    }

}