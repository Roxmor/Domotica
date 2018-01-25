<%@ Page Title="Opties" Language="C#" MasterPageFile="~/Restricted/MasterPage1.Master" AutoEventWireup="true" CodeBehind="Opties.aspx.cs" Inherits="WebDashBoard.Opties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="gridview">
        <asp:Button ID="btnSites" class="griditem" runat="server" Text="Klik hier om sites te selecteren" OnClick="btnSites_Click" />
        <asp:Button ID="btnGames" class="griditem" runat="server" Text="Klik hier om games te selecteren" OnClick="btnGames_Click" />
    </div>
    <asp:MultiView ID="mvwOpties" runat="server">
        <asp:View ID="viwSites" runat="server">
            <asp:AccessDataSource ID="adsWebsites" runat="server" DataFile="~/App_Data/Dashboard.accdb" SelectCommand="SELECT * FROM [Weblinks]"></asp:AccessDataSource>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="adsWebsites" DataTextField="Naam" DataValueField="Naam">
            </asp:DropDownList>
            <br />
            <br />
        </asp:View>
        <asp:View ID="viwGames" runat="server">
        </asp:View>
        <asp:View ID="viwAddsites" runat="server">
        </asp:View>
        <asp:View ID="viwEditsites" runat="server">
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="lblUserid" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>
