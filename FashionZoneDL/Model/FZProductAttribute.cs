using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class PRODUCT_ATTRIBUTE
    {
        private string _value;
        public virtual String Value
        {
            get
            {
                return D_ATTRIBUTE_VALUE == null ? _value : D_ATTRIBUTE_VALUE.Value;
            }
            set
            {
                _value = value;
            }
        }

        private int _order;
        public virtual int ValueOrder
        {
            get
            {
                return D_ATTRIBUTE_VALUE == null ? _order : D_ATTRIBUTE_VALUE.ShowOrder;
            }
            set
            {
                _order = value;
            }
        }
    }
}
