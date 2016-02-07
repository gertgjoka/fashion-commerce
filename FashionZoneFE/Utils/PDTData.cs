using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace FashionZone.FE.Utils
{
    public class PDTData
    {
        public Decimal GrossTotal { get; set; }
        public Decimal Fee { get; set; }
        public string Invoice { get; set; }
        public string PaymentStatus { get; set; }
        public string PayerFirstName { get; set; }
        public string PayerStatus { get; set; }
        public string BusinessEmail { get; set; }
        public string PayerEmail { get; set; }
        public string TxToken { get; set; }
        public string PayerLastName { get; set; }
        public string ReceiverEmail { get; set; }
        public string Currency { get; set; }
        public string TransactionId { get; set; }
        // In the case of a refund, reversal, or canceled reversal, 
        // this variable contains the txn_id of the original transaction, while txn_id contains a new ID for the new transaction.
        public string ParentTransactionId { get; set; }
        public string SubscriberId { get; set; }
        public string Custom { get; set; }
        public DateTime PaymentDate { get; set; }

        public static PDTData Parse(string postData, bool IPN)
        {
            String sKey, sValue;
            PDTData ph = new PDTData();

            try
            {
                //split response into string array using whitespace delimeter
                String[] StringArray = postData.Split('\n');

                // NOTE:
                /*
                * loop is set to start at 1 rather than 0 because first
                string in array will be single word SUCCESS or FAIL
                Only used to verify post data
                */
                CultureInfo ci = new CultureInfo("en-US");
                // use split to split array we already have using "=" as delimiter
                int i;
                for (i = 0; i < StringArray.Length - 1; i++)
                {
                    if (!IPN && i == 0)
                    {
                        continue;
                    }
                    String[] StringArray1 = StringArray[i].Split('=');

                    if (StringArray1.Length < 2)
                    {
                        continue;
                    }
                    sKey = StringArray1[0];
                    sValue = StringArray1[1];

                    // set string vars to hold variable names using a switch
                    switch (sKey)
                    {
                        case "mc_gross":
                            ph.GrossTotal = Convert.ToDecimal(sValue, ci);
                            break;

                        case "mc_fee":
                            ph.Fee = Convert.ToDecimal(sValue, ci);
                            break;

                        case "invoice":
                            ph.Invoice = sValue;
                            break;

                        case "payment_status":
                            ph.PaymentStatus = Convert.ToString(sValue);
                            break;

                        case "payment_date":
                            ph.PaymentDate = ConvertPayPalDateTime(sValue);
                            break;

                        case "first_name":
                            ph.PayerFirstName = Convert.ToString(sValue);
                            break;

                        case "business":
                            ph.BusinessEmail = Convert.ToString(sValue);
                            break;

                        case "payer_email":
                            ph.PayerEmail = Convert.ToString(sValue);
                            break;

                        case "Tx Token":
                            ph.TxToken = Convert.ToString(sValue);
                            break;

                        case "last_name":
                            ph.PayerLastName = Convert.ToString(sValue);
                            break;

                        case "receiver_email":
                            ph.ReceiverEmail = Convert.ToString(sValue);
                            break;

                        case "mc_currency":
                            ph.Currency = Convert.ToString(sValue);
                            break;

                        case "txn_id":
                            ph.TransactionId = Convert.ToString(sValue);
                            break;

                        case "custom":
                            ph.Custom = Convert.ToString(sValue);
                            break;

                        case "subscr_id":
                            ph.SubscriberId = Convert.ToString(sValue);
                            break;

                        case "payer_status":
                            ph.PayerStatus = Convert.ToString(sValue);
                            break;

                        case "parent_txn_id":
                            ph.ParentTransactionId = Convert.ToString(sValue);
                            break;
                    }
                }

                return ph;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime ConvertPayPalDateTime(string payPalDateTime)
        {
            // accept a few different date formats because of PST/PDT timezone and slight month difference in sandbox vs. prod.
            string[] dateFormats = { "HH:mm:ss MMM dd, yyyy PST", "HH:mm:ss MMM. dd, yyyy PST", "HH:mm:ss MMM dd, yyyy PDT", "HH:mm:ss MMM. dd, yyyy PDT" };
            DateTime outputDateTime;

            DateTime.TryParseExact(payPalDateTime, dateFormats, new CultureInfo("en-US"), DateTimeStyles.None, out outputDateTime);

            // convert to local timezone
            // UTC + 1
            outputDateTime = outputDateTime.AddHours(8);

            return outputDateTime;
        }
    }
}