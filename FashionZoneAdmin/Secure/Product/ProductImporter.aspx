<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductImporter.aspx.cs" Inherits="FashionZone.Admin.Secure.Product.ProductImporter" %>

<%@ Register Src="~/CustomControl/FZFileUpload.ascx" TagName="FZFileUpload" TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/js/jquery-1.8.1.min.js"></script>
    <script type="text/javascript" src="/js/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="/js/jquery.fileupload.js"></script>
    <script type="text/javascript" src="/js/jquery.iframe-transport.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="box">
                <h3 id="addCategory">Categories</h3>
                <div id="form">
                    <fieldset id="personal">
                        <legend>Product importer</legend>
                        <label for="Name">
                            Brands :
                        </label>
                        <asp:DropDownList ID="ddl_brand" runat="server" Width="250" />
                        <ajaxToolkit:CascadingDropDown ID="cddl_brand"
                            runat="server"
                            LoadingText="Po karikoj Brand..."
                            TargetControlID="ddl_brand"
                            Category="BRAND"
                            PromptText="Zgjidh nje Brand"
                            ServiceMethod="GetBrand"
                            ServicePath="~/Services/PopulateCddlCat.asmx"/>
                        <br />
                        <br />
                        <label for="Name">
                            Campaign :
                        </label>
                        <asp:DropDownList ID="dll_campain" runat="server"
                            OnSelectedIndexChanged="dll_campain_SelectedIndexChanged" Width="250" />
                        <ajaxToolkit:CascadingDropDown ID="sddl_campain"
                            runat="server"
                            LoadingText="Po karikoj Campagnen..."
                            TargetControlID="dll_campain"
                            ParentControlID="ddl_brand"
                            Category="CAMPAIN"
                            PromptText="Zgjidh nje Campain"
                            ServiceMethod="GetCampain"
                            ServicePath="~/Services/PopulateCddlCat.asmx" />
                        <br />
                        <br />
                        <label for="genericFile">
                            Campaign file :
                        </label>
                        <custom:FZFileUpload ID="uplGenericFile" UsedBy="GenericFile" runat="server" FileExtensions="(xls)|(xlsx)" />
                        <br />
                        <br />
                </div>
            </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
