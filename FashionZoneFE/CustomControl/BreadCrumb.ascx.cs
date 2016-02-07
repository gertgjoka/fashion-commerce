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
    public partial class BreadCrumb : System.Web.UI.UserControl
    {
       public int CategoryID
        {
            get
            {
                int id;
                if (ViewState["CID"] != null && Int32.TryParse(ViewState["CID"].ToString(), out id))
                    return id;
                else
                    return 0;
            }
            set
            {
                ViewState["CID"] = value;
            }
        }

       public int ProductID
       {
           get
           {
               int id;
               if (ViewState["PID"] != null && Int32.TryParse(ViewState["PID"].ToString(), out id))
                   return id;
               else
                   return 0;
           }
           set
           {
               ViewState["PID"] = value;
           }
       }

       public int CampaignID
       {
           get
           {
               int id;
               if (ViewState["CaID"] != null && Int32.TryParse(ViewState["CaID"].ToString(), out id))
                   return id;
               else
                   return 0;
           }
           set
           {
               ViewState["CaID"] = value;
           }
       }

        public string CampaignName
        {
            get { return litCampaign.Text; }
            set { litCampaign.Text = value; }
        }

        public string ParentCategoryName
        {
            get { return litParentCategory.Text; }
            set { litParentCategory.Text = value; }
        }

        public string CategoryName
        {
            get { return litCategory.Text; }
            set { litCategory.Text = value; }
        }

        public string ProductName
        {
            get { return litProduct.Text; }
            set { litProduct.Text = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string encCampaignID = String.Empty;

            string href;

            if (!String.IsNullOrEmpty(CampaignName) && Page.RouteData.Values["camp"] != null)
            {
                if (Page.RouteData.Values["cat"] != null)
                {
                    encCampaignID = Page.RouteData.Values["camp"].ToString();
                    href = "/campaign/" + encCampaignID + "/";
                    aCampaign.HRef = href;
                    setCategoryAndParent(href);
                }
                else
                {
                    this.Visible = false;
                }
            }

            if (ProductID != 0)
            {
                encCampaignID = Encryption.Encrypt(CampaignID.ToString());
                href = "/campaign/" + encCampaignID + "/";
                aCampaign.HRef = href;
                setCategoryAndParent(href);
                if (Page.RouteData.Values["prod"] != null)
                {
                    aProduct.HRef = "";
                    aProduct.Disabled = true;
                    aProduct.Style["color"] = "grey";
                    aProduct.Style["text-decoration"] = "none";
                }
                sep3.Visible = true;
            }
        }

        private void setCategoryAndParent(string href)
        {
            string encCat = String.Empty;
            if (Page.RouteData.Values["cat"] != null)
            {
                encCat = Page.RouteData.Values["cat"].ToString();
                aCategory.HRef = "";
                aCategory.Style["color"] = "grey";
                aCategory.Style["text-decoration"] = "none";
            }
            else
            {
                encCat = Encryption.Encrypt(CategoryID.ToString());
                aCategory.HRef = href + "cat/" + encCat + "/";
            }

            sep2.Visible = true;

            CATEGORY cat = ApplicationContext.Current.Categories.GetById(CategoryID);

            if (Session["Culture"] == null || Session["Culture"].ToString() != "en-US")
            {
                CategoryName = cat.Name;
            }
            else
            {
                CategoryName = cat.NameEng;
            }
            if (cat.ParentID.HasValue)
            {
                string encParent = Encryption.Encrypt(cat.ParentID.Value.ToString());
                if (Session["Culture"] == null || Session["Culture"].ToString() != "en-US")
                {
                    ParentCategoryName = cat.CATEGORY2.Name;
                }
                else
                {
                    ParentCategoryName = cat.CATEGORY2.NameEng;
                }
                aParentCategory.HRef = href + "cat/" + encParent + "/";
                sep.Visible = true;
            }
        }
    }
}