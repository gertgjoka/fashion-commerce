<%@ Page Title="FZone - Enter the club" Language="C#" MasterPageFile="~/Public.Master"
    AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="FashionZone.FE.register"
    EnableViewStateMac="true" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <div style="padding-top: 0.5em;">
            <div id="plhTabDatPers">
                <table>
                    <tr>
                        <td colspan="2" class="Red" style="text-align: left;">
                            <h2>
                                <asp:Label runat="server" ID="Label2" Text="<%$Resources:Lang, EnterTheClubLabel%>"></asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label runat="server" ID="Label3" Text="<%$Resources:Lang, ExclusiveSalesLabel%>"></asp:Label>
                            </b>
                            <br />
                            <br />
                        </td>
                    </tr>
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
                            <asp:TextBox ID="txtBirthday" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="txtBirthday" ValidationGroup="regValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red" ValidationGroup="regValidation"
                                ErrorMessage="dd/mm/yyyy" ControlToValidate="txtBirthday" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$">
                            </asp:RegularExpressionValidator>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtBirthday"
                                SelectedDate="01/01/1980" Format="dd/MM/yyyy" StartDate="01/01/1900" runat="server">
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
                        <td colspan="2" style="text-align: left;">
                            <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Red" style="text-align: left;">
                            Email
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" ID="txtEmail" class="TableRegisterInput"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="regValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="valEmail" runat="server" ErrorMessage="<%$Resources:Lang, NotValidLabel%>"
                                ForeColor="Red" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="regValidation"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr class="Red" style="text-align: left;">
                        <td>
                            <asp:Label runat="server" ID="lblPass" Text="<%$Resources:Lang, PasswordLabel%>"></asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" ID="txtPassword" class="TableRegisterInput" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                ControlToValidate="txtPassword" ForeColor="Red" ValidationGroup="regValidation"></asp:RequiredFieldValidator>
                            <%--                        <ajaxToolkit:PasswordStrength ID="PasswordStrength1" runat="server" MinimumLowerCaseCharacters="5"
                            MinimumUpperCaseCharacters="1" MinimumNumericCharacters="1" MinimumSymbolCharacters="1"
                            TargetControlID="txtPassword">
                        </ajaxToolkit:PasswordStrength>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="Red" style="text-align: left;">
                            <asp:Label runat="server" ID="lblPass2" Text="<%$Resources:Lang, RepeatPasswordLabel%>"></asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" ID="txtPassword2" class="TableRegisterInput" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                ControlToValidate="txtPassword2" ForeColor="Red" ValidationGroup="regValidation"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="comparePassword" runat="server" ControlToValidate="txtPassword"
                                ControlToCompare="txtPassword2" ErrorMessage="<%$Resources:Lang, PasswordMismatchLabel%>"
                                ForeColor="Red" ValidationGroup="regValidation"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="RedNormal" colspan="2" style="text-align: left;">
                            <br />
                            <asp:CheckBox runat="server" ID="chkGeneralTerms" Text="<%$Resources:Lang, ConditionAcceptanceText%>"
                                ViewStateMode="Enabled" />
                            <a href="/public/terms/" title="Terms of use" target="_blank">
                                <asp:Label runat="server" ID="lblTermsOfUse" Text="<%$Resources:Lang, TermsOfUseLabel%>"></asp:Label></a>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="RedNormal" colspan="2">
                            <br />
                            <asp:Literal runat="server" ID="litError" ViewStateMode="Enabled"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <div class="MyButton">
                                <asp:LinkButton ID="lnkRegister" runat="server" Text="<%$Resources:Lang, RegisterLabel%>"
                                    ValidationGroup="regValidation" OnClick="lnkRegister_Click"></asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </center>
</asp:Content>
