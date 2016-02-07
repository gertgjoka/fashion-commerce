using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FashionZone.Admin.Utils
{
    public static class Util
    {
        private static readonly string _logFile = Path.Combine(HttpContext.Current.Server.MapPath("/") + "/Log/");

        internal static void Log(Exception ex, String Message, String Trace, String Where)
        {
            using (StreamWriter w = File.AppendText(_logFile + DateTime.Today.ToString("dd-MM-yyyy") + ".log"))
            {
                w.WriteLine(DateTime.Now + " - " + Where);
                w.WriteLine(Message);
                w.WriteLine(Trace);
                if (ex != null && ex.InnerException != null)
                {
                    w.WriteLine("INNER - " + ex.InnerException.Message);
                }
                w.Flush();
                // Close the writer and underlying file.
                w.Close();
            }
        }
    }
}