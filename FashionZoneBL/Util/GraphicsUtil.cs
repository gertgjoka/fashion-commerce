using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace FashionZone.BL.Util
{
    public enum ImageType
    {
        List = 1,
        Small = 2,
        Medium = 3
    }

    public static class GraphicsUtil
    {
        public static Image ResizeProductImage(Image imgToResize, ImageType type)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            switch (type)
            {
                case ImageType.List:
                    nPercentW = ((float)Configuration.ProductImgWidthList / (float)sourceWidth);
                    nPercentH = ((float)Configuration.ProductImgHeightList / (float)sourceHeight);
                    break;
                case ImageType.Small:
                    nPercentW = ((float)Configuration.ProductImgWidthSmall / (float)sourceWidth);
                    nPercentH = ((float)Configuration.ProductImgHeightSmall / (float)sourceHeight);
                    break;
                case ImageType.Medium:
                    nPercentW = ((float)Configuration.ProductImgWidthMedium / (float)sourceWidth);
                    nPercentH = ((float)Configuration.ProductImgHeightMedium / (float)sourceHeight);
                    break;
            }
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            using (Graphics g = Graphics.FromImage((Image)b))
            {
                //g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.InterpolationMode = InterpolationMode.Low;
                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            }
            return (Image)b;
        }

        public static void SaveProductImages(string ImageName, bool IsPrincipal)
        {
            string medFileName = "MED" + ImageName;
            string smallFileName = "SMALL" + ImageName;
            string listFileName = "LIST" + ImageName;
            
            string fileWithPath = Path.Combine(Configuration.ImagesUploadPath , ImageName);
            string thumbWithPath = Path.Combine(Configuration.ImagesUploadPath, smallFileName);
            System.Drawing.Image img = System.Drawing.Image.FromFile(fileWithPath);
            FileInfo fi2 = new FileInfo(thumbWithPath);
            if (!fi2.Exists)
            {

                using (System.Drawing.Image imgResized = GraphicsUtil.ResizeProductImage(img, ImageType.Small))
                {
                    imgResized.Save(thumbWithPath);
                }
            }

            string mediumWithPath = Path.Combine(Configuration.ImagesUploadPath, medFileName);
            fi2 = new FileInfo(mediumWithPath);
            if (!fi2.Exists)
            {
                using (System.Drawing.Image imgResized = GraphicsUtil.ResizeProductImage(img, ImageType.Medium))
                {
                    imgResized.Save(mediumWithPath);
                }
            }


            if (IsPrincipal && !String.IsNullOrEmpty(ImageName))
            {
                //string fileWithPath = Path.Combine(Server.MapPath(Configuration.ImagesUploadPath), image.Image);
                //string fileListWithPath = Path.Combine(Server.MapPath(Configuration.ImagesUploadPath), "LIST" + image.Image);

                string fileListWithPath = Path.Combine(Configuration.ImagesUploadPath, listFileName);

                FileInfo fi = new FileInfo(fileListWithPath);
                if (!fi.Exists)
                {
                    using (System.Drawing.Image imgResized = GraphicsUtil.ResizeProductImage(img, ImageType.List))
                    {
                        imgResized.Save(fileListWithPath);
                    }
                }
            }

            img.Dispose();
            img = null;
        }
    }
}
