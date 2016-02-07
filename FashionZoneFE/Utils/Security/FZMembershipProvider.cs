using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FashionZone.BL;
using System.Web.Configuration;
using FashionZone.DataLayer.Model;

namespace FashionZone.FE.Utils.Security
{
    public class FZMembershipProvider : MembershipProvider
    {

        public override string ApplicationName
        {
            get { return "FZone.al"; }
            set { }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            oldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(oldPassword, Configuration.PasswordHashMethod);
            newPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, Configuration.PasswordHashMethod);

            var ctx = ApplicationContext.Current.Customers;
           ctx.ChangePassword(username, oldPassword, newPassword);
           return true; 
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, Configuration.PasswordHashMethod);
            var ctx = ApplicationContext.Current.Customers;

            MembershipUser u = new MembershipUser(Name, username, username, email, passwordQuestion, String.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            CUSTOMER customer = new CUSTOMER();
            customer.Password = password;
            customer.Email = username;
            customer.Active = true;
            
            try
            {
                ctx.Insert(customer);
            }
            catch (Exception ex)
            {
                status = MembershipCreateStatus.ProviderError;
                return null;
            }

            status = MembershipCreateStatus.Success;
            return u;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return true; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var ctx = ApplicationContext.Current.Customers;
            CUSTOMER u = ctx.GetByEmail(username);


            return new MembershipUser(this.Name, u.Name, username, u.Email, string.Empty, string.Empty, true, false, u.RegistrationDate.Value, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return GetUser(providerUserKey.ToString(), userIsOnline);
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return String.Empty; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override string ResetPassword(string username, string answer)
        {
            string newPassword = GenerateRandomPassword();

            var ctx = ApplicationContext.Current.Customers;
            var user = ctx.GetByEmail(username);

            if (user == null)
                throw new MembershipPasswordException("Cannot find the specified user");

            user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "MD5"); ;

            try
            {
                ctx.ResetPassword(user);
            }
            catch (Exception ex)
            {
                throw new MembershipPasswordException("Cannot reset the password. See inner exception.", ex);
            }

            return newPassword;
        }

        private string GenerateRandomPassword()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, MinRequiredPasswordLength);
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, Configuration.PasswordHashMethod).ToLower();
            var ctx = ApplicationContext.Current.Customers;
            bool valid = ctx.Validate(username, password);

            if (valid)
            {
                var customer = ApplicationContext.Current.Customers.GetByEmail(username);
                SessionCustomer sC = new SessionCustomer(customer);
                System.Web.HttpContext.Current.Session["CurrentCustomer"] = sC;
            }

            return valid;
        }
    }
}