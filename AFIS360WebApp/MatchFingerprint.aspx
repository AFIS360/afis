<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MatchFingerprint.aspx.cs" Inherits="AFIS360WebApp.WebForm5" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:Table ID="TableMain" runat="server">

        <asp:TableRow>
            <asp:TableCell>
                <asp:Panel ID="PanelFingerprintMatchControls" runat="server" BorderStyle="Solid" Width="819px">
                    <asp:Table ID="TableFingerprintMatch" runat="server" Height="96px" Width="434px">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:FileUpload ID="FileUploadMatchFpUpload" runat="server" /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="BtnMatchLoadFp" runat="server" OnClick="BtnMatchLoadFp_Click" Text="Load Fingerprint" /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="BtnMatchFingerprint" runat="server" OnClick="BtnMatchFingerprint_Click" Text="Match Fingerprint" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
            </asp:TableCell>

            <asp:TableCell>
                <asp:Panel ID="PanelFingerprintImageView" runat="server">
                    <asp:Image ID="FingerprintImage" runat="server" BorderStyle="Solid" Height="100" Width="100" />
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Image ID="PassportPhoto" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Panel ID="PanelPersonDetail" runat="server" BorderStyle="Solid">
                    <asp:Table ID="TablePersonDetail" runat="server">
                        <asp:TableRow>
                            <asp:TableCell Width="100">
                                <asp:Label ID="LabelPersonID" runat="server"></asp:Label></asp:TableCell>
                            <asp:TableCell Width="100">
                                <asp:Label ID="LabelPersonName" runat="server"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="200">
                                <asp:Label ID="LabelAddress" runat="server"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
            </asp:TableCell>

        </asp:TableRow>

    </asp:Table>

</asp:Content>
