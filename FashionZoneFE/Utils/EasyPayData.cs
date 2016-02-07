using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FashionZone.FE.Utils
{
    public class EasyPayData
    {
        public int ErrorCode { get; set; }
        public string ErrorCodeS { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public decimal ServiceCharge { get; set; }
        public string Message { get; set; }

        public static EasyPayData Parse(string postData)
        {
            EasyPayData data = new EasyPayData();
            string[] response = postData.Split('|');
            CultureInfo ci = new CultureInfo("en-US");

            if (response.Length > 0)
            {
                data.ErrorCode = Int32.Parse(response[0]);
                data.TransactionId = response[1];
                data.OrderId = response[2];
                data.Amount = Decimal.Parse(response[3], ci);
                switch (data.ErrorCode)
                {
                    case 400:
                        data.ErrorCodeS = "Success";
                        break;
                    case 401:
                        data.ErrorCodeS = "Invalid user name or reference number";
                        break;
                    case 402:
                        data.ErrorCodeS = "Under process";
                        break;
                    case 403:
                        data.ErrorCodeS = "Declined";
                        break;
                    case 404:
                        data.ErrorCodeS = "Killed";
                        break;
                    case 405:
                        data.ErrorCodeS = "Pending";
                        break;
                    case 406:
                        data.ErrorCodeS = "Empty value";
                        break;
                    case 407:
                        data.ErrorCodeS = "Incorrect mobile no";
                        break;
                    case 408:
                        data.ErrorCodeS = "Invalid IP";
                        break;
                    case 471:
                        data.ErrorCodeS = "Invalid amount";
                        break;
                    case 411:
                        data.ErrorCodeS = "Transaction exist";
                        break;
                    case 500:
                        data.ErrorCodeS = "Timeout";
                        break;
                    case 305:
                        data.ErrorCodeS = "User not registered";
                        break;
                    case 304:
                        data.ErrorCodeS = "Registration pending";
                        break;
                    case 302:
                        data.ErrorCodeS = "Registered User";
                        break;
                }
            }
            return data;
        }

        public static EasyPayData ParseUnverified(string postData)
        {
            String sKey, sValue;
            EasyPayData data = new EasyPayData();
            String[] StringArray = postData.Split('&');
            CultureInfo ci = new CultureInfo("en-US");
            // use split to split array we already have using "=" as delimiter
            int i;
            for (i = 0; i < StringArray.Length - 1; i++)
            {
                String[] StringArray1 = StringArray[i].Split('=');

                sKey = StringArray1[0];
                sValue = StringArray1[1];

                // set string vars to hold variable names using a switch
                switch (sKey)
                {
                    case "errorcode":
                        data.ErrorCode = Int32.Parse(sValue);
                        switch (data.ErrorCode)
                        {
                            case 400:
                                data.ErrorCodeS = "Success";
                                break;
                            case 401:
                                data.ErrorCodeS = "Invalid user name or reference number";
                                break;
                            case 402:
                                data.ErrorCodeS = "Under process";
                                break;
                            case 403:
                                data.ErrorCodeS = "Declined";
                                break;
                            case 404:
                                data.ErrorCodeS = "Killed";
                                break;
                            case 405:
                                data.ErrorCodeS = "Pending";
                                break;
                            case 406:
                                data.ErrorCodeS = "Empty value";
                                break;
                            case 407:
                                data.ErrorCodeS = "Incorrect mobile no";
                                break;
                            case 408:
                                data.ErrorCodeS = "Invalid IP";
                                break;
                            case 471:
                                data.ErrorCodeS = "Invalid amount";
                                break;
                            case 411:
                                data.ErrorCodeS = "Transaction exist";
                                break;
                            case 500:
                                data.ErrorCodeS = "Timeout";
                                break;
                            case 305:
                                data.ErrorCodeS = "User not registered";
                                break;
                            case 304:
                                data.ErrorCodeS = "Registration pending";
                                break;
                            case 302:
                                data.ErrorCodeS = "Registered User";
                                break;
                        }
                        break;
                    case "transactionid":
                        data.TransactionId = sValue;
                        break;
                    case "Orderid":
                        data.OrderId = sValue;
                        break;
                    case "Amount":
                        data.Amount = Decimal.Parse(sValue, ci);
                        break;
                    case "ServiceCharge":
                        data.ServiceCharge = Decimal.Parse(sValue, ci);
                        break;
                    case "message":
                        data.Message = sValue;
                        break;
                }
            }
            return data;
        }
    }
}