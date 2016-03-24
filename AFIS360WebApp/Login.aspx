<%@ Page Title="Login" Language="C#" MasterPageFile="~/SiteLogin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AFIS360WebApp.WebForm1" %>

<asp:Content ID="ContentMenu" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div>
        <table>
            <tr style="text-align: center">
                <td><h1>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1></td>
                <td>
                    <asp:Image ID="ImageComapnyLogo" runat="server" Height="200" Width="200"/>
                </td>
            </tr>
            <tr>
                <td><h1>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h1></td>
                <td>
                    <asp:Login ID="UserLogin" runat="server" DestinationPageUrl="~/MainMenu.aspx" OnAuthenticate="UserLogin_Authenticate" BorderColor="#CCCC99" BorderStyle="Solid" BackColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt">
                        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
                    </asp:Login>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
