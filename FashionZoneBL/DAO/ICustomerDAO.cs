using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;

namespace FashionZone.BL.DAO
{
    public interface ICustomerDAO : IDAO<CUSTOMER>
    {
        CUSTOMER GetByEmail(string Email);
        CUSTOMER GetByFBId(string FbId);
        void Update(CUSTOMER Customer, bool IgnorePass, bool Attach = true);
        void Update(CUSTOMER Customer, bool Attach = true);
        bool Validate(string Email, string Password);
        bool Validate(int Id, string Password);
        void ChangePassword(string Email, string OldPassword, string NewPasword);
        void ResetPassword(CUSTOMER Customer);
        void UpdateAddress(ADDRESS address);
        void InsertAddress(ADDRESS address);
        List<D_ADDRESS_TYPE> GetAddressTypeList();
        List<D_CITY> GetCities(D_STATE State);
        ADDRESS GetAddressById(int Id);
        void DeleteAddress(ADDRESS Address);
        void DeleteAddressById(int Id);
        ICollection<ADDRESS> GetAddresses(int CustomerID);
        List<SHOPPING_CART> GetCart(CUSTOMER Customer);
        List<string> GetFullNameWithDateReg(string Name);
        List<EMAIL> MailList(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection);
    }
}
