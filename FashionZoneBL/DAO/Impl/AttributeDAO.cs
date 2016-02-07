using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;
using System.Data.Objects;
using System.Data;

namespace FashionZone.BL.DAO.Impl
{
    public class AttributeDAO : BaseDAO, IAttributeDAO
    {
        public AttributeDAO(IContextContainer container)
            : base(container)
        {
        }

        public List<D_ATTRIBUTE> Search(D_ATTRIBUTE Attribute, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            var result = Context.D_ATTRIBUTE.AsQueryable();
            if (Attribute != null)
            {
                if (!String.IsNullOrWhiteSpace(Attribute.Name))
                {
                    result = result.Where(p => p.Name.Contains(Attribute.Name));
                }
            }

            TotalRecords = result.Count();

            GenericSorterCaller<D_ATTRIBUTE> sorter = new GenericSorterCaller<D_ATTRIBUTE>();
            result = sorter.Sort(result, String.IsNullOrEmpty(OrderExp) ? "Name" : OrderExp, SortDirection);

            result = result.Skip(PageIndex * PageSize).Take(PageSize);
            var SQL = (result as ObjectQuery).ToTraceString();

            return result.ToList();
        }

        public List<D_ATTRIBUTE> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public D_ATTRIBUTE GetById(int Id)
        {
            D_ATTRIBUTE attribute = Context.D_ATTRIBUTE.Include("D_ATTRIBUTE_VALUE").Where(a => a.ID == Id).FirstOrDefault();
            return attribute;
        }

        public void Insert(D_ATTRIBUTE Attribute)
        {
            Context.D_ATTRIBUTE.AddObject(Attribute);
            //Context.SaveChanges();
        }

        public void Delete(D_ATTRIBUTE Attribute)
        {
            Context.D_ATTRIBUTE.Attach(Attribute);
            Context.D_ATTRIBUTE.DeleteObject(Attribute);
            //Context.SaveChanges();
        }

        public void Update(D_ATTRIBUTE Attribute)
        {
            Context.D_ATTRIBUTE.Attach(Attribute);
            Context.ObjectStateManager.ChangeObjectState(Attribute, EntityState.Modified);
            //Context.SaveChanges();
        }

        public void UpdateValue(D_ATTRIBUTE_VALUE Value)
        {
            Context.D_ATTRIBUTE_VALUE.Attach(Value);
            Context.ObjectStateManager.ChangeObjectState(Value, EntityState.Modified);
            //Context.SaveChanges();
        }

        public void InsertValue(D_ATTRIBUTE_VALUE Value)
        {
            Context.D_ATTRIBUTE_VALUE.AddObject(Value);
            //Context.SaveChanges();
        }

        public void DeleteValueById(int Id)
        {
            D_ATTRIBUTE_VALUE value = new D_ATTRIBUTE_VALUE() { ID = Id };
            Context.D_ATTRIBUTE_VALUE.Attach(value);
            Context.D_ATTRIBUTE_VALUE.DeleteObject(value);
            //Context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            D_ATTRIBUTE obj = new D_ATTRIBUTE() { ID = id };
            Delete(obj);
        }

        public D_ATTRIBUTE_VALUE GetValueById(int Id)
        {
            D_ATTRIBUTE_VALUE val = Context.D_ATTRIBUTE_VALUE.Where(a => a.ID == Id).FirstOrDefault();
            return val;
        }
        
        public List<D_ATTRIBUTE> GetAll()
        {
            List<D_ATTRIBUTE> list = Context.D_ATTRIBUTE.OrderBy(a => a.Name).ToList();
            return list;
        }
        
        public List<D_ATTRIBUTE_VALUE> GetAttributeValues(int AttributeId)
        {

            List<D_ATTRIBUTE_VALUE> values = Context.D_ATTRIBUTE_VALUE.Where(v => v.AttributeID == AttributeId).OrderBy(v => v.Value).ToList();
            return values;
        }
    }
}
