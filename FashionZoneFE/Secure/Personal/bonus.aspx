<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="bonus.aspx.cs"
    Inherits="FashionZone.FE.Secure.Personal.bonus" %>

<%@ Import Namespace="Resources" %>
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
            <asp:Label runat="server" ID="lblBonusTit" Text="<%$Resources:Lang, BonusLabel%>"></asp:Label>
        </a>
        <div class="clearer">
        </div>
        <div class="Tab">
            <img src="/image/buoni.png" alt="Bonus" class="carrellogrey" />
            <asp:Repeater ID="RepeatBonus" runat="server">
                <HeaderTemplate>
                    <table class="TabTable">
                        <tr class="TabTableRed">
                            <td>
                                <asp:Label runat="server" ID="lblMyAddresesHead" Text="<%$Resources:Lang, DescriptionLabel%>">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, ValueLabel%>">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label5" Text="<%$Resources:Lang, AvailableBonusLabel%>">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label2" Text="<%$Resources:Lang, AssignedDateLabel%>">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label4" Text="<%$Resources:Lang, ExpirationDate%>">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label3" Text="<%$Resources:Lang, ValidityLabel%>">
                                </asp:Label>
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="TabTableGrey">
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Description")%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Value")%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "ValueRemainder")%>
                        </td>
                        <td>
                            <%# string.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "DateAssigned"))%>
                        </td>
                        <td>
                            <%# string.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "Validity"))%>
                        </td>
                        <td class="Red">
                            <%# (DateTime.Parse(Eval("Validity").ToString())) >= DateTime.Now ? "<span Class=\"Red\">Y</span>" : "<span Class=\"Red\">N</<span>"%>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                </ItemTemplate>
                <SeparatorTemplate>
                    <td colspan="4">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </SeparatorTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <!--fine tab-->
    </div>
</asp:Content>
