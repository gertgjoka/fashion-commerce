<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cart.ascx.cs" Inherits="FashionZone.FE.CustomControl.cart" %>
<center>
    <div class="myBoxRed">
        <img src="/image/cart.png" class="cart" alt="cart" />
        <h2>
            <asp:Label runat="server" ID="Label6" Text="<%$Resources:Lang, MyCartLabel%>"></asp:Label></h2>
        <asp:Repeater ID="rptDetails" runat="server" OnItemDataBound="rptDetails_ItemDataBound"
            OnItemCommand="rptDetails_ItemCommand" ViewStateMode="Enabled">
            <HeaderTemplate>
                <table id="tblParent" cellpadding="0" cellspacing="0" border="1" rules="none" >
                    <thead style="height: 20px; background-image: url('/image/red.jpg'); background-repeat: repeat-x; color:White;">
                        <tr>
                            <%--<th style="width: 20%">
                    </th>--%>
                            <th style="width: 50%" colspan="2">
                                <asp:Label runat="server" ID="Label6" Text="<%$Resources:Lang, ProductLabel%>"></asp:Label>
                            </th>
                            <th style="width: 12%">
                                <asp:Label runat="server" ID="lblQuantity" Text="<%$Resources:Lang, QuantityLabel%>"></asp:Label>
                            </th>
                            <th style="width: 12%">
                                <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, PriceLabel%>"></asp:Label>
                            </th>
                            <th style="width: 12%">
                                <asp:Label runat="server" ID="Label4" Text="<%$Resources:Lang, TotalLabel%>"></asp:Label>
                            </th>
                            <th style="width: 13%">
                                <asp:Label runat="server" ID="Label5" Text="<%$Resources:Lang, RemoveLabel%>"></asp:Label>
                            </th>
                        </tr>
                    </thead>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:HiddenField ID="ProdAttrID" runat="server" Value='<%# Eval("ProdAttrID") %>' />
                <tr>
                    <td >
                        <asp:ImageButton runat="server" ID="imgProduct" ClientIDMode="AutoID" ImageUrl='<%# Eval("ProductThumbnail") %>'
                            AlternateText="No Image" style="margin:5px;" />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lnkProduct" ClientIDMode="AutoID" Text='<%# Eval("BrandName") + " - " + Eval("ProductNameWithSize") %>'></asp:Label>
                    </td>
                    <td style="text-align:center;">
                        <asp:DropDownList runat="server" ID="ddlQty" ClientIDMode="AutoID" Width="70" ViewStateMode="Enabled"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlQty_SelectedIndexChanged" Visible="false">
                        </asp:DropDownList>
                        <asp:LinkButton runat="server" ID="lnkEdit" ClientIDMode="AutoID" CommandArgument='<%# Eval("ProdAttrID") %>'
                            Text='<%# Eval("Quantity") %>'></asp:LinkButton>
                    </td>
                    <td style="text-align:center;">
                        <asp:Label runat="server" ID="Label2" ClientIDMode="AutoID" Text='<%# Eval("UnitPrice") %>'></asp:Label>
                        €
                    </td>
                    <td style="text-align:center;">
                        <asp:Label runat="server" ID="Label3" ClientIDMode="AutoID" Text='<%# Eval("Amount") %>'></asp:Label>
                        €
                    </td>
                    <td style="vertical-align: middle;" align="center">
                        <asp:LinkButton runat="server" ID="lnkRemove" ClientIDMode="AutoID" CommandArgument='<%# Eval("ProdAttrID") %>' Text="<%$Resources:Lang, RemoveLabel%>"></asp:LinkButton>
                        <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkRemove"
                            ConfirmText='<%# Resources.Lang.RemoveProductQuestion + Eval("ProductNameWithSize") + "?"%>' />
                    </td>
                </tr>
            </ItemTemplate>
            <SeparatorTemplate>
                <tr>
                    <td colspan="6">
                        <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration">
                    </td>
                </tr>
            </SeparatorTemplate>
            <FooterTemplate>
                <tfoot>
                    <tr style="border-top:thin black; background-color:#F8EFEF; ">
                        <td colspan="4" style="text-align: left;">
                            <asp:Label runat="server" ID="lblQuantity" Text="<%$Resources:Lang, SubtotalLabel%>"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label runat="server" ID="lblSubTotal" ClientIDMode="AutoID">0</asp:Label>
                            €
                        </td>
                        <td></td>
                    </tr>
                </tfoot>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Label runat="server" ID="lblMessage"></asp:Label>
    </div>
</center>
