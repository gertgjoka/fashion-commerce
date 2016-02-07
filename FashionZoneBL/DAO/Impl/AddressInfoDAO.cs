using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO.Impl
{
    public class AddressInfoDAO : BaseDAO, IAddressInfoDAO
    {
          public AddressInfoDAO(IContextContainer container)
            : base(container)
        {
        }

        public List<ADDRESSINFO> Search(ADDRESSINFO Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public List<ADDRESSINFO> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public ADDRESSINFO GetById(int id)
        {
            var addr = Context.ADDRESSINFO.Include("D_CITY").Where(a => a.ID == id).FirstOrDefault();
            return addr;
        }

        public void Insert(ADDRESSINFO Entity)
        {
            Context.ADDRESSINFO.AddObject(Entity);
        }

        public void Update(ADDRESSINFO Entity)
        {
            Context.ADDRESSINFO.Attach(Entity);
            Context.ObjectStateManager.ChangeObjectState(Entity, System.Data.EntityState.Modified);
        }

        public void Delete(ADDRESSINFO Entity)
        {
            Context.ADDRESSINFO.Attach(Entity);
            Context.ADDRESSINFO.DeleteObject(Entity);
        }

        public void DeleteById(int Id)
        {
            ADDRESSINFO addr = new ADDRESSINFO() { ID = Id };
        }
    }
}
