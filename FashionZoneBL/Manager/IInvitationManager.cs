using System;
using System.Collections.Generic;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager
{
    public interface IInvitationManager:IManager <INVITATION>
    {
        List<INVITATION> GetAllInvitationOfCustomer(int idCust);
    }
}
