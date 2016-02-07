using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using System.Transactions;
using System.Data.Objects;

namespace FashionZone.BL.Manager.Impl
{
    public class ShoppingCartManager : BaseManager, IShoppingCartManager
    {
        private IShoppingCartDAO _shoppingCartDAO;
        private IProductDAO _productDAO;

        public ShoppingCartManager(IContextContainer container)
            : base(container)
        {
            _shoppingCartDAO = new ShoppingCartDAO(container);
            _productDAO = new ProductDAO(container);
        }

        public List<SHOPPING_CART> Search(SHOPPING_CART Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _shoppingCartDAO.Search(Entity, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<SHOPPING_CART> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _shoppingCartDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public SHOPPING_CART GetById(int Id)
        {
            return _shoppingCartDAO.GetById(Id);
        }

        public void Insert(SHOPPING_CART Entity)
        {
            PRODUCT_ATTRIBUTE prodAttr = _productDAO.GetProductAttributeById(Entity.ProdAttrID);

            Context.PRODUCT_ATTRIBUTE.Detach(prodAttr);

            prodAttr.Availability -= Entity.Quantity;
            if (prodAttr.Availability < 0)
            {
                // partial verification, the exception is raised only if the resulting availability is less than 0
                // using the version that was initially retrieved from db
                // if another user modified this record an OptimisticConcurrencyException will be raised
                prodAttr.Version = Entity.ProductAttributeVersion;
            }

            _productDAO.UpdateProductAttribute(prodAttr);

            _shoppingCartDAO.Insert(Entity);
            try
            {
                if (prodAttr.Availability < 0)
                {
                    throw new System.Data.OptimisticConcurrencyException("Product availability negative.");
                }
                Context.SaveChanges();
            }
            catch (System.Data.OptimisticConcurrencyException ex)
            {
                throw;
            }
            finally
            {
                Context.Refresh(RefreshMode.StoreWins, prodAttr);
                Entity.PRODUCT_ATTRIBUTE = prodAttr;

                // setting the new version for further modifications
                Entity.ProductAttributeVersion = prodAttr.Version;
            }
        }

        public void Update(SHOPPING_CART Entity, int PreviousQuantity)
        {
            PRODUCT_ATTRIBUTE prodAttr = _productDAO.GetProductAttributeById(Entity.ProdAttrID);
            Context.PRODUCT_ATTRIBUTE.Detach(prodAttr);

            if (PreviousQuantity != Entity.Quantity)
            {
                int difference = Entity.Quantity - PreviousQuantity;

                // using the version that was initially retrieved from db
                // if another user modified this record an OptimisticConcurrencyException will be raised only if the new quantity is greater than the old
                // so there is the risk of a negative availability

                // if difference is negative, products are being "returned to warehouse" so the 
                // concurrency check is not needed.
                if ((prodAttr.Availability - difference) < 0)
                {
                    //TODO think what to do with version
                    // set the version that was read when the product-attribute was first retrieved from user interface
                    // the entity must be detached in order to set it's concurrency-check attribute (version in this case)
                    prodAttr.Version = Entity.ProductAttributeVersion;
                }
                prodAttr.Availability -= difference;
                _productDAO.UpdateProductAttribute(prodAttr);
            }

            Entity.PRODUCT_ATTRIBUTE = null;
            _shoppingCartDAO.Update(Entity);
            try
            {
                if (prodAttr.Availability < 0)
                {
                    throw new System.Data.OptimisticConcurrencyException("Product availability negative.");
                }
                Context.SaveChanges();
            }
            catch (System.Data.OptimisticConcurrencyException)
            {
                // when an optimistic exception is raised, the old quantity is restored the old quantity
                Entity.Quantity = PreviousQuantity;
                throw;
            }
            finally
            {
                Context.Refresh(RefreshMode.StoreWins, prodAttr);
                Context.Refresh(RefreshMode.StoreWins, Entity);
                Entity.PRODUCT_ATTRIBUTE = prodAttr;

                // setting the new version for further modifications
                Entity.ProductAttributeVersion = prodAttr.Version;
            }
        }

        public void Update(SHOPPING_CART Entity)
        {
            Update(Entity, Entity.Quantity);
        }

        public void Delete(SHOPPING_CART Entity)
        {
            // first "put back" the product that was in cart
            // version is not needed as the availability is incremented, so no risk
            PRODUCT_ATTRIBUTE prodAttr = _productDAO.GetProductAttributeById(Entity.ProdAttrID);
            prodAttr.Availability += Entity.Quantity;
            _productDAO.UpdateProductAttribute(prodAttr, false);

            // second remove item from cart
            _shoppingCartDAO.Delete(Entity, false);

            // then save if everything was successful
            Context.SaveChanges();
        }

        public void DeleteById(String Id, int ProdAttrID)
        {
            SHOPPING_CART cart = _shoppingCartDAO.GetById(Id, ProdAttrID);
            Delete(cart);
        }

        public List<SHOPPING_CART> GetShoppingCartItems(String Id)
        {
            return _shoppingCartDAO.GetShoppingCartItems(Id);
        }

        public void DeleteShoppingCart(String Id)
        {
            // Get all shopping cart items
            List<SHOPPING_CART> items = _shoppingCartDAO.GetShoppingCartItems(Id);
            PRODUCT_ATTRIBUTE prodAttr;
            // put back to the warehouse each product that was in the cart
            // version is not needed as the availability is incremented, so no risk
            foreach (SHOPPING_CART item in items)
            {
                prodAttr = null;
                prodAttr = _productDAO.GetProductAttributeById(item.ProdAttrID);
                prodAttr.Availability += item.Quantity;
                _productDAO.UpdateProductAttribute(prodAttr, false);
            }
            // delete the shopping cart
            _shoppingCartDAO.DeleteShoppingCart(Id);
            Context.SaveChanges();
        }

        public decimal? GetShoppingCartTotalAmount(String Id)
        {
            return _shoppingCartDAO.GetShoppingCartTotalAmount(Id);
        }


        public List<String> GetShoppingCarts()
        {
            return _shoppingCartDAO.GetShoppingCarts();
        }


        public void DeleteById(int Id)
        {
            throw new NotImplementedException();
        }


        public SHOPPING_CART GetById(string Id, int ProdAttrID)
        {
            return _shoppingCartDAO.GetById(Id, ProdAttrID);
        }


        public int? GetShoppingCartTotalItems(string Id)
        {
            return _shoppingCartDAO.GetShoppingCartTotalItems(Id);
        }


        public List<string> GetExpiredCarts()
        {
            return _shoppingCartDAO.GetExpiredCarts();
        }
    }
}
