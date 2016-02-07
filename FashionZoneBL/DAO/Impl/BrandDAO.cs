using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.DAO;
using FashionZone.BL.Util;

namespace FashionZone.BL.DAO.Impl
{
    /// <summary>
    /// Implements the functionality of the Brand entity
    /// </summary>
    public class BrandDAO : BaseDAO, IBrandDAO
    {
        public BrandDAO(IContextContainer container)
            : base(container)
        {
        }

        private const string DEFAULT_ORDER_EXP = "ShowName";
        /// <summary>
        /// Gets the details of the brand identified by id
        /// </summary>
        /// <param name="id">Identification of the brand</param>
        /// <returns>A single Brand,or a default value if the sequence contains no elements.</returns>
        public BRAND GetById(int id)
        {
            var brand = Context.BRAND.Where(b => b.ID == id).FirstOrDefault();
            return brand;
        }

        /// <summary>
        /// Get a listo of Brands.
        /// </summary>
        /// <param name="length">How many elemens extract.</param>
        /// <returns>List of TOP "length" BRAND.</returns>
        public List<BRAND> GetBrandTopList(int length)
        {

            var brands = Context.BRAND.Take(length).ToList();
            return brands;
        }


        public void Insert(BRAND newBrand)
        {
            Context.BRAND.AddObject(newBrand);
            //Context.SaveChanges();
        }

        public void Update(BRAND upBrand)
        {
            Context.BRAND.Attach(upBrand);
            Context.ObjectStateManager.ChangeObjectState(upBrand, System.Data.EntityState.Modified);
            //int countRowUpdate = Context.SaveChanges();
        }

        public void Delete(BRAND delBrand)
        {
            Context.BRAND.Attach(delBrand);
            Context.DeleteObject(delBrand);
            //Context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            BRAND obj = new BRAND() { ID = id };
            Delete(obj);
        }

        public List<BRAND> Search(BRAND Brand, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            var result = Context.BRAND.AsQueryable();
            if (Brand != null)
            {
                if (!String.IsNullOrEmpty(Brand.Name))
                {
                    result = result.Where(b => b.Name.Contains(Brand.Name));
                }

                if (!String.IsNullOrEmpty(Brand.ShowName))
                {
                    result = result.Where(b => b.ShowName.Contains(Brand.ShowName));
                }

                if (!String.IsNullOrEmpty(Brand.Description))
                {
                    result = result.Where(b => b.Description.Contains(Brand.Description));
                }

                if (!String.IsNullOrEmpty(Brand.Email))
                {
                    result = result.Where(b => b.Email.Contains(Brand.Email));
                }
            }
            TotalRecords = result.Count();

            GenericSorterCaller<BRAND> sorter = new GenericSorterCaller<BRAND>();
            result = sorter.Sort(result, string.IsNullOrEmpty(OrderExp) ? DEFAULT_ORDER_EXP : OrderExp, SortDirection);

            // pagination
            return result.Skip(PageIndex * PageSize).Take(PageSize).ToList();
        }

        public List<BRAND> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        /// <summary>
        /// Get a list of Brands with ACTIVE or future Campaign.
        /// </summary>
        /// <returns>List of BRAND with ACTIVE Campaign.</returns>
        public List<BRAND> GetBrandWithActiveCampaign()
        {
            var result = Context.BRAND.Include("CAMPAIGN").AsQueryable();
            var brandL = result.Where(b => b.CAMPAIGN.Any(cam => cam.Active || cam.StartDate >= DateTime.Today)).ToList();
            return brandL;
        }


        public List<BRAND> GetBrandWithRecentCampaign()
        {
            var result = Context.BRAND.Include("CAMPAIGN").AsQueryable();
            DateTime start = DateTime.Today.AddMonths(-5);
            DateTime end = DateTime.Today.AddMonths(5);
            var brandL = result.Where(b => b.CAMPAIGN.Any(cam => cam.Active || (cam.StartDate >= start && cam.EndDate <= end))).ToList();
            return brandL;
        }
    }
}
