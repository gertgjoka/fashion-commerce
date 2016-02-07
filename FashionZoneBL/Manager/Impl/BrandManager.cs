using System;
using System.Collections.Generic;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager.Impl
{
    class BrandManager : BaseManager, IBrandManager
    {
        private IBrandDAO _brandDAO;

        public BrandManager(IContextContainer container)
            : base(container)
        {
            _brandDAO = new BrandDAO(container);
        }

        public List<BRAND> GetBrandTopList(int length)
        {
            return _brandDAO.GetBrandTopList(length);
        }

        public List<BRAND> GetBrandWithActiveCampaign()
        {
            return _brandDAO.GetBrandWithActiveCampaign();
        }

        public List<BRAND> Search(BRAND Brand, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _brandDAO.Search(Brand, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<BRAND> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _brandDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public BRAND GetById(int id)
        {
            return _brandDAO.GetById(id);
        }

        public void Insert(BRAND Brand)
        {
            _brandDAO.Insert(Brand);
            Context.SaveChanges();
        }

        public void Update(BRAND Brand)
        {
            _brandDAO.Update(Brand);
            Context.SaveChanges();
        }

        public void Delete(BRAND Brand)
        {
            _brandDAO.Delete(Brand);
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            _brandDAO.DeleteById(Id);
            Context.SaveChanges();
        }


        public List<BRAND> GetBrandWithRecentCampaign()
        {
            return _brandDAO.GetBrandWithRecentCampaign();
        }
    }
}
