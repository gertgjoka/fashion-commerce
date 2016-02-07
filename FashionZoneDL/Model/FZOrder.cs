using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class ORDERS
    {

        private string _customerName;
        public virtual String CustomerName
        {
            get
            {
                if (String.IsNullOrEmpty(_customerName))
                {
                    return CUSTOMER == null ? null : CUSTOMER.Name + " " + CUSTOMER.Surname;
                }
                else
                    return _customerName;
            }
            set { _customerName = value; }
        }

        public virtual String CampaignName
        {
            get
            {
                if (ORDER_DETAIL != null && ORDER_DETAIL.Count > 0)
                    return ORDER_DETAIL.ElementAt(0).CampaignName;
                else
                    return string.Empty;
            }
            set { }
        }

        public virtual String StatusName
        {
            get
            {
                return D_ORDER_STATUS == null ? null : D_ORDER_STATUS.Name;
            }
            set { }
        }

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
    }
}
