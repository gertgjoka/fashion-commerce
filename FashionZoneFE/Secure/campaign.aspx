<%@ Page Title="FZone" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="campaign.aspx.cs" Inherits="FashionZone.FE.Secure.campaign" %>

<%@ Register Src="~/CustomControl/CategoryMenu.ascx" TagPrefix="custom" TagName="category" %>
<%@ Register Src="~/CustomControl/ProductUC.ascx" TagPrefix="custom" TagName="products" %>
<%@ Register Src="~/CustomControl/BreadCrumb.ascx" TagPrefix="custom" TagName="breadcrumb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <custom:breadcrumb runat="server" ID="breadcrumb"></custom:breadcrumb>
    <div class="clearer">
    </div>
    <div class="Dx" runat="server" visible="false" id="divProductContent">
        <%--<div class="DropDown">
            <select>
                <option>Taglia</option>
            </select>
            <select>
                <option>Ordina</option>
            </select>
        </div>--%>
        <custom:products runat="server" ID="productList"></custom:products>
    </div>
    <div>
        <asp:Image runat="server" ID="imgLogo" />
    </div>
    <div class="mainBrandEle">
        <custom:category runat="server" ID="menuCategory"></custom:category>
        <br />
        <br />
        <asp:Image runat="server" ID="imgMenuBrand" Visible="false" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FootContent" runat="server">
</asp:Content>
