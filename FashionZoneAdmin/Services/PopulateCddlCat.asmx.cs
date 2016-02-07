using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Services;
using AjaxControlToolkit;
using System.Collections.Specialized;
using FashionZone.DataLayer.Model;
using FashionZone.BL;

namespace FashionZone.Admin.Services
{
    /// <summary>
    /// Summary description for PopulateCddlCat
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PopulateCddlCat : System.Web.Services.WebService
    {

        [WebMethod]
        public CascadingDropDownNameValue[] GetBrand(string knownCategoryValues, string category)
        {
            //test
            List<BRAND> brandL = ApplicationContext.Current.Brands.GetBrandWithRecentCampaign();
            return brandL.Select(singleB => new CascadingDropDownNameValue(singleB.ShowName, singleB.ID.ToString(CultureInfo.InvariantCulture))).ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetCampain(string knownCategoryValues, string category)
        {
            //test
            StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            int brandId;
            if (!kv.ContainsKey("BRAND") || !Int32.TryParse(kv["BRAND"], out brandId))
            {
                return null;
            }
            List<CAMPAIGN> campL = ApplicationContext.Current.Campaigns.GetActiveCampaignByBrand(brandId);
            return campL.Select(singleC => new CascadingDropDownNameValue(singleC.NameWithStartDate, singleC.ID.ToString(CultureInfo.InvariantCulture))).ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetCategory(string knownCategoryValues, string category)
        {
            //test
            StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            int campainId;
            if (!kv.ContainsKey("CAMPAIN") || !Int32.TryParse(kv["CAMPAIN"], out campainId))
            {
                return null;
            }

            List<CATEGORY> campL = ApplicationContext.Current.Categories.GetCategoryListByCampaign(campainId);
            return campL.Select(singleC => new CascadingDropDownNameValue(singleC.Name, singleC.ID.ToString(CultureInfo.InvariantCulture))).ToArray();
        }
    }
}