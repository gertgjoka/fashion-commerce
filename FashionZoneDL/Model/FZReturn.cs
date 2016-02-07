using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class RETURN
    {
        private string _customerName;

        public virtual String CustomerName
        {
            get
            {
                if (String.IsNullOrEmpty(_customerName))
                {
                    return ORDERS == null ? null : ORDERS.CUSTOMER.Name + " " + ORDERS.CUSTOMER.Surname;
                }
                else
                    return _customerName;
            }
            set { _customerName = value; }
        }
    }
}
