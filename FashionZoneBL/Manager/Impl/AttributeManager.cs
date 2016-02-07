using System;
using System.Collections.Generic;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager.Impl
{
    class AttributeManager : BaseManager, IAttributeManager
    {
        private IAttributeDAO _attributeDAO;

        public AttributeManager(IContextContainer container)
            : base(container)
        {
            _attributeDAO = new AttributeDAO(container);
        }

        public List<D_ATTRIBUTE> Search(D_ATTRIBUTE Entity, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _attributeDAO.Search(Entity, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<D_ATTRIBUTE> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _attributeDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public D_ATTRIBUTE GetById(int id)
        {
            return _attributeDAO.GetById(id);
        }

        public D_ATTRIBUTE_VALUE GetValueById(int Id)
        {
            return _attributeDAO.GetValueById(Id);
        }

        public List<D_ATTRIBUTE> GetAll()
        {
            return _attributeDAO.GetAll();
        }

        public List<D_ATTRIBUTE_VALUE> GetAttributeValues(int AttributeId)
        {
            return _attributeDAO.GetAttributeValues(AttributeId);
        }

        
        public void Insert(D_ATTRIBUTE Entity)
        {
            _attributeDAO.Insert(Entity);
            Context.SaveChanges();
        }

        public void Update(D_ATTRIBUTE Entity)
        {
            _attributeDAO.Update(Entity);
            Context.SaveChanges();
        }

        public void Delete(D_ATTRIBUTE Entity)
        {
            _attributeDAO.Delete(Entity);
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            _attributeDAO.DeleteById(Id);
            Context.SaveChanges();
        }
        public void UpdateValue(D_ATTRIBUTE_VALUE Value)
        {
            _attributeDAO.UpdateValue(Value);
            Context.SaveChanges();
        }

        public void InsertValue(D_ATTRIBUTE_VALUE Value)
        {
            _attributeDAO.InsertValue(Value);
            Context.SaveChanges();
        }

        public void DeleteValueById(int Id)
        {
            _attributeDAO.DeleteValueById(Id);
            Context.SaveChanges();
        }
    }
}
