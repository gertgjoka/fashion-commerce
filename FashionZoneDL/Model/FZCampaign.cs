using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class CAMPAIGN
    {
        /// <summary>
        /// Read only
        /// </summary>
        public virtual String BrandName
        {
            get
            {
                return BRAND == null ? null : BRAND.ShowName;
            }
            // this will not be saved on the db
            // used for search purposes
            set { }
        }

        public string ImageHomeWithPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ImagesUploadPath"] + ImageHome; }
            set { }
        }

        public string LogoWithPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ImagesUploadPath"] + Logo; }
            set { }
        }

        /// <summary>
        /// Search only
        /// </summary>
        public Nullable<DateTime> SearchStartDate
        {
            get;
            set;
        }

        public Nullable<DateTime> SearchEndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Read only
        /// </summary>
        public virtual String NameWithStartDate
        {
            get { return Name + "_" + StartDate.ToString("dd/MM/yyyy"); }  
        }

    }
}
