using System;
using System.Collections.Generic;
using System.Linq;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;
using System.Data;
using System.Data.Objects;

namespace FashionZone.BL.DAO.Impl
{
    public class CustomerDAO : BaseDAO, ICustomerDAO
    {
        public CustomerDAO(IContextContainer container)
            : base(container)
        {
        }

        public CUSTOMER GetById(int Id)
        {
            var customer = Context.CUSTOMER.Include("ADDRESS").Include("ADDRESS.D_CITY").Where(c => c.ID == Id).FirstOrDefault();
            Context.Refresh(RefreshMode.StoreWins, customer.ADDRESS);
            return customer;
        }

        public CUSTOMER GetByEmail(string Email)
        {
            if (!String.IsNullOrWhiteSpace(Email))
            {
                var customer = Context.CUSTOMER.Include("ADDRESS").Where(c => c.Email.ToLower().Equals(Email.ToLower())).FirstOrDefault();
                return customer;
            }
            else
            {
                return null;
            }
        }

        public List<CUSTOMER> Search(CUSTOMER Customer, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            var result = Context.CUSTOMER.AsQueryable();
            if (Customer != null)
            {
                if (!String.IsNullOrWhiteSpace(Customer.Name))
                {
                    result = result.Where(c => (c.Name + " " + c.Surname).Contains(Customer.Name));
                }

                if (!String.IsNullOrWhiteSpace(Customer.Email))
                {
                    result = result.Where(c => c.Email.ToLower().Contains(Customer.Email.ToLower()));
                }

                if (Customer.Newsletter.HasValue)
                {
                    result = result.Where(c => c.Newsletter == Customer.Newsletter.Value);
                }
            }

            TotalRecords = result.Count();

            GenericSorterCaller<CUSTOMER> sorter = new GenericSorterCaller<CUSTOMER>();
            result = sorter.Sort(result, String.IsNullOrEmpty(OrderExp) ? "ID" : OrderExp, SortDirection);

            return result.Skip(PageIndex * PageSize).Take(PageSize).ToList();
        }

        public List<EMAIL> MailList(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            var result = Context.EMAIL.OrderBy(e => e.Email1); ;

            TotalRecords = result.Count();

            return result.Skip(PageIndex * PageSize).Take(PageSize).ToList();
        }


        public List<CUSTOMER> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public void Insert(CUSTOMER Customer)
        {
            Customer.Email = Customer.Email.ToLower();
            Context.CUSTOMER.AddObject(Customer);
        }

        public void Update(CUSTOMER Customer, bool Attach = true)
        {
            Update(Customer, true, Attach);
        }

        public void Update(CUSTOMER Customer, bool IgnorePassword, bool Attach = true)
        {
            if (Attach)
            {
                Context.CUSTOMER.Attach(Customer);
            }
            if (IgnorePassword)
            {
                var entry = Context.ObjectStateManager.GetObjectStateEntry(Customer);
                entry.SetModifiedProperty("Name");
                entry.SetModifiedProperty("Surname");

                if (!String.IsNullOrWhiteSpace(Customer.Email))
                {
                    entry.SetModifiedProperty("Email");
                }
                entry.SetModifiedProperty("Gender");

                if (Customer.BirthDate.HasValue)
                {
                    entry.SetModifiedProperty("BirthDate");
                }
                entry.SetModifiedProperty("Active");

                if (Customer.Newsletter.HasValue)
                {
                    entry.SetModifiedProperty("Newsletter");
                }
                entry.SetModifiedProperty("Telephone");
                entry.SetModifiedProperty("Mobile");
            }
            else
            {
                Context.ObjectStateManager.ChangeObjectState(Customer, EntityState.Modified);
            }
        }

        public bool Validate(string Email, string Password)
        {
            return Context.CUSTOMER.Count(c => c.Email.Equals(Email.ToLower()) && c.Password.Equals(Password)) == 1;
        }

        public bool Validate(int Id, string Password)
        {
            return Context.CUSTOMER.Count(c => c.ID == Id && c.Password.Equals(Password)) == 1;
        }

        public void Delete(CUSTOMER Customer)
        {
            Context.CUSTOMER.Attach(Customer);
            Context.CUSTOMER.DeleteObject(Customer);
        }

        public void DeleteById(int id)
        {
            var obj = new CUSTOMER() { ID = id };
            Delete(obj);
        }

        public void ChangePassword(string Email, string OldPassword, string NewPasword)
        {
            if (Validate(Email, OldPassword))
            {
                CUSTOMER customer = GetByEmail(Email);
                customer.Password = NewPasword;
                //Context.CUSTOMER.Attach(customer);
                var entry = Context.ObjectStateManager.GetObjectStateEntry(customer);
                entry.SetModifiedProperty("Password");
            }
        }

        public void ResetPassword(CUSTOMER Customer)
        {
            // TODO reset password logic
            throw new NotImplementedException();
        }

        public void UpdateAddress(ADDRESS address)
        {
            Context.ADDRESS.AddObject(address);
            Context.ObjectStateManager.ChangeObjectState(address, EntityState.Modified);
        }

        public void InsertAddress(ADDRESS address)
        {
            Context.ADDRESS.AddObject(address);
        }

        public List<D_ADDRESS_TYPE> GetAddressTypeList()
        {
            return Context.D_ADDRESS_TYPE.ToList();
        }

        public List<D_CITY> GetCities(D_STATE State)
        {
            List<D_CITY> cities;
            if (State != null)
            {
                cities = Context.D_CITY.Where(x => x.StateID == State.ID).ToList();
            }
            else
            {
                cities = Context.D_CITY.ToList();
            }
            return cities;
        }

        public ADDRESS GetAddressById(int Id)
        {
            var addr = Context.ADDRESS.Include("D_CITY").Where(c => c.ID == Id).FirstOrDefault();
            return addr;
        }


        public void DeleteAddress(ADDRESS Address)
        {
            Context.ADDRESS.Attach(Address);
            Context.ADDRESS.DeleteObject(Address);
        }

        public void DeleteAddressById(int id)
        {
            var obj = new ADDRESS() { ID = id };
            DeleteAddress(obj);
        }

        public List<SHOPPING_CART> GetCart(CUSTOMER Customer)
        {
            var shopping = Context.SHOPPING_CART.Where(x => x.CustomerID == Customer.ID).ToList();
            return shopping;
        }


        public List<string> GetFullNameWithDateReg(string Name)
        {
            //var c = Context.CUSTOMER.ToList();
            //return c.Select(customer => customer.Name + " " + customer.Surname + "-" + customer.BirthDate + "-" + customer.ID).ToList();
            return Context.CUSTOMER.Where(n => n.Name.Contains(Name) || n.Surname.Contains(Name)).ToList()
                .Select(customer => customer.BirthDate != null ? customer.Name + " " + customer.Surname + "-" + customer.BirthDate.Value.ToString("dd/MM/yyyy") + "-" + customer.ID
                : customer.Name + " " + customer.Surname + "- NO DATE-" + customer.ID).ToList();
        }


        public ICollection<ADDRESS> GetAddresses(int CustomerID)
        {
            return Context.ADDRESS.Where(a => a.CustomerID == CustomerID).ToList();
        }


        public void Update(CUSTOMER Entity)
        {
            Update(Entity, true);
        }


        public CUSTOMER GetByFBId(string FbId)
        {
            if (!String.IsNullOrWhiteSpace(FbId))
            {
                var customer = Context.CUSTOMER.Where(c => c.FBId.Equals(FbId)).FirstOrDefault();
                return customer;
            }
            else
            {
                return null;
            }
        }
    }
}
