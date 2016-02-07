using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using System.Drawing;

namespace FashionZone.Admin.Secure.Product
{
    public partial class BrandNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Error += new EventHandler(Page_Error);

            string idBrand = Request["ID"];
            int id;
            //bool result = Int32.TryParse(idBrand, out id);

            if (!IsPostBack)
            {
                BRAND br = null;
                if (!String.IsNullOrWhiteSpace(idBrand) && Int32.TryParse(idBrand, out id) && id != 0)
                {
                    br = getBrand(id);
                    ID.Value = id.ToString();
                }
            }
        }

        private BRAND getBrand(int Id)
        {
            BRAND brand = null;
            brand = ApplicationContext.Current.Brands.GetById(Id);
            if (brand != null)
            {
                txtName.Text = brand.Name;
                txtShowName.Text = brand.ShowName;
                txt_web.Text = brand.Website;
                txt_tel.Text = brand.Telephone;
                txtEmail.Text = brand.Email;
                txtDesc.Text = brand.Description;
                txt_contact.Text = brand.Contact;
                chkShop.Checked = brand.Shop.HasValue ? brand.Shop.Value : false;
                txtAddress.Text = brand.Address;
            }
            return brand;
        }

        void Page_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                //Save Brand
                BRAND br = new BRAND();
                br.Name = txtName.Text;
                br.ShowName = txtShowName.Text;
                br.Website = txt_web.Text;
                br.Telephone = txt_tel.Text;
                br.Email = txtEmail.Text;
                br.Description = txtDesc.Text;
                br.Contact = txt_contact.Text;
                br.Shop = chkShop.Checked;
                br.Address = txtAddress.Text;

                int id = 0;
                lblErrors.Visible = true;
                lblErrors.ForeColor = Color.Green;
                string operation = String.Empty;

                try
                {
                    if (!String.IsNullOrWhiteSpace(ID.Value) && Int32.TryParse(ID.Value, out id) && id != 0)
                    {
                        br.ID = id;
                        ApplicationContext.Current.Brands.Update(br);
                        operation = "updated";
                    }
                    else
                    {
                        ApplicationContext.Current.Brands.Insert(br);
                        operation = "inserted";
                        ID.Value = br.ID.ToString();
                    }
                    lblErrors.Text = "Brand " + operation + " correctly.";
                }
                catch (Exception ex)
                {
                    // TODO log error
                    lblErrors.ForeColor = Color.Red;
                    lblErrors.Text = "Error occurred: " + ex.Message;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetFields();
        }

        private void resetFields()
        {
            txtName.Text = "";
            txtShowName.Text = "";
            txtDesc.Text = "";
            txt_contact.Text = "";
            txt_tel.Text = "";
            txt_web.Text = "";
            txtEmail.Text = "";
        }
    }
}