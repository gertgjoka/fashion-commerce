using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;
using System.Data.Objects;

namespace FashionZone.BL.DAO.Impl
{
    public class ShoppingCartDAO : BaseDAO, IShoppingCartDAO
    {
        public ShoppingCartDAO(IContextContainer container)
            : base(container)
        {
        }

        public List<SHOPPING_CART> Search(SHOPPING_CART Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            var result = Context.SHOPPING_CART.AsQueryable();

            if (Entity != null)
            {
                if (!String.IsNullOrWhiteSpace(Entity.ProductName))
                {
                    result = result.Where(s => s.PRODUCT_ATTRIBUTE.PRODUCT.Name.Contains(Entity.ProductName));
                }

                if (!String.IsNullOrWhiteSpace(Entity.CampaignName))
                {
                    result = result.Where(s => s.CAMPAIGN.Name.Contains(Entity.CampaignName));
                }

                if (!String.IsNullOrWhiteSpace(Entity.CustomerName))
                {
                    result = result.Where(s => (s.CUSTOMER.Name + " " + s.CUSTOMER.Surname).Contains(Entity.CustomerName));
                }

                if (Entity.SearchStartDate.HasValue)
                {
                    result = result.Where(s => s.DateAdded >= Entity.SearchStartDate.Value);
                }

                if (Entity.CampaignID != 0)
                {
                    result = result.Where(s => s.CampaignID == Entity.CampaignID);
                }

                if (!String.IsNullOrWhiteSpace(Entity.ID))
                {
                    result = result.Where(s => s.ID == Entity.ID);
                }
            }

            TotalRecords = result.Count();

            if (!String.IsNullOrEmpty(OrderExp) && OrderExp.Equals("ProductName"))
            {
                if (SortDirection == SortDirection.Ascending)
                    result = result.OrderBy(s => s.PRODUCT_ATTRIBUTE.PRODUCT.Name);
                else
                    result = result.OrderByDescending(s => s.PRODUCT_ATTRIBUTE.PRODUCT.Name);
            }
            else if (!String.IsNullOrEmpty(OrderExp) && OrderExp.Equals("CustomerName"))
            {
                if (SortDirection == SortDirection.Ascending)
                    result = result.OrderBy(s => s.CUSTOMER.Name).ThenBy(s => s.CUSTOMER.Surname);
                else
                    result = result.OrderByDescending(s => s.CUSTOMER.Name).ThenBy(s => s.CUSTOMER.Surname);
            }
            else if (!String.IsNullOrEmpty(OrderExp) && OrderExp.Equals("CampaignName"))
            {
                if (SortDirection == SortDirection.Ascending)
                    result = result.OrderBy(s => s.CAMPAIGN.Name);
                else
                    result = result.OrderByDescending(s => s.CAMPAIGN.Name);
            }
            else
            {
                GenericSorterCaller<SHOPPING_CART> sorter = new GenericSorterCaller<SHOPPING_CART>();
                result = sorter.Sort(result, String.IsNullOrEmpty(OrderExp) ? "ID" : OrderExp, SortDirection);
            }
            // pagination
            return result.Skip(PageIndex * PageSize).Take(PageSize).ToList();
        }

        public List<SHOPPING_CART> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public SHOPPING_CART GetById(int id)
        {
            throw new NotImplementedException();
        }

        public SHOPPING_CART GetById(String Id, int ProdAttrID)
        {
            return Context.SHOPPING_CART.Include("PRODUCT_ATTRIBUTE").Include("PRODUCT_ATTRIBUTE.PRODUCT").Include("CAMPAIGN").Include("PRODUCT_ATTRIBUTE.PRODUCT.PROD_IMAGES").Where(s => s.ID == Id && s.ProdAttrID == ProdAttrID).FirstOrDefault();
        }

        public void Insert(SHOPPING_CART Entity)
        {
            if (String.IsNullOrWhiteSpace(Entity.ID))
            {
                throw new Exception("Entity key null or empty or white space.");
            }

            Context.SHOPPING_CART.AddObject(Entity);
        }

        public void Update(SHOPPING_CART Entity)
        {
            Context.SHOPPING_CART.Attach(Entity);
            var entry = Context.ObjectStateManager.GetObjectStateEntry(Entity);
            entry.SetModifiedProperty("Quantity");
            entry.SetModifiedProperty("DateAdded");
        }

        public void Delete(SHOPPING_CART Entity, bool Attach = true)
        {
            if (Attach)
            {
                Context.SHOPPING_CART.Attach(Entity);
            }
            Context.SHOPPING_CART.DeleteObject(Entity);
        }

        public void Delete(SHOPPING_CART Entity)
        {
            Delete(Entity, true);
        }

        public void DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This must never be used alone.
        /// To guarantee product availability consistency, the method must be called inside an ObjectContext transaction scope which also
        /// updates the availability of product quantities.
        /// </summary>
        /// <param name="Id"> The guid identifying this particular shopping cart</param>
        public void DeleteShoppingCart(String Id)
        {
            Context.ExecuteStoreCommand("delete from shopping_cart where ID = {0}", Id);
        }

        public List<SHOPPING_CART> GetShoppingCartItems(String Id)
        {

            List<SHOPPING_CART> list = Context.SHOPPING_CART.Include("PRODUCT_ATTRIBUTE").Include("PRODUCT_ATTRIBUTE.PRODUCT").Include("CAMPAIGN").Include("PRODUCT_ATTRIBUTE.PRODUCT.PROD_IMAGES").Where(s => s.ID == Id).ToList();
            return list;
        }

        public decimal? GetShoppingCartTotalAmount(String Id)
        {
            decimal? result = Context.GetShoppingCartTotalAmount(Id).First();
            return result;
        }

        public int? GetShoppingCartTotalItems(String Id)
        {
            int? result = Context.GetShoppingCartTotalItems(Id).First();
            return result;
        }


        public List<String> GetShoppingCarts()
        {
            return Context.SHOPPING_CART.Select(c => c.ID).Distinct().ToList();
        }


        public List<string> GetExpiredCarts()
        {
            return Context.GetExpiredCarts().ToList();
        }
    }
}
