using System.Collections.Generic;
using FashionZone.BL.DAO;
using FashionZone.BL.DAO.Impl;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager.Impl
{
    public class CampaignManager : BaseManager, ICampaignManager
    {
        private ICampaignDAO _campaignDAO;
        public CampaignManager(IContextContainer container)
            : base(container)
        {
            _campaignDAO = new CampaignDAO(container);
        }

        public List<CAMPAIGN> GetCampaignTopList(int length)
        {
            return _campaignDAO.GetCampaignTopList(length);
        }

        public List<CAMPAIGN> GetActiveCampaignByBrand(int brandId)
        {
            return _campaignDAO.GetActiveCampaignByBrand(brandId);
        }

        public List<CAMPAIGN> GetActiveCampaigns(bool? Approved)
        {
            return _campaignDAO.GetActiveCampaigns(Approved);
        }

        public List<CAMPAIGN> Search(CAMPAIGN Campaign, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _campaignDAO.Search(Campaign, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public List<CAMPAIGN> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, Util.SortDirection SortDirection)
        {
            return _campaignDAO.GetAll(PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        public CAMPAIGN GetById(int id)
        {
            return _campaignDAO.GetById(id);
        }

        public void Insert(CAMPAIGN Campaign)
        {
            _campaignDAO.Insert(Campaign);
            Context.SaveChanges();
        }

        public void Update(CAMPAIGN Campaign, bool Attach = true)
        {
            _campaignDAO.Update(Campaign, Attach);
            Context.SaveChanges();
        }

        public void Delete(CAMPAIGN Campaign)
        {
            _campaignDAO.Delete(Campaign);
            Context.SaveChanges();
        }

        public void DeleteById(int Id)
        {
            _campaignDAO.DeleteById(Id);
            Context.SaveChanges();
        }


        public int? ActivateActualCampaigns()
        {
            return _campaignDAO.ActivateActualCampaigns();
        }

        public int? DeactivatePastCampaigns()
        {
            return _campaignDAO.DeactivatePastCampaigns();
        }

        public void ChangeApproval(CAMPAIGN Campaign)
        {
            _campaignDAO.ChangeApproval(Campaign);
        }


        public bool? GetCampaignApprovalStatus(int CampaignId)
        {
            return _campaignDAO.GetCampaignApprovalStatus(CampaignId);
        }

        public List<CAMPAIGN> GetCampaignsByCategoryName(string CategoryName, LanguageEnum Language)
        {
            return _campaignDAO.GetCampaignsByCategoryName(CategoryName, Language);
        }

        public void Update(CAMPAIGN Entity)
        {
            Update(Entity, true);
        }
    }
}
