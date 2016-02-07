<%@ Page Title="" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true"
    CodeBehind="UserView.aspx.cs" Inherits="FashionZone.Admin.Users.UserView" %>

<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="box">
        <h3>
            Users</h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin-bottom:10px;">
                    <asp:Label ID="lblName" runat="server">Name</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <span></span>
                    <asp:Label ID="lblEmail" runat="server">Email</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <span></span>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
                <custom:FZGrid ID="gridUser" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridUser_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridUser_PageIndexChanging" SortOrder="Ascending" CustomCustomVirtualItemCount="-1" HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Login" HeaderText="Login" SortExpression="Login" />
                        <asp:CheckBoxField DataField="Enabled" HeaderText="Enabled" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="UserNew.aspx?ID={0}" />
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" Text="Delete" CommandArgument='<%# Eval("ID")%>' OnClick="lnkDelete_Click"/>
                                <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                                    ConfirmText='<%# "Are you sure about deleting user " + Eval("Name") + "?"%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                </custom:FZGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
