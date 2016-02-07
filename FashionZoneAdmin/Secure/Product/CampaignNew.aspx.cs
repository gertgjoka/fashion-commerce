using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using FashionZone.Admin.Utils;
using System.IO;
using Configuration = FashionZone.Admin.Utils.Configuration;
using System.Drawing;

namespace FashionZone.Admin.Secure.Product
{
    public partial class CampaignNew : FZBasePage<BRAND>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Error += new EventHandler(Page_Error);

            string idCampaign = Request["ID"];
            int id;
            if (!IsPostBack)
            {
                CAMPAIGN campaign = null;

                if (!String.IsNullOrWhiteSpace(idCampaign) && Int32.TryParse(idCampaign, out id))
                {
                    campaign = GetCampaign(id);
                    ID.Value = id.ToString();
                    if (campaign != null)
                    {
                        brandId.Value = campaign.BrandID.ToString();
                        btnSelectBrand.Visible = false;
                    }
                }

                if (!String.IsNullOrEmpty(User.Identity.Name))
                {
                    USER user = ApplicationContext.Current.Users.GetByUserName(User.Identity.Name);
                    // not an administrator neither a moderator
                    if (user.RoleID > 2)
                    {
                        chkApproved.Enabled = false;
                    }
                }
                else
                {
                    chkApproved.Enabled = false;
                }
            }
            txtDateFrom.Attributes.Add("readonly", "readonly");
            txtDateTo.Attributes.Add("readonly", "readonly");
            txtDeliveryStart.Attributes.Add("readonly", "readonly");
            txtDeliveryEnd.Attributes.Add("readonly", "readonly");
            txtBrand.Attributes.Add("readonly", "readonly");
            txtApprover.Attributes.Add("readonly", "readonly");
            txtEncryptedId.Attributes.Add("readonly", "readonly");
        }


        private CAMPAIGN GetCampaign(int id)
        {
            CAMPAIGN campaign = null;
            try
            {
                campaign = ApplicationContext.Current.Campaigns.GetById(id);

                if (campaign != null)
                {
                    txtEncryptedId.Text = FashionZone.BL.Util.Encryption.Encrypt(id.ToString());
                    txtName.Text = campaign.Name;
                    txtBrand.Text = campaign.BrandName;
                    txtDesc.Text = campaign.Description;

                    txtDateFrom.Text = campaign.StartDate.Date.ToString("dd/MM/yyyy");
                    txtTimeFrom.Text = campaign.StartDate.ToString("HH:mm");

                    txtDateTo.Text = campaign.EndDate.Date.ToString("dd/MM/yyyy");
                    txtTimeTo.Text = campaign.EndDate.ToString("HH:mm");

                    if (campaign.Approved.HasValue)
                    {
                        chkApproved.Checked = campaign.Approved.Value;

                        if (campaign.USER != null)
                        {
                            txtApprover.Text = campaign.USER.Login;
                        }
                    }

                    if (campaign.DeliveryStartDate.HasValue)
                    {
                        txtDeliveryStart.Text = campaign.DeliveryStartDate.Value.Date.ToString("dd/MM/yyyy");
                    }
                    if (campaign.DeliveryEndDate.HasValue)
                    {
                        txtDeliveryEnd.Text = campaign.DeliveryEndDate.Value.Date.ToString("dd/MM/yyyy");
                    }

                    // Uploads
                    uplHome.FileName = campaign.ImageHome;
                    uplLogo.FileName = campaign.Logo;
                    uplImgDet.FileName = campaign.ImageDetail;
                    uplImgHeader.FileName = campaign.ImageListHeader;
                    uplGenericFile.FileName = campaign.GenericFile;
                }
            }
            catch (Exception ex)
            {
                // TODO log error
                lblErrors.Visible = true;
                lblErrors.ForeColor = Color.Red;
                lblErrors.Text = "Error occurred: " + ex.Message;
            }
            return campaign;
        }


        void Page_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int bId = 0;
            if (Int32.TryParse(btn.CommandArgument, out bId))
            {
                BRAND brand = ApplicationContext.Current.Brands.GetById(bId);
                brandId.Value = bId.ToString();
                txtBrand.Text = brand.ShowName;
            }
            modalPopup.Hide();
        }

        private void Save()
        {
            CAMPAIGN campaign = new CAMPAIGN();
            campaign.Name = txtName.Text;

            int bId = 0;

            if (!String.IsNullOrWhiteSpace(brandId.Value) && Int32.TryParse(brandId.Value, out bId))
            {
                campaign.BrandID = bId;
            }
            else
            {
                lblErrors.Visible = true;
                lblErrors.ForeColor = Color.Red;
                lblErrors.Text = "Associated brand is not correct.";
                return;
            }

            campaign.Description = txtDesc.Text;


            campaign.StartDate = DateTime.Parse(txtDateFrom.Text + " " + txtTimeFrom.Text);
            campaign.EndDate = DateTime.Parse(txtDateTo.Text + " " + txtTimeTo.Text);

            if (!String.IsNullOrWhiteSpace(txtDeliveryStart.Text))
            {
                campaign.DeliveryStartDate = DateTime.Parse(txtDeliveryStart.Text);
            }

            if (!String.IsNullOrWhiteSpace(txtDeliveryEnd.Text))
            {
                campaign.DeliveryEndDate = DateTime.Parse(txtDeliveryEnd.Text);
            }

            if (!String.IsNullOrWhiteSpace(uplLogo.FileName))
            {
                if (uplLogo.FileName.Contains("/"))
                {
                    campaign.Logo = uplLogo.FileName.Substring(uplLogo.FileName.LastIndexOf("/") + 1);
                }
                else
                {
                    campaign.Logo = uplLogo.FileName;
                }
            }

            if (!String.IsNullOrWhiteSpace(uplHome.FileName))
            {
                if (uplHome.FileName.Contains("/"))
                {
                    campaign.ImageHome = uplHome.FileName.Substring(uplHome.FileName.LastIndexOf("/") + 1);
                }
                else
                {
                    campaign.ImageHome = uplHome.FileName;
                }
            }

            if (!String.IsNullOrWhiteSpace(uplImgDet.FileName))
            {
                if (uplImgDet.FileName.Contains("/"))
                {
                    campaign.ImageDetail = uplImgDet.FileName.Substring(uplImgDet.FileName.LastIndexOf("/") + 1);
                }
                else
                {
                    campaign.ImageDetail = uplImgDet.FileName;
                }
            }

            if (!String.IsNullOrWhiteSpace(uplImgHeader.FileName))
            {
                if (uplImgHeader.FileName.Contains("/"))
                {
                    campaign.ImageListHeader = uplImgHeader.FileName.Substring(uplImgHeader.FileName.LastIndexOf("/") + 1);
                }
                else
                {
                    campaign.ImageListHeader = uplImgHeader.FileName;
                }
            }

            if (!String.IsNullOrWhiteSpace(uplGenericFile.FileName))
            {
                if (uplGenericFile.FileName.Contains("/"))
                {
                    campaign.GenericFile = uplGenericFile.FileName.Substring(uplGenericFile.FileName.LastIndexOf("/") + 1);
                }
                else
                {
                    campaign.GenericFile = uplGenericFile.FileName;
                }
            }

            int id = 0;
            lblErrors.Visible = true;
            lblErrors.ForeColor = Color.Green;
            string operation = String.Empty;

            try
            {
                USER user = null;
                if (!String.IsNullOrEmpty(User.Identity.Name))
                {
                    user = ApplicationContext.Current.Users.GetByUserName(User.Identity.Name);
                }
                if (!String.IsNullOrWhiteSpace(ID.Value) && Int32.TryParse(ID.Value, out id) && id != 0)
                {
                    campaign.ID = id;
                    bool attached = false;
                    if (user != null)
                    {
                        bool? previousStatus = ApplicationContext.Current.Campaigns.GetCampaignApprovalStatus(campaign.ID);
                        
                        if (chkApproved.Checked)
                        {
                            if (previousStatus == null || !previousStatus.Value)
                            {
                                if (user.RoleID <= 2)
                                {
                                    campaign.Approved = true;
                                    campaign.ApprovedBy = user.ID;
                                    ApplicationContext.Current.Campaigns.ChangeApproval(campaign);
                                    attached = true;
                                    txtApprover.Text = user.Login;
                                }
                            }
                        }
                        else
                        {
                            if (previousStatus.HasValue && previousStatus.Value)
                            {
                                if (user.RoleID <= 2)
                                {
                                    campaign.Approved = false;
                                    ApplicationContext.Current.Campaigns.ChangeApproval(campaign);
                                    attached = true;
                                    txtApprover.Text = String.Empty;
                                }
                            }
                        }
                    }

                    ApplicationContext.Current.Campaigns.Update(campaign, ! attached);
                    operation = "updated";
                }
                else
                {
                    if (user != null && chkApproved.Checked)
                    {
                        campaign.Approved = true;
                        campaign.ApprovedBy = user.ID;
                        txtApprover.Text = user.Login;
                    }
                    else
                    {
                        campaign.Approved = false;
                    }
                    ApplicationContext.Current.Campaigns.Insert(campaign);
                    operation = "inserted";
                    ID.Value = campaign.ID.ToString();
                }
                txtEncryptedId.Text = FashionZone.BL.Util.Encryption.Encrypt(ID.Value);
                lblErrors.Text = "Campaign " + operation + " correctly.";
            }
            catch (Exception ex)
            {
                // TODO log error
                lblErrors.ForeColor = Color.Red;
                lblErrors.Text = "Error occurred: " + ex.Message;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Save();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetFields();
        }

        private void resetFields()
        {
            txtBrand.Text = "";
            brandId.Value = String.Empty;
            txtName.Text = "";
            txtDateFrom.Text = String.Empty;
            txtDateTo.Text = String.Empty;
            txtDeliveryEnd.Text = String.Empty;
            txtDeliveryStart.Text = String.Empty;
            txtDesc.Text = String.Empty;
            txtSearchName.Text = String.Empty;
            txtTimeFrom.Text = String.Empty;
            txtTimeTo.Text = String.Empty;
            //TODO decide whether to create new object
            //ID.Value = String.Empty;

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchObject == null)
                SearchObject = new BRAND();

            if (!String.IsNullOrWhiteSpace(txtSearchName.Text))
            {
                SearchObject.Name = txtSearchName.Text;
            }
            else
            {
                SearchObject.Name = null;
            }
            base.dataBind(gridBrands.SortExpression, gridBrands.PageIndex, gridBrands);
            updModal.Update();
        }

        protected void gridBrands_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            base.grid_PageIndexChanging(sender, e, gridBrands);
            updModal.Update();
        }

        protected void gridBrands_Sorting(object sender, GridViewSortEventArgs e)
        {
            base.grid_Sorting(sender, e, gridBrands);
            updModal.Update();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            modalPopup.Hide();
        }

        protected void btnSelectBrand_Click(object sender, EventArgs e)
        {
            txtSearchName.Text = String.Empty;
            SearchObject = null;
            dataBind("ID", 0, gridBrands);
            updModal.Update();
            modalPopup.Show();
        }
    }
}