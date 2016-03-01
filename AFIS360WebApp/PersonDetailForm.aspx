<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonDetailForm.aspx.cs" Inherits="AFIS360WebApp.PersonDetailForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    
        <asp:Table ID="TablePersonDetailSearch" runat="server" Height="96px" Width="434px">

            <asp:TableRow>
                <asp:TableCell>Person ID:</asp:TableCell><asp:TableCell>
                <asp:TextBox ID="txtBoxPersonId" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

        </asp:Table>

        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="Button1_Click" />

        <asp:Table ID="TablePersonDetail" runat="server" Height="96px" Width="395px">

            <asp:TableRow>
                <asp:TableCell>First Name:</asp:TableCell>
                <asp:TableCell><asp:Label ID="lblFirstName" runat="server"></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Last Name:</asp:TableCell>
                <asp:TableCell><asp:Label ID="lblLastName" runat="server"></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Image ID="passportPhoto" runat="server" /> </asp:TableCell>
            </asp:TableRow>

        </asp:Table>

    </form>
</body>
</html>
