<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CartView.aspx.cs" Inherits="FashionZone.Admin.Secure.Order.CartView" %>
<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div id="box">
        <h3>
            Active shopping carts</h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin-bottom: 10px;">
                 <asp:Label ID="lbl" runat="server">Cart</asp:Label>
                    <span></span>
                     <asp:DropDownList runat="server" ID="ddlGuid" AutoPostBack="true" Width="120" 
                        ViewStateMode="Enabled" onselectedindexchanged="ddlGuid_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="lblName" runat="server">Campaign</asp:Label>
                    <span></span>
                   <asp:DropDownList runat="server" ID="ddlCampaign" AutoPostBack="true" 
                        DataValueField="ID" DataTextField="Name" Width="140" ViewStateMode="Enabled" 
                        onselectedindexchanged="ddlCampaign_SelectedIndexChanged"></asp:DropDownList>
                    <span></span>
                    <asp:Label ID="lblNumber" runat="server">Customer</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtCustomer" runat="server" Width="100px"></asp:TextBox>
                    <span></span>
                    <asp:Label ID="lblDateFrom" runat="server">From</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtDateFrom" runat="server" Width="70px"></asp:TextBox>
                    <span></span>
                    <ajaxToolkit:CalendarExtender ID="calFromExtender" runat="server" TargetControlID="txtDateFrom"
                        FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                    <br />
                    <br />
                    <asp:Button ID="btnDeleteCart" runat="server" Text="Delete entire cart" ToolTip="Delete the selected cart" OnClick="btnDeleteCart_Click" />
                    <ajaxToolkit:ConfirmButtonExtender ID="btnConfirmDelEntire" runat="server" TargetControlID="btnDeleteCart"
                                    ConfirmText="Are you sure you want to delete the selected shopping cart with all its items?" />
                </div>
                <custom:FZGrid ID="gridCarts" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridCarts_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridCarts_PageIndexChanging" CustomVirtualItemCount="-1" 
                    CurrentPageIndex="0" SortOrder="Ascending" HeaderStyle-CssClass="GridHeader" DataKeyNames="ProdAttrID">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" >
                        </asp:BoundField>
                        <asp:BoundField DataField="DateAdded" HeaderText="Date" SortExpression="DateAdded"
                            DataFormatString="{0:g}" HtmlEncode="false" />
                        <asp:HyperLinkField HeaderText="Customer" DataTextField="CustomerName" DataNavigateUrlFields="CustomerID"
                            DataNavigateUrlFormatString="/Secure/Customer/CustomerNew.aspx?ID={0}" Target="_blank" SortExpression="CustomerName"/>
                        <asp:BoundField DataField="CampaignName" HeaderText="Campaign" SortExpression="CampaignName" />
                        <asp:HyperLinkField HeaderText="Product" DataTextField="ProductName" DataNavigateUrlFields="ProductID"
                            DataNavigateUrlFormatString="/Secure/Product/ProductNew.aspx?ID={0}" Target="_blank" SortExpression="ProductName"/>
                        <asp:BoundField DataField="UnitPrice" HeaderText="Price" />
                        <asp:BoundField DataField="Quantity" HeaderText="Q.ty" SortExpression="Quantity" />
                        <asp:BoundField DataField="ProductAttribute" HeaderText="Attr."/>
                        <asp:CheckBoxField DataField="FrontEnd" HeaderText="FE" SortExpression="FrontEnd" />
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" Text="Delete"
                                    CommandArgument='<%# Eval("ID") + "|" + Eval("ProdAttrID")%>' OnClick="lnkDelete_Click" />
                                <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                                    ConfirmText='<%# "Delete shopping cart " + Eval("ID") + "?"%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass"  />
                </custom:FZGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
