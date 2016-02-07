using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Objects;

namespace FashionZone.BL
{
    public class ContextContainer : IContextContainer
    {
        private IContextContainer _container = null;

        public ContextContainer()
        {
            if (HttpContext.Current == null)
            {
                _container = new ThreadContextContainer();
            }
            else
            {
                _container = new WebContextContainer();
            }
        }

        public ObjectContext Current
        {
            get { return _container.Current; }
        }

        public void Clear()
        {
            _container.Clear();
        }
    }
}
