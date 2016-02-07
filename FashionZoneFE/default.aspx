<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FashionZone.FE._default"
    EnableViewStateMac="true" ViewStateEncryptionMode="Always" EnableEventValidation="true" ViewStateMode="Disabled" %>

<%@ Register Src="~/CustomControl/campaignLogosUC.ascx" TagPrefix="custom" TagName="logos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="description" content="Online Fashion sales in Albania for world renowned brands."/>
    <meta name="ROBOTS" content="INDEX, FOLLOW"/>
    <title>FZone - Online Fashion sales in Albania</title>
    <link href="/css/intro2.css" rel="stylesheet" type="text/css" />
    <link rel="SHORTCUT ICON" href="/favicon.ico" />
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-31957354-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
</head>
<body>
<div id="fb-root"></div>
    <script type="text/javascript">
        window.fbAsyncInit = function () {
            FB.init({
                appId: '382326631847472', // App ID
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                xfbml: true,
                oauth: true// parse XFBML
            });

            // auto popup
            //FB.getLoginStatus(function (response) {
            //    if (response.status === 'not_authorized') {
            //        // the user is logged in to Facebook, 
            //        // but has not authenticated your app
            //        FB.login(function (response) { }, { scope: 'email' });
            //    }
            //});

            FB.Event.subscribe('auth.authResponseChange', function (response) {
                if (response.status === 'connected') {
                    // the user is logged in and has authenticated your
                    // app, and response.authResponse supplies
                    // the user's ID, a valid access token, a signed
                    // request, and the time the access token 
                    // and signed request each expire
                    var uid = response.authResponse.userID;
                    var accessToken = response.authResponse.accessToken;

                    // Handle the access token
                    var form = document.createElement("form");
                    form.setAttribute("method", 'post');
                    form.setAttribute("action", '');

                    var field = document.createElement("input");
                    field.setAttribute("type", "hidden");
                    field.setAttribute("name", 'accessToken');
                    field.setAttribute("value", accessToken);
                    form.appendChild(field);

                    document.body.appendChild(form);
                    form.submit();
                }
            });
        };

        // Load the SDK Asynchronously
        (function (d) {
            var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement('script'); js.id = id; js.async = true;
            js.src = "//connect.facebook.net/en_US/all.js";
            ref.parentNode.insertBefore(js, ref);
        }(document));
    </script>
    <form id="form1" runat="server" action="/">
        <asp:Panel ID="panelLogin" runat="server" DefaultButton="LoginUser$LoginButton">
            <div class="allContent">
                <asp:Login ID="LoginUser" runat="server" EnableViewState="false" Width="400" DestinationPageUrl="/home"
                    FailureText="<%$Resources:Lang, LoginFailed%>">
                    <LayoutTemplate>
                        <div class="IntroContent">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <img src="/image/logo_fzone.png" alt="FZone" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <span class="failureNotification">
                                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Red">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" ForeColor="Black">Email:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" Width="140"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            CssClass="failureNotification" ErrorMessage="Email is required." ToolTip="Email is required."
                                            ValidationGroup="LoginUserValidationGroup" ForeColor="Black">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="valEmail" runat="server" ErrorMessage="<%$Resources:Lang, NotValidLabel%>"
                                            ForeColor="Red" ControlToValidate="UserName" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="regValidation"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Red">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" ForeColor="Black">Password:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" Width="140" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                            ValidationGroup="LoginUserValidationGroup" ForeColor="Black">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <div class="MyButton">
                                            <asp:LinkButton ID="LoginButton" runat="server" CommandName="Login" Text="<%$Resources:Lang, LoginLabel%>"
                                                ValidationGroup="LoginUserValidationGroup"></asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: right;">
                                        <a href="javascript:void(0)" class="facebook-login" onclick="FB.login(function (response) { }, { scope: 'email, user_birthday' });">
                                            <image src="image/login.png" alt="Login with Facebook"></image>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label runat="server" ID="Label2" Text="<%$Resources:Lang, EnterTheClubLabel%>"
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a href="/resetPassword/">
                                            <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, ForgotPassword%>"></asp:Label></a>
                                    </td>
                                    <td style="text-align: center;">
                                        <div class="MyButton">
                                            <a href="/register">
                                                <asp:Literal runat="server" ID="litRegister" Text="<%$Resources:Lang, RegisterLabel%>"></asp:Literal></a>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" rowspan="5" valign="bottom">
                                        <a href="/public/aboutus/" title="About">
                                            <asp:Label runat="server" ID="lblAbout" Text="<%$Resources:Lang, AboutUsLabel%>"
                                                Font-Bold="true"></asp:Label></a>
                                    </td>
                                    <td colspan="1" style="text-align: right;">
                                        <asp:ImageButton runat="server" ID="lnkShqip" OnClick="languageLinkButton_Click"
                                            CommandArgument="sq-AL" ImageUrl="~/image/flag-al.png" AlternateText="Shqip"></asp:ImageButton>
                                        <asp:ImageButton runat="server" ID="lnkEnglish" OnClick="languageLinkButton_Click"
                                            CommandArgument="en-US" ImageUrl="~/image/flag-en.png" AlternateText="English"></asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </LayoutTemplate>
                </asp:Login>
            </div>
            <div class="Dx">
                <asp:Label runat="server" ID="lblWhatIs" Text="<%$Resources:Lang, WhatIsFzoneLabel%>"></asp:Label>
                <br />
                <br />
                <asp:Label runat="server" ID="lblActualBrands" Text="<%$Resources:Lang, ActualCampaignsIntroLabel%>" Font-Bold="true">
                   <%-- <asp:Label runat="server" ID="Label3" Text="Oferta e fundvitit: Paguaj 1 merr 2!" Font-Bold="true">--%>
                </asp:Label>
                <br />
                <br />
                <custom:logos runat="server" ID="rptLogos"></custom:logos>
            </div>
        </asp:Panel>

    </form>
</body>
</html>
