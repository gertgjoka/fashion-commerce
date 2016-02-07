using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FashionZone.FE.CustomControl
{
    public partial class ProductUC : System.Web.UI.UserControl
    {
        public Repeater ProductRepeater
        {
            get { return rptProduct; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string availability(object remaining)
        {
            if (remaining.ToString() != "0")
            {
                return Resources.Lang.AvailableLabel;
            }
            else
            {
                return Resources.Lang.TerminatedLabel;
            }
        }
    }
}