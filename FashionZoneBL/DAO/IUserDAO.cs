using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO
{
    public interface IUserDAO : IDAO<USER>
    {
        USER GetByUserName(string UserName);

        /// <summary>
        /// Returns all the roles of a given user
        /// Actually one user can have only one assigned role, to be decided whether more are needed.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        List<ROLE> GetAllUserRoles(string UserName);

        /// <summary>
        /// Updates all the fields of a user
        /// Fields not set will anyway override existing values
        /// </summary>
        /// <param name="User"></param>
        void Update(USER User, bool IgnorePassword);

        /// <summary>
        /// Validates user by checking the user name and password
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password">The hash of the password</param>
        /// <returns></returns>
        bool Validate(string UserName, string Password);
        bool ChangePassword(string UserName, string OldPassword, string NewPassword);        
        string ResetPassword(USER User, string Answer);
        ROLE GetRoleByName(string RoleName);
        List<ROLE> GetAllRoles();
    }
}
