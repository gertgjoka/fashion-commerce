using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FashionZone.BL
{
    public class Configuration
    {
        internal static ConnectionStringSettingsCollection ConnectionStrings;

        internal static string ContextKey = "EFObjectContext";
        internal static object UnitySection
        {
            get { return ConfigurationManager.GetSection(ConfigurationKeys.Unity); }
        }

        public static string XmlPath
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.XmlPath]; }
        }

        public static string ConnectionString
        {
            get { return Configuration.ConnectionStrings[ConfigurationKeys.ConnectionString].ConnectionString; }
        }

        public static string NewsletterTemplate
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.NewsletterTemplate]; }
        }

        public static string OrderTemplate
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.OrderTemplate]; }
        }

        public static string InviteTemplate
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.InviteTemplate]; }
        }

        public static string BonusTemplate
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.BonusTemplate]; }
        }

        public static string UnsubscribeUrl
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.UnsubscribeUrl]; }
        }

        public static string UrlEncryptionKey
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.UrlEncryptionKey]; }
        }

        public static string NewsletterHeaderText
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.NewsletterHeaderText]; }
        }

        public static string DeploymentURL
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.DeploymentURL]; }
        }

        public static int FutureCampaignDays
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["FutureCampaignDays"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 30;
                }
            }
        }

        public static int CartExpirationValue
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["CartExpirationValue"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 30;
                }
            }
        }

        public static int CurrencyDelta
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["CurrencyDelta"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 2;
                }
            }
        }

        public static int BonusValue
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["BonusValue"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 5;
                }
            }
        }

        public static string ImagesUploadPath
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.ImagesUploadPath]; }
        }

        public static string FBAppId
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.FBAppId]; }
        }
        public static string FBAppSecret
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.FBAppSecret]; }
        }

        public static string ImagesVisualizationPath
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.ImagesVisualizationPath]; }
        }

        public static string PaypalEnv
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.PaypalEnv]; }
        }

        public static string PaypalSellerEmail
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.PaypalSellerEmail]; }
        }

        public static string PaypalCurrency
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.PaypalCurrency]; }
        }

        public static string PaypalReturnUrl
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.PaypalReturnUrl]; }
        }

        public static string PaypalNotifyUrl
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.PaypalNotifyUrl]; }
        }

        public static string PaypalCancelUrl
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.PaypalCancelUrl]; }
        }

        public static string PaypalPDTToken
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.PaypalPDTToken]; }
        }

        public static string EasyPayEnv
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.EasyPayEnv]; }
        }

        public static string EasyPayDualAuth
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.EasyPayDualAuth]; }
        }

        public static string EasyPayMerchantUser
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.EasyPayMerchantUser]; }
        }

        public static string EasyPayMerchantRef
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.EasyPayMerchantRef]; }
        }

        public static string EasyPayReturnUrl
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.EasyPayReturnUrl]; }
        }

        public static string PasswordHashMethod
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.PasswordHashMethod]; }
        }

        public static string OrderNrFormatting
        {
            get { return ConfigurationManager.AppSettings[ConfigurationKeys.OrderNrFormatting]; }
        }

        public static int MaxOrderQuantityPerProduct
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["MaxOrderQuantityPerProduct"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 5;
                }
            }
        }

        public static int ProductImgWidthList
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["ProductImgWidthList"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 250;
                }
            }
        }

        public static int ProductImgHeightList
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["ProductImgHeightList"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 5;
                }
            }
        }

        public static int ProductImgWidthSmall
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["ProductImgWidthSmall"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 250;
                }
            }
        }

        public static int ProductImgHeightSmall
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["ProductImgHeightSmall"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 5;
                }
            }
        }

        public static int ProductImgWidthMedium
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["ProductImgWidthMedium"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 250;
                }
            }
        }

        public static int ProductImgHeightMedium
        {
            get
            {
                int iOut = 0;
                if (Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["ProductImgHeightMedium"].ToString(), out iOut))
                {
                    return iOut;
                }
                else
                {
                    return 5;
                }
            }
        }
        public static int State
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
    }
}
