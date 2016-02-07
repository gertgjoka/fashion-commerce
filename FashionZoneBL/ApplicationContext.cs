using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using FashionZone.BL.DAO;
using System.Threading;

namespace FashionZone.BL
{
    public sealed class ApplicationContext : IApplicationContext
    {
        // thread safe 
        private static readonly ApplicationContext currentIstance = new ApplicationContext();
        private static object sync = new object();

        private ApplicationContext()
        {
            this.Factory = new UnityFactory();
        }

        public IFactory Factory { get; private set; }

        public static ApplicationContext Current
        {
            get
            {
                return currentIstance;
            }
        }

        public IUnityContainer Container
        {
            get { return this.Factory.Container as UnityContainer; }
        }


        #region MANAGER Entity
        public Manager.IAttributeManager Attributes
        {
            get { return this.Factory.CreateAttributeManager(); }
        }

        public Manager.IBonusManager Bonuses
        {
            get { return this.Factory.CreateBonusManager(); }
        }

        public Manager.IBrandManager Brands
        {
            get { return this.Factory.CreateBrandManager(); }
        }

        public Manager.ICampaignManager Campaigns
        {
            get { return this.Factory.CreateCampaignManager(); }
        }

        public Manager.ICategoryManager Categories
        {
            get { return this.Factory.CreateCategoryManager(); }
        }

        public Manager.ICustomerManager Customers
        {
            get { return this.Factory.CreateCustomerManager(); }
        }

        public Manager.IInvitationManager Invitations
        {
            get { return this.Factory.CreateInvitationManager(); }
        }

        public Manager.IOrderManager Orders
        {
            get { return this.Factory.CreateOrderManager(); }
        }

        public Manager.IPaymentManager Payments
        {
            get { return this.Factory.CreatePaymentManager(); }
        }

        public Manager.IProductManager Products
        {
            get { return this.Factory.CreateProductManager(); }
        }

        public Manager.IShoppingCartManager Carts
        {
            get { return this.Factory.CreateShoppingCartManager(); }
        }

        public Manager.IReturnManager Returns
        {
            get { return Factory.CreateReturnManager(); }
        }

        public Manager.IUserManager Users
        {
            get { return Factory.CreateUserManager(); }
        }
        #endregion Fine MANAGER Entity

    }
}
