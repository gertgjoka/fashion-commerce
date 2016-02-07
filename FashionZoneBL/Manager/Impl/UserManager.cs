using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager.Impl
{
    class UserManager : BaseManager, IUserManager
    {
        private IUserDAO _userDAO;
        public UserManager(IContextContainer container)
            : base(container)
        {
            _userDAO = new UserDAO(container);
        }

        public USER GetByUserName(string UserName)
        {
            return _userDAO.GetByUserName(UserName);
        }

        public List<ROLE> GetAllUserRoles(string UserName)
        {
            return _userDAO.GetAllUserRoles(UserName);
        }

        public void Update(USER User, bool IgnorePassword)
        {
            _userDAO.Update(User, IgnorePassword);
            Context.SaveChanges();
        }

        public bool Validate(string UserName, string Password)
        {
            return _userDAO.Validate(UserName, Password);
        }

        public bool ChangePassword(string UserName, string OldPassword, string NewPassword)
        {
            var esito = _userDAO.ChangePassword(UserName, OldPassword, NewPassword);
            if (esito)
                Context.SaveChanges();
            return esito;
        }

        public string ResetPassword(USER User, string Answer)
        {
            return _userDAO.ResetPassword(User, Answer);
        }

        public ROLE GetRoleByName(string RoleName)
        {
            return _userDAO.GetRoleByName(RoleName);
        }

        public List<ROLE> GetAllRoles()
        {
            return _userDAO.GetAllRoles();
        }

        public List<USER> Search(USER Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _userDAO.Search(Entity, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<USER> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _userDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public USER GetById(int id)
        {
            return _userDAO.GetById(id);
        }

        public void Insert(USER Entity)
        {
            _userDAO.Insert(Entity);
            Context.SaveChanges();
        }

        public void Update(USER Entity)
        {
            _userDAO.Update(Entity);
            Context.SaveChanges();
        }

        public void Delete(USER Entity)
        {
            _userDAO.Delete(Entity);
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            _userDAO.DeleteById(Id);
            Context.SaveChanges();
        }
    }
}
