<%@ Page Title="Account" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="WebDashBoard.Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvwAccount" runat="server">
        <asp:View ID="viwAccount" runat="server">
            blyat<br />
            <asp:Button ID="btnPassword" runat="server" OnClick="btnPassword_Click" Text="Verander wachtwoord" />
        </asp:View>
        <asp:View ID="viwPassword" runat="server">
            <asp:ChangePassword ID="ChangePassword1" runat="server">
            </asp:ChangePassword>
            <br />
            <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Terug naar het begin" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
