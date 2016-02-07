using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FashionZone.Admin.Utils;
using Configuration = FashionZone.Admin.Utils.Configuration;
using System.IO;

namespace FashionZone.Admin.CustomControl
{
    public partial class FZFileUpload : System.Web.UI.UserControl
    {
        public string UsedBy
        {
            get { return usedBy.Value; }
            set { usedBy.Value = value; }
        }
        public string FileExtensions { get; set;}
        protected string FileExtMessage
        {
            get { return FileExtensions.Replace("(", "").Replace(")", "").Replace("|", ", "); }
        }

        public string FileName
        {
            get { return fileName.Value; }
            set { fileName.Value = value; }
        }
        protected bool IsImageFile
        {
            get { return FileName.Contains("jpg") || FileName.Contains("jpeg") || FileName.Contains("png"); }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(FileName))
            {
                bool isImageFile = FileName.Contains("jpg") || FileName.Contains("jpeg") || FileName.Contains("png");
                if (isImageFile)
                {
                    if (FileName.Contains(Configuration.ImagesVisualizationPath))
                    {
                        uploadLink.HRef = FileName;
                    }
                    else
                    {
                        uploadLink.HRef = Configuration.ImagesVisualizationPath + FileName;
                    }
                }
                else
                {
                    if (FileName.Contains(Configuration.DocsPath))
                    {
                        uploadLink.HRef = FileName;
                    }
                    else
                    {
                        uploadLink.HRef = Configuration.DocsPath + FileName;
                    }
                }
                if (FileName.Contains(Configuration.UploadSeparator))
                {
                    string file = FileName.Substring(FileName.LastIndexOf(Configuration.UploadSeparator) + 2);
                    uploadLink.InnerHtml = file;
                }
                else
                {
                    uploadLink.InnerHtml = FileName;
                }
            }
        }

        public void Delete()
        {
            if (!String.IsNullOrWhiteSpace(FileName))
            {
                //FileInfo fi = new FileInfo(Path.Combine(Server.MapPath(Configuration.ImagesUploadPath) + FileName));
                FileInfo fi = new FileInfo(Path.Combine(Configuration.ImagesUploadPath + FileName));

                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
        }
    }
}