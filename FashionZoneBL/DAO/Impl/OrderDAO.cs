using System;
using System.Collections.Generic;
using System.Linq;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;

namespace FashionZone.BL.DAO.Impl
{
    public class OrderDAO : BaseDAO, IOrderDAO
    {
        public OrderDAO(IContextContainer container)
            : base(container)
        {
        }

        public ORDERS GetById(int id)
        {
            var order = Context.ORDERS.Include("CUSTOMER").Include("ORDER_DETAIL").Where(c => c.ID == id).FirstOrDefault();
            return order;
        }

        public List<ORDERS> GetOrdersTopList(int length)
        {
            var orders = Context.ORDERS.Take(length).ToList();
            return orders;
        }

        public List<ORDERS> Search(ORDERS Order, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            var result = Context.ORDERS.Include("CUSTOMER").Include("D_ORDER_STATUS").AsQueryable();

            if (Order != null)
            {
                if (!String.IsNullOrWhiteSpace(Order.CustomerName))
                {
                    result = result.Where(o => (o.CUSTOMER.Name + " " + o.CUSTOMER.Surname).Contains(Order.CustomerName));
                }

                if (Order.ID != 0)
                {
                    result = result.Where(o => o.ID == Order.ID);
                }

                if (Order.SearchStartDate.HasValue)
                {
                    result = result.Where(o => o.DateCreated >= Order.SearchStartDate.Value);
                }

                if (Order.SearchEndDate.HasValue)
                {
                    result = result.Where(o => o.DateCreated <= Order.SearchEndDate.Value);
                }

                if (Order.Status.HasValue)
                {
                    result = result.Where(o => o.Status == Order.Status.Value);
                }
            }

            TotalRecords = result.Count();

            if (!String.IsNullOrEmpty(OrderExp) && OrderExp.Equals("CustomerName"))
            {
                if (SortDirection == SortDirection.Ascending)
                    result = result.OrderBy(c => c.CUSTOMER.Name).ThenBy(c => c.CUSTOMER.Surname);
                else
                    result = result.OrderByDescending(c => c.CUSTOMER.Name).ThenBy(c => c.CUSTOMER.Surname);
            }
            else
            {
                GenericSorterCaller<ORDERS> sorter = new GenericSorterCaller<ORDERS>();
                result = sorter.Sort(result, String.IsNullOrEmpty(OrderExp) ? "ID" : OrderExp, SortDirection);
            }
            // pagination
            return result.Skip(PageIndex * PageSize).Take(PageSize).ToList();
        }

        public List<ORDERS> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public void Insert(ORDERS newOrder)
        {
            Context.ORDERS.AddObject(newOrder);
        }

        public void Update(ORDERS upOrder)
        {
            Update(upOrder, true);
        }

        public void Delete(ORDERS delOrder)
        {
            Context.ORDERS.Attach(delOrder);
            Context.DeleteObject(delOrder);
        }

        public void DeleteById(int id)
        {
            ORDERS obj = new ORDERS() { ID = id };
            Delete(obj);
        }


        public void RemoveBonus(ORDER_BONUS Bonus)
        {
            Context.ORDER_BONUS.Attach(Bonus);
            Context.DeleteObject(Bonus);
        }


        public List<ORDER_BONUS> GetOrderBonuses(ORDERS Order)
        {
            return Context.ORDER_BONUS.Where(b => b.OrderID == Order.ID).ToList();
        }


        public List<ORDER_BONUS> SearchOrderBonuses(int BonusID, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            var result = Context.ORDER_BONUS.AsQueryable().Where(b => b.BonusID == BonusID);

            TotalRecords = result.Count();

            if (!String.IsNullOrEmpty(OrderExp) && OrderExp.Equals("Date"))
            {
                if (SortDirection == SortDirection.Ascending)
                    result = result.OrderBy(c => c.ORDERS.DateCreated);
                else
                    result = result.OrderByDescending(c => c.ORDERS.DateCreated);
            }
            else
            {
                GenericSorterCaller<ORDER_BONUS> sorter = new GenericSorterCaller<ORDER_BONUS>();
                result = sorter.Sort(result, String.IsNullOrEmpty(OrderExp) ? "OrderID" : OrderExp, SortDirection);
            }
            // pagination
            return result.Skip(PageIndex * PageSize).Take(PageSize).ToList();
        }
        public void InsertDetail(ORDER_DETAIL Detail)
        {
            Context.ORDER_DETAIL.AddObject(Detail);
        }


        public List<ORDERS> GetOrdersForReturn(int idCustomer, DateTime validDate)
        {
            //var result = Context.ORDERS.Where(o => o.CustomerID == idCustomer && o.DateCreated >= validDate).ToList();
            var result =
                Context.ORDERS.Where(
                    o =>
                    o.CustomerID == idCustomer && o.DateCreated >= validDate &&
                    !(Context.RETURN.Select(ret => ret.OrderID).Contains(o.ID))).ToList();
            return result;
        }


        public List<ORDER_NOTES> GetNotes(int OrderId)
        {
            var notes = Context.ORDER_NOTES.Where(n => n.OrderID == OrderId).OrderByDescending(n => n.CreatedOn).ToList();
            return (List<ORDER_NOTES>)notes;
        }


        public void InsertNote(ORDER_NOTES Note)
        {
            Context.ORDER_NOTES.AddObject(Note);
        }

        public void DeleteNote(int NoteId)
        {
            ORDER_NOTES note = new ORDER_NOTES() { ID = NoteId };
            Context.ORDER_NOTES.Attach(note);
            Context.ORDER_NOTES.DeleteObject(note);
        }


        public List<ORDER_DETAIL> GetDetails(int OrderID)
        {
            return Context.ORDER_DETAIL.Include("PRODUCT_ATTRIBUTE").Include("PRODUCT_ATTRIBUTE.PRODUCT").Include("PRODUCT_ATTRIBUTE.D_ATTRIBUTE_VALUE").Include("CAMPAIGN").Where(d => d.OrderID == OrderID).ToList();
        }

        public string GetOrderCampaigns(int OrderId)
        {
            return Context.GetOrderCampaigns(OrderId).First();
        }


        public void Update(ORDERS Order, bool Attach = true)
        {
            if (Attach)
            {
                Context.ORDERS.Attach(Order);
            }
            var entry = Context.ObjectStateManager.GetObjectStateEntry(Order);
            // excluding datecreated from the updatable fields
            entry.SetModifiedProperty("DateShipped");
            entry.SetModifiedProperty("Verified");
            entry.SetModifiedProperty("Completed");
            entry.SetModifiedProperty("Canceled");
            entry.SetModifiedProperty("Comments");
            entry.SetModifiedProperty("ShippingID");
            entry.SetModifiedProperty("ShippingAddress");
            entry.SetModifiedProperty("BillingAddress");
            entry.SetModifiedProperty("Status");
            entry.SetModifiedProperty("ShippingDetails");
            entry.SetModifiedProperty("TrackingNumber");
            entry.SetModifiedProperty("DateDelivered");
        }
    }
}
