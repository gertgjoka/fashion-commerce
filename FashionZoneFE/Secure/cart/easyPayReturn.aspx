<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="easyPayReturn.aspx.cs" Inherits="FashionZone.FE.Secure.cart.easyPayReturn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div align="center" style="min-height:350px; padding-top:40px;">
        <br />

        <asp:Label ID="lblResult" runat="server" Text="<%$Resources:Lang, PayPalThankyouLabel%>"></asp:Label>

        <br />
        <br />
        <b><asp:Label ID="lblPDT" runat="server" Visible="false"></asp:Label></b>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FootContent" runat="server">
</asp:Content>
