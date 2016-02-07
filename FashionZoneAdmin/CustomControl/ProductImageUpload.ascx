<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductImageUpload.ascx.cs"
    Inherits="FashionZone.Admin.CustomControl.ProductImageUpload" %>
<%@ Register Src="~/CustomControl/FZFileUpload.ascx" TagName="FZFileUpload" TagPrefix="custom" %>
<asp:UpdatePanel runat="server" ID="updPnl" ClientIDMode="AutoID">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="prodID" />
        <asp:HiddenField runat="server" ID="oldImg" />
        <asp:HiddenField runat="server" ID="imgID" />
        <label for="Active">
            Principal :
        </label>
        <asp:CheckBox ID="chkPrincipal" runat="server" TabIndex="34" ClientIDMode="AutoID" />
        <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="MutuallyExclusiveCheckBoxExtender1"
            runat="server" TargetControlID="chkPrincipal" Key="<%=UsedBy %>">
        </ajaxToolkit:MutuallyExclusiveCheckBoxExtender>
        <br />
        <label for="LargeImage">
            Large image (720x720) :
        </label>
        <custom:FZFileUpload ID="img3" runat="server" FileExtensions="(jpg)|(png)|(jpeg)"
            ClientIDMode="AutoID" />
        <asp:Label ID="lblErrors" runat="server" ForeColor="Red" ClientIDMode="AutoID"></asp:Label>
        <div align="right">
            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" Text="Delete"
                Visible="false" ClientIDMode="AutoID" OnClick="lnkDelete_Click" />
            <ajaxToolkit:ConfirmButtonExtender ID="btnConfirm" runat="server" TargetControlID="lnkDelete"
                ConfirmText="Are you sure about deleting this image? Unsaved changes to other images will be lost."
                ClientIDMode="AutoID" />
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="lnkDelete" />
    </Triggers>
</asp:UpdatePanel>
