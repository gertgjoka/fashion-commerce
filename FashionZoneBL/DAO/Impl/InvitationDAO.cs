using System;
using System.Collections.Generic;
using System.Linq;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;

namespace FashionZone.BL.DAO.Impl
{
    public class InvitationDAO : BaseDAO, IInvitationDAO
    {
        private const string DEFAULT_ORDER_EXP = "ID";

        public InvitationDAO(IContextContainer container)
            : base(container)
        {
        }

        public List<INVITATION> Search(INVITATION Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            var result = Context.INVITATION.AsQueryable();
            if (Entity != null)
            {
                if (Entity.ID != 0)
                {
                    result = result.Where(b => b.ID == Entity.ID);
                }

                if (Entity.CustomerID.HasValue)
                {
                    result = result.Where(i => i.CustomerID == Entity.CustomerID);
                }

                if (!String.IsNullOrEmpty(Entity.InvitedMail))
                {
                    result = result.Where(i => i.InvitedMail.Contains(Entity.InvitedMail));
                }

                if (Entity.RegistrationDate != null)
                {
                    result = result.Where(i => i.RegistrationDate == (Entity.RegistrationDate));
                }
            }
            TotalRecords = result.Count();

            GenericSorterCaller<INVITATION> sorter = new GenericSorterCaller<INVITATION>();
            result = sorter.Sort(result, string.IsNullOrEmpty(OrderExp) ? DEFAULT_ORDER_EXP : OrderExp, SortDirection);

            // pagination
            return result.Skip(PageIndex * PageSize).Take(PageSize).ToList();
        }

        public List<INVITATION> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public INVITATION GetById(int id)
        {
            var inv = Context.INVITATION.Where(i => i.ID == id).FirstOrDefault();
            return inv;
        }

        public void Insert(INVITATION newInv)
        {
            Context.INVITATION.AddObject(newInv);
        }

        public void Update(INVITATION upInv)
        {
            Context.INVITATION.Attach(upInv);
            Context.ObjectStateManager.ChangeObjectState(upInv, System.Data.EntityState.Modified);
        }

        public void Delete(INVITATION delInv)
        {
            Context.INVITATION.Attach(delInv);
            Context.DeleteObject(delInv);
        }

        public void DeleteById(int Id)
        {
            INVITATION obj = new INVITATION() { ID = Id };
            Delete(obj);
        }

        public List<INVITATION> GetAllInvitationOfCustomer(int idCust)
        {
            var inv = Context.INVITATION.Where(i => i.CustomerID == idCust);
            return inv.ToList();
        }
    }
}
