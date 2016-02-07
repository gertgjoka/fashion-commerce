using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.Admin.Utils;

namespace FashionZone.Admin.Secure.Customer
{
    public partial class BonusView : FZBasePage<BONUS>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridBonus.SortExp = "ID";
            }
            base.Page_Load(sender, e, gridBonus);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new BONUS();

            if (!String.IsNullOrWhiteSpace(txtName.Text))
            {
                SearchObject.CustomerFullName = txtName.Text;
            }
            else
            {
                SearchObject.Description = null;
            }

            if (!String.IsNullOrWhiteSpace(txtDesc.Text))
            {
                SearchObject.Description = txtDesc.Text;
            }
            else
            {
                SearchObject.CustomerID = null;
            }
            base.dataBind(gridBonus.SortExpression, gridBonus.PageIndex, gridBonus);
        }

        protected void gridCustomer_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridBonus);
        }
        protected void gridCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridBonus);
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            base.lnkDelete_Click(sender, e, gridBonus);
        }
    }
}