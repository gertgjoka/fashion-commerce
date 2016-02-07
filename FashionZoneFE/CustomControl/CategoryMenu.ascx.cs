using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using FashionZone.BL.Util;
using System.Xml;

namespace FashionZone.FE.CustomControl
{
    public partial class CategoryMenu : System.Web.UI.UserControl
    {
        public int CampaignID
        {
            get
            {
                int id;
                if (ViewState["CampaignID"] != null && Int32.TryParse(ViewState["CampaignID"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["CampaignID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadMenu();
            }
        }

        private void loadMenu()
        {
            List<CATEGORY> categories = ApplicationContext.Current.Categories.GetCategoryListByCampaign(CampaignID);
            string path = Context.Server.MapPath(Configuration.XmlPath);
            string culture = String.Empty;
            if (Session["Culture"] != null)
            {
                culture = Session["Culture"].ToString();
            }
            String filename = XmlUtil.CreateCategoriesXml(categories, CampaignID, path, culture);
            if (!String.IsNullOrEmpty(filename))
            {
                XmlDataSource menuDataSource = new XmlDataSource();
                menuDataSource.XPath = "Node/Menu";
                menuDataSource.DataFile = filename;
                menuCat.DataSource = menuDataSource;
                menuCat.DataBind();
            }
        }
    }
}