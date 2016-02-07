<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="personalInfo.aspx.cs"
    Inherits="FashionZone.FE.Secure.Personal.personalInfo" EnableViewStateMac="true" EnableEventValidation="true"%>

<%@ Register Src="../../CustomControl/LeftSidePanelPersInfo.ascx" TagName="LeftSidePanelPersInfo"
    TagPrefix="uc1" %>
<%@ Register Src="/CustomControl/CustomerAddress.ascx" TagPrefix="custom" TagName="address" %>
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
        <div class="clearer">
        </div>
        <img src="/image/datipersonali.png" alt="carrello" class="carrellogrey" />
        <ajaxToolkit:TabContainer ID="tabCustContainer" runat="server" ActiveTabIndex="0"
            CssClass="TabPanel">
            <ajaxToolkit:TabPanel ID="tabInfo" runat="server" CssClass="Tab">
                <HeaderTemplate>
                    <div class="TabPanelHeader">
                        <asp:Label runat="server" ID="lblInfoHeader" Text="<%$Resources:Lang, PersonalDataHeadLabel%>"></asp:Label>
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="updPanMyData" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td class="Red" style="text-align: left;">
                                        <asp:Label runat="server" ID="lblName" Text="<%$Resources:Lang, NameLabel%>"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox runat="server" ID="txtName" class="TableRegisterInput"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtName" ForeColor="Red" ValidationGroup="regValidation"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Red" style="text-align: left;">
                                        <asp:Label runat="server" ID="lblSurname" Text="<%$Resources:Lang, SurnameLabel%>"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox runat="server" ID="txtSurname" class="TableRegisterInput"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Red" style="text-align: left;">
                                        <asp:Label runat="server" ID="lblPhone" Text="<%$Resources:Lang, TelLabel%>"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox runat="server" ID="txtPhone" class="TableRegisterInput"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Red" style="text-align: left;">
                                        <asp:Label runat="server" ID="lblMobile" Text="<%$Resources:Lang, MobileLabel%>"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox runat="server" ID="txtMobile" class="TableRegisterInput"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Red" style="text-align: left;">
                                        <asp:Label runat="server" ID="lblBirthday" Text="<%$Resources:Lang, BirthdayLabel%>"></asp:Label>
                                    </td>
                                    <td class="Red" style="text-align: left;">
                                        <asp:TextBox ID="txtBirthday" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calExBirthday" TargetControlID="txtBirthday" Format="dd/MM/yyyy"
                                            runat="server">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Red" style="text-align: left;">
                                        <asp:Label runat="server" ID="lblGender" Text="<%$Resources:Lang, GenderLabel%>"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlGender" runat="server" ViewStateMode="Enabled">
                                            <asp:ListItem Value="M">M</asp:ListItem>
                                            <asp:ListItem Value="F">F</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <div class="MyButton">
                                            <asp:LinkButton ID="lnkModify" runat="server" Text="<%$Resources:Lang, SaveLabel%>"
                                                ValidationGroup="regValidation" OnClick="lnkModify_Click"> </asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: left;">
                                        <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Red" style="text-align: left; vertical-align: top;">
                                        Email
                                    </td>
                                    <td style="text-align: left;">
                                        <div style="height: 30px;">
                                            <asp:Label ID="lblMailAdres" runat="server" Style="float: left" ViewStateMode="Enabled"></asp:Label>
                                            <div class="MyButton">
                                                <asp:LinkButton Style="margin-top: 0px;" ID="lbtnShowEditMail" runat="server" Text="<%$Resources:Lang, EditButton%>"
                                                    OnClick="lbtnShowEditMail_Click"> </asp:LinkButton>
                                            </div>
                                        </div>
                                        <asp:Panel runat="server" ID="panEditMail" Visible="False">
                                            <br />
                                            <table>
                                                <tr class="Red" style="text-align: left;">
                                                    <td>
                                                        E-mail
                                                    </td>
                                                    <td style="text-align: left; font-size: 10px;">
                                                        <asp:TextBox runat="server" ID="txtNewMail" class="TableRegisterInput"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtNewMail" ForeColor="Red" ValidationGroup="reMailValidation"> </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="valEmail" runat="server" ErrorMessage="<%$Resources:Lang, NotValidLabel%>"
                                                            ForeColor="Red" ControlToValidate="txtNewMail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="reMailValidation"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr class="Red" style="text-align: left;">
                                                    <td valign="middle">
                                                        <asp:Label runat="server" ID="lblRepeatMail" Text="<%$Resources:Lang, RepeatLabel%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; font-size: 10px;">
                                                        <asp:TextBox runat="server" ID="txtNewMail2" class="TableRegisterInput"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtNewMail2" ForeColor="Red" ValidationGroup="reMailValidation"> </asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtNewMail"
                                                            ControlToCompare="txtNewMail2" ErrorMessage="<%$Resources:Lang, MailMismatchLabel%>"
                                                            ForeColor="Red" ValidationGroup="reMailValidation"> </asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Red" valign="middle">
                                                        <asp:Label runat="server" ID="Label3" Text="<%$Resources:Lang, PasswordLabel%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox runat="server" ID="txtEmailPassword" class="TableRegisterInput" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtEmailPassword" ForeColor="Red" ValidationGroup="reMailValidation"> </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="MyButton">
                                                            <asp:LinkButton ID="lbtnSaveMail" runat="server" Text="<%$Resources:Lang, SaveLabel%>"
                                                                ValidationGroup="reMailValidation" OnClick="lbtnSaveMail_Click"> </asp:LinkButton>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr class="Red" style="text-align: left;">
                                    <td style="vertical-align: top;">
                                        <asp:Label runat="server" ID="lblPass" Text="<%$Resources:Lang, PasswordLabel%>"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblHidPw" runat="server" Style="float: left">*********</asp:Label>
                                        <div class="MyButton">
                                            <asp:LinkButton Style="margin-top: 0px;" ID="lbtnShowEditPw" runat="server" Text="<%$Resources:Lang, EditButton%>"
                                                OnClick="lbtnShowEditPw_Click"> </asp:LinkButton>
                                        </div>
                                        <asp:Panel runat="server" ID="pnlEditPW" Visible="False">
                                            <br />
                                            <br />
                                            <table>
                                                <tr class="Red" style="text-align: left;">
                                                    <td>
                                                        <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, NewPassLabel%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; font-size: 10px;">
                                                        <asp:TextBox runat="server" ID="txtEditPw" class="TableRegisterInput" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtEditPw" ForeColor="Red" ValidationGroup="rePwValidation"> </asp:RequiredFieldValidator>
                                                        <%--<ajaxToolkit:PasswordStrength ID="PasswordStrength1" runat="server" MinimumLowerCaseCharacters="5"
                                                    MinimumUpperCaseCharacters="1" MinimumNumericCharacters="1" MinimumSymbolCharacters="1"
                                                    TargetControlID="txtPassword">
                                                </ajaxToolkit:PasswordStrength>--%>
                                                    </td>
                                                </tr>
                                                <tr class="Red" style="text-align: left;">
                                                    <td>
                                                        <asp:Label runat="server" ID="Label2" Text="<%$Resources:Lang, RepeatPasswordLabel%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; font-size: 10px;">
                                                        <asp:TextBox runat="server" ID="txtEditPw2" class="TableRegisterInput" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtEditPw2" ForeColor="Red" ValidationGroup="rePwValidation"> </asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtEditPw"
                                                            ControlToCompare="txtEditPw2" ErrorMessage="<%$Resources:Lang, PasswordMismatchLabel%>"
                                                            ForeColor="Red" ValidationGroup="rePwValidation"> </asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Red" style="vertical-align: top;">
                                                        <asp:Label runat="server" ID="Label4" Text="<%$Resources:Lang, OldPassLabel%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; font-size: 10px;">
                                                        <asp:TextBox runat="server" ID="txtOldPass" class="TableRegisterInput" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtOldPass" ForeColor="Red" ValidationGroup="rePwValidation"> </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="MyButton">
                                                            <asp:LinkButton ID="lbtnSavePw" runat="server" Text="<%$Resources:Lang, SaveLabel%>"
                                                                ValidationGroup="rePwValidation" OnClick="lbtnSavePw_Click"> </asp:LinkButton>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <tr>
                                            <td class="RedNormal" colspan="2">
                                                <br />
                                                <asp:Literal runat="server" ID="litError" ViewStateMode="Enabled"></asp:Literal>
                                            </td>
                                        </tr>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tabAddr" runat="server" HeaderText="<%$Resources:Lang, MyAddresesHeadLabel%>" CssClass="Tab">
                <HeaderTemplate>
                    <div class="TabPanelHeader">
                        <asp:Label runat="server" ID="Label5" Text="<%$Resources:Lang, MyAddresesHeadLabel%>"></asp:Label>
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:UpdatePanel ID="updPanAddress" runat="server" ViewStateMode="Enabled">
                        <ContentTemplate>
                            <custom:address ID="address1" runat="server" Visible="false" OnAddressDelete="address_OnAddressDelete"
                                OnAddressAsyncRemove="address_OnAddressAsyncRemove" OnSaving="address_Saving" />
                            <custom:address ID="address2" runat="server" Visible="false" OnAddressDelete="address_OnAddressDelete"
                                OnAddressAsyncRemove="address_OnAddressAsyncRemove" />
                            <custom:address ID="address3" runat="server" Visible="false" OnAddressDelete="address_OnAddressDelete"
                                OnAddressAsyncRemove="address_OnAddressAsyncRemove" />
                            <custom:address ID="address4" runat="server" Visible="false" OnAddressDelete="address_OnAddressDelete"
                                OnAddressAsyncRemove="address_OnAddressAsyncRemove" />
                            <custom:address ID="address5" runat="server" Visible="false" OnAddressDelete="address_OnAddressDelete"
                                OnAddressAsyncRemove="address_OnAddressAsyncRemove" />
                            <img src="/image/more.png" runat="server" alt="<%$Resources:Lang, AddAddressLabel%>" />
                            <asp:LinkButton ID="lnkAddAddress" runat="server" Text="<%$Resources:Lang, AddAddressLabel%>"
                                OnClick="lnkAddAddress_Click" Font-Bold="true"></asp:LinkButton>
                            <asp:Label ID="lblErrors" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>

    </div>
</asp:Content>
