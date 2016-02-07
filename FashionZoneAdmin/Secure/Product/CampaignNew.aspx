<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CampaignNew.aspx.cs" Inherits="FashionZone.Admin.Secure.Product.CampaignNew" %>

<%@ Register Assembly="FashionZone.Admin" Namespace="FashionZone.Admin.CustomControl"
    TagPrefix="custom" %>
<%@ Register Src="~/CustomControl/FZFileUpload.ascx" TagName="FZFileUpload" TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/js/jquery-1.8.1.min.js"></script>
    <script type="text/javascript" src="/js/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="/js/jquery.fileupload.js"></script>
    <script type="text/javascript" src="/js/jquery.iframe-transport.js"></script>
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
            <input type="hidden" id="brandId" runat="server" />
            <input type="hidden" id="ID" runat="server" />
            <div id="box">
                <h3 id="adduser">
                    Add Campaign</h3>
                <div id="form">
                    <fieldset id="personal">
                        <legend>Campaign Information</legend>
                        <label for="Name">
                            Name :
                        </label>
                        <asp:TextBox ID="txtName" TabIndex="10" runat="server" />
                        <asp:RequiredFieldValidator ID="validName" runat="server" ControlToValidate="txtName"
                            ErrorMessage="*" ForeColor="Red" ValidationGroup="campaignValidate"></asp:RequiredFieldValidator>
                        <br />
                        <label for="Brand">
                            Brand :
                        </label>
                        <asp:TextBox ID="txtBrand" runat="server" />
                        <asp:Button ID="btnSelectBrand" runat="server" Height="23px" Text="Select" Width="63px"
                            CausesValidation="false" OnClick="btnSelectBrand_Click" TabIndex="20" ViewStateMode="Enabled" />
                        <asp:RequiredFieldValidator ID="validBrand" runat="server" ControlToValidate="txtBrand"
                            ErrorMessage="*" ForeColor="Red" ValidationGroup="campaignValidate"></asp:RequiredFieldValidator>
                        <br />
                        <label for="approved">
                            Approved :
                        </label>
                        <asp:CheckBox ID="chkApproved" runat="server" TabIndex="25" CssClass="customCheck"
                            ViewStateMode="Enabled"></asp:CheckBox>
                        <br />
                        <label for="approver">
                            Approver :
                        </label>
                        <asp:TextBox ID="txtApprover" TabIndex="270" runat="server" />
                        <br />
                        <label for="encrypted">
                            Encrypted :
                        </label>
                        <asp:TextBox ID="txtEncryptedId" TabIndex="270" runat="server" />
                        <br />
                        <label for="description">
                            Description :
                        </label>
                        <asp:TextBox ID="txtDesc" TabIndex="30" runat="server" TextMode="MultiLine" Rows="6" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesc"
                            ErrorMessage="*" ForeColor="Red" ValidationGroup="campaignValidate"> </asp:RequiredFieldValidator>
                        <br />
                        <label for="dateFrom">
                            Active from :
                        </label>
                        <asp:TextBox ID="txtDateFrom" TabIndex="35" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                            ControlToValidate="txtDateFrom" ForeColor="Red" ValidationGroup="campaignValidate"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="calFromExtender" runat="server" TargetControlID="txtDateFrom"
                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        hh:mm
                        <asp:TextBox ID="txtTimeFrom" TabIndex="40" runat="server" Width="46px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                            ControlToValidate="txtTimeFrom" ForeColor="Red" ValidationGroup="campaignValidate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Not a valid time"
                            ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ForeColor="Red" ControlToValidate="txtTimeFrom"
                            ValidationGroup="campaignValidate"></asp:RegularExpressionValidator>
                        <br />
                        <label for="dateTo">
                            Active to :
                        </label>
                        <asp:TextBox ID="txtDateTo" TabIndex="50" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                            ControlToValidate="txtDateTo" ForeColor="Red" ValidationGroup="campaignValidate"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="calToExtender" runat="server" TargetControlID="txtDateTo"
                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        hh:mm
                        <asp:TextBox ID="txtTimeTo" TabIndex="55" runat="server" Width="46px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                            ControlToValidate="txtTimeTo" ForeColor="Red" ValidationGroup="campaignValidate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Not a valid time"
                            ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ForeColor="Red" ControlToValidate="txtTimeTo"
                            ValidationGroup="campaignValidate"></asp:RegularExpressionValidator>
                        <br />
                        <label for="deliveryFrom">
                            Delivery from :
                        </label>
                        <asp:TextBox ID="txtDeliveryStart" TabIndex="60" runat="server" />
                        <ajaxToolkit:CalendarExtender ID="calDelStartExtender" runat="server" TargetControlID="txtDeliveryStart"
                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <br />
                        <label for="deliveryTo">
                            Delivery to :
                        </label>
                        <asp:TextBox ID="txtDeliveryEnd" TabIndex="70" runat="server" />
                        <ajaxToolkit:CalendarExtender ID="calDelEndExtender" runat="server" TargetControlID="txtDeliveryEnd"
                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                    </fieldset>
                    <fieldset id="Fieldset1">
                        <legend>Upload Area</legend>
                        <label for="Logo">
                            Logo :
                            <br />
                            (230 x 100)
                        </label>
                        <custom:FZFileUpload UsedBy="Logo" ID="uplLogo" runat="server" FileExtensions="(jpg)|(png)|(jpeg)" />
                        <br />
                        <label for="imgHome">
                            Image home :
                            <br />
                            (504 x 160)
                        </label>
                        <custom:FZFileUpload ID="uplHome" UsedBy="Home" runat="server" FileExtensions="(jpg)|(png)|(jpeg)" />
                        <br />
                        <label for="imgDetail">
                            Image detail :
                            <br />
                            (1024 x ...)
                        </label>
                        <custom:FZFileUpload ID="uplImgDet" UsedBy="ImgDet" runat="server" FileExtensions="(jpg)|(png)|(jpeg)" />
                        <br />
                        <label for="imgListHeader">
                            Image menu :
                            <br />
                            (230 x ...)
                        </label>
                        <custom:FZFileUpload ID="uplImgHeader" UsedBy="ImgHeader" runat="server" FileExtensions="(jpg)|(png)|(jpeg)" />
                        <br />
                        <label for="genericFile">
                            Generic file(pdf) :
                        </label>
                        <custom:FZFileUpload ID="uplGenericFile" UsedBy="GenericFile" runat="server" FileExtensions="(pdf)" />
                        <br />
                    </fieldset>
                    <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                    <div align="center">
                        <asp:LoginView ID="lgnViewSave" runat="server">
                            <RoleGroups>
                                <asp:RoleGroup Roles="Moderator, Administrator">
                                    <ContentTemplate>
                                        <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" ValidationGroup="campaignValidate" />
                                        <asp:Button ID="btnReset" Text="Reset" runat="server" CausesValidation="False" OnClick="btnReset_Click" />
                                    </ContentTemplate>
                                </asp:RoleGroup>
                            </RoleGroups>
                        </asp:LoginView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="fake"
        CancelControlID="btnClose" Drag="false" PopupControlID="Dialog_ChargeFilter"
        Enabled="True" BackgroundCssClass="c" />
    <asp:HiddenField ID="fake" runat="server" />
    <asp:Panel ID="Dialog_ChargeFilter" CssClass="modalPopup" runat="server" Width="700">
        <asp:Panel ID="DialogHeaderFrame" CssClass="DialogHeaderFrame" runat="server" Width="700px">
            <asp:Panel ID="DialogHeader" runat="server" CssClass="DialogHeader">
                &nbsp;<asp:Label ID="LblPopupHeader" runat="server" Text="Search Brand" />
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
                <custom:FZGrid ID="gridBrands" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" OnSorting="gridBrands_Sorting" ViewStateMode="Enabled"
                    OnPageIndexChanging="gridBrands_PageIndexChanging" SortOrder="Ascending" CustomVirtualItemCount="-1"
                    HeaderStyle-CssClass="GridHeader">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" Text="Select"
                                    CommandArgument='<%# Eval("ID")%>' OnClick="lnkSelect_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ShowName" HeaderText="ShowName" SortExpression="ShowName" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact" />
                        <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="pagerClass" />
                </custom:FZGrid>
                <br />
                <div align="center">
                    <asp:Button ClientIDMode="Static" ID="btnClose" Text="Close" ToolTip="close filter-dialog"
                        CausesValidation="false" Width="70px" runat="server" OnClick="btnClose_Click" /><br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
