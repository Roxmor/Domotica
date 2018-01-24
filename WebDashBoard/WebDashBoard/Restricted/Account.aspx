<%@ Page Title="Account" Language="C#" MasterPageFile="~/Restricted/MasterPage1.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="WebDashBoard.Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="account">
        Username:
        <asp:LoginName class="accountname" ID="lnnName" runat="server" />
    </div>
</asp:Content>
