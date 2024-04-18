using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FormValidation
{
    public partial class ValidationForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ValidateName(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string name = txtName.Text.Trim();
            string familyName = txtFamilyName.Text.Trim();

            // Validate that name is different from family name
            args.IsValid = !name.Equals(familyName, StringComparison.OrdinalIgnoreCase);
        }

        protected void ValidateAddress(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string address = txtAddress.Text.Trim();

            // Validate that address has at least 2 letters
            args.IsValid = address.Length >= 2;
        }

        protected void ValidateCity(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string city = txtCity.Text.Trim();

            // Validate that city has at least 2 letters
            args.IsValid = city.Length >= 2;
        }

        protected void ValidateZipCode(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string zipCode = txtZipCode.Text.Trim();

            // Validate that zip-code has 5 digits
            args.IsValid = Regex.IsMatch(zipCode, @"^\d{5}$");
        }

        protected void ValidatePhone(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string phone = txtPhone.Text.Trim();

            // Validate that phone matches the format XX-XXXXXXX or XXX-XXXXXXX
            args.IsValid = Regex.IsMatch(phone, @"^\d{2,3}-\d{7}$");
        }

        protected void ValidateEmail(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string email = txtEmail.Text.Trim();

            // Validate that email is a valid email address
            args.IsValid = IsValidEmail(email);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Validation successful, redirect to new HTML page
                string name = txtName.Text;
                string familyName = txtFamilyName.Text;
                string address = txtAddress.Text;
                string city = txtCity.Text;
                string zipCode = txtZipCode.Text;
                string phone = txtPhone.Text;
                string email = txtEmail.Text;

                // Redirect to welcome page
                Response.Redirect("WelcomeWebForm.aspx?Name=" + name + "&FamilyName=" + familyName + "&Address=" + address + "&City=" + city + "&ZipCode=" + zipCode + "&Phone=" + phone + "&Email=" + email);
            }
            else
            {
                // Validation failed, stay on the current page
            }
        }
    }
}
