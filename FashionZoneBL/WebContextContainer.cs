using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Web;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL
{
    public class WebContextContainer : IContextContainer
    {
        public WebContextContainer()
        {
        }

        public ObjectContext Current
        {
            get { return (ObjectContext)HttpContext.Current.Items[Configuration.ContextKey]; }
        }

        public void Clear()
        {
            HttpContext.Current.Items[Configuration.ContextKey] = new privEntities();
        }
    }
}
