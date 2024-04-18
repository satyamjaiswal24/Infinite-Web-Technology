<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelcomeWebForm.aspx.cs" Inherits="FormValidation.WelcomeWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome</h1>
            <asp:Label ID="lblName" runat="server"></asp:Label><br />
            <asp:Label ID="lblFamilyName" runat="server"></asp:Label><br />
            <asp:Label ID="lblAddress" runat="server"></asp:Label><br />
            <asp:Label ID="lblCity" runat="server"></asp:Label><br />
            <asp:Label ID="lblZipCode" runat="server"></asp:Label><br />
            <asp:Label ID="lblPhone" runat="server"></asp:Label><br />
            <asp:Label ID="lblEmail" runat="server"></asp:Label><br />
        </div>
    </form>
</body>
</html>
