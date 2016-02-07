<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CampaignUC.ascx.cs"
    Inherits="FashionZone.FE.CustomControl.CampaignUC" %>
<asp:Repeater runat="server" ID="rptCampaign">
    <ItemTemplate>
        <div class="Deal">
            <a href='<%# Url(Eval("Active")) %>' title='<%# Eval("BrandName") %>'>
                <asp:Image runat="server" ID="imgCampaignImage" ClientIDMode="AutoID" ImageUrl='<%# Eval("ImageHomeWithPath") %>'
                    AlternateText='<%# Eval("BrandName") %>' /></a>
            <ul>
                <li>
                <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, FromLabel%>" Visible='<%# !(bool)Eval("Active") %>'></asp:Label>
                    <b><asp:Label runat="server" ID="Label2" Text='<%# ((DateTime)Eval("StartDate")).ToString("dd/MM/yyyy")%>' Visible='<%# !(bool)Eval("Active") %>'></asp:Label></b>
                    &nbsp;
                    <asp:Label runat="server" ID="lblTill" Text="<%$Resources:Lang, TillLabel%>"></asp:Label>
                    <b><asp:Label runat="server" ID="lblTillDate" Text='<%# ((DateTime)Eval("EndDate")).ToString("dd/MM/yyyy")%>'></asp:Label></b>
                    <img src="/image/vline.png" alt="vline" class="vline" />
                </li>
                <li><a runat="server" href='<%#"/campaign/" + FashionZone.BL.Util.Encryption.Encrypt(Eval("Id").ToString()) + "/"%>'
                    title='<%# Eval("BrandName") %>' class="red" visible='<%# (bool)Eval("Active") %>'>
                    <asp:Label runat="server" ID="lblAccessSaleLabel" Text="<%$Resources:Lang, AccessSaleLabel%>"
                        Visible='<%# (bool)Eval("Active") %>'></asp:Label></a></li>
                <li>
                    <img src="/image/vline.png" runat="server" alt="vline" class="vline" visible='<%# (bool)Eval("Active") %>' />
                </li>
                <li>
                    <img src="/image/mail.jpg" alt="email" style="padding: 0 0.2em;" /></li>
                <li>
                    <img src="/image/vline.png" alt="vline" style="padding: 0 0.2em;" /></li>
                <li>
                    <img src="/image/like.png" alt="like" style="padding: 0 0.2em;" /></li>
            </ul>
        </div>
    </ItemTemplate>
</asp:Repeater>
