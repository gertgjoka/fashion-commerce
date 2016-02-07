using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.FE.CustomControl;
using FashionZone.DataLayer.Model;
using System.Text;
using System.Threading;
using FashionZone.FE.Utils;
using System.Collections.Specialized;
using System.Globalization;
using FashionZone.BL.Util;

namespace FashionZone.FE.Secure.cart
{
    public partial class checkout : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((SiteMaster)Master).SetImgBackground("", "ContentIII");
            ScriptManager sc = this.Master.FindControl("ScriptManager1") as ScriptManager;
            sc.RegisterPostBackControl(accordionPayment.FindControl("btnPaypal"));
            sc.RegisterPostBackControl(accordionPayment.FindControl("btnEasyPay"));

            if (!IsPostBack)
            {
                try
                {
                    Version = new SortedList<int, byte[]>();
                    Session["AddrForEasyPay"] = null;
                    Session["BonForEasyPay"] = null;
                    Session["BonIdForEasyPay"] = null;
                    RefreshCart();
                    if (CartSession != null && CartSession.Count > 0)
                    {
                        loadCustomerAddresses(null, null);
                        DataBind();
                    }
                    else
                    {
                        Response.Redirect("/home");
                    }
                }
                catch (System.Threading.ThreadAbortException ex)
                {
                }
                catch (Exception ex)
                {
                    Log(ex, ex.Message, ex.StackTrace, "Checkout.Page_load");
                }
            }
            base.Page_Load(sender, e);
        }

        public SortedList<int, byte[]> Version
        {
            get
            {
                if (Session["BonusV"] != null)
                {
                    return (SortedList<int, byte[]>)Session["BonusV"];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                Session["BonusV"] = value;
            }
        }

        protected Decimal BonusUsed
        {
            get
            {
                decimal tot;
                if (ViewState["BUsed"] != null && Decimal.TryParse(ViewState["BUsed"].ToString(), out tot))
                    return tot;
                else
                    return 0;
            }
            set
            {
                ViewState["BUsed"] = value;
            }
        }

        protected Decimal ShipCost
        {
            get
            {
                decimal tot;
                if (ViewState["ShipCost"] != null && Decimal.TryParse(ViewState["ShipCost"].ToString(), out tot))
                    return tot;
                else
                    return 0;
            }
            set
            {
                ViewState["ShipCost"] = value;
            }
        }

        protected Decimal TotalOrder
        {
            get
            {
                return TotalAmount.Value + ShipCost - BonusUsed;
            }
        }

        private void DataBind()
        {
            rptDetails.DataSource = ApplicationContext.Current.Carts.GetShoppingCartItems(CartSession.First().Id);
            rptDetails.DataBind();


            loadCustomerBonus();

            //List<SHIPPING> shippings = ApplicationContext.Current.Orders.GetShippings();
            //ddlShipping.DataSource = shippings;
            //if (shippings != null && shippings.Count > 0)
            //{
            //    ShipCost = shippings.ElementAt(0).ShippingCost;
            //}

            //ddlShipping.DataBind();
        }

        #region Address handling

        //protected void ddlShipping_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlShipping.SelectedValue != String.Empty)
        //    {
        //        int shippingType;
        //        if (Int32.TryParse(ddlShipping.SelectedValue, out shippingType))
        //        {
        //            SHIPPING ship = ApplicationContext.Current.Orders.GetShipping(shippingType);
        //            ShipCost = ship.ShippingCost;
        //            updShipCost.Update();
        //        }
        //    }
        //}

        protected void ddlShippingAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlShippingAddress.SelectedValue != "0")
            {
                loadShippingAddress(ddlShippingAddress.SelectedValue, true);
                bAddress.Visible = true;
                address1.ResetErrors();
            }
            else
            {
                address1.ResetControl(true);
                address1.Visible = false;
            }
        }

        protected void loadShippingAddress(string AddressID, bool reload)
        {
            address1.AddrID = Int32.Parse(AddressID);
            address1.ChangeSelectedPanel(0);

            if (reload)
            {
                address1.ReloadControl(false);
            }
            address1.Visible = true;
        }

        protected void loadCustomerBonus()
        {
            List<BONUS> bonuses = ApplicationContext.Current.Bonuses.GetAvailableBonusForCustomer(CurrentCustomer.Id);

            if (bonuses.Count > 0)
            {
                foreach (BONUS bon in bonuses)
                {
                    Version.Add(bon.ID, bon.Version);
                }

                var query = from b in bonuses
                            select new { b.ID, b.BonusString };
                var v = new { ID = -1, BonusString = Resources.Lang.ChooseLabel };

                var a = query.ToList();

                a.Insert(0, v);
                ddlBonus.DataSource = a;
                ddlBonus.DataBind();
            }
            else
            {
                ddlBonus.Visible = false;
            }

        }

        protected void loadCustomerAddresses(int? selectedShipping, int? selectedBilling)
        {
            if (CurrentCustomer != null)
            {
                var customer = ApplicationContext.Current.Customers.GetById(CurrentCustomer.Id);
                if (customer.ADDRESS != null && customer.ADDRESS.Count > 0)
                {
                    List<ADDRESS> addresses = customer.ADDRESS.ToList();
                    addresses.Insert(0, new ADDRESS() { ID = 0 });

                    ddlShippingAddress.DataSource = addresses;
                    ddlShippingAddress.DataBind();
                    ddlShippingAddress.Enabled = true;
                    if (selectedShipping.HasValue && selectedShipping.Value != 0)
                    {
                        ddlShippingAddress.SelectedValue = selectedShipping.Value.ToString();
                    }
                    else if (address1.AddrID != 0)
                    {
                        ddlShippingAddress.SelectedValue = address1.AddrID.ToString();
                    }

                    if (customer.ADDRESS.Count < 5)
                    {
                        lnkAddAddress.Visible = true;
                    }
                }
                else
                {
                    lnkAddAddress.Visible = true;
                }
            }
        }

        protected void lnkAddAddress_Click(object sender, EventArgs e)
        {
            address1.ResetControl(true);
            ddlShippingAddress.Enabled = false;
            address1.Visible = true;
            bAddress.Visible = true;
            address1.ChangeSelectedPanel(1);
        }

        protected void address_OnAddressAsyncRemove(object sender, EventArgs e)
        {
            ddlShippingAddress.Enabled = true;
        }

        protected void address1_Saving(object sender, EventArgs e)
        {
            if (!bAddress.Visible)
            {
                bAddress.Visible = true;
            }
            loadCustomerAddresses(address1.AddrID, null);
        }

        #endregion

        protected void lnkConfirm_Click(object sender, EventArgs e)
        {
            // refreshing cart before continuing with order insertion, cart may have expired in the meanwhile
            // in case of expiration a popup with a message will be shown
            RefreshCart();
            if (CartSession != null && CartSession.Count > 0)
            {
                saveOrder();
            }
            else
            {
                //popup with message and redirect to home page.
                modalPopup.Show();
            }
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CartSession != null && CartSession.Count > 0)
                {
                    ApplicationContext.Current.Carts.DeleteShoppingCart(CartSession.First().Id);
                }
                CartSession = null;
                Response.Redirect("/home/");
            }
            catch (System.Threading.ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "Checkout - Cancel");
            }
        }

        private void saveOrder()
        {
            ORDERS order = new ORDERS();

            order.TotalAmount = TotalAmount.Value + ShipCost;

            order.BonusUsed = 0;
            order.AmountPaid = TotalOrder;
            order.Verified = false;

            order.Canceled = false;
            order.Completed = false;

            order.Status = 1;

            string paymentType = String.Empty;

            int i;
            try
            {
                order.CustomerID = CurrentCustomer.Id;
                if (Int32.TryParse(selectedPayment.Value, out i))
                {
                    if (i == 3 || i == 4)
                    {
                        order.PAYMENT = new PAYMENT() { Type = (int) PaymentType.CA };
                        order.PAYMENT.CASH_PAYMENT = new CASH_PAYMENT() { Amount = TotalOrder };
                        if (i == 3)
                        {
                            order.PAYMENT.CASH_PAYMENT.Comments = txtCashComments.Text;
                            order.PAYMENT.CASH_PAYMENT.Receiver = "Korrier";
                            //TODO paid date
                            order.ShippingID = 3;
                            paymentType = "Korrier";
                        }
                        else
                        {
                            order.PAYMENT.CASH_PAYMENT.Receiver = "Zyra";
                            order.ShippingID = 1;
                            paymentType = "Zyra";
                        }
                    }
                }
                if ((ddlShippingAddress.SelectedValue == "0" || ddlShippingAddress.Items.Count == 0) && i == 3)
                {
                    writeResult(Resources.Lang.SpecifyShippingAddress);
                    radioBtnCarrier.Checked = true;
                    divConfirmLink.Visible = true;
                    return;
                }
                // setting used bonuses in the order object
                setBonus(order);
                order.DateCreated = DateTime.Now;

                if (i == 3)
                {
                    // addresses
                    ADDRESSINFO shipping = new ADDRESSINFO(address1.GetAddress());
                    //ADDRESSINFO billing = new ADDRESSINFO(address2.GetAddress());
                    order.ADDRESSINFO = shipping;
                    //order.ADDRESSINFO1 = billing;
                }

                //inserting and not saving if there are any details, as the saving will be done in the details insertion
                ApplicationContext.Current.Orders.Insert(order, true, false);

                // inserting the products of this order
                List<SHOPPING_CART> carts = ApplicationContext.Current.Carts.GetShoppingCartItems(CartSession.First().Id);

                ApplicationContext.Current.Orders.InsertDetailsFromCart(order, carts, false);

                List<ORDER_DETAIL> details = ApplicationContext.Current.Orders.GetDetails(order.ID);

                Thread thread = new Thread(() => sendOrderMailToAdmins(details, paymentType, txtCashComments.Text, TotalOrder, CurrentCustomer.FullName));
                thread.Start();

                CUSTOMER customer = ApplicationContext.Current.Customers.GetByEmail(User.Identity.Name);

                Thread thread2 = new Thread(() => sendOrderMailToCustomer(customer, details, "Porosia ne FZone.al"));
                thread2.Start();

                CartSession = null;

                Response.Redirect("/personal/orderDet/" + FashionZone.BL.Util.Encryption.Encrypt(order.ID.ToString()));
            }
            catch (System.Threading.ThreadAbortException ex)
            {
            }
            catch (Exception e)
            {
                //TODO log exception
                Log(e, e.Message, e.StackTrace, "Checkout.Save");
                writeResult(Resources.Lang.ErrorVerifiedLabel);
            }
        }

        private void writeResult(string Message)
        {
            litResult.Text = Message;
            updResult.Update();
        }

        protected void ddlBonus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBonus.SelectedValue == "-1")
            {
                BonusUsed = 0;
                accPanePaypal.Visible = true;
                accPaneEasypay.Visible = true;
                radioBtnEasypay.Checked = false;
                radioBtnPaypal.Checked = true;
                radioBtnCarrier.Checked = false;
                radioBtnOffice.Checked = false;
                accordionPayment.SelectedIndex = 0;
                selectedPayment.Value = "1";
                updPnlPayment.Update();
            }
            else
            {
                decimal d = 0;
                if (Decimal.TryParse(ddlBonus.SelectedItem.Text, out d))
                {
                    if (d <= TotalAmount)
                    {
                        BonusUsed = d;
                    }
                    else
                    {
                        BonusUsed = TotalAmount.Value;
                        accPanePaypal.Visible = false;
                        accPaneEasypay.Visible = false;
                        radioBtnEasypay.Checked = false;
                        radioBtnPaypal.Checked = false;
                        radioBtnCarrier.Checked = true;
                        accordionPayment.SelectedIndex = 0;
                        divConfirmLink.Style.Add("display", "block");
                        selectedPayment.Value = "3";
                        updPnlPayment.Update();
                    }
                }
            }
        }

        private void setBonus(ORDERS order)
        {
            int bonusID = 0;

            if (Int32.TryParse(ddlBonus.SelectedValue, out bonusID) && bonusID != -1)
            {
                BONUS bonus = new BONUS() { ID = bonusID, Version = Version.Where(b => b.Key == bonusID).FirstOrDefault().Value };

                ORDER_BONUS ordBonus = new ORDER_BONUS() { OrderID = order.ID, BonusID = bonusID, Value = BonusUsed, ORDERS = order, BONUS = bonus };

                order.BonusUsed = BonusUsed;
                order.ORDER_BONUS.Add(ordBonus);
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            modalPopup.Hide();
            Response.Redirect("/home/");
        }

        protected void btnPaypal_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlShippingAddress.SelectedValue == "0" || ddlShippingAddress.Items.Count == 0)
            {
                writeResult(Resources.Lang.SpecifyShippingAddress);
                radioBtnPaypal.Checked = true;
                return;
            }

            RefreshCart();
            if (CartSession != null && CartSession.Count > 0)
            {
                preparePayPalForm();
            }
            else
            {
                //popup with message and redirect to home page.
                modalPopup.Show();
            }
        }

        private void preparePayPalForm()
        {
            CultureInfo culture = new CultureInfo("en-US");
            NameValueCollection paypalCreator = new NameValueCollection();
            paypalCreator.Add("cmd", "_cart");
            paypalCreator.Add("upload", "1");
            paypalCreator.Add("return", Configuration.PaypalReturnUrl);
            //paypalCreator.Add("notify_url", Configuration.PaypalNotifyUrl);
            paypalCreator.Add("cancel_return", Configuration.PaypalCancelUrl);
            paypalCreator.Add("business", Configuration.PaypalSellerEmail);
            paypalCreator.Add("currency_code", Configuration.PaypalCurrency);
            paypalCreator.Add("discount_amount_cart", BonusUsed.ToString("N2", culture));
            paypalCreator.Add("invoice", CartSession.First().Id);
            if (BonusUsed > 0)
            {
                paypalCreator.Add("custom", address1.AddrID.ToString() + "-" + ddlBonus.SelectedValue + "-" + BonusUsed.ToString("N2", culture));
            }
            else
            {
                paypalCreator.Add("custom", address1.AddrID.ToString());
            }
            for (int i = 1; i <= CartSession.Count; i++)
            {
                paypalCreator.Add("item_number_" + i, CartSession.ElementAt(i - 1).ProductAttributeId.ToString());
                paypalCreator.Add("item_name_" + i, CartSession.ElementAt(i - 1).FullName);

                paypalCreator.Add("amount_" + i, CartSession.ElementAt(i - 1).Price.ToString("N2", culture));
                paypalCreator.Add("quantity_" + i, CartSession.ElementAt(i - 1).Quantity.ToString());
            }

            RedirectAndPOST(this, paypalCreator, Configuration.PaypalEnv);
            lblModalMessage.Text = Resources.Lang.PassingToPaypalLabel;
            btnModalOk.Visible = false;
            modalPopup.Show();
        }

        public static void RedirectAndPOST(Page page, NameValueCollection data, string Env)
        {

            //Prepare the Posting form
            string strForm = PreparePOSTForm(Env, data);

            //Add a literal control the specified page holding the Post Form, this is to submit the Posting form with the request.
            page.Controls.Add(new LiteralControl(strForm));
        }

        private static String PreparePOSTForm(string url, NameValueCollection data)
        {
            //Set a name for the form
            string formID = "PostForm";

            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
            foreach (string key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + data[key] + "\">");
            }
            strForm.Append("</form>");

            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." + formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");

            //Return the form and the script concatenated. (The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }

        protected void btnEasyPay_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlShippingAddress.SelectedValue == "0" || ddlShippingAddress.Items.Count == 0)
            {
                writeResult(Resources.Lang.SpecifyShippingAddress);
                radioBtnEasypay.Checked = true;
                return;
            }

            RefreshCart();
            if (CartSession != null && CartSession.Count > 0)
            {
                prepareEasyPayForm();
            }
            else
            {
                //popup with message and redirect to home page.
                modalPopup.Show();
            }
        }

        private void prepareEasyPayForm()
        {
            Session["AddrForEasyPay"] = address1.AddrID;
            Session["BonForEasyPay"] = BonusUsed;
            Session["BonIdForEasyPay"] = ddlBonus.SelectedValue;

            CultureInfo culture = new CultureInfo("en-US");
            NameValueCollection paypalCreator = new NameValueCollection();
            paypalCreator.Add("Orderid", CartSession.First().Id);
            paypalCreator.Add("Mer_refno", Configuration.EasyPayMerchantRef);
            paypalCreator.Add("Mer_username",  Configuration.EasyPayMerchantUser);

            decimal rate = ApplicationContext.Current.Payments.GetLastConversionRate().CurrencyRate + Configuration.CurrencyDelta;
            decimal amount = (decimal)rate * TotalOrder;
            paypalCreator.Add("Amount", amount.ToString("N2", culture));

            RedirectAndPOST(this, paypalCreator, Configuration.EasyPayEnv);

            lblModalMessage.Text = Resources.Lang.PassingToEasypayLabel;
            btnModalOk.Visible = false;
            modalPopup.Show();
        }
    }
}