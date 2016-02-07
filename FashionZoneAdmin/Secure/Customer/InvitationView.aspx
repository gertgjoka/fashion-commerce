<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvitationView.aspx.cs" Inherits="FashionZone.Admin.Secure.Customer.InvitationView" %>
<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl" TagPrefix="bonus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="box">
    <h3>Invitation</h3>

    <asp:PlaceHolder ID="plhView" runat="server">
        <div style="margin-bottom:10px;">
            <asp:Label ID="lblEmail" runat="server">Email</asp:Label>
            <span></span>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <span></span>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>
        <bonus:FZGrid ID="gridInvitation" 
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
                <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="InvitationNew.aspx?ID={0}" />
                <%--<asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" 
                                        runat="server" 
                                        CausesValidation="False" 
                                        Text="Delete" 
                                        CommandArgument='<%# Eval("ID")%>' 
                                        OnClick="lnkDelete_Click"
                                        />
                        <ajaxToolkit:ConfirmButtonExtender 
                            ID="btnConfirm" 
                            runat="server" 
                            TargetControlID="lnkDelete"
                            ConfirmText='<%# "Are you sure about deleting invitation " + Eval("InvitedMail") + "?"%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="CustomerFullName" HeaderText="Custumer" SortExpression="CustomerFullName" />
                <asp:BoundField DataField="CustomerID" HeaderText="Customer ID" SortExpression="CustomerID" />
                <asp:BoundField DataField="InvitedMail" HeaderText="Invited Mail" SortExpression="InvitedMail" />
                <asp:BoundField DataField="RegistrationDate" HeaderText="Registration Date" SortExpression="RegistrationDate" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"/>
                <asp:CheckBoxField DataField="Registered" HeaderText="Registered" SortExpression="Registered" />
            </Columns>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
        </bonus:FZGrid>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhEdit" runat="server" Visible="false">
        <div id="form">
            <fieldset id="Customer">
                <legend>Invitation</legend>
            <label for="Name">
                Customer Id : 
            </label>
            <asp:Label ID="lblCustomerID" runat="server" ></asp:Label>
            <br />
            <label for="Name">
                Invited Mail : 
            </label>
            <asp:Label ID="lblInvitedMail" runat="server"  ></asp:Label>
            <br />
            <label for="Name">
                Registered : 
            </label>
            <asp:Label ID="lblRegistered" runat="server" ></asp:Label>
            <br />
            <label for="Name">
                Regist. Date : 
            </label>
            <asp:Label ID="lblRegistrationDate" runat="server" ></asp:Label>
            <br />
                
            </fieldset>
            <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
            <div align="center">
                <asp:Button ID="btn_save" Text="Save" runat="server" TabIndex="80" ValidationGroup="Bonus" />
                <asp:Button ID="btn_reset" Text="Reset" runat="server" CausesValidation="False" TabIndex="90" />
            </div>
        </div>
    </asp:PlaceHolder>
</div>
</asp:Content>
