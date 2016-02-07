using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using FashionZone.BL.Util;
using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace FashionZone.FE.Secure
{
    public partial class campaign : Utils.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl divContent = (HtmlGenericControl)Master.FindControl("divMainContent");

            string encCampaignID = RouteData.Values["camp"].ToString();
            string encCat = String.Empty;
            if (RouteData.Values["cat"] == null)
            {
                divContent.Attributes["class"] = "ContentII";
                imgLogo.Style["margin-top"] = "42px";
            }
            else
            {
                divContent.Attributes["class"] = "ContentIII";
                encCat = RouteData.Values["cat"].ToString();
            }
            try
            {
                string campID = Encryption.Decrypt(encCampaignID);

                int idCamp;
                if (Int32.TryParse(campID, out idCamp))
                {
                    menuCategory.CampaignID = idCamp;
                    

                    CAMPAIGN campaign = ApplicationContext.Current.Campaigns.GetById(idCamp);
                    if (campaign != null && (campaign.Active || (campaign.Approved.Value && campaign.StartDate > DateTime.Today)))
                    {

                        imgLogo.ImageUrl = Configuration.ImagesUploadPath + campaign.Logo;
                        breadcrumb.CampaignName = campaign.BrandName;

                        this.Title = campaign.BrandName + " - FZone.al";
                        if (RouteData.Values["cat"] == null)
                        {
                            divContent.Attributes["style"] = "background-image:url('" + Configuration.ImagesUploadPath + campaign.ImageDetail + "');";
                        }

                        string catID;
                        int iCatId;

                        if (!String.IsNullOrEmpty(encCat))
                        {
                            catID = Encryption.Decrypt(encCat);
                            if (Int32.TryParse(catID, out iCatId))
                            {
                                breadcrumb.CategoryID = iCatId;
                                loadProducts(idCamp, iCatId);
                                imgMenuBrand.Visible = true;
                                imgMenuBrand.ImageUrl = Configuration.ImagesUploadPath + campaign.ImageListHeader;
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("/home");
                    }
                }
            }
            catch(System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Log(ex, ex.Message, ex.StackTrace, "Campaign");
                Response.Redirect("/home");
            }
            base.Page_Load(sender, e);
        }

        private void loadProducts(int campaignID, int categoryID)
        {
            List<PRODUCT> products = ApplicationContext.Current.Products.GetProductsByCampaign(campaignID, categoryID);
            if (products != null && products.Count > 0)
            {
                productList.ProductRepeater.DataSource = products;
                productList.ProductRepeater.DataBind();
                divProductContent.Visible = true;
            }
            else
            {
                divProductContent.Visible = false;
            }
        }

    }
}