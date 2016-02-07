using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.BL.Util;
using FashionZone.DataLayer.Model;
using System.Web.Security;
using System.Globalization;


namespace FashionZone.FE
{
    public partial class register : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Response.Redirect("/home");
            }

            ((PublicMaster)Master).SetImgBackground("", "ContentIII");
            //txtBirthday.Attributes.Add("readonly", "readonly");

            //Check if is invited. Yes ? Set Cookie : nothing
            if (!IsPostBack
                && RouteData.Values["idcustinvited"] != null
                && !String.IsNullOrWhiteSpace(RouteData.Values["idcustinvited"].ToString()))
            {
                if (RouteData.Values["invid"] != null && !String.IsNullOrWhiteSpace(RouteData.Values["invid"].ToString()))
                {
                    string invitId = Encryption.Decrypt(RouteData.Values["invid"].ToString());
                    int idInv;
                    if (int.TryParse(invitId, out idInv))
                    {
                        var inv = ApplicationContext.Current.Invitations.GetById(idInv);
                        if (inv != null)
                            txtEmail.Text = inv.InvitedMail;
                    }
                    Response.Cookies["InvBy"]["InvId"] = invitId;
                }
                string invitByCustId = Encryption.Decrypt(RouteData.Values["idcustinvited"].ToString());
                Response.Cookies["InvBy"]["IdCust"] = invitByCustId;
                Response.Cookies["InvBy"].Expires = DateTime.Now.AddDays(1d);
            }

            // returning customer
            if (!IsPostBack && Request.Cookies["InvBy"] != null && Request.Cookies["InvBy"]["InvId"] != null)
            {
                string invitId = Request.Cookies["InvBy"]["InvId"].ToString();
                int idInv;
                if (int.TryParse(invitId, out idInv))
                {
                    var inv = ApplicationContext.Current.Invitations.GetById(idInv);
                    if (inv != null)
                        txtEmail.Text = inv.InvitedMail;
                }
            }
        }

        protected void lnkRegister_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                CUSTOMER customer = new CUSTOMER();
                if (String.IsNullOrWhiteSpace(txtEmail.Text) || !valEmail.IsValid)
                {
                    litError.Text = "Email " + Resources.Lang.NotValidLabel + ".";
                    return;
                }

                if (!chkGeneralTerms.Checked)
                {
                    litError.Text = Resources.Lang.PleaseAcceptLabel;
                    return;
                }
                int invCust = 0;
                //Check if have cookie. Yes ? Add id of customer whu invited : nothing
                if (Request.Cookies["InvBy"] != null)
                {
                    if (Request.Cookies["InvBy"]["IdCust"] != null)
                    {
                        if (int.TryParse(Request.Cookies["InvBy"]["IdCust"], out invCust))
                        {
                            customer.InvitedFrom = invCust;
                        }
                    }
                }

                customer.Email = txtEmail.Text;

                try
                {
                    if (ApplicationContext.Current.Customers.GetByEmail(txtEmail.Text) != null)
                    {
                        litError.Text = Resources.Lang.AlreadyRegisteredMailLabel;
                        return;
                    }

                    customer.Name = txtName.Text;
                    customer.Surname = txtSurname.Text;
                    DateTime date = new DateTime();
                    IFormatProvider formatProvider = new CultureInfo("it-IT");

                    if (!String.IsNullOrWhiteSpace(txtBirthday.Text))
                    {
                        DateTime.TryParse(txtBirthday.Text, formatProvider, DateTimeStyles.AdjustToUniversal, out date);

                    }
                    customer.BirthDate = date;
                    customer.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, Configuration.PasswordHashMethod).ToLower();
                    customer.Active = true;
                    customer.RegistrationDate = DateTime.Now;
                    customer.Newsletter = true;
                    customer.Gender = ddlGender.SelectedValue;
                    customer.Telephone = txtPhone.Text;
                    customer.Mobile = txtMobile.Text;

                    //If cookie exist ? delte && set InviteTabele a True : nothing
                    if (Request.Cookies["InvBy"] != null)
                    {
                        if (Request.Cookies["InvBy"]["InvId"] != null)
                        {
                            int idInv;
                            if (int.TryParse(Request.Cookies["InvBy"]["InvId"], out idInv))
                            {
                                var invitation = ApplicationContext.Current.Invitations.GetById(idInv);
                                if (invitation.InvitedMail == txtEmail.Text)
                                {
                                    invitation.Registered = true;
                                    invitation.RegistrationDate = DateTime.Now;
                                    //ApplicationContext.Current.Invitations.Update(invitation);
                                    //No need to do an update (i.e. attach and save context), object already attached
                                }
                                // TODO Check logic
                                // case when invitation id is specified, but user is registering another email
                                else if (invCust != 0)
                                {
                                    INVITATION invt = new INVITATION() { CustomerID = invCust, InvitedMail = txtEmail.Text, Registered = true, RegistrationDate = DateTime.Now, IP = Request.UserHostAddress };
                                    ApplicationContext.Current.Invitations.Insert(invt);
                                }
                            }
                            else
                            {
                                //case when invitation id is not specified, user may be referred in another way
                                INVITATION invt = new INVITATION() { CustomerID = invCust, InvitedMail = txtEmail.Text, Registered = true, RegistrationDate = DateTime.Now, IP = Request.UserHostAddress };
                                ApplicationContext.Current.Invitations.Insert(invt);
                            }
                        }
                        HttpCookie myCookie = new HttpCookie("InvBy");
                        myCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(myCookie);
                    }

                    ApplicationContext.Current.Customers.Insert(customer);

                    CurrentCustomer = new SessionCustomer(customer);

                    Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                    FormsAuthentication.SetAuthCookie(customer.Email, false);

                    Response.Redirect("/home");

                }
                catch (System.Threading.ThreadAbortException ex)
                {
                }
                catch (Exception ex)
                {
                    //TODO log ex
                    Log(ex, ex.Message, ex.StackTrace, "Register");
                    litError.Text = Resources.Lang.ErrorVerifiedLabel;
                }
            }
        }
    }
}