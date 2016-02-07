using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.Admin.Utils;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using FashionZone.BL.Util;
using SortDirection = FashionZone.BL.Util.SortDirection;

namespace FashionZone.Admin.Secure.Product
{
    public partial class CampaignView : FZBasePage<CAMPAIGN>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridCampaign.SortExp = "StartDate";
                gridCampaign.SortOrder = SortDirection.Descending;
            }
            base.Page_Load(sender, e, gridCampaign);
            txtDateFrom.Attributes.Add("readonly", "readonly");
            txtDateTo.Attributes.Add("readonly", "readonly");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new CAMPAIGN();

            if (!String.IsNullOrWhiteSpace(txtName.Text))
            {
                SearchObject.Name = txtName.Text;
            }
            else
            {
                SearchObject.Name = null;
            }


            if (!String.IsNullOrWhiteSpace(txtDateFrom.Text))
            {
                SearchObject.SearchStartDate = DateTime.Parse(txtDateFrom.Text);
            }
            else
            {
                SearchObject.SearchStartDate = null;
            }


            if (!String.IsNullOrWhiteSpace(txtDateTo.Text))
            {
                SearchObject.SearchEndDate = DateTime.Parse(txtDateTo.Text);
            }
            else
            {
                SearchObject.SearchEndDate = null;
            }

            if (chkActive.Checked)
            {
                SearchObject.Active = true;
            }
            else
            {
                SearchObject.Active = false;
            }

            base.dataBind(gridCampaign.SortExpression, gridCampaign.PageIndex, gridCampaign);
        }

        protected void gridCampaign_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridCampaign);
        }

        protected void gridCampaign_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridCampaign);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            base.lnkDelete_Click(sender, e, gridCampaign);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = String.Empty;
            txtDateFrom.Text = String.Empty;
            txtDateTo.Text = String.Empty;
            SearchObject = null;
        }
    }
}