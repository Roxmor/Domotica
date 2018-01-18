<%@ Page Title="DaHaus" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeBehind="DaHaus.aspx.cs" Inherits="WebDashBoard.DaHaus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <asp:RangeValidator ID="ravHeater" runat="server" ErrorMessage="Voer een temperatuur in tussen de 12 en 35 graden." ControlToValidate="txt_heater" MaximumValue="35,0" MinimumValue="12,0" Type="Double"></asp:RangeValidator>
            <p>
            <asp:Label ID="lbl_State" runat="server" Text="¯\_(ツ)_/¯"></asp:Label>
        </p>
</asp:Content>
