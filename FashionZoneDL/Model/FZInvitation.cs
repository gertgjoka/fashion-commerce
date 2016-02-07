using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class INVITATION
    {
        public virtual String CustomerFullName
        {
            get
            {
                return (CUSTOMER != null ? string.Format("{0} {1}", CUSTOMER.Name, CUSTOMER.Surname) : "");
            }
        }
    }
}
