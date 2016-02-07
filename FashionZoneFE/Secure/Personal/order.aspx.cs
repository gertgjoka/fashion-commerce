using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace FashionZone.FE.Secure.Personal
{
    public partial class order : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //MasterPage set cssClass
            ((SiteMaster)Master).SetImgBackground("", "ContentIII");

            if (CurrentCustomer != null)
            {
                try
                {
                    var orders = ApplicationContext.Current.Customers.GetById(CurrentCustomer.Id).ORDERS.OrderByDescending(o => o.DateCreated);
                    //var o = orders.FirstOrDefault().StatusName;
                    RepeatBonus.DataSource = orders;
                    RepeatBonus.DataBind();
                }
                catch (Exception ex)
                {
                    Log(ex, ex.Message, ex.StackTrace, "Order");
                }
            }
            base.Page_Load(sender, e);
        }

        protected string campaignsString(object orderId)
        {
            int id = 0;
            if (orderId != null && Int32.TryParse(orderId.ToString(), out id))
            {
                return ApplicationContext.Current.Orders.GetOrderCampaigns(id);
            }
            else
                return string.Empty;
        }
    }
}