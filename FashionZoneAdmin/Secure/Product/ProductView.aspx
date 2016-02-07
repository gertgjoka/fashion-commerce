<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductView.aspx.cs" Inherits="FashionZone.Admin.Secure.Product.ProductView"
    ViewStateMode="Disabled" %>

<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="box">
        <h3>
            Products</h3>
        <asp:UpdatePanel ID="updPanel" runat="server">
            <ContentTemplate>
                <div style="margin-bottom: 10px">
                    <asp:Label ID="lblName" runat="server">Name</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <span></span>
                    <asp:Label ID="lblDescription" runat="server">Desc.</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                     <span></span>
                    <asp:Label ID="lblCode" runat="server">Code</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                    <span></span>
                    <asp:Label ID="Label1" runat="server">Campaign</asp:Label>
                    <span></span>
                    <asp:DropDownList runat="server" ID="ddlCampaign" DataValueField="ID" DataTextField="Name"
                        Width="140" ViewStateMode="Enabled">
                    </asp:DropDownList>
                    <span></span>
                    <asp:CheckBox runat="server" ID="chkActive" Text="Active" Checked="false" />
                    <span></span>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
                <custom:FZGrid ID="gridProduct" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridProduct_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridProduct_PageIndexChanging" SortOrder="Ascending" CustomVirtualItemCount="-1"
                    CurrentPageIndex="0" HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:ImageField DataImageUrlField="Thumbnail" NullDisplayText="No Image">
                            <ItemStyle Wrap="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:ImageField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Remaining" HeaderText="Avail." ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="InitialQuantity" HeaderText="Q.ty" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="OurPrice" HeaderText="Price" SortExpression="OurPrice"
                            DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                        <%--                        <asp:BoundField DataField="OriginalPrice" HeaderText="R. Price" SortExpression="OriginalPrice"
                            DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />--%>
                        <asp:BoundField DataField="Discount" HeaderText="%" SortExpression="Discount" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Campaign" HeaderText="Campaign" />
                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="ProductNew.aspx?ID={0}" />
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <%--<asp:LoginView ID="lgnViewDelete" runat="server">
                                    <RoleGroups>
                                        <asp:RoleGroup Roles="Moderator, Administrator">
                                            <ContentTemplate>--%>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" Text="Delete" CommandArgument='<%# Eval("ID")%>' OnClick="lnkDelete_Click" />
                                <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                                    ConfirmText='<%# "Are you sure about deleting product " + Eval("Name") + "?"%>' />
                                <%-- </ContentTemplate>
                                        </asp:RoleGroup>
                                    </RoleGroups>
                                </asp:LoginView>--%>
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
