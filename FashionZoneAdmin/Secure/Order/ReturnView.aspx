<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReturnView.aspx.cs"
    Inherits="FashionZone.Admin.Secure.Order.ReturnView" %>
<%@ Register TagPrefix="custom" Namespace="FashionZone.Admin.CustomControl" Assembly="FashionZone.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="box">
        <h3>
            Returns</h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin-bottom:10px;">
                    <table style="width: 90%; border: none;">
                        <tr style="border: none">
                            <td style="width: 30%; border: none">
                                <asp:Label ID="lblName" runat="server">Customer Name (or Surname)</asp:Label>
                            </td>
                            <td style="width: 50%; border: none">
                                <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="border: none">
                                <asp:Label ID="lblVerificationNumber" runat="server">Verification Number</asp:Label>
                            </td>
                            <td style="border: none">
                                <asp:TextBox ID="txtVerificationNumber" runat="server" Width="250px"></asp:TextBox>                                
                            </td>
                        </tr>
                         <tr>
                            <td style="border: none">
                                <asp:Label ID="lblOdrerId" runat="server">Order number</asp:Label>                                
                            </td>
                            <td style="border: none">
                                <asp:TextBox ID="txtOdrerId" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <span></span><asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
                <custom:FZGrid ID="gridReturn" 
                               runat="server" 
                               AllowPaging="True" 
                               AllowSorting="True" 
                               AutoGenerateColumns="False" 
                               OnSorting="gridReturn_Sorting" 
                               ViewStateMode="Enabled" 
                               OnPageIndexChanging="gridReturn_PageIndexChanging" 
                               CustomVirtualItemCount="-1" 
                               SortOrder="Ascending" 
                               HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                        <asp:BoundField DataField="OrderID" HeaderText="Order" SortExpression="OrderID" />
                        <asp:BoundField DataField="VerificationNumber" HeaderText="VerificationNumber" SortExpression="VerificationNumber" />
                        <asp:BoundField DataField="RequestDate" HeaderText="Request Date" SortExpression="RequestDate" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"  />                        
                        <asp:BoundField DataField="ReceivedDate" HeaderText="Received Date" SortExpression="ReceivedDate" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"  />
                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="ID,OrderID" DataNavigateUrlFormatString="ReturnNew.aspx?IDRETURN={0}&IDORDER={1}" />                       
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" Text="Delete" CommandArgument='<%# Eval("ID")%>' OnClick="lnkDelete_Click"/>
                                <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                                    ConfirmText='<%# "Are you sure about deleting return id = " + Eval("ID") + "?"%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" CssClass="pagerClass" />
                </custom:FZGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
