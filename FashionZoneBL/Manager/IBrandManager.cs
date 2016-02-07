using System.Collections.Generic;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager
{
    public interface IBrandManager : IManager<BRAND>
    {
        /// <summary>
        /// Get a list of Brands.
        /// </summary>
        /// <param name="length">How many elemens extract.</param>
        /// <returns>List of TOP "length" BRAND.</returns> 
        List<BRAND> GetBrandTopList(int length);

        /// <summary>
        /// Get a list of Brands with ACTIVE Campaign. 
        /// </summary>
        /// <returns>List of BRAND with ACTIVE Campaign.</returns>
        List<BRAND> GetBrandWithActiveCampaign();

        List<BRAND> GetBrandWithRecentCampaign();
    }
}
