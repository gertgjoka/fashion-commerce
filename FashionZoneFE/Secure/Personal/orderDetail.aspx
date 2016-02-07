<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="orderDetail.aspx.cs"
    Inherits="FashionZone.FE.Secure.Personal.orderDetail" %>

<%@ Register Src="../../CustomControl/LeftSidePanelPersInfo.ascx" TagName="LeftSidePanelPersInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function popWin(id) {
            window.open('/personal/pdfDownload/' + id, '', '');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearer">
    </div>
    <uc1:LeftSidePanelPersInfo ID="LeftSidePanelPersInfo1" runat="server" />
    <div class="Dx">
        <div class="DxTit">
            <asp:Label runat="server" ID="lblMyAccount" Text="<%$Resources:Lang, MyAccountLabel%>"></asp:Label>
        </div>
        <div class="TabTitle">
            <asp:Label runat="server" ID="lblDetailTit" Text="<%$Resources:Lang, DetailLabel%>"></asp:Label>
        </div>
        <div class="clearer">
        </div>
        <div class="Tab">
            <img src="/image/carrellogrey.png" alt="carrello" class="carrellogrey" />
            <div>
                <strong class="Red">N°
                    <asp:Literal ID="litOrderIdTit" runat="server" Text="<%$Resources:Lang, OrderLabel%>"></asp:Literal>:
                </strong>
                <asp:Literal ID="litOrderId" runat="server"></asp:Literal>
            </div>
            <br />
            <div>
                <strong class="Red">
                    <asp:Literal ID="litOrderDataTit" runat="server" Text="<%$Resources:Lang, OrderDateLabel%>"></asp:Literal>:
                </strong>
                <asp:Literal ID="litOrderData" runat="server"></asp:Literal>
            </div>
            <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" />
            <div>
                <strong class="Red">
                    <asp:Literal ID="litOrderShippingStartTit" runat="server" Text="<%$Resources:Lang, ShippingStartLabel%>"
                        Visible="false"></asp:Literal>
                </strong>
                <asp:Literal ID="litOrderShippingStart" runat="server"></asp:Literal>
            </div>
            <ul class="BoxInfo">
                <li><strong class="Red">
                    <asp:Literal ID="litOrderShippingAddrTit" runat="server" Text="<%$Resources:Lang, DeliveryAddressLabel%>"></asp:Literal>
                </strong>
                    <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" /></li>
                <li><strong class="Red">
                    <asp:Literal ID="litOrderTelTit" runat="server" Text="<%$Resources:Lang, TelLabel%>"></asp:Literal>:
                </strong>
                    <asp:Literal ID="litOrderTel" runat="server"></asp:Literal>
                </li>
                <li><strong class="Red">
                    <asp:Literal ID="litOrderAddresShippingTit" runat="server" Text="<%$Resources:Lang, AddressLabel%>"></asp:Literal>:
                </strong>
                    <asp:Literal ID="litOrderAddresShipping" runat="server"></asp:Literal>
                </li>
            </ul>
            <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" />
            <div class="DxTitRed">
                <asp:Literal ID="litOrderCampagnName" runat="server"></asp:Literal>
            </div>
            <div>
                <strong class="Red">
                    <asp:Literal ID="litShippingEndTit" runat="server" Text="<%$Resources:Lang, ShippingEndLabel%>"
                        Visible="false"></asp:Literal>
                </strong>
                <asp:Literal ID="litOrdeDateShippmentEnd" runat="server"></asp:Literal>
            </div>
            <br />
            <div>
                <strong class="Red">
                    <asp:Literal ID="litStateTit" runat="server" Text="<%$Resources:Lang, StateLabel%>"></asp:Literal>
                </strong>
            </div>
            <asp:Literal ID="litStatus" runat="server"></asp:Literal>
            <%--
            <a href="#" class="StatoOn">Aperto</a> 
            <a href="#" class="StatoOff">Attesa</a> 
            <a href="#" class="StatoOff">In magazzino</a> 
            <a href="#" class="StatoOff">In consegna</a>
            <a href="#" class="StatoOff">Consegnato</a>
            --%>
            <asp:Repeater ID="RepeatOrderDet" runat="server" ViewStateMode="Enabled">
                <HeaderTemplate>
                    <table class="TabTable3">
                        <tr>
                            <td style="width: 90px;">
                                &nbsp;
                            </td>
                            <td class="Red2" style="width: 360px; text-align: center;">
                                <asp:Literal ID="litItemTit" runat="server" Text="<%$Resources:Lang, ItemLabel%>"></asp:Literal>
                            </td>
                            <td class="Red2" style="width: 40px; text-align: center;">
                                <asp:Literal ID="litSizeTit" runat="server" Text="<%$Resources:Lang, SizeLabel%>"></asp:Literal>
                            </td>
                            <td class="Red2" style="width: 60px; text-align: center;">
                                <asp:Literal ID="litPriceTit" runat="server" Text="<%$Resources:Lang, PriceLabel%>"></asp:Literal>
                            </td>
                            <td class="Red2" style="width: 40px; text-align: center;">
                                <asp:Literal ID="litQuantityTit" runat="server" Text="<%$Resources:Lang, QuantityLabel%>"></asp:Literal>
                            </td>
                            <td class="Red2" style="width: 60px; text-align: center;">
                                <asp:Literal ID="litTotaleTit" runat="server" Text="<%$Resources:Lang, TotalLabel%>"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <img src="/image/linetrasparent.png" alt="Decoration" />
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="ImgMyAccout">
                            <img runat="server" id="imgProduct" clientidmode="AutoID" src='<%# Eval("ProductThumbnail") %>'
                                alt="No Image" />
                        </td>
                        <td style="text-align: left;">
                            <%# DataBinder.Eval(Container.DataItem, "ProductName")%>
                        </td>
                        <td style="text-align: center;">
                            <%# DataBinder.Eval(Container.DataItem, "ProductAttribute")%>
                        </td>
                        <td style="text-align: center;">
                            €
                            <%# DataBinder.Eval(Container.DataItem, "UnitPrice")%>
                        </td>
                        <td style="text-align: center;">
                            <%# DataBinder.Eval(Container.DataItem, "Quantity")%>
                        </td>
                        <td style="text-align: center;">
                            €
                            <%# DataBinder.Eval(Container.DataItem, "Amount")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <SeparatorTemplate>
                    <tr>
                        <td colspan="7">
                            <img src="/image/linetrasparent.png" alt="Decoration" />
                        </td>
                    </tr>
                </SeparatorTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="7">
                            <img src="/image/linetrasparent.png" alt="Decoration" />
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <%--<asp:LinkButton ID="lbtnPdf" runat="server" CssClass="BtnRed">Download</asp:LinkButton>--%>
            <div id="divBtnInvoice" class="MyButton" runat="server" visible="false">
                <asp:LinkButton ID="btnPdfDownload" runat="server" Text="<%$Resources:Lang, InvoiceLabel%>" />
            </div>
            <div class="clearer">
            </div>
            <div>
            <br />
                <strong class="Red">
                    <asp:Literal ID="litPaymentData" runat="server" Text="<%$Resources:Lang, PaymentDataLabel%>"></asp:Literal>
                </strong>
            </div>
            <ul class="BoxInfo">
               
                <li><strong class="Red">
                    <asp:Literal ID="litSubTotaleTit" runat="server" Text="<%$Resources:Lang, SubTotalLabel%>"></asp:Literal>:
                </strong>€
                    <asp:Literal ID="litSubTotale" runat="server"></asp:Literal>
                </li>
                <li><strong class="Red">
                    <asp:Literal ID="litBonusTit" runat="server" Text="<%$Resources:Lang, BonusLabel%>"></asp:Literal>:
                </strong>€
                    <asp:Literal ID="litBonus" runat="server"></asp:Literal>
                </li>
                <li><strong class="Red">
                    <asp:Literal ID="litShippinCostTit" runat="server" Text="<%$Resources:Lang, ShippingCostLabel%>"></asp:Literal>:
                </strong>€
                    <asp:Literal ID="litShippinCost" runat="server"></asp:Literal>
                </li>
                <li><strong class="Red">
                    <asp:Literal ID="litTotalPaiedTit" runat="server" Text="<%$Resources:Lang, TotalLabel%>"></asp:Literal>
                </strong>€
                    <asp:Literal ID="litTotalPaied" runat="server"></asp:Literal>
                </li>
            </ul>
            <img src="/image/linetrasparent.png" alt="Decoration" />
            <table class="TabTable">
                <tr>
                    <td>
                        <strong class="Red">
                            <asp:Literal ID="litPaymentData2" runat="server" Text="<%$Resources:Lang, PaymentDataLabel%>"></asp:Literal>
                        </strong>
                    </td>
                    <%--                    <td>
                        <strong class="Red">Num. autorizzazione</strong>
                    </td>--%>
                    <td colspan="2">
                        <strong class="Red">
                            <asp:Literal ID="litComment" runat="server" Text="<%$Resources:Lang, NotesLabel%>"></asp:Literal>
                        </strong>
                    </td>
                    <td>
                        <strong class="Red">
                            <asp:Literal ID="litAmountPayment" runat="server" Text="<%$Resources:Lang, AmountLabel%>"></asp:Literal>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="libPaymetType" runat="server"></asp:Literal>
                    </td>
                    <%--<td>
                        <asp:Literal ID="libAutorizInfo" runat="server" ></asp:Literal>
                    </td>--%>
                    <td colspan="2">
                        <asp:Literal ID="libTransNum" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="lblTotalPay" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <!--fine tab-->
    </div>
</asp:Content>
