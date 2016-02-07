using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO.Impl
{
    public class EasyPayPaymentDAO : BaseDAO, IEasyPayPaymentDAO
    {

        public EasyPayPaymentDAO(IContextContainer container)
            : base(container)
        {
        }

        public List<EASYPAY_PAYMENT> Search(EASYPAY_PAYMENT Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public List<EASYPAY_PAYMENT> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public EASYPAY_PAYMENT GetById(int Id)
        {
            return Context.EASYPAY_PAYMENT.Where(p => p.ID == Id).FirstOrDefault();
        }

        public void Insert(EASYPAY_PAYMENT Entity)
        {
            Context.EASYPAY_PAYMENT.AddObject(Entity);
        }

        public void Update(EASYPAY_PAYMENT Entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(EASYPAY_PAYMENT Entity)
        {
            Context.EASYPAY_PAYMENT.Attach(Entity);
            Context.DeleteObject(Entity);
        }

        public void DeleteById(int Id)
        {
            EASYPAY_PAYMENT pPayment = new EASYPAY_PAYMENT() { ID = Id };
            Delete(pPayment);
        }

        public EASYPAY_PAYMENT GetByTxnId(string TxnID)
        {
            return Context.EASYPAY_PAYMENT.Where(p => p.TransactionID == TxnID).FirstOrDefault();
        }
    }
}
