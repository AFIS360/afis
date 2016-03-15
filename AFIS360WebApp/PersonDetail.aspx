<%@ Page Title="Person Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonDetail.aspx.cs" Inherits="AFIS360WebApp.WebForm4" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

            <asp:Table ID="TablePersonDetailSearch" runat="server" Height="96px" Width="434px">
            <asp:TableRow>
                <asp:TableCell>Person ID:</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="txtBoxPersonId" runat="server"></asp:TextBox></asp:TableCell>
                <asp:TableCell><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="Button1_Click" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="TablePersonDetail" runat="server" Height="96px" Width="395px">
            <asp:TableRow>
                <asp:TableCell><asp:Image ID="passportPhoto" runat="server" /> </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>First Name:</asp:TableCell>
                <asp:TableCell><asp:Label ID="lblFirstName" runat="server"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Last Name:</asp:TableCell>
                <asp:TableCell><asp:Label ID="lblLastName" runat="server"></asp:Label></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

</asp:Content>
