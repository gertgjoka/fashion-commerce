using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Manager
{
    public interface ICategoryManager : IManager<CATEGORY>
    {
        /// <summary>
        /// Get a list of CATEGORY.
        /// </summary>
        /// <param name="length">How many elemens extract.</param>
        /// <returns>List of TOP "length" CATEGORY.</returns>
        List<CATEGORY> GetCategoryTopList(int length);

        /// <summary>
        /// Get a list of CATEGORY by Campaign.
        /// </summary>
        /// <param name="length">Campaign ID</param>
        /// <returns>List of CATEGORY that belong a Campaign.</returns>
        List<CATEGORY> GetCategoryListByCampaign(int campaignId);

        /// <summary>
        /// Get a list of Children CATEGORY.
        /// </summary>
        /// <param name="length">Father Category ID</param>
        /// <returns>List of CATEGORY that belong a Campaign.</returns>
        List<CATEGORY> GetChildrenCategoryList(int categoryId);
    }
}
