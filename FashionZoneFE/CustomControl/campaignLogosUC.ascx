<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="campaignLogosUC.ascx.cs" Inherits="FashionZone.FE.CustomControl.campaignLogosUC" %>
<asp:Repeater runat="server" ID="rptCampaign">
    <ItemTemplate>
        <div class="CampEle">
                <asp:Image runat="server" ID="imgCampaignImage" ClientIDMode="AutoID" ImageUrl='<%# Eval("LogoWithPath") %>'
                    AlternateText='<%# Eval("BrandName") %>' Width="120" Height="52"/>
            </ul>
        </div>
    </ItemTemplate>
</asp:Repeater>