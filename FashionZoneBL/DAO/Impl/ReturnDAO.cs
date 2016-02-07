using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;

namespace FashionZone.BL.DAO.Impl
{
    public class ReturnDAO : BaseDAO, IReturnDAO
    {
        public ReturnDAO(IContextContainer container)
            : base(container)
        {
        }

        private const string DEFAULT_ORDER_EXP = "ID";

        public List<RETURN> Search(RETURN Return, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            var result = Context.RETURN.Include("ORDERS").Include("ORDERS.CUSTOMER").AsQueryable();
            if (Return != null)
            {
                if (Return.ID != 0)
                {
                    result = result.Where(b => b.ID == Return.ID);
                }

                if (!String.IsNullOrEmpty(Return.Comments))
                {
                    result = result.Where(b => b.Comments.Contains(Return.Comments));
                }

                if (Return.OrderID.HasValue)
                {
                    result = result.Where(b => b.OrderID == Return.OrderID.Value);
                }

                if (Return.ORDERS != null)
                {
                    result = result.Where(b => b.ORDERS == Return.ORDERS);
                }

                if (Return.ReceivedDate.HasValue)
                {
                    result = result.Where(b => b.ReceivedDate == Return.ReceivedDate);
                }

                if (Return.RequestDate.HasValue)
                {
                    result = result.Where(b => b.RequestDate == Return.RequestDate);
                }

                if (!string.IsNullOrEmpty(Return.CustomerName))
                {
                    result = result.Where(r => (r.ORDERS.CUSTOMER.Name + " " + r.ORDERS.CUSTOMER.Surname).Contains(Return.CustomerName));
                }

                if (!string.IsNullOrEmpty(Return.VerificationNumber))
                {
                    result = result.Where(r => r.VerificationNumber == Return.VerificationNumber);
                }
            }

            TotalRecords = result.Count();
            if (!String.IsNullOrEmpty(OrderExp))
            {
                if (SortDirection == SortDirection.Ascending)
                    result = result.OrderBy(c => c.ID).ThenBy(c => c.OrderID);
                else
                    result = result.OrderByDescending(c => c.ID).ThenBy(c => c.OrderID);
            }
            else
            {
                var sorter = new GenericSorterCaller<RETURN>();
                result = sorter.Sort(result, string.IsNullOrEmpty(OrderExp) ? DEFAULT_ORDER_EXP : OrderExp, SortDirection);
            }

            // pagination
            return result.Skip(PageIndex * PageSize).Take(PageSize).ToList();
        }

        public List<RETURN> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public RETURN GetById(int id)
        {
            return Context.RETURN.FirstOrDefault(b => b.ID == id);
        }

        public void Insert(RETURN Return)
        {
            Context.RETURN.AddObject(Return);
        }

        public void Update(RETURN upReturn)
        {
            Context.RETURN.Attach(upReturn);
            Context.ObjectStateManager.ChangeObjectState(upReturn, System.Data.EntityState.Modified);
        }

        public void Delete(RETURN delReturn)
        {
            Context.RETURN.Attach(delReturn);
            Context.DeleteObject(delReturn);
        }


        public void DeleteById(int Id)
        {
            Delete(new RETURN() { ID = Id });
        }

        public D_RETURN_MOTIVATION GetAllReturnMotivationById(int idMot)
        {
            return Context.D_RETURN_MOTIVATION.FirstOrDefault(m=>m.ID == idMot);
        }


        public bool ExistVerificationNumber(string VerificationNumber)
        {
            var l = from ret in Context.RETURN
                    where ret.VerificationNumber == VerificationNumber
                    select ret.ID;
            return true && l.Count()!=0;
        }

        public List<D_RETURN_MOTIVATION> GetAllReturnMotivation()
        {
            return Context.D_RETURN_MOTIVATION.ToList();
        }
    }
}
