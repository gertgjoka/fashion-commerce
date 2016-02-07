<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="inviteFriend.aspx.cs"
    Inherits="FashionZone.FE.Secure.Personal.inviteFriend" %>

<%@ Register Src="../../CustomControl/LeftSidePanelPersInfo.ascx" TagName="LeftSidePanelPersInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function checkMultipeMail(source, args) {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            var txt = document.getElementById('<%= txtInvitedMail.ClientID %>').value;
            var n = txt.split(",");
            for (var i = 0; i < n.length; i++) {
                if (!filter.test(n[i])) {
                    return args.IsValid = false;
                }
            }
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
        <a href="#" class="TabTitle">
            <asp:Label runat="server" ID="lblBonusTit" Text="<%$Resources:Lang, FriendsLabel%>"></asp:Label>
        </a>
        <div class="clearer">
        </div>
        <div class="Tab">
            <img src="/image/buoni.png" alt="Bonus" class="carrellogrey" />
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
                        <td colspan="2">
                            <b>
                                <asp:Label runat="server" ID="Label3" Text="<%$Resources:Lang, InviteAndWinLabel%>"></asp:Label>€
                            </b>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="Red" style="text-align:left;">
                            <asp:Label runat="server" ID="lblName" Text="<%$Resources:Lang, NameLabel%>"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox runat="server" ID="txtName" class="TableRegisterInput"></asp:TextBox>
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator1" 
                                runat="server" 
                                ErrorMessage="*"
                                ControlToValidate="txtName" 
                                ForeColor="Red" 
                                ValidationGroup="regValidation">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="Red" style="text-align:left;">
                            <asp:Label runat="server" ID="lblSurname" Text="<%$Resources:Lang, SurnameLabel%>"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox runat="server" ID="txtSurname" class="TableRegisterInput"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="Red" style="text-align: left;">
                            Email
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" ID="txtInvitedMail" class="TableRegisterInput" Rows="4"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="txtInvitedMail" ValidationGroup="regValidation">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvalidatorInvitedMail" ControlToValidate="txtInvitedMail"
                                ClientValidationFunction="checkMultipeMail" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:Lang, NotValidLabel %>"
                                ValidationGroup="regValidation">
                            </asp:CustomValidator>
                            <br />
                            <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, MailSeparatorLabel%>"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left;">
                            <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" />
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
                                <asp:LinkButton ID="lnkRegister" runat="server" Text="<%$Resources:Lang, InviteLabel%>"
                                    ValidationGroup="regValidation" OnClick="lnkRegister_Click">
                                </asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
