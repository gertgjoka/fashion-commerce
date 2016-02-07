using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using Configuration = FashionZone.Admin.Utils.Configuration;
using FashionZone.DataLayer.Model;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Drawing;

namespace FashionZone.Admin.Secure.Admin
{
    public partial class UserNew : System.Web.UI.Page
    {
        private const string fakePassChars = "11111";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Error += new EventHandler(Page_Error);

            string idUser = Request["ID"];
            int id;
            if (!IsPostBack)
            {
                USER user = null;
                if (!String.IsNullOrWhiteSpace(idUser) && Int32.TryParse(idUser, out id) && id != 0)
                {
                    user = GetUser(id);
                    ID.Value = id.ToString();
                }
                // viewstate of dropdown is enabled, so there is no need to reload it on every postback
                LoadRoles(user == null ? null : user.RoleID);
            }
        }

        private void Save()
        {
            USER user = new USER();
            user.Login = txtLogin.Text;
            user.Name = txtName.Text;
            user.Email = txtEmail.Text;
            user.Enabled = chkEnabled.Checked;
            //user.Enabled = chkEnabled.Enabled;
            if (ddlRoles.SelectedValue != "-1")
            {
                user.RoleID = Int32.Parse(ddlRoles.SelectedValue);
            }
            bool ignorePass = false;
            if (txtPass.Text != fakePassChars)
            {
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Text, Configuration.PasswordHashMethod).ToLower();
            }
            else
            {
                ignorePass = true;
            }
            // not a new user
            int id = 0;
            lblErrors.Visible = true;
            lblErrors.ForeColor = Color.Green;
            string operation = String.Empty;
            try
            {
                if (!String.IsNullOrWhiteSpace(ID.Value) && Int32.TryParse(ID.Value, out id) && id != 0)
                {
                    user.ID = id;
                    ApplicationContext.Current.Users.Update(user, ignorePass);
                    operation = "updated";
                }
                else
                {
                    ApplicationContext.Current.Users.Insert(user);
                    operation = "inserted";
                    ID.Value = user.ID.ToString();
                }
                lblErrors.Text = "User " + operation + " correctly.";

                //Reset password to fake, otherwise the textbox will be empty
                txtPass.Attributes.Add("value", fakePassChars);
                txtPass2.Attributes.Add("value", fakePassChars);
            }
            catch (Exception ex)
            {
                // TODO log error
                lblErrors.ForeColor = Color.Red;
                lblErrors.Text = "Error occurred: " + ex.Message;
            }
        }


        private USER GetUser(int id)
        {
            USER user = null;
            try
            {
                user = ApplicationContext.Current.Users.GetById(id);
                if (user != null)
                {
                    txtName.Text = user.Name;
                    txtLogin.Text = user.Login;
                    txtEmail.Text = user.Email;
                    if (user.Enabled)
                    {
                        chkEnabled.Checked = user.Enabled;
                    }
                    // fake password because hash can't be reversed
                    txtPass.Attributes.Add("value", fakePassChars);
                    txtPass2.Attributes.Add("value", fakePassChars);
                }

            }
            catch (Exception ex)
            {
                // TODO log error
                lblErrors.Visible = true;
                lblErrors.ForeColor = Color.Red;
                lblErrors.Text = "Error occurred: " + ex.Message;
            }
            return user;
        }

        void Page_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
        }

        private void LoadRoles(int? selectedRole)
        {
            try
            {
                List<ROLE> roles = ApplicationContext.Current.Users.GetAllRoles();
                ROLE emptyRole = new ROLE() { ID = -1, Name = "" };
                roles.Insert(0, emptyRole);
                ddlRoles.DataSource = roles;
                ddlRoles.DataValueField = "ID";
                ddlRoles.DataTextField = "Name";
                if (selectedRole.HasValue)
                {
                    ddlRoles.SelectedValue = selectedRole.Value.ToString();
                }
                ddlRoles.DataBind();
            }
            catch (Exception ex)
            {
                // TODO log error
                lblErrors.Visible = true;
                lblErrors.ForeColor = Color.Red;
                lblErrors.Text = "Error occurred: " + ex.Message;
            }
        }

        protected void button1_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (ddlRoles.SelectedValue == "-1")
                {
                    lblErrors.Visible = true;
                    lblPassStrength.Visible = false;
                    lblErrors.Text = "You  must select a role for this user!";
                    txtPass.Attributes.Add("value", fakePassChars);
                    txtPass2.Attributes.Add("value", fakePassChars);
                    return;
                }

                Regex reg = new Regex(Configuration.PasswordStrengthRegularExpression);
                if (!txtPass.Text.Equals(fakePassChars) && !reg.IsMatch(txtPass.Text))
                {
                    lblPassStrength.Visible = true;
                    lblErrors.Visible = false;
                    txtPass.Attributes.Add("value", String.Empty);
                    txtPass2.Attributes.Add("value", String.Empty);
                    return;
                }

                lblPassStrength.Visible = false;
                lblErrors.Visible = false;

                //save changes
                Save();
            }
        }

        protected void button2_Click(object sender, EventArgs e)
        {
            resetFields();
        }

        private void resetFields()
        {
            txtEmail.Text = "";
            txtName.Text = "";
            txtLogin.Text = "";
            txtPass.Attributes.Add("value", String.Empty);
            txtPass2.Attributes.Add("value", String.Empty);
            //ID.Value = String.Empty;
            ddlRoles.SelectedValue = "-1";
            chkEnabled.Checked = false;
        }
    }
}