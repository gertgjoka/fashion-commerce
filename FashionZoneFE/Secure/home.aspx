<%@ Page Title="FZone" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="home.aspx.cs" Inherits="FashionZone.FE.Home" %>

<%@ Register Src="~/CustomControl/CampaignUC.ascx" TagPrefix="custom" TagName="campaign" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="ContentDealHome" runat="server" id="divActualDeals">
        <div class="SFRed">
            <asp:Label runat="server" ID="lblactualCampaigns" Text="<%$Resources:Lang, ActualCampaignsLabel%>"></asp:Label>
        </div>
        <div class="opacity25">
            <custom:campaign ID="rptActualCampaigns" runat="server" />
        </div>
    </div>
    <!--end content deal home-->

    <div class="BoxSxHome">
        <div class="Ozone">
<%--            <span style="font-size:larger; color:black; padding-top:5px;"><b>Oferta e fundvitit: Paguaj 1 merr 2!</b></span>
            <br />--%>
            <br />
            <a href="http://www.ozone.al" target="_blank" title="Ozone">
                <img src="/image/ozone.jpg" alt="Ozone Partner"  style="padding-bottom:10px;"/></a>
            <span style="font-size:larger; color:black; padding-top:5px;"><b>Partneret</b></span>
            <br />
            <a href="http://www.easypay.al" target="_blank" title="EasyPay">
                <img src="/image/logo.png" alt="Ozone Partner" width="100"  style="margin-right:20px;"/></a>

             <a href="http://www.kinemamillennium.com" target="_blank" title="Kinema Millennium">
                <img src="/image/logo_millennium.png" alt="Ozone Partner" width="100" style="margin-left:20px;"/></a>
        </div>
    </div>

    <div class="NextDealHome" runat="server" id="divFutureDeals">
        <div class="SFBlu">
            <asp:Label runat="server" ID="lblCampaignSoon" Text="<%$Resources:Lang, CampaignSoonLabel%>"></asp:Label>
        </div>
        <div class="opacity25Next">
            <custom:campaign ID="rptFutureCampaigns" runat="server" />
        </div>
    </div>
    <!--end next deal home-->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FootContent" runat="server">
</asp:Content>
