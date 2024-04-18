<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidationForm.aspx.cs" Inherits="FormValidation.ValidationForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Form</title>
</head>
    <style>

       body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        #formContainer {
            background-color: #fff;
            width: 500px;
            padding: 30px;
            border-radius: 8px;
            margin: 20px auto;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }

        .form-group input[type="text"] {
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .error-message {
            color: red;
            font-size: 12px;
        }

        #submit_button {
            background-color: deepskyblue;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

       


    </style>
<body>
    <form id="form1" runat="server">
        <div id="formContainer">

            <h2 style="position:center;text-align:center; color:orangered">Registration Form</h2><br />

            <div class="form-group">
                <label for="txtName">Name :</label>
                <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="error-message" ID="RequiredNameValidator" runat="server" ControlToValidate="txtName" ErrorMessage="Name is Required">*</asp:RequiredFieldValidator>
            </div> 
            <div class="form-group">
                <label for="txtFamilyName">Family Name :</label>
                <asp:TextBox ID="txtFamilyName" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="error-message" ID="RequiredFamilyNameValidator" runat="server" ControlToValidate="txtFamilyName" ErrorMessage="Family Name is Required">*</asp:RequiredFieldValidator>
                <asp:CompareValidator CssClass ="error-message" ID="CompareNameValidator" runat="server" ControlToValidate="txtName" ControlToCompare="txtFamilyName" Operator="NotEqual" ErrorMessage="Name and Family Name should not match"></asp:CompareValidator>
            </div>
            

            <div class="form-group">
                <label for="txtAddress">Address :</label>
                <asp:TextBox ID="txtAddress" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredAddressValidator" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is Required">*</asp:RequiredFieldValidator>
                <asp:CustomValidator CssClass="error-message" ID="MinLengthAddressValidator" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address must be at least 2 characters long" ValidateValue="ValidateMinLength">*</asp:CustomValidator>
            </div>
            <div class="form-group">
                <label for="txtCity">City :</label>
                <asp:TextBox ID="txtCity" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredCityValidator" runat="server" ControlToValidate="txtCity" ErrorMessage="City is Required">*</asp:RequiredFieldValidator>
                <asp:CustomValidator CssClass="error-message" ID="MinLengthCityValidator" runat="server" ControlToValidate="txtCity" ErrorMessage="City must be at least 2 characters long" ValidateValue="ValidateMinLength"></asp:CustomValidator>
            </div>
            <div class="form-group">
                <label for="txtZipCode">Zip Code :</label>
                <asp:TextBox ID="txtZipCode" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtZipCode" ErrorMessage="Zip Code is Required">*</asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator CssClass="error-message" ID="regexZipCode" runat="server" ControlToValidate="txtZipCode" ErrorMessage="Zip Code must be 5 digits" ValidationExpression="\d{5}"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label for="txtPhone">Phone :</label>
                <asp:TextBox ID="txtPhone" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone Number is Required">*</asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator CssClass="error-message" ID="PhoneFormatValidator" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number must be in XX-XXXXXXX or XXX-XXXXXXX format" ValidationExpression="^\d{2,3}-\d{7}$"></asp:RegularExpressionValidator>

            </div>

            <div class="form-group">
                <label for="txtEmail">E-Mail :</label>
                <asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is Required">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="error-message" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+"></asp:RegularExpressionValidator>
            </div>
            <br /><br />
            <div class="form-group">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                <asp:Button ID="submit_button" runat="server" Text="Submit" OnClick="Button1_Click" />
            </div>

        </div>
    </form>
</body>
</html>
