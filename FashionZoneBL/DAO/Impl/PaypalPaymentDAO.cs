using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO.Impl
{
    public class PaypalPaymentDAO : BaseDAO, IPaypalPaymentDAO
    {
        public PaypalPaymentDAO(IContextContainer container)
            : base(container)
        {
        }

        public List<PAYPAL_PAYMENT> Search(PAYPAL_PAYMENT Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public List<PAYPAL_PAYMENT> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public PAYPAL_PAYMENT GetById(int Id)
        {
            return Context.PAYPAL_PAYMENT.Where(p => p.ID == Id).FirstOrDefault();
        }

        public void Insert(PAYPAL_PAYMENT Entity)
        {
            Context.PAYPAL_PAYMENT.AddObject(Entity);
        }

        public void Update(PAYPAL_PAYMENT Entity)
        {
            Update(Entity, true);
        }

        public void Delete(PAYPAL_PAYMENT Entity)
        {
            Context.PAYPAL_PAYMENT.Attach(Entity);
            Context.DeleteObject(Entity);
        }

        public void DeleteById(int Id)
        {
            PAYPAL_PAYMENT pPayment = new PAYPAL_PAYMENT() { ID = Id };
            Delete(pPayment);
        }

        public PAYPAL_PAYMENT GetByTxnId(string TxnID)
        {
            return Context.PAYPAL_PAYMENT.Where(p => p.TransactionKey == TxnID).FirstOrDefault();
        }


        public void Update(PAYPAL_PAYMENT PP, bool Attach = true)
        {
            if (Attach)
            {
                Context.PAYPAL_PAYMENT.Attach(PP);
            }
            var entry = Context.ObjectStateManager.GetObjectStateEntry(PP);
            entry.SetModifiedProperty("TransactionStatus");
            entry.SetModifiedProperty("Response");
        }
    }
}
