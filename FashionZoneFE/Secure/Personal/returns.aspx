<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="returns.aspx.cs"
    Inherits="FashionZone.FE.Secure.Personal.returns" %>

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
        <div class="TabTitle">
            RESI</div>
        <div class="clearer">
        </div>
        <div class="Tab">
            <img src="/image/resi.png" alt="carrello" class="carrellogrey" />
            <div class="DxTitRed">
                Ordini che puoi restituire</div>
            <table class="TabTable3">
                <tr>
                    <td>
                    </td>
                    <td class="Red2">
                        Articolo
                    </td>
                    <td class="Red2">
                        Taglia
                    </td>
                    <td class="Red2">
                        Prezzo
                    </td>
                    <td class="Red2">
                        Quantità
                    </td>
                    <td class="Red2">
                        Totale
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </tr>
                <tr class="TabTableGrey2">
                    <td class="ImgMyAccout">
                        <img src="/image/imgSmall2.png" alt="img small" />
                    </td>
                    <td>
                        Decoltè
                    </td>
                    <td>
                        38
                    </td>
                    <td>
                        € 123,00
                    </td>
                    <td>
                        1
                    </td>
                    <td>
                        € 123,00
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div class="BtnRedII">
                            Effettua reso</div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="Red2">
                        Articolo
                    </td>
                    <td class="Red2">
                        Taglia
                    </td>
                    <td class="Red2">
                        Prezzo
                    </td>
                    <td class="Red2">
                        Quantità
                    </td>
                    <td class="Red2">
                        Totale
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </tr>
                <tr class="TabTableGrey2">
                    <td class="ImgMyAccout">
                        <img src="/image/imgSmall2.png" alt="img small" />
                    </td>
                    <td>
                        Decoltè
                    </td>
                    <td>
                        38
                    </td>
                    <td>
                        € 123,00
                    </td>
                    <td>
                        1
                    </td>
                    <td>
                        € 123,00
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div class="BtnRedII">
                            Effettua reso</div>
                    </td>
                </tr>
            </table>
            <!--FINE PARTE DINAMICA RESO-->
            <h3 class="Red">
                Campagna: GUESS</h3>
            <table class="TabTable3">
                <tr>
                    <td class="Red2">
                        Scegli
                    </td>
                    <td class="Red2">
                        Prezzo
                    </td>
                    <td class="Red2">
                        Taglia
                    </td>
                    <td class="Red2">
                        Prodotto
                    </td>
                    <td class="Red2">
                        Quantità
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </tr>
                <tr class="TabTableGrey2">
                    <td>
                        <input type="checkbox" />
                    </td>
                    <td>
                        € 123,00
                    </td>
                    <td>
                        38
                    </td>
                    <td>
                        Scarpe decoltè fiori
                    </td>
                    <td>
                        2
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </tr>
                <tr class="TabTableGrey2">
                    <td>
                        <input type="checkbox" />
                    </td>
                    <td>
                        € 123,00
                    </td>
                    <td>
                        38
                    </td>
                    <td>
                        Scarpe decoltè fiori
                    </td>
                    <td>
                        2
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </tr>
                <td class="Red">
                    Modalità riaccredito
                </td>
                <td>
                    <select>
                        <option>VISA</option>
                        <option>POSTE PAY</option>
                        <option>MASTER CARD</option>
                        <option>AMERICAN EXPRESS</option>
                    </select>
                </td>
                <tr>
                    <td class="Red">
                        Note
                    </td>
                    <td colspan="6" valign="top">
                        <input type="text" height="200" class="TableResiNote" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <div class="BtnRedII">
                            Effettua reso</div>
                    </td>
                </tr>
            </table>
            <!--FINE PARTE DINAMICA RESO-->
            <div class="DxTitRed">
                Resi effettutati</div>
            <table class="TabTable">
                <tr>
                    <td>
                        <strong class="Red">Vendita/Campagna</strong>
                    </td>
                    <td>
                        <strong class="Red">Data</strong>
                    </td>
                    <td>
                        <strong class="Red">Prezzo</strong>
                    </td>
                    <td>
                        <strong class="Red">N° Articoli</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        Guess
                    </td>
                    <td>
                        12/12/12
                    </td>
                    <td>
                        € 123,00
                    </td>
                    <td>
                        2
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong class="Red">Vendita/Campagna</strong>
                    </td>
                    <td>
                        <strong class="Red">Data</strong>
                    </td>
                    <td>
                        <strong class="Red">Prezzo</strong>
                    </td>
                    <td>
                        <strong class="Red">N° Articoli</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        Guess
                    </td>
                    <td>
                        12/12/12
                    </td>
                    <td>
                        € 123,00
                    </td>
                    <td>
                        2
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <img src="/image/linetrasparent.png" alt="Decoration" />
                    </td>
                </tr>
            </table>
        </div>
        <!--fine tab-->
    </div>
</asp:Content>
