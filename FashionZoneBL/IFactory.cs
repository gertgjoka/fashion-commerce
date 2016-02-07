using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.BL.DAO;
using FashionZone.BL.Manager;

namespace FashionZone.BL
{
    public interface IFactory
    {
        object Container { get; }

        ICampaignManager CreateCampaignManager();
        IOrderManager CreateOrderManager();
        IProductManager CreateProductManager();
        IBonusManager CreateBonusManager();
        IInvitationManager CreateInvitationManager();
        IShoppingCartManager CreateShoppingCartManager();

        //new Manger 
        IAttributeManager CreateAttributeManager();
        IBrandManager CreateBrandManager();
        ICategoryManager CreateCategoryManager();
        ICustomerManager CreateCustomerManager();
        IReturnManager CreateReturnManager();
        IUserManager CreateUserManager();
        IPaymentManager CreatePaymentManager();
        //END new Manager

        ICustomerDAO CreateCustomerDAO();
        IProductDAO CreateProductDAO();
        ICategoryDAO CreateCategoryDAO();
        IOrderDAO CreateOrderDAO();
        IBrandDAO CreateBrandDAO();
        ICampaignDAO CreateCampaignDAO();
        IUserDAO CreateUserDAO();
        IReturnDAO CreateReturnDAO();
        IBonusDAO CreateBonusDAO();
        IAttributeDAO CreateAttributeDAO();
        IInvitationDAO CreateInvitationDAO();
    }
}
