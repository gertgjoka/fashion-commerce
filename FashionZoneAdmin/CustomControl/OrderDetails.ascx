<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetails.ascx.cs"
    Inherits="FashionZone.Admin.CustomControl.OrderDetails" ViewStateMode="Enabled" %>
<asp:Repeater ID="rptDetails" runat="server" OnItemDataBound="rptDetails_ItemDataBound"
    OnItemCommand="rptDetails_ItemCommand">
    <HeaderTemplate>
        <table id="tblParent" cellpadding="0" cellspacing="0" width="400" border="0">
            <thead>
                <tr>
                    <%--<th style="width: 20%">
                    </th>--%>
                    <th style="width: 90%" colspan="3">
                        Product
                    </th>
                    <%--<th style="width: 35%">
                    </th>--%>
                    <th style="width: 10">
                    </th>
                </tr>
            </thead>
    </HeaderTemplate>
    <ItemTemplate>
    <asp:HiddenField ID="ProdAttrID" runat="server" Value='<%# Eval("ProdAttrID") %>' />
        <tr>
            <td rowspan="3">
                <asp:ImageButton runat="server" ID="imgProduct" ClientIDMode="AutoID" ImageUrl='<%# Eval("ProductThumbnail") %>'
                    AlternateText="No Image" />
            </td>
            <td colspan="2">
                <asp:LinkButton runat="server" ID="lnkProduct" ClientIDMode="AutoID" Text='<%# Eval("BrandName")+ " - " + Eval("ProductNameWithSize") %>'></asp:LinkButton>
            </td>
            <td style="vertical-align: middle;" rowspan="2" align="center">
                 <asp:LinkButton runat="server" ID="lnkRemove" ClientIDMode="AutoID" CommandArgument='<%# Eval("ProdAttrID") %>' Visible='<%# !(bool)ReadOnly %>'>Remove</asp:LinkButton>
                 <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkRemove"
                                    ConfirmText='<%# "Remove product " + Eval("ProductNameWithSize") + "?"%>' />
            </td>
        </tr>
        <tr>
            <td>
                Size:
                <asp:DropDownList runat="server" ID="ddlSize" ClientIDMode="AutoID" Width="70" Enabled="false"
                    DataTextField="Value" DataValueField="ID" ViewStateMode="Enabled">
                </asp:DropDownList>
            </td>
            <td>
                Q.ty:
                <asp:DropDownList runat="server" ID="ddlQty" ClientIDMode="AutoID" Width="70" Enabled="false"
                    ViewStateMode="Enabled" AutoPostBack="true" OnSelectedIndexChanged="ddlQty_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:LinkButton runat="server" ID="lnkEdit" ClientIDMode="AutoID" CommandArgument='<%# Eval("ProdAttrID") %>' Visible='<%# !(bool)ReadOnly %>'>Edit</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Unit price:
                <asp:Label runat="server" ID="lblPrice" ClientIDMode="AutoID" Text='<%# Eval("UnitPrice") %>'></asp:Label>
            </td>
            <td align="right">
                <asp:Label runat="server" ID="Label1" ClientIDMode="AutoID" Text='<%# Eval("Amount") %>'></asp:Label>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        <tfoot>
            <tr>
                <td colspan="3">
                    Subtotal
                </td>
                <td align="right">
                    <asp:Label runat="server" ID="lblSubTotal" ClientIDMode="AutoID">0</asp:Label>
                </td>
            </tr>
        </tfoot>
        </table>
    </FooterTemplate>
</asp:Repeater>
<br />
