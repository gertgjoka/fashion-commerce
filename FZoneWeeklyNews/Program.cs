using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionZone.FZoneWeeklyNews
{
    class Program
    {
        static void Main(string[] args)
        {
            MailerTask task = new MailerTask();
            task.Execute();
        }
    }
}
