using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO.Impl
{
    public class CurrencyDAO : BaseDAO, ICurrencyDAO
    {
        public CurrencyDAO(IContextContainer container)
            : base(container)
        {
        }

        public List<CURRENCY> Search(CURRENCY Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public List<CURRENCY> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public CURRENCY GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(CURRENCY Entity)
        {
            Context.CURRENCY.AddObject(Entity);
        }

        public void Update(CURRENCY Entity)
        {
            //Context.CURRENCY.Attach(Entity);
            //Context.ObjectStateManager.ChangeObjectState(Entity, System.Data.EntityState.Modified);
        }

        public void Delete(CURRENCY Entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

        public CURRENCY GetLastRate()
        {
            CURRENCY last = Context.CURRENCY.Where(c => c.Date == DateTime.Today.Date).FirstOrDefault();
            if (last == null)
            {
                last = Context.CURRENCY.OrderByDescending(c => c.Date).ToList().FirstOrDefault();
            }
            return last;
        }
    }
}
