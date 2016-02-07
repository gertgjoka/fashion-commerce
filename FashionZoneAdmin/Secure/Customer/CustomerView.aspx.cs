using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.DataLayer.Model;
using FashionZone.Admin.Utils;
using SortDirection = FashionZone.BL.Util.SortDirection;

namespace FashionZone.Admin.Secure.Customer
{
    public partial class CustomerView : FZBasePage<CUSTOMER>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridCustomer.SortExp = "ID";
                gridCustomer.SortOrder = SortDirection.Descending;
            }
            base.Page_Load(sender, e, gridCustomer);
        }

        protected void gridCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridCustomer);
        }

        protected void gridCustomer_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridCustomer);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new CUSTOMER();

            if (!String.IsNullOrWhiteSpace(txtName.Text))
            {
                SearchObject.Name = txtName.Text;
            }
            else
            {
                SearchObject.Name = null;
            }

            if (!String.IsNullOrWhiteSpace(txtEmail.Text))
            {
                SearchObject.Email = txtEmail.Text;
            }
            else
            {
                SearchObject.Email = null;
            }
            base.dataBind(gridCustomer.SortExpression, gridCustomer.PageIndex, gridCustomer);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            base.lnkDelete_Click(sender, e, gridCustomer);
        }
    }
}