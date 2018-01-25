<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebDashBoard.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <p>
                    <asp:CreateUserWizard ID="cuwRegister" runat="server" FinishDestinationPageUrl="~/Login.aspx" LoginCreatedUser="False" MembershipProvider="KortereNaam" RequireEmail="False">
                        <WizardSteps>
                            <asp:CreateUserWizardStep runat="server" />
                            <asp:CompleteWizardStep runat="server" />
                        </WizardSteps>
                    </asp:CreateUserWizard>
</p>
        </div>
    </form>
</body>
</html>
