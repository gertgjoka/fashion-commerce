using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.BL.Util;

namespace FashionZone.BL.DAO
{
    /// <summary>
    /// Generic interface with common functionality to all DAOs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDAO<T>
    {
        List<T> Search(T Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection);
        List<T> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection);
        T GetById(int Id);
        void Insert(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
        void DeleteById(int Id);
    }
}
