using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace FashionZone.Admin.Utils.Security
{
    public class FZRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                return "FashionZone";
            }
            set
            {
            }
        }



        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var ctx = ApplicationContext.Current.Users;

            return ctx.GetAllUserRoles(username).Select(x => x.Name).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var ctx = ApplicationContext.Current.Users;
            ROLE role = ctx.GetRoleByName(roleName);

            return (ctx.GetAllUserRoles(username).Contains(role));
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}