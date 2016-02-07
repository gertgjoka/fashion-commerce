using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.Admin.Utils;
using FashionZone.DataLayer.Model;

namespace FashionZone.Admin.Secure.Order
{
    public partial class ReturnView : FZBasePage<RETURN>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridReturn.SortExp = "ID";
            }
            base.Page_Load(sender, e, gridReturn);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int idOrder;
            if (SearchObject == null)
                SearchObject = new RETURN();

            if (!String.IsNullOrWhiteSpace(txtName.Text))
                SearchObject.CustomerName = txtName.Text;
            else
                SearchObject.CustomerName = null;
            if (!String.IsNullOrWhiteSpace(txtVerificationNumber.Text))
                SearchObject.VerificationNumber = txtVerificationNumber.Text;
            else
                SearchObject.VerificationNumber = null;
            if (!String.IsNullOrWhiteSpace(txtOdrerId.Text))
            {
                if (int.TryParse(txtOdrerId.Text, out idOrder))
                    SearchObject.OrderID = idOrder;
            }
            else
                SearchObject.OrderID = null;

            base.dataBind(gridReturn.SortExpression, gridReturn.PageIndex, gridReturn);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            base.lnkDelete_Click(sender, e, gridReturn);
        }

        #region GridGest
        protected void gridReturn_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridReturn);
        }

        protected void gridReturn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridReturn);
        }
        #endregion Fine GridGest
    }
}