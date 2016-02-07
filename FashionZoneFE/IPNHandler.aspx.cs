using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net;
using FashionZone.BL;
using FashionZone.FE.Utils;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using FashionZone.FE.Utils;
using System.Threading;
using System.Globalization;

namespace FashionZone.FE
{
    public partial class IPNHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            processRequest();
        }

        protected void processRequest()
        {
            try
            {
                string postUrl = Configuration.PaypalEnv;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(postUrl);

                //Set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);
                string ipnPost = strRequest;
                strRequest += "&cmd=_notify-validate";
                req.ContentLength = strRequest.Length;

                //Send the request to PayPal and get the response
                StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                streamOut.Write(strRequest);
                streamOut.Close();

                StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
                string strResponse = streamIn.ReadToEnd();
                streamIn.Close();

                if (strResponse == "VERIFIED")
                {
                    strRequest = HttpUtility.UrlDecode(strRequest);
                    strRequest = strRequest.Replace('&', '\n');
                    PDTData pdt = PDTData.Parse(strRequest, true);
                    //check that receiver_email is your Primary PayPal email
                    if (pdt.ReceiverEmail != Configuration.PaypalSellerEmail)
                    {
                        BasePage.Log(null, "PayPal receiver email different from default:" + pdt.ReceiverEmail + " - txnID: " + pdt.TransactionId, "", "IPN Handler");
                        return;
                    }

                    //check that payment_amount/payment_currency are correct
                    if (pdt.Currency != Configuration.PaypalCurrency)
                    {
                        BasePage.Log(null, "PayPal currency different from default:" + pdt.ReceiverEmail + " - txnID: " + pdt.TransactionId, "", "IPN Handler");
                        return;
                    }

                    //check the payment_status is Completed

                    //check that txn_id has not been previously processed
                    PAYPAL_PAYMENT pp = ApplicationContext.Current.Payments.GetByTxnId(pdt.TransactionId);
                    List<SHOPPING_CART> carts = ApplicationContext.Current.Carts.GetShoppingCartItems(pdt.Invoice);

                    if (pdt.PaymentStatus.ToUpper() == "COMPLETED" || pdt.PaymentStatus.ToUpper() == "PENDING" || pdt.PaymentStatus.ToUpper() == "PROCESSED")
                    {
                        // first time proccessing for this payment
                        if (pp == null && carts != null && carts.Count > 0)
                        {
                            savePayPalOrder(pdt, carts, strRequest);
                        }
                        else if (pp == null)
                        {
                            BasePage.Log(null, "PayPal " + pdt.PaymentStatus + " TxnId: " + pdt.TransactionId + " but CART EXPIRED \n Response: " + strRequest, "", "IPN Handler");
                            BasePage.sendMailToAdmins("PayPal payment " + pdt.PaymentStatus + " but cart expired. PLACE REFUND!", "A " + pdt.PaymentStatus + " IPN was notified for cart " + pdt.Invoice +
                                " from " + pdt.PayerEmail + " for the amount " + pdt.GrossTotal + " " + pdt.Currency + " but the cart expired. \nProceed with REFUND!");
                        }
                    }
                    else
                    {
                        //In the case of a refund, reversal, or canceled reversal 
                        if (pp == null && !String.IsNullOrWhiteSpace(pdt.ParentTransactionId))
                        {
                            pp = ApplicationContext.Current.Payments.GetByTxnId(pdt.ParentTransactionId);
                        }
                    }
                    // update of existing paypal transaction
                    if (pp != null)
                    {
                        updatePayPalData(pdt, pp, strRequest);
                    }
                }
                else if (strResponse == "INVALID")
                {
                    BasePage.Log(null, "Invalid IPN request" + strRequest, "", "IPN Handler");
                }
                else
                {
                    BasePage.Log(null, "IPN request" + strRequest, "", "IPN Handler");
                }
            }
            catch (Exception ex)
            {
                BasePage.Log(ex, ex.Message, ex.StackTrace, "paypalSuccess.Save");
            }
        }

        protected void savePayPalOrder(PDTData data, List<SHOPPING_CART> carts, string originalResponse)
        {
            CultureInfo ci = new CultureInfo("en-US");
            string cartID = data.Invoice;
            if (carts != null && carts.Count > 0)
            {
                decimal total = ApplicationContext.Current.Carts.GetShoppingCartTotalAmount(cartID).Value;

                ORDERS order = new ORDERS();

                int addrID = 0;
                // Bonus has been used
                if (total != data.GrossTotal)
                {
                    string[] custom = data.Custom.Split('-');
                    if (custom.Length == 3)
                    {
                        decimal bonus = Convert.ToDecimal(custom[2], ci);
                        int bonusID = Convert.ToInt32(custom[1]);
                        addrID = Convert.ToInt32(custom[0]);
                        if (bonus == total - data.GrossTotal)
                        {
                            setBonus(order, bonusID, bonus);
                        }
                        else
                        {
                            BasePage.Log(null, "IPN order save - bonus MANIPULATION. Original:" + (total - data.GrossTotal) + " new: " + bonus +
                                " - txnID: " + data.TransactionId, "", "IPN Handler");
                            return;
                        }
                    }
                }
                else
                {
                    addrID = Convert.ToInt32(data.Custom);
                }

                order.TotalAmount = total;
                order.AmountPaid = data.GrossTotal;
                order.Verified = false;
                order.Canceled = false;
                if (data.PaymentStatus.ToUpper() == "COMPLETED")
                {
                    order.Completed = true;
                }
                else
                {
                    order.Completed = false;
                }
                order.Status = 1;

                try
                {
                    order.CustomerID = carts.First().CustomerID;

                    order.PAYMENT = new PAYMENT() { Type = 2 };
                    order.PAYMENT.PAYPAL_PAYMENT = new PAYPAL_PAYMENT()
                    {
                        Amount = data.GrossTotal,
                        CartID = cartID,
                        PaidOn = DateTime.Now,
                        Currency = data.Currency,
                        Fee = data.Fee,
                        PayerEmail = data.PayerEmail,
                        TransactionKey = data.TransactionId,
                        PayerName = data.PayerFirstName + " " + data.PayerLastName,
                        PaypalEmail = data.ReceiverEmail,
                        Response = originalResponse,
                        TransactionStatus = data.PaymentStatus,
                        PayerStatus = data.PayerStatus
                    };

                    // for now it is set to carrier without a fee
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

                    Thread thread = new Thread(() => BasePage.sendOrderMailToAdmins(details, "PayPal", "PayPal Order", order.TotalAmount, customer.Name + " " + customer.Surname));
                    thread.Start();

                    Thread thread2 = new Thread(() => BasePage.sendOrderMailToCustomer(customer, details, "Porosia ne FZone.al"));
                    thread2.Start();
                }
                catch (System.Threading.ThreadAbortException ex)
                {
                }
                catch (Exception e)
                {
                    //TODO log exception
                    BasePage.Log(e, e.Message, e.StackTrace, "paypalSuccess.Save");
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

        protected void updatePayPalData(PDTData data, PAYPAL_PAYMENT pp, string originalResponse)
        {
            pp.TransactionStatus = data.PaymentStatus;
            pp.Response = originalResponse;
            ApplicationContext.Current.Payments.UpdatePayPalPayment(pp, false);

            if (data.PaymentStatus.ToUpper() == "COMPLETED")
            {
                ORDERS order = ApplicationContext.Current.Orders.GetById(pp.ID);
                order.Completed = true;
                ApplicationContext.Current.Orders.Update(order, false);
            }
            else if (data.PaymentStatus.ToUpper() == "REFUNDED" || data.PaymentStatus.ToUpper() == "REVERSED")
            {
                ORDERS order = ApplicationContext.Current.Orders.GetById(pp.ID);
                order.Completed = false;
                order.Canceled = true;
                // I anulluar / canceled
                order.Status = 6;
                order.Comments = order.Comments + "Order was canceled because: " + data.PaymentStatus;
                ApplicationContext.Current.Orders.Update(order, false);
                BasePage.Log(null, "PayPal Reversal/Refund: " + data.PaymentStatus + " TxnId: " + pp.TransactionKey + "\n Response: " + originalResponse, "", "IPN Handler");
                Thread thread = new Thread(() => BasePage.sendMailToAdmins("PayPal payment " + data.PaymentStatus + " for Order: " + pp.ID, "A " + data.PaymentStatus + " IPN was notified for order " + pp.ID +
                    " from " + data.PayerEmail + " for the amount " + data.GrossTotal + " " + data.Currency));
                thread.Start();
            }
            else
            {
                BasePage.Log(null, "Payment status from PayPal not handled: " + data.PaymentStatus + "\n Response: " + originalResponse, "", "IPN Handler");
            }
        }
    }
}