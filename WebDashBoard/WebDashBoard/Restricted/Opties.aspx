<%@ Page Title="Opties" Language="C#" MasterPageFile="~/Restricted/MasterPage1.Master" AutoEventWireup="true" CodeBehind="Opties.aspx.cs" Inherits="WebDashBoard.Opties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvwOpties" runat="server">
        <asp:View ID="viwKeuze" runat="server">
            <asp:Button ID="btnSites" runat="server" Text="Kies je sites voor de dashboard" OnClick="btnSites_Click" />
            <br />
            <asp:Button ID="btnGames" runat="server" Text="Kies je games voor de dashboard" OnClick="btnGames_Click" />
        </asp:View>
        <asp:View ID="viwSites" runat="server">
            <br />
            <asp:Button ID="btnSitesDisplay" runat="server" Text="Klik hier om de site weer te geven op de dashboard" OnClick="btnSitesDisplay_Click" />
            <br />
            Uw site niet gevonden? Klik hieronder om een nieuwe site toe te voegen:
            <br />
            <asp:Button ID="btnSitesAdd" runat="server" Text="Klik hier om een nieuwe site toe te voegen" OnClick="btnSitesAdd_Click" />
            <br />
            <asp:Button ID="btnReturnS" runat="server" Text="Klik hier om terug te gaan naar het begin" />
        </asp:View>
        <asp:View ID="viwGames" runat="server">
            <asp:Button ID="btnReturnG" runat="server" OnClick="btnReturnG_Click" Text="Klik hier om terug te gaan naar het begin" />
        </asp:View>
        <asp:View ID="viwSitesAdd" runat="server">

            <asp:Button ID="btnReturnAS" runat="server" OnClick="btnReturnAS_Click" Text="Klik hier om terug te gaan naar de website selectie" />
            <br />
            <asp:Button ID="btnReturnA" runat="server" OnClick="btnReturnA_Click" Text="Klik hier om terug te gaan naar het begin" />

        </asp:View>
    </asp:MultiView>
    <br />
    <asp:Label ID="lblUsername" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblUserid" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblWebid" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>
