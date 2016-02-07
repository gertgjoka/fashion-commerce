<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="contactUs.aspx.cs" Inherits="FashionZone.FE.Static.contactUs" %>

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
                                <asp:Label runat="server" ID="Label2" Text="<%$Resources:Lang, ContactLabel%>"></asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td class="Red" style="text-align: left;">
                            Email
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" ID="txtEmail" class="TableRegisterInput"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="regValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator 
                                ID="valEmail" 
                                runat="server" 
                                ErrorMessage="<%$Resources:Lang, NotValidLabel%>"
                                ForeColor="Red" 
                                ControlToValidate="txtEmail" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="regValidation">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="Red" style="text-align: left;">
                            <asp:Label runat="server" ID="lblGender" Text="<%$Resources:Lang, MotivationLabel%>"></asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlMotiv" runat="server" ViewStateMode="Enabled" 
                                Width="270px">
                                <asp:ListItem Value="-1" Text="--"></asp:ListItem>
                                <asp:ListItem Value="<%$Resources:Lang, PayProblemLabel%>" Text="<%$Resources:Lang, PayProblemLabel%>"></asp:ListItem>
                                <asp:ListItem Value="<%$Resources:Lang, BuyProblemLabel%>" Text="<%$Resources:Lang, BuyProblemLabel%>"></asp:ListItem>
                                <asp:ListItem Value="<%$Resources:Lang, RegProblemLabel%>" Text="<%$Resources:Lang, RegProblemLabel%>"></asp:ListItem>
                                <asp:ListItem Value="<%$Resources:Lang, OtherLabel%>" Text="<%$Resources:Lang, OtherLabel%>"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldDDL" runat="server" 
                                ControlToValidate="ddlMotiv"
                                ErrorMessage="*" 
                                InitialValue="--" 
                                ValidationGroup="regValidation">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left;">
                            <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Red" style="text-align: left;vertical-align: top;">
                            <asp:Label runat="server" ID="lblRequest" Text="<%$Resources:Lang, RequestLabel%>"></asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" 
                                ID="txtText" 
                                class="TableRegisterInput"
                                Width="95%"
                                TextMode="MultiLine" 
                                Rows="5">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                runat="server" ErrorMessage="*"
                                ForeColor="Red" ControlToValidate="txtText" 
                                ValidationGroup="regValidation">
                            </asp:RequiredFieldValidator>
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
                            <div class="MyButton" style="margin-right: 15px;">
                                <asp:LinkButton 
                                    ID="lnkRegister" 
                                    runat="server" 
                                    Text="<%$Resources:Lang, SendLabel%>"
                                    ValidationGroup="regValidation" onclick="lnkRegister_Click">
                                </asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </center>
</asp:Content>
