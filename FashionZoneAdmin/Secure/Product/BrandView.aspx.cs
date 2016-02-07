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
    public partial class BrandView : FZBasePage<BRAND>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridBrands.SortExp = "ShowName";
            }
            base.Page_Load(sender, e, gridBrands);
        }

        protected void gridBrands_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridBrands);
        }

        protected void gridBrands_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridBrands);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new BRAND();

            if (!String.IsNullOrWhiteSpace(txtName.Text))
            {
                SearchObject.Name = txtName.Text;
            }
            else
            {
                SearchObject.Name = null;
            }
            base.dataBind(gridBrands.SortExpression, gridBrands.PageIndex, gridBrands);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            base.lnkDelete_Click(sender, e, gridBrands);
        }

    }
}