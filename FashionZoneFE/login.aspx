<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true"
    CodeBehind="login.aspx.cs" Inherits="FashionZone.FE.login" EnableViewStateMac="true" ViewStateEncryptionMode="Always" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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

            FB.Event.subscribe('auth.authResponseChange', function (response) {
                if (response.status === 'connected') {
                    // the user is logged in and has authenticated your
                    // app, and response.authResponse supplies
                    // the user's ID, a valid access token, a signed
                    // request, and the time the access token 
                    // and signed request each expire
                    var uid = response.authResponse.userID;
                    var accessToken = response.authResponse.accessToken;

                    // TODO: Handle the access token
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
    <center>
        <div>
            <asp:Panel ID="panelLogin" runat="server" DefaultButton="LoginUser$LoginButton">
                <asp:Login ID="LoginUser" runat="server" Width="400" DestinationPageUrl="/home/" FailureText="<%$Resources:Lang, LoginFailed%>">
                    <LayoutTemplate>
                        <table class="TableRegister">
                            <tr>
                                <td colspan="2" class="Red" style="text-align: left;">
                                    <h2>
                                        <asp:Label runat="server" ID="lblGender" Text="<%$Resources:Lang, AreYourRegisteredLabel%>"></asp:Label>
                                    </h2>
                                </td>
                                <%--                                <tr>
                                    <td style="text-align: left;">
                                        <b>
                                            <asp:Label runat="server" ID="Label1" Text="<%$Resources:Lang, AccessTheSiteLabel%>"></asp:Label>
                                        </b>
                                    </td>
                                </tr>--%>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <span class="failureNotification">
                                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td class="Red" style="text-align: left;">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Email:</asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="UserName" runat="server" Width="140"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                        ValidationGroup="LoginUserValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="valEmail" runat="server" ErrorMessage="<%$Resources:Lang, NotValidLabel%>"
                                        ForeColor="Red" ControlToValidate="UserName" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="LoginUserValidationGroup"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="Red" style="text-align: left;">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="Password" runat="server" Width="140" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                        ValidationGroup="LoginUserValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="Red" colspan="2">
                                    <a href="/resetPassword/">
                                        <asp:Label runat="server" ID="Label4" Text="<%$Resources:Lang, ForgotPassword%>"></asp:Label></a>
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
                                        <a href="javascript:void(0)" onclick="FB.login(function (response) { }, { scope: 'email, user_birthday' });">
                                            <image src="/image/login.png" alt="Login with Facebook"></image>
                                        </a>
                                    </td>
                                </tr>
                            <tr>
                                <td colspan="2">
                                    <img src="/image/linetrasparent.png" alt="Decoration" class="Decoration" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="Red" style="text-align: left;">
                                    <h2>
                                        <asp:Label runat="server" ID="Label2" Text="<%$Resources:Lang, EnterTheClubLabel%>"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <b>
                                        <asp:Label runat="server" ID="Label3" Text="<%$Resources:Lang, ExclusiveSalesLabel%>"></asp:Label>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <div class="MyButton">
                                        <a href="/register/">
                                            <asp:Literal runat="server" ID="litRegister" Text="<%$Resources:Lang, RegisterLabel%>"></asp:Literal></a>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:Login>
            </asp:Panel>
        </div>
    </center>
</asp:Content>
