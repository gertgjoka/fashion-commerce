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
    public partial class bonus : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //MasterPage set cssClass
            ((SiteMaster)Master).SetImgBackground("", "ContentIII");

            if (!IsPostBack)
            {
                try
                {
                    if (CurrentCustomer != null)
                    {
                        RepeatBonus.DataSource = ApplicationContext.Current.Bonuses.GetAllBonusOfCustomer(CurrentCustomer.Id);
                        //RepeatBonus.DataSource = customer.BONUS;
                        RepeatBonus.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Log(ex, ex.Message, ex.StackTrace, "Bonus");
                }
            }
            base.Page_Load(sender, e);
        }
    }
}