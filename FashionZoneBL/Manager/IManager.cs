using System;
using System.Collections.Generic;
using FashionZone.BL.Util;

namespace FashionZone.BL.Manager
{
    /// <summary>
    /// Generic interface with common functionality to all Managers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IManager<T>
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
