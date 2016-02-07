using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using Resources;
using FashionZone.FE.CustomControl;
using System.Globalization;

namespace FashionZone.FE.Secure.Personal
{
    public partial class personalInfo : Utils.BasePage
    {
        public int AddrCounter
        {
            get
            {
                int id;
                if (ViewState["AddrCounter"] != null && Int32.TryParse(ViewState["AddrCounter"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["AddrCounter"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //MasterPage set cssClass
                ((SiteMaster)Master).SetImgBackground("", "ContentIII");
                if (CurrentCustomer != null)
                {
                    loadCustomerData();
                }
            }
            txtBirthday.Attributes.Add("readonly", "readonly");
            base.Page_Load(sender, e);
        }

        private void loadCustomerData()
        {
            if (CurrentCustomer != null)
            {
                CUSTOMER customer = ApplicationContext.Current.Customers.GetById(CurrentCustomer.Id);

                txtName.Text = customer.Name;
                txtSurname.Text = customer.Surname;
                if (customer.BirthDate.HasValue)
                {
                    txtBirthday.Text = customer.BirthDate.Value.ToString("dd/MM/yyyy");
                    calExBirthday.SelectedDate = customer.BirthDate.Value;
                }
                ddlGender.SelectedValue = customer.Gender;
                lblMailAdres.Text = customer.Email;
                txtMobile.Text = customer.Mobile;
                txtPhone.Text = customer.Telephone;


                if (customer.ADDRESS != null && customer.ADDRESS.Count != 0)
                {
                    loadAddresses(true, customer);
                }
                else
                {
                    AddrCounter = 0;
                }
            }
            else
            {
                Log(null, "CurrentCustomer null", String.Empty, "personalInfo load");
            }
        }

        protected void lnkModify_Click(object sender, EventArgs e)
        {
            Validate("regValidation");
            if (IsValid)
            {
                if (CurrentCustomer != null)
                {

                    var customer = new CUSTOMER() { ID = CurrentCustomer.Id };
                    try
                    {
                        customer.Name = txtName.Text;
                        customer.Surname = txtSurname.Text;
                        customer.Telephone = txtPhone.Text;
                        customer.Mobile = txtMobile.Text;
                        customer.Active = true;

                        CurrentCustomer.Name = customer.Name;
                        CurrentCustomer.Surname = customer.Surname;

                        DateTime date = new DateTime();
                        IFormatProvider formatProvider = new CultureInfo("it-IT");

                        if (!String.IsNullOrWhiteSpace(txtBirthday.Text))
                        {
                            DateTime.TryParse(txtBirthday.Text, formatProvider, DateTimeStyles.AdjustToUniversal, out date);
                            customer.BirthDate = date;
                        }

                        customer.Gender = ddlGender.SelectedValue;
                        ApplicationContext.Current.Customers.Update(customer);
                        litError.Text = Resources.Lang.DataSavedCorrectly;
                    }
                    catch (Exception ex)
                    {
                        //TODO log ex
                        Log(ex, ex.Message, ex.StackTrace, "PersonalInfo.Modify");
                        litError.Text = Resources.Lang.ErrorVerifiedLabel;
                    }
                }
            }
        }

        protected void lbtnShowEditPw_Click(object sender, EventArgs e)
        {
            pnlEditPW.Visible = true;
            lbtnShowEditPw.Visible = lblHidPw.Visible = false;
        }

        protected void lbtnSavePw_Click(object sender, EventArgs e)
        {
            Validate("rePwValidation");
            if (IsValid)
            {
                if (CurrentCustomer != null)
                {

                    try
                    {
                        string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtOldPass.Text, Configuration.PasswordHashMethod).ToLower();

                        if (!ApplicationContext.Current.Customers.Validate(User.Identity.Name, pass))
                        {
                            litError.Text = Resources.Lang.OldPassLabel + " " + Resources.Lang.NotValidLabel + ".";
                            return;
                        }

                        pnlEditPW.Visible = true;
                        lbtnShowEditMail.Visible = lblHidPw.Visible = false;
                        string newPass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtEditPw.Text, Configuration.PasswordHashMethod).ToLower();

                        ApplicationContext.Current.Customers.ChangePassword(User.Identity.Name, pass, newPass);
                        litError.Text = Resources.Lang.SavePwMsgLabel;
                        pnlEditPW.Visible = false;
                    }
                    catch (Exception ex)
                    {//TODO log ex
                        Log(ex, ex.Message, ex.StackTrace, "PersonalInfo.SavePw");
                        litError.Text = Resources.Lang.ErrorVerifiedLabel;
                    }

                }
            }
        }

        protected void lbtnShowEditMail_Click(object sender, EventArgs e)
        {
            panEditMail.Visible = true;
            lbtnShowEditMail.Visible = false;
        }

        protected void lbtnSaveMail_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (CurrentCustomer != null)
                {
                    var customer = ApplicationContext.Current.Customers.GetById(CurrentCustomer.Id);
                    if (customer != null)
                    {
                        if (String.IsNullOrWhiteSpace(txtNewMail.Text))
                        {
                            litError.Text = "Email " + Resources.Lang.NotValidLabel + ".";
                            return;
                        }

                        string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtEmailPassword.Text, Configuration.PasswordHashMethod).ToLower();

                        if (!ApplicationContext.Current.Customers.Validate(User.Identity.Name, pass))
                        {
                            litError.Text = Resources.Lang.PasswordLabel + " " + Resources.Lang.NotValidLabel + ".";
                            return;
                        }

                        customer.Email = txtNewMail.Text;

                        if (ApplicationContext.Current.Customers.GetByEmail(txtNewMail.Text) != null)
                        {
                            litError.Text = Resources.Lang.AlreadyRegisteredMailLabel;
                            return;
                        }
                        try
                        {
                            ApplicationContext.Current.Customers.Update(customer, false);
                            litError.Text = Resources.Lang.SaveMailLabel;
                            lblMailAdres.Text = txtNewMail.Text;
                            lblMailAdres.Visible = true;
                            panEditMail.Visible = false;

                            // re-authenticateuser with new email
                            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                            FormsAuthentication.SetAuthCookie(customer.Email, false);
                        }
                        catch (Exception ex)
                        {//TODO log ex
                            Log(ex, ex.Message, ex.StackTrace, "PersonalInfo.SaveMail");
                            litError.Text = Resources.Lang.ErrorVerifiedLabel;
                        }

                    }
                }
            }
        }

        #region Addresses

        private void loadAddresses(bool Load, CUSTOMER customer)
        {
            int i;
            address1.Visible = false;
            address2.Visible = false;
            address3.Visible = false;
            address4.Visible = false;
            address5.Visible = false;

            if (!Load)
            {
                address1.ResetControl(true);
                address2.ResetControl(true);
                address3.ResetControl(true);
                address4.ResetControl(true);
                address5.ResetControl(true);
            }

            for (i = 0; i < customer.ADDRESS.Count; i++)
            {
                CustomerAddress custAddr = (CustomerAddress)updPanAddress.FindControl("address" + (i + 1));
                custAddr.AddrID = customer.ADDRESS.ElementAt(i).ID;
                custAddr.ReloadControl(false);
                custAddr.Visible = true;
            }
            AddrCounter = i;
        }

        protected void address_OnAddressDelete(object sender, EventArgs e)
        {
            CustomerAddress custAddr = (CustomerAddress)sender;
            custAddr.ResetControl(true);
            custAddr.Visible = false;

            // refreshing session
            if (CurrentCustomer != null)
            {
                var customer = ApplicationContext.Current.Customers.GetById(CurrentCustomer.Id);
                loadAddresses(false, customer);

                resetErrors();
            }
            else
            {

            }
        }

        protected void lnkAddAddress_Click(object sender, EventArgs e)
        {
            if (AddrCounter == 5)
            {
                lblErrors.Text = Resources.Lang.MaximumAddressLabel;
                return;
            }
            if (AddrCounter > 0)
            {
                // checking if previous address is saved
                CustomerAddress prevAddr = (CustomerAddress)updPanAddress.FindControl("address" + AddrCounter);
                prevAddr.ResetErrors();
                if (prevAddr.AddrID == 0)
                {
                    //writeError("You must save actual address first.");
                    return;
                }
            }
            AddrCounter++;
            CustomerAddress custAddr = (CustomerAddress)updPanAddress.FindControl("address" + AddrCounter);
            custAddr.AddrID = 0;
            custAddr.Visible = true;
        }

        protected void address_OnAddressAsyncRemove(object sender, EventArgs e)
        {
            AddrCounter--;
        }

        protected void address_Saving(object sender, EventArgs e)
        {
            // refreshing session
            //CurrentCustomer = ApplicationContext.Current.Customers.GetById(CurrentCustomer.ID);
        }

        private void resetErrors()
        {
            lblErrors.Text = "";
            lblErrors.Visible = false;
        }

        #endregion
    }
}