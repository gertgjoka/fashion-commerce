using System;
using System.Collections.Generic;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;


namespace FashionZone.BL.Manager.Impl
{
    public class InvitationManager : BaseManager, IInvitationManager
    {
        private IInvitationDAO _invititationDAO;

        public InvitationManager(IContextContainer container)
            : base(container)
        {
            _invititationDAO = new InvitationDAO (container);
        }

        public List<INVITATION> Search(INVITATION Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _invititationDAO.Search(Entity, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<INVITATION> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _invititationDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public INVITATION GetById(int id)
        {
            return _invititationDAO.GetById(id);
        }

        public void Insert(INVITATION Entity)
        {
            _invititationDAO.Insert(Entity);
            Context.SaveChanges();
        }

        public void Update(INVITATION Entity)
        {
            _invititationDAO.Update(Entity);
            Context.SaveChanges();
        }

        public void Delete(INVITATION Entity)
        {
            _invititationDAO.Delete(Entity);
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            _invititationDAO.DeleteById(Id);
            Context.SaveChanges();
        }

        public List<INVITATION> GetAllInvitationOfCustomer(int idCust)
        {
           return _invititationDAO.GetAllInvitationOfCustomer(idCust);
        }
    }
}
