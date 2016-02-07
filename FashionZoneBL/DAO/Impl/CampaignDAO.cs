using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionZone.DataLayer.Model;
using FashionZone.BL.Util;
using System.Data.Objects;

namespace FashionZone.BL.DAO.Impl
{
    public class CampaignDAO : BaseDAO, ICampaignDAO
    {
        public CampaignDAO(IContextContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Gets the details of the brand identified by id.
        /// </summary>
        /// <param name="id">Identification of the brand.</param>
        /// <returns>A single CAMPAIGN, or a default value if the sequence contains no elements.</returns>
        public CAMPAIGN GetById(int id)
        {
            var campaign = Context.CAMPAIGN.Include("BRAND").Where(c => c.ID == id).FirstOrDefault();
            return campaign;
        }

        /// <summary>
        /// Get a list of CAMPAIGN.
        /// </summary>
        /// <param name="length">How many elemens extract.</param>
        /// <returns>List of TOP "length" CAMPAIGN.</returns>
        public List<CAMPAIGN> GetCampaignTopList(int length)
        {
            var campains = Context.CAMPAIGN.Take(length).ToList();
            return campains;
        }

        public List<CAMPAIGN> Search(CAMPAIGN Campaign, int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            var result = Context.CAMPAIGN.Include("BRAND").AsQueryable();

            if (Campaign != null)
            {
                if (!String.IsNullOrWhiteSpace(Campaign.Name))
                {
                    result = result.Where(c => c.Name.Contains(Campaign.Name));
                }

                if (!String.IsNullOrWhiteSpace(Campaign.BrandName))
                {
                    result = result.Where(c => c.BRAND.Name.Contains(Campaign.Name));
                }

                if (Campaign.SearchStartDate.HasValue)
                {
                    result = result.Where(c => c.StartDate >= Campaign.SearchStartDate.Value);
                }

                if (Campaign.SearchEndDate.HasValue)
                {
                    result = result.Where(c => c.StartDate <= Campaign.SearchEndDate.Value);
                }

                if (Campaign.Active)
                {
                    result = result.Where(c => c.Active);
                }
                if (Campaign.Approved.HasValue)
                {
                    result = result.Where(c => c.Approved == Campaign.Approved.Value);
                }
            }

            TotalRecords = result.Count();

            if (!String.IsNullOrEmpty(OrderExp) && OrderExp.Equals("BrandName"))
            {
                if (SortDirection == SortDirection.Ascending)
                    result = result.OrderBy(c => c.BRAND.ShowName);
                else
                    result = result.OrderByDescending(c => c.BRAND.ShowName);
            }
            else
            {
                var sorter = new GenericSorterCaller<CAMPAIGN>();
                result = sorter.Sort(result, String.IsNullOrEmpty(OrderExp) ? "ID" : OrderExp, SortDirection);
            }
            // pagination
            return result.Skip(PageIndex * PageSize).Take(PageSize).ToList();
        }

        public List<CAMPAIGN> GetAll(int PageSize, int PageIndex, out int TotalRecords, string OrderExp, SortDirection SortDirection)
        {
            return Search(null, PageSize, PageIndex, out TotalRecords, OrderExp, SortDirection);
        }

        /// <summary>
        /// Insert new CAMPAIGN element.
        /// </summary>
        /// <param name="newCampaign">CAMPAIGN object.</param>
        public void Insert(CAMPAIGN newCampaign)
        {
            Context.CAMPAIGN.AddObject(newCampaign);
            //Context.SaveChanges();
        }

        /// <summary>
        /// Update CAMPAIGN elment.
        /// </summary>
        /// <param name="upCampaign">The CAMPAIGN object that will update.</param>
        public void Update(CAMPAIGN upCampaign, bool Attach)
        {
            if (Attach)
            {
                Context.CAMPAIGN.Attach(upCampaign);
            }
            var entry = Context.ObjectStateManager.GetObjectStateEntry(upCampaign);
            entry.SetModifiedProperty("Name");
            entry.SetModifiedProperty("Description");
            entry.SetModifiedProperty("StartDate");
            entry.SetModifiedProperty("EndDate");
            entry.SetModifiedProperty("DeliveryStartDate");
            entry.SetModifiedProperty("DeliveryEndDate");
            entry.SetModifiedProperty("Logo");
            entry.SetModifiedProperty("ImageHome");
            entry.SetModifiedProperty("ImageDetail");
            entry.SetModifiedProperty("ImageListHeader");
            entry.SetModifiedProperty("GenericFile");
        }

        /// <summary>
        /// Delete CAMPAIGN elment.
        /// </summary>
        /// <param name="delCampaign">The CAMPAIGN object that will delete.</param>
        public void Delete(CAMPAIGN delCampaign)
        {
            Context.CAMPAIGN.Attach(delCampaign);
            Context.DeleteObject(delCampaign);
            //Context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            CAMPAIGN obj = new CAMPAIGN() { ID = id };
            Delete(obj);
        }

        /// <summary>
        /// Get a list of ACTIVE Campaign.
        /// </summary>
        /// <param name="length">ID of Brand</param>
        /// <returns>List of CAMPAIGN with ACTIVE Campaign.</returns>
        public List<CAMPAIGN> GetActiveCampaignByBrand(int brandID)
        {
            var campL = Context.CAMPAIGN.Where(c => c.BrandID == brandID).ToList();
            return campL;
        }

        public List<CAMPAIGN> GetActiveCampaigns(bool? Approved)
        {
            var campL = Context.CAMPAIGN.Where(c => c.Active == true);
            if (Approved.HasValue)
                campL = campL.Where(c => c.Approved == Approved.Value);

            return campL.OrderBy(c => c.Name).ToList();
        }

        public int? ActivateActualCampaigns()
        {
            int? result = Context.ActivateActualCampaigns().First();
            return result;
        }

        public int? DeactivatePastCampaigns()
        {
            int? result = Context.DeActivatePastCampaigns().First();
            return result;
        }


        public void ChangeApproval(CAMPAIGN Campaign)
        {
            Context.CAMPAIGN.Attach(Campaign);
            var entry = Context.ObjectStateManager.GetObjectStateEntry(Campaign);
            entry.SetModifiedProperty("Approved");
            entry.SetModifiedProperty("ApprovedBy");
        }

        public bool? GetCampaignApprovalStatus(int CampaignId)
        {
            return Context.GetCampaignApprovalStatus(CampaignId).First();
        }


        public void Update(CAMPAIGN Entity)
        {
            Update(Entity, true);
        }

        public List<CAMPAIGN> GetCampaignsByCategoryName(string CategoryName, LanguageEnum Language)
        {
            if (Language == LanguageEnum.AL)
                return Context.CAMPAIGN.Where(c => c.CATEGORY.Any(cat => cat.Name == CategoryName) && c.Active && c.Approved.Value).ToList();
            else
                return Context.CAMPAIGN.Where(c => c.CATEGORY.Any(cat => cat.NameEng == CategoryName) && c.Active && c.Approved.Value).ToList();
        }


        public void CopyCampaign(int CampaignId, string NewName)
        {
            CAMPAIGN camp = Context.CAMPAIGN.Where(c => c.ID == CampaignId).FirstOrDefault();
            Context.Detach(camp);
            camp.ID = 0;

        }
    }
}
