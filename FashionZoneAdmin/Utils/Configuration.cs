using System;
using System.Configuration;

namespace FashionZone.Admin.Utils
{
    public class Configuration
    {
        internal static string PasswordHashMethod
        {
            get { return ConfigurationManager.AppSettings["PasswordHashMethod"]; }
        }

        public static string ImagesUploadPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ImagesUploadPath"]; }
        }

        public static string DocsPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["DocsPath"]; }
        }

        public static string ImagesVisualizationPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ImagesVisualizationPath"]; }
        }

        public static int MaxReturnDay
        {
            get
            {
                int iOut;
                if (Int32.TryParse(ConfigurationManager.AppSettings["MaxReturnDay"].ToString(), out iOut))
                    return iOut;
                else
                    return 10;
            }
        }

        internal static string UploadSeparator
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["UploadSeparator"]; }
        }

        internal static int MaxPasswordAttempts
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["MaxPasswordAttempts"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 5;
                }
            }
        }

        internal static int State
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["STATE"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 5;
                }
            }
        }

        internal static int MaxAddresses
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["MaxAddresses"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 5;
                }
            }
        }

        internal static int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["MinRequiredNonAlphanumericCharacters"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 5;
                }
            }
        }

        internal static int MinRequiredPasswordLength
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["MinRequiredPasswordLength"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 5;
                }
            }
        }

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
                    return 10;
                }
            }
        }

        internal static string PasswordStrengthRegularExpression
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["PasswordStrengthRegularExpression"]; }
        }
    }
}