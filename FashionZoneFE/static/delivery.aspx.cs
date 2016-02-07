using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FashionZone.FE.Static
{
	public partial class delivery : Utils.BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

            ((SiteMaster)Master).SetImgBackground("", "ContentIII");
            base.Page_Load(sender, e);
		}
	}
}