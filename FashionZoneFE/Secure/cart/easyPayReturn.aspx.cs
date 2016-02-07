using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.FE.Utils;
using FashionZone.BL;
using System.Net;
using System.Text;
using System.IO;
using System.Globalization;
using FashionZone.DataLayer.Model;
using System.Threading;

namespace FashionZone.FE.Secure.cart
{
    public partial class easyPayReturn : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((SiteMaster)Master).SetImgBackground("", "ContentIII");
            if (!Page.IsPostBack)
            {
                processEasyPayRequest();
            }
            base.Page_Load(sender, e);
        }

        protected void processEasyPayRequest()
        {
            try
            {
                string postUrl = Configuration.EasyPayDualAuth;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(postUrl);
                string strResponse;
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);
                strRequest = HttpUtility.UrlDecode(strRequest);
                EasyPayData unverified = EasyPayData.ParseUnverified(strRequest);

                string query = String.Format("OrderId={0}&Mer_username={1}&Mer_refno={2}", unverified.OrderId, Configuration.EasyPayMerchantUser, Configuration.EasyPayMerchantRef);
                // Write the request for dual auth
                StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                stOut.Write(query);
                stOut.Close();

                StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
                strResponse = stIn.ReadToEnd();
                stIn.Close();
                strResponse = HttpUtility.UrlDecode(strResponse);
                EasyPayData dataVerified = EasyPayData.Parse(strResponse);

                EASYPAY_PAYMENT pp = ApplicationContext.Current.Payments.GetEPByTxnId(dataVerified.TransactionId);
                List<SHOPPING_CART> carts = ApplicationContext.Current.Carts.GetShoppingCartItems(dataVerified.OrderId);

                // success
                if (dataVerified.ErrorCode == 400)
                {
                    if (pp == null && carts != null && carts.Count > 0)
                    {
                        saveEasyPayOrder(dataVerified, carts, strRequest, unverified.ServiceCharge);
                    }
                    else if (pp == null)
                    {
                        BasePage.Log(null, "EasyPay " + dataVerified.ErrorCodeS + " TxnId: " + dataVerified.TransactionId + " but CART EXPIRED \n Response: " + strRequest, "", "EasyPay Return");
                        BasePage.sendMailToAdmins("EasyPay payment " + dataVerified.ErrorCodeS + " but cart expired. PLACE REFUND!", "A " + dataVerified.ErrorCodeS + " EasyPay was notified for cart " + dataVerified.OrderId +
                            " from " + User.Identity.Name + " for the amount " + dataVerified.Amount + " but the cart expired. \nProceed with REFUND!");
                    }
                    else
                    {
                        BasePage.Log(null, "EasyPay already processed payment -  TxnId: " + dataVerified.TransactionId + " Response: " + strRequest, "", "EasyPay Return");
                        lblResult.Text = Resources.Lang.PaymentProblemLiteral;
                    }
                }
                else
                {
                    BasePage.Log(null, "EasyPay, transaction did not succeed - code:" + dataVerified.ErrorCode + " - response:" + dataVerified.ErrorCodeS + " OrderId: " + dataVerified.OrderId + " - txnID: " + dataVerified.TransactionId + " - Amount:" + dataVerified.Amount.ToString("N2"), "", "easyPay.Proccess");
                    lblResult.Text = Resources.Lang.PaymentProblemLiteral + "</b> <br/><br/>" +
                    Resources.Lang.SaveTransactionID + " <b>" + dataVerified.TransactionId + "</b>"; ;
                    lblPDT.Text = unverified.Message;
                    lblPDT.Visible = true;
                }
            }
            catch (Exception ex)
            {
                BasePage.Log(ex, ex.Message, ex.StackTrace, "easyPay.Proccess");
                lblResult.Text = Resources.Lang.PaymentProblemLiteral;
            }
        }

        protected void saveEasyPayOrder(EasyPayData data, List<SHOPPING_CART> carts, string originalResponse, decimal charge)
        {
            if (carts != null && carts.Count > 0)
            {
                int addrId, bonId = 0;
                decimal bonusUsed = 0;
                if (Session["AddrForEasyPay"] != null)
                {
                    addrId = (int)Session["AddrForEasyPay"];
                }
                else
                {
                    BasePage.Log(null, "EasyPay save - null address.", "", "EasyPay.saveOrder");
                }
                
                CultureInfo ci = new CultureInfo("en-US");
                string cartID = data.OrderId;

                decimal total = ApplicationContext.Current.Carts.GetShoppingCartTotalAmount(cartID).Value;

                ORDERS order = new ORDERS();

                int addrID = 0;
                // Bonus has been used
                decimal rate = ApplicationContext.Current.Payments.GetLastConversionRate().CurrencyRate + Configuration.CurrencyDelta;
                decimal amount = data.Amount / rate;
                if (total != amount)
                {
                    if (Session["BonForEasyPay"] != null && Session["BonIdForEasyPay"] != null && !String.IsNullOrWhiteSpace(Session["BonIdForEasyPay"].ToString()))
                    {
                        bonusUsed = (decimal)Session["BonForEasyPay"];
                        bonId = (int)Session["BonIdForEasyPay"];
                    }
                    if (bonusUsed == total - amount)
                    {
                        setBonus(order, bonId, bonusUsed);
                    }
                    else
                    {
                        BasePage.Log(null, "EasyPay order save - bonus MANIPULATION. Original:" + (total - amount) + " new: " + bonusUsed +
                            " - txnID: " + data.TransactionId, "", "EasyPay order save");
                        return;
                    }
                }

                order.TotalAmount = total;
                order.AmountPaid = amount;
                order.Verified = false;
                order.Canceled = false;

                order.Completed = true;
                order.Status = 1;

                try
                {
                    order.CustomerID = carts.First().CustomerID;

                    order.PAYMENT = new PAYMENT() { Type = 3 };
                    originalResponse = originalResponse.Replace("&", "\n");
                    order.PAYMENT.EASYPAY_PAYMENT = new EASYPAY_PAYMENT()
                    {
                        Amount = data.Amount,
                        MerchantUsername = Configuration.EasyPayMerchantUser,
                        TransactionID = data.TransactionId,
                        ResponseCode = data.ErrorCode,
                        Rate = rate,
                        OriginalResponse = originalResponse,
                        TransactionStatus = data.ErrorCodeS,
                        Date = DateTime.Now,
                        Fee = charge
                    };

                    // TODO for now it is set to carrier without a fee
                    order.ShippingID = 3;

                    order.DateCreated = DateTime.Now;
                    ADDRESS addr = ApplicationContext.Current.Customers.GetAddressById(addrID);
                    // addresses
                    if (addr != null)
                    {
                        ADDRESSINFO shipping = new ADDRESSINFO(addr);
                        order.ADDRESSINFO = shipping;
                    }
                    //inserting and not saving as the saving will be done in the details insertion
                    ApplicationContext.Current.Orders.Insert(order, true, false);

                    // inserting the products of this order

                    ApplicationContext.Current.Orders.InsertDetailsFromCart(order, carts, false);

                    List<ORDER_DETAIL> details = ApplicationContext.Current.Orders.GetDetails(order.ID);

                    CUSTOMER customer = ApplicationContext.Current.Customers.GetById(carts[0].CustomerID);

                    Thread thread = new Thread(() => BasePage.sendOrderMailToAdmins(details, "EasyPay", "EasyPay Order", order.TotalAmount, customer.Name + " " + customer.Surname));
                    thread.Start();

                    Thread thread2 = new Thread(() => BasePage.sendOrderMailToCustomer(customer, details, "Porosia ne FZone.al"));
                    thread2.Start();

                    lblResult.Text = "<b>" + Resources.Lang.PayPalThankyouLabel + "</b> <br/><br/>" +
                    Resources.Lang.SaveTransactionID + "<b>" + data.TransactionId + "</b>";
                }
                catch (System.Threading.ThreadAbortException ex)
                {
                }
                catch (Exception e)
                {
                    //TODO log exception
                    BasePage.Log(e, e.Message, e.StackTrace, "easyPay.Save");
                    lblResult.Text = Resources.Lang.PaymentProblemLiteral + "</b> <br/><br/>" +
                    Resources.Lang.SaveTransactionID + "<b>" + data.TransactionId + "</b>"; ;
                }
            }
        }
        private void setBonus(ORDERS order, int bonusID, decimal BonusUsed)
        {
            BONUS bonus = new BONUS() { ID = bonusID };
            ORDER_BONUS ordBonus = new ORDER_BONUS() { OrderID = order.ID, BonusID = bonusID, Value = BonusUsed, ORDERS = order, BONUS = bonus };
            order.BonusUsed = BonusUsed;
            order.ORDER_BONUS.Add(ordBonus);
        }
    }
}