using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO
{
    interface IEasyPayPaymentDAO : IDAO<EASYPAY_PAYMENT>
    {
        EASYPAY_PAYMENT GetByTxnId(string TxnID);
    }
}
