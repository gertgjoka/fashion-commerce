using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.Admin.Utils;
using FashionZone.DataLayer.Model;
using FashionZone.BL;

namespace FashionZone.Admin.Secure.Product
{
    public partial class ProductView : FZBasePage<PRODUCT>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridProduct.SortExp = "Name";

                CAMPAIGN campaign = new CAMPAIGN() { SearchStartDate = DateTime.Now.Date.AddMonths(-5), SearchEndDate = DateTime.Now.Date.AddMonths(5) };

                int totalRecords;
                List<CAMPAIGN> campaigns = ApplicationContext.Current.Campaigns.Search(campaign, 10, 0, out totalRecords, "StartDate", BL.Util.SortDirection.Descending).OrderByDescending(c => c.StartDate).ToList();
                campaigns.Insert(0, new CAMPAIGN() { ID = -1, Name = "" });
                ddlCampaign.DataSource = campaigns;
                ddlCampaign.DataBind();
            }
            base.Page_Load(sender, e, gridProduct);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new PRODUCT();

            if (!String.IsNullOrWhiteSpace(txtName.Text))
            {
                SearchObject.Name = txtName.Text;
            }
            else
            {
                SearchObject.Name = null;
            }

            if (!String.IsNullOrWhiteSpace(txtDescription.Text))
            {
                SearchObject.Description = txtDescription.Text;
            }
            else
            {
                SearchObject.Description = null;
            }

            if (!String.IsNullOrWhiteSpace(txtCode.Text))
            {
                SearchObject.Code = txtCode.Text;
            }
            else
            {
                SearchObject.Code = null;
            }

            if (chkActive.Checked)
            {
                SearchObject.Closed = false;
            }
            else
            {
                SearchObject.Closed = null;
            }

            if (ddlCampaign.SelectedValue != "-1")
            {
                SearchObject.CampaignID = Int32.Parse(ddlCampaign.SelectedValue);
            }
            else
            {
                SearchObject.CampaignID = null;
            }

            base.dataBind(gridProduct.SortExpression, gridProduct.PageIndex, gridProduct);
        }

        protected void gridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridProduct);
        }

        protected void gridProduct_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridProduct);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            base.lnkDelete_Click(sender, e, gridProduct);
        }
    }
}