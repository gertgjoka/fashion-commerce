using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.Web.UI;

namespace FashionZone.FE
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Codice eseguito all'avvio dell'applicazione
            RegisterRoutes(RouteTable.Routes);

            ScriptResourceDefinition jQuery = new ScriptResourceDefinition();

            jQuery.Path = "/js/jquery-1.8.1.min.js";

            //jQuery.DebugPath = "/js/jquery-1.8.1.min.js";

            jQuery.CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.8.1.min.js";

            //jQuery.CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.8.1.js";

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", jQuery); 
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Codice eseguito all'arresto dell'applicazione

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Codice eseguito in caso di errore non gestito

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Codice eseguito all'avvio di una nuova sessione

        }

        void Session_End(object sender, EventArgs e)
        {
            // Codice eseguito al termine di una sessione. 
            // Nota: l'evento Session_End viene generato solo quando la modalità sessionstate
            // è impostata su InProc nel file Web.config. Se la modalità è impostata su StateServer 
            // o SQLServer, l'evento non viene generato.

        }

        public static void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.MapPageRoute("Default", "", "~/default.aspx");
            routeCollection.MapPageRoute("CampaignRoute", "campaign/{camp}/", "~/secure/campaign.aspx");
            routeCollection.MapPageRoute("CampaignCatRoute", "campaign/{camp}/cat/{cat}/{*queryvalues}", "~/secure/campaign.aspx");
            routeCollection.MapPageRoute("ProductRoute", "product/{prod}/{*queryvalues}", "~/secure/product.aspx");
            routeCollection.MapPageRoute("Login", "login/{*queryvalues}", "~/login.aspx");

            routeCollection.MapPageRoute("Register", "register/", "~/register.aspx");            
            routeCollection.MapPageRoute("RegisterMial", "register/{idcustinvited}/{*invid}", "~/register.aspx");
            routeCollection.MapPageRoute("Home", "home/{*queryvalues}", "~/secure/home.aspx"); 
            routeCollection.MapPageRoute("Cart", "cart/mycart/{*queryvalues}", "~/secure/cart/mycart.aspx");
            routeCollection.MapPageRoute("Checkout", "cart/checkout/{*queryvalues}", "~/secure/cart/checkout.aspx");
            routeCollection.MapPageRoute("PayPalSuccess", "cart/paypal/{*queryvalues}", "~/secure/cart/paypalSuccess.aspx");
            routeCollection.MapPageRoute("PayPalIPN", "cart/paypalIPN/{*queryvalues}", "~/IPNHandler.aspx");
            routeCollection.MapPageRoute("EasyPayReturn", "cart/easypay/{*queryvalues}", "~/secure/cart/easyPayReturn.aspx");

            routeCollection.MapPageRoute("PdfDownload", "personal/pdfDownload/{*queryvalues}", "~/secure/Personal/pdfPrint.aspx");
            routeCollection.MapPageRoute("Invite", "personal/invite/{*queryvalues}", "~/secure/Personal/inviteFriend.aspx");

            routeCollection.MapPageRoute("Personal", "personal/info/{*queryvalues}", "~/secure/Personal/personalInfo.aspx");
            routeCollection.MapPageRoute("MyBonus", "personal/bonus/{*queryvalues}", "~/secure/Personal/bonus.aspx");
            routeCollection.MapPageRoute("MyFriends", "personal/friends/{*queryvalues}", "~/secure/Personal/friends.aspx");
            routeCollection.MapPageRoute("MyNotification", "personal/notification/{*queryvalues}", "~/secure/Personal/notification.aspx");
            routeCollection.MapPageRoute("MyOrder", "personal/order/{*queryvalues}", "~/secure/Personal/order.aspx");
            routeCollection.MapPageRoute("OrderDetail", "personal/orderDet/{ord}/", "~/secure/Personal/orderDetail.aspx");

            routeCollection.MapPageRoute("AboutUs", "private/aboutus/{*queryvalues}", "~/static/aboutUs.aspx");
            routeCollection.MapPageRoute("Delivery", "private/delivery/{*queryvalues}", "~/static/delivery.aspx");
            routeCollection.MapPageRoute("Payment", "private/payment/{*queryvalues}", "~/static/payment.aspx");
            routeCollection.MapPageRoute("Privacy", "private/privacy/{*queryvalues}", "~/static/privacy.aspx");
            routeCollection.MapPageRoute("Returns", "private/returns/{*queryvalues}", "~/static/returns.aspx");
            routeCollection.MapPageRoute("Security", "private/security/{*queryvalues}", "~/static/security.aspx");
            routeCollection.MapPageRoute("TermsOfUse", "private/terms/{*queryvalues}", "~/static/termsOfUse.aspx");
            routeCollection.MapPageRoute("ContactUs", "private/contact/{*queryvalues}", "~/static/contactUs.aspx");

            routeCollection.MapPageRoute("AboutUsP", "public/aboutus/{*queryvalues}", "~/public/aboutUs.aspx");
            routeCollection.MapPageRoute("DeliveryP", "public/delivery/{*queryvalues}", "~/public/delivery.aspx");
            routeCollection.MapPageRoute("PaymentP", "public/payment/{*queryvalues}", "~/public/payment.aspx");
            routeCollection.MapPageRoute("PrivacyP", "public/privacy/{*queryvalues}", "~/public/privacy.aspx");
            routeCollection.MapPageRoute("ReturnsP", "public/returns/{*queryvalues}", "~/public/returns.aspx");
            routeCollection.MapPageRoute("SecurityP", "public/security/{*queryvalues}", "~/public/security.aspx");
            routeCollection.MapPageRoute("TermsOfUseP", "public/terms/{*queryvalues}", "~/public/termsOfUse.aspx");
            routeCollection.MapPageRoute("ContactUsP", "public/contact/{*queryvalues}", "~/public/contactUs.aspx");

            routeCollection.MapPageRoute("ResetPassword", "resetpassword/", "~/resetPassword.aspx");
            routeCollection.MapPageRoute("Password", "password/{uid}/{pid}/{*queryvalues}", "~/password.aspx");
        }
    }
}
