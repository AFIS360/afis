<%@ Page Title="Login" Language="C#" MasterPageFile="~/SiteLogin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AFIS360WebApp.WebForm1" %>

<asp:Content ID="ContentMenu" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 1000px;">
                    <asp:Image ID="ImageComapnyLogo" runat="server" Height="157px" />
                </td>
            </tr>
            <tr>
                <td style="width: 1000px;" colspan="1">
                    <asp:Login ID="UserLogin" runat="server" DestinationPageUrl="~/MainMenu.aspx" OnAuthenticate="UserLogin_Authenticate" BorderColor="#CCCC99" BorderStyle="Solid" BackColor="#F7F7DE" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt">
                        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
                    </asp:Login>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
