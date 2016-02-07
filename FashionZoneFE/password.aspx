<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true"
    CodeBehind="password.aspx.cs" Inherits="FashionZone.FE.password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <div style="padding-top: 0.5em;">
            <div id="plhTabDatPers">
                <table width="440">
                    <tr>
                        <td colspan="2" class="Red" style="text-align: left;">
                            <h3>
                                <asp:Label runat="server" ID="Label2" Text="<%$Resources:Lang, ForgotPassword%>"></asp:Label>
                            </h3>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="2">
                     <asp:Label runat="server" ID="Label3" Text="<%$Resources:Lang, EnterNewPassword%>" ForeColor="Black"></asp:Label>
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
                        <td colspan="2" style="text-align: center;">
                            <div class="MyButton">
                                <asp:LinkButton ID="lnkRegister" runat="server" Text="<%$Resources:Lang, SaveLabel%>"
                                    ValidationGroup="regValidation" OnClick="lnkRegister_Click"></asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblResult"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </center>
</asp:Content>
