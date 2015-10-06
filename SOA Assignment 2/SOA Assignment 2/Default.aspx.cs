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
            footballDiv.Visible = true;
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

            footballResultsBox.Text = "";

            foreach (string player in playerNames)
            {
                footballResultsBox.Text += player;
                footballResultsBox.Text += Environment.NewLine;
            }
        }

        protected void topScorersSubmitButton_Click(object sender, EventArgs e)
        {
            footballDiv.Visible = true;
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

            footballResultsBox.Text = "";

            foreach (string player in playerNames)
            {
                footballResultsBox.Text += player;
                footballResultsBox.Text += Environment.NewLine;
            }
        }

        protected void stadiumNamesButton_Click(object sender, EventArgs e)
        {
            footballDiv.Visible = true;
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

            footballResultsBox.Text = "";

            foreach (string stadium in stadiumNames)
            {
                footballResultsBox.Text += stadium;
                footballResultsBox.Text += Environment.NewLine;
            }
        }

        protected void getStadiumInfoButton_Click(object sender, EventArgs e)
        {
            footballDiv.Visible = true;
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = footballServiceURL;
            string webServiceName = footballServiceName;
            string webMethodName = "StadiumInfo";
            object[] arguments = { stadiumNameBox.Text.ToString() };

            string stadiumName = null;
            int seatCapacity = 0;
            string cityName = null;
            string wikiLink = null;
            string mapLink = null;

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
                        stadiumName = child.InnerText;
                    }
                    else if (child.Name == "iSeatsCapacity")
                    {
                        seatCapacity = Convert.ToInt32(child.InnerText);
                    }
                    else if (child.Name == "sCityName")
                    {
                        cityName = child.InnerText;
                    }
                    else if (child.Name == "sWikipediaURL")
                    {
                        wikiLink = child.InnerText;
                    }
                    else if (child.Name == "sGoogleMapsURL")
                    {
                        mapLink = child.InnerText;
                    }
                }
            }
            
            footballResultsBox.Text = "";

            if (stadiumName != null && seatCapacity != 0 && cityName != null && wikiLink != null && mapLink != null)
            {
                footballResultsBox.Text += "Stadium name: " + stadiumName;
                footballResultsBox.Text += Environment.NewLine;
                footballResultsBox.Text += "Seats capacity: " + seatCapacity.ToString();
                footballResultsBox.Text += Environment.NewLine;
                footballResultsBox.Text += "City name: " + cityName;
                footballResultsBox.Text += Environment.NewLine;
                footballResultsBox.Text += "Wikipedia URL: " + wikiLink;
                footballResultsBox.Text += Environment.NewLine;
                footballResultsBox.Text += "Google map link: " + mapLink;
            }
        }

        protected void getTeamInfoButton_Click(object sender, EventArgs e)
        {
            footballDiv.Visible = true;
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

            footballResultsBox.Text = "";

            for (int i = 0; i < name.Count; i++)
            {
                footballResultsBox.Text += "Team name: " + name[i];
                footballResultsBox.Text += Environment.NewLine;
                footballResultsBox.Text += "Wikipedia link: " + wikiLink[i];
                footballResultsBox.Text += Environment.NewLine;
                footballResultsBox.Text += "Flag Link: " + flagLink[i];
                footballResultsBox.Text += Environment.NewLine;
            }
        }

        protected void getCountryNamesByNameButton_Click(object sender, EventArgs e)
        {
            countryInformationDiv.Visible = true;
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = countryInfoServiceURL;
            string webServiceName = countryInfoServiceName;
            string webMethodName = "ListOfCountryNamesByName";
            object[] arguments = null;

            ArrayList isoCodes = new ArrayList();
            ArrayList countryNames = new ArrayList();

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("tCountryCodeAndName");

            foreach (XmlNode node in nodes)
            {
                XmlNodeList children = node.ChildNodes;

                foreach (XmlNode child in children)
                {
                    if (child.Name == "sISOCode")
                    {
                        isoCodes.Add(child.InnerText);
                    }
                    else if (child.Name == "sName")
                    {
                        countryNames.Add(child.InnerText);
                    }
                }
            }

            countryInfoResultsBox.Text = "";

            for (int i = 0; i < isoCodes.Count; i++)
            {
                countryInfoResultsBox.Text += "Iso code: " + isoCodes[i];
                countryInfoResultsBox.Text += Environment.NewLine;
                countryInfoResultsBox.Text += "Country name: " + countryNames[i];
                countryInfoResultsBox.Text += Environment.NewLine;
            }
        }

        protected void capitalByCountryCodeButton_Click(object sender, EventArgs e)
        {
            countryInformationDiv.Visible = true;
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = countryInfoServiceURL;
            string webServiceName = countryInfoServiceName;
            string webMethodName = "CapitalCity";
            object[] arguments = { capitalCodeBox.Text.ToString() };

            string capitalCity = null;

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("string");

            foreach (XmlNode node in nodes)
            {
                capitalCity = node.InnerText;
            }

            countryInfoResultsBox.Text = "";

            if (capitalCity != null)
            {
                countryInfoResultsBox.Text = capitalCity;
            }
        }

        protected void currenciesByCountryCodeButton_Click(object sender, EventArgs e)
        {
            countryInformationDiv.Visible = true;
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = countryInfoServiceURL;
            string webServiceName = countryInfoServiceName;
            string webMethodName = "ListOfCurrenciesByCode";
            object[] arguments = null; 

            ArrayList isoCodes = new ArrayList();
            ArrayList currencyNames = new ArrayList();

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("tCurrency");

            foreach (XmlNode node in nodes)
            {
                XmlNodeList children = node.ChildNodes;

                foreach (XmlNode child in children)
                {
                    if (child.Name == "sISOCode")
                    {
                        // check if there is an iso code to prevent an exception
                        if (child.InnerText != "")
                        {
                            isoCodes.Add(child.InnerText);
                        }
                        else
                        {
                            isoCodes.Add("N/A");
                        }
                    }
                    else if (child.Name == "sName")
                    {
                        currencyNames.Add(child.InnerText);
                    }
                }
            }

            countryInfoResultsBox.Text = "";

            for (int i = 0; i < isoCodes.Count; i++)
            {
                countryInfoResultsBox.Text += "ISO code: " + isoCodes[i];
                countryInfoResultsBox.Text += Environment.NewLine;
                countryInfoResultsBox.Text += "Currency: " + currencyNames[i];
                countryInfoResultsBox.Text += Environment.NewLine;
            }
        }

        protected void isoForCountryButton_Click(object sender, EventArgs e)
        {
            countryInformationDiv.Visible = true;
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = countryInfoServiceURL;
            string webServiceName = countryInfoServiceName;
            string webMethodName = "CountryISOCode";
            object[] arguments = { isoForCountryBox.Text.ToString() };

            string isoCode = null;

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("string");

            foreach (XmlNode node in nodes)
            {
                isoCode = node.InnerText;
            }

            countryInfoResultsBox.Text = "";

            if (isoCode != null)
            {
                countryInfoResultsBox.Text += isoCode;
            }
        }

        protected void theatresAndMoviesButton_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = movieInfoServiceURL;
            string webServiceName = movieInfoServiceName;
            string webMethodName = "GetTheatresAndMovies";
            object[] arguments = { zipCodeMovieBox.Text.ToString(),
                Convert.ToInt32(radiusMovieBox.Text.ToString()) };
            int i = 0;
            string[] movieList = null;

            Dictionary<KeyValuePair<string, string>, string[]> theatres = new Dictionary<KeyValuePair<string, string>, string[]>();

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);
            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("Theater");

            foreach (XmlNode node in nodes)
            {
                XmlNodeList children = node.ChildNodes;

                foreach (XmlNode child in children)
                {        
                    if (child.Name == "Name")
                    {
                        string theaterName = child.InnerText;   
                    }
                    else if (child.Name == "Address")
                    {
                        string theaterAddress = child.InnerText;
                    }
                    else if (child.Name == "Movie")
                    {
                        i++;
                        movieList[i] = child.InnerText;
                    }
                }
            }
        }

        protected void upcomingMoviesButton_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = movieInfoServiceURL;
            string webServiceName = movieInfoServiceName;
            string webMethodName = "GetUpcomingMovies";
            object[] arguments = { Convert.ToInt32(monthMovieBox.Text.ToString()),
                Convert.ToInt32(yearMovieBox.Text.ToString()) };

            ArrayList upcomingMovies = new ArrayList();

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);

            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("MovieName");

            foreach (XmlNode node in nodes)
            {
                upcomingMovies.Add(node.InnerText);
            }
        }

        protected void convertToFahrenheitButton_Click(object sender, EventArgs e)
        {
            tempConvertDiv.Visible = true;
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = tempConvertServiceURL;
            string webServiceName = tempConvertServiceName;
            string webMethodName = "CelsiusToFahrenheit";
            object[] arguments = { celciusBox.Text.ToString() };

            string result = null;

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);

            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("string");

            foreach (XmlNode node in nodes)
            {
                result = node.InnerText;
            }

            tempConvertResultBox.Text = "";

            if (result != null)
            {
                tempConvertResultBox.Text = result;
            }
        }

        protected void convertToCelciusButton_Click(object sender, EventArgs e)
        {
            tempConvertDiv.Visible = true;
            XmlDocument xmlDoc = new XmlDocument();
            string webServiceURL = tempConvertServiceURL;
            string webServiceName = tempConvertServiceName;
            string webMethodName = "FahrenheitToCelcius";
            object[] arguments = { fahrenheitBox.Text.ToString() };

            string result = null;

            object results = getResult(webServiceURL, webServiceName, webMethodName, arguments);
            string xdoc = SerializeToXml(results);

            xmlDoc.LoadXml(xdoc);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("string");

            foreach (XmlNode node in nodes)
            {
                result = node.InnerText;
            }

            tempConvertResultBox.Text = "";

            if (result != null)
            {
                tempConvertResultBox.Text = result;
            }
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