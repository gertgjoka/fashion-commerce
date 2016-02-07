using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;
using System.Data;
using System.Data.Objects;

namespace FashionZone.BL.DAO.Impl
{
    public class ProductDAO : BaseDAO, IProductDAO
    {
        public ProductDAO(IContextContainer container)
            : base(container)
        {
        }

        public void DeleteById(int Id)
        {
            PRODUCT product = new PRODUCT() { ID = Id };
            Delete(product);
        }

        public List<PRODUCT> Search(PRODUCT Product, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            // creating a graph with category and campaign to have the related campign for this product
            var result = Context.PRODUCT.Include("CATEGORY.CAMPAIGN").Include("PROD_IMAGES").AsQueryable();
            if (Product != null)
            {
                if (!String.IsNullOrWhiteSpace(Product.Name))
                {
                    result = result.Where(p => p.Name.Contains(Product.Name));
                }

                if (!String.IsNullOrWhiteSpace(Product.Description))
                {
                    result = result.Where(p => p.Description.Contains(Product.Description));
                }

                if (!String.IsNullOrWhiteSpace(Product.Code))
                {
                    result = result.Where(p => p.Code.Contains(Product.Code));
                }

                if (Product.CampaignID.HasValue)
                {
                    result = result.Where(p => p.CATEGORY.FirstOrDefault().CampaignID == Product.CampaignID);
                }

                if (Product.Closed.HasValue)
                {
                    result = result.Where(p => p.CATEGORY.FirstOrDefault().CAMPAIGN.Active == !Product.Closed.Value);
                }
            }
            TotalRecords = result.Count();
            if (!String.IsNullOrEmpty(OrderExp) && OrderExp.Equals("Remaining"))
            {
                if (SortDirection == SortDirection.Ascending)
                    result = result.OrderBy(c => c.PRODUCT_ATTRIBUTE.Select(p => p.Availability).Sum());
                else
                    result = result.OrderByDescending(c => c.PRODUCT_ATTRIBUTE.Select(p => p.Availability).Sum());
            }
            else
            {
                GenericSorterCaller<PRODUCT> sorter = new GenericSorterCaller<PRODUCT>();
                result = sorter.Sort(result, String.IsNullOrEmpty(OrderExp) ? "Name" : OrderExp, SortDirection);
            }
            result = result.Skip(PageIndex * PageSize).Take(PageSize);
            var SQL = (result as ObjectQuery).ToTraceString();

            return result.ToList();
        }

        public List<PRODUCT> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public PRODUCT GetById(int Id)
        {
            var product = Context.PRODUCT.Include("PROD_IMAGES").Include("CATEGORY").Where(p => p.ID == Id).FirstOrDefault();

            if (product.CATEGORY != null && product.CATEGORY.Count > 0)
            {
                product.Campaign = product.CATEGORY.ElementAt(0).CAMPAIGN.Name;
                product.CampaignID = product.CATEGORY.ElementAt(0).CAMPAIGN.ID;
            }
            return product;

        }

        public void Insert(PRODUCT Product)
        {
            Context.PRODUCT.AddObject(Product);
            // Many to many relationship, the categories have to be treated 
            // differently, so no new category is created
            if (Product.CATEGORY != null && Product.CATEGORY.Count > 0)
            {
                foreach (CATEGORY cat in Product.CATEGORY)
                {
                    Context.ObjectStateManager.ChangeObjectState(cat, EntityState.Unchanged);
                }
            }
        }

        public void Update(PRODUCT Product, bool Attach)
        {
            // first remove all categories
            Context.ExecuteStoreCommand("delete from product_category where ProductID = {0}", Product.ID);
            if (Attach)
            {
                Context.PRODUCT.AddObject(Product);
                Context.ObjectStateManager.ChangeObjectState(Product, EntityState.Modified);
            }
            // Many to many relationship, the categories have to be treated 
            // differently, so no new category is created
            if (Product.CATEGORY != null && Product.CATEGORY.Count > 0)
            {
                foreach (CATEGORY cat in Product.CATEGORY)
                {
                    Context.ObjectStateManager.ChangeObjectState(cat, EntityState.Unchanged);
                }
            }
        }

        public void Delete(PRODUCT Product)
        {
            Context.PRODUCT.Attach(Product);
            Context.PRODUCT.DeleteObject(Product);
        }

        public List<PROD_IMAGES> GetImages(PRODUCT Product)
        {
            throw new NotImplementedException();
        }

        public List<PROD_IMAGES> GetImages(int ProductId)
        {
            var images = Context.PROD_IMAGES.Where(i => i.ProductID == ProductId).ToList();
            return images;
        }

        public List<PRODUCT_ATTRIBUTE> GetProductAttributes(int ProductID)
        {
            List<PRODUCT_ATTRIBUTE> attributes = Context.PRODUCT_ATTRIBUTE.Include("D_ATTRIBUTE_VALUE").Where(pa => pa.ProductID == ProductID).ToList();
            return attributes;
        }

        public void AddProductAttribute(PRODUCT_ATTRIBUTE Attribute)
        {
            Context.PRODUCT_ATTRIBUTE.AddObject(Attribute);
        }

        public void DeleteImageById(int ImageId)
        {
            PROD_IMAGES img = new PROD_IMAGES() { ID = ImageId };

            Context.PROD_IMAGES.Attach(img);
            Context.PROD_IMAGES.DeleteObject(img);
        }

        public void UpdateImage(PROD_IMAGES Image)
        {
            Context.PROD_IMAGES.AddObject(Image);
            Context.ObjectStateManager.ChangeObjectState(Image, EntityState.Modified);
        }

        public void InsertImage(PROD_IMAGES Image)
        {
            Context.PROD_IMAGES.AddObject(Image);
        }

        public void UpdateProductAttribute(PRODUCT_ATTRIBUTE Attribute, bool Attach = true)
        {
            if (Attach)
            {
                Context.PRODUCT_ATTRIBUTE.Attach(Attribute);
            }
            var entry = Context.ObjectStateManager.GetObjectStateEntry(Attribute);
            entry.SetModifiedProperty("Availability");
        }


        public PRODUCT_ATTRIBUTE GetProductAttributeById(int ProdAttrID)
        {
            return Context.PRODUCT_ATTRIBUTE.Where(p => p.ID == ProdAttrID).FirstOrDefault();
        }


        public List<FZAttributeAvailability> GetProductAttributeValues(int ProductID)
        {
            IQueryable<FZAttributeAvailability> l = from a in Context.D_ATTRIBUTE_VALUE
                                                    join p in Context.PRODUCT_ATTRIBUTE
                                                    on a.ID equals p.AttributeValueID
                                                    where p.ProductID == ProductID
                                                    orderby a.ShowOrder
                                                    select new FZAttributeAvailability { Id = p.ID, Value = a.Value, Availability = p.Availability };

            return l.ToList();
        }

        public PRODUCT_ATTRIBUTE GetProductAvailability(int AttributeValueID, out List<int> AvailabilityList, int? AlreadyInCart = null)
        {
            PRODUCT_ATTRIBUTE prodAttr = Context.PRODUCT_ATTRIBUTE.Where(p => p.ID == AttributeValueID).FirstOrDefault();
            int itemNr = 0;
            if ((prodAttr.Availability >= Configuration.MaxOrderQuantityPerProduct && !AlreadyInCart.HasValue) || (AlreadyInCart.HasValue && AlreadyInCart.Value < Configuration.MaxOrderQuantityPerProduct
                && (prodAttr.Availability + AlreadyInCart.Value) >= Configuration.MaxOrderQuantityPerProduct))
            {
                itemNr = Configuration.MaxOrderQuantityPerProduct;
            }
            else if (AlreadyInCart.HasValue && AlreadyInCart.Value >= Configuration.MaxOrderQuantityPerProduct)
            {
                itemNr = AlreadyInCart.Value;
            }
            else if (prodAttr.Availability > 0 || (AlreadyInCart.HasValue && prodAttr.Availability + AlreadyInCart.Value < Configuration.MaxOrderQuantityPerProduct))
            {
                if (AlreadyInCart.HasValue)
                {
                    itemNr = prodAttr.Availability + AlreadyInCart.Value;
                }
                else
                {
                    itemNr = prodAttr.Availability;
                }
            }

            AvailabilityList = new List<int>();
            if (itemNr > 0)
            {
                for (int i = 1; i <= itemNr; i++)
                {
                    AvailabilityList.Add(i);
                }
            }

            return prodAttr;
        }

        public void ChangeApproval(PRODUCT Product)
        {
            Context.PRODUCT.AddObject(Product);
            Context.ObjectStateManager.ChangeObjectState(Product, EntityState.Modified);
            var entry = Context.ObjectStateManager.GetObjectStateEntry(Product);
            entry.SetModifiedProperty("Approved");
            entry.SetModifiedProperty("ApprovedBy");
        }

        public bool? GetApprovalStatus(int ProductId)
        {
            return Context.GetProductApprovalStatus(ProductId).First();
        }

        public void Update(PRODUCT Entity)
        {
            Update(Entity, true);
        }


        public List<PRODUCT> GetProductsByCampaign(int CampaignID, int? CategoryID)
        {
            return Context.GetCampaignProducts(CampaignID, CategoryID).ToList();
        }
    }
}
