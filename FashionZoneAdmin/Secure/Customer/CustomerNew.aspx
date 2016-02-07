<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CustomerNew.aspx.cs" Inherits="FashionZone.Admin.Secure.Customer.CustomerNew" %>

<%@ Register Src="~/CustomControl/CustomerAddress.ascx" TagPrefix="custom" TagName="address" %>
<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="bonus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .customCheck input
        {
            margin-top: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
            <input type="hidden" id="addressCount" runat="server" value="1" />
            <div id="box">
                <h3 id="addUser">
                    Add Customer</h3>
                <div id="form">
                    <ajaxToolkit:TabContainer ID="tabCustContainer" runat="server" ActiveTabIndex="0">
                        <ajaxToolkit:TabPanel ID="tabInfo" runat="server" HeaderText="Information" CssClass="TabContent">
                            <ContentTemplate>
                                <fieldset id="personal">
                                    <legend>Personal information</legend>
                                    <label for="Name">
                                        Name :</label>
                                    <asp:TextBox ID="txtName" TabIndex="10" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="validName" runat="server" ControlToValidate="txtName"
                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="customerValidation"></asp:RequiredFieldValidator>
                                    <br />
                                    <label for="Surname">
                                        Surname :</label>
                                    <asp:TextBox ID="txtSurname" TabIndex="20" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="validSurname" runat="server" ControlToValidate="txtSurname"
                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="customerValidation"></asp:RequiredFieldValidator>
                                    <br />
                                    <label for="birthDate">
                                        Birth date :
                                    </label>
                                    <asp:TextBox ID="txtBirthDate" TabIndex="25" runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="calBDateExtender" runat="server" TargetControlID="txtBirthDate"
                                        FirstDayOfWeek="Monday" Format="dd/MM/yyyy" Enabled="True">
                                    </ajaxToolkit:CalendarExtender>
                                    <br />
                                    <label for="registrationDate">
                                        Registration :
                                    </label>
                                    <asp:TextBox ID="txtRegDate" TabIndex="27" runat="server" />
                                    <br />
                                    <label for="gender">
                                        Gender :
                                    </label>
                                    <asp:RadioButton ID="btnMale" runat="server" GroupName="radioGender" Checked="True"
                                        TabIndex="27" />
                                    Male
                                    <asp:RadioButton ID="btnFemale" runat="server" GroupName="radioGender" TabIndex="28" />
                                    Female
                                    <br />
                                    <label for="Email">
                                        Email :</label>
                                    <asp:TextBox ID="txtEmail" TabIndex="30" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="validEmail" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="customerValidation"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="valEmail" runat="server" ErrorMessage="Not valid."
                                        ForeColor="Red" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="customerValidation"></asp:RegularExpressionValidator>
                                    <br />
                                    <label for="Active">
                                        Active :
                                    </label>
                                    <asp:CheckBox ID="chkActive" runat="server" TabIndex="34" Checked="true" CssClass="customCheck" />
                                    <br />
                                    <label for="Newsletter">
                                        Newsletter :
                                    </label>
                                    <asp:CheckBox ID="chkNewsletter" runat="server" TabIndex="36" Checked="true" CssClass="customCheck" />
                                    <br />
                                    <label for="phone">
                                        Telephone :
                                    </label>
                                    <asp:TextBox ID="txtPhone" TabIndex="40" runat="server" />
                                    <br />
                                    <label for="mobile">
                                        Mobile :
                                    </label>
                                    <asp:TextBox ID="txtMobile" TabIndex="40" runat="server" />
                                    <br />
                                    <label for="pass">
                                        Password :
                                    </label>
                                    <asp:TextBox ID="txtPass" TabIndex="40" runat="server" TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="validPassword" runat="server" ControlToValidate="txtPass"
                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="customerValidation"></asp:RequiredFieldValidator>
                                    <br />
                                    <label for="pass-2">
                                        Password :
                                    </label>
                                    <asp:TextBox ID="txtPass2" TabIndex="50" runat="server" TextMode="Password" />
                                    <asp:CompareValidator ID="comparePassword" runat="server" ControlToValidate="txtPass"
                                        ControlToCompare="txtPass2" ErrorMessage="Passwords must match!" ForeColor="Red"
                                        ValidationGroup="customerValidation"></asp:CompareValidator>
                                    <br />
                                    <label for="invitedFrom">
                                        Invited from :
                                    </label>
                                    <a href="" runat="server" id="aInvitedFrom" target="_blank">
                                        <asp:Literal runat="server" ID="litInvitedFrom"></asp:Literal>
                                    </a>
                                </fieldset>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="tabAddresses" runat="server" HeaderText="Addresses" ViewStateMode="Enabled"
                            CssClass="TabContent">
                            <ContentTemplate>
                                <fieldset id="addresses">
                                    <custom:address ID="address1" runat="server" Visible="false" OnAddressDelete="address_OnAddressDelete"
                                        OnAddressAsyncRemove="address_OnAddressAsyncRemove" />
                                    <custom:address ID="address2" runat="server" Visible="false" OnAddressDelete="address_OnAddressDelete"
                                        OnAddressAsyncRemove="address_OnAddressAsyncRemove" />
                                    <custom:address ID="address3" runat="server" Visible="false" OnAddressDelete="address_OnAddressDelete"
                                        OnAddressAsyncRemove="address_OnAddressAsyncRemove" />
                                    <custom:address ID="address4" runat="server" Visible="false" OnAddressDelete="address_OnAddressDelete"
                                        OnAddressAsyncRemove="address_OnAddressAsyncRemove" />
                                    <custom:address ID="address5" runat="server" Visible="false" OnAddressDelete="address_OnAddressDelete"
                                        OnAddressAsyncRemove="address_OnAddressAsyncRemove" />
                                    <asp:LinkButton ID="lnkAddAddress" runat="server" Text="Add address" OnClick="lnkAddAddress_Click"
                                        Font-Bold="true"></asp:LinkButton>
                                </fieldset>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="tabOrders" runat="server" HeaderText="Orders">
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="tabBonus" runat="server" HeaderText="Bonuses" CssClass="TabContent">
                            <ContentTemplate>
                                <bonus:FZGrid ID="gridBonus" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" OnSorting="gridBonus_Sorting" ViewStateMode="Enabled"
                                    OnPageIndexChanging="gridBonus_PageIndexChanging" SortOrder="Ascending" CustomVirtualItemCount="-1"
                                    HeaderStyle-CssClass="GridHeader">
                                    <Columns>
                                        <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" DataFormatString="{0:N2}"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="ValueRemainder" HeaderText="Remainder" SortExpression="ValueRemainder"
                                            DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="DateAssigned" HeaderText="Date Assigned" SortExpression="DateAssigned"
                                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                        <asp:BoundField DataField="Validity" HeaderText="Validity" SortExpression="Validity"
                                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    </Columns>
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                                </bonus:FZGrid>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="tabInvite" runat="server" HeaderText="Invites" CssClass="TabContent">
                            <ContentTemplate>
                                <bonus:FZGrid ID="gridInvitation" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" OnSorting="gridCustomer_Sorting" ViewStateMode="Enabled"
                                    OnPageIndexChanging="gridCustomer_PageIndexChanging" SortOrder="Ascending" CustomVirtualItemCount="-1"
                                    HeaderStyle-CssClass="GridHeader">
                                    <Columns>
                                        <asp:BoundField DataField="InvitedMail" HeaderText="Invited Mail" SortExpression="InvitedMail" />
                                        <asp:BoundField DataField="RegistrationDate" HeaderText="Registration Date" SortExpression="RegistrationDate"
                                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                        <asp:CheckBoxField DataField="Registered" HeaderText="Registered" SortExpression="Registered" />
                                    </Columns>
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                                </bonus:FZGrid>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                    <br />
                    <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                    <div align="center">
                        <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" ValidationGroup="customerValidation"
                            Width="59px" TabIndex="110" />
                        <asp:Button ID="btnReset" Text="Reset" runat="server" CausesValidation="False" OnClick="btnReset_Click"
                            TabIndex="120" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
