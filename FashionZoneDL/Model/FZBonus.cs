using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class BONUS
    {
        private string _customerName;
        public virtual String CustomerFullName
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

        public virtual string BonusString
        {
            get { return ValueRemainder.Value.ToString("N2"); }
        }
    }
}
