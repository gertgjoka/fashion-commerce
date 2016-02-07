using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class PRODUCT
    {
        private string campaign;
        public string Campaign
        {
            get
            {
                if (String.IsNullOrEmpty(campaign) && CATEGORY != null && CATEGORY.Count > 0)
                {
                    return CATEGORY.ElementAt(0).CAMPAIGN.Name;
                }
                else
                {
                    return campaign;
                }
            }
            set { campaign = value; }
        }

        public string Delivery
        {
            get
            {
                if (CATEGORY != null && CATEGORY.Count > 0)
                {
                    return CATEGORY.ElementAt(0).CAMPAIGN.StartDate.ToString("dd/MM/yyyy") + " - " + CATEGORY.ElementAt(0).CAMPAIGN.EndDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    return campaign;
                }
            }
            set { campaign = value; }
        }

        public string Thumbnail
        {
            get
            {
                if (PROD_IMAGES != null && PROD_IMAGES.Count > 0 && PROD_IMAGES.Where(i => i.Principal == true).Count() > 0)
                {
                    return System.Configuration.ConfigurationManager.AppSettings["ImagesVisualizationPath"] + PROD_IMAGES.Where(i => i.Principal == true).First().Thumbnail;
                }
                else
                {
                    return null;
                }
            }
        }

        public string ImageForList
        {
            get
            {
                if (PROD_IMAGES != null && PROD_IMAGES.Count > 0 && PROD_IMAGES.Where(i => i.Principal == true).Count() > 0)
                {
                    string image = PROD_IMAGES.Where(i => i.Principal == true).First().Image;
                    if (image.Contains("MED"))
                    {
                        image = image.Replace("MED", "LIST");
                    }
                    else
                    {
                        // for old upladed images
                        image = "LIST" + image;
                    }
                    return System.Configuration.ConfigurationManager.AppSettings["ImagesUploadPath"] + image;
                }
                else
                {
                    return null;
                }
            }
        }

        public int? CampaignID
        {
            get;
            set;
        }

        public int Remaining
        {
            get
            {
                if (PRODUCT_ATTRIBUTE != null && PRODUCT_ATTRIBUTE.Count > 0)
                {
                    return PRODUCT_ATTRIBUTE.Select(p => p.Availability).Sum();
                }
                else
                {
                    return 0;
                }
            }
        }

        public int InitialQuantity
        {
            get
            {
                if (PRODUCT_ATTRIBUTE != null && PRODUCT_ATTRIBUTE.Count > 0)
                {
                    return PRODUCT_ATTRIBUTE.Select(p => p.Quantity).Sum();
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
