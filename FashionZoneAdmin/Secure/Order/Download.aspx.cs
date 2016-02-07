using System;
using System.Web;
using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace FashionZone.Admin
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //PDF
            int id;
            if (int.TryParse(Request["IDORDER"], out id))
                OutputDocPdf(ApplicationContext.Current.Orders.GetById(id));
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