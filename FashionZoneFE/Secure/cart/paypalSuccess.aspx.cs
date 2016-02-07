using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using FashionZone.FE.Utils;

namespace FashionZone.FE.Secure.cart
{
    public partial class Success : BasePage
    {
        string authToken, txToken, query;
        string strResponse;

        protected void Page_Load(object sender, EventArgs e)
        {
            ((SiteMaster)Master).SetImgBackground("", "ContentIII");
            if (!Page.IsPostBack)
            {
                processPayPalData();
            }
            base.Page_Load(sender, e);
        }

        protected void processPayPalData()
        {
            // refreshing cart (should be empty as IPN should have fired)
            RefreshCart();
            authToken = Configuration.PaypalPDTToken;

            //read in txn token from querystring
            txToken = Request.QueryString.Get("tx");


            query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);

            // Create the request back
            string url = Configuration.PaypalEnv;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            // Write the request back IPN strings
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();

            // Do the request to PayPal and get the response
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();

            // sanity check
            lblPDT.Text = strResponse;

            // If response was SUCCESS, parse response string and output details
            if (strResponse.StartsWith("SUCCESS"))
            {
                PDTData data = PDTData.Parse(strResponse, false);
                lblResult.Text = "<b>" + Resources.Lang.PayPalThankyouLabel + "</b> <br/><br/>" +
                    Resources.Lang.SaveTransactionID + "<b>" + data.TransactionId + "</b>";
                
            }
            else
            {
                lblResult.Text = Resources.Lang.ErrorInPayPalLabel;
            }
        }
    }
}