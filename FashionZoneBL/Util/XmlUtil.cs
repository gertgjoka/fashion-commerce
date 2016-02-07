using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FashionZone.DataLayer.Model;
using System.IO;

namespace FashionZone.BL.Util
{
    public static class XmlUtil
    {
        public static String CreateCategoriesXml(List<CATEGORY> Categories, int CampaignID, string Path, string lang)
        {
            if (Categories != null && Categories.Count > 0)
            {
                string encCamp = Encryption.Encrypt(CampaignID.ToString());

                bool exists = false;
                string fileName = System.IO.Path.Combine(Path, (encCamp + (lang == "en-US" ? lang : string.Empty) + ".xml"));
                XmlDocument doc = new XmlDocument();

                try
                {
                    FileInfo fi = new FileInfo(fileName);
                    if (fi.Exists)
                    {
                        exists = true;
                    }
                    else
                    {
                        exists = false;
                    }
                }
                catch (Exception)
                {
                    exists = false;
                }

                if (!exists)
                {
                    XmlNode root;
                    root = doc.CreateNode(XmlNodeType.Element, "Node", "");

                    List<CATEGORY> fatherCats = Categories.Where(c => c.ParentID == null).ToList();
                    List<CATEGORY> childCats = null;
                    foreach (CATEGORY cat in fatherCats)
                    {
                        XmlNode node = createNode(cat, encCamp, doc, root, lang);

                        childCats = Categories.Where(c => c.ParentID == cat.ID).ToList();

                        if (childCats != null && childCats.Count > 0)
                        {
                            foreach (CATEGORY childCat in childCats)
                            {
                                createNode(childCat, encCamp, doc, node, lang);
                            }
                        }
                    }
                    doc.AppendChild(root);
                    doc.Save(fileName);
                    doc = null;
                }
                return fileName;
            }
            else
            {
                return null;
            }
        }

        private static XmlNode createNode(CATEGORY category, string encCampaign, XmlDocument doc, XmlNode father, string lang)
        {
            string encCategory;
            XmlNode node = doc.CreateNode(XmlNodeType.Element, "Menu", "");
            XmlAttribute attr;
            // text attribute for Menu name
            attr = doc.CreateAttribute("text");
            if (lang == "en-US")
            {
                attr.Value = category.NameEng;
            }
            else
            {
                attr.Value = category.Name;                
            }
            node.Attributes.Append(attr);

            // value attribute for category id
            attr = doc.CreateAttribute("value");
            attr.Value = category.ID.ToString();
            node.Attributes.Append(attr);

            encCategory = Encryption.Encrypt(category.ID.ToString());
            attr = doc.CreateAttribute("url");
            attr.Value = "/campaign/" + encCampaign + "/cat/" + encCategory + "/";
            node.Attributes.Append(attr);

            father.AppendChild(node);

            return node;
        }
    }

}
