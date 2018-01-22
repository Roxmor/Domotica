<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebDashBoard.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="restricted/css/main.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <asp:Login ID="lgnLogin" runat="server" DestinationPageUrl="~/Restricted/MainPage.aspx" MembershipProvider="KortereNaam">
                </asp:Login>
    <br />
    <asp:Label ID="lblRegister" runat="server" Text="Nog geen account?"></asp:Label>
    <br />
    <asp:Button ID="btnRegister" runat="server" Text="Klik hier!" OnClick="btnRegister_Click" />
        </div>
    </form>
</body>
</html>
