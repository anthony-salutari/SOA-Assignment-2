using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace SOA_Assignment_2
{
    public partial class Default : System.Web.UI.Page
    {
        WebServiceCaller webServiceCaller;

        // football service consts
        public const string footballServiceURL = "http://footballpool.dataaccess.eu/data/info.wso";
        public const string footballServiceName = "Worldcup 2010 Football Championship";
        // country info service consts
        public const string countryInfoServiceURL = "http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso";
        public const string countryInfoServiceName = "Country Info Service";
        // movie info service consts
        public const string movieInfoServiceURL = "http://www.ignyte.com/webservices/ignyte.whatsshowing.webservice/moviefunctions.asmx";
        public const string movieInfoServiceName = "Movie Information Service";
        // calculator service consts
        public const string calculatorServiceURL = "http://ws1.parasoft.com/glue/calculator";
        public const string calculatorServiceName = "Calculator";

        protected void Page_Load(object sender, EventArgs e)
        {
            footballDiv.Visible = false;
            countryInformationDiv.Visible = false;
            movieInformationDiv.Visible = false;
            calculatorDiv.Visible = false;
        }

        protected void serviceDropdown_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void getAllPlayersButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "AllPlayerNames";
            object[] arguments = { false };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void topScorersSubmitButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "TopGoalScorers";
            object[] arguments = { topScorerPercentBox.Text.ToString() };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void stadiumNamesButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "StadiumNames";
            object[] arguments = null;

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void getStadiumInfoButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "StadiumInfo";
            object[] arguments = { stadiumNameBox.Text.ToString() };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void getTeamInfoButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "Teams";
            object[] arguments = null;

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void getCountryNamesByNameButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = countryInfoServiceURL;
            string webServiceName = countryInfoServiceName;
            string webMethodName = "ListOfCountryNamesByName";
            object[] arguments = null;

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void capitalByCountryCodeButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = countryInfoServiceURL;
            string webServiceName = countryInfoServiceName;
            string webMethodName = "CapitalCity";
            object[] arguments = { capitalCodeBox.Text.ToString() };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void currenciesByCountryCodeButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = countryInfoServiceURL;
            string webServiceName = countryInfoServiceName;
            string webMethodName = "ListOfCurrenciesByCode";
            object[] arguments = { currenciesCodeBox.Text.ToString() };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void isoForCountryButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = countryInfoServiceURL;
            string webServiceName = countryInfoServiceName;
            string webMethodName = "CountryISOCode";
            object[] arguments = { isoForCountryBox.Text.ToString() };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void theatresAndMoviesButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = movieInfoServiceURL;
            string webServiceName = movieInfoServiceName;
            string webMethodName = "GetTheatresAndMovies";
            object[] arguments = { zipCodeMovieBox.Text.ToString(),
                Convert.ToInt32(radiusMovieBox.Text.ToString()) };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void upcomingMoviesButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = movieInfoServiceURL;
            string webServiceName = movieInfoServiceName;
            string webMethodName = "GetUpcomingMovies";
            object[] arguments = { Convert.ToInt32(monthMovieBox.Text.ToString()),
                Convert.ToInt32(yearMovieBox.Text.ToString()) };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = calculatorServiceURL;
            string webServiceName = calculatorServiceName;
            string webMethodName = "add";
            object[] arguments = { float.Parse(firstCalcNumberBox.Text.ToString()),
                float.Parse(secondCalcNumberBox.Text.ToString()) };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void divideButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = calculatorServiceURL;
            string webServiceName = calculatorServiceName;
            string webMethodName = "divide";
            object[] arguments = { float.Parse(firstCalcNumberBox.Text.ToString()),
                float.Parse(secondCalcNumberBox.Text.ToString()) };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void multiplyButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = calculatorServiceURL;
            string webServiceName = calculatorServiceName;
            string webMethodName = "multiply";
            object[] arguments = { float.Parse(firstCalcNumberBox.Text.ToString()),
                float.Parse(secondCalcNumberBox.Text.ToString()) };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void subtractButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = calculatorServiceURL;
            string webServiceName = calculatorServiceName;
            string webMethodName = "subtract";
            object[] arguments = { float.Parse(firstCalcNumberBox.Text.ToString()),
                float.Parse(secondCalcNumberBox.Text.ToString()) };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        public object getResult(string webServiceURL, string webServiceName, string webMethodName, object[] arguments)
        {
            webServiceCaller = new WebServiceCaller();
            object results;

            results = webServiceCaller.CallWebMethod(webServiceURL, webServiceName, webMethodName, arguments);

            return results;
        }

        public string SerializeToXml(object input)
        {
            XmlSerializer ser = new XmlSerializer(input.GetType());
            string result = string.Empty;

            using (MemoryStream memStm = new MemoryStream())
            {
                ser.Serialize(memStm, input);

                memStm.Position = 0;
                result = new StreamReader(memStm).ReadToEnd();
            }

            return result;
        }
    }
}