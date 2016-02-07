<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BrandView.aspx.cs" Inherits="FashionZone.Admin.Secure.Product.BrandView" %>

<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="box">
        <h3>
            Brands</h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin-bottom:10px;">
                    <asp:Label ID="lblName" runat="server">Name</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <span></span>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
                <custom:FZGrid ID="gridBrands" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridBrands_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridBrands_PageIndexChanging" CustomVirtualItemCount="-1" SortOrder="Ascending" HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:BoundField DataField="ShowName" HeaderText="Show name" SortExpression="ShowName" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact" />
                        <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="BrandNew.aspx?ID={0}" />                       
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" Text="Delete" CommandArgument='<%# Eval("ID")%>' OnClick="lnkDelete_Click"/>
                                <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                                    ConfirmText='<%# "Are you sure about deleting brand " + Eval("Name") + "?"%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" CssClass="pagerClass" />
                </custom:FZGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
