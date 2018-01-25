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
    <asp:Label ID="lblUsername" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:AccessDataSource ID="adsUserid" runat="server" DataFile="~/App_Data/Dashboard.accdb" SelectCommand="SELECT [Userid] FROM [Gebruiker] WHERE ([Gebruikersnaam] = ?)">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblUsername" Name="Gebruikersnaam" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:AccessDataSource>
    <asp:GridView ID="grvUserid" runat="server" AutoGenerateColumns="False" DataKeyNames="Userid" DataSourceID="adsUserid" SelectedIndex="0" Visible="False">
        <Columns>
            <asp:BoundField DataField="Userid" HeaderText="Userid" InsertVisible="False" ReadOnly="True" SortExpression="Userid" />
        </Columns>
</asp:GridView>
</asp:Content>

