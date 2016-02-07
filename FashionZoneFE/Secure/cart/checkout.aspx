<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="checkout.aspx.cs" Inherits="FashionZone.FE.Secure.cart.checkout"
    EnableViewStateMac="true" ViewStateEncryptionMode="Always" EnableEventValidation="true" %>

<%@ Register Src="../../CustomControl/CustomerAddress.ascx" TagPrefix="custom" TagName="address" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/js/jquery-1.8.1.min.js"></script>
    <%-- <script type="text/javascript">
        bindRadio();
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearer">
    </div>
    <div class="MyAccSx">
        <div class="boxRed">
            <img src="/image/cart.png" class="cart" alt="" />
            <h3>
                <asp:Label runat="server" ID="Label6" Text="<%$Resources:Lang, MyCartLabel%>"></asp:Label></h3>
            <img src="/image/linetrasparent200.png" alt="" />
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Repeater ID="rptDetails" runat="server" ViewStateMode="Enabled">
                            <HeaderTemplate>
                                <table id="tblParent" style="border: 0;" cellpadding="0" cellspacing="0" width="290">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td colspan="2" style="font-size: larger;">
                                        <asp:Label runat="server" ID="lblCampaign" Text='<%# Eval("BrandName") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, DeliveryLabel%>"></asp:Label>
                                        <%--</td>
                                    <td class="Red">--%>
                                        <asp:Label runat="server" ID="lblDelivery" Text='<%# Eval("CampaignDelivery") %>'
                                            CssClass="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CartProduct">
                                        <img id="Img1" src='<%# Eval("ProductThumbnail") %>' alt="Image" runat="server" />
                                    </td>
                                    <td>
                                        <a title="Product detail">
                                            <asp:Label runat="server" ID="Label2" Text='<%# Eval("ProductNameWithSize") %>'></asp:Label></a>
                                        <br />
                                        <asp:Label runat="server" ID="lblQuantity" Text="<%$Resources:Lang, QuantityLabel%>"></asp:Label>:
                                        <asp:Label runat="server" ID="Label11" Text='<%# Eval("Quantity") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Ppu" colspan="1">
                                        <asp:Label runat="server" ID="Label3" Text="<%$Resources:Lang, PriceLabel%>"></asp:Label>
                                        :
                                        <asp:Label runat="server" ID="Label4" ClientIDMode="AutoID" Text='<%# Eval("UnitPrice") %>'></asp:Label>
                                        €
                                    </td>
                                    <td style="text-align: right;">
                                        <span class="Ppt1">
                                            <asp:Label runat="server" ID="Label5" ClientIDMode="AutoID" Text='<%# Eval("Amount") %>'></asp:Label>
                                            €</span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <tr>
                                    <td colspan="2">
                                        <img src="/image/linetrasparent200.png" alt="" />
                                    </td>
                                </tr>
                            </SeparatorTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="2">
                                        <img src="/image/linetrasparent200.png" alt="" />
                                    </td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel runat="server" ID="updTotal">
                            <ContentTemplate>
                                <table width="290">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label runat="server" ID="Label7" Text="<%$Resources:Lang, SubtotalLabel%>"></asp:Label>
                                        </td>
                                        <td class="Red" style="text-align: right;">
                                            <asp:Label runat="server" ID="lblSubTotal" ClientIDMode="AutoID"><%=TotalAmount.Value.ToString("N2") %></asp:Label>
                                            €
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label runat="server" ID="Label8" Text="<%$Resources:Lang, ShippingCostLabel%>"></asp:Label>
                                        </td>
                                        <td class="Red" style="text-align: right;">
                                            <asp:Label runat="server" ID="lblShippingCost" ClientIDMode="AutoID"><%=ShipCost.ToString("N2") %></asp:Label>
                                            €
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="Label9" Text="<%$Resources:Lang, BonusLabel%>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlBonus" DataValueField="ID" DataTextField="BonusString" ViewStateMode="Enabled"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlBonus_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Red" style="text-align: right;">
                                            <asp:Label runat="server" ID="lblBonus" ClientIDMode="AutoID"><%= BonusUsed.ToString("N2") %></asp:Label>
                                            €
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Red" colspan="2">
                                            <asp:Label runat="server" ID="Label10" Text="<%$Resources:Lang, TotalLabel%>"></asp:Label>
                                        </td>
                                        <td class="Ppt" style="text-align: right;">
                                            <asp:Label runat="server" ID="lblTotal" ClientIDMode="AutoID"><%=TotalOrder.ToString("N2") %></asp:Label>
                                            €
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--End Sx my account-->
    <div class="Dx">
        <a class="TabTitle" id="titleTabAddress">
            <asp:Label runat="server" ID="Label12" Text="<%$Resources:Lang, DeliveryAddressLabel%>"></asp:Label></a>
        <div class="clearer">
        </div>
        <div class="Tab" id="tabAddresses">
            <img src="/image/datipersonali.png" alt="carrello" class="carrellogrey" />
            <asp:UpdatePanel ID="updAddresses" runat="server" ViewStateMode="Enabled">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td></td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlShippingAddress" Width="400" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlShippingAddress_SelectedIndexChanged" DataTextField="AddressSummary"
                                    DataValueField="ID" ViewStateMode="Enabled">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="bAddress" runat="server" visible="false">
                                    <custom:address ID="address1" runat="server" Visible="false" InOrder="true" OnSaving="address1_Saving"
                                        OnAddressAsyncRemove="address_OnAddressAsyncRemove" />
                                </div>
                                <img id="Img2" src="/image/more.png" runat="server" alt="<%$Resources:Lang, AddAddressLabel%>" />
                                <asp:LinkButton ID="lnkAddAddress" runat="server" Text="<%$Resources:Lang, AddAddressLabel%>"
                                    OnClick="lnkAddAddress_Click" Font-Bold="true"></asp:LinkButton>
                            </td>
                        </tr>
                        <%--  <tr>
                            <td colspan="2">
                                <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" runat="server"
                                    id="imgSeparator" visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="ddlShipping" DataValueField="ID" DataTextField="ShippingType"
                                    ViewStateMode="Enabled" AutoPostBack="true" OnSelectedIndexChanged="ddlShipping_SelectedIndexChanged">
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2" style="font-size: small;">
                                <br />
                                <b>Per momentin dergimet jane te mundura vetem per Tiranen. Se shpejti do te jemi prezent
                                    dhe ne qytetet e tjera.</b>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" ID="updResult" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <div class="Red2">
                        <asp:Literal runat="server" ID="litResult"></asp:Literal>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--fine tab-->
        <div class="TabIII">
            <h3>
                <asp:Label runat="server" ID="Label13" Text='<%$Resources:Lang,  PaymentLabel%>'></asp:Label>
            </h3>
            <img src="/image/cards.png" alt="carrello" class="carrellogrey" />

            <asp:UpdatePanel ID="updPnlPayment" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <script type="text/javascript">
                        bindRadio();

                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindRadio);

                        function bindRadio() {
                            $(function () {
                                $('#radioBtnCarrier').attr('name', 'RadioGroup');
                                $('#radioBtnOffice').attr('name', 'RadioGroup');
                                $('#radioBtnPaypal').attr('name', 'RadioGroup');
                                $('#radioBtnEasypay').attr('name', 'RadioGroup');
                                if ($('#selectedPayment').val() == '1' || $('#selectedPayment').val() == 2) {
                                    $('#<%=divConfirmLink.ClientID%>').hide();
                                    $('#divConfirmText').hide();
                                    $('#tabAddresses').show();
                                    $('#titleTabAddress').show();
                                }

                                $('#accPanePaypalDiv').click(function () {
                                    $('#selectedPayment').val('1');
                                    $('#radioBtnPaypal').attr('checked', 'checked');
                                    $('#<%=divConfirmLink.ClientID%>').hide();
                                    $('#divConfirmText').hide();
                                    $('#tabAddresses').show();
                                    $('#titleTabAddress').show();
                                });

                                $('#accPaneEasypayDiv').click(function () {
                                    $('#selectedPayment').val('2');
                                    $('#radioBtnEasypay').attr('checked', 'checked');
                                    $('#<%=divConfirmLink.ClientID%>').hide();
                                    $('#divConfirmText').hide();
                                    $('#tabAddresses').show();
                                    $('#titleTabAddress').show();
                                });

                                $('#accPaneCashDiv').click(function () {
                                    $('#radioBtnCarrier').attr('checked', 'checked');
                                    $('#selectedPayment').val('3');
                                    $('#<%=divConfirmLink.ClientID%>').show();
                                    $('#divConfirmText').show();
                                    $('#tabAddresses').show();
                                    $('#titleTabAddress').show();
                                });

                                $('#accPaneOfficeDiv').click(function () {
                                    $('#selectedPayment').val('4');
                                    $('#radioBtnOffice').attr('checked', 'checked');
                                    $('#<%=divConfirmLink.ClientID%>').show();
                                    $('#divConfirmText').show();
                                    $('#tabAddresses').hide();
                                    $('#titleTabAddress').hide();
                                });
                            });
                        }
                    </script>
                    <asp:HiddenField runat="server" ID="selectedPayment" ClientIDMode="Static" Value="1" />
                    <ajaxToolkit:Accordion ID="accordionPayment" runat="server" SelectedIndex="0" FadeTransitions="true"
                        TransitionDuration="50" FramesPerSecond="30" RequireOpenedPane="true" AutoSize="None">
                        <Panes>
                            <ajaxToolkit:AccordionPane ID="accPanePaypal" runat="server" ClientIDMode="Static">
                                <Header>
                                    <div id="accPanePaypalDiv">
                                        <asp:RadioButton ID="radioBtnPaypal" ClientIDMode="Static" runat="server" Checked="true"
                                            Text="PayPal" ViewStateMode="Enabled" Font-Bold="true" />
                                    </div>
                                </Header>
                                <Content>
                                    <br />
                                    <div style="font-size: smaller;">
                                        Mund të blini me cdo lloj karte krediti (Master Card, American Express, Visa, Visa Electron) 
                                        duke përdorur sistemin me të sigurtë në botë për pagesat online, PayPal.
                                        <br />
                                        Për më shumë info rreth PayPal-it klikoni <a href="https://www.paypal.com/webapps/mpp/paypal-popup" target="_blank">Cfarë është PayPal?</a>
                                         <br />
                                    </div>
                                    <div style="font-size: smaller; margin-left: 200px;">
                                        <br />
                                        <asp:Literal runat="server" ID="litPayPalData"></asp:Literal>
                                        <asp:ImageButton ID="btnPaypal" runat="server" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_paynowCC_LG.gif" AlternateText="PayPal" OnClick="btnPaypal_Click" />
                                    </div>
                                    <br />
                                </Content>
                            </ajaxToolkit:AccordionPane>
                            <ajaxToolkit:AccordionPane ID="accPaneEasypay" runat="server" ClientIDMode="Static">
                                <Header>
                                    <div id="accPaneEasypayDiv">
                                        <asp:RadioButton ID="radioBtnEasypay" ClientIDMode="Static" runat="server" Checked="false"
                                            Text="EasyPay" ViewStateMode="Enabled" Font-Bold="true" />
                                    </div>
                                </Header>
                                <Content>
                                    <br />
                                    <div style="font-size: smaller;">
                                        EasyPay është mënyra më e re dhe inovative e transaksioneve monetare ne LEK
                                        duke përdorur SMS dhe WEB. Është sistemi i parë shqiptar i pagesave nëpërmjet telefonit tuaj celular. 
                                        <br />
                                        EasyPay krijon lehtesi. 
                                        <br />
                                        Për më shumë rreth EasyPay klikoni <a href="https://www.easypay.al/easypay_whatis.aspx" target="_blank">Cfarë është EasyPay?</a>
                                        <br />
                                    </div>
                                    <div style="font-size: smaller; margin-left: 200px;">
                                        <asp:Literal runat="server" ID="litEasyPayData"></asp:Literal>
                                        <asp:ImageButton ID="btnEasyPay" runat="server" ImageUrl="~/image/butoni.png" AlternateText="EasyPay" OnClick="btnEasyPay_Click" />
                                    </div>
                                    <br />
                                </Content>
                            </ajaxToolkit:AccordionPane>
                            <ajaxToolkit:AccordionPane ID="accPaneCarrier" ClientIDMode="Static" runat="server">
                                <Header>
                                    <div id="accPaneCashDiv">
                                        <asp:RadioButton ID="radioBtnCarrier" ClientIDMode="Static" runat="server" Checked="false"
                                            Text='<%$Resources:Lang,  WithCarrierLabel%>' ViewStateMode="Enabled" Font-Bold="true" />
                                    </div>
                                </Header>
                                <Content>
                                    <br />
                                    <div style="font-size: smaller;">
                                        Sherbim pa pagese
                                <br />
                                        Plotesoni adresen tuaj me siper, dhe pas konfirmit, do te kontaktoheni nga operatoret
                                tane per te percaktuar detajet e dorezimit dhe pageses.
                                <br />
                                        Nese keni ndonje kerkese te vecante, shkruajeni ate ne hapesiren e meposhtme.
                                <br />
                                        <br />
                                    </div>
                                    <asp:Label runat="server" ID="Label14" Text="<%$Resources:Lang, NotesLabel%>" Font-Size="Smaller"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtCashComments" Width="400" TextMode="MultiLine"
                                        Rows="3" Height="50" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                        ForeColor="Red" ControlToValidate="txtCashComments" ValidationGroup="CashValidation"></asp:RequiredFieldValidator>
                                    <br />
                                    <br />
                                </Content>
                            </ajaxToolkit:AccordionPane>

                            <ajaxToolkit:AccordionPane ID="accPaneOffice" runat="server" ClientIDMode="Static">
                                <Header>
                                    <div id="accPaneOfficeDiv">
                                        <asp:RadioButton ID="radioBtnOffice" ClientIDMode="Static" runat="server" Checked="false"
                                            Text='<%$Resources:Lang,  AtOurOfficeLabel%>' ViewStateMode="Enabled" Font-Bold="true" />
                                    </div>
                                </Header>
                                <Content>
                                    <div style="font-size: smaller;">
                                        <br />
                                        <br />
                                        Paraqituni prane zyrave tona ne adresen Rr. e Bogdaneve / Gjon Muzaka nr. 1, Tirane,
                                brenda 24 oreve te ardhshme, per te blere produktin personalisht.
                                    </div>
                                </Content>
                            </ajaxToolkit:AccordionPane>
                        </Panes>
                    </ajaxToolkit:Accordion>

                    <table width="600">
                        <tr>
                            <td style="text-align: center;">
                                <div class="MyButton2" style="margin-left: 100px;">
                                    <asp:LinkButton ID="lnkCancel" runat="server" Text="<%$Resources:Lang, CancelLabel%>"
                                        OnClick="lnkCancel_Click"></asp:LinkButton>
                                    <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkCancel"
                                        ConfirmText='<%$Resources:Lang, CancelOrderQuestion%>' />
                                </div>
                            </td>
                            &nbsp;&nbsp;&nbsp;
                    <td style="text-align: center;">
                        <asp:Panel ID="divConfirmLink" ViewStateMode="Enabled" runat="server">
                            <div class="MyButton2">
                                <asp:LinkButton ID="lnkConfirm" runat="server" OnClick="lnkConfirm_Click" ClientIDMode="AutoID"><%=Resources.Lang.ConfirmLabel + "*"%></asp:LinkButton>
                            </div>
                        </asp:Panel>
                    </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: smaller;">
                                <div id="divConfirmText">
                                    <br />
                                    <b>*Duke klikuar mbi "Konfirmo", produktet rezervohen per ju per 24 oret e ardhshme.
                            Brenda ketij afati duhet te perfundoje pagesa.</b>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="clearer">
            </div>
        </div>
        <div class="Radiusgreybox">
            <div class="Red2">
                <asp:Label runat="server" ID="lblInformationsHead" Text="<%$Resources:Lang, SecureTransactionsLabel%>"></asp:Label>
            </div>
            <img src="/image/lock.png" alt="lock" />
            <div class="clearer">
            </div>
            <asp:Label runat="server" ID="lblInformationsText" Text="<%$Resources:Lang, SecureTransactionsTextLabel%>"></asp:Label>
        </div>
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
                    <asp:Label runat="server" ID="lblModalMessage" Text="<%$Resources:Lang, CartExpired%>"></asp:Label>
                </div>
                <br />
                <br />
                <div align="center" runat="server" id="btnModalOk">
                    <asp:Button ClientIDMode="Static" ID="btnOk" Text="Ok" ToolTip="close filter-dialog"
                        CausesValidation="false" Width="70px" runat="server" OnClick="btnOk_Click" /><br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
