using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FashionZone.Admin
{
    public partial class ErrorManager : System.Web.UI.Page
    {
        private static string _msgError = string.Empty;
        private static string _stkError = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Simo nel caso in qui l'errovi vine passato dal web.comfig quindi nn abbimo l'Exception!
            if (Request["ERR"] == "111")
            {
                Exception LastException = Context.Server.GetLastError();
                if (LastException != null) SerError(LastException);
            }
            if (!Page.IsPostBack)
            {
                string _stackErr = "<p>Error stack:" + _stkError + "</p>";
                lbl_Error.Text = "<p>Error message:" + _msgError + "</p>" + _stackErr;
            }
        }

        public static void SerError(Exception err)
        {
            if (err != null)
            {
                _msgError = (!string.IsNullOrEmpty(err.Message)) ? "<ul><li>" + err.Message + "</li><li>" + (err.InnerException != null ? err.InnerException.Message : "") + "</li></ul>" : "<ul><li>" + err.InnerException.Message + "</li></ul>";
                _stkError = (!string.IsNullOrEmpty(err.StackTrace)) ? err.StackTrace : err.InnerException.StackTrace;
            }
        }
    }
}