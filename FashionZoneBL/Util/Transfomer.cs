using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using FashionZone.DataLayer.Model;
using System.IO;

namespace FashionZone.BL.Util
{
    public static class Transfomer
    {
        public static string GenerateNewsletter(CUSTOMER Customer, List<CAMPAIGN> Campaigns, List<CAMPAIGN> FutureCampaigns)
        {
            // Input data will be defined in this XML document.
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement xmlRoot;
            XmlNode xmlNode, xmlNode2;
            XmlNode xmlChild, xmlChild2;

            xmlDoc.LoadXml(
            "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
            "<Root>" +
                //"<UserName/>" +
                //"<Message/>" +
            "<Campaigns/>" +
             "<FutureCampaigns/>" +
            "<UnsubscribeUrl/>" +
            "</Root>");

            // Set the values of the XML nodes that will be used by XSLT.
            xmlRoot = xmlDoc.DocumentElement;

            //xmlNode = xmlRoot.SelectSingleNode("/Root/UserName");
            //string prefix = (Customer.Gender == "M" ? "I " : "E ");
            //xmlNode.InnerText = prefix + "dashur " + Customer.Name + " " + Customer.Surname;

            //xmlNode = xmlRoot.SelectSingleNode("/Root/Message");
            //xmlNode.InnerText = Configuration.NewsletterHeaderText;

            xmlNode = xmlRoot.SelectSingleNode("/Root/Campaigns");

            foreach (CAMPAIGN campaign in Campaigns)
            {
                xmlChild = xmlDoc.CreateNode(XmlNodeType.Element, "Campaign", null);

                // title
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Title", null);
                xmlChild2.InnerText = campaign.BrandName;
                xmlChild.AppendChild(xmlChild2);

                // from
                //xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "From", null);
                //xmlChild2.InnerText = campaign.StartDate.ToString("dd/MM/yyyy");
                //xmlChild.AppendChild(xmlChild2);

                // to
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "To", null);
                xmlChild2.InnerText = campaign.EndDate.ToString("dd/MM/yyyy");
                xmlChild.AppendChild(xmlChild2);

                // image
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Image", null);
                xmlChild2.InnerText = Configuration.DeploymentURL + Configuration.ImagesUploadPath + campaign.ImageHome;
                xmlChild.AppendChild(xmlChild2);

                // Url
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Url", null);
                xmlChild2.InnerText = Configuration.DeploymentURL + "/campaign/" + Encryption.Encrypt(campaign.ID.ToString());
                xmlChild.AppendChild(xmlChild2);

                xmlNode.AppendChild(xmlChild);
            }

            if (FutureCampaigns != null && FutureCampaigns.Count > 0)
            {
                xmlNode2 = xmlRoot.SelectSingleNode("/Root/FutureCampaigns");

                foreach (CAMPAIGN campaign in FutureCampaigns)
                {
                    xmlChild = xmlDoc.CreateNode(XmlNodeType.Element, "Campaign", null);

                    // title
                    xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Title", null);
                    xmlChild2.InnerText = campaign.BrandName;
                    xmlChild.AppendChild(xmlChild2);

                    // from
                    xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "From", null);
                    xmlChild2.InnerText = campaign.StartDate.ToString("dd/MM/yyyy");
                    xmlChild.AppendChild(xmlChild2);

                    // to
                    xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "To", null);
                    xmlChild2.InnerText = campaign.EndDate.ToString("dd/MM/yyyy");
                    xmlChild.AppendChild(xmlChild2);

                    // image
                    xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Image", null);
                    xmlChild2.InnerText = Configuration.DeploymentURL + Configuration.ImagesUploadPath + campaign.ImageHome;
                    xmlChild.AppendChild(xmlChild2);

                    // Url
                    xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Url", null);
                    xmlChild2.InnerText = Configuration.DeploymentURL; // +"/campaign/" + Encryption.Encrypt(campaign.ID.ToString());
                    xmlChild.AppendChild(xmlChild2);

                    xmlNode2.AppendChild(xmlChild);
                }
            }
            //xmlNode = xmlRoot.SelectSingleNode("/Root/UnsubscribeUrl");
            //xmlNode.InnerText = Configuration.UnsubscribeUrl + Encryption.Encrypt(Customer.ID.ToString());

            // This is our XSL template.
            XslCompiledTransform xslDoc = new XslCompiledTransform();
            xslDoc.Load(@Configuration.NewsletterTemplate);

            XsltArgumentList xslArgs = new XsltArgumentList();
            StringWriter writer = new StringWriter();

            // Merge XSLT document with data XML document 
            // (writer will hold resulted transformation).
            xslDoc.Transform(xmlDoc, xslArgs, writer);

            return writer.ToString();
        }

        public static string GenerateOrderMail(CUSTOMER Customer, List<ORDER_DETAIL> Details)
        {
            // Input data will be defined in this XML document.
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement xmlRoot;
            XmlNode xmlNode;
            XmlNode xmlChild2;

            xmlDoc.LoadXml(
            "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
            "<Root>" +
                //"<UserName/>" +
                //"<Message/>" +
            "<Products/>" +
            "<Customer/>" +
            "<Order/>" +
            "</Root>");

            // Set the values of the XML nodes that will be used by XSLT.
            xmlRoot = xmlDoc.DocumentElement;

            xmlNode = xmlRoot.SelectSingleNode("/Root/Customer");


            xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Name", null);
            xmlChild2.InnerText = Customer.Name + " " + Customer.Surname;
            xmlNode.AppendChild(xmlChild2);



            // This is our XSL template.
            XslCompiledTransform xslDoc = new XslCompiledTransform();
            xslDoc.Load(@Configuration.OrderTemplate);

            XsltArgumentList xslArgs = new XsltArgumentList();
            StringWriter writer = new StringWriter();

            // Merge XSLT document with data XML document 
            // (writer will hold resulted transformation).
            xslDoc.Transform(xmlDoc, xslArgs, writer);

            return writer.ToString();
        }
        public static string GenerateInviteMail(SessionCustomer Customer, string Url, string message)
        {
            // Input data will be defined in this XML document.
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement xmlRoot;
            XmlNode xmlNode, xmlNode2, xmlChild2, xmlChild, xmlNode3;

            xmlDoc.LoadXml(
            "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
            "<Root>" +
            "<Customer/>" +
            "<Message/>" +
            "<Url/>" +
            "<Campaigns/>" +
            "</Root>");

            // Set the values of the XML nodes that will be used by XSLT.
            xmlRoot = xmlDoc.DocumentElement;

            xmlNode = xmlRoot.SelectSingleNode("/Root/Customer");


            xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Name", null);
            xmlChild2.InnerText = Customer.FullName;
            xmlNode.AppendChild(xmlChild2);


            xmlNode = xmlRoot.SelectSingleNode("/Root/Url");
            xmlNode.InnerText = Url;

            xmlNode2 = xmlRoot.SelectSingleNode("/Root/Message");
            xmlNode2.InnerText = message;

            xmlNode3 = xmlRoot.SelectSingleNode("/Root/Campaigns");

            CAMPAIGN searchCampaign = new CAMPAIGN() { Active = true, Approved = true };
            int TotalCampaignsForToday;
            List<CAMPAIGN> campaigns = ApplicationContext.Current.Campaigns.Search(searchCampaign, 100, 0, out TotalCampaignsForToday, "StartDate", BL.Util.SortDirection.Descending);


            foreach (CAMPAIGN campaign in campaigns)
            {
                xmlChild = xmlDoc.CreateNode(XmlNodeType.Element, "Campaign", null);

                // title
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Title", null);
                xmlChild2.InnerText = campaign.BrandName;
                xmlChild.AppendChild(xmlChild2);

                // from
                //xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "From", null);
                //xmlChild2.InnerText = campaign.StartDate.ToString("dd/MM/yyyy");
                //xmlChild.AppendChild(xmlChild2);

                // to
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "To", null);
                xmlChild2.InnerText = campaign.EndDate.ToString("dd/MM/yyyy");
                xmlChild.AppendChild(xmlChild2);

                // image
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Image", null);
                xmlChild2.InnerText = Configuration.DeploymentURL + Configuration.ImagesUploadPath + campaign.ImageHome;
                xmlChild.AppendChild(xmlChild2);

                // Url
                //xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Url", null);
                //xmlChild2.InnerText = Configuration.DeploymentURL + "/campaign/" + Encryption.Encrypt(campaign.ID.ToString());
                //xmlChild.AppendChild(xmlChild2);

                xmlNode3.AppendChild(xmlChild);
            }

            // This is our XSL template.
            XslCompiledTransform xslDoc = new XslCompiledTransform();
            xslDoc.Load(@Configuration.InviteTemplate);

            XsltArgumentList xslArgs = new XsltArgumentList();
            StringWriter writer = new StringWriter();

            // Merge XSLT document with data XML document 
            // (writer will hold resulted transformation).
            xslDoc.Transform(xmlDoc, xslArgs, writer);

            return writer.ToString();
        }

        public static string GenerateBonusMail(CUSTOMER Customer)
        {
            // Input data will be defined in this XML document.
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement xmlRoot;
            XmlNode xmlNode, xmlChild2, xmlChild, xmlNode3;

            xmlDoc.LoadXml(
            "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
            "<Root>" +
            "<Customer/>" +
            "<Invited/>" +
            "<Bonus/>" +
            "<Url/>" +
            "<Campaigns/>" +
            "</Root>");

            // Set the values of the XML nodes that will be used by XSLT.
            xmlRoot = xmlDoc.DocumentElement;

            xmlNode = xmlRoot.SelectSingleNode("/Root/Customer");

            xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Name", null);
            xmlChild2.InnerText = Customer.CUSTOMER2.Name + " " + Customer.CUSTOMER2.Surname;
            xmlNode.AppendChild(xmlChild2);

            xmlNode = xmlRoot.SelectSingleNode("/Root/Url");
            xmlNode.InnerText = Configuration.DeploymentURL;

            xmlNode = xmlRoot.SelectSingleNode("/Root/Invited");
            xmlNode.InnerText = Customer.Email;

            xmlNode = xmlRoot.SelectSingleNode("/Root/Bonus");
            xmlNode.InnerText = Configuration.BonusValue.ToString() + " €";

            xmlNode3 = xmlRoot.SelectSingleNode("/Root/Campaigns");

            CAMPAIGN searchCampaign = new CAMPAIGN() { Active = true, Approved = true };
            int TotalCampaignsForToday;
            List<CAMPAIGN> campaigns = ApplicationContext.Current.Campaigns.Search(searchCampaign, 100, 0, out TotalCampaignsForToday, "StartDate", BL.Util.SortDirection.Descending);

            string path;
            foreach (CAMPAIGN campaign in campaigns)
            {
                xmlChild = xmlDoc.CreateNode(XmlNodeType.Element, "Campaign", null);

                // title
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Title", null);
                xmlChild2.InnerText = campaign.BrandName;
                xmlChild.AppendChild(xmlChild2);

                // from
                //xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "From", null);
                //xmlChild2.InnerText = campaign.StartDate.ToString("dd/MM/yyyy");
                //xmlChild.AppendChild(xmlChild2);

                // to
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "To", null);
                xmlChild2.InnerText = campaign.EndDate.ToString("dd/MM/yyyy");
                xmlChild.AppendChild(xmlChild2);

                // image
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Image", null);
                if (Configuration.ImagesVisualizationPath.Contains(Configuration.DeploymentURL))
                    path = Configuration.ImagesVisualizationPath;
                else
                    path = Configuration.DeploymentURL + Configuration.ImagesVisualizationPath;
                xmlChild2.InnerText = path + campaign.ImageHome;
                xmlChild.AppendChild(xmlChild2);

                // Url
                xmlChild2 = xmlDoc.CreateNode(XmlNodeType.Element, "Url", null);
                xmlChild2.InnerText = Configuration.DeploymentURL + "/campaign/" + Encryption.Encrypt(campaign.ID.ToString());
                xmlChild.AppendChild(xmlChild2);

                xmlNode3.AppendChild(xmlChild);
            }

            // This is our XSL template.
            XslCompiledTransform xslDoc = new XslCompiledTransform();
            xslDoc.Load(@Configuration.BonusTemplate);

            XsltArgumentList xslArgs = new XsltArgumentList();
            StringWriter writer = new StringWriter();

            // Merge XSLT document with data XML document 
            // (writer will hold resulted transformation).
            xslDoc.Transform(xmlDoc, xslArgs, writer);

            return writer.ToString();
        }
    }
}
