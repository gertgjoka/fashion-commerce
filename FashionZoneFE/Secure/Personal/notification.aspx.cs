using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
namespace FashionZone.FE.Secure.Personal
{
    public partial class notification : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //MasterPage set cssClass
            ((SiteMaster)Master).SetImgBackground("", "ContentIII");
            if (!IsPostBack)
            {
                if (CurrentCustomer != null)
                {
                    try
                    {
                        CUSTOMER customer = ApplicationContext.Current.Customers.GetById(CurrentCustomer.Id);
                        radioDaily.SelectedValue = customer.Newsletter.ToString();
                    }
                    catch (Exception ex)
                    {
                        Log(ex, ex.Message, ex.StackTrace, "Bonus");
                        litError.Text = Resources.Lang.ErrorVerifiedLabel;
                    }
                }
            }

            base.Page_Load(sender, e);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentCustomer != null)
            {
                try
                {
                    CUSTOMER customer = ApplicationContext.Current.Customers.GetById(CurrentCustomer.Id);
                    bool dailyEnabled;
                    if (Boolean.TryParse(radioDaily.SelectedValue, out dailyEnabled))
                    {
                        customer.Newsletter = dailyEnabled;
                    }
                    else
                    {
                        customer.Newsletter = true;
                    }

                    ApplicationContext.Current.Customers.Update(customer, false);
                }
                catch (Exception ex)
                {
                    Log(ex, ex.Message, ex.StackTrace, "Bonus");
                    litError.Text = Resources.Lang.ErrorVerifiedLabel;
                }
            }
        }
    }
}