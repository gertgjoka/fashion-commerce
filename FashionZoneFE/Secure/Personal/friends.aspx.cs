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
    public partial class friends : Utils.BasePage
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
                        RepeatFriends.DataSource =
                            ApplicationContext.Current.Invitations.GetAllInvitationOfCustomer(CurrentCustomer.Id);
                        RepeatFriends.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Log(ex, ex.Message, ex.StackTrace, "Friends");
                }
            }
            base.Page_Load(sender, e);
        }

        protected void btnInvite_Click(object sender, EventArgs e)
        {
            Response.Redirect("/personal/invite/");
        }
    }
}