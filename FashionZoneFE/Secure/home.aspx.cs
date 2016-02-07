using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using System.Web.UI.HtmlControls;

namespace FashionZone.FE
{
    public partial class Home : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var ctx = ApplicationContext.Current.Campaigns;
                List<CAMPAIGN> campaigns = ctx.GetActiveCampaigns(true).OrderByDescending(c => c.StartDate).ToList();
                rptActualCampaigns.CampaignRepeater.DataSource = campaigns;
                rptActualCampaigns.CampaignRepeater.DataBind();

                CAMPAIGN campaign = new CAMPAIGN() { SearchStartDate = DateTime.Now, SearchEndDate = DateTime.Now.AddDays(Configuration.FutureCampaignDays), Approved = true };
                int totalRecords;
                List<CAMPAIGN> futureCampaigns = ctx.Search(campaign, 10, 0, out totalRecords, "StartDate", BL.Util.SortDirection.Ascending);
                if (futureCampaigns.Count > 0)
                {
                    rptFutureCampaigns.CampaignRepeater.DataSource = futureCampaigns;
                    rptFutureCampaigns.CampaignRepeater.DataBind();
                }
                else
                {
                    divFutureDeals.Visible = false;
                }
                base.Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                //TODO complete exception handling
                Log(ex, ex.Message, ex.StackTrace, "Home");
            }
        }
    }
}
