using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;

namespace FashionZone.BL.DAO.Impl
{
    public class CategoryDAO : BaseDAO, ICategoryDAO
    {
        public CategoryDAO(IContextContainer container)
            : base(container)
        {
        }

        public CATEGORY GetById(int id)
        {
            var cat = Context.CATEGORY.Where(c => c.ID == id).FirstOrDefault();
            return cat;
        }

        public List<CATEGORY> GetCategoryTopList(int length)
        {
            var cat = Context.CATEGORY.Take(length).ToList();
            return cat;
        }

        public List<CATEGORY> Search(CATEGORY Category, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            throw new NotImplementedException();
        }

        public List<CATEGORY> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public void Insert(CATEGORY newCategory)
        {
            Context.CATEGORY.AddObject(newCategory);
            //Context.SaveChanges();
        }

        public void Update(CATEGORY upCategory)
        {
            Context.CATEGORY.Attach(upCategory);


            var entry = Context.ObjectStateManager.GetObjectStateEntry(upCategory);
            entry.SetModifiedProperty("Name");
            entry.SetModifiedProperty("NameEng");
            entry.SetModifiedProperty("Description");

            entry.SetModifiedProperty("AttributeID");
            entry.SetModifiedProperty("Ordering");
        }

        public void Delete(CATEGORY delCategory)
        {
            Context.CATEGORY.Attach(delCategory);
            Context.DeleteObject(delCategory);
            //Context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            CATEGORY obj = new CATEGORY() { ID = id };
            Delete(obj);
        }

        public List<CATEGORY> GetCategoryListByCampaign(int campaignId)
        {
            var catL = Context.CATEGORY.Where(c => c.CampaignID == campaignId).OrderBy(c => c.Ordering).ToList();
            return catL;
        }

        public List<CATEGORY> GetChildrenCategoryList(int categoryId)
        {
            var catL = Context.CATEGORY.Where(c => c.ParentID == categoryId).OrderBy(c2 => c2.Ordering).ToList();
            return catL;
        }
    }
}
