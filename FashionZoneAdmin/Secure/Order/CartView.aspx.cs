using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.Admin.Utils;
using FashionZone.BL;

namespace FashionZone.Admin.Secure.Order
{
    public partial class CartView : FZBasePage<SHOPPING_CART>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridCarts.SortExp = "ID";
                List<string> carts = ApplicationContext.Current.Carts.GetShoppingCarts();
                carts.Insert(0, "-");
                ddlGuid.DataSource = carts;
                ddlGuid.DataBind();

                List<CAMPAIGN> campaigns = ApplicationContext.Current.Campaigns.GetActiveCampaigns();
                campaigns.Insert(0, new CAMPAIGN() { ID = -1, Name = "" });
                ddlCampaign.DataSource = campaigns;
                ddlCampaign.DataBind();
            }
            base.Page_Load(sender, e, gridCarts);
            txtDateFrom.Attributes.Add("readonly", "readonly");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            createSearchObject();
           
            base.dataBind(gridCarts.SortExpression, gridCarts.PageIndex, gridCarts);
        }

        private void createSearchObject()
        {
            SearchObject = new SHOPPING_CART();

            if (!String.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                SearchObject.CustomerName = txtCustomer.Text;
            }
            else
            {
                SearchObject.CustomerName = null;
            }

            if (!String.IsNullOrWhiteSpace(txtDateFrom.Text))
            {
                SearchObject.SearchStartDate = DateTime.Parse(txtDateFrom.Text);
            }
            else
            {
                SearchObject.SearchStartDate = null;
            }

            if (ddlCampaign.SelectedValue != "-1")
            {
                SearchObject.CampaignID = Int32.Parse(ddlCampaign.SelectedValue);
            }
            else
            {
                SearchObject.CampaignID = 0;
            }
        }

        protected void gridCarts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridCarts);
        }

        protected void gridCarts_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridCarts);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int prodAttrId;
            string[] args = btn.CommandArgument.Split('|');
            if (!String.IsNullOrWhiteSpace(args[0]) && Int32.TryParse(args[1], out prodAttrId))
            {
                ApplicationContext.Current.Carts.DeleteById(args[0], prodAttrId);
                dataBind(gridCarts.SortExpression, gridCarts.PageIndex, gridCarts);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            reset(true);
            ddlGuid.SelectedValue = "-";
            ddlCampaign.Enabled = true;
        }

        private void reset(bool campaign)
        {
            txtCustomer.Text = String.Empty;
            txtDateFrom.Text = String.Empty;
            if (campaign)
                ddlCampaign.SelectedValue = "-1";
            SearchObject = null;
        }

        protected void btnDeleteCart_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(ddlGuid.SelectedValue) && ddlGuid.SelectedValue != "-")
            {
                ApplicationContext.Current.Carts.DeleteShoppingCart(ddlGuid.SelectedValue);
                dataBind(gridCarts.SortExpression, gridCarts.PageIndex, gridCarts);
            }
        }

        protected void ddlGuid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGuid.SelectedValue != "-")
            {
                reset(true);
                txtCustomer.Enabled = false;
                txtDateFrom.Enabled = false;
                ddlCampaign.Enabled = false;

                SearchObject = new SHOPPING_CART() { ID = ddlGuid.SelectedValue };
                base.dataBind(gridCarts.SortExpression, gridCarts.PageIndex, gridCarts);
            }
            else
            {
                txtCustomer.Enabled = true;
                txtDateFrom.Enabled = true;
                ddlCampaign.Enabled = true;
                SearchObject = null;
                base.dataBind(gridCarts.SortExpression, gridCarts.PageIndex, gridCarts);
            }
        }

        protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCampaign.SelectedValue != "-1")
            {
                reset(false);
                createSearchObject();
                SearchObject.CampaignID = Int32.Parse(ddlCampaign.SelectedValue);
                base.dataBind(gridCarts.SortExpression, gridCarts.PageIndex, gridCarts);
            }
        }
    }
}