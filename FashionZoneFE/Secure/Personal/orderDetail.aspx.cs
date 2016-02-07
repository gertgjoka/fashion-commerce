using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.BL.Util;
using FashionZone.DataLayer.Model;

namespace FashionZone.FE.Secure.Personal
{
    public partial class orderDetail : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //MasterPage set cssClass
            ((SiteMaster)Master).SetImgBackground("", "ContentIII");
            try
            {
                if (RouteData.Values["ord"] != null
                    && !String.IsNullOrWhiteSpace(RouteData.Values["ord"].ToString()))
                {
                    string productId = Encryption.Decrypt(RouteData.Values["ord"].ToString());
                    int id;
                    if (int.TryParse(productId, out id))
                    {
                        ORDERS order = null;
                        order = ApplicationContext.Current.Orders.GetById(id);
                        litOrderId.Text = String.Format(FashionZone.BL.Configuration.OrderNrFormatting, order.ID);
                        litOrderData.Text = order.DateCreated.ToString("dd/MM/yyyy");
                        if (order.DateShipped.HasValue)
                        {
                            litOrderShippingStart.Text = order.DateShipped.Value.ToString("dd/MM/yyyy");
                            litOrderShippingStartTit.Visible = true;
                        }
                        if (order.DateDelivered.HasValue)
                        {
                            litOrdeDateShippmentEnd.Text = order.DateDelivered.Value.ToString("dd/MM/yyyy");
                            litShippingEndTit.Visible = true;
                        }
                        litOrderCampagnName.Text = ApplicationContext.Current.Orders.GetOrderCampaigns(id);
                        litSubTotale.Text = order.TotalAmount.ToString("N2");
                        litBonus.Text = order.BonusUsed.HasValue ? order.BonusUsed.Value.ToString("N2") : "";

                        if (order.SHIPPING != null)
                        {
                            litShippinCost.Text = order.SHIPPING.ShippingCost.ToString("N2");
                            litTotalPaied.Text = order.AmountPaid.ToString("N2");
                        }

                        if (order.ADDRESSINFO != null)
                        {
                            litOrderTel.Text = order.ADDRESSINFO.Telephone;

                            litOrderAddresShipping.Text = order.ADDRESSINFO.Name;
                            litOrderAddresShipping.Text += "<br />" + order.ADDRESSINFO.Address;
                            litOrderAddresShipping.Text += "<br />" + order.ADDRESSINFO.Location + ", " + order.ADDRESSINFO.CityName + ", " + order.ADDRESSINFO.PostCode;
                        }

                        if (order.Status.HasValue && order.Status.Value == 6)
                        {
                            if (Session["Culture"] == null || Session["Culture"] == "sq-AL")
                            {
                                litStatus.Text += string.Format("<a href=\"#\" class=\"StatoOn\">{0}</a>", "I Anulluar");
                            }
                            else
                            {
                                litStatus.Text += string.Format("<a href=\"#\" class=\"StatoOn\">{0}</a>", "Canceled");
                            }
                        }

                        else
                        {
                            foreach (var s in ApplicationContext.Current.Orders.GetStatuses().OrderBy(st => st.ID))
                            {
                                if (s.ID != 6)
                                {
                                    if (order.Status.HasValue && s.ID == order.Status.Value)
                                        litStatus.Text += string.Format("<a href=\"#\" class=\"StatoOn\">{0}</a>", s.Name);
                                    else
                                        litStatus.Text += string.Format("<a href=\"#\" class=\"StatoOff\">{0}</a>", s.Name);
                                }
                            }
                        }
                        RepeatOrderDet.DataSource = order.ORDER_DETAIL;
                        RepeatOrderDet.DataBind();

                        if (order.PAYMENT != null)
                        {
                            if (order.PAYMENT.Type == (int)PaymentType.CA)
                            {

                                libPaymetType.Text = "Cash";
                                lblTotalPay.Text = "€ " + order.PAYMENT.CASH_PAYMENT.Amount.Value.ToString("N2");
                                libTransNum.Text = order.PAYMENT.CASH_PAYMENT.Comments;

                            }
                            else if (order.PAYMENT.Type == (int)PaymentType.PP)
                            {
                                libPaymetType.Text = "PayPal";
                                lblTotalPay.Text = "€ " + order.PAYMENT.PAYPAL_PAYMENT.Amount.Value.ToString("N2");
                                libTransNum.Text = order.PAYMENT.PAYPAL_PAYMENT.TransactionStatus + " Id: " + order.PAYMENT.PAYPAL_PAYMENT.TransactionKey;
                            }
                            else if (order.PAYMENT.Type == (int)PaymentType.EP)
                            {
                                libPaymetType.Text = "EasyPay";
                                lblTotalPay.Text = order.PAYMENT.EASYPAY_PAYMENT.Amount.ToString("N2") + " Lek";
                                libTransNum.Text = order.PAYMENT.EASYPAY_PAYMENT.TransactionStatus + " Id: " + order.PAYMENT.EASYPAY_PAYMENT.TransactionID;
                            }
                        }

                        if (order.Completed)
                        {
                            btnPdfDownload.Attributes.Add("onclick", "javascript:popWin('" + RouteData.Values["ord"].ToString() + "');return false;");
                            divBtnInvoice.Visible = true;
                        }
                        else
                        {
                            divBtnInvoice.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "OrderDetail");
            }
            base.Page_Load(sender, e);
        }
    }
}