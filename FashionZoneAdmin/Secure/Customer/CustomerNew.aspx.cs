using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using System.Drawing;
using System.Web.Security;
using Configuration = FashionZone.Admin.Utils.Configuration;
using FashionZone.Admin.Controls;
using FashionZone.Admin.Utils;

namespace FashionZone.Admin.Secure.Customer
{
    public partial class CustomerNew : Page
    {
        #region Fields
        private const string fakePassChars = "11111";

        protected int CustomerID
        {
            get
            {
                int id;
                if (ViewState["CustomerID"] != null && Int32.TryParse(ViewState["CustomerID"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["CustomerID"] = value;
            }
        }
        #endregion

        #region Page utils

        protected void Page_Load(object sender, EventArgs e)
        {
            string idCustomer = Request["ID"];
            int id;
            if (!IsPostBack)
            {
                if (!String.IsNullOrWhiteSpace(idCustomer) && Int32.TryParse(idCustomer, out id) && id != 0)
                {
                    if (LoadCustomer(id))
                    {
                        CustomerID = id;

                        dataBindBonus(gridBonus.SortExp, 0);
                        dataBindInvitation(gridInvitation.SortExp, 0);
                    }
                    else
                    {
                        writeError("User was not found.");
                    }
                }
                else
                {
                    //New customer, one address control should be visible
                    address1.Visible = true;
                }
                setAddrCustomerId();

            }
            
            txtBirthDate.Attributes.Add("readonly", "readonly");
            txtRegDate.Attributes.Add("readonly", "readonly");
        }

        private void writeError(string errorMessage)
        {
            lblErrors.Visible = true;
            lblErrors.ForeColor = Color.Red;
            lblErrors.Text = "Error occurred: " + errorMessage;
        }

        private void resetErrors()
        {
            lblErrors.Text = "";
            lblErrors.Visible = false;
        }

        private void resetFields()
        {
            txtEmail.Text = "";
            txtName.Text = "";
            txtSurname.Text = "";
            txtPhone.Text = "";
            txtMobile.Text = "";
            txtPass.Attributes.Add("value", String.Empty);
            txtPass2.Attributes.Add("value", String.Empty);
            txtBirthDate.Text = "";
            chkActive.Checked = false;
            chkNewsletter.Checked = false;
            btnMale.Checked = true;
            //ID.Value = String.Empty;
            //TODO decide if new customer or not
        }

        #endregion

        #region Customer handling

        private bool LoadCustomer(int id)
        {
            CUSTOMER customer = null;
            try
            {
                customer = ApplicationContext.Current.Customers.GetById(id);
                if (customer != null)
                {
                    txtName.Text = customer.Name;
                    txtSurname.Text = customer.Surname;
                    txtEmail.Text = customer.Email;
                    txtPhone.Text = customer.Telephone;
                    txtMobile.Text = customer.Mobile;

                    chkActive.Checked = customer.Active;
                    if (customer.Newsletter.HasValue)
                    {
                        chkNewsletter.Checked = customer.Newsletter.Value;
                    }
                    if (customer.Gender == "M")
                    {
                        btnMale.Checked = true;
                    }
                    else if (customer.Gender == "F")
                    {
                        btnFemale.Checked = true;
                    }
                    if (customer.BirthDate.HasValue)
                    {
                        txtBirthDate.Text = customer.BirthDate.Value.ToString("dd/MM/yyyy");
                    }

                    if (customer.RegistrationDate.HasValue)
                    {
                        txtRegDate.Text = customer.RegistrationDate.Value.ToString("dd/MM/yyyy");
                    }

                    txtPass.Attributes.Add("value", fakePassChars);
                    txtPass2.Attributes.Add("value", fakePassChars);

                    if (customer.ADDRESS != null && customer.ADDRESS.Count != 0)
                    {
                        loadAddresses(customer.ADDRESS, id, true);
                    }
                    else
                    {
                        addressCount.Value = "0";
                    }

                    if (customer.InvitedFrom.HasValue)
                    {
                        litInvitedFrom.Text = customer.CUSTOMER2.Name + " " + customer.CUSTOMER2.Surname;
                        aInvitedFrom.HRef = "CustomerNew.aspx?ID=" + customer.InvitedFrom.Value;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                writeError(ex.Message);
                return false;
            }
            return true;
        }

        private void Save()
        {
            CUSTOMER customer = new CUSTOMER();
            customer.Name = txtName.Text;
            customer.Surname = txtSurname.Text;
            customer.Email = txtEmail.Text;
            customer.Active = chkActive.Checked;
            customer.Newsletter = chkNewsletter.Checked;
            customer.Telephone = txtPhone.Text;
            customer.Mobile = txtMobile.Text;

            if (btnMale.Checked)
            {
                customer.Gender = "M";
            }
            else if (btnFemale.Checked)
            {
                customer.Gender = "F";
            }
            bool ignorePass = false;
            if (txtPass.Text != fakePassChars)
            {
                customer.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Text, Configuration.PasswordHashMethod).ToLower();
            }
            else
            {
                ignorePass = true;
            }

            if (!String.IsNullOrWhiteSpace(txtBirthDate.Text))
            {
                customer.BirthDate = DateTime.Parse(txtBirthDate.Text);
            }

            // not a new customer
            int id = 0;
            lblErrors.Visible = true;
            lblErrors.ForeColor = Color.Green;
            string operation = String.Empty;
            try
            {
                if (CustomerID != 0)
                {
                    customer.ID = CustomerID;
                    ApplicationContext.Current.Customers.Update(customer, ignorePass, true);
                    operation = "updated";
                }
                else
                {
                    // new customer, set registration date to today
                    customer.RegistrationDate = DateTime.Today.Date;
                    ApplicationContext.Current.Customers.Insert(customer);
                    operation = "inserted";
                    CustomerID = customer.ID;
                    setAddrCustomerId();
                }


                for (int i = 0; i < Int32.Parse(addressCount.Value); i++)
                {
                    CustomerAddress custAddr = (CustomerAddress)tabAddresses.FindControl("address" + (i + 1));
                    custAddr.Save(true);
                }
                lblErrors.Text = "Customer " + operation + " correctly.";
            }
            catch (Exception ex)
            {
                writeError(ex.Message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetFields();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Save();

                //Reset password to fake, otherwise the textbox will be empty
                txtPass.Attributes.Add("value", fakePassChars);
                txtPass2.Attributes.Add("value", fakePassChars);
            }
            else
            {
                tabCustContainer.ActiveTabIndex = 0;
            }
        }

        #endregion

        #region Addresses

        private void loadAddresses(ICollection<ADDRESS> addresses, int id, bool Load)
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

            for (i = 0; i < addresses.Count; i++)
            {
                CustomerAddress custAddr = (CustomerAddress)tabAddresses.FindControl("address" + (i + 1));
                custAddr.CustomerID = id;
                custAddr.AddrID = addresses.ElementAt(i).ID;
                custAddr.ReloadControl(false);
                custAddr.Visible = true;
            }
            addressCount.Value = (i).ToString();
        }

        private void setAddrCustomerId()
        {
            // Set of customer ID and handling of the bubbled event
            address1.CustomerID = CustomerID;
            address2.CustomerID = CustomerID;
            address3.CustomerID = CustomerID;
            address4.CustomerID = CustomerID;
            address5.CustomerID = CustomerID;
        }

        protected void address_OnAddressDelete(object sender, EventArgs e)
        {
            if (CustomerID != 0)
            {
                CustomerAddress custAddr = (CustomerAddress)sender;
                custAddr.Visible = false;
                custAddr.AddrID = 0;
                CUSTOMER customer = ApplicationContext.Current.Customers.GetById(CustomerID);
                loadAddresses(customer.ADDRESS, CustomerID, false);
            }
            resetErrors();
        }

        protected void lnkAddAddress_Click(object sender, EventArgs e)
        {
            if (addressCount.Value == "5")
            {
                writeError("A maximum of 5 addresses is allowed");
                return;
            }

            int count;
            if (Int32.TryParse(addressCount.Value, out count))
            {
                if (count > 0)
                {
                    // checking if previous address is saved
                    CustomerAddress prevAddr = (CustomerAddress)tabAddresses.FindControl("address" + count);
                    prevAddr.ResetErrors();
                    if (prevAddr.AddrID == 0)
                    {
                        writeError("You must save actual address first.");
                        return;
                    }
                }
                count++;
                CustomerAddress custAddr = (CustomerAddress)tabAddresses.FindControl("address" + count);
                custAddr.CustomerID = CustomerID;
                custAddr.AddrID = 0;
                custAddr.Visible = true;
                addressCount.Value = count.ToString();
            }
        }

        protected void address_OnAddressAsyncRemove(object sender, EventArgs e)
        {
            int count;
            if (Int32.TryParse(addressCount.Value, out count) && count > 0)
            {
                count--;
                addressCount.Value = count.ToString();
            }
        }

        #endregion

        protected void gridCustomer_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (gridInvitation.SortExp == e.SortExpression)
            {
                gridInvitation.ChangeSorting();
            }
            else
            {
                gridInvitation.SortOrder = FashionZone.BL.Util.SortDirection.Ascending;
            }

            gridInvitation.SortExp = e.SortExpression;
            dataBindInvitation(e.SortExpression, gridInvitation.CurrentPageIndex);
        }

        protected void gridCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridInvitation.CurrentPageIndex = e.NewPageIndex;
            dataBindInvitation(gridInvitation.SortExp, e.NewPageIndex);
        }

        protected void dataBindInvitation(string sortExp, int pageIndex)
        {
            try
            {
                int totalRecords = 0;
                gridInvitation.PageSize = Utils.Configuration.PageSize;
                int id = 0;
                INVITATION invitation = new INVITATION() { CustomerID = this.CustomerID };
                List<INVITATION> list = ApplicationContext.Current.Invitations.Search(invitation, Utils.Configuration.PageSize, pageIndex, out totalRecords, sortExp, gridBonus.SortOrder);
                gridInvitation.DataSource = list;
                gridInvitation.CustomCustomVirtualItemCount = totalRecords;
                gridInvitation.DataBind();
            }
            catch (Exception e)
            {
                writeError(e.Message);
            }
        }

        protected void gridBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridBonus.CurrentPageIndex = e.NewPageIndex;
            dataBindBonus(gridBonus.SortExp, e.NewPageIndex);
        }

        protected void gridBonus_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (gridBonus.SortExp == e.SortExpression)
            {
                gridBonus.ChangeSorting();
            }
            else
            {
                gridBonus.SortOrder = FashionZone.BL.Util.SortDirection.Ascending;
            }

            gridBonus.SortExp = e.SortExpression;
            dataBindBonus(e.SortExpression, gridBonus.CurrentPageIndex);
        }

        protected void dataBindBonus(string sortExp, int pageIndex)
        {
            try
            {
                int totalRecords = 0;
                gridBonus.PageSize = Utils.Configuration.PageSize;
                int id = 0;
                BONUS bonus = new BONUS() { CustomerID = this.CustomerID };
                List<BONUS> list = ApplicationContext.Current.Bonuses.Search(bonus, Utils.Configuration.PageSize, pageIndex, out totalRecords, sortExp, gridBonus.SortOrder);
                gridBonus.DataSource = list;
                gridBonus.CustomCustomVirtualItemCount = totalRecords;
                gridBonus.DataBind();
            }
            catch (Exception e)
            {
                writeError(e.Message);
            }
        }
    }
}