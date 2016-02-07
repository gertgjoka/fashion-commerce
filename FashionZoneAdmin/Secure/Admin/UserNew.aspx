<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserNew.aspx.cs" Inherits="FashionZone.Admin.Secure.Admin.UserNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
        .customCheck input
        {
            margin-top: 5px;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <input type="hidden" id="ID" runat="server" />
            <div id="box">
                <h3 id="adduser">
                    Add user</h3>
                <div id="form">
                    <fieldset id="personal">
                        <legend>PERSONAL INFORMATION</legend>
                        <label for="Name">
                            Name :
                        </label>
                        <asp:TextBox ID="txtName" TabIndex="10" runat="server" />
                        <asp:RequiredFieldValidator ID="validName" runat="server" ControlToValidate="txtName"
                            ErrorMessage="*" ForeColor="Red" ValidationGroup="userValidation"></asp:RequiredFieldValidator>
                        <br />
                        <label for="Login">
                            Login :
                        </label>
                        <asp:TextBox ID="txtLogin" TabIndex="20" runat="server" />
                        <asp:RequiredFieldValidator ID="validLogin" runat="server" ControlToValidate="txtLogin"
                            ErrorMessage="*" ForeColor="Red" ValidationGroup="userValidation"></asp:RequiredFieldValidator>
                        <br />
                        <label for="email">
                            Email :
                        </label>
                        <asp:TextBox ID="txtEmail" TabIndex="30" runat="server" />
                        <asp:RegularExpressionValidator ID="valEmail" runat="server" ErrorMessage="Not valid."
                            ForeColor="Red" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="userValidation"></asp:RegularExpressionValidator>
                        <br />
                        <label for="pass">
                            Password :
                        </label>
                        <asp:TextBox ID="txtPass" TabIndex="40" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="validPassword" runat="server" ControlToValidate="txtPass"
                            ErrorMessage="*" ForeColor="Red" ValidationGroup="userValidation"></asp:RequiredFieldValidator>
                        <br />
                        <label for="pass-2">
                            Password :
                        </label>
                        <asp:TextBox ID="txtPass2" TabIndex="50" runat="server" TextMode="Password" />
                        <asp:CompareValidator ID="comparePassword" runat="server" ControlToValidate="txtPass"
                            ControlToCompare="txtPass2" ErrorMessage="Passwords must match!" ForeColor="Red"
                            Operator="Equal" ValidationGroup="userValidation"></asp:CompareValidator>
                        <br />
                        <label for="role">
                            Role :
                        </label>
                        <asp:DropDownList ID="ddlRoles" runat="server" TabIndex="60" ViewStateMode="Enabled">
                        </asp:DropDownList>
                        <br />
                        <label for="role">
                            Enabled :
                        </label>
                        <asp:CheckBox ID="chkEnabled" runat="server" TabIndex="70" CssClass="customCheck"></asp:CheckBox>
                    </fieldset>
                    <br />
                    <asp:Label ID="lblPassStrength" runat="server" Text="The Password must be at least 8 characters and contain three of the following 4 items: 
                        upper case letter, lower case letter, a symbol, a number." Visible="false" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                    <div align="center">
                        <asp:Button ID="button1" Text="Save" runat="server" OnClick="button1_Click" 
                            ValidationGroup="userValidation" Width="59px" TabIndex="80"/>
                        <asp:Button ID="button2" Text="Reset" runat="server" CausesValidation="False" OnClick="button2_Click" TabIndex="90"/>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
