<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerView.aspx.cs" Inherits="FashionZone.Admin.Secure.Customer.CustomerView" %>
<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div id="box">
        <h3>
            Customers</h3>
        <asp:UpdatePanel ID="updPanel" runat="server">
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
                <custom:FZGrid ID="gridCustomer" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridCustomer_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridCustomer_PageIndexChanging" SortOrder="Ascending" CustomVirtualItemCount="-1" HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                        <asp:CheckBoxField DataField="Newsletter" HeaderText="Newsletter" SortExpression="Newsletter" />
                        <asp:BoundField DataField="RegistrationDate" HeaderText="Date" SortExpression="RegistrationDate" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"/>
                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="CustomerNew.aspx?ID={0}" />
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" Text="Delete" CommandArgument='<%# Eval("ID")%>' OnClick="lnkDelete_Click"/>
                                <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                                    ConfirmText='<%# "Delete customer " + Eval("Name") + "?"%>' />
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
