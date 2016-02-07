using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using FashionZone.DataLayer.Model;
using Microsoft.Practices.Unity;

namespace FashionZone.BL.DAO
{
    public class BaseDAO
    {
        protected privEntities Context { get; set; }
        public BaseDAO()
        {

        }

        public BaseDAO(IContextContainer container)
        {
            Context = (privEntities)container.Current;
        }

        protected void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
