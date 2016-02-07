using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace FashionZone.FE.CustomControl
{
    public partial class campaignLogosUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                bindCampaigns();
            }
        }

        protected void bindCampaigns()
        {
            var ctx = ApplicationContext.Current.Campaigns;
            List<CAMPAIGN> campaigns = ctx.GetActiveCampaigns(true).OrderByDescending(c => c.StartDate).ToList();
            rptCampaign.DataSource = campaigns;
            rptCampaign.DataBind();
        }
    }
}