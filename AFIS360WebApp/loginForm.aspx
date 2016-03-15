<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="AFIS360WebApp.loginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In</title>
    <script src="scripts/jquery-1.12.1.js"></script>
    <script>
        function startTime() {
            var today = new Date();
            var d = today.getDate();
            var M = today.getMonth() + 1;
            var y = today.getFullYear();
            var h = today.getHours();
            var m = today.getMinutes();
            var s = today.getSeconds();
            m = checkTime(m);
            s = checkTime(s);
            document.getElementById('LabelDate').innerHTML = d + "/" + M + "/" + y;
            document.getElementById('LabelClock').innerHTML = h + ":" + m + ":" + s;
            var t = setTimeout(startTime, 500);
        }
        function checkTime(i) {
            if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
            return i;
        }
    </script>
</head>
<body onload="startTime()">
    <form id="UserLoginForm" runat="server">
    <div>
        <asp:Table ID="HeaderTableCompanyLogo" runat="server" HorizontalAlign ="Left" Width="40%" BorderStyle="Solid">
             <asp:TableRow>
                  <asp:TableCell HorizontalAlign="Left" Font-Italic="True"><asp:Label ID="LabelCompanyLogo" runat="server" Text="AFIS/360 powered by Lakers Tek USA"></asp:Label></asp:TableCell>
              </asp:TableRow>
        </asp:Table>
        <asp:Table ID="HeaderTableDateTime" runat="server" HorizontalAlign="Center" Width="60%" BorderStyle="Solid">
             <asp:TableRow>
                  <asp:TableCell HorizontalAlign="Right"><asp:Label ID="LabelToday" runat="server" Text="Today: "></asp:Label></asp:TableCell>
                  <asp:TableCell HorizontalAlign="Left" Font-Bold="True"><asp:Label ID="LabelDate" runat="server"></asp:Label></asp:TableCell>
                  <asp:TableCell HorizontalAlign="Right"><asp:Label ID="LabelLocalTime" runat="server" Text="Local Time: "></asp:Label></asp:TableCell>
                  <asp:TableCell HorizontalAlign="Left"><asp:Label ID="LabelClock" runat="server" Font-Bold="True"></asp:Label></asp:TableCell>
                  <asp:TableCell HorizontalAlign="Right"><asp:Label ID="LabelLoginAs" runat="server" Text="Login As:"></asp:Label></asp:TableCell>
                  <asp:TableCell HorizontalAlign="Left" Font-Bold="True"><asp:Label ID="LabelLoginUserInfo" runat="server"></asp:Label></asp:TableCell>
             </asp:TableRow>
        </asp:Table>

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
