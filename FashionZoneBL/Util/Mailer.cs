using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using FashionZone.DataLayer.Model;
using System.Configuration;

namespace FashionZone.BL.Util
{
    public static class Mailer
    {
        public static void SendCustomerNewsletter(CUSTOMER Customer, List<CAMPAIGN> Campaigns, String Subject, List<CAMPAIGN> FutureCampaigns)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(Customer.Email));
            mail.Subject = Subject;
            mail.IsBodyHtml = true;
            mail.Body = Transfomer.GenerateNewsletter(Customer, Campaigns, FutureCampaigns);
            mail.From = new MailAddress("communication@fzone.al", "FZone");

            SmtpClient client = new SmtpClient();
            //client.EnableSsl = true;
            client.Timeout = 5000;
            client.Send(mail);
        }

        public static void SendMailer(EMAIL Email, List<CAMPAIGN> Campaigns, String Subject, List<CAMPAIGN> FutureCampaigns)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(Email.Email1));
            mail.Subject = Subject;
            mail.IsBodyHtml = true;
            mail.Body = Transfomer.GenerateNewsletter(null, Campaigns, FutureCampaigns);
            mail.From = new MailAddress("communication@fzone.al", "FZone");
            SmtpClient client = new SmtpClient();
            //client.EnableSsl = true;
            client.Timeout = 5000;
            client.Send(mail);
        }


        public static void SendCustomerOrder(CUSTOMER Customer, List<ORDER_DETAIL> Details, String Subject)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(Customer.Email));
            mail.Subject = Subject;
            mail.IsBodyHtml = true;
            mail.Body = Transfomer.GenerateOrderMail(Customer, Details);
            mail.From = new MailAddress("info@fzone.al", "FZone");

            SmtpClient client = new SmtpClient();
            //client.EnableSsl = true;
            client.Timeout = 5000;
            client.Send(mail);
        }

        public static void SendMailGeneric(string mailTo,  string mailSubject, string mailBody)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(mailTo));
            mail.Subject = mailSubject;
            mail.IsBodyHtml = true;
            mail.Body = mailBody;
            mail.From = new MailAddress("info@fzone.al", "FZone");

            SmtpClient client = new SmtpClient();
            //client.EnableSsl = true;
            client.Timeout = 5000;
            client.Send(mail);
        }

        public static void SendInvite(SessionCustomer Customer, string Url, string mailTo, string mailSubject, string message)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(mailTo));
            mail.Subject = mailSubject;
            mail.IsBodyHtml = true;
            mail.Body = Transfomer.GenerateInviteMail(Customer, Url, message);
            mail.From = new MailAddress("info@fzone.al", "FZone");

            SmtpClient client = new SmtpClient();
            //client.EnableSsl = true;
            client.Timeout = 5000;
            client.Send(mail);
        }

        public static void SendBonus(CUSTOMER Customer, string mailTo, string mailSubject)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(mailTo));
            mail.Subject = mailSubject;
            mail.IsBodyHtml = true;
            mail.Body = Transfomer.GenerateBonusMail(Customer);
            mail.From = new MailAddress("info@fzone.al", "FZone");

            SmtpClient client = new SmtpClient();
            //client.EnableSsl = true;
            client.Timeout = 5000;
            client.Send(mail);
        }

        public static void SendMailGenericMailFrom(string mailTo, string mailFrom, string mailSubject, string mailBody, MailPriority Priority)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(mailTo));
            mail.From = new MailAddress(mailFrom);
            mail.Subject = mailSubject;
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("info@fzone.al", "FZone");
            mail.Body = mailBody;
            mail.Priority = Priority;

            SmtpClient client = new SmtpClient();
            //client.EnableSsl = true;
            client.Timeout = 5000;
            client.Send(mail);
        }
    }
}
