<%@ Page Title="Main" Language="C#" MasterPageFile="~/Restricted/MasterPage1.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebDashBoard.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="gridview">
        <asp:Button ID="btnDaHaus" class="griditem" runat="server" Text="Da Haus" OnClick="btnDaHaus_Click" />

        <asp:Button ID="btnGames" class="griditem" runat="server" Text="Games" OnClick="btnGames_Click" />

        <asp:Button ID="btnSites" class="griditem" runat="server" Text="Sites" OnClick="btnSites_Click" />

        <asp:Button ID="btnOpties" class="griditem" runat="server" Text="Opties" OnClick="btnOpties_Click" />

        <asp:Button ID="btnSteam" class="griditem" runat="server" Text="Steam" OnClick="btnSteam_Click" />

        <asp:Button ID="btnAccount" class="griditem" runat="server" Text="Account" OnClick="btnAccount_Click" />
    </div>
</asp:Content>

