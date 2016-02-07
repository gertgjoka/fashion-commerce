using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.Admin.Utils;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using SortDirection = FashionZone.BL.Util.SortDirection;

namespace FashionZone.Admin.Secure.Order
{
    public partial class OrderView : FZBasePage<ORDERS>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridOrders.SortExp = "DateCreated";
                gridOrders.SortOrder = SortDirection.Descending;

                populateStatus();
            }
            base.Page_Load(sender, e, gridOrders);
            txtDateFrom.Attributes.Add("readonly", "readonly");
            txtDateTo.Attributes.Add("readonly", "readonly");
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new ORDERS();

            if (!String.IsNullOrWhiteSpace(txtName.Text))
            {
                SearchObject.CustomerName = txtName.Text;
            }
            else
            {
                SearchObject.CustomerName = null;
            }

            int number;
            if (!String.IsNullOrWhiteSpace(txtNumber.Text) && Int32.TryParse(txtNumber.Text, out number))
            {
                SearchObject.ID = number;
            }
            else
            {
                SearchObject.ID = 0;
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

            int status;
            Int32.TryParse(ddlStatus.SelectedValue, out status);
            if (status != 0)
            {
                SearchObject.Status = status;
            }
            else
            {
                SearchObject.Status = null;
            }
            base.dataBind(gridOrders.SortExpression, gridOrders.PageIndex, gridOrders);
        }

        protected void populateStatus()
        {
            List<D_ORDER_STATUS> statuses = ApplicationContext.Current.Orders.GetStatuses();
            statuses.Insert(0, new D_ORDER_STATUS() { ID = 0, Name = "" });
            ddlStatus.DataSource = statuses;
            ddlStatus.DataBind();
        }

        protected void gridOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridOrders);
        }

        protected void gridOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridOrders);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            base.lnkDelete_Click(sender, e, gridOrders);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = String.Empty;
            txtNumber.Text = String.Empty;
            txtDateFrom.Text = String.Empty;
            txtDateTo.Text = String.Empty;
            ddlStatus.SelectedValue = "0";
            SearchObject = null;
        }
    }
}