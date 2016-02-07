using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;

namespace FashionZone.BL.Manager.Impl
{
    class CustomerManager : BaseManager, ICustomerManager
    {
        private ICustomerDAO _customerDAO;
        public CustomerManager(IContextContainer container)
            : base(container)
        {
            _customerDAO = new CustomerDAO(container);
        }

        public CUSTOMER GetByEmail(string Email)
        {
            return _customerDAO.GetByEmail(Email);
        }

        public bool Validate(string Email, string Password)
        {
            return _customerDAO.Validate(Email, Password);
        }

        public bool Validate(int Id, string Password)
        {
            return _customerDAO.Validate(Id, Password);
        }

        public void ChangePassword(string Email, string OldPassword, string NewPasword)
        {
            _customerDAO.ChangePassword(Email, OldPassword, NewPasword);
            Context.SaveChanges();
        }

        public void ResetPassword(CUSTOMER Customer)
        {
            _customerDAO.ResetPassword(Customer);
        }
        
        public List<D_ADDRESS_TYPE> GetAddressTypeList()
        {
            return _customerDAO.GetAddressTypeList();
        }

        public List<D_CITY> GetCities(D_STATE State)
        {
            return _customerDAO.GetCities(State);
        }

        public ADDRESS GetAddressById(int Id)
        {
            return _customerDAO.GetAddressById(Id);
        }

        public List<SHOPPING_CART> GetCart(CUSTOMER Customer)
        {
            return _customerDAO.GetCart(Customer);
        }

        public List<string> GetFullNameWithDateReg(string Name)
        {
            return _customerDAO.GetFullNameWithDateReg(Name);
        }

        public List<CUSTOMER> Search(CUSTOMER Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _customerDAO.Search(Entity, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<CUSTOMER> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _customerDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public CUSTOMER GetById(int id)
        {
            return _customerDAO.GetById(id);
        }

        public void Insert(CUSTOMER Entity)
        {
            _customerDAO.Insert(Entity);
            Context.SaveChanges();
        }

        public void Update(CUSTOMER Customer, bool IgnorePass, bool Attach = true)
        {
            _customerDAO.Update(Customer, IgnorePass, Attach);
            Context.SaveChanges();
        }

        public void Update(CUSTOMER Entity, bool Attach = true)
        {
            _customerDAO.Update(Entity, Attach);
            Context.SaveChanges();
        }

        public void Delete(CUSTOMER Entity)
        {
            _customerDAO.Delete(Entity);
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            _customerDAO.DeleteById(Id);
            Context.SaveChanges();
        }

        public void UpdateAddress(ADDRESS address)
        {
            _customerDAO.UpdateAddress(address);
            Context.SaveChanges();
        }

        public void InsertAddress(ADDRESS address)
        {
            _customerDAO.InsertAddress(address);
            Context.SaveChanges();
        }

        public void DeleteAddress(ADDRESS Address)
        {
            _customerDAO.DeleteAddress(Address);
            Context.SaveChanges();
        }

        public void DeleteAddressById(int Id)
        {
            _customerDAO.DeleteAddressById(Id);
            Context.SaveChanges();
        }


        public ICollection<ADDRESS> GetAddresses(int CustomerID)
        {
            return _customerDAO.GetAddresses(CustomerID);
        }


        public void Update(CUSTOMER Entity)
        {
            Update(Entity, true);
        }

        public List<EMAIL> MailList(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            return _customerDAO.MailList(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }


        public CUSTOMER GetByFBId(string FbId)
        {
            return _customerDAO.GetByFBId(FbId);
        }
    }
}
