using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.BL
{
    internal static class ConfigurationKeys
    {
        public static readonly string ConnectionString = "PrivEntities";
        public static readonly string Unity = "unity";
        public static readonly string OrderNrFormatting = "orderNrFormatting";
        public static readonly string NewsletterTemplate = "NewsletterTemplate";
        public static readonly string OrderTemplate = "OrderTemplate";
        public static readonly string InviteTemplate = "InviteTemplate";
        public static readonly string BonusTemplate = "BonusTemplate";
        public static readonly string UnsubscribeUrl = "UnsubscribeUrl";
        public static readonly string NewsletterHeaderText = "NewsletterHeaderText";
        public static readonly string UrlEncryptionKey = "UrlEncryptionKey";
        public static readonly string XmlPath = "XmlPath";
        public static readonly string ProductImgWidth = "ProductImgWidth";
        public static readonly string ProductImgHeight = "ProductImgHeight";
        public static readonly string ImagesUploadPath = "ImagesUploadPath";
        public static readonly string ImagesVisualizationPath = "ImagesVisualizationPath";
        public static readonly string PasswordHashMethod = "PasswordHashMethod";
        public static readonly string DeploymentURL = "DeploymentURL";
        public static readonly string FutureCampaignDays = "FutureCampaignDays";
        public static readonly string CartExpirationValue = "CartExpirationValue";
        public static readonly string CurrencyDelta = "CurrencyDelta";
        public static readonly string BonusValue = "BonusValue";
        public static readonly string FBAppId = "FBAppId";
        public static readonly string FBAppSecret = "FBAppSecret";

        public static readonly string PaypalEnv = "PaypalEnv";
        public static readonly string PaypalSellerEmail = "PaypalSellerEmail";
        public static readonly string PaypalCurrency = "PaypalCurrency";
        public static readonly string PaypalReturnUrl = "PaypalReturnUrl";
        public static readonly string PaypalNotifyUrl = "PaypalNotifyUrl";
        public static readonly string PaypalCancelUrl = "PaypalCancelUrl";
        public static readonly string PaypalPDTToken = "PaypalPDTToken";

        public static readonly string EasyPayEnv = "EasyPayEnv";
        public static readonly string EasyPayDualAuth = "EasyPayDualAuth";
        public static readonly string EasyPayMerchantUser = "EasyPayMerchantUser";
        public static readonly string EasyPayMerchantRef = "EasyPayMerchantRef";
        public static readonly string EasyPayReturnUrl = "EasyPayReturnUrl";

    }
}
