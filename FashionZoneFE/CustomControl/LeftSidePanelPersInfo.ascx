<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftSidePanelPersInfo.ascx.cs"
    Inherits="FashionZone.FE.CustomControl.LeftSidePanelPersInfo" %>
<div class="MyAccSx">
    <div class="MenuMyAccount">
        <ul>
            <li><a href="/personal/order/">
                <asp:Label runat="server" ID="lblOrder" Text="<%$Resources:Lang, OrderLabel%>"></asp:Label>
            </a></li>
            <li><a href="/personal/info/">
                <asp:Label runat="server" ID="lblPersonalData" Text="<%$Resources:Lang, PersonalDataLabel%>"></asp:Label></a></li>
            <li><a href="/personal/bonus/">
                <asp:Label runat="server" ID="lblBonus" Text="<%$Resources:Lang, BonusLabel%>"></asp:Label></a></li>
            <li><a href="/personal/friends/">
                <asp:Label runat="server" ID="lblFriends" Text="<%$Resources:Lang, FriendsLabel%>"></asp:Label></a></li>
            <li><a href="/personal/notification/">
                <asp:Label runat="server" ID="lblSalesReminder" Text="<%$Resources:Lang, SalesReminderLabel%>"></asp:Label></a></li>
            <%--<li><a href="returns.aspx">
                <asp:Label runat="server" ID="lblReturns" Text="<%$Resources:Lang, ReturnsAndRefundsLabel%>"></asp:Label></a></li>--%>
        </ul>
    </div>
    <!--end menu my account-->
    <div class="boxGrey">
        <img src="/image/visa.png" alt="visa img" />
        <h3>
            <asp:Label runat="server" ID="lblSecureTransactions" Text="<%$Resources:Lang, SecureTransactionsLabel%>"></asp:Label>
        </h3>
        <asp:Label runat="server" ID="lblSecureTransactionsText" Text="<%$Resources:Lang, SecureTransactionsTextLabel%>"></asp:Label>
        <div class="clearer">
        </div>
        <a href="#">
            <img src="/image/arrow.png" alt="arrow img" /></a>
    </div>
   <%-- <div class="boxGrey">
        <img src="/image/cassa.png" alt="pay img" />
        <h3>
            Testo finto</h3>
        lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum
        lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum
        <div class="clearer">
        </div>
        <a href="#">
            <img src="/image/arrow.png" alt="arrow img" /></a>
    </div>--%>
</div>
