<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Attributes.aspx.cs" Inherits="FashionZone.Admin.Secure.Product.Attributes"%>

<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ViewStateMode="Enabled">
    <div id="box">
        <h3>
            Attributes</h3>
        <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <div style="margin-bottom: 10px">
                    <asp:Label ID="lblName" runat="server">Name</asp:Label>
                    <span></span>
                    <asp:TextBox ID="txtNameSearch" runat="server" TabIndex="10"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" TabIndex="20" OnClick="btnSearch_Click" CausesValidation="false" />
                </div>
                <custom:FZGrid ID="gridAttributes" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridAttributes_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridAttributes_PageIndexChanging" CustomVirtualItemCount="-1"
                    CurrentPageIndex="0" SortOrder="Ascending" HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" Text="Select"
                                    CommandArgument='<%# Eval("ID")%>' OnClick="lnkSelect_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" Text="Delete"
                                    CommandArgument='<%# Eval("ID")%>' OnClick="lnkDelete_Click" />
                                <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                                    ConfirmText='<%# "Are you sure about deleting attribute " + Eval("Name") + "?"%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                    <SelectedRowStyle BackColor="#CCFFCC" />
                </custom:FZGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ViewStateMode="Enabled">
            <ContentTemplate>
                <div align="center">
                    <asp:Button ID="btnAddAttribute" runat="server" CausesValidation="false" Text="New attribute"
                        OnClick="btnAddAttribute_Click" />
                </div>
                <div id="form">
                    <div id="divDetail" runat="server" visible="false">
                        <fieldset id="personal">
                            <asp:HiddenField ID="attrID" runat="server" />
                            <legend>Attribute Info</legend>
                            <label for="Name">
                                Name :
                            </label>
                            <asp:TextBox ID="txtName" TabIndex="30" runat="server" />
                            <asp:RequiredFieldValidator ID="validName" runat="server" ControlToValidate="txtName"
                                ErrorMessage="*" ForeColor="Red" ValidationGroup="attrValidation"></asp:RequiredFieldValidator>
                            <br />
                            <label for="description">
                                Description :
                            </label>
                            <asp:TextBox ID="txtDesc" TabIndex="35" runat="server" TextMode="MultiLine" Rows="3"
                                Height="80" />
                            <asp:RequiredFieldValidator ID="validDescription" runat="server" ControlToValidate="txtDesc"
                                ErrorMessage="*" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                            <br />
                            <div style="border: 1px solid #C3D7DB; margin: 10px;">
                                <h4 style="color: #294145;">
                                    Attribute values
                                </h4>
                                <asp:CheckBoxList ID="chkValueList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                    DataTextField="Value" DataValueField="ID">
                                </asp:CheckBoxList>
                                <div runat="server" id="divValueEdit" visible="false" align="center">
                                    <asp:HiddenField ID="valID" runat="server" />
                                    <br />
                                    Value :
                                    <asp:TextBox ID="txtValue" TabIndex="40" runat="server" />
                                    <asp:RequiredFieldValidator ID="validValue" runat="server" ControlToValidate="txtValue"
                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="valueValidation"></asp:RequiredFieldValidator>
                                    Order :
                                    <asp:TextBox ID="txtOrder" TabIndex="45" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOrder"
                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="valueValidation"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtOrder"
                                        ErrorMessage="*"  ValidationExpression="^\d{1,5}$" ForeColor="Red"  ValidationGroup="valueValidation"></asp:RegularExpressionValidator>
                                    <asp:Button ID="btnSaveValue" runat="server" ValidationGroup="valueValidation" Text="Save"
                                        OnClick="btnSaveValue_Click" />
                                    <br />
                                    <br />
                                </div>
                                <asp:Label ID="lblResultValue" runat="server" ForeColor="Red"></asp:Label>
                                <div align="center">
                                    <asp:Button ID="btnNewValue" runat="server" CausesValidation="false" Text="Add" OnClick="btnNewValue_Click" />
                                    <asp:Button ID="btnEditValue" runat="server" CausesValidation="false" Text="Edit"
                                        OnClick="btnEditValue_Click" />
                                    <asp:Button ID="btnDelValues" runat="server" CausesValidation="false" Text="Delete"
                                        OnClick="btnDelValues_Click" />
                                         <ajaxToolkit:ConfirmButtonExtender ID="confirmDelValue" runat="server" TargetControlID="btnDelValues"
                                    ConfirmText="Are you sure about deleting value(s)?" />
                                </div>
                            </div>
                        </fieldset>
                        <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                        <div align="center">
                            <asp:Button ID="button1" Text="Save" runat="server" OnClick="btnSave_Click" ValidationGroup="attrValidation"
                                Width="59px" TabIndex="80" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
