using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace ShoppingCartJob
{
    public class ShoppingCartTask
    {
        private static readonly string _logFile = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + ConfigurationManager.AppSettings["LogFile"]);

        public ShoppingCartTask()
        {
            //log("Starting shopping cart task.");
        }

        public void Execute()
        {
            try
            {
                List<string> cartIDs = ApplicationContext.Current.Carts.GetExpiredCarts();
                StringBuilder logMsg = new StringBuilder();
                if (cartIDs != null && cartIDs.Count > 0)
                {
                    foreach (string cart in cartIDs)
                    {
                        logMsg.Append("CartID: " + cart);
                        int? cartItems = ApplicationContext.Current.Carts.GetShoppingCartTotalItems(cart);
                        decimal? cartAmount = ApplicationContext.Current.Carts.GetShoppingCartTotalAmount(cart);

                        SHOPPING_CART sc = ApplicationContext.Current.Carts.GetShoppingCartItems(cart).FirstOrDefault();
                        if (sc != null)
                        {
                            logMsg.Append(", Customer: " + sc.CustomerName);
                            logMsg.Append(", Campaign: " + sc.CampaignName);
                        }
                        // logging
                        if (cartItems.HasValue && cartAmount.HasValue)
                        {
                            logMsg.Append(", Items: " + cartItems.Value + ", Amount: " + cartAmount.Value);
                        }
                        log(logMsg.ToString());
                         
                        // returning in stock the items that were in the cart
                        ApplicationContext.Current.Carts.DeleteShoppingCart(cart);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "/nInner: " + ex.InnerException.Message;
                }
                log(message);
            }
        }

        public static void log(String log)
        {
            log = DateTime.Now + " - " + log;
            Console.WriteLine(log);
            using (StreamWriter w = File.AppendText(_logFile))
            {
                w.WriteLine(log);
                w.Flush();
                // Close the writer and underlying file.
                w.Close();
            }
        }
    }
}
