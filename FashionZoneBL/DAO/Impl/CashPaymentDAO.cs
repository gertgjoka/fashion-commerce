using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO.Impl
{
    public class CashPaymentDAO : BaseDAO, ICashPaymentDAO
    {
         public CashPaymentDAO(IContextContainer container)
            : base(container)
        {
        }

        public List<CASH_PAYMENT> Search(CASH_PAYMENT Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public List<CASH_PAYMENT> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public CASH_PAYMENT GetById(int Id)
        {
            return Context.CASH_PAYMENT.Where(c => c.ID == Id).FirstOrDefault();
        }

        public void Insert(CASH_PAYMENT Entity)
        {
            Context.CASH_PAYMENT.AddObject(Entity);
        }

        public void Update(CASH_PAYMENT Entity)
        {
            Context.CASH_PAYMENT.Attach(Entity);
            Context.ObjectStateManager.ChangeObjectState(Entity, System.Data.EntityState.Modified);
        }

        public void Delete(CASH_PAYMENT Entity)
        {
            Context.CASH_PAYMENT.Attach(Entity);
            Context.DeleteObject(Entity);
        }

        public void DeleteById(int Id)
        {
            CASH_PAYMENT cPayment = new CASH_PAYMENT() { ID = Id };
            Delete(cPayment);
        }
    }
}
