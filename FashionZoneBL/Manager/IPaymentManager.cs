using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager
{
    public interface IPaymentManager : IManager<PAYMENT>
    {
        PAYPAL_PAYMENT GetByTxnId(string TxnID);
        EASYPAY_PAYMENT GetEPByTxnId(string TxnID);
        void UpdatePayPalPayment(PAYPAL_PAYMENT PPayment);
        void UpdatePayPalPayment(PAYPAL_PAYMENT PP, bool Attach = true);
        CURRENCY GetLastConversionRate();
        void InsertConversionRate(CURRENCY Entity);
        void UpdateConversionRate(CURRENCY Entity);
    }
}
