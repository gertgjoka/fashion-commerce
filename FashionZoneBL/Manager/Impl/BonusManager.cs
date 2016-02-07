using System;
using System.Collections.Generic;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;
using System.Transactions;

namespace FashionZone.BL.Manager.Impl
{
    class BonusManager : BaseManager, IBonusManager
    {
        private IBonusDAO _bonusDAO;

        public BonusManager(IContextContainer container) : base(container)
        {
            _bonusDAO = new BonusDAO(container);
        }


        public List<BONUS> Search(BONUS Bonus, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _bonusDAO.Search(Bonus, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<BONUS> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _bonusDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public BONUS GetById(int id)
        {
            return _bonusDAO.GetById(id);
        }

        public void Insert(BONUS Bonus)
        {
            _bonusDAO.Insert(Bonus);
            Context.SaveChanges();
        }

        public void Update(BONUS Bonus)
        {
            _bonusDAO.Update(Bonus);
            Context.SaveChanges();
        }

        public void Delete(BONUS Bonus)
        {
            _bonusDAO.Delete(Bonus);
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            BONUS obj = new BONUS() { ID = Id };
            Delete(obj);
        }

        public List<BONUS> GetAllBonusOfCustomer(int idCust)
        {
            return _bonusDAO.GetAllBonusOfCustomer(idCust);
        }

        public List<BONUS> GetAvailableBonusForCustomer(int CustomerId)
        {
            return _bonusDAO.GetAvailableBonusForCustomer(CustomerId);
        }
    }
}
