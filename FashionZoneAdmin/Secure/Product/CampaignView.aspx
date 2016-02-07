<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CampaignView.aspx.cs" Inherits="FashionZone.Admin.Secure.Product.CampaignView" %>

<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="box">
        <h3>
            Campaigns</h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin-bottom: 10px;">
                    <asp:Label ID="lblName" runat="server">Name</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <span></span>
                    <asp:Label ID="lblDateFrom" runat="server">From</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                    <span></span>
                    <ajaxToolkit:CalendarExtender ID="calFromExtender" runat="server" TargetControlID="txtDateFrom"
                        FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <span></span>
                    <asp:Label ID="lblDateTo" runat="server">To</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                    <span></span>
                    <ajaxToolkit:CalendarExtender ID="calToExtender" runat="server" TargetControlID="txtDateTo"
                        FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <asp:CheckBox runat="server" ID="chkActive" Text="Active" Checked="false" />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                </div>
                <custom:FZGrid ID="gridCampaign" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridCampaign_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridCampaign_PageIndexChanging" CustomVirtualItemCount="-1" SortOrder="Ascending"
                    HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="BrandName" HeaderText="Brand" SortExpression="BrandName"
                            HtmlEncode="False" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start" SortExpression="StartDate" />
                        <asp:BoundField DataField="EndDate" HeaderText="End" SortExpression="StartDate" />
                        <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="CampaignNew.aspx?ID={0}" />
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LoginView ID="lgnViewDelete" runat="server">
                                    <RoleGroups>
                                        <asp:RoleGroup Roles="Moderator, Administrator">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" Text="Delete"
                                                    CommandArgument='<%# Eval("ID")%>' OnClick="lnkDelete_Click" />
                                                <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                                                    ConfirmText='<%# "Delete campaign " + Eval("Name") + "?"%>' />
                                            </ContentTemplate>
                                        </asp:RoleGroup>
                                    </RoleGroups>
                                </asp:LoginView>
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
