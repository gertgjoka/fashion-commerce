<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="InvitationNew.aspx.cs" Inherits="FashionZone.Admin.Secure.Customer.InvitationNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        div#form input
        {
            width: 250px;
        }
    </style>
    <script type="text/javascript">
        function checkMultipeMail(source, args) {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            var txt = document.getElementById('<%= txtInvitedMail.ClientID %>').value;
            var n = txt.split(";");
            for (var i = 0; i < n.length; i++) {
                if (!filter.test(n[i])) {
                    return args.IsValid = false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<input id="hdnIdInvt" runat="server" type="hidden" />
<input id="hdnCustomerId" runat="server" type="hidden" />
    <div id="box">
        <div id="form">
            
            <fieldset id="Customer">
                <legend>Invitation</legend>
            <label for="Name">
                Customer : 
            </label>
            <asp:TextBox ID="txtCustomer" runat="server" ></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteName" 
                                              runat="server"
                                              TargetControlID="txtCustomer"
                                              ServicePath="~/Services/AutoCompleteName.asmx"
                                              ServiceMethod="GetNameList"
                                              MinimumPrefixLength="3" 
                                              CompletionInterval="100" 
                                              CompletionListCssClass="autocomplete_completionListElement" 
                                              DelimiterCharacters=";, :"
                                              Enabled="True"
                                              />

            <br />
            <label for="Name">
                Invited Mail : 
            </label>
            <asp:TextBox ID="txtInvitedMail" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                        runat="server" 
                                        ErrorMessage="*"
                                        ControlToValidate="txtInvitedMail" 
                                        ForeColor="Red"
                                        ValidationGroup="Invit" >
            </asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cvalidatorInvitedMail" 
                ControlToValidate="txtInvitedMail"
                ClientValidationFunction="checkMultipeMail" 
                runat="server" 
                ForeColor="Red"
                ErrorMessage="*"
                ValidationGroup="Invit">
            </asp:CustomValidator>
            <br />
            <label for="Name"></label>
            Insert ';' to separate email addresses
            <br />
            <label for="Name">
                Registered : 
            </label>
            <asp:CheckBox ID="chkRegistered" runat="server"/>
            <br />
            <label for="Name">
                Regist. Date : 
            </label>
            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                        runat="server" 
                                        ErrorMessage="*"
                                        ControlToValidate="txtDate" 
                                        ForeColor="Red"
                                        ValidationGroup="Invit" >
            </asp:RequiredFieldValidator>
            <br />
            <ajaxToolkit:CalendarExtender ID="CalExtDate" 
                                            runat="server" 
                                            FirstDayOfWeek="Monday" 
                                            TargetControlID="txtDate"
                                            Format="dd/MM/yyyy">
            </ajaxToolkit:CalendarExtender>
            <br />
                
            </fieldset>
            <asp:Label ID="lblErrors" runat="server" ></asp:Label>
            <div align="center">
                <asp:Button ID="btn_save" Text="Save" runat="server" TabIndex="80" 
                            ValidationGroup="Invit" onclick="btn_save_Click" />
                <asp:Button ID="btn_reset" Text="Reset" runat="server" CausesValidation="False" 
                            TabIndex="90" onclick="btn_reset_Click" />
            </div>
        </div>
    </div>
</asp:Content>