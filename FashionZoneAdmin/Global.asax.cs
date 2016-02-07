using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace FashionZone.Admin
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.WebForms;
            //ScriptResourceDefinition jQuery = new ScriptResourceDefinition();

            //jQuery.Path = "/js/jquery-1.8.1.min.js";

            //jQuery.DebugPath = "/js/jquery-1.8.1.min.js";

            //jQuery.CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.8.1.min.js";

            //jQuery.CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.8.1.js";

            //ScriptManager.ScriptResourceMapping.AddDefinition("jquery", jQuery); 
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ////La gestione degli Errori da Global.asax serve per errori di connessione
            ////Sulla pagina Master.Page sono gestiti gli errori specifici delle pagg.
            Exception LastException = Context.Server.GetLastError();

            if (LastException != null)
            {
                FashionZone.Admin.ErrorManager.SerError(LastException);
                Response.Redirect("/ErrorManager.aspx");
            }
            Context.Server.ClearError();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
