<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MatchFingerprint.aspx.cs" Inherits="AFIS360WebApp.WebForm5" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <table style="width: 100%;">
        <tr>
            <td class="auto-style1" style="vertical-align: top">
                <asp:Table ID="TableFileUpload" runat="server" Width="383px">
                    <asp:TableRow>
                        <asp:TableCell>
                            Step 1:
                            <asp:FileUpload ID="FileUploadMatchFpUpload" runat="server" ToolTip="Choose the fingerprint file" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Step 2:
                            <asp:Button ID="BtnMatchLoadFp" runat="server" OnClick="BtnMatchLoadFp_Click" Text="Load Fingerprint" ToolTip="Load the fingerprint to the server" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Step 3:
                            <asp:Button ID="BtnMatchFingerprint" runat="server" OnClick="BtnMatchFingerprint_Click" Text="Match Fingerprint" ToolTip="Match the fingerpting" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
            <td>
                <asp:Table ID="TableFingerptint" runat="server" Height="151px" Width="161px">
                    <asp:TableRow>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1">
                            <asp:Image ID="FingerprintImage" runat="server" Height="150" Width="150" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td class="auto-style1" colspan="2">
                <asp:Table ID="TablePersonDemography" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Image ID="PassportPhoto" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="TablePersonIdentity" runat="server">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Font-Bold="True">
                            <asp:Label ID="LabelPersonID" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="LabelPersonName" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="LabelAddress" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            width: 397px;
        }
    </style>
</asp:Content>

