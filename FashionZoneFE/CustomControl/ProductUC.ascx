<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductUC.ascx.cs" Inherits="FashionZone.FE.CustomControl.ProductUC" %>
<asp:Repeater runat="server" ID="rptProduct">
    <ItemTemplate>
        <div class="DealEle">
            <ul class="DealElePrice">
                <li class="DealElePriceRed">€
                    <asp:Literal ID="litPrice" runat="server" Text='<%# Eval("OurPrice") %>'></asp:Literal></li>
                <li class="DealElePriceGrey">€
                    <asp:Literal ID="litOriginalPrice" runat="server" Text='<%# Eval("OriginalPrice") %>'></asp:Literal></li>
            </ul>
            <ul class="DealEleInfo">
                <li><asp:Literal ID="Literal1" runat="server" Text='<%# Eval("Name") %>'></asp:Literal></li>
            </ul>
            <div class="DealDisp">
                <asp:Label runat="server" ID="lblAvailability" Text='<%# availability(Eval("Remaining")) %>'></asp:Label>
            </div>
            <div class="DealImg">
                <a href='<%#"/product/" + FashionZone.BL.Util.Encryption.Encrypt(Eval("Id").ToString()) + "/"%>'
                    title='<%# Eval("Name") %>'>
                    <asp:Image runat="server" ID="imgProductImage" ClientIDMode="AutoID" ImageUrl='<%# Eval("ImageForList") %>'
                        AlternateText='<%# Eval("Name") %>' /></a>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
