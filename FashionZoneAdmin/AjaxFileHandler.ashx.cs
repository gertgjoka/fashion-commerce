using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using FashionZone.Admin.Utils;

namespace FashionZone.Admin
{
    /// <summary>
    /// Summary description for AjaxFileHandler
    /// </summary>
    public class AjaxFileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
              
                string name;
                // IE bug: fileName contains the full path
                if (file.FileName.Contains("\\"))
                {
                    name = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);
                }
                else
                {
                    name = file.FileName;
                }
                string path = String.Empty, oldFileName = String.Empty, fileName = String.Empty;
                DirectoryInfo di;
                bool isImageFile = name.Contains ("jpg") || name.Contains ("jpeg") || name.Contains ("png");
                fileName = Guid.NewGuid() + Configuration.UploadSeparator + name;

                if (isImageFile)
                {
                    oldFileName = context.Request.Form["oldFile"];
                    path = Path.Combine(Configuration.ImagesUploadPath, fileName);
                    di = new DirectoryInfo(Configuration.ImagesUploadPath);
                }
                else
                {
                    path = Path.Combine(context.Server.MapPath(Configuration.DocsPath), fileName);
                    di = new DirectoryInfo(context.Server.MapPath(Configuration.DocsPath));
                }
                if (!di.Exists)
                {
                    di.Create();
                }

                //TODO resize image
                file.SaveAs(path);

                // remove the old file from the folder
                if (isImageFile && !String.IsNullOrWhiteSpace(oldFileName))
                {
                    //FileInfo info = new FileInfo(Path.Combine(context.Server.MapPath(Configuration.ImagesUploadPath), oldFileName));
                    FileInfo info = new FileInfo(Path.Combine(Configuration.ImagesUploadPath, oldFileName));
                    if (info.Exists && !info.IsReadOnly)
                    {
                        info.Delete();
                    }

                    info = new FileInfo(Path.Combine(Configuration.ImagesUploadPath, "SMALL" + oldFileName));
                    if (info.Exists && !info.IsReadOnly)
                    {
                        info.Delete();
                    }

                    info = new FileInfo(Path.Combine(Configuration.ImagesUploadPath, "MED" + oldFileName));
                    if (info.Exists && !info.IsReadOnly)
                    {
                        info.Delete();
                    }

                    info = new FileInfo(Path.Combine(Configuration.ImagesUploadPath, "LIST" + oldFileName));
                    if (info.Exists && !info.IsReadOnly)
                    {
                        info.Delete();
                    }
                }
                var serializer = new JavaScriptSerializer();
                var result = new { name =  fileName};
                
                context.Response.ContentType = "text/plain";
                context.Response.Write(serializer.Serialize(result));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}