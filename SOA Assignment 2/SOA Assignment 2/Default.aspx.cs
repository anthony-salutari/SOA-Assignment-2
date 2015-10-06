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
using System.Collections;

namespace SOA_Assignment_2
{
    public partial class Default : System.Web.UI.Page
    {
        WebServiceCaller webServiceCaller;

        // football service consts
        public const string footballServiceURL = "http://footballpool.dataaccess.eu/data/info.wso";
        public const string footballServiceName = "Info";
        // country info service consts
        public const string countryInfoServiceURL = "http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso";
        public const string countryInfoServiceName = "CountryInfoService";
        // movie info service consts
        public const string movieInfoServiceURL = "http://www.ignyte.com/webservices/ignyte.whatsshowing.webservice/moviefunctions.asmx";
        public const string movieInfoServiceName = "MovieInformation";
        // temp convert service consts
        public const string tempConvertServiceURL = "http://www.w3schools.com/webservices/tempconvert.asmx";
        public const string tempConvertServiceName = "TempConvert";

        protected void Page_Load(object sender, EventArgs e)
        {
            footballDiv.Visible = false;
            countryInformationDiv.Visible = false;
            movieInformationDiv.Visible = false;
            tempConvertDiv.Visible = false;
        }

        protected void serviceDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(serviceDropdown.SelectedIndex == 1)
            {
                footballDiv.Visible = true;
                countryInformationDiv.Visible = false;
                movieInformationDiv.Visible = false;
                tempConvertDiv.Visible = false;
            }
            else if(serviceDropdown.SelectedIndex == 2)
            {
                countryInformationDiv.Visible = true;
                footballDiv.Visible = false;
                movieInformationDiv.Visible = false;
                tempConvertDiv.Visible = false;
            }
            else if(serviceDropdown.SelectedIndex == 3)
            {
                movieInformationDiv.Visible = true;
                footballDiv.Visible = false;
                countryInformationDiv.Visible = false;
                tempConvertDiv.Visible = false;
            }
            else if(serviceDropdown.SelectedIndex == 4)
            {
                tempConvertDiv.Visible = true;
                footballDiv.Visible = false;
                countryInformationDiv.Visible = false;
                movieInformationDiv.Visible = false;
            }
        }

        protected void getAllPlayersButton_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "AllPlayerNames";
            object[] arguments = { false };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("tPlayerNames");

            ArrayList playerNames = new ArrayList();

            foreach (XmlNode node in nodes)
            {
                XmlNodeList children = node.ChildNodes;

                foreach(XmlNode child in children)
                {
                    if (child.Name == "sName")
                    {
                        playerNames.Add(child.InnerText);
                    }
                }
            }
        }

        protected void topScorersSubmitButton_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "TopGoalScorers";
            object[] arguments = { Convert.ToInt32(topScorerPercentBox.Text.ToString()) };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("tTopGoalScorer");

            ArrayList playerNames = new ArrayList();

            foreach (XmlNode node in nodes)
            {
                XmlNodeList children = node.ChildNodes;

                foreach (XmlNode child in children)
                {
                    if (child.Name == "sName")
                    {
                        playerNames.Add(child.InnerText);
                    }
                }
            }
        }

        protected void stadiumNamesButton_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "StadiumNames";
            object[] arguments = null;

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
            xmlDoc.LoadXml(xdoc);
            ArrayList stadiumNames = new ArrayList();

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("string");

            foreach (XmlNode node in nodes)
            {
                stadiumNames.Add(node.InnerText);
            }
        }

        protected void getStadiumInfoButton_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "StadiumInfo";
            object[] arguments = { stadiumNameBox.Text.ToString() };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
            xmlDoc.LoadXml(xdoc);

            XmlNodeList stadiumInfo = xmlDoc.GetElementsByTagName("tStadiumInfo");

            foreach (XmlNode node in stadiumInfo)
            {
                XmlNodeList children = node.ChildNodes;

                foreach (XmlNode child in children)
                {
                    if (child.Name == "sStadiumName")
                    {
                        string stadiumName = child.InnerText;
                    }
                    else if (child.Name == "iSeatsCapacity")
                    {
                        int seatCapacity = Convert.ToInt32(child.InnerText);
                    }
                    else if (child.Name == "sCityName")
                    {
                        string cityName = child.InnerText;
                    }
                    else if (child.Name == "sWikipediaURL")
                    {
                        string wikiLink = child.InnerText;
                    }
                    else if (child.Name == "sGoogleMapsURL")
                    {
                        string mapLink = child.InnerText;
                    }
                }
            }
        }

        protected void getTeamInfoButton_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "Teams";
            object[] arguments = null;

            ArrayList name = new ArrayList();
            ArrayList wikiLink = new ArrayList();
            ArrayList flagLink = new ArrayList();

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("tTeamInfo");

            foreach (XmlNode node in nodes)
            {
                XmlNodeList children = node.ChildNodes;

                foreach (XmlNode child in children)
                {
                    if (child.Name == "sName")
                    {
                        name.Add(child.InnerText);
                    }
                    else if (child.Name == "sWikipediaURL")
                    {
                        wikiLink.Add(child.InnerText);
                    }
                    else if (child.Name == "sCountryFlagLarge")
                    {
                        flagLink.Add(child.InnerText);
                    }
                }
            }
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

        protected void convertToFahrenheitButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = tempConvertServiceURL;
            string webServiceName = tempConvertServiceName;
            string webMethodName = "CelsiusToFahrenheit";
            object[] arguments = { celciusBox.Text.ToString() };

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
        }

        protected void convertToCelciusButton_Click(object sender, EventArgs e)
        {
            string webServiceURL = tempConvertServiceURL;
            string webServiceName = tempConvertServiceName;
            string webMethodName = "FahrenheitToCelcius";
            object[] arguments = { fahrenheitBox.Text.ToString() };

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