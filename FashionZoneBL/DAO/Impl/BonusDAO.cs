using System;
using System.Collections.Generic;
using System.Linq;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;

namespace FashionZone.BL.DAO.Impl
{
    class BonusDAO : BaseDAO, IBonusDAO
    {
        public BonusDAO(IContextContainer container)
            : base(container)
        {
        }

        private const string DEFAULT_ORDER_EXP = "ID";

        public List<BONUS> Search(BONUS Bonus, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            var result = Context.BONUS.AsQueryable();
            if (Bonus != null)
            {
                if (Bonus.ID != 0)
                {
                    result = result.Where(b => b.ID == Bonus.ID);
                }

                if (!String.IsNullOrEmpty(Bonus.Description))
                {
                    result = result.Where(b => b.Description.Contains(Bonus.Description));
                }

                if (Bonus.CustomerID.HasValue)
                {
                    result = result.Where(b => b.CustomerID == Bonus.CustomerID.Value);
                }

                if (!String.IsNullOrWhiteSpace(Bonus.CustomerFullName))
                {
                    result = result.Where(s => (s.CUSTOMER.Name + " " + s.CUSTOMER.Surname).Contains(Bonus.CustomerFullName));
                }

                // useful for filtering expired bonuses
                if (Bonus.Validity.HasValue)
                {
                    result = result.Where(b => b.Validity >= Bonus.Validity);
                }

                if (Bonus.CustomerID.HasValue)
                {
                    result = result.Where(b => b.CustomerID == Bonus.CustomerID);
                }
            }
            TotalRecords = result.Count();
            if (!String.IsNullOrEmpty(OrderExp) && OrderExp.Equals("CustomerFullName"))
            {
                if (SortDirection == SortDirection.Ascending)
                    result = result.OrderBy(c => c.CUSTOMER.Name).ThenBy(c => c.CUSTOMER.Surname);
                else
                    result = result.OrderByDescending(c => c.CUSTOMER.Name).ThenBy(c => c.CUSTOMER.Surname);
            }
            else
            {
                GenericSorterCaller<BONUS> sorter = new GenericSorterCaller<BONUS>();
                result = sorter.Sort(result, string.IsNullOrEmpty(OrderExp) ? DEFAULT_ORDER_EXP : OrderExp, SortDirection);
            }

            // pagination
            return result.Skip(PageIndex * PageSize).Take(PageSize).ToList();
        }

        public List<BONUS> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public BONUS GetById(int id)
        {
            var bonus = Context.BONUS.Where(b => b.ID == id).FirstOrDefault();
            return bonus;
        }

        public List<BONUS> GetAvailableBonusForCustomer(int CustomerId)
        {
            var bonuses = Context.BONUS.Where(b => b.CustomerID == CustomerId && b.Validity >= DateTime.Today && b.ValueRemainder.Value > 0).ToList();
            return bonuses;
        }

        public void Insert(BONUS newBonus)
        {
            Context.BONUS.AddObject(newBonus);
        }

        public void Update(BONUS upBonus)
        {
            Context.BONUS.Attach(upBonus);
            Context.ObjectStateManager.ChangeObjectState(upBonus, System.Data.EntityState.Modified);
        }

        public void Delete(BONUS delBonus)
        {
            BONUS bon = Context.BONUS.Where(b => b.ID == delBonus.ID).FirstOrDefault();
            Context.DeleteObject(bon);
        }

        public void DeleteById(int Id)
        {
            BONUS obj = new BONUS() { ID = Id };
            Delete(obj);
        }

        public List<BONUS> GetAllBonusOfCustomer(int idCust)
        {
            var bonus = Context.BONUS.Where(b => b.CustomerID == idCust);
            return bonus.ToList();
        }
    }
}
