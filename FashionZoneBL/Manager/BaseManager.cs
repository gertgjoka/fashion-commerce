using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using System.Data.Objects;

namespace FashionZone.BL.Manager
{
    public class BaseManager
    {
        protected privEntities Context { get; set; }
        public BaseManager()
        {

        }

        public BaseManager(IContextContainer container)
        {
            Context = (privEntities)container.Current;
        }

        protected void SaveChanges(SaveOptions opt)
        {
            Context.SaveChanges(opt);
        }
    }
}
