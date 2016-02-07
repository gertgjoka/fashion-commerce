using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using System.Drawing;
using FashionZone.BL;
using Configuration = FashionZone.Admin.Utils.Configuration;

namespace FashionZone.Admin.Controls
{
    public partial class CustomerAddress : System.Web.UI.UserControl
    {
        public event EventHandler AddressDelete;
        public event EventHandler AddressAsyncRemove;
        public event EventHandler Saving;

        protected string UniqueValidationGroup { get { return ClientID + "AddrValidationGroup"; } }

        public int CustomerID
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
            get { 
                bool disable;
                if (ViewState["DisableOriginal"] != null && Boolean.TryParse(ViewState["DisableOriginal"].ToString(), out disable))
                {
                    return disable;
                }
                else
                {
                    return false;
                }
            }
            set { ViewState["DisableOriginal"] = value; }
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
            valPostal.ValidationGroup = UniqueValidationGroup;
            validName.ValidationGroup = UniqueValidationGroup;
            validLoc.ValidationGroup = UniqueValidationGroup;
            validTel.ValidationGroup = UniqueValidationGroup;
            lnkSave.ValidationGroup = UniqueValidationGroup;
        }

        //void Page_PreRender(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        ReloadControl(true);
        //    }

        //    if (!DisableOriginal)
        //    {
        //        if (AddrID == "0")
        //            changeSelectedPanel(1);
        //    }
        //}

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
                AddressAsyncRemove(sender, e);
            }

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {            
            if (CustomerID != 0)
            {
                Save(false);
            }
            else
            {
                writeError("Customer must be saved before you can proceed.");
            }
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

            if (CustomerID != 0)
            {
                addr.CustomerID = CustomerID;
            }

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
                    if (!fromPage)
                    {
                        lblErrors.Visible = true;
                        lblErrors.ForeColor = Color.Green;
                        lblErrors.Text = "Address " + operation + " correctly.";
                    }
                    else
                    {
                        lblErrors.Visible = false;
                    }

                    if (!InOrder)
                    {
                        lnkDelAddr.Visible = true;
                    }
                    if (Saving != null)
                    {
                        Saving(null, null);
                    }
                    ChangeSelectedPanel(0);
                }
                catch (Exception ex)
                {
                    writeError(ex.Message);
                }
            }
            else
            {
                Page.Validate(UniqueValidationGroup);
                writeError("Address could not be saved because some required fields are missing. ");
            }
        }

        public bool IsValid()
        {
            if (ddlCities.SelectedValue != "-1" && ddlAddrType.SelectedValue != "-1" && !String.IsNullOrWhiteSpace(txtAddress.Text)
                && !String.IsNullOrWhiteSpace(txtLocation.Text) && !String.IsNullOrWhiteSpace(txtName.Text) && !String.IsNullOrWhiteSpace(txtPostal.Text)
                && !String.IsNullOrWhiteSpace(txtTel.Text))
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

            ddlCities.SelectedValue = "-1";
            ddlAddrType.SelectedValue = "-1";
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
                litTel.Text = " Tel: " + addr.Telephone + "<br />";
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
                D_CITY emptyCity = new D_CITY() { ID = -1, Name = "" };
                cities.Insert(0, emptyCity);
                ddlCities.DataSource = cities;
                ddlCities.DataValueField = "ID";
                ddlCities.DataTextField = "Name";
                if (selectedCity.HasValue)
                {
                    ddlCities.SelectedValue = selectedCity.Value.ToString();
                }
                ddlCities.DataBind();

                List<D_ADDRESS_TYPE> types = ApplicationContext.Current.Customers.GetAddressTypeList();
                D_ADDRESS_TYPE emptyType = new D_ADDRESS_TYPE() { ID = -1, Name = "" };
                types.Insert(0, emptyType);
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
            lblErrors.Text = "Error occurred: " + errorMessage;
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
                writeError(ex.Message);
            }
        }
    }
}