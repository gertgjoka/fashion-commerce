using System.Collections.Generic;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager
{
    public interface IAttributeManager : IManager<D_ATTRIBUTE>
    {
        D_ATTRIBUTE_VALUE GetValueById(int Id);
        void UpdateValue(D_ATTRIBUTE_VALUE Value);
        void InsertValue(D_ATTRIBUTE_VALUE Value);
        void DeleteValueById(int Id);
        List<D_ATTRIBUTE> GetAll();
        List<D_ATTRIBUTE_VALUE> GetAttributeValues(int AttributeId);
    }
}
