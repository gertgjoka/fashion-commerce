using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.DataLayer.Model;
using FashionZone.BL;
using System.Drawing;
using FashionZone.BL.Util;
using System.IO;

namespace FashionZone.Admin.CustomControl
{
    public partial class ProductImageUpload : System.Web.UI.UserControl
    {
        public event EventHandler ImageDelete;
        private string usedBy;
        public string UsedBy
        {
            set
            {
                img3.UsedBy = value + "3";
                usedBy = value;
            }
            get
            {
                return usedBy;
            }
        }

        public string OldImg
        {
            set
            {
                oldImg.Value= value;
            }
            get
            {
                return oldImg.Value;
            }
        }

        public string Image
        {
            get { return "MED" + img3.FileName; }
        }

        public string Thumbnail
        {
            get { return "SMALL" + img3.FileName; }
        }

        public string LargeImage
        {
            get { return img3.FileName; }
            set { img3.FileName = value; }
        }

        public bool Principal
        {
            get { return chkPrincipal.Checked; }
            set { chkPrincipal.Checked = value; }
        }


        public String ProdID
        {
            get
            {
                int id;
                if (!String.IsNullOrWhiteSpace(prodID.Value) && Int32.TryParse(prodID.Value, out id))
                    return id.ToString();
                else
                    return "0";
            }
            set
            {
                prodID.Value = value;
            }
        }

        public String ImgID
        {
            get
            {
                int id;
                if (!String.IsNullOrWhiteSpace(imgID.Value) && Int32.TryParse(imgID.Value, out id))
                    return id.ToString();
                else
                    return "0";
            }
            set
            {
                imgID.Value = value;
                if (value != "0")
                {
                    if (!String.IsNullOrEmpty(Page.User.Identity.Name))
                    {
                        USER user = ApplicationContext.Current.Users.GetByUserName(Page.User.Identity.Name);
                        // not an administrator neither a moderator
                        if (user.RoleID <= 2)
                        {
                            lnkDelete.Visible = true;
                        }
                    }
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool IsFormValid()
        {
            if (!String.IsNullOrWhiteSpace(Image) && !String.IsNullOrWhiteSpace(Thumbnail) && !String.IsNullOrWhiteSpace(LargeImage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsSaved()
        {
            return ImgID != "0";
        }

        public void Save()
        {
            if (String.IsNullOrEmpty(oldImg.Value) || oldImg.Value != LargeImage)
            {
                PROD_IMAGES image = new PROD_IMAGES();
                image.Image = Image;
                image.Principal = Principal;
                image.LargeImage = LargeImage;
                image.Thumbnail = Thumbnail;
                image.ProductID = Int32.Parse(ProdID);

                int id = 0;
                lblErrors.Visible = true;
                lblErrors.ForeColor = Color.Green;
                string operation = String.Empty;

                try
                {
                    GraphicsUtil.SaveProductImages(LargeImage, Principal);

                    // TODO the following code should be removed as it was encapsulated in the GraphicsUtil.SaveProductImages method
                    //string fileWithPath = Path.Combine(Configuration.ImagesUploadPath, LargeImage);
                    //string thumbWithPath = Path.Combine(Configuration.ImagesUploadPath, Thumbnail);
                    //System.Drawing.Image img = System.Drawing.Image.FromFile(fileWithPath);
                    //FileInfo fi2 = new FileInfo(thumbWithPath);
                    //if (!fi2.Exists)
                    //{

                    //    using (System.Drawing.Image imgResized = GraphicsUtil.ResizeProductImage(img, ImageType.Small))
                    //    {
                    //        imgResized.Save(thumbWithPath);
                    //    }
                    //}

                    //string mediumWithPath = Path.Combine(Configuration.ImagesUploadPath, Image);
                    //fi2 = new FileInfo(mediumWithPath);
                    //if (!fi2.Exists)
                    //{
                    //    using (System.Drawing.Image imgResized = GraphicsUtil.ResizeProductImage(img, ImageType.Medium))
                    //    {
                    //        imgResized.Save(mediumWithPath);
                    //    }
                    //}


                    //if (Principal && !String.IsNullOrEmpty(image.Image))
                    //{
                    //    //string fileWithPath = Path.Combine(Server.MapPath(Configuration.ImagesUploadPath), image.Image);
                    //    //string fileListWithPath = Path.Combine(Server.MapPath(Configuration.ImagesUploadPath), "LIST" + image.Image);

                    //    string fileListWithPath = Path.Combine(Configuration.ImagesUploadPath, "LIST" + image.LargeImage);

                    //    FileInfo fi = new FileInfo(fileListWithPath);
                    //    if (!fi.Exists)
                    //    {
                    //        using (System.Drawing.Image imgResized = GraphicsUtil.ResizeProductImage(img, ImageType.List))
                    //        {
                    //            imgResized.Save(fileListWithPath);
                    //        }
                    //    }
                    //}

                    //img.Dispose();
                    //img = null;

                    if (!String.IsNullOrWhiteSpace(ImgID) && Int32.TryParse(ImgID, out id) && id != 0)
                    {
                        image.ID = id;
                        ApplicationContext.Current.Products.UpdateImage(image);
                        operation = "updated";
                    }
                    else
                    {
                        ApplicationContext.Current.Products.InsertImage(image);
                        operation = "inserted";
                        ImgID = image.ID.ToString();
                    }
                    lblErrors.Text = "Images " + operation + " correctly.";
                    oldImg.Value = LargeImage;
                    if (!String.IsNullOrEmpty(Page.User.Identity.Name))
                    {
                        USER user = ApplicationContext.Current.Users.GetByUserName(Page.User.Identity.Name);
                        // not an administrator neither a moderator
                        if (user.RoleID <= 2)
                        {
                            lnkDelete.Visible = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // TODO log error
                    lblErrors.ForeColor = Color.Red;
                    lblErrors.Text = "Error occurred: " + ex.Message;
                }
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            ImageDelete(this, e);
            if (!String.IsNullOrWhiteSpace(LargeImage))
            {
                //FileInfo fi = new FileInfo(Path.Combine(Server.MapPath(Configuration.ImagesUploadPath) + FileName));
                FileInfo fi = new FileInfo(Path.Combine(Configuration.ImagesUploadPath + "LIST" + LargeImage));

                if (fi.Exists)
                {
                    fi.Delete();
                }

                fi = new FileInfo(Path.Combine(Configuration.ImagesUploadPath + Image));

                if (fi.Exists)
                {
                    fi.Delete();
                }

                fi = new FileInfo(Path.Combine(Configuration.ImagesUploadPath + Thumbnail));

                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
            img3.Delete();
        }
    }
}