using System;
using System.Collections.Generic;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager
{
    public interface IBonusManager : IManager<BONUS>
    {
        List<BONUS> GetAllBonusOfCustomer(int idCust);

        List<BONUS> GetAvailableBonusForCustomer(int CustomerId);
    }
}
