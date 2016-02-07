using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace FashionZone.BL
{
    public interface IContextContainer
    {
        ObjectContext Current { get; }
        void Clear();
    }
}
