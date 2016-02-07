using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.BL.DAO;
using FashionZone.BL.Manager;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace FashionZone.BL
{
    public class UnityFactory : IFactory
    {
        private IUnityContainer container;
        private UnityConfigurationSection configurationSection;

        public UnityFactory()
        {
            Initialize();
        }
        private void Initialize()
        {
            container = new UnityContainer();

            configurationSection = Configuration.UnitySection as UnityConfigurationSection;

            if (configurationSection.Containers.Default != null)
            {
                configurationSection.Configure(container);
            }
        }

        public object Container
        {
            get { return container; }
        }

        #region DAO
        public ICustomerDAO CreateCustomerDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<ICustomerDAO>();
        }

        public IProductDAO CreateProductDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IProductDAO>();
        }

        public ICategoryDAO CreateCategoryDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<ICategoryDAO>();
        }

        public IOrderDAO CreateOrderDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IOrderDAO>();
        }

        public IBrandDAO CreateBrandDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IBrandDAO>();
        }

        public ICampaignDAO CreateCampaignDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<ICampaignDAO>();
        }

        public IUserDAO CreateUserDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IUserDAO>();
        }

        public IReturnDAO CreateReturnDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IReturnDAO>();
        }

        public IBonusDAO CreateBonusDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IBonusDAO>();
        }

        public IAttributeDAO CreateAttributeDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IAttributeDAO>();
        }

        public IInvitationDAO CreateInvitationDAO()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IInvitationDAO>();
        }
        #endregion Fine DAO

        #region MANAGER
        public IAttributeManager CreateAttributeManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IAttributeManager>();
        }

        public IBonusManager CreateBonusManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IBonusManager>();
        }

        public IBrandManager CreateBrandManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IBrandManager>();
        }

        public ICampaignManager CreateCampaignManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<ICampaignManager>();
        }

        public ICategoryManager CreateCategoryManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<ICategoryManager>();
        }

        public ICustomerManager CreateCustomerManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<ICustomerManager>();
        }

        public IInvitationManager CreateInvitationManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IInvitationManager>();
        }

        public IOrderManager CreateOrderManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IOrderManager>();
        }

        public IProductManager CreateProductManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IProductManager>();
        }

        public IPaymentManager CreatePaymentManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IPaymentManager>();
        }

        public IShoppingCartManager CreateShoppingCartManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IShoppingCartManager>();
        }
        
        public IReturnManager CreateReturnManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IReturnManager>();
        }

        public IUserManager CreateUserManager()
        {
            // Resolve the abstract type and activate the concrete type specified in the application configuration
            return container.Resolve<IUserManager>();
        }
        #endregion Fine MANAGER
    }
}
