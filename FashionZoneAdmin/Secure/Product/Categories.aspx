<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="FashionZone.Admin.Secure.Product.Categories" ViewStateMode="Enabled"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        /* TreView custom css to override values from the general one */
        .tree table
        {
            margin: 0px;
            border-collapse: separate;
            table-layout: auto;
            width: auto;
            line-height: 0px;
        }
        
        .tree img
        {
            margin-top: 0px;
            margin-left: 0px;
            margin-bottom: 0px;
            margin-right: 0px;
        }
        
        .tree td, th
        {
            border: 0px;
            padding: 0px;
        }
        .tree thead
        {
            background: none;
        }
        
        .tree textarea
        {
            padding: 0px;
        }
        
        .tree
        {
            width: 520px;
            margin-left: 105px;
        }
        div#form textarea
        {
            padding: 0px;
        }
        div#form textarea 
        {
            height: 120px;
            overflow: auto;
            padding: 5px;
            width: 410px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ViewStateMode="Enabled">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<input id="hdnIdNode" type="hidden" runat="server" />
<div id="box">
    <h3 id="addCategory">Categories</h3>
    <div id="form">  
         
        <fieldset id="chooser">
            <legend>Categories Associations</legend>
                There should be one/some macro category (Woman, Man, Child) and one/some subcategory (Jeans, T-Shirt, etc)
            <br />
            <br />
                    <label for="Name">
                        Brands :
                    </label>                   
                    <asp:DropDownList ID="ddl_brand" runat="server" />
                    <ajaxToolkit:CascadingDropDown ID="cddl_brand" 
                                 runat="server"
                                 LoadingText="Po karikoj Brand..."
                                 TargetControlID="ddl_brand" 
                                 Category="BRAND"
                                 PromptText="Zgjidh nje Brand"
                                 ServiceMethod="GetBrand"
                                 ServicePath="~/Services/PopulateCddlCat.asmx"/>
                    <br /><br />
                    <label for="Name">
                        Campaign :
                    </label>
                    <asp:DropDownList ID="dll_campain" runat="server" 
                        OnSelectedIndexChanged="dll_campain_SelectedIndexChanged" 
                        AutoPostBack="true" />
                    <ajaxToolkit:CascadingDropDown ID="sddl_campain" 
                                 runat="server"
                                 LoadingText="Po karikoj Campagnen..."
                                 TargetControlID="dll_campain" 
                                 ParentControlID="ddl_brand"
                                 Category="CAMPAIN"
                                 PromptText="Zgjidh nje Campain"
                                 ServiceMethod="GetCampain"
                                 ServicePath="~/Services/PopulateCddlCat.asmx" />
                    <br /><br />

                    <div class="tree">
                        <asp:TreeView ID="TreeView1" 
                              runat="server" 
                              onselectednodechanged="TreeView1_SelectedNodeChanged"
                              Visible="false" EnableViewState="true">
                        </asp:TreeView>
                    </div>
                    <br />
                    <label for="Name">&nbsp;</label>
                    <asp:Label ID="lblAddError" runat="server" ForeColor="Red" Visible="false" ></asp:Label>
                    <br />
                    <asp:Button ID="btnAddNewCamp" 
                                runat="server" 
                                Text="Add Categories" 
                                onclick="btnAddNewCamp_Click" />
                    <asp:Button id="btn_del" 
                                Text="Delete Categories" 
                                runat="server" 
                                TabIndex="80" 
                                onclick="btn_del_Click"/> 
                    
    </fieldset>

<asp:Panel ID="pnlInfo" runat="server" Visible="false">
    <fieldset id="personal">
                <legend>Categories Information</legend>
                    <label for="Name">
                        Name :
                    </label> 
                    <asp:TextBox id="txtName" tabindex="10" runat="server" />
                    <asp:RequiredFieldValidator ID="validName" 
                                                runat="server" 
                                                ControlToValidate="txtName" 
                                                ErrorMessage="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    <br />
                    <label for="NameEng">
                        Name Eng :
                    </label> 
                    <asp:TextBox id="txtNameEng" tabindex="10" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                                runat="server" 
                                                ControlToValidate="txtNameEng" 
                                                ErrorMessage="*" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    <br />
                    <label for="Description">
                        Description :
                    </label> 
                    <asp:TextBox id="txtDesc" tabindex="20" runat="server" TextMode="MultiLine" Rows="3"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                                runat="server" 
                                                ControlToValidate="txtDesc" 
                                                ErrorMessage="*" 
                                                ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    <br />
                    <label for="Ordering">
                        Ordering :
                    </label>    
                    <asp:TextBox id="txtOrder" tabindex="30" runat="server" />  
                    <br />
                    <label for="Attributes">
                        Attributes :
                    </label>                     
                    <asp:DropDownList ID="ddl_Attributes" runat="server">
                    </asp:DropDownList>
                    <br />  
        </fieldset>
</asp:Panel>    
    <asp:Label ID="lblErrors" runat="server" ForeColor="Red"></asp:Label>

    <div align="center">
        <asp:Button id="btn_save" Text="Save" runat="server" 
                onclick="btnSave_Click" TabIndex="80"/>     
        <asp:Button id="btn_reset" Text="Reset" runat="server" CausesValidation="False" 
                onclick="btnReset_Click" TabIndex="90"/>
    </div>
    </div>
</div>
    </ContentTemplate>        
</asp:UpdatePanel>
</asp:Content>