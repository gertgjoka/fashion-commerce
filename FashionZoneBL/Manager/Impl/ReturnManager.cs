using System.Collections.Generic;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager.Impl
{
    class ReturnManager : BaseManager, IReturnManager
    {
        private IReturnDAO _returnDAO;

        public ReturnManager(IContextContainer container)
            : base(container)
        {
            _returnDAO = new ReturnDAO(container);
        }

        public List<RETURN> Search(RETURN Return, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _returnDAO.Search(Return, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<RETURN> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _returnDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public RETURN GetById(int id)
        {
            return _returnDAO.GetById(id);
        }

        public void Insert(RETURN Return)
        {
            _returnDAO.Insert(Return);
            Context.SaveChanges();
        }

        public void Update(RETURN Return)
        {
            _returnDAO.Update(Return);
            Context.SaveChanges();
        }

        public void Delete(RETURN Return)
        {
            _returnDAO.Delete(Return);
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            _returnDAO.DeleteById(Id);
            Context.SaveChanges();
        }

        public D_RETURN_MOTIVATION GetAllReturnMotivationById(int IdMot)
        {
            return _returnDAO.GetAllReturnMotivationById(IdMot);
        }

        public List<D_RETURN_MOTIVATION> GetAllReturnMotivation()
        {
            return _returnDAO.GetAllReturnMotivation();
        }

        public bool ExistVerificationNumber(string VerificationNumber)
        {
            return _returnDAO.ExistVerificationNumber(VerificationNumber);
        }
    }
}
