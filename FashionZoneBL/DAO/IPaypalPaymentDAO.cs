using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO
{
    public interface IPaypalPaymentDAO : IDAO<PAYPAL_PAYMENT>
    {
        PAYPAL_PAYMENT GetByTxnId(string TxnID);
        void Update(PAYPAL_PAYMENT PP, bool Attach = true);
    }
}
