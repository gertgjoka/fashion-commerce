using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.BL.Manager;
using FashionZone.BL.DAO;

namespace FashionZone.BL
{
    public interface IApplicationContext
    {
        IAttributeManager Attributes { get; }
        IBonusManager Bonuses { get; }
        IBrandManager Brands { get; }
        ICampaignManager Campaigns { get; }
        ICategoryManager Categories { get; }
        ICustomerManager Customers { get; }
        IInvitationManager Invitations { get; }
        IOrderManager Orders { get; }
        IProductManager Products { get; }
        IShoppingCartManager Carts { get; }
        IReturnManager Returns { get; }
        IUserManager Users { get; }
    }
}
