<%@ Page Title="FZone" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="product.aspx.cs" Inherits="FashionZone.FE.Secure.product" ViewStateEncryptionMode="Always"
    EnableViewStateMac="true" EnableEventValidation="true" %>

<%@ Register Assembly="FashionZone.FE" Namespace="FashionZone.FE.CustomControl" TagPrefix="custom" %>
<%@ Register Src="~/CustomControl/BreadCrumb.ascx" TagPrefix="custom" TagName="breadcrumb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/css/lightbox.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.1.min.js"></script>
    <script type="text/javascript" src="/js/lightbox.js"></script>
    <script type="text/javascript">
        function switchImage(url, url2, imageId) {

            // activate lightbox for previous image
            $('#image' + $('#<%=hdnPreviousImage.ClientID%>').attr("value")).attr("rel", "lightbox[Image]");
            // set new image id
            $('#<%=hdnPreviousImage.ClientID%>').attr("value", imageId);

            // deactivate light box in the small carousel
            $('#image' + imageId).attr("rel", "");

            // set new image link for lightbox in the big carousel
            $('#<%=lnkImage.ClientID%>').attr('href', url2);
            // change visual image
            $('#<%=imgProdBig.ClientID%>').attr('src', url);


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <custom:breadcrumb runat="server" id="breadcrumb"></custom:breadcrumb>
    <div class="clearer">
    </div>
    <div class="Carosel">
        <asp:HiddenField runat="server" ID="hdnPreviousImage" />
        <div class="CaroselBig">
            <a href="#" runat="server" id="lnkImage" rel="lightbox[Image]">
                <asp:Image runat="server" ID="imgProdBig" /></a>
        </div>
        <asp:Repeater runat="server" ID="rptImages">
            <ItemTemplate>
                <div class="CaroselSmall">
                    <a href="javascript:;" onclick="switchImage('<%# FashionZone.BL.Configuration.ImagesUploadPath + Eval("Image") %>', 
                    '<%# FashionZone.BL.Configuration.ImagesUploadPath + Eval("LargeImage") %>', '<%# Eval("ID") %>')">
                        <img src='<%#FashionZone.BL.Configuration.ImagesUploadPath + Eval("Thumbnail") %>'
                            id="btnSmallmage" alt="" /></a> <a href='<%# FashionZone.BL.Configuration.ImagesUploadPath + Eval("LargeImage") %>'
                                rel='<%# !(bool) Eval("Principal") ? "lightbox[Image]" : "" %>' id='<%# "image" + Eval("ID") %>'></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <!--end carosel-->
    <div class="DxDetail">
        <div class="small">
            <asp:Image runat="server" ID="imgBrandLogo" />
        </div>
        <img src="/image/linegrey.jpg" alt="line" />
        <ul class="Info">
            <li>
                <h2>
                    <asp:Literal runat="server" ID="litProdName"></asp:Literal>
                </h2>
            </li>
        </ul>
        <img src="/image/linegrey.jpg" alt="line" />
        <ul class="Info">
            <li>
                <asp:Literal runat="server" ID="litNotAvailable" Text="<%$Resources:Lang, ProductNotAvailableLiteral %>"
                    Visible="false"></asp:Literal>
            </li>
        </ul>
        <div runat="server" id="divWhenAvailable" visible="true">
            <ul class="Price">
                <li class="PriceRed">€
                    <asp:Literal runat="server" ID="litPrice"></asp:Literal></li>
                <li class="PriceGrey">€
                    <asp:Literal runat="server" ID="litOriginalPrice"></asp:Literal></li>
            </ul>
            <img src="/image/linegrey.jpg" alt="line" />
            <ul class="Info">
                <li>
                    <asp:Label runat="server" ID="lblEstimatedDelivery" Text="<%$Resources:Lang, EstimatedDeliveryLabel%>"></asp:Label>
                    <asp:Literal runat="server" ID="litEstimatedDates"></asp:Literal>
                    <asp:Label runat="server" ID="lblInfo" ForeColor="#C33000" Visible="false"></asp:Label>
                </li>
            </ul>
            <img src="/image/linegrey.jpg" alt="line" />
            <asp:UpdatePanel runat="server" ID="updPanelDDL" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="DropDownDetail">
                        <asp:Label runat="server" ID="lblSize" Text="<%$Resources:Lang, SizeLabel%>"></asp:Label>
                        <custom:fzdropdownlist runat="server" id="ddlSize" viewstatemode="Enabled" autopostback="true"
                            onselectedindexchanged="ddlSize_SelectedIndexChanged" datatextfield="Value" datavaluefield="Id">
                        </custom:fzdropdownlist>
                        <asp:Label runat="server" ID="lblQuantity" Text="<%$Resources:Lang, QuantityLabel%>"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlQuantity" Enabled="false" ViewStateMode="Enabled">
                        </asp:DropDownList>
                    </div>
                    <img src="/image/linegrey.jpg" alt="line" runat="server" />
                    <div class="RedButton" runat="server">
                        <asp:LinkButton runat="server" ID="lnkAddToBasket" Text="<%$Resources:Lang, AddToBasketLink%>"
                            OnClick="lnkAddToBasket_Click" Enabled="false"></asp:LinkButton>
                    </div>
                    <br />
                    <asp:Label runat="server" ID="lblMessage"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%-- <img src="/image/linegrey.jpg" alt="line" />
        <div class="Social">
            <ul>
                <li><a href="#">
                    <img alt="I like it" src="/image/mipiace.png" /></a></li>
                <li><a href="#">
                    <img alt="Facebook" src="/image/fb.png" /></a></li>
                <li><a href="#">
                    <img alt="Twitter" src="/image/tw.png" /></a></li>
                <li><a href="#">
                    <img alt="Share" src="/image/sent.png" /></a></li>
            </ul>
        </div>--%>
    </div>
    <div class="clearer">
    </div>
    <div class="DescriptionPink">
        <img src="/image/butterflySmall.png" alt="Butterfly FZone" class="butterflySmall" />
        <div class="clearer">
        </div>
        <div class="DescriptionPinkTit">
            <asp:Label runat="server" ID="lblDescription" Text="<%$Resources:Lang, DescriptionLabel%>"></asp:Label>
        </div>
        <ul class="InfoPink">
            <li>
                <h3>
                    <asp:Literal runat="server" ID="litProdName2"></asp:Literal>
                    (Ref.
                <asp:Literal runat="server" ID="litProdCode"></asp:Literal>)</h3>
                <br />
                <asp:Literal runat="server" ID="litDescription"></asp:Literal>
            </li>
        </ul>
    </div>
    <!--End description-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FootContent" runat="server">
</asp:Content>
