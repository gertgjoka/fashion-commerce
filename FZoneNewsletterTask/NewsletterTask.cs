using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using Configuration = FashionZone.BL.Configuration;

namespace FashionZone.NewsletterTask
{
    public class NewsletterTask
    {
        internal static int PageSize
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 100;
                }
            }
        }

        private static readonly string _logFile = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + ConfigurationManager.AppSettings["LogFile"]);

        public NewsletterTask()
        {
            log("Starting campaign task.");
        }

        public void Execute()
        {
            // Retrieve users in a paginated way, to avoid loading thousands in memory
            int TotalRecords;
            CUSTOMER searchCustomer = new CUSTOMER();
            searchCustomer.Newsletter = true;
            List<CUSTOMER> customers = ApplicationContext.Current.Customers.Search(searchCustomer, PageSize, 0, out TotalRecords, "ID", BL.Util.SortDirection.Ascending);
            int numPages = (int)Math.Floor((double)(TotalRecords / PageSize));

            //retrieve campaigns that starts today
            int TotalCampaignsForToday;
            int TotalCampaignsForFuture;
            CAMPAIGN searchCampaign = new CAMPAIGN(){SearchStartDate = DateTime.Today, SearchEndDate = DateTime.Today.AddHours(24)};
            List<CAMPAIGN> campaigns = ApplicationContext.Current.Campaigns.Search(searchCampaign, 100, 0, out TotalCampaignsForToday, "Name", BL.Util.SortDirection.Ascending);

            searchCampaign = new CAMPAIGN() { SearchStartDate = DateTime.Today.AddDays(1), SearchEndDate = DateTime.Today.AddDays(Configuration.FutureCampaignDays) };
            List<CAMPAIGN> campaignsFuture = ApplicationContext.Current.Campaigns.Search(searchCampaign, 100, 0, out TotalCampaignsForFuture, "Name", BL.Util.SortDirection.Ascending);

            StringBuilder subject = new StringBuilder();
            subject.Append("Sot ne FZone: ");
            for (int i = 0; i < campaigns.Count; i++)
            {
                if (i != 0 && campaigns.Count > 1 && i == (campaigns.Count - 1))
                {
                    subject.Append(" dhe ");
                }

                if (i != 0 && campaigns.Count != 1 && i != (campaigns.Count - 1))
                {
                    subject.Append(", ");
                }

                subject.Append(campaigns.ElementAt(i).BrandName);
            }
            subject.Append(" sot ne FZone");

            if (campaigns == null || campaigns.Count == 0)
            {
                log("There are no campaigns starting today!");
                return;
            }
            
            for (int i = 0; i <= numPages; i++)
            {
                if (i != 0)
                {
                    customers = ApplicationContext.Current.Customers.Search(searchCustomer, PageSize, i, out TotalRecords, "ID", BL.Util.SortDirection.Ascending);
                }
                foreach (CUSTOMER customer in customers)
                {
                    SendEmail(customer, campaigns, subject.ToString(), campaignsFuture);
                    // TODO decide if this should be done in different threads/tasks
                }
            }
            log("Newsletter sending terminated");
        }

        private void SendEmail(CUSTOMER Customer, List<CAMPAIGN> Campaigns, string Subject, List<CAMPAIGN> FutureCampaigns)
        {
            try
            {
                FashionZone.BL.Util.Mailer.SendCustomerNewsletter(Customer, Campaigns, Subject, FutureCampaigns);
                log("Mail to " + Customer.Email + " sent.");
            }
            catch (Exception ex)
            {
                log("Error sending newsletter to " + Customer.Email + " - " + ex.Message);
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
