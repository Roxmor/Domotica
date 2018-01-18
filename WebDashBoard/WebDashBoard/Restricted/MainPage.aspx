<%@ Page Title="Main" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebDashBoard.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Button ID="btnDaHaus" runat="server" Text="Da Haus" OnClick="btnDaHaus_Click" />
    <br />
    <asp:Button ID="btnGames" runat="server" Text="Games" OnClick="btnGames_Click" />
    <br />
    <asp:Button ID="btnSites" runat="server" Text="Sites" OnClick="btnSites_Click" />
    <br />
    <asp:Button ID="btnOpties" runat="server" Text="Opties" OnClick="btnOpties_Click" />
    <br />
    <asp:Button ID="btnSteam" runat="server" Text="Steam" OnClick="btnSteam_Click" />
    <br />
    <asp:Button ID="btnAccount" runat="server" Text="Account" OnClick="btnAccount_Click" />
    <br />
</asp:Content>

