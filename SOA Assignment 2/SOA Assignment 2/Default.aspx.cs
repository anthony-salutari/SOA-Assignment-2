using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SOA_Assignment_2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            footballDiv.Visible = false;
            countryInformationDiv.Visible = false;
            movieInformationDiv.Visible = false;
            calculatorDiv.Visible = false;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(serviceDropdown.SelectedIndex == 1)
            {
                footballDiv.Visible = true;
                countryInformationDiv.Visible = false;
                movieInformationDiv.Visible = false;
                calculatorDiv.Visible = false;
            }
            else if(serviceDropdown.SelectedIndex == 2)
            {
                countryInformationDiv.Visible = true;
                footballDiv.Visible = false;
                movieInformationDiv.Visible = false;
                calculatorDiv.Visible = false;
            }
            else if(serviceDropdown.SelectedIndex == 3)
            {
                movieInformationDiv.Visible = true;
                footballDiv.Visible = false;
                countryInformationDiv.Visible = false;
                calculatorDiv.Visible = false;
            }
            else if(serviceDropdown.SelectedIndex == 4)
            {
                calculatorDiv.Visible = true;
                footballDiv.Visible = false;
                countryInformationDiv.Visible = false;
                movieInformationDiv.Visible = false;
            }
        }
    }
}