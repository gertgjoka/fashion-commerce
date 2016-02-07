using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class ORDER_DETAIL
    {

        private string campaignName;
        public virtual String CampaignName
        {
            get { return CAMPAIGN != null ? CAMPAIGN.Name : campaignName; }
            set { campaignName = value; }
        }

        private string brandName;
        public virtual String BrandName
        {
            get { return CAMPAIGN != null && CAMPAIGN.BRAND != null ? CAMPAIGN.BrandName : brandName; }
            set { brandName = value; }
        }

        /// <summary>
        /// Read & search only
        /// </summary>
        /// 
        public Decimal? UnitPrice
        {
            get { return PRODUCT_ATTRIBUTE != null ? PRODUCT_ATTRIBUTE.PRODUCT.OurPrice : default(Decimal?); }
            set { } // only for search
        }

        /// <summary>
        /// Read & search only
        /// </summary>
        public Decimal? Amount
        {
            get { return UnitPrice != null ? UnitPrice * Quantity : default(Decimal?); }
            set { } // only for search
        }

        public String ProductNameWithSize
        {
            get
            {
                return ProductName + " - " + ProductAttribute;
            }
        }

        public String ProductNameWithSizeAndCode
        {
            get
            {
                return ProductName + " - " + ProductAttribute + " (" + ProductCode + ")";
            }
        }

        public String ProductAttribute
        {
            get { return PRODUCT_ATTRIBUTE != null ? PRODUCT_ATTRIBUTE.D_ATTRIBUTE_VALUE.Value : String.Empty; }
        }

        public String ProductCode
        {
            get { return PRODUCT_ATTRIBUTE != null ? PRODUCT_ATTRIBUTE.PRODUCT.Code : String.Empty; }
        }

        /// <summary>
        /// Read & search only
        /// </summary>
        private string _productName;
        public String ProductName
        {
            get
            {
                if (String.IsNullOrEmpty(_productName) && PRODUCT_ATTRIBUTE != null)
                    return PRODUCT_ATTRIBUTE.PRODUCT.Name;
                else
                    return _productName;
            }
            set { _productName = value; }
        }

        public String ProductThumbnail
        {
            get
            {
                if (PRODUCT_ATTRIBUTE != null && PRODUCT_ATTRIBUTE.PRODUCT.Thumbnail != null)
                {
                    return PRODUCT_ATTRIBUTE.PRODUCT.Thumbnail;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
