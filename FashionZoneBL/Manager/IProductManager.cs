using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager
{
    public interface IProductManager : IManager<PRODUCT>
    {
        List<PROD_IMAGES> GetImages(PRODUCT Product);
        List<PROD_IMAGES> GetImages(int ProductId);
        void DeleteImageById(int ImageId);
        List<PRODUCT_ATTRIBUTE> GetProductAttributes(int ProductID);
        void AddProductAttribute(PRODUCT_ATTRIBUTE Attribute);
        void UpdateProductAttribute(PRODUCT_ATTRIBUTE Attribute);
        void UpdateImage(PROD_IMAGES Image);
        void InsertImage(PROD_IMAGES Image);
        List<FZAttributeAvailability> GetProductAttributeValues(int ProductID);
        PRODUCT_ATTRIBUTE GetProductAvailability(int AttributeValueID, out List<int> AvailabilityList, int? AlreadyInCart = null);
        List<PRODUCT> GetProductsByCampaign(int CampaignID, int? CategoryID);
        void Update(PRODUCT Product, bool Attach = true);

        void ChangeApproval(PRODUCT Product);

        bool? GetApprovalStatus(int ProductId);

        void ImportProducts(int CampaignId, string Path);
    }
}
