using System;
using System.Web;
using FashionZone.BL.Util;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using FashionZone.FE.Utils;

namespace FashionZone.FE.Secure.Personal
{
    public partial class pdfPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["queryvalues"] != null && !String.IsNullOrWhiteSpace(RouteData.Values["queryvalues"].ToString()))
            {
                try
                {
                    string productId = Encryption.Decrypt(RouteData.Values["queryvalues"].ToString());
                    int id;
                    if (int.TryParse(productId, out id))
                    {
                        OutputDocPdf(ApplicationContext.Current.Orders.GetById(id));
                    }
                }
                catch (Exception ex)
                {
                    BasePage.Log(ex, ex.Message, ex.StackTrace, "Product.AddToCart");
                }
            }
        }

        private void OutputDocPdf(ORDERS Order)
        {
            using (var memoryF = BL.PDF.PdfGenerator.DocPDF(Order))
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.OutputStream.Write(memoryF.GetBuffer(), 0, memoryF.GetBuffer().Length);
                Response.OutputStream.Close();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
    }
}