using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using FashionZone.FE.Utils;

namespace FashionZone.FE.CustomControl
{
    public partial class CustomerAddress : System.Web.UI.UserControl
    {
        public event EventHandler AddressDelete;
        public event EventHandler AddressAsyncRemove;
        public event EventHandler Saving;

        protected string UniqueValidationGroup { get { return ClientID + "AddrValidationGroup"; } }

        public SessionCustomer CurrentCustomer
        {
            get
            {
                if (Page is Secure.cart.checkout)
                    return ((Secure.cart.checkout)Page).CurrentCustomer;
                else
                    return ((Secure.Personal.personalInfo)Page).CurrentCustomer;
            }
            set
            {
                if (Page is Secure.cart.checkout)
                    ((Secure.cart.checkout)Page).CurrentCustomer = value;
                else
                    ((Secure.Personal.personalInfo)Page).CurrentCustomer = value;
            }
        }

        public int AddrID
        {
            get
            {
                int id;
                if (ViewState["AddrID"] != null && Int32.TryParse(ViewState["AddrID"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["AddrID"] = value;
            }
        }

        public bool InOrder
        {
            get;
            set;
        }

        public bool DisableOriginal
        {
            get
            {
                bool disable;
                if (Boolean.TryParse(disableOriginal.Value, out disable))
                {
                    return disable;
                }
                else
                {
                    return false;
                }
            }
            set { disableOriginal.Value = value.ToString(); }
        }

        public void ResetID()
        {
            AddrID = 0;
        }

        public bool IsSaved()
        {
            return AddrID != 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReloadControl(true);
            }

            if (!DisableOriginal)
            {
                if (AddrID == 0)
                    ChangeSelectedPanel(1);
            }

            //Page.PreRender += new EventHandler(Page_PreRender);
            validAddress.ValidationGroup = UniqueValidationGroup;
            validName.ValidationGroup = UniqueValidationGroup;
            validTel.ValidationGroup = UniqueValidationGroup;
            lnkSave.ValidationGroup = UniqueValidationGroup;
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            if (IsSaved())
            {
                ChangeSelectedPanel(0);
            }
            else
            {
                ResetControl(true);
                // to parent page
                Visible = false;
                if (AddressAsyncRemove != null)
                {
                    AddressAsyncRemove(sender, e);
                }
            }

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        public ADDRESS GetAddress()
        {
            ADDRESS addr = new ADDRESS();
            addr.Address1 = txtAddress.Text;
            addr.Name = txtName.Text;
            addr.Location = txtLocation.Text;
            addr.PostCode = txtPostal.Text;
            addr.Telephone = txtTel.Text;

            if (ddlAddrType.SelectedValue != "-1")
            {
                addr.TypeID = Int32.Parse(ddlAddrType.SelectedValue);
            }

            if (ddlCities.SelectedValue != "-1")
            {
                addr.City = Int32.Parse(ddlCities.SelectedValue);
                addr.CityName = ddlCities.SelectedItem.Text;
            }

            addr.CustomerID = CurrentCustomer.Id;

            return addr;
        }

        public void Save(bool fromPage)
        {
            if (IsValid())
            {
                ADDRESS addr = GetAddress();

                string operation = String.Empty;
                try
                {
                    // not a new address
                    if (AddrID != 0)
                    {
                        addr.ID = AddrID;
                        if (!DisableOriginal)
                        {
                            ApplicationContext.Current.Customers.UpdateAddress(addr);
                        }
                        else
                        {
                            ADDRESSINFO addrInfo = new ADDRESSINFO(addr);
                            ApplicationContext.Current.Orders.UpdateAddress(addrInfo);
                        }

                        operation = "updated";
                    }
                    else
                    {
                        ApplicationContext.Current.Customers.InsertAddress(addr);
                        operation = "inserted";
                        AddrID = addr.ID;
                    }
                    loadAddress(addr);

                    if (!InOrder)
                    {
                        lnkDelAddr.Visible = true;
                        imgDelete.Visible = true;
                        imgSeparator.Visible = true;
                    }
                    if (Saving != null)
                    {
                        Saving(null, null);
                    }
                    ChangeSelectedPanel(0);
                }
                catch (Exception ex)
                {
                    BasePage.Log(ex, ex.Message, ex.StackTrace, "CustomerAddressControl.Save");
                    writeError(ex.Message);
                }
            }
            else
            {
                Page.Validate(UniqueValidationGroup);
            }
        }

        public bool IsValid()
        {
            if (ddlCities.SelectedValue != "-1" && ddlAddrType.SelectedValue != "-1" && !String.IsNullOrWhiteSpace(txtAddress.Text)
                && !String.IsNullOrWhiteSpace(txtName.Text) && !String.IsNullOrWhiteSpace(txtTel.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ResetControl(bool ResetId)
        {
            if (ResetId)
            {
                ResetID();
            }

            txtAddress.Text = "";
            txtLocation.Text = "";
            txtLocation.Text = "";
            txtName.Text = "";
            txtPostal.Text = "";
            txtTel.Text = "";

            litAddress.Text = "";
            litName.Text = "";
            litOther.Text = "";
            litTel.Text = "";

            ddlCities.SelectedValue = "1";
            ddlAddrType.SelectedValue = "1";
            lblErrors.Text = "";
            ResetErrors();
        }

        public void ResetErrors()
        {
            lblErrors.Text = "";
            lblErrors.Visible = false;
        }

        public void ReloadControl(bool PageLoad)
        {
            ADDRESS addr = null;
            if (AddrID != 0)
            {
                if (!DisableOriginal)
                {
                    addr = ApplicationContext.Current.Customers.GetAddressById(AddrID);
                }
                else
                {
                    ADDRESSINFO a = ApplicationContext.Current.Orders.GetAddress(AddrID);
                    addr = new ADDRESS(a);
                }

                loadAddress(addr);
                if (!InOrder)
                {
                    lnkDelAddr.Visible = true;
                    imgDelete.Visible = true;
                    imgSeparator.Visible = true;
                }
            }

            if (PageLoad)
            {
                // viewstate of dropdown is enabled, so there is no need to reload it on every postback
                LoadDDLs(addr == null ? null : addr.City, addr == null ? null : addr.TypeID);
            }
            else
            {
                // ddl already loaded, the values must be re-set
                if (addr != null && addr.City.HasValue)
                {
                    ddlCities.SelectedValue = addr.City.Value.ToString();
                }

                if (addr != null && addr.TypeID.HasValue)
                {
                    ddlAddrType.SelectedValue = addr.TypeID.Value.ToString();
                }
            }
        }
        private void loadAddress(ADDRESS addr)
        {
            txtAddress.Text = addr.Address1;
            txtLocation.Text = addr.Location;
            txtName.Text = addr.Name;
            txtPostal.Text = addr.PostCode;
            txtTel.Text = addr.Telephone;

            litName.Text = addr.Name;
            if (!String.IsNullOrWhiteSpace(addr.Telephone))
            {
                litTel.Text = "<tr><td>Tel: " + addr.Telephone + "</td></tr>";
            }
            litAddress.Text = addr.Address1;
            litOther.Text = addr.Location + ", " + (addr.CityName == null ? ddlCities.SelectedItem.Text : addr.CityName) + ", " + addr.PostCode;
        }

        private void LoadDDLs(int? selectedCity, int? selectedType)
        {
            try
            {
                D_STATE state = new D_STATE() { ID = Configuration.State };
                List<D_CITY> cities = ApplicationContext.Current.Customers.GetCities(state);
                //D_CITY emptyCity = new D_CITY() { ID = -1, Name = "" };
                //cities.Insert(0, emptyCity);
                ddlCities.DataSource = cities;
                ddlCities.DataValueField = "ID";
                ddlCities.DataTextField = "Name";
                if (selectedCity.HasValue)
                {
                    ddlCities.SelectedValue = selectedCity.Value.ToString();
                }
                ddlCities.DataBind();

                List<D_ADDRESS_TYPE> types = ApplicationContext.Current.Customers.GetAddressTypeList();
                //D_ADDRESS_TYPE emptyType = new D_ADDRESS_TYPE() { ID = -1, Name = "" };
                //types.Insert(0, emptyType);
                ddlAddrType.DataSource = types;
                ddlAddrType.DataValueField = "ID";
                ddlAddrType.DataTextField = "Name";
                if (selectedType.HasValue)
                {
                    ddlAddrType.SelectedValue = selectedType.Value.ToString();
                }
                ddlAddrType.DataBind();
            }
            catch (Exception ex)
            {
                BasePage.Log(ex, ex.Message, ex.StackTrace, "CustomerAddress.LoadDDL");
                writeError(ex.Message);
            }
        }

        public void ChangeSelectedPanel(int index)
        {
            accAddresses.FadeTransitions = true;
            accAddresses.SelectedIndex = index;
        }

        private void writeError(string errorMessage)
        {
            lblErrors.Visible = true;
            lblErrors.ForeColor = Color.Red;
            //TODO log error
            lblErrors.Text = Resources.Lang.ErrorVerifiedLabel;
        }

        protected void lnkDelAddr_Click(object sender, EventArgs e)
        {
            try
            {
                var ctx = ApplicationContext.Current.Customers;
                ctx.DeleteAddressById(AddrID);

                // Event bubbling to parent page
                AddressDelete(this, e);
            }
            catch (Exception ex)
            {
                BasePage.Log(ex, ex.Message, ex.StackTrace, "CustomerAddress.DelAddr");
                writeError(ex.Message);
            }
        }
    }
}