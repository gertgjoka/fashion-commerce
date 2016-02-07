<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="mycart.aspx.cs" Inherits="FashionZone.FE.Secure.cart.mycart" EnableViewStateMac="true"
    ViewStateEncryptionMode="Always" EnableEventValidation="true" %>

<%@ Register Src="~/CustomControl/cart.ascx" TagPrefix="custom" TagName="cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/js/jquery-1.8.1.min.js"></script>
    <script type="text/javascript">
        countdown();
        function countdown() {
            $(function () {
                var number = $('#lblCartExp').text();
                if (number > 0) {
                    // set to change every minute
                    setTimeout(countdown, 60000);
                    number--;
                    $('#lblCartExp').text(number);
                }
            }
        );
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearer">
        <br />
    </div>

    <div style="text-align: center; min-height: 400px;">
        <asp:Panel CssClass="counterBlock" runat="server" ID="divClock" Visible="false" ViewStateMode="Enabled">
            <img src="../../image/faq4.png" alt="" width="50" height="50" />
            <br />
            <asp:Label runat="server" ID="Label4" Text="<%$Resources:Lang, CartExpiresInLabel%>"></asp:Label>
            <asp:UpdatePanel ID="updMinutes" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                <ContentTemplate>
                    <b>
                        <asp:Label runat="server" ID="lblCartExp" ClientIDMode="Static" ViewStateMode="Enabled"></asp:Label>
                    </b>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Label runat="server" ID="Label6" Text="<%$Resources:Lang, MinutesLabel%>"></asp:Label>
            <br />
            <asp:Label runat="server" ID="Label7" Text="<%$Resources:Lang, CartExpiringInfo%>"></asp:Label>
        </asp:Panel>
        <custom:cart runat="server" ID="cartControl" ViewStateMode="Enabled" OnNeedRefresh="cart_NeedRefresh"></custom:cart>


        <br />
        <center>
            <asp:Panel CssClass="actionBlock" runat="server" ID="divAction" Visible="false" ViewStateMode="Enabled">
                <div style="float: left; margin-left: 10px; color: #C33; margin-top:10px;">
                    <asp:LinkButton ID="lnkCancel" runat="server" Text="<%$Resources:Lang, CancelCartLabel%>"
                        OnClick="lnkCancel_Click"></asp:LinkButton>
                    <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkCancel"
                        ConfirmText='<%$Resources:Lang, CancelCartQuestion%>' />
                </div>
                <div style="float: right; margin-right: 2px; margin-top: 4px;">
                    <div class="MyButton2">
                        <asp:LinkButton runat="server" ID="lnkCheckout" Text='<%$Resources:Lang, CheckoutLabel %>'
                            ViewStateMode="Enabled" OnClick="lnkCheckout_Click"></asp:LinkButton>
                    </div>
                </div>
                <div style="float: right; margin-right: 10px; color: #C33; margin-top:10px;">
                    <a href="/home/">
                        <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, ContinueShoppingLabel%>"></asp:Label></a>
                </div>
            </asp:Panel>
        </center>
        <br />
        <br />
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FootContent" runat="server">
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="fake"
        Drag="false" PopupControlID="Dialog_ChargeFilter" Enabled="True" BackgroundCssClass="modalBackground" />
    <asp:HiddenField ID="fake" runat="server" />
    <asp:Panel ID="Dialog_ChargeFilter" runat="server" Width="400" CssClass="modalPopup">
        <asp:Panel ID="DialogHeaderFrame" CssClass="DialogHeaderFrame" runat="server" Width="400px">
            <asp:Panel ID="DialogHeader" runat="server" CssClass="DialogHeader">
                &nbsp;<asp:Label ID="LblPopupHeader" runat="server" Text="<%$Resources:Lang, InformationLabel%>" />
            </asp:Panel>
        </asp:Panel>
        <asp:UpdatePanel ID="updModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:Label runat="server" ID="Label15" Text="<%$Resources:Lang, CartExpired%>"></asp:Label>
                </div>
                <br />
                <div align="center">
                    <asp:Button ClientIDMode="Static" ID="btnOk" Text="Ok" ToolTip="close filter-dialog"
                        CausesValidation="false" Width="70px" runat="server" OnClick="btnOk_Click" /><br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
