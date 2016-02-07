<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="order.aspx.cs"
    Inherits="FashionZone.FE.Secure.Personal.order" EnableViewStateMac="true" EnableEventValidation="true" %>

<%@ Register Src="../../CustomControl/LeftSidePanelPersInfo.ascx" TagName="LeftSidePanelPersInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearer">
    </div>
    <uc1:LeftSidePanelPersInfo ID="LeftSidePanelPersInfo1" runat="server" />
    <div class="Dx">
        <div class="DxTit">
            <asp:Label runat="server" ID="lblMyAccount" Text="<%$Resources:Lang, MyAccountLabel%>"></asp:Label>
        </div>
        <a href="#" class="TabTitle">
            <asp:Label runat="server" ID="lblBonusTit" Text="<%$Resources:Lang, MyOrdersLowerLabel%>"></asp:Label>
        </a>
        <div class="clearer">
        </div>
        <div class="Tab">
            <p>
                <asp:Label runat="server" ID="lblOrderText" Text="<%$Resources:Lang, MyOrdersTextLabel%>"></asp:Label>
            </p>
            <asp:Repeater ID="RepeatBonus" runat="server">
                <HeaderTemplate>
                    <table class="TabTable">
                        <tr class="TabTableRed">
                            <td>
                                <asp:Label runat="server" ID="lblCampaignTit" Text="<%$Resources:Lang, CampaignLabel%>"></asp:Label>
                            </td>
                            <td>
                                N°
                                <asp:Label runat="server" ID="lblOrderidTit" Text="<%$Resources:Lang, OrderLabel%>"></asp:Label>
                            </td>
                            <td>
                                Data
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblStateTit" Text="<%$Resources:Lang, StateLabel%>"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblAmountTit" Text="<%$Resources:Lang, AmountLabel%>"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblDettailsTit" Text="<%$Resources:Lang, DetailLabel%>"></asp:Label>
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="TabTableGrey">
                        <td>
                            <%# campaignsString(Eval("ID"))%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "ID")%>
                        </td>
                        <td>
                            <%# string.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "DateCreated"))%>
                        </td>
                        <td class="Red">
                            <%# DataBinder.Eval(Container.DataItem, "StatusName")%>
                        </td>
                        <td>
                            €
                            <%# DataBinder.Eval(Container.DataItem, "AmountPaid")%>
                        </td>
                        <td>
                            <a href='<%# "/personal/orderDet/" + FashionZone.BL.Util.Encryption.Encrypt(DataBinder.Eval(Container.DataItem, "ID").ToString())%>'
                                class="SFRedSmall">+ info</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <SeparatorTemplate>
                    <tr>
                        <td colspan="6">
                            <img src="/image/linetrasparent.png" alt="Decoration" />
                        </td>
                    </tr>
                </SeparatorTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
