using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;

namespace FashionZone.BL.Manager.Impl
{
    public class PaymentManager : BaseManager, IPaymentManager
    {
        private IPaypalPaymentDAO _paypalPaymentDAO;
        private ICashPaymentDAO _cashPaymentDAO;
        private IPaymentDAO _paymentDAO;
        private ICurrencyDAO _currencyDAO;
        private IEasyPayPaymentDAO _easyPayPaymentDAO;
        public PaymentManager(IContextContainer container)
            : base(container)
        {
            _paypalPaymentDAO = new PaypalPaymentDAO(container);
            _easyPayPaymentDAO = new EasyPayPaymentDAO(container);
            _cashPaymentDAO = new CashPaymentDAO(container);
            _paymentDAO = new PaymentDAO(container);
            _currencyDAO = new CurrencyDAO(container);
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
            return _paymentDAO.GetById(Id);
        }

        public void Insert(PAYMENT Entity)
        {
            _paymentDAO.Insert(Entity);
            if (Entity.Type == (int)PaymentType.CA)
            {
                //Context.ObjectStateManager.ChangeObjectState(Entity.CASH_PAYMENT, System.Data.EntityState.Added);
                _cashPaymentDAO.Insert(Entity.CASH_PAYMENT);
            }
            else if (Entity.Type == (int)PaymentType.PP)
            {
                //Context.ObjectStateManager.ChangeObjectState(Entity.PAYPAL_PAYMENT, System.Data.EntityState.Added);
                _paypalPaymentDAO.Insert(Entity.PAYPAL_PAYMENT);
            }
            else if (Entity.Type == (int)PaymentType.EP)
            {
                //Context.ObjectStateManager.ChangeObjectState(Entity.PAYPAL_PAYMENT, System.Data.EntityState.Added);
                _easyPayPaymentDAO.Insert(Entity.EASYPAY_PAYMENT);
            }
            Context.SaveChanges();
        }

        public void Update(PAYMENT Entity)
        {
            _paymentDAO.Update(Entity);

            if (Entity.Type == (int)PaymentType.CA)
            {
                _cashPaymentDAO.Update(Entity.CASH_PAYMENT);
            }
            else if (Entity.Type == (int)PaymentType.PP)
            {
                _paypalPaymentDAO.Update(Entity.PAYPAL_PAYMENT);
            }
            Context.SaveChanges();
        }

        public void Delete(PAYMENT Entity)
        {
            _paymentDAO.Delete(Entity);

            if (Entity.Type == (int)PaymentType.CA)
            {
                _cashPaymentDAO.Delete(Entity.CASH_PAYMENT);
            }
            else if (Entity.Type == (int)PaymentType.PP)
            {
                _paypalPaymentDAO.Delete(Entity.PAYPAL_PAYMENT);
            }
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

        public PAYPAL_PAYMENT GetByTxnId(string TxnID)
        {
            return _paypalPaymentDAO.GetByTxnId(TxnID);
        }


        public void UpdatePayPalPayment(PAYPAL_PAYMENT PPayment)
        {
            _paypalPaymentDAO.Update(PPayment, true);
            Context.SaveChanges();
        }


        public void UpdatePayPalPayment(PAYPAL_PAYMENT PP, bool Attach = true)
        {
            _paypalPaymentDAO.Update(PP, Attach);
            Context.SaveChanges();
        }


        public CURRENCY GetLastConversionRate()
        {
            return _currencyDAO.GetLastRate();
        }


        public void InsertConversionRate(CURRENCY Entity)
        {
            _currencyDAO.Insert(Entity);
            Context.SaveChanges();
        }

        public void UpdateConversionRate(CURRENCY Entity)
        {
            _currencyDAO.Update(Entity);
            Context.SaveChanges();
        }

        public EASYPAY_PAYMENT GetEPByTxnId(string TxnID)
        {
            return _easyPayPaymentDAO.GetByTxnId(TxnID);
        }

    }
}
