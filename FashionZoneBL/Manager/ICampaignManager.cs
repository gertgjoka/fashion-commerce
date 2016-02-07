using System.Collections.Generic;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager
{
    public interface ICampaignManager : IManager<CAMPAIGN>
    {
        /// <summary>
        /// Get a list of CAMPAIGN.
        /// </summary>
        /// <param name="length">How many elemens extract.</param>
        /// <returns>List of TOP "length" CAMPAIGN.</returns>
        List<CAMPAIGN> GetCampaignTopList(int length);

        /// <summary>
        /// Get a list of ACTIVE Campaign.
        /// </summary>
        /// <param name="brandId">ID of brand</param>
        /// <returns>List of CAMPAIGN with ACTIVE Campaign.</returns>
        List<CAMPAIGN> GetActiveCampaignByBrand(int brandId);

        List<CAMPAIGN> GetCampaignsByCategoryName(string CategoryName, LanguageEnum Language);
        /// <summary>
        /// Get Active Campaigns
        /// </summary>
        /// <returns>List of Active Campaigns.</returns>
        List<CAMPAIGN> GetActiveCampaigns(bool? Approved = null);

        void Update(CAMPAIGN Campaign, bool Attach = true);
        int? ActivateActualCampaigns();
        int? DeactivatePastCampaigns();

        void ChangeApproval(CAMPAIGN Campaign);

        bool? GetCampaignApprovalStatus(int CampaignId);
    }
}
