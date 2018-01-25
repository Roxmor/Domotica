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
            <asp:AccessDataSource ID="adsWebsites" runat="server" DataFile="~/App_Data/Dashboard.accdb" DeleteCommand="DELETE FROM [Weblinks] WHERE [Webid] = ?" InsertCommand="INSERT INTO [Mainweb] ([Userid], [Webid]) VALUES (?, ?)" SelectCommand="SELECT Weblinks.Webid, Weblinks.Url, Weblinks.Naam, Mainweb.Userid, Mainweb.Webid AS Expr1, Gebruiker.Userid AS Expr2, Gebruiker.Gebruikersnaam, Gebruiker.Wachtwoord, Gebruiker.Email, Gebruiker.Vraag, Gebruiker.Reminder FROM ((Weblinks INNER JOIN Mainweb ON Weblinks.Webid = Mainweb.Webid) INNER JOIN Gebruiker ON Gebruiker.Userid = Mainweb.Userid)" UpdateCommand="UPDATE [Weblinks] SET [Url] = ?, [Naam] = ? WHERE [Webid] = ?">
                <DeleteParameters>
                    <asp:Parameter Name="Webid" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:ControlParameter Name="Userid" ControlID="lblUserid" PropertyName="" />
                    <asp:Parameter Name="Webid" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Url" Type="String" />
                    <asp:Parameter Name="Naam" Type="String" />
                    <asp:Parameter Name="Webid" Type="Int32" />
                </UpdateParameters>
            </asp:AccessDataSource>
            <asp:AccessDataSource ID="adsForDDL" runat="server" DataFile="~/App_Data/Dashboard.accdb" SelectCommand="SELECT * FROM [Weblinks]"></asp:AccessDataSource>
            <asp:DropDownList ID="ddlWebsites" runat="server" DataSourceID="adsWebsites" DataTextField="Naam" DataValueField="Naam" AutoPostBack="True" OnSelectedIndexChanged="ddlWebsites_SelectedIndexChanged">
            </asp:DropDownList>
            <br />
            <asp:FormView ID="fvwAddsites" runat="server" DataSourceID="adsWebsites">
                <EditItemTemplate>
                    Webid:
                    <asp:Label ID="WebidLabel1" runat="server" Text='<%# Eval("Webid") %>' />
                    <br />
                    Url:
                    <asp:TextBox ID="UrlTextBox" runat="server" Text='<%# Bind("Url") %>' />
                    <br />
                    Naam:
                    <asp:TextBox ID="NaamTextBox" runat="server" Text='<%# Bind("Naam") %>' />
                    <br />
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                    &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                    &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                    &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
                    &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
                </ItemTemplate>
            </asp:FormView>
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
