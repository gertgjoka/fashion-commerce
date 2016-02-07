using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.FE.Utils;
using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace FashionZone.FE.CustomControl
{
    public partial class MainMenuItem : System.Web.UI.UserControl
    {
        public string ItemName
        {
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<CAMPAIGN> campaigns = null;
                if (Session["Culture"] == null || Session["Culture"].ToString() == "sq-AL")
                {
                    campaigns = ApplicationContext.Current.Campaigns.GetCampaignsByCategoryName(ItemName, LanguageEnum.AL);
                }
                else
                {
                    campaigns = ApplicationContext.Current.Campaigns.GetCampaignsByCategoryName(ItemName, LanguageEnum.EN);
                }

                if (campaigns != null && campaigns.Count > 0)
                {
                    rptBrands.DataSource = campaigns.OrderByDescending(c => c.StartDate);
                    rptBrands.DataBind();
                }
                else
                {
                    pnlBrands.Visible = false;
                    a1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                BasePage.Log(ex, ex.Message, ex.StackTrace, "MainMenuItem page load");
            }
        }
    }
}