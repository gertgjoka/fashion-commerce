<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BonusNew.aspx.cs"
    Inherits="FashionZone.Admin.Secure.Customer.BonusNew" %>

<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        div#form textarea
        {
            height: 100px;
            overflow: auto;
            padding: 5px;
            width: 410px;
        }
    </style>
    <script type="text/javascript">
        function removeCurrentCustomer() {
            var LeftListBox = document.forms[0].lBoxSelectedCustomer;
            for (var i = (LeftListBox.options.length - 1); i >= 0; i--) {
                if (LeftListBox.options[i].selected)
                    LeftListBox.options[i] = null;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ViewStateMode="Enabled">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <input type="hidden" id="IdBonus" runat="server" />
    <div id="box">
        
        <h3 id="adduser">
            Bonus</h3>
        <div id="form">
        <asp:PlaceHolder ID="plhCustomerAssoc" runat="server">
            <fieldset id="Customer">
                <legend>Customer Associations</legend>
                <div style="margin-bottom: 10px;">
                    <asp:Label ID="lblName" runat="server">Name</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <span></span>
                    <asp:Label ID="lblEmail" runat="server">Email</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <span></span>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
                <cc1:FZGrid ID="gridCustomer" 
                            runat="server" 
                            AllowPaging="True" 
                            AllowSorting="True"
                            AutoGenerateColumns="False" 
                            OnSorting="gridCustomer_Sorting" 
                            ViewStateMode="Enabled"
                            OnPageIndexChanging="gridCustomer_PageIndexChanging" 
                            SortOrder="Ascending" 
                            CustomVirtualItemCount="-1" HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" Text="Select"
                                    CommandArgument='<%# Eval("ID") + "|" + Eval("Name") + " " + Eval("Surname")%>' OnClick="lnkSelect_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField HeaderText="Name" DataTextField="Name" DataNavigateUrlFields="ID"
                            DataNavigateUrlFormatString="CustomerNew.aspx?ID={0}" Target="_blank" />
                        <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:CheckBoxField DataField="Active" HeaderText="Active" ReadOnly="true" SortExpression="Active" />
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                </cc1:FZGrid>
                <br />
                <label for="Name">
                    <asp:Literal ID="litCustomers" Text="Customers:" Visible="false" runat="server"></asp:Literal>
                </label>
                <asp:CheckBoxList ID="CheckSelectedCustomer" 
                                  Visible="false" 
                                  runat="server" ViewStateMode="Enabled">
                </asp:CheckBoxList>
                <asp:Button ID="btnDeleteCusomer" runat="server" Text="Delete selected" Visible="false" OnClick="btnDeleteCusomer_lick" />
            </fieldset>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhBonusData" runat="server" Visible="false">
            <fieldset id="BonusDatas">
                <legend>Bonus Associations</legend>

                <asp:PlaceHolder ID="plhCustomerInfoEdit" runat="server" Visible="false">
                <label for="Name">
                    Customer :
                </label>
                <asp:HyperLink ID="hplCustomer" runat="server"></asp:HyperLink>
                <input type="hidden" id="hdnCustomer" runat="server" />
                <br />
                </asp:PlaceHolder>

                <label for="Name">
                    Bonus Value :
                </label>
                <asp:TextBox ID="txtBonusValue" TabIndex="10" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="validName" 
                                            runat="server" 
                                            ControlToValidate="txtBonusValue"
                                            ErrorMessage="*" 
                                            ValidationGroup="Bonus" 
                                            ForeColor="Red">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                                runat="server" 
                                                ControlToValidate="txtBonusValue"
                                                ValidationExpression="^\d{1,5}(\,\d{2}){0,1}$" 
                                                ErrorMessage="Not a numeric value!" 
                                                ForeColor="Red"
                                                ValidationGroup="Bonus" >
                </asp:RegularExpressionValidator>
                <br />
                <label for="Name">
                    Bonus remainder:
                </label>
                <asp:TextBox ID="txtBonusRemainder" TabIndex="20" runat="server" Enabled="false"></asp:TextBox>
                (read only)
                <br />
                <label for="Name">
                    Date Assigned :
                </label>
                <asp:TextBox ID="txtDateFrom" TabIndex="30" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                            runat="server" 
                                            ErrorMessage="*"
                                            ControlToValidate="txtDateFrom" 
                                            ForeColor="Red"
                                            ValidationGroup="Bonus" >
                </asp:RequiredFieldValidator>
                <br />
                <ajaxToolkit:CalendarExtender ID="CalExtDate" 
                                              runat="server" 
                                              FirstDayOfWeek="Monday" 
                                              TargetControlID="txtDateFrom"
                                              Format="dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
                <label for="Name">
                    Validity:
                </label>
                <asp:TextBox ID="txtValid" TabIndex="40" runat="server">
                </asp:TextBox>
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                            runat="server" 
                                            ControlToValidate="txtValid"
                                            ErrorMessage="*" 
                                            ForeColor="Red"
                                            ValidationGroup="Bonus" >
                </asp:RequiredFieldValidator>
               <ajaxToolkit:CalendarExtender ID="CalendarExtender1" 
                                              runat="server" 
                                              FirstDayOfWeek="Monday" 
                                              TargetControlID="txtValid"
                                              Format="dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
                <br />
                <label for="Name">
                    Descriptions :
                </label>
                <asp:TextBox ID="txtDesc" TabIndex="50" runat="server" TextMode="MultiLine" Rows="3" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                            runat="server" 
                                            ControlToValidate="txtDesc"
                                            ErrorMessage="*" 
                                            ForeColor="Red" 
                                            ValidationGroup="Bonus" >
                </asp:RequiredFieldValidator>
            </fieldset>
            <asp:Panel ID="pnlOrdersUsed" runat="server" Visible="false">
             <fieldset id="Fieldset1">
             <legend>Used in orders</legend>
             <div align="center">
                <cc1:FZGrid ID="gridOrderUsed" 
                            runat="server" 
                            AllowPaging="True" 
                            AllowSorting="True"
                            AutoGenerateColumns="False" 
                            OnSorting="gridOrderUsed_Sorting" 
                            ViewStateMode="Enabled"
                            OnPageIndexChanging="gridOrderUsed_PageIndexChanging" 
                            SortOrder="Ascending" 
                            CustomVirtualItemCount="-1" PageSize="5" Width="300" HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:HyperLinkField HeaderText="Order" DataTextField="OrderID" DataNavigateUrlFields="OrderID"
                            DataNavigateUrlFormatString="/Secure/Order/OrderNew.aspx?ID={0}" Target="_blank" SortExpression="OrderID" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:g}" HtmlEncode="false" ItemStyle-HorizontalAlign="Center"/>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                </cc1:FZGrid>
                </div>
                </fieldset>
            </asp:Panel>
            <asp:Label ID="lblErrors" runat="server" ForeColor="Green"></asp:Label>
            <div align="center">
                <asp:Button ID="btn_save" Text="Save" runat="server" TabIndex="80" ValidationGroup="Bonus" OnClick="btn_save_Click" />
                <asp:Button ID="btn_reset" Text="Reset" runat="server" CausesValidation="False" TabIndex="90"
                    OnClick="btn_reset_Click" />
            </div>
        </asp:PlaceHolder>
        </div>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
