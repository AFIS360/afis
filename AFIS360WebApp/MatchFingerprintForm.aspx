<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchFingerprintForm.aspx.cs" Inherits="AFIS360WebApp.MatchFingerprintForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:FileUpload ID="FileUploadMatchFpUpload" runat="server" />
    </div>
        <asp:Button ID="BtnMatchLoadFp" runat="server" OnClick="BtnMatchLoadFp_Click" Text="Load Fingerprint" />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Image ID="fingerprintImage" runat="server" />
        <p>
            <asp:Button ID="BtnMatchFingerprint" runat="server" OnClick="BtnMatchFingerprint_Click" Text="Match Fingerprint" />
        </p>
    </form>
</body>
</html>
