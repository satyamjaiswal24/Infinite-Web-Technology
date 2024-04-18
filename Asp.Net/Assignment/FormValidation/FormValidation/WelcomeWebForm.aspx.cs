using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FormValidation
{
    public partial class WelcomeWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve data from query string
                string name = Request.QueryString["Name"];
                string familyName = Request.QueryString["FamilyName"];
                string address = Request.QueryString["Address"];
                string city = Request.QueryString["City"];
                string zipCode = Request.QueryString["ZipCode"];
                string phone = Request.QueryString["Phone"];
                string email = Request.QueryString["Email"];

                // Display data in labels
                lblName.Text = "Name: " + name;
                lblFamilyName.Text = "Family Name: " + familyName;
                lblAddress.Text = "Address: " + address;
                lblCity.Text = "City: " + city;
                lblZipCode.Text = "Zip Code: " + zipCode;
                lblPhone.Text = "Phone: " + phone;
                lblEmail.Text = "Email: " + email;
            }
        }
    }
}