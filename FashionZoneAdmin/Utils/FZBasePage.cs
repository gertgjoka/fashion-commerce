using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SortDirection = FashionZone.BL.Util.SortDirection;
using System.Web.UI.WebControls;
using System.Drawing;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using FashionZone.Admin.CustomControl;
using FashionZone.BL.DAO;
using FashionZone.BL.Manager;

namespace FashionZone.Admin.Utils
{
    public class FZBasePage<T> : System.Web.UI.Page
    {
        private const string _searchObject = "fzSearchObject";
        private const string _isManagerObject = "fzIsManagerObject";

        private IManager<T> _contextManager
        {
            get
            {
                if (typeof(T) == typeof(D_ATTRIBUTE))
                {
                    return (IManager<T>)ApplicationContext.Current.Attributes;
                }
                if (typeof(T) == typeof(BONUS))
                {
                    return (IManager<T>)ApplicationContext.Current.Bonuses;
                }
                if (typeof(T) == typeof(BRAND))
                {
                    return (IManager<T>)ApplicationContext.Current.Brands;
                }
                if (typeof(T) == typeof(CAMPAIGN))
                {
                    return (IManager<T>)ApplicationContext.Current.Campaigns;
                }
                if (typeof(T) == typeof(CATEGORY))
                {
                    return (IManager<T>)ApplicationContext.Current.Categories;
                }
                if (typeof(T) == typeof(CUSTOMER))
                {
                    return (IManager<T>)ApplicationContext.Current.Customers;
                }
                if (typeof(T) == typeof(INVITATION))
                {
                    return (IManager<T>)ApplicationContext.Current.Invitations;
                }
                if (typeof(T) == typeof(ORDERS))
                {
                    return (IManager<T>)ApplicationContext.Current.Orders;
                }
                if (typeof(T) == typeof(PRODUCT))
                {
                    return (IManager<T>)ApplicationContext.Current.Products;
                }
                if (typeof(T) == typeof(SHOPPING_CART))
                {
                    return (IManager<T>)ApplicationContext.Current.Carts;
                }
                if (typeof(T) == typeof(RETURN))
                {
                    return (IManager<T>)ApplicationContext.Current.Returns;
                }
                if (typeof(T) == typeof(USER))
                {
                    return (IManager<T>)ApplicationContext.Current.Users;
                }
                return default(IManager<T>);
            }
        }

        protected void Page_Load(object sender, EventArgs e, FZGrid grid)
        {
            if (!IsPostBack)
            {
                SearchObject = default(T);
                if (grid != null)
                {
                    dataBind(grid.SortExp, 0, grid);
                }
            }
        }

        public T SearchObject
        {
            get { return (T)Session[_searchObject]; }
            set { Session[_searchObject] = value; }
        }

        protected void dataBind(string sortExp, int pageIndex, FZGrid grid)
        {
            int totalRecords = 0;
            grid.PageSize = Utils.Configuration.PageSize;
            //List<T> list = IsManagerObject ? _contextManager.Search(SearchObject, Utils.Configuration.PageSize, pageIndex, out totalRecords, sortExp, grid.SortOrder)
            //                               : _context.Search(SearchObject, Utils.Configuration.PageSize, pageIndex, out totalRecords, sortExp, grid.SortOrder);

            List<T> list = _contextManager.Search(SearchObject, Utils.Configuration.PageSize, pageIndex,
                                                        out totalRecords, sortExp, grid.SortOrder);

            grid.DataSource = list;
            grid.CustomCustomVirtualItemCount = totalRecords;
            grid.DataBind();
        }

        protected void clearBinding(FZGrid grid)
        {
            grid.DataSource = null;
            grid.DataBind();
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e, FZGrid grid)
        {
            grid.CurrentPageIndex = e.NewPageIndex;
            dataBind(grid.SortExp, e.NewPageIndex, grid);
        }

        protected void grid_Sorting(object sender, GridViewSortEventArgs e, FZGrid grid)
        {
            if (grid.SortExp == e.SortExpression)
            {
                grid.ChangeSorting();
            }
            else
            {
                grid.SortOrder = SortDirection.Ascending;
            }

            grid.SortExp = e.SortExpression;
            dataBind(e.SortExpression, grid.CurrentPageIndex, grid);
        }

        protected void lnkDelete_Click(object sender, EventArgs e, FZGrid grid)
        {
            LinkButton btn = sender as LinkButton;
            int id = 0;
            if (Int32.TryParse(btn.CommandArgument, out id))
            {
                _contextManager.DeleteById(id);

                dataBind(grid.SortExpression, grid.PageIndex, grid);
            }
        }
    }
}