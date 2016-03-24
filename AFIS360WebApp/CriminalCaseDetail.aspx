<%@ Page Title="Criminal Case Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CriminalCaseDetail.aspx.cs" Inherits="AFIS360WebApp.WebForm6" %>

<asp:Content ID="ContentCriminalCaseDetail" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h2>Detail Case Information:</h2>
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Table ID="TableCriminalCaseDetail" runat="server" HorizontalAlign="Left">
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            <asp:Label ID="lblCrimeRecord" runat="server" Text="Crime Record:" Font-Bold="True"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Case ID: <asp:Label ID="lblCaseID" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Crime Location: <asp:Label ID="lblCrimeLoc" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Occurance Date: <asp:Label ID="lblCrimeDate" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
            <td>
                <asp:Table ID="TableCourtAndStatute" runat="server" HorizontalAlign="Left">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblCourtAndStatute" runat="server" Text="Court & Statute:" Font-Bold="True"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Court: <asp:Label ID="lblCourtType" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Address: <asp:Label ID="lblCourtAddress" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Statute: <asp:Label ID="lblStatute" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Table ID="TableArrestAndSentence" runat="server" HorizontalAlign="Left">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblArrestAndSentence" runat="server" Text="Arrest & Sentence:" Font-Bold="True"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Arrest Date: <asp:Label ID="lblArrestDate" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Arrest Agency: <asp:Label ID="lblArrestAgency" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Sentence Date: <asp:Label ID="lblSentenceDate" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Release Date: <asp:Label ID="lblReleaseDate" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Parole Date: <asp:Label ID="lblParoleDate" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Current Status: <asp:Label ID="lblCurrentStatus" runat="server" Font-Bold="True"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

            </td>
            <td>
                <asp:Table ID="TableAlertNote" runat="server" HorizontalAlign="Left">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblAlertNote" runat="server" Text="Alert Note:" Font-Bold="True"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Alert Level: <asp:Label ID="lblAlertLevel" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>
                            Alert Message: <asp:Label ID="lblAlertMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td colspan ="2">
                <asp:Table ID="TableCrimeDetail" runat="server" HorizontalAlign="Left">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblCrimeDescription" runat="server" Text="Crime Description:" Font-Bold="True"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblCrimeDescriptionText" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>


</asp:Content>
