<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BonusView.aspx.cs" Inherits="FashionZone.Admin.Secure.Customer.BonusView" %>
<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl" TagPrefix="bonus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="box">
    <h3>Bonus</h3>
    <div style="margin-bottom:10px;">
        <asp:Label ID="lblName" runat="server">Customer Name</asp:Label>
        <span></span>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <span></span>
        <asp:Label ID="lblDesc" runat="server">Description</asp:Label>
        <span></span>
        <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
        <span></span>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
    </div>
    <bonus:FZGrid ID="gridBonus" 
                  runat="server" 
                  AllowPaging="True" 
                  AllowSorting="True"
                  AutoGenerateColumns="False" 
                  OnSorting="gridCustomer_Sorting" 
                  ViewStateMode="Enabled"
                  OnPageIndexChanging="gridCustomer_PageIndexChanging" 
                  SortOrder="Ascending" 
                  CustomVirtualItemCount="-1" HeaderStyle-CssClass="GridHeader">
        <Columns>
            <asp:BoundField DataField="CustomerFullName" HeaderText="Customer" SortExpression="CustomerFullName" />
            <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
            <asp:BoundField DataField="ValueRemainder" HeaderText="Remainder" SortExpression="ValueRemainder" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
            <asp:BoundField DataField="DateAssigned" HeaderText="Date Assigned" SortExpression="DateAssigned" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
            <asp:BoundField DataField="Validity" HeaderText="Validity" SortExpression="Validity" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
             <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="BonusNew.aspx?IDBonus={0}" />
            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" 
                                    runat="server" 
                                    CausesValidation="False" 
                                    Text="Delete" 
                                    CommandArgument='<%# Eval("ID")%>' 
                                    OnClick="lnkDelete_Click"/>
                    <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                        ConfirmText='<%# "Delete bonus " + Eval("Description") + "?"%>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
    </bonus:FZGrid>

</div>
</asp:Content>
