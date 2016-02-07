using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class SHOPPING_CART
    {
        #region Search no perstisting properties

        /// <summary>
        /// Custom ctor for "casting" a order detail in a shopping cart
        /// </summary>
        /// <param name="Detail"></param>
        public SHOPPING_CART(ORDER_DETAIL Detail)
        {
            this.ProdAttrID = Detail.ProdAttrID;
            this.CampaignID = Detail.CampaignID;
            this.CampaignName = Detail.CampaignName;
            this.BrandName = Detail.BrandName;
            this.ProductName = Detail.ProductName;
            this.ProductAttribute = Detail.ProductAttribute;
            this.ProductThumbnail = Detail.ProductThumbnail;
            this.Quantity = Detail.Quantity;
            if (Detail.UnitPrice.HasValue)
            {
                this.UnitPrice = Detail.UnitPrice.Value;
            }
        }

        public SHOPPING_CART()
        {
        }

        /// <summary>
        /// Search Only
        /// </summary>
        public Nullable<DateTime> SearchStartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Read & search only
        /// </summary>
        private string _productName;
        public virtual String ProductName
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


        public virtual String ProductNameWithSize
        {
            get
            {
                return ProductName + " - " + ProductAttribute;
            }
        }

        private string _productThumbnail;
        public virtual String ProductThumbnail
        {
            get
            {
                if (PRODUCT_ATTRIBUTE != null && PRODUCT_ATTRIBUTE.PRODUCT.Thumbnail != null)
                {
                    return PRODUCT_ATTRIBUTE.PRODUCT.Thumbnail;
                }
                else
                {
                    return _productThumbnail;
                }
            }

            set { _productThumbnail = value; }
        }

        /// <summary>
        /// Read & search only
        /// </summary>
        public int? ProductID
        {
            get { return PRODUCT_ATTRIBUTE != null ? PRODUCT_ATTRIBUTE.ProductID : 0; }
            set { }
        }

        #endregion

        /// <summary>
        /// Read & search only
        /// </summary>
        private string _customerName;
        public virtual String CustomerName
        {
            get
            {
                if (String.IsNullOrEmpty(_customerName) && CUSTOMER != null)
                    return CUSTOMER.Name + " " + CUSTOMER.Surname;
                else
                    return _customerName; // for search
            }
            set { _customerName = value; } // only for search
        }


        private string campaignName;
        public virtual String CampaignName
        {
            get { return CAMPAIGN != null ? CAMPAIGN.Name : campaignName; }
            set { campaignName = value; }
        }

        /// <summary>
        /// Read only
        /// </summary>

        private string brandName;
        public virtual String BrandName
        {
            get { return CAMPAIGN != null && CAMPAIGN.BRAND != null ? CAMPAIGN.BrandName : brandName; }
            set { brandName = value; }
        }

        /// <summary>
        /// Read only
        /// </summary>
        public virtual String CampaignDelivery
        {
            get
            {
                return CAMPAIGN != null ?
                    CAMPAIGN.StartDate.ToString("dd/MM/yyyy") + " - " + CAMPAIGN.EndDate.ToString("dd/MM/yyyy")
                    : String.Empty;
            }
            set { }
        }

        /// <summary>
        /// Read & search only
        /// </summary>
        /// 
        private decimal? _unitPrice;

        public virtual Decimal? UnitPrice
        {
            get { return PRODUCT_ATTRIBUTE != null ? PRODUCT_ATTRIBUTE.PRODUCT.OurPrice : _unitPrice; }
            set { _unitPrice = value; }
        }

        /// <summary>
        /// Read & search only
        /// </summary>
        public virtual Decimal? Amount
        {
            get { return UnitPrice != null ? UnitPrice * Quantity : default(Decimal?); }
            set { } // only for search
        }

        private string _productAttribute;
        public virtual String ProductAttribute
        {
            get { return PRODUCT_ATTRIBUTE != null ? PRODUCT_ATTRIBUTE.D_ATTRIBUTE_VALUE.Value : _productAttribute; }
            set { _productAttribute = value; }
        }

        private byte[] _productAttributeVersion;
        public byte[] ProductAttributeVersion
        {
            get
            {
                if (_productAttributeVersion == null)
                {
                    _productAttributeVersion = PRODUCT_ATTRIBUTE.Version;
                }
                return _productAttributeVersion;
            }
            set { _productAttributeVersion = value; }
        }
    }

    [Serializable]
    public class SessionCart
    {
        public SessionCart(SHOPPING_CART cart)
        {
            Id = cart.ID;
            ProductAttributeId = cart.ProdAttrID;
            ProductAttributeVersion = cart.ProductAttributeVersion;
            Quantity = cart.Quantity;
            DateAdded = cart.DateAdded;
            FullName = cart.BrandName + " - " + cart.ProductNameWithSize;
            Price = cart.UnitPrice.Value;
        }

        public string Id { get; set; }
        public int ProductAttributeId { get; set; }
        public int Quantity { get; set; }
        public string FullName { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public byte[] ProductAttributeVersion { get; set; }

    }
}
