using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.ComponentModel;
using SortDirection = FashionZone.BL.Util.SortDirection;

namespace FashionZone.Admin.CustomControl
{
    public class FZGrid : GridView
    {

        private const string _CustomVirtualItemCount = "CustomVirtualItemCount";
        private const string _currentPageIndex = "currentPageIndex";
        private const string _sortOrder = "sortOrder";
        private const string _sortExp = "sortExp";

        [Browsable(true), Category("Custom")]

        [Description("Set the virtual item count for this grid")]

        public int CustomCustomVirtualItemCount
        {
            get
            {
                if (ViewState[_CustomVirtualItemCount] == null)
                    ViewState[_CustomVirtualItemCount] = -1;
                return Convert.ToInt32(ViewState[_CustomVirtualItemCount]);
            }
            set
            {
                ViewState[_CustomVirtualItemCount] = value;
            }
        }

        public int CurrentPageIndex
        {
            get
            {
                if (ViewState[_currentPageIndex] == null)
                    ViewState[_currentPageIndex] = 0;
                return Convert.ToInt32(ViewState[_currentPageIndex]);
            }
            set
            {
                ViewState[_currentPageIndex] = value;
            }
        }

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }

            set
            {
                base.DataSource = value;
            }
        }

        public SortDirection SortOrder
        {
            get
            {
                if (ViewState[_sortOrder] == null)
                    ViewState[_sortOrder] = FashionZone.BL.Util.SortDirection.Ascending;
                return (SortDirection)ViewState[_sortOrder];
            }
            set
            {
                ViewState[_sortOrder] = value;
            }
        }

        public String SortExp
        {
            get
            {
                return ViewState[_sortExp] == null ? null : ViewState[_sortExp].ToString();
            }
            set
            {
                ViewState[_sortExp] = value;
            }
        }

        public void ChangeSorting()
        {
            if ((SortDirection)ViewState[_sortOrder] == FashionZone.BL.Util.SortDirection.Descending)
                ViewState[_sortOrder] = FashionZone.BL.Util.SortDirection.Ascending;
            else
                ViewState[_sortOrder] = FashionZone.BL.Util.SortDirection.Descending;
        }

        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            if (CustomPaging)
            {
                pagedDataSource.AllowCustomPaging = true;
                pagedDataSource.VirtualCount = this.CustomCustomVirtualItemCount;
                pagedDataSource.CurrentPageIndex = this.CurrentPageIndex;
            }
            base.InitializePager(row, columnSpan, pagedDataSource);
        }

        public bool CustomPaging
        {
            get { return (this.CustomCustomVirtualItemCount != -1); }
        }

    }
}