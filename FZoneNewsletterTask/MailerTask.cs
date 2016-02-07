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
    public class MailerTask
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

        public MailerTask()
        {
            log("Starting Mailer task.");
        }

        public void Execute()
        {
            // Retrieve users in a paginated way, to avoid loading thousands in memory
            int TotalRecords;
            List<EMAIL> customers = ApplicationContext.Current.Customers.MailList(PageSize, 0, out TotalRecords, "ID", BL.Util.SortDirection.Ascending);
            int numPages = (int)Math.Floor((double)(TotalRecords / PageSize));

            //retrieve campaigns that starts today
            int TotalCampaignsForToday;
            int TotalCampaignsForFuture;
            CAMPAIGN searchCampaign = new CAMPAIGN() { Active = true, Approved = true };
            List<CAMPAIGN> campaigns = ApplicationContext.Current.Campaigns.Search(searchCampaign, 100, 0, out TotalCampaignsForToday, "StartDate", BL.Util.SortDirection.Descending);

            searchCampaign = new CAMPAIGN() { SearchStartDate = DateTime.Today.AddDays(1), SearchEndDate = DateTime.Today.AddDays(Configuration.FutureCampaignDays), Approved = true };
            List<CAMPAIGN> campaignsFuture = ApplicationContext.Current.Campaigns.Search(searchCampaign, 100, 0, out TotalCampaignsForFuture, "StartDate", BL.Util.SortDirection.Ascending);

            CAMPAIGN todaySearchCampaign = new CAMPAIGN() { SearchStartDate = DateTime.Today, SearchEndDate = DateTime.Today.AddHours(23) };
            List<CAMPAIGN> todayCampaigns = ApplicationContext.Current.Campaigns.Search(todaySearchCampaign, 100, 0, out TotalCampaignsForToday, "Name", BL.Util.SortDirection.Ascending);

            if (todayCampaigns == null || todayCampaigns.Count == 0)
            {
                log("There are no campaigns starting today!");
                return;
            }

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
            
            for (int i = 0; i <= numPages; i++)
            {
                if (i != 0)
                {
                    customers = ApplicationContext.Current.Customers.MailList(PageSize, i, out TotalRecords, "ID", BL.Util.SortDirection.Ascending);
                }
                foreach (EMAIL email in customers)
                {
                    SendEmail(email, campaigns, subject.ToString(), campaignsFuture);
                    // TODO decide if this should be done in different threads/tasks
                }
            }
            log("Newsletter sending terminated");
        }

        private void SendEmail(EMAIL Customer, List<CAMPAIGN> Campaigns, string Subject, List<CAMPAIGN> FutureCampaigns)
        {
            try
            {
                FashionZone.BL.Util.Mailer.SendMailer(Customer, Campaigns, Subject, FutureCampaigns);
                log("Mail to " + Customer.Email1 + " sent.");
            }
            catch (Exception ex)
            {
                log("Error sending newsletter to " + Customer.Email1 + " - " + ex.Message);
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
