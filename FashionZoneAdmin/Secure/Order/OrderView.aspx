<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderView.aspx.cs" Inherits="FashionZone.Admin.Secure.Order.OrderView" %>

<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="box">
        <h3>Orders</h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin-bottom: 10px;">
                    <asp:Label ID="lblName" runat="server">Name</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtName" runat="server" Width="100px"></asp:TextBox>
                    <span></span>
                    <asp:Label ID="lblNumber" runat="server">Number</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtNumber" runat="server" Width="50px"></asp:TextBox>
                    <span></span>
                    <asp:Label ID="lblDateFrom" runat="server">From</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtDateFrom" runat="server" Width="80px"></asp:TextBox>
                    <span></span>
                    <ajaxToolkit:CalendarExtender ID="calFromExtender" runat="server" TargetControlID="txtDateFrom"
                        FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <span></span>
                    <asp:Label ID="lblDateTo" runat="server">To</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtDateTo" runat="server" Width="80px"></asp:TextBox>
                    <span>&nbsp;</span>
                    Status<span></span>
                    <asp:DropDownList runat="server" ID="ddlStatus" TabIndex="20" DataValueField="ID"
                        DataTextField="Name" ViewStateMode="Enabled" Width="100">
                    </asp:DropDownList>
                    <ajaxToolkit:CalendarExtender ID="calToExtender" runat="server" TargetControlID="txtDateTo"
                        FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <span>&nbsp;</span>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                </div>
                <custom:FZGrid ID="gridOrders" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridOrders_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridOrders_PageIndexChanging" CustomVirtualItemCount="-1"
                    CurrentPageIndex="0" SortOrder="Ascending" HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="Number" SortExpression="ID">
                            <ItemStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DateCreated" HeaderText="Date" SortExpression="DateCreated"
                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:HyperLinkField DataTextField="CustomerName" HeaderText="Customer" SortExpression="CustomerName" DataNavigateUrlFields="CustomerID"
                            DataNavigateUrlFormatString="/Secure/Customer/CustomerNew.aspx?ID={0}" Target="_blank" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total"
                            SortExpression="TotalAmount">
                            <ItemStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="Status" />
                        <asp:CheckBoxField DataField="Verified" HeaderText="Veri." SortExpression="Verified" />
                        <asp:CheckBoxField DataField="Completed" HeaderText="Compl." SortExpression="Completed" />
                        <asp:CheckBoxField DataField="Canceled" HeaderText="Canc." SortExpression="Canceled" />

                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="OrderNew.aspx?ID={0}" />
                        <%-- <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" Text="Delete"
                                    CommandArgument='<%# Eval("ID")%>' OnClick="lnkDelete_Click" />
                                <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                                    ConfirmText='<%# "Delete order " + Eval("ID") + "?"%>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                </custom:FZGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
