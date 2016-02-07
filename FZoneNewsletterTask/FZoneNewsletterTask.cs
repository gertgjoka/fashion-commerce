using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace FashionZone.NewsletterTask
{
    public class FZoneNewsletterTask
    {
        public static void Main(string[] args)
        {
            //NewsletterTask newsletter = new NewsletterTask();
            //newsletter.Execute();

            MailerTask task = new MailerTask();
            task.Execute();
        }
    }
}
