<%@ Page Title="Opties" Language="C#" MasterPageFile="~/Restricted/MasterPage1.Master" AutoEventWireup="true" CodeBehind="Opties.aspx.cs" Inherits="WebDashBoard.Opties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="gridview">
            <asp:Button ID="btnSites" runat="server" Text="Klik hier om sites te selecteren" />
            <asp:Button ID="btnGames" runat="server" Text="Button" />
        </div>
    <asp:Label ID="lblUsername" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>
