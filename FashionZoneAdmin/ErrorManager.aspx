<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ErrorManager.aspx.cs" Inherits="FashionZone.Admin.ErrorManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="contentRight">
        <p>
            <asp:Literal ID="lbl_Error" runat="server"></asp:Literal>
        </p>
    </div>
</asp:Content>
