<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartsDrawingUpload.aspx.cs" Inherits="Pages_WorkOrderManage_PartsDrawingUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <meta content="width=device-width, initial-scale=1.0" name="viewport" />

    <meta content="" name="description" />

    <meta content="" name="author" />
    <script src="../../ClientMedia/js/jquery-1.11.2.js" type="text/javascript"></script>
    <script src="../../ClientMedia/js/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function uploadSelect(jQElement) {
            try {
                var err = parent.upload.uploading();
                if ($.trim(err) != '') {
                    alert(err);
                    jQElement.after(jQElement.clone()).remove();
                    return;
                }
                jQElement.fadeOut("fast");
                $("#frmUpload").submit();
            } catch (e) { };
        };

        var uploaderror = parent.upload ? parent.upload.uploaderror : null;

        var uploadsuccess = parent.upload ? parent.upload.uploadsuccess : null;
    </script>
</head>
<body>
    <form runat="server" id="frmUpload" method="post" enctype="multipart/form-data">
        <div>
            <input type="file" runat="server" id="file1" size="40" onchange="javascript:uploadSelect($(this));" />
            <asp:Button ID="Button1" runat="server" Text="上传" OnClick="Button1_Click" />
            <asp:Label ID="Label1" runat="server" Text="消息"></asp:Label>
        </div>
    </form>
</body>
</html>
