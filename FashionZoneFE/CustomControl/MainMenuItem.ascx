<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainMenuItem.ascx.cs"
    Inherits="FashionZone.FE.CustomControl.MainMenuItem" %>
<a id="a1" href="/" runat="server" clientidmode="AutoID">
    <asp:Label runat="server" ID="lblName" ClientIDMode="AutoID"><%= ItemName%></asp:Label></a>
<asp:Panel runat="server" ID="pnlBrands" ClientIDMode="AutoID" CssClass="SubMenu" ViewStateMode="Enabled">
    <br />
    <asp:Repeater runat="server" ID="rptBrands">
        <ItemTemplate>
            <a runat="server" id="aBrand" href='<%# "/campaign/" + FashionZone.BL.Util.Encryption.Encrypt(Eval("Id").ToString()) + "/" %>'
                style="border: 0; color: Black; display:list-item;">
                <asp:Label runat="server" ID="lblBrand" Text='<%# Eval("BrandName") %>'></asp:Label>
            </a>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
<ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="lblName"
    PopupControlID="pnlBrands" PopupPosition="Bottom" ClientIDMode="AutoID">
</ajaxToolkit:HoverMenuExtender>
