<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAddress.ascx.cs"
    Inherits="FashionZone.FE.CustomControl.CustomerAddress" %>
<input type="hidden" id="disableOriginal" runat="server" />
<style type="text/css">
    .hideContent
    {
        display: none;
        height: 0px;
    }
</style>
<ajaxToolkit:Accordion ID="accAddresses" runat="server" SelectedIndex="0" FadeTransitions="true"
    SuppressHeaderPostbacks="true" TransitionDuration="80" FramesPerSecond="40" RequireOpenedPane="true"
    AutoSize="None" Height="377px" HeaderSelectedCssClass="hideContent">
    <Panes>
        <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="hideContent">
            <Content>
                <asp:Panel ID="Panel1" runat="server">
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="litName" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="litAddress" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="litOther" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <asp:Literal ID="litTel" runat="server"></asp:Literal>
                                    <tr>
                                        <td>
                                            <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" runat="server" id="imgSeparator" visible="false" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td rowspan="3" style="text-align: right;" valign="middle">
                                <img src="/image/x.png" alt="Delete" runat="server" visible="false" id="imgDelete">
                                <asp:LinkButton ID="lnkDelAddr" runat="server" Text="<%$Resources:Lang, DeleteLabel%>"
                                    OnClick="lnkDelAddr_Click" Visible="false" Font-Bold="true"></asp:LinkButton>
                                <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelAddr"
                                    ConfirmText="<%$Resources:Lang, DeleteAddressQuestion%>" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </Content>
        </ajaxToolkit:AccordionPane>
        <ajaxToolkit:AccordionPane ID="pane" runat="server">
            <Header>
                <img src="/image/modifica.png" alt="Modify" />
                <asp:LinkButton runat="server" ID="lnkModify" Text="<%$Resources:Lang, EditButton%>"></asp:LinkButton>
            </Header>
            <Content>
                <asp:Panel ID="pnlModify" runat="server">
                    <table>
                        <tr>
                            <td class="Red" valign="middle" style="text-align: left;">
                                <asp:Label runat="server" ID="Label3" Text="<%$Resources:Lang, TypeLabel%>"></asp:Label>
                            </td>
                            <td valign="middle" style="text-align: left;">
                                <asp:DropDownList ID="ddlAddrType" runat="server" TabIndex="5" ViewStateMode="Enabled"
                                    Width="155">
                                </asp:DropDownList>
                            </td>
                            <td class="Red" valign="middle" style="text-align: left;">
                                <asp:Label runat="server" ID="lblNameAddres" Text="<%$Resources:Lang, NameLabel%>"></asp:Label>
                            </td>
                            <td valign="middle" style="text-align: left;">
                                <asp:TextBox ID="txtName" TabIndex="10" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="validName" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="Red" valign="middle" style="text-align: left;">
                                <asp:Label runat="server" ID="lblAddress" Text="<%$Resources:Lang, AddressLabel%>"></asp:Label>
                            </td>
                            <td colspan="3" valign="middle" style="text-align: left;">
                                <asp:TextBox ID="txtAddress" TabIndex="20" runat="server" TextMode="MultiLine" Height="28px"
                                    Width="385"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="validAddress" runat="server" ControlToValidate="txtAddress"
                                    ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="Red" valign="middle" style="text-align: left;">
                                <asp:Label runat="server" ID="lblZIPCode" Text="<%$Resources:Lang, LocationLabel%>"></asp:Label>
                            </td>
                            <td valign="middle" style="text-align: left;">
                                <asp:TextBox ID="txtLocation" TabIndex="30" runat="server"></asp:TextBox>
                            </td>
                            <td class="Red" valign="middle" style="text-align: left;">
                                <asp:Label runat="server" ID="lblTel" Text="<%$Resources:Lang, TelLabel%>"></asp:Label>
                            </td>
                            <td valign="middle" style="text-align: left;">
                                <asp:TextBox ID="txtTel" TabIndex="40" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="validTel" runat="server" ControlToValidate="txtTel"
                                    ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="Red" valign="middle" style="text-align: left;">
                                <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, ZIPCodeLabel%>"></asp:Label>
                            </td>
                            <td valign="middle" style="text-align: left;">
                                <asp:TextBox ID="txtPostal" TabIndex="50" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                            <td class="Red" valign="middle" style="text-align: left;">
                                <asp:Label runat="server" ID="Label2" Text="<%$Resources:Lang, CityLabel%>"></asp:Label>
                            </td>
                            <td valign="middle" style="text-align: left;">
                                <asp:DropDownList ID="ddlCities" runat="server" TabIndex="60" Width="155" ViewStateMode="Enabled">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div align="center">
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <div class="MyButton">
                                                    <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click" Font-Bold="true"
                                                        Text="<%$Resources:Lang, CancelLabel%>" />
                                                </div>
                                            </td>
                                            <td colspan="2">
                                                <div class="MyButton">
                                                    <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click" Text="<%$Resources:Lang, SaveLabel%>"
                                                        Font-Bold="true" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </Content>
        </ajaxToolkit:AccordionPane>
    </Panes>
</ajaxToolkit:Accordion>
<asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>

<br />
