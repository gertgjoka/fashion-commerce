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
    public class FZoneTask
    {
        private static readonly string _logFile = ConfigurationManager.AppSettings["LogFile"];
        
        public static void Main(string[] args)
        {
            CampaignTask task = new CampaignTask();
            task.Execute();
        }
    }
}
