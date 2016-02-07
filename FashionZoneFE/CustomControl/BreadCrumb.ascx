<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BreadCrumb.ascx.cs"
    Inherits="FashionZone.FE.CustomControl.BreadCrumb" %>
<div class="Breadcrumbs">
    <ul class="Breadcrumbs_ul">
        <li><a href="#" runat="server" id="aCampaign">
            <asp:Literal runat="server" ID="litCampaign"></asp:Literal></a></li>
        <li>
            <img src="/image/list-image.png" runat="server" visible="false"
                id="sep" alt="" />
            <a href="#" runat="server" id="aParentCategory">
                <asp:Literal runat="server" ID="litParentCategory"></asp:Literal></a></li>
        <li>
            <img src="/image/list-image.png" runat="server" visible="false"
                id="sep2" alt="" />
            <a href="#" runat="server" id="aCategory">
                <asp:Literal runat="server" ID="litCategory"></asp:Literal></a></li>
        <li>
            <img src="/image/list-image.png" runat="server" visible="false" id="sep3" alt="" />
            <a href="#" runat="server" id="aProduct">
                <asp:Literal runat="server" ID="litProduct"></asp:Literal></a></li>
    </ul>
</div>
