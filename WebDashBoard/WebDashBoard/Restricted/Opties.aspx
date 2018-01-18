<%@ Page Title="Opties" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="Opties.aspx.cs" Inherits="WebDashBoard.Opties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="viwKeuze" runat="server"></asp:View>
        <asp:View ID="viwSites" runat="server"></asp:View>
        <asp:View ID="viwGames" runat="server"></asp:View>
    </asp:MultiView>
    
</asp:Content>
