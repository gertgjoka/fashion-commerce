using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL
{
    public class ThreadContextContainer : IContextContainer
    {
        public ThreadContextContainer()
        {
        }

        [ThreadStatic]
        private static ObjectContext _currentContext;

        public ObjectContext Current
        {
            get { return _currentContext ?? (_currentContext = new privEntities()); }
        }

        public void Clear()
        {
            _currentContext = new privEntities();
        }
    }
}
