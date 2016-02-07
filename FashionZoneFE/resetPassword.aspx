<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true"
    CodeBehind="resetPassword.aspx.cs" Inherits="FashionZone.FE.resetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <center>
        <div style="padding-top: 0.5em; height:300px;">
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
                        <td class="Red" style="text-align: left;">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Email:</asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="UserName" runat="server" Width="190"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                CssClass="failureNotification" ValidationGroup="LoginUserValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="valEmail" runat="server" ErrorMessage="<%$Resources:Lang, NotValidLabel%>"
                                ForeColor="Red" ControlToValidate="UserName" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="LoginUserValidationGroup"></asp:RegularExpressionValidator>
                        </td>
                        <td style="text-align: center;">
                            <div class="MyButton">
                                <asp:LinkButton ID="lnkPass" runat="server" Text="<%$Resources:Lang, SendLabel%>"
                                    ValidationGroup="LoginUserValidationGroup" OnClick="lnkPass_Click"></asp:LinkButton>
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
