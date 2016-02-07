using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.DAO
{
    public interface ICampaignDAO : IDAO<CAMPAIGN>
    {
        /// <summary>
        /// Get a list of CAMPAIGN.
        /// </summary>
        /// <param name="length">How many elemens extract.</param>
        /// <returns>List of TOP "length" CAMPAIGN.</returns>
        List<CAMPAIGN> GetCampaignTopList(int length);

        void CopyCampaign(int CampaignId, String NewName);

        /// <summary>
        /// Get a list of ACTIVE Campaign.
        /// </summary>
        /// <param name="length">ID of Brand</param>
        /// <returns>List of CAMPAIGN with ACTIVE Campaign.</returns>
        List<CAMPAIGN> GetActiveCampaignByBrand( int brandID);

        List<CAMPAIGN> GetActiveCampaigns(bool? Approved = null);

        List<CAMPAIGN> GetCampaignsByCategoryName(string CategoryName, LanguageEnum Language);

        void Update(CAMPAIGN Campaign, bool Attach);

        int? ActivateActualCampaigns();
        int? DeactivatePastCampaigns();

        void ChangeApproval(CAMPAIGN Campaign);

        bool? GetCampaignApprovalStatus(int CampaignId);
    }
}
