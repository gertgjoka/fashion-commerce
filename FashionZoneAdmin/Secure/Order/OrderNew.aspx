<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="OrderNew.aspx.cs" Inherits="FashionZone.Admin.Secure.Order.OrderNew"
    EnableEventValidation="false" EnableViewStateMac="true" ViewStateEncryptionMode="Always" %>

<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="custom" %>
<%@ Register Src="~/CustomControl/CustomerAddress.ascx" TagPrefix="custom" TagName="address" %>
<%@ Register Src="~/CustomControl/OrderDetails.ascx" TagPrefix="custom" TagName="cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        /* HTML editor custom css to override values from the general one */
        .customCheck input
        {
            margin-top: 10px;
        }

        .hideGridColumn
        {
            display: none;
        }

        .customSelect
        {
            margin-top: 3px;
        }

        .customBox
        {
            margin-left: 7px;
        }

        .customText
        {
            margin-bottom: 0px;
        }
    </style>
    <script type="text/javascript" src="/js/jquery-1.8.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="box">
        <h3 id="addOrder">Order
        </h3>
        <div id="form">
            <ajaxToolkit:TabContainer ID="tabContainer" runat="server" ActiveTabIndex="0">
                <ajaxToolkit:TabPanel runat="server" ID="tabInfo" HeaderText="Information" CssClass="TabContent">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="pnlCustomerBonus" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <fieldset id="orderInfo">
                                    <legend>Order information</legend>
                                    <asp:Panel runat="server" ID="divOnEdit" Visible="false" ViewStateMode="Enabled">
                                        <label for="Number">
                                            Number :</label>
                                        <asp:Literal runat="server" ID="litNumber" ViewStateMode="Enabled">0</asp:Literal><br />
                                        <label for="Date">
                                            Date :</label>
                                        <asp:Label runat="server" ID="lblDate" CssClass="customSelect" ViewStateMode="Enabled"></asp:Label><br />
                                        <label for="Status">
                                            Status :</label>
                                        <asp:DropDownList runat="server" ID="ddlStatus" TabIndex="20" DataValueField="ID"
                                            DataTextField="Name" ViewStateMode="Enabled" CssClass="customSelect">
                                        </asp:DropDownList>
                                        <br />
                                        <label for="Completed">
                                            Completed :</label>
                                        <asp:CheckBox runat="server" ID="chkCompleted" TabIndex="40" CssClass="customCheck" />
                                        <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender2"
                                            runat="server" Enabled="True" Key="CanceledOrCompleted" TargetControlID="chkCompleted">
                                        </ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                                        Canceled :
                                    <asp:CheckBox runat="server" ID="chkCanceled" TabIndex="50" CssClass="customCheck" Enabled="false"/><br />
                                        <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender1"
                                            runat="server" Enabled="True" Key="CanceledOrCompleted" TargetControlID="chkCanceled">
                                        </ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
                                    </asp:Panel>
                                    <label for="Customer">
                                        Customer :
                                    </label>
                                    <asp:TextBox ID="txtCustomer" runat="server" />
                                    <asp:RequiredFieldValidator ID="validCustomer" runat="server" ErrorMessage="*" ForeColor="Red"
                                        ControlToValidate="txtCustomer" ValidationGroup="validateOrder"></asp:RequiredFieldValidator>
                                    <asp:Button ID="btnSelectCustomer" runat="server" Height="25px" Text="Select" Width="63px"
                                        CausesValidation="False" OnClick="btnSelectCustomer_Click" TabIndex="60" ViewStateMode="Enabled" />
                                    <br />
                                    <label for="Amount">
                                        Amount :</label>
                                    <asp:TextBox ID="txtAmount" runat="server" MaxLength="7" Style="text-align: right;" Enabled="false"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="validAmount" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAmount" ValidationGroup="validateOrder"></asp:RequiredFieldValidator>--%>
                                    <br />
                                    <label for="Bonus">
                                        Bonus :</label>
                                    <asp:LinkButton ID="lnkBonus" runat="server" OnClick="lnkBonus_Click" ViewStateMode="Enabled"
                                        TabIndex="65">0,00</asp:LinkButton>
                                    <br />
                                    <label for="Paid">
                                        Paid :</label>
                                    <asp:TextBox ID="txtPaid" runat="server" MaxLength="7" Style="text-align: right;" Enabled="false"></asp:TextBox>
                                    <br />
                                    <label for="comments">
                                        Comments* :
                                    </label>
                                    <asp:TextBox ID="txtComments" TabIndex="110" runat="server" TextMode="MultiLine"
                                        Rows="3" Height="50" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="*"
                                        ForeColor="Red" ControlToValidate="txtComments" ValidationGroup="validateOrder"></asp:RequiredFieldValidator>
                                    <br />
                                    <label for="Shipping">
                                        Shipping :</label>
                                    <asp:DropDownList runat="server" ID="ddlShipping" TabIndex="25" DataValueField="ID"
                                        DataTextField="ShippingType" ViewStateMode="Enabled" CssClass="customSelect"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlShipping_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    Cost :
                                    <asp:TextBox runat="server" ID="txtShippingCost" Width="46" CssClass="customBox"></asp:TextBox>
                                    <br />
                                </fieldset>
                                <%--                                <fieldset id="Fieldset2">
                                    <legend>Shipping details</legend>--%>

                                <div runat="server" visible="false">
                                    <label for="Shipped">
                                        Shipped on :
                                    </label>
                                    <asp:TextBox ID="txtDateShipped" TabIndex="60" runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="calFromExtender" runat="server" TargetControlID="txtDateShipped"
                                        FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    hh:mm
                                    <asp:TextBox ID="txtTimeShipped" TabIndex="70" runat="server" Width="46px" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Not a valid time"
                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ForeColor="Red" ControlToValidate="txtTimeShipped"
                                        ValidationGroup="campaignValidate"></asp:RegularExpressionValidator>
                                    <br />
                                    <label for="Delivery">
                                        Delivered on :
                                    </label>
                                    <asp:TextBox ID="txtDeliveryDate" TabIndex="80" runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDeliveryDate"
                                        FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    hh:mm
                                    <asp:TextBox ID="txtDeliveryTime" TabIndex="90" runat="server" Width="46px" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Not a valid time"
                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ForeColor="Red" ControlToValidate="txtDeliveryTime"
                                        ValidationGroup="campaignValidate"></asp:RegularExpressionValidator>
                                    <br />
                                    <label for="Tracking">
                                        Tracking No. :
                                    </label>
                                    <asp:TextBox ID="txtTracking" TabIndex="100" runat="server" />
                                    <br />
                                    <label for="ShippingDetails">
                                        Shipping Details :
                                    </label>
                                    <asp:TextBox ID="txtShippingDetails" TabIndex="110" runat="server" TextMode="MultiLine"
                                        Rows="3" Height="50" />
                                    <br />
                                </div>
                                <%-- </fieldset>--%>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlShipping" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <fieldset id="orderDet">
                            <legend>Products</legend>
                            <asp:UpdatePanel runat="server" ID="updProducts" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                <ContentTemplate>
                                    <custom:cart runat="server" ID="cart" Visible="false" OnNeedRefresh="cart_NeedRefresh" />
                                    <asp:LinkButton runat="server" ID="lnkAddProduct" OnClick="lnkAddProduct_Click" Font-Bold="true"
                                        Visible="true" ViewStateMode="Enabled">Add product</asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                        <asp:UpdatePanel runat="server" ID="updAddresses" UpdateMode="Conditional" ViewStateMode="Enabled">
                            <ContentTemplate>
                                <fieldset id="shAddress" runat="server" visible="false">
                                    <legend>Shipping address</legend>
                                    <asp:DropDownList runat="server" ID="ddlShippingAddress" Width="400" Enabled="false"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlShippingAddress_SelectedIndexChanged"
                                        DataTextField="AddressSummary" DataValueField="ID" ViewStateMode="Enabled">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:LinkButton runat="server" ID="lnkAddAddress" OnClick="lnkAddAddress_Click" Visible="false">Add address</asp:LinkButton>
                                    <custom:address ID="address1" runat="server" Visible="false" InOrder="true" OnSaving="address1_Saving" />
                                </fieldset>
                                <fieldset id="bAddress" runat="server" visible="false">
                                    <legend>Billing address</legend>
                                    <asp:DropDownList runat="server" ID="ddlBillingAddress" Width="400" Enabled="false"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBillingAddress_SelectedIndexChanged"
                                        DataTextField="AddressSummary" DataValueField="ID" ViewStateMode="Enabled">
                                    </asp:DropDownList>
                                    <br />
                                    <custom:address ID="address2" runat="server" Visible="false" InOrder="true" OnSaving="address2_Saving" />
                                </fieldset>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlShippingAddress" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlBillingAddress" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel runat="server" ID="updPayment" UpdateMode="Conditional" ViewStateMode="Enabled">
                            <ContentTemplate>
                                <fieldset id="Fieldset1">
                                    <legend>Payment</legend>
                                    <script type="text/javascript">
                                        bindRadio();

                                        //The add_endRequest function is needed to rebind the jquery events on every update panel partial refresh
                                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindRadio);

                                        function bindRadio() {
                                            $(function () {
                                                $('#radioBtnCash').attr('name', 'RadioGroup');
                                                $('#radioBtnPaypal').attr('name', 'RadioGroup');
                                                $('#radioBtnEasypay').attr('name', 'RadioGroup');

                                                $('#accPaneCashDiv').click(function () {
                                                    $('#radioBtnCash').attr('checked', 'checked');
                                                    $('#selectedPayment').val('1');
                                                });

                                                $('#accPanePaypalDiv').click(function () {
                                                    $('#selectedPayment').val('2');
                                                    $('#radioBtnPaypal').attr('checked', 'checked');
                                                });

                                                $('#accPaneEasyPay').click(function () {
                                                    $('#selectedPayment').val('3');
                                                    $('#radioBtnEasypay').attr('checked', 'checked');
                                                });
                                            });
                                        }
                                    </script>
                                    <asp:HiddenField runat="server" ID="selectedPayment" ClientIDMode="Static" Value="1" />
                                    <ajaxToolkit:Accordion ID="accordionPayment" runat="server" SelectedIndex="0" FadeTransitions="true"
                                        TransitionDuration="120" FramesPerSecond="70" RequireOpenedPane="true" AutoSize="None">
                                        <Panes>
                                            <ajaxToolkit:AccordionPane ID="accPaneCash" ClientIDMode="Static" runat="server">
                                                <Header>
                                                    <div id="accPaneCashDiv">
                                                        <asp:RadioButton ID="radioBtnCash" ClientIDMode="Static" runat="server" Checked="true"
                                                            Text="Cash Payment" ViewStateMode="Enabled" />
                                                    </div>
                                                </Header>
                                                <Content>
                                                    <label for="Receiver">
                                                        Receiver</label>
                                                    <asp:TextBox runat="server" ID="txtReceiver" TabIndex="510"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                        ForeColor="Red" ControlToValidate="txtReceiver" ValidationGroup="CashValidation"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <label for="Amount">
                                                        Amount</label>
                                                    <asp:TextBox runat="server" ID="txtPaymentAmount" TabIndex="520"></asp:TextBox><br />
                                                    <label for="Amount">
                                                        Paid on</label>
                                                    <asp:TextBox runat="server" ID="txtPaidOn" TabIndex="530"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPaidOn"
                                                        FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <br />
                                                    <label for="Amount">
                                                        Comments</label>
                                                    <asp:TextBox runat="server" ID="txtCashComments" TabIndex="540" TextMode="MultiLine"
                                                        Rows="3" Height="50" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                        ForeColor="Red" ControlToValidate="txtCashComments" ValidationGroup="CashValidation"></asp:RequiredFieldValidator>
                                                    <br />
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="accPanePaypal" runat="server" ClientIDMode="Static" Visible="false">
                                                <Header>
                                                    <div id="accPanePaypalDiv">
                                                        <asp:RadioButton ID="radioBtnPaypal" ClientIDMode="Static" runat="server" Checked="false"
                                                            Text="Paypal Payment" ViewStateMode="Enabled" />
                                                    </div>
                                                </Header>
                                                <Content>
                                                    <div class="customText">
                                                        <label for="Transaction" style="margin-bottom: 0px;">
                                                            Transaction</label>
                                                        <asp:Label ID="litPPTransaction" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="Status" style="margin-bottom: 0px;">
                                                            Status</label>
                                                        <b>
                                                            <asp:Label ID="litPPStatus" runat="server" CssClass="customText"></asp:Label></b>
                                                        <br />
                                                        <label for="Amount" style="margin-bottom: 0px;">
                                                            Amount</label>
                                                        <asp:Label ID="litPPAmount" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="Fee" style="margin-bottom: 0px;">
                                                            Fee</label>
                                                        <asp:Label ID="litPPFee" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="PaidOn" style="margin-bottom: 0px;">
                                                            Date</label>
                                                        <asp:Label ID="litPPDate" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="Currency" style="margin-bottom: 0px;">
                                                            Currency</label>
                                                        <asp:Label ID="litPPCurrency" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="PayerEmail" style="margin-bottom: 0px;">
                                                            Payer Email</label>
                                                        <asp:Label ID="litPPPayerEmail" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="PayerName" style="margin-bottom: 0px;">
                                                            Payer Name</label>
                                                        <asp:Label ID="litPPName" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="PayerStatus" style="margin-bottom: 0px;">
                                                            Payer Status</label>
                                                        <asp:Label ID="litPPPayerStatus" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="Response" style="margin-bottom: 0px;">
                                                            Response</label>
                                                        <asp:TextBox ID="txtPPResponse" TabIndex="110" runat="server" TextMode="MultiLine"
                                                            Rows="5" Height="70" Enabled="false" />
                                                    </div>
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                            <ajaxToolkit:AccordionPane ID="accPaneEasyPay" runat="server" ClientIDMode="Static" Visible="false">
                                                <Header>
                                                    <div id="accPaneEasypayDiv">
                                                        <asp:RadioButton ID="radioBtnEasypay" ClientIDMode="Static" runat="server" Checked="false"
                                                            Text="EasyPay Payment" ViewStateMode="Enabled" />
                                                    </div>
                                                </Header>
                                                <Content>
                                                    <div class="customText">
                                                        <label for="Transaction" style="margin-bottom: 0px;">
                                                            Transaction</label>
                                                        <asp:Label ID="lblEPTransaction" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="PaidOn" style="margin-bottom: 0px;">
                                                            Date</label>
                                                        <asp:Label ID="lblEPDate" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="Status" style="margin-bottom: 0px;">
                                                            Status</label>
                                                        <b>
                                                            <asp:Label ID="lblEPStatus" runat="server" CssClass="customText"></asp:Label></b>
                                                        <br />
                                                        <label for="Amount" style="margin-bottom: 0px;">
                                                            Amount</label>
                                                        <asp:Label ID="lblEPAmount" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="Fee" style="margin-bottom: 0px;">
                                                            Fee</label>
                                                        <asp:Label ID="lblEPFee" runat="server" CssClass="customText"></asp:Label>
                                                         <br />
                                                        <label for="Rate" style="margin-bottom: 0px;">
                                                            Rate</label>
                                                        <asp:Label ID="lblEPRate" runat="server" CssClass="customText"></asp:Label>
                                                        <br />
                                                        <label for="Response" style="margin-bottom: 0px;">
                                                            Response</label>
                                                        <asp:TextBox ID="lblEPResponse" TabIndex="110" runat="server" TextMode="MultiLine"
                                                            Rows="5" Height="70" Enabled="false" />
                                                    </div>
                                                </Content>
                                            </ajaxToolkit:AccordionPane>
                                        </Panes>
                                    </ajaxToolkit:Accordion>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" ID="tabNotes" HeaderText="Notes" CssClass="TabContent">
                    <ContentTemplate>
                        <asp:UpdatePanel runat="server" ID="updNotes" UpdateMode="Conditional" ViewStateMode="Enabled">
                            <ContentTemplate>
                                <fieldset id="Notes">
                                    <legend>Order notes</legend>
                                    <asp:GridView runat="server" ID="gridNotes" AllowPaging="false" AllowSorting="false"
                                        AutoGenerateColumns="False" HeaderStyle-CssClass="GridHeader">
                                        <Columns>
                                            <asp:BoundField DataField="CreatedOn" HeaderText="Created on" DataFormatString="{0:g}"
                                                ItemStyle-Width="125" />
                                            <asp:BoundField DataField="Text" HeaderText="Note" />
                                            <asp:CheckBoxField DataField="DisplayToCustomer" HeaderText="Display to customer"
                                                ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Login" HeaderText="User" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <br />
                                    <h4>Add order note</h4>
                                    <br />
                                    <label for="Note">
                                        Note :</label>
                                    <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine" TabIndex="1010"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="validTxtNote" runat="server" ErrorMessage="*" ControlToValidate="txtNote"
                                        ForeColor="Red" ValidationGroup="validNote"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:CheckBox runat="server" ID="chkDisplayToCustomer" Text="Display to customer"
                                        TabIndex="1020" />
                                    <div align="center">
                                        <asp:Button runat="server" ID="btnAddNote" Text="Add note" TabIndex="1030" OnClick="btnAddNote_Click"
                                            ValidationGroup="validNote" Visible="false" />
                                    </div>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
            <br />
            <asp:UpdatePanel ID="updPnl" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional"
                ViewStateMode="Enabled">
                <ContentTemplate>
                    <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                    <div align="center">
                        <asp:Button ID="buttExportPdf" Text="Print" runat="server" Width="59px"
                            TabIndex="105" Visible="False" OnClick="buttExportPdf_Click" />
                        <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" ValidationGroup="validateOrder"
                            Width="59px" TabIndex="110" />
                        <ajaxToolkit:ConfirmButtonExtender ID="btnConfirmSaving" runat="server" TargetControlID="btnSave"
                            ConfirmText="The order cannot be modified once it is saved. Are you sure you want to proceed?" />
                        <asp:Button ID="btnReset" Text="Reset" runat="server" CausesValidation="False"
                            OnClick="btnReset_Click" TabIndex="1220" />
                        <ajaxToolkit:ConfirmButtonExtender ID="btnConfirmCancel" runat="server" TargetControlID="btnReset"
                            ConfirmText="All the changes will be lost, and the cart will be emptied. Are you sure you want to proceed?" />
                         <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" Visible="false"
                            OnClick="btnCancel_Click" TabIndex="1221" />
                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
                            ConfirmText="All the products will be returned available to warehouse. Are you sure you want to proceed?" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--Customer popup--%>
    <ajaxToolkit:ModalPopupExtender ID="popupCustomer" runat="server" TargetControlID="fake"
        CancelControlID="btnClose" Drag="false" PopupControlID="pnlCustomer" Enabled="True"
        BackgroundCssClass="modalBackground" />
    <asp:HiddenField ID="fake" runat="server" />
    <asp:Panel ID="pnlCustomer" CssClass="modalPopup" runat="server" Width="500">
        <asp:Panel ID="DialogHeaderFrame" CssClass="DialogHeaderFrame" runat="server" Width="500px">
            <asp:Panel ID="DialogHeader" runat="server" CssClass="DialogHeader">
                &nbsp;<asp:Label ID="LblPopupHeader" runat="server" Text="Choose Customer" />
            </asp:Panel>
        </asp:Panel>
        <asp:UpdatePanel ID="updModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:Label ID="Label1" runat="server">Name</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtSearchName" runat="server"></asp:TextBox>
                    <span></span>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                        CausesValidation="False" />
                </div>
                <custom:FZGrid ID="gridCustomer" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridCustomer_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridCustomer_PageIndexChanging" SortOrder="Ascending" HeaderStyle-CssClass="GridHeader"
                    CustomVirtualItemCount="-1">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" Text="Select"
                                    CommandArgument='<%# Eval("ID")%>' OnClick="lnkSelect_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                </custom:FZGrid>
                <br />
                <div align="center">
                    <asp:Button ClientIDMode="Static" ID="btnClose" Text="Close" ToolTip="Close dialog"
                        CausesValidation="false" Width="70px" runat="server" OnClick="btnClose_Click" /><br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--End customer popup--%>
    <%--Bonus popup--%>
    <ajaxToolkit:ModalPopupExtender ID="popupBonus" runat="server" TargetControlID="fake2"
        CancelControlID="btnCloseBonus" Drag="false" PopupControlID="pnlBonus" Enabled="True"
        BackgroundCssClass="modalBackground" />
    <asp:HiddenField ID="fake2" runat="server" />
    <asp:Panel ID="pnlBonus" CssClass="modalPopup" runat="server" Width="500">
        <asp:Panel ID="Panel2" CssClass="DialogHeaderFrame" runat="server" Width="500px">
            <asp:Panel ID="Panel3" runat="server" CssClass="DialogHeader">
                &nbsp;<asp:Label ID="Label2" runat="server" Text="Bonuses for this order" />
            </asp:Panel>
        </asp:Panel>
        <asp:UpdatePanel ID="updModalBonus" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <custom:FZGrid ID="gridBonus" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridBonus_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridBonus_PageIndexChanging" SortOrder="Ascending" HeaderStyle-CssClass="GridHeader"
                    CustomVirtualItemCount="-1" OnRowCommand="gridBonus_RowCommand" DataKeyNames="Version">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelectBonus" runat="server" CausesValidation="False" Text="Select"
                                    CommandArgument='<%# Eval("ID") + "|" + Eval("ValueRemainder")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
                        <asp:BoundField DataField="ValueRemainder" HeaderText="Remainder" SortExpression="Remainder" />
                        <asp:BoundField DataField="DateAssigned" HeaderText="Assigned" SortExpression="Assigned"
                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:BoundField DataField="Validity" HeaderText="Validity" SortExpression="Validity"
                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hideGridColumn"
                            ItemStyle-CssClass="hideGridColumn" />
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                </custom:FZGrid>
                <br />
                <asp:Panel ID="divSelectedBonuses" runat="server" ViewStateMode="Enabled" Visible="false">
                    Bonuses for this order
                    <asp:CheckBoxList ID="chkListBonuses" runat="server">
                    </asp:CheckBoxList>
                    <asp:Button ID="btnRemoveBonus" runat="server" Text="Remove selected" OnClick="btnRemoveBonus_lick" />
                </asp:Panel>
                <asp:Label ID="lblBonusError" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                <div align="right">
                    Total order:
                    <asp:Literal ID="litTotalOrder" runat="server" ViewStateMode="Enabled">0</asp:Literal><br />
                    Total bonus:
                    <asp:Literal ID="litTotalBonus" runat="server" ViewStateMode="Enabled">0</asp:Literal>
                </div>
                <div align="center">
                    <asp:Button ClientIDMode="Static" ID="btnCloseBonus" Text="Close" ToolTip="Close dialog"
                        CausesValidation="false" Width="70px" runat="server" OnClick="btnCloseBonus_Click" /><br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--End bonus popup--%>
    <%--Product popup--%>
    <ajaxToolkit:ModalPopupExtender ID="popupProduct" runat="server" TargetControlID="fake3"
        CancelControlID="btnCloseProduct" Drag="true" PopupControlID="pnlProduct" Enabled="True"
        BackgroundCssClass="modalBackground" />
    <asp:HiddenField ID="fake3" runat="server" />
    <asp:Panel ID="pnlProduct" CssClass="modalPopup" runat="server" Width="600">
        <asp:Panel ID="Panel4" CssClass="DialogHeaderFrame" runat="server" Width="600px">
            <asp:Panel ID="Panel5" runat="server" CssClass="DialogHeader">
                &nbsp;<asp:Label ID="Label3" runat="server" Text="Choose products for this order" />
            </asp:Panel>
        </asp:Panel>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" ViewStateMode="Enabled">
                    Brand :
                                        <asp:DropDownList ID="ddlNewBrand" runat="server" ViewStateMode="Enabled" />
                    <ajaxToolkit:CascadingDropDown ID="CascadingDropDown3" runat="server" LoadingText="Loading brands..."
                        TargetControlID="ddlNewBrand" Category="BRAND" PromptText="Choose a brand" ServiceMethod="GetBrand"
                        ServicePath="~/Services/PopulateCddlCat.asmx" ViewStateMode="Enabled" Enabled="True" />
                    Campaign :
                                        <asp:DropDownList ID="ddlNewCampaign" runat="server" OnSelectedIndexChanged="ddlCampaign_SelectedIndexChanged"
                                            AutoPostBack="True" ViewStateMode="Enabled" />
                    <ajaxToolkit:CascadingDropDown ID="CascadingDropDown4" runat="server" LoadingText="Loading campaigns..."
                        TargetControlID="ddlNewCampaign" ParentControlID="ddlNewBrand" Category="CAMPAIN" PromptText="Choose a campaign"
                        ServiceMethod="GetCampain" ServicePath="~/Services/PopulateCddlCat.asmx" Enabled="True" />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlNewCampaign" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updModalProducts" runat="server" ChildrenAsTriggers="false"
            UpdateMode="Conditional">
            <ContentTemplate>
                <custom:FZGrid ID="gridProduct" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridProduct_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridProduct_PageIndexChanging" SortOrder="Ascending" HeaderStyle-CssClass="GridHeader"
                    CustomVirtualItemCount="-1" OnRowCommand="gridProduct_RowCommand" OnRowDataBound="gridProduct_RowDataBound"
                    DataKeyNames="ID">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelectProduct" runat="server" CausesValidation="False" Text="Select"
                                    CommandArgument='<%# Eval("ID") + "|" + Eval("OurPrice")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ImageField DataImageUrlField="Thumbnail" NullDisplayText="No Image">
                            <ItemStyle Wrap="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:ImageField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="OurPrice" HeaderText="Price" SortExpression="OurPrice"
                            DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="OriginalPrice" HeaderText="Original Price" SortExpression="OriginalPrice"
                            DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                        <%--<asp:BoundField DataField="Discount" HeaderText="Discount" SortExpression="Discount"
                            ItemStyle-HorizontalAlign="Right" />--%>
                        <asp:BoundField DataField="Remaining" HeaderText="Remaining" SortExpression="Remaining" />
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate>
                                <custom:FZDropDownList ID="ddlSizeGrid" runat="server" DataTextField="Value" DataValueField="ID"
                                    Width="80" AutoPostBack="True" OnSelectedIndexChanged="ddlSizeGrid_SelectedIndexChanged"
                                    ViewStateMode="Enabled">
                                </custom:FZDropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlQtyGrid" runat="server" Width="80" Enabled="false" ViewStateMode="Enabled">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Campaign" HeaderText="Campaign" />--%>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                </custom:FZGrid>
                <br />
                <asp:Label ID="lblProductError" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                <div align="center">
                    <asp:Button ClientIDMode="Static" ID="btnCloseProduct" Text="Close" ToolTip="Close dialog"
                        CausesValidation="false" Width="70px" runat="server" OnClick="btnCloseProduct_Click" /><br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--End product popup--%>
</asp:Content>
