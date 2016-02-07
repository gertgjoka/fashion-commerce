<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryMenu.ascx.cs"
    Inherits="FashionZone.FE.CustomControl.CategoryMenu" %>
<asp:Menu runat="server" ID="menuCat" StaticDisplayLevels="2" Orientation="Vertical"
    RenderingMode="List" StaticSubMenuIndent="40" StaticMenuItemStyle-ItemSpacing="3" SkipLinkText="">
    <DataBindings>
        <asp:MenuItemBinding DataMember="Menu" TextField="text" ValueField="value" NavigateUrlField="url" />
    </DataBindings>
    <LevelMenuItemStyles>
        <asp:MenuItemStyle CssClass="mainBrandEleTit" ForeColor="White" />
    </LevelMenuItemStyles>
    <LevelSelectedStyles>
    <asp:MenuItemStyle Font-Underline="true" VerticalPadding="3"/>
    </LevelSelectedStyles>
</asp:Menu>
