<%@ Page Title="Sites" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="Sites.aspx.cs" Inherits="WebDashBoard.Sites" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvwSites" runat="server" AutoGenerateColumns="False" DataKeyNames="Webid" DataSourceID="AccessDataSource1" OnSelectedIndexChanged="gvwSites_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="Url" HeaderText="Url" SortExpression="Url" />
            <asp:BoundField DataField="Naam" HeaderText="Naam" SortExpression="Naam" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:FormView ID="fvwSites" runat="server" DataKeyNames="Webid" DataSourceID="AccessDataSource1">
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
            Url:
            <asp:TextBox ID="UrlTextBox" runat="server" Text='<%# Bind("Url") %>' />
            <br />
            Naam:
            <asp:TextBox ID="NaamTextBox" runat="server" Text='<%# Bind("Naam") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            Webid:
            <asp:Label ID="WebidLabel" runat="server" Text='<%# Eval("Webid") %>' />
            <br />
            Url:
            <asp:Label ID="UrlLabel" runat="server" Text='<%# Bind("Url") %>' />
            <br />
            Naam:
            <asp:Label ID="NaamLabel" runat="server" Text='<%# Bind("Naam") %>' />
            <br />

            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
        </ItemTemplate>
    </asp:FormView>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/Dashboard.accdb" DeleteCommand="DELETE FROM [Weblinks] WHERE [Webid] = ?" InsertCommand="INSERT INTO [Weblinks] ([Url], [Naam]) VALUES (?, ?)" SelectCommand="SELECT * FROM [Weblinks]" UpdateCommand="UPDATE [Weblinks] SET [Url] = ?, [Naam] = ? WHERE [Webid] = ?">
        <DeleteParameters>
            <asp:Parameter Name="Webid" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Url" Type="String" />
            <asp:Parameter Name="Naam" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Url" Type="String" />
            <asp:Parameter Name="Naam" Type="String" />
            <asp:Parameter Name="Webid" Type="Int32" />
        </UpdateParameters>
    </asp:AccessDataSource>
</asp:Content>
