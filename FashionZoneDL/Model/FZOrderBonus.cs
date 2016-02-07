using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    public partial class ORDER_BONUS
    {
        public DateTime? Date
        {
            get { return ORDERS == null ? default(DateTime) : ORDERS.DateCreated; }
        }
    }
}
