<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="AFIS360WebApp.loginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In</title>
    <script src="scripts/jquery-1.12.1.js"></script>
</head>
<body>
    <form id="UserLoginForm" runat="server">
    <div>
        <asp:Panel ID="PanelCompanyLogo" runat="server" Width="475px">
            <asp:Image ID="ImageComapnyLogo" runat="server" ImageUrl="~/images/CompanyLogo.jpg" />
        </asp:Panel>
        <asp:Panel ID="PanelUserLoginPanel" runat="server" Width="475px">
            <asp:Login ID="UserLogin" runat="server" DestinationPageUrl="~/MenuPage.aspx" OnAuthenticate="UserLogin_Authenticate"  BorderColor="Black" BorderStyle="Solid"></asp:Login>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
