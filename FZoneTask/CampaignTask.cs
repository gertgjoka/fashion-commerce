using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using FashionZone.DataLayer.Model;
using FashionZone.BL;

namespace FashionZone.Task
{
    public class CampaignTask
    {
        private static readonly string _logFile = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + ConfigurationManager.AppSettings["LogFile"]);

        public CampaignTask()
        {
            log("Starting campaign task.");
        }

        public void Execute()
        {
            try
            {
               

                // Activating new campaigns
                int? campaignsActivated = ApplicationContext.Current.Campaigns.ActivateActualCampaigns();
                if (campaignsActivated.HasValue && campaignsActivated.Value != 0)
                {
                    log("Activated " + campaignsActivated.Value + " campaigns.");
                }
                else
                {
                    log("No deactivated campaigns starting today were found.");
                }

                // Deactivating past campaigns
                int? campaignsDeactivated = ApplicationContext.Current.Campaigns.DeactivatePastCampaigns();
                if (campaignsDeactivated.HasValue && campaignsDeactivated.Value != 0)
                {
                    log("Deactivated " + campaignsDeactivated.Value + " past campaigns.");
                }
                else
                {
                    log("No active campaigns ending today were found.");
                }

                // updating currency
                CurrencyConverter.CurrencyConvertorSoapClient currencyClient = new CurrencyConverter.CurrencyConvertorSoapClient("CurrencyConvertorSoap12");
                double rate = currencyClient.ConversionRate(CurrencyConverter.Currency.EUR, CurrencyConverter.Currency.ALL);
                CURRENCY curr = new CURRENCY(){ Date = DateTime.Today.Date, CurrencyRate = (decimal)rate };
                CURRENCY saved = ApplicationContext.Current.Payments.GetLastConversionRate();
                if (saved.Date != DateTime.Today.Date)
                {
                    ApplicationContext.Current.Payments.InsertConversionRate(curr);
                }
                else
                {
                    saved.CurrencyRate = curr.CurrencyRate;
                    ApplicationContext.Current.Payments.UpdateConversionRate(saved);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    log(ex.Message);
                }
                else
                {
                    log(ex.Message + "\n Inner: " + ex.InnerException.Message);
                }
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
