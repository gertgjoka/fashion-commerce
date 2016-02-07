using System.Collections.Generic;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager.Impl
{
    class CategoryManager:BaseManager, ICategoryManager
    {
        private ICategoryDAO _categoryDAO;

        public CategoryManager(IContextContainer container)
            : base(container)
        {
            _categoryDAO = new CategoryDAO(container);
        }

        
        public List<CATEGORY> GetCategoryTopList(int length)
        {
            return _categoryDAO.GetCategoryTopList(length);
        }

        public List<CATEGORY> GetCategoryListByCampaign(int campaignId)
        {
            return _categoryDAO.GetCategoryListByCampaign(campaignId);
        }

        public List<CATEGORY> GetChildrenCategoryList(int categoryId)
        {
            return _categoryDAO.GetChildrenCategoryList(categoryId);
        }

        public List<CATEGORY> Search(CATEGORY Category, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _categoryDAO.Search(Category, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<CATEGORY> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _categoryDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public CATEGORY GetById(int id)
        {
            return _categoryDAO.GetById(id);
        }

        public void Insert(CATEGORY Category)
        {
            _categoryDAO.Insert(Category);
            Context.SaveChanges();
        }

        public void Update(CATEGORY Category)
        {
            _categoryDAO.Update(Category);
            Context.SaveChanges();
        }

        public void Delete(CATEGORY Category)
        {
            _categoryDAO.Delete(Category);
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            _categoryDAO.DeleteById(Id);
            Context.SaveChanges();
        }
    }
}
