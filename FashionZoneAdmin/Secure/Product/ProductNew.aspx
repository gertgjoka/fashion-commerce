<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ProductNew.aspx.cs" Inherits="FashionZone.Admin.Secure.Product.ProductNew"
    ValidateRequest="false" ClientIDMode="AutoID" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>
<%@ Register Src="~/CustomControl/ProductImageUpload.ascx" TagPrefix="custom" TagName="productUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        /* HTML editor custom css to override values from the general one */
        .htmlEditor table {
            margin: 0px;
            border-collapse: separate;
            table-layout: auto;
            width: auto;
            line-height: 0px;
        }

        .htmlEditor img {
            margin-top: 0px;
            margin-left: 0px;
            margin-bottom: 0px;
            margin-right: 0px;
        }

        .htmlEditor td, th {
            border: 0px;
            padding: 0px;
        }

        .htmlEditor thead {
            background: none;
        }

        .htmlEditor textarea {
            padding: 0px;
        }

        .htmlEditor {
            width: 520px;
            margin-left: 105px;
        }

        div#form textarea {
            padding: 0px;
        }

        /* TreView custom css to override values from the general one */
        .tree table {
            margin: 0px;
            border-collapse: separate;
            table-layout: auto;
            width: auto;
            line-height: 0px;
        }

        .tree img {
            margin-top: 0px;
            margin-left: 0px;
            margin-bottom: 0px;
            margin-right: 0px;
        }

        .tree td, th {
            border: 0px;
            padding: 0px;
        }

        .tree thead {
            background: none;
        }

        .tree textarea {
            padding: 0px;
        }

        .tree {
            width: 520px;
            margin-left: 105px;
        }

        div#form textarea {
            padding: 0px;
        }
    </style>
    <script type="text/javascript" src="/js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="/js/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="/js/jquery.fileupload.js"></script>
    <script type="text/javascript" src="/js/jquery.iframe-transport.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="box">
        <h3 id="addProduct">Add Product
        </h3>
        <div id="form">
            <ajaxToolkit:TabContainer ID="tabProdContainer" runat="server" ActiveTabIndex="0">
                <ajaxToolkit:TabPanel runat="server" ID="tabInfo" HeaderText="Information" CssClass="TabContent">
                    <ContentTemplate>
                        <fieldset id="productInfo">
                            <legend>Product information</legend>
                            <label for="Name">
                                Name :</label>
                            <asp:TextBox ID="txtName" TabIndex="10" runat="server" Width="400"></asp:TextBox><asp:RequiredFieldValidator
                                ID="validName" runat="server" ControlToValidate="txtName" ErrorMessage="*" ForeColor="Red"
                                ValidationGroup="productValidation"></asp:RequiredFieldValidator><br />
                            <label for="Code">
                                Code :</label>
                            <asp:TextBox ID="txtCode" TabIndex="15" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCode" ErrorMessage="*"
                                ForeColor="Red" ValidationGroup="productValidation"></asp:RequiredFieldValidator><br />

                            <label for="OurPrice">
                                Price :</label>
                            <asp:TextBox ID="txtPrice" TabIndex="20" runat="server" MaxLength="7" ToolTip="Enter price es. 234,50"
                                Style="text-align: right;"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="txtPrice" ErrorMessage="*" ForeColor="Red"
                                    ValidationGroup="productValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPrice"
                                runat="server" ForeColor="Red" ErrorMessage="Not in correct format." ValidationExpression="^\d{1,5}\,\d{2}$"></asp:RegularExpressionValidator>
                            <br />
                            <label for="OriginalPrice">
                                Original price :</label>
                            <asp:TextBox ID="txtOriginalPrice" TabIndex="30" runat="server" MaxLength="7" ToolTip="Enter original price es. 234,50"
                                Style="text-align: right;"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server" ControlToValidate="txtOriginalPrice" ErrorMessage="*" ForeColor="Red"
                                    ValidationGroup="productValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtOriginalPrice"
                                runat="server" ForeColor="Red" ErrorMessage="Not in correct format." ValidationExpression="^\d{1,5}\,\d{2}$"></asp:RegularExpressionValidator>
                            <br />
                            <label for="SupplierPrice">
                                Supplier price :</label>
                            <asp:TextBox ID="txtSupplierPrice" TabIndex="30" runat="server" MaxLength="7" ToolTip="Enter supplier price es. 234,50"
                                Style="text-align: right;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                runat="server" ControlToValidate="txtSupplierPrice" ErrorMessage="*" ForeColor="Red"
                                ValidationGroup="productValidation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtSupplierPrice"
                                runat="server" ForeColor="Red" ErrorMessage="Not in correct format." ValidationExpression="^\d{1,5}\,\d{2}$"></asp:RegularExpressionValidator>
                            <br />
                            <asp:UpdatePanel ID="updDiscountApprover" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <label for="Discount">
                                        Discount :</label>
                                    <asp:TextBox ID="txtDiscount" runat="server" MaxLength="7" Style="text-align: right;"></asp:TextBox>
                                    <br />
                                    <label for="approved">
                                        Approved :
                                    </label>
                                    <asp:CheckBox ID="chkApproved" runat="server" TabIndex="37" CssClass="customCheck"
                                        ViewStateMode="Enabled"></asp:CheckBox>
                                    <br />
                                    <label for="approver">
                                        Approver :
                                    </label>
                                    <asp:TextBox ID="txtApprover" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <label for="Description">
                                Description :</label>
                            <div class="htmlEditor">
                                <HTMLEditor:Editor runat="server" ID="txtDescription" Height="460px" NoScript="True"
                                    TabIndex="40" />
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" ID="tabCategories" HeaderText="Categories" CssClass="TabContent">
                    <ContentTemplate>
                        <asp:UpdatePanel runat="server" ID="updPanelBrandCampaign" ViewStateMode="Enabled"
                            UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="chooseBrandCampPnl" runat="server" ViewStateMode="Enabled">
                                    <label for="Brand">
                                        Brand :
                                    </label>
                                    <asp:DropDownList ID="ddlBrand" runat="server" />
                                    <ajaxToolkit:CascadingDropDown ID="cddlBrand" runat="server" LoadingText="Loading brands..."
                                        TargetControlID="ddlBrand" Category="BRAND" PromptText="Choose a brand" ServiceMethod="GetBrand"
                                        ServicePath="~/Services/PopulateCddlCat.asmx" ViewStateMode="Enabled" />
                                    <br />
                                    <label for="Campaign">
                                        Campain :
                                    </label>
                                    <asp:DropDownList ID="ddlCampaign" runat="server" OnSelectedIndexChanged="ddlCampain_SelectedIndexChanged"
                                        AutoPostBack="true" />
                                    <ajaxToolkit:CascadingDropDown ID="cddlCampain" runat="server" LoadingText="Loading campaign..."
                                        TargetControlID="ddlCampaign" ParentControlID="ddlBrand" Category="CAMPAIN" PromptText="Choose a campaign"
                                        ServiceMethod="GetCampain" ServicePath="~/Services/PopulateCddlCat.asmx" />
                                    <br />
                                    <br />
                                </asp:Panel>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red">Choose at least one category (ex. Woman) and one sub-category (ex.Jeans)</asp:Label>
                                <label for="Categories" runat="server" id="lblCat" visible="false">
                                    Categories :
                                </label>
                                <div class="tree">
                                    <asp:TreeView ID="tvCategories" runat="server" Visible="false">
                                    </asp:TreeView>
                                </div>
                                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCampaign" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" ID="tabImages" HeaderText="Images" CssClass="TabContent">
                    <ContentTemplate>
                        <asp:Repeater ID="repeater" runat="server" ViewStateMode="Enabled">
                            <ItemTemplate>
                                <fieldset id='<%#"Image" + Eval("ID") %>'>
                                    <legend>Image</legend>
                                    <custom:productUpload runat="server" ImgID='<%#Eval("ID") %>' ProdID='<%#Eval("ProductID") %>'
                                        Image='<%#Eval("Image") %>' Thumbnail='<%#Eval("Thumbnail") %>' LargeImage='<%#Eval("LargeImage") %>' OldImg='<%#Eval("LargeImage") %>'
                                        UsedBy='<%#"Image" + Eval("ID") %>' Principal='<%#Eval("Principal") %>' ClientIDMode="AutoID"
                                        OnImageDelete="imageDelete" />
                                </fieldset>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:LoginView ID="lgnViewAddImage" runat="server">
                            <RoleGroups>
                                <asp:RoleGroup Roles="Moderator, Administrator">
                                    <ContentTemplate>
                                        <asp:UpdatePanel runat="server" ID="updPnlLnkAdd" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lnkAddImage" runat="server" OnClick="lnkAddImage_Click" Text="Add image"
                                                    CausesValidation="false" ClientIDMode="AutoID"></asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkAddImage" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:RoleGroup>
                            </RoleGroups>
                        </asp:LoginView>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel runat="server" ID="tabAttributes" HeaderText="Attributes" CssClass="TabContent">
                    <ContentTemplate>
                        <asp:UpdatePanel runat="server" ID="updPanelAttributes" UpdateMode="Conditional"
                            ChildrenAsTriggers="False" ViewStateMode="Enabled">
                            <ContentTemplate>
                                <label for="Attribute">
                                    Attribute :
                                </label>
                                <asp:DropDownList ID="ddlAttributes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAttributes_SelectedIndexChanged"
                                    DataTextField="Name" DataValueField="ID" ViewStateMode="Enabled">
                                </asp:DropDownList>
                                <div align="center">
                                    <asp:ListView ID="lvAttributeValues" runat="server" DataKeyNames="Version" ViewStateMode="Enabled">
                                        <LayoutTemplate>
                                            <table border="0" cellpadding="1" style="width: 400px;">
                                                <tr style="background-color: #E5E5FE; height: 20px; vertical-align: middle; border: 1px;">
                                                    <th align="center">Value
                                                    </th>
                                                    <th align="center">Quantity
                                                    </th>
                                                    <th align="center">Availability
                                                    </th>
                                                    <th></th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" style="height: 20px; vertical-align: middle;">
                                                </tr>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="prodAttrID" runat="server" Value='<%#Eval("ID") %>' />
                                            <asp:HiddenField ID="attrValID" runat="server" Value='<%#Eval("AttributeValueID") %>' />
                                            <tr style="height: 20px; vertical-align: middle;">
                                                <td align="center">
                                                    <asp:Label runat="server" ID="lblValue"><%#Eval("Value") %></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label runat="server" ID="lblQuantity"><%#Eval("Quantity") %></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:TextBox ID="txtAvailability" runat="server" Text='<%#Eval("Availability") %>'
                                                        Width="100px">Availability</asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                        ForeColor="Red" ControlToValidate="txtAvailability" ValidationGroup="productValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtAvailability" ErrorMessage="0 to 99999" ValidationExpression="^\d{1,5}$"
                                                        ValidationGroup="productValidation"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlAttributes" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
            <br />
            <asp:UpdatePanel ID="updPnl" runat="server">
                <ContentTemplate>
                    <input type="hidden" id="ID" runat="server" value="0" />
                    <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>
                    <asp:LoginView ID="lgnViewSave" runat="server">
                        <RoleGroups>
                            <asp:RoleGroup Roles="Moderator, Administrator">
                                <ContentTemplate>
                                    <div align="center">
                                        <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" ValidationGroup="productValidation"
                                            Width="59px" TabIndex="110" />
                                        <asp:Button ID="btnReset" Text="Reset" runat="server" CausesValidation="False" OnClick="btnReset_Click"
                                            TabIndex="120" />
                                    </div>
                                </ContentTemplate>
                            </asp:RoleGroup>
                        </RoleGroups>
                    </asp:LoginView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
