using System;
using System.Web.UI.WebControls;
using FashionZone.Admin.Utils;
using FashionZone.DataLayer.Model;
using SortDirection = FashionZone.BL.Util.SortDirection;

namespace FashionZone.Admin.Secure.Customer
{
    public partial class InvitationView : FZBasePage<INVITATION>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridInvitation.SortExp = "ID";
                gridInvitation.SortOrder = SortDirection.Descending;
            }
            Page_Load(sender, e, gridInvitation);
        }      

        #region GridCustomers Code
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new INVITATION();
            if (!String.IsNullOrWhiteSpace(txtEmail.Text))
            {
                SearchObject.InvitedMail = txtEmail.Text;
            }

            base.dataBind(gridInvitation.SortExpression, gridInvitation.PageIndex, gridInvitation);
        }

        protected void gridCustomer_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridInvitation);
        }

        protected void gridCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridInvitation);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            base.lnkDelete_Click(sender, e, gridInvitation);
        }
        #endregion
    }
}