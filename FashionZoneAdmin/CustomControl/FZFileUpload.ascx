<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FZFileUpload.ascx.cs"
    Inherits="FashionZone.Admin.CustomControl.FZFileUpload" %>
<script type="text/javascript">
    //binding the upload control for the first time
    bindUpload();

    //The add_endRequest function is needed to rebind the jquery events on every update panel partial refresh
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindUpload);

    function bindUpload() {
        $(function () {
            function cleanFields() {
                $('#<%=UsedBy + "Upload"%>').attr('value', '');
            }

            function correctUpload(message) {
                $('#<%=UsedBy + "Correct"%>').css('display', '');
                $('#<%=UsedBy + "Correct"%>').html('File ' + message + ' uploaded correctly.');
            }

            function errorUpload(message) {
                $('#<%=UsedBy + "WrongExension"%>').css('display', '');
                $('#<%=UsedBy + "WrongExension"%>').html('Upload error: ' + message);
            }

            $('#<%=UsedBy + "Upload"%>').fileupload({
                replaceFileInput: false,
                dataType: 'json',
                url: '/AjaxFileHandler.ashx',
                formData: function () {
                    return [{ name: "oldFile", value: $('#<%=fileName.ClientID%>').val()}]
                },
                add: function (e, data) {
                    $('#<%=UsedBy + "WrongExension"%>').css('display', 'none');
                    $('#<%=UsedBy + "Correct"%>').css('display', 'none');
                    var valid = true;
                    var re = /^.+\.(<%=FileExtensions %>)$/i;
                    $.each(data.files, function (index, file) {
                        if (!re.test(file.name)) {
                            errorUpload('wrong file type, only <%=FileExtMessage %> are allowed.');
                            valid = false;
                            cleanFields();
                        }
                    });

                    if (valid)
                        data.submit();
                },
                submit: function (e, data) {
                    $('#<%=UsedBy + "Throbber"%>').css('display', '');
                },
                done: function (e, data) {
                    $.each(data.result, function (index, file) {
                        $('#<%=fileName.ClientID%>').attr('value', file);
                        $('#<%=uploadLink.ClientID%>').attr('href', '<%= FashionZone.Admin.Utils.Configuration.ImagesVisualizationPath %>' + file);
                        $('#<%=UsedBy + "Throbber"%>').css('display', 'none');
                    });

                    $.each(data.files, function (index, file) {
                        $('#<%=uploadLink.ClientID%>').html(file.name);
                        correctUpload(file.name);
                    });
                    // clean the fileupload input
                    $('#<%=UsedBy + "Upload"%>').attr('value', '');
                },

                fail: function (e, data) {
                    $.each(data.files, function (index, file) {
                        $('#<%=UsedBy + "Throbber"%>').css('display', 'none');
                        errorUpload(' file ' + file.name + ' couldn\'t be uploaded');
                        cleanFields();
                    });
                }
            });
        });
    }
</script>
<asp:HiddenField ID="usedBy" runat="server" ClientIDMode="AutoID" />
<asp:HiddenField ID="fileName" runat="server" ClientIDMode="AutoID" />
<asp:LoginView ID="lgnViewUpload" runat="server">
    <RoleGroups>
        <asp:RoleGroup Roles="Moderator, Administrator">
            <ContentTemplate>
                <input id="<%=UsedBy + "Upload"%>" type="file" name="file" style="width: 300px;" />
            </ContentTemplate>
        </asp:RoleGroup>
    </RoleGroups>
</asp:LoginView>
<a href="#" id="uploadLink" runat="server" clientidmode="AutoID" target="_blank">
</a>
<div id="<%=UsedBy + "Throbber"%>" style="display: none;">
    <img id="<%=UsedBy + "ThrobberImg"%>" alt="Uploading" src="/Images/ajax-loader.gif" />
</div>
<div id="fileUploadText">
</div>
<p id="<%=UsedBy + "WrongExension"%>" style="display: none; color: Red;">
</p>
<p id="<%=UsedBy + "Correct"%>" style="display: none; color: Green;">
</p>
