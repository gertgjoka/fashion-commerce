<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrandNew.aspx.cs" Inherits="FashionZone.Admin.Secure.Product.BrandNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">
        .customCheck input
        {
            margin-top: 5px;
            margin-bottom: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <input type="hidden" id="ID" runat="server" />
            <div id="box">
                <h3 id="adduser">Add Brand</h3>
                <div id="form">
                    <fieldset id="personal">
                        <legend>BRAND INFORMATION</legend>
                        <label for="Name">Name : </label>
                        <asp:TextBox ID="txtName" TabIndex="10" runat="server" />
                        <asp:RequiredFieldValidator ID="validName"
                            runat="server"
                            ControlToValidate="txtName"
                            ErrorMessage="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        <br />

                        <label for="Login">Show Name : </label>
                        <asp:TextBox ID="txtShowName" TabIndex="20" runat="server" />
                        <asp:RequiredFieldValidator ID="validLogin"
                            runat="server"
                            ControlToValidate="txtShowName"
                            ErrorMessage="*"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        <br />
                        <label for="approved">
                            Shop :
                        </label>
                        <asp:CheckBox ID="chkShop" runat="server" TabIndex="25" CssClass="customCheck"
                            ViewStateMode="Enabled"></asp:CheckBox>
                        <br />
                        <label for="email">Description : </label>
                        <asp:TextBox ID="txtDesc" TabIndex="30" runat="server" TextMode="MultiLine" Rows="6" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            runat="server"
                            ControlToValidate="txtDesc"
                            ErrorMessage="*"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        <br />
                         <label for="email">Address : </label>
                        <asp:TextBox ID="txtAddress" TabIndex="35" runat="server" TextMode="MultiLine" Rows="2" Height="40" />
                        <br />
                        <label for="Name">Contatct : </label>
                        <asp:TextBox ID="txt_contact" TabIndex="40" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            runat="server"
                            ControlToValidate="txt_contact"
                            ErrorMessage="*"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        <br />

                        <label for="Name">Telephone : </label>
                        <asp:TextBox ID="txt_tel" TabIndex="50" runat="server" />
                        <asp:RegularExpressionValidator ID="RequiredFieldValidator3"
                            runat="server"
                            ControlToValidate="txt_tel"
                            ErrorMessage="Not valit number"
                            ForeColor="Red"
                            ValidationExpression="[0-9]{6,}">
                        </asp:RegularExpressionValidator>
                        <br />

                        <label for="Name">Web Site : </label>
                        <asp:TextBox ID="txt_web" TabIndex="60" runat="server" />
                        <asp:RegularExpressionValidator ID="RequiredFieldValidator4"
                            runat="server"
                            ControlToValidate="txt_web"
                            ErrorMessage="Not valid URL"
                            ForeColor="Red"
                            ValidationExpression="^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$">
                        </asp:RegularExpressionValidator>
                        <br />
                        <label for="email">Email : </label>
                        <asp:TextBox ID="txtEmail" TabIndex="70" runat="server" />
                        <asp:RegularExpressionValidator ID="valEmail"
                            runat="server"
                            ErrorMessage="Not valid."
                            ForeColor="Red"
                            ControlToValidate="txtEmail"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        </asp:RegularExpressionValidator>
                        <br />

                        <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                        <div align="center">
                            <asp:Button ID="btn_save" Text="Save" runat="server"
                                OnClick="btnSave_Click" TabIndex="80" />
                            <asp:Button ID="btn_reset" Text="Reset" runat="server" CausesValidation="False"
                                OnClick="btnReset_Click" TabIndex="90" />
                        </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
