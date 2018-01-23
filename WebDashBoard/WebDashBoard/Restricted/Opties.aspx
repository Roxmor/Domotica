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
            <asp:AccessDataSource ID="adsDDLSites" runat="server" DataFile="~/App_Data/Dashboard.accdb" SelectCommand="SELECT [Naam] FROM [Weblinks]"></asp:AccessDataSource>
            <asp:DropDownList ID="ddlSites" runat="server" AutoPostBack="True" DataSourceID="adsDDLSites" DataTextField="Naam" DataValueField="Naam">
            </asp:DropDownList>
            <br />
            <asp:AccessDataSource ID="adsWebid" runat="server" DataFile="~/App_Data/Dashboard.accdb" SelectCommand="SELECT [Webid] FROM [Weblinks] WHERE ([Naam] = ?)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlSites" Name="Naam" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:AccessDataSource>
            <asp:AccessDataSource ID="adsUserid" runat="server" DataFile="~/App_Data/Dashboard.accdb" SelectCommand="SELECT [Userid] FROM [Gebruiker] WHERE ([Gebruikersnaam] = ?)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblUsername" Name="Gebruikersnaam" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:AccessDataSource>
            <asp:AccessDataSource ID="adsINSERT" runat="server" DataFile="~/App_Data/Dashboard.accdb" SelectCommand="SELECT * FROM [Mainweb]" InsertCommand="INSER INTO Mainweb (Userid, Webid) VALUES (?,?)">
                <InsertParameters>
                    <asp:ControlParameter ControlID="ddlUserid" Name="?" PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="ddlWebid" Name="?" PropertyName="SelectedValue" />
                </InsertParameters>
            </asp:AccessDataSource>
            <asp:Button ID="btnInvoer" runat="server" Text="Selecteer site om te weergeven" OnClick="btnInvoer_Click" />
            <br />
        </asp:View>
        <asp:View ID="viwGames" runat="server">
        </asp:View>
        <asp:View ID="viwAddsites" runat="server">
        </asp:View>
        <asp:View ID="viwEditsites" runat="server">
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="lblUsername" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:DropDownList ID="ddlWebid" runat="server" DataSourceID="adsWebid" DataTextField="Webid" DataValueField="Webid" Visible="False"></asp:DropDownList>
    <asp:DropDownList ID="ddlUserid" runat="server" DataSourceID="adsUserid" DataTextField="Userid" DataValueField="Userid" Visible="False"></asp:DropDownList>
</asp:Content>
