using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using FashionZone.DataLayer.Model;
using System.Web.Security;
using FashionZone.BL;
using System.IO;
using System.Configuration;
using System.Text;
using Facebook;

namespace FashionZone.FE.Utils
{
    public class BasePage : Page
    {
        private const string CULTURE = "sq-AL";
        private static readonly string _logFile = Path.Combine(HttpContext.Current.Server.MapPath("/") + "/Log/");

        void Page_Init(object sender, EventArgs e)
        {
            //Stopping the one-click attack
            // force the session to exist, so the session id doesn't change with every request
            if (Session.IsNewSession)
            {
                Session["ForceSession"] = DateTime.Now;
            }
            // 'sign' the viewstate with the current session
            ViewStateUserKey = Session.SessionID;


            if (Page.EnableViewState)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["__VIEWSTATE"]))
                {
                    throw new Exception("Viewstate on query string detected!");
                }
            }

            if (String.IsNullOrEmpty(Request.Form["__VIEWSTATE"]) && Page.IsPostBack)
            {
                throw new Exception("Viewstate did not exist on the form!");
            }

            // session terminated, redirect to login
            try
            {
                if (CurrentCustomer == null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        FormsAuthentication.SignOut();
                        Response.Redirect(Request.RawUrl);

                        if (CartSession != null && CartSession.Count > 0)
                        {
                            ApplicationContext.Current.Carts.DeleteShoppingCart(CartSession.First().Id);
                        }

                        CartSession = null;
                        Session.Abandon();
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "BasePage.Pre_Init");
                FormsAuthentication.SignOut();
                Response.Redirect(Request.RawUrl);

                Session.Abandon();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshCartNumber();
            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "BasePage.Page_Load");
                FormsAuthentication.SignOut();
                Response.Redirect(Request.RawUrl);

                Session.Abandon();
            }
        }

        public void RefreshCartNumber()
        {
            SiteMaster master = this.Master as SiteMaster;
            master.CartNumber.Text = TotalCartItems.ToString();
        }

        public void RefreshCart()
        {
            string cartID = String.Empty;
            if (CartSession != null && CartSession.Count > 0)
            {
                cartID = CartSession.First().Id;
            }
            List<SHOPPING_CART> carts = ApplicationContext.Current.Carts.GetShoppingCartItems(cartID);

            decimal total = 0;
            int i = 0;
            CartSession = new List<SessionCart>();
            if (carts != null)
            {
                foreach (SHOPPING_CART cart in carts)
                {
                    total += cart.Amount.Value;
                    i += cart.Quantity;
                    SessionCart sC = new SessionCart(cart);
                    CartSession.Add(sC);
                }
            }
            TotalAmount = total;
        }

        public int? CartExpirationMinutes()
        {
            if (CartSession != null && CartSession.Count > 0)
            {
                List<SessionCart> cSession = CartSession.OrderByDescending(c => c.DateAdded).ToList();
                SessionCart sessionCart = cSession.First();
                TimeSpan ts = DateTime.Now - sessionCart.DateAdded;
                return FashionZone.BL.Configuration.CartExpirationValue - (int)ts.TotalMinutes;
            }
            else
            {
                return null;
            }
        }


        protected override void InitializeCulture()
        {
            if (Session["Culture"] != null)
            {
                // Retrieve culture information from session.
                string culture = Convert.ToString(Session["Culture"]);

                Culture = string.IsNullOrEmpty(culture) ? CULTURE : culture;

                // Set culture to current thread.
                //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }

            base.InitializeCulture();
        }

        public SessionCustomer CurrentCustomer
        {
            get
            {
                if (Session["CurrentCustomer"] != null)
                {
                    return Session["CurrentCustomer"] as SessionCustomer;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                Session["CurrentCustomer"] = value;
            }
        }

        public List<SessionCart> CartSession
        {
            get
            {
                if (Session["CartSession"] != null)
                {
                    return Session["CartSession"] as List<SessionCart>;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                Session["CartSession"] = value;
            }
        }

        public decimal? TotalAmount
        {
            get
            {
                if (Session["TotalAmount"] != null)
                {
                    return (decimal)Session["TotalAmount"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                Session["TotalAmount"] = value;
            }
        }

        public int? TotalCartItems
        {
            get
            {
                int i = 0;
                if (CartSession != null)
                {
                    foreach (SessionCart cart in CartSession)
                    {
                        i += cart.Quantity;
                    }
                    return i;
                }
                else
                    return 0;
            }
        }

        internal static void Log(Exception ex, String Message, String Trace, String Where)
        {
            using (StreamWriter w = File.AppendText(_logFile + DateTime.Today.ToString("dd-MM-yyyy") + ".log"))
            {
                w.WriteLine(DateTime.Now + " - " + Where);
                w.WriteLine(Message);
                w.WriteLine(Trace);
                if (ex != null && ex.InnerException != null)
                {
                    w.WriteLine("INNER - " + ex.InnerException.Message);
                }
                w.Flush();
                // Close the writer and underlying file.
                w.Close();
            }
        }

        internal static void sendOrderMailToCustomer(CUSTOMER customer, List<ORDER_DETAIL> details, string subject)
        {
            try
            {
                BL.Util.Mailer.SendCustomerOrder(customer, details, subject);
            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "Checkout - SendmailAdmins");
            }
        }

        internal static void sendOrderMailToAdmins(List<ORDER_DETAIL> carts, string paymentType, string comments, decimal TotalOrder, string FullName)
        {
            try
            {
                string sub = "New order in FZone";
                StringBuilder body = new StringBuilder();
                body.Append(string.Format("<b>Automatic mail from FZone</b><br /> A new order has been created from customer {0}.",
                    FullName));
                body.Append("<br />");
                body.Append("Products ordered: ");
                foreach (ORDER_DETAIL cart in carts)
                {
                    body.Append("<br />");
                    body.Append(cart.ProductNameWithSize + " - quantity: " + cart.Quantity + " - price: " + cart.UnitPrice + " - amount: " + cart.Amount);
                }
                body.Append("<br />");
                body.Append("<b>Total amount: " + TotalOrder + "</b>");
                body.Append("<br />");
                body.Append("Payment type : " + paymentType);
                body.Append("<br />");
                body.Append("Comments : " + comments);

                int totalRecs;
                List<USER> users = ApplicationContext.Current.Users.Search(new USER() { Enabled = true, RoleID = 1 }, 10, 0, out totalRecs, "", BL.Util.SortDirection.Ascending);
                foreach (USER user in users)
                {
                    BL.Util.Mailer.SendMailGeneric(user.Email, sub, body.ToString());
                }
            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "Checkout - SendmailAdmins");
            }
        }

        internal static void sendMailToAdmins(String Subject, String Message)
        {
            try
            {
                string sub = Subject;
                int totalRecs;
                List<USER> users = ApplicationContext.Current.Users.Search(new USER() { Enabled = true, RoleID = 1 }, 10, 0, out totalRecs, "", BL.Util.SortDirection.Ascending);
                foreach (USER user in users)
                {
                    BL.Util.Mailer.SendMailGeneric(user.Email, sub, Message);
                }
            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "Checkout - SendmailAdmins");
            }
        }

        protected bool facebookAuthenticate()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(Request["accessToken"]))
                {
                    FacebookClient client = new FacebookClient(Request["accessToken"]);
                    dynamic person = client.Get("me");

                    // data returned from facebook
                    if (person != null)
                    {
                        CUSTOMER customer = new CUSTOMER(person);
                        CUSTOMER fbCustomer = ApplicationContext.Current.Customers.GetByFBId(person.id);
                        // user not found in db with this fb id
                        if (fbCustomer == null)
                        {
                            CUSTOMER myCustomer = ApplicationContext.Current.Customers.GetByEmail(customer.Email);
                            // user not present with this fb id nor with this email
                            if (myCustomer == null)
                            {
                                customer.RegistrationDate = DateTime.Today;
                                ApplicationContext.Current.Customers.Insert(customer);
                            }
                            else
                            {
                                myCustomer.Email = customer.Email;
                                myCustomer.Name = customer.Name;
                                myCustomer.Surname = customer.Surname;
                                myCustomer.Gender = customer.Gender;
                                myCustomer.FBId = customer.FBId;
                                myCustomer.BirthDate = customer.BirthDate;
                                ApplicationContext.Current.Customers.Update(myCustomer, false);
                                customer.ID = myCustomer.ID;
                            }
                        }
                        else
                        {
                            // user already saved as fb user, updating email if necessary
                            if (fbCustomer.Email != customer.Email)
                            {
                                fbCustomer.Email = customer.Email;
                                ApplicationContext.Current.Customers.Update(fbCustomer, false);
                            }
                            customer.ID = fbCustomer.ID;
                        }

                        CurrentCustomer = new SessionCustomer(customer);
                        Session["accessToken"] = Request["accessToken"];
                        Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                        FormsAuthentication.SetAuthCookie(customer.Email, false);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "BasePage - FacebookAuth");
                return false;
            }
        }
    }
}