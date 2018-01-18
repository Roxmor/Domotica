<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication4.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:Button ID="btn_Lamp1" runat="server" OnClick="Btn_Lamp1_Click" Text="Lamp 1" />
            <asp:Button ID="btn_Lamp2" runat="server" OnClick="Btn_Lamp2_Click" Text="Lamp 2" />
            <asp:Button ID="btn_Lamp3" runat="server" OnClick="Btn_Lamp3_Click" Text="Lamp 3" />
            <asp:Button ID="btn_Lamp4" runat="server" OnClick="Btn_Lamp4_Click" Text="Lamp 4" />
            <asp:Button ID="btn_Lamp5" runat="server" OnClick="Btn_Lamp5_Click" Text="Lamp 5" />
        </p>
        <p>
            <asp:Button ID="btn_Window1" runat="server" OnClick="Btn_Window1_Click" Text="Window 1" />
            <asp:Button ID="btn_Window2" runat="server" OnClick="Btn_Window2_Click" Text="Window 2" />
        </p>
        <asp:Button ID="btn_Heater" runat="server" OnClick="Btn_Heater_Click" Text="Heater" />
        <asp:TextBox ID="txt_heater" runat="server" Width="47px">21</asp:TextBox>
        <p>
            <asp:Label ID="lbl_State" runat="server" Text="¯\_(ツ)_/¯"></asp:Label>
        </p>
    </form>
</body>
</html>
