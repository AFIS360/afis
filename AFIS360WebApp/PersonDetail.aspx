<%@ Page Title="Person Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonDetail.aspx.cs" Inherits="AFIS360WebApp.WebForm4" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <table style="width: 100%;">
        <tr>
            <td colspan ="2" class="auto-style2">
                <asp:Table ID="TablePersonDetailSearch" runat="server" Height="23px" Width="998px">
                    <asp:TableRow VerticalAlign="Top">
                        <asp:TableCell HorizontalAlign="Left">Person ID: <asp:TextBox ID="txtBoxPersonId" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="SubmitButton_Click" /></asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="lblStatusMsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Table ID="TablePersonDetail" runat="server" Height="96px" Width="395px">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Image ID="passportPhoto" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Font-Bold="True">
                            Name:
                <asp:Label ID="lblName" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Font-Bold="True">
                            Address:
                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Street:
                <asp:Label ID="lblStreet" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            City:
                <asp:Label ID="lblCity" runat="server"></asp:Label>, State:
                <asp:Label ID="lblState" runat="server"></asp:Label>, Country:
                <asp:Label ID="lblCountry" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Font-Bold="True">
                            DOB:
                <asp:Label ID="lblDOB" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Font-Bold="True">
                            Cell#:
                <asp:Label ID="lblCellNbr" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Work#:
                <asp:Label ID="lblWorkNbr" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Home#:
                <asp:Label ID="lblHomeNbr" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Email:
                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

            </td>
            <td>
                <asp:Table ID="TablePersonDetailPhysical" runat="server" HorizontalAlign="Left">
                    <asp:TableRow>
                        <asp:TableCell Font-Bold="True">Physical Characteristics:</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>Height: <asp:Label ID="lblHeight" runat="server"></asp:Label>, Weight: <asp:Label ID="lblWeight" runat="server"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                            <asp:TableCell>Eye Color: <asp:Label ID="lblEyeColor" runat="server"></asp:Label>, Hair Color: <asp:Label ID="lblHairColor" runat="server"></asp:Label>, Complexion: <asp:Label ID="lblComplexion" runat="server"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                            <asp:TableCell>Build Type: <asp:Label ID="lblBuildType" runat="server"></asp:Label>, Birth Mark: <asp:Label ID="lblBrithMark" runat="server"></asp:Label>, Other Identification Mark: <asp:Label ID="lblIDMark" runat="server"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Top" HorizontalAlign="Left">
                            <asp:TableCell>Gender: <asp:Label ID="lblGender" runat="server"></asp:Label>, Death Date: <asp:Label ID="lblDeathDate" runat="server"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="TableCriminalRecord" runat="server" HorizontalAlign="Left">
                    <asp:TableRow>
                        <asp:TableCell Font-Bold="True">Criminal Record(s):</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="RowCriminalRec_1" VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>Case#: <asp:HyperLink ID="CaseNo_1" runat="server"><asp:Label ID="lblCaseNo_1" runat="server"></asp:Label></asp:HyperLink>, Crime Location: <asp:Label ID="lblCrimeLoc_1" runat="server"></asp:Label>, Occurance Date: <asp:Label ID="lblCrimeDate_1" runat="server"></asp:Label>, Status: <asp:Label ID="lblStatus_1" runat="server"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="RowCriminalRec_2" VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>Case#: <asp:HyperLink ID="CaseNo_2" runat="server"><asp:Label ID="lblCaseNo_2" runat="server"></asp:Label></asp:HyperLink>, Crime Location: <asp:Label ID="lblCrimeLoc_2" runat="server"></asp:Label>, Occurance Date: <asp:Label ID="lblCrimeDate_2" runat="server"></asp:Label>, Status: <asp:Label ID="lblStatus_2" runat="server"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="RowCriminalRec_3" VerticalAlign="Top" HorizontalAlign="Left">
                        <asp:TableCell>Case#: <asp:HyperLink ID="CaseNo_3" runat="server"><asp:Label ID="lblCaseNo_3" runat="server"></asp:Label></asp:HyperLink>, Crime Location: <asp:Label ID="lblCrimeLoc_3" runat="server"></asp:Label>, Occurance Date: <asp:Label ID="lblCrimeDate_3" runat="server"></asp:Label>, Status: <asp:Label ID="lblStatus_3" runat="server"></asp:Label></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>



</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 393px;
        }
        .auto-style2 {
            height: 64px;
        }
    </style>
</asp:Content>

