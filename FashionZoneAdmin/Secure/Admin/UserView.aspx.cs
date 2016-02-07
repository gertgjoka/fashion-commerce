using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using SortDirection = FashionZone.BL.Util.SortDirection;
using FashionZone.DataLayer.Model;
using FashionZone.Admin.Utils;

namespace FashionZone.Admin.Users
{
    public partial class UserView : FZBasePage<USER>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridUser.SortExp = "Name";
            }
            base.Page_Load(sender, e, gridUser);
        }

        protected void gridUser_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridUser);
        }

        protected void gridUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridUser);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new USER();

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
            base.dataBind(gridUser.SortExpression, gridUser.PageIndex, gridUser);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            base.lnkDelete_Click(sender, e, gridUser);
        }
    }
}