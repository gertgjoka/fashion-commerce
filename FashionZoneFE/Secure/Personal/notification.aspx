<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="notification.aspx.cs"
    Inherits="FashionZone.FE.Secure.Personal.notification" %>

<%@ Register Src="../../CustomControl/LeftSidePanelPersInfo.ascx" TagName="LeftSidePanelPersInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 130px;
        }
        .stypeRBL
        {
            margin-left: 25px;
        }
    </style>
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
            <asp:Label runat="server" ID="lblNotificationHead" Text="<%$Resources:Lang, NotificationHeadLabel%>"></asp:Label>
        </a>
        <div class="clearer">
        </div>
        <div class="Tab">
            <table class="TablePromemoria">
                <tr>
                    <td class="style1">
                        <img src="/image/daily.png" alt="Daily" border="0" />
                    </td>
                    <td class="TxtPromemoria">
                        <asp:Label runat="server" ID="lblNotifTextDaily" Text="<%$Resources:Lang, NotifTextDailyLabel%>"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="radioDaily" runat="server" CssClass="stypeRBL">
                            <asp:ListItem class="RadioOn" Text="<%$Resources:Lang, YesLabel%>" Value="True"></asp:ListItem>
                            <asp:ListItem class="RadioOff" Text="<%$Resources:Lang, NoLabel%>" Value="False"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <%-- <tr>
                    <td colspan="3">
                        <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <img src="/image/weekly.png" alt="Weekly" border="0" />
                    </td>
                    <td class="TxtPromemoria">
                        <asp:Label runat="server" ID="lblNotifTextWeekly" Text="<%$Resources:Lang, NotifTextWeeklyLabel%>"></asp:Label>                        
                    </td>
                    <td>
                        <asp:RadioButtonList id="RadioButtonList1" runat="server" CssClass="stypeRBL"> 
                            <asp:ListItem class="RadioOn" selected="true" Text="<%$Resources:Lang, YesLabel%>"></asp:ListItem> 
                            <asp:ListItem class="RadioOff" Text="<%$Resources:Lang, NoLabel%>"></asp:ListItem> 
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <img src="/image/special.png" alt="Special" border="0" />
                    </td>
                    <td class="TxtPromemoria">
                        <asp:Label runat="server" ID="lblNotifTextSpecial" Text="<%$Resources:Lang, NotifTextSpecialLabel%>"></asp:Label>
                    </td>
                    <td>
                        
                        <asp:RadioButtonList id="rdbSpecialActivation" runat="server" CssClass="stypeRBL"> 
                            <asp:ListItem class="RadioOn" selected="true" Text="<%$Resources:Lang, YesLabel%>"></asp:ListItem> 
                            <asp:ListItem class="RadioOff" Text="<%$Resources:Lang, NoLabel%>"></asp:ListItem> 
                        </asp:RadioButtonList>
                        <div class="RadioOn">
                            Attivo<input type="radio" /></div>
                        <div class="RadioOff">
                            Inattivo<input type="radio" /></div>
                        
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="3">
                        <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Literal runat="server" ID="litError" ViewStateMode="Enabled"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div class="MyButton">
                <asp:LinkButton ID="btnSave" runat="server" Text="<%$Resources:Lang, SaveLabel%>"
                    OnClick="btnSave_Click" />
            </div>
            <div class="clearer">
            </div>
        </div>
        <!--fine tab-->
    </div>
</asp:Content>
