<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAddress.ascx.cs"
    Inherits="FashionZone.Admin.Controls.CustomerAddress" %>
<style type="text/css">
    .accordionContent
    {
        background-color: #D3DEEF;
        border-color: -moz-use-text-color #2F4F4F #2F4F4F;
        border-right: 1px dashed #2F4F4F;
        border-style: none dashed dashed;
        border-width: medium 1px 1px;
        padding: 10px 5px 5px;
    }
    .accordionHeaderSelected
    {
        background-color: #D3DEEF;
        color: Black;
        border: 1px solid #2F4F4F;
        cursor: pointer;
        font-family: Arial,Sans-Serif;
        font-size: 12px;
        font-weight: bold;
        margin-top: 5px;
        padding: 5px;
    }
    .accordionHeader
    {
        color: Black;
        cursor: pointer;
        font-family: Arial,Sans-Serif;
        font-size: 12px;
        font-weight: bold;
        margin-top: 5px;
        padding: 5px;
    }
    .hideContent
    {
        display: none;
        height: 0px;
        background-color: White;
    }
</style>
<ajaxToolkit:Accordion ID="accAddresses" runat="server" SelectedIndex="0" ContentCssClass="accordionContent"
    HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
    FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250"
    FramesPerSecond="40" RequireOpenedPane="true" AutoSize="None">
    <Panes>
        <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="hideContent">
            <Header>
                <asp:Literal ID="litAddressTitle" runat="server" Text="Address"></asp:Literal></Header>
            <Content>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Literal ID="litName" runat="server"></asp:Literal>
                    <br />
                     <asp:Literal ID="litTel" runat="server"></asp:Literal>
                    <asp:Literal ID="litAddress" runat="server"></asp:Literal>
                    <br />
                    <asp:Literal ID="litOther" runat="server"></asp:Literal>
                    <br />
                    <asp:LinkButton ID="lnkDelAddr" runat="server" Text="Remove address" OnClick="lnkDelAddr_Click" Visible="false" Font-Bold="true"></asp:LinkButton>
                     <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelAddr"
                                    ConfirmText="Are you sure about deleting address?" />
                </asp:Panel>
            </Content>
        </ajaxToolkit:AccordionPane>
        <ajaxToolkit:AccordionPane ID="pane" runat="server">
            <Header>
                Modify address</Header>
            <Content>
                <asp:Panel ID="pnlModify" runat="server">
                    <label for="AddressType">
                        Type :</label>
                    <asp:DropDownList ID="ddlAddrType" runat="server" TabIndex="5" Width="151" ViewStateMode="Enabled">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name : &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtName" TabIndex="10" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <label for="Address">
                        Address :</label>
                    <asp:TextBox ID="txtAddress" TabIndex="20" runat="server" TextMode="MultiLine" Height="28px"
                        Width="422px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validAddress" runat="server" ControlToValidate="txtAddress"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <label for="Location">
                        Location :</label>
                    <asp:TextBox ID="txtLocation" TabIndex="30" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validLoc" runat="server" ControlToValidate="txtLocation"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp; Telephone :
                    <asp:TextBox ID="txtTel" TabIndex="40" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validTel" runat="server" ControlToValidate="txtTel"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <label for="PostalCode">
                        Postal code :</label>
                    <asp:TextBox ID="txtPostal" TabIndex="50" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valPostal" runat="server" ControlToValidate="txtPostal"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp; City :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlCities" runat="server" TabIndex="60" Width="151" ViewStateMode="Enabled">
                    </asp:DropDownList>
                    <div align="center">
                        <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click" Text="Cancel" Font-Bold="true" />&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click" Text="Save" Font-Bold="true" 
                             />
                    </div>
                </asp:Panel>
            </Content>
        </ajaxToolkit:AccordionPane>
    </Panes>
</ajaxToolkit:Accordion>
<asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
<br />
