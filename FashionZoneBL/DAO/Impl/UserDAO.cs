using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Objects;
using System.Data;
using FashionZone.BL.Util;

namespace FashionZone.BL.DAO.Impl
{
    public class UserDAO : BaseDAO, IUserDAO
    {
        public UserDAO(IContextContainer container)
            : base(container)
        {
        }

        public USER GetById(int Id)
        {
                var singleUser = Context.USER.Where(us => us.ID == Id).FirstOrDefault();
                return singleUser;
        }

        public USER GetByUserName(string UserName)
        {
            if (!String.IsNullOrEmpty(UserName))
            {
                    return Context.USER.Where(x => x.Login.Equals(UserName)).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public List<USER> Search(USER User, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
                var result = Context.USER.AsQueryable();
                if (User != null)
                {
                    if (!String.IsNullOrWhiteSpace(User.Email))
                    {
                        result = result.Where(o => o.Email.Contains(User.Email));
                    }
                    if (!String.IsNullOrWhiteSpace(User.Login))
                    {
                        result = result.Where(o => o.Login.Contains(User.Login));
                    }
                    if (!String.IsNullOrWhiteSpace(User.Name))
                    {
                        result = result.Where(o => o.Name.Contains(User.Name));
                    }
                    if (User.RoleID.HasValue)
                    {
                        result = result.Where(o => o.RoleID == User.RoleID.Value);
                    }

                    if (User.Enabled)
                    {
                        result = result.Where(o => o.Enabled == User.Enabled);
                    }
                }

                TotalRecords = result.Count();

                GenericSorterCaller<USER> sorter = new GenericSorterCaller<USER>();
                result = sorter.Sort(result, string.IsNullOrEmpty(OrderExp) ? "ID" : OrderExp, SortDirection);

                // pagination
                return result.Skip( PageIndex * PageSize).Take(PageSize).ToList();
        }

        public List<USER> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

       
        public List<ROLE> GetAllUserRoles(string UserName)
        {
                List<ROLE> roles = new List<ROLE>();
                USER user = Context.USER.Where(x => x.Login.Equals(UserName)).FirstOrDefault();
                roles.Add(user.ROLE);
                return roles;
        }

      
        public void Insert(USER User)
        {
                Context.USER.AddObject(User);
                //Context.SaveChanges();
        }

        public void Update(USER User, bool IgnorePassword)
        {
                Context.USER.Attach(User);
                if (IgnorePassword)
                {
                    var entry = Context.ObjectStateManager.GetObjectStateEntry(User);
                    entry.SetModifiedProperty("Name");
                    entry.SetModifiedProperty("Login");
                    entry.SetModifiedProperty("Email");
                    entry.SetModifiedProperty("RoleID");
                    entry.SetModifiedProperty("Enabled");
                }
                else
                {
                    Context.ObjectStateManager.ChangeObjectState(User, EntityState.Modified);
                }
                //Context.SaveChanges();
        }

        public void Update(USER User)
        {
            Update(User, true);
        }

        public void Delete(USER User)
        {
                Context.USER.Attach(User);
                Context.USER.DeleteObject(User);
                //Context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            USER obj = new USER() { ID = id };
            Delete(obj);
        }
        
       
        public bool Validate(string UserName, String Password)
        {
                return Context.USER.Count(user => user.Login.ToLower().Equals(UserName) && user.Password.Equals(Password)) == 1;
        }

        public bool ChangePassword(string UserName, String OldPassword, String NewPassword)
        {
                if (Validate(UserName, OldPassword))
                {
                    USER user = GetByUserName(UserName);
                    user.Password = NewPassword;
                    //Context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public string ResetPassword(USER user, string Answer)
        {
            // TODO reset password logic
            return String.Empty;
        }

        public ROLE GetRoleByName(string RoleName)
        {
                return Context.ROLE.Where(x => x.Name.Equals(RoleName)).FirstOrDefault();
        }

        public List<ROLE> GetAllRoles()
        {
                return Context.ROLE.ToList();
        }
    }
}
