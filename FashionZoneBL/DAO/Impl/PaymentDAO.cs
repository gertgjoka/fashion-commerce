using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO.Impl
{
    public class PaymentDAO : BaseDAO, IPaymentDAO
    {
        public PaymentDAO(IContextContainer container)
            : base(container)
        {
        }
        public List<PAYMENT> Search(PAYMENT Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public List<PAYMENT> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public PAYMENT GetById(int Id)
        {
            return Context.PAYMENT.Where(p => p.ID == Id).FirstOrDefault();
        }

        public void Insert(PAYMENT Entity)
        {
            Context.PAYMENT.AddObject(Entity);
        }

        public void Update(PAYMENT Entity)
        {
            Context.PAYMENT.Attach(Entity);
            Context.ObjectStateManager.ChangeObjectState(Entity, System.Data.EntityState.Modified);
        }

        public void Delete(PAYMENT Entity)
        {
            Context.PAYMENT.Attach(Entity);
            Context.DeleteObject(Entity);
        }

        public void DeleteById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
