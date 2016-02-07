<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="friends.aspx.cs"
    Inherits="FashionZone.FE.Secure.Personal.friends" %>

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
            <asp:Label runat="server" ID="lblFriends" Text="<%$Resources:Lang, FriendsLabel%>"></asp:Label>
        </a>
        <div class="clearer">
        </div>
        <div class="Tab">
            <img src="/image/amici.png" alt="Friends" class="carrellogrey" />
            <asp:Repeater ID="RepeatFriends" runat="server">
                <HeaderTemplate>
                    <table class="TabTable">
                        <tr class="TabTableRed">
                            <td>
                                <asp:Label runat="server" ID="lblMyAddresesHead" Text="<%$Resources:Lang, FriendsLabel%>">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, RegisteredLabel%>">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label2" Text="<%$Resources:Lang, RegistrationDateLabel%>">
                                </asp:Label>
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="TabTableGrey">
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "InvitedMail")%>
                        </td>
                        <td>
                            <%# (Eval("Registered") != null && Boolean.Parse(Eval("Registered").ToString())) ? "<span Class=\"Red\">Yes</span>" : "<span Class=\"Red\">No</<span>"%>
                        </td>
                        <td>
                            <%# string.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "RegistrationDate"))%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <img src="/image/linetrasparent.png" alt="Decoration" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="MyButton">
                <asp:LinkButton ID="btnInvite" CssClass="BtnRed" runat="server" Text="<%$Resources:Lang, InviteLabel%>"
                    OnClick="btnInvite_Click" />
            </div>
            <br />
            <br />
            <%--
            <table class="TabTable">
                <tr class="TabTableRed">
                    <td>
                        Amici
                    </td>
                    <td>
                        Stato registrazione
                    </td>
                    <td>
                        Data
                    </td>
                </tr>
                <tr class="TabTableGrey">
                    <td>
                        Pinco Pallo
                    </td>
                    <td class="Red">
                        Attivo
                    </td>
                    <td>
                        12/07/2012
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </tr>
            </table>
            --%>
        </div>
        <!--fine tab-->
    </div>
</asp:Content>
