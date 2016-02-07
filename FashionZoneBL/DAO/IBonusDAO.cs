using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO
{
    public interface IBonusDAO : IDAO<BONUS>
    {
        List<BONUS> GetAllBonusOfCustomer(int idCust);

        List<BONUS> GetAvailableBonusForCustomer(int CustomerId);
    }
}
