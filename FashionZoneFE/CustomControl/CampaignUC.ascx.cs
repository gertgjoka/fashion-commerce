using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.BL.Util;
using FashionZone.DataLayer.Model;
using FashionZone.BL;

namespace FashionZone.FE.CustomControl
{
    public partial class CampaignUC : System.Web.UI.UserControl
    {
        public Repeater CampaignRepeater
        {
            get { return rptCampaign; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected string Url(object Active)
        {
           if ((bool)Active)
           {
               return "/campaign/" + FashionZone.BL.Util.Encryption.Encrypt(Eval("Id").ToString()) + "/";
           } 
           else
           {
               return String.Empty;
           }
        }
    }
}