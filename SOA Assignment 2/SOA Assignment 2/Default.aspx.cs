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

namespace SOA_Assignment_2
{
    public partial class Default : System.Web.UI.Page
    {
        TcpClient mTcpClient;
        HttpWebRequest mHttpWebRequest;

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
            mTcpClient = new TcpClient();
            mHttpWebRequest = createFootballRequest();
            XmlDocument soapRequest = new XmlDocument();

            soapRequest.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <AllPlayerNames xmlns=""http://footballpool.dataaccess.eu"">
                      <bSelected>false</bSelected>
                    </AllPlayerNames>
                  </soap:Body>
                </soap:Envelope>");

            string result = getSoapResponse(soapRequest);
        }

        protected void topScorersSubmitButton_Click(object sender, EventArgs e)
        {
            mTcpClient = new TcpClient();
            mHttpWebRequest = createFootballRequest();
            XmlDocument soapRequest = new XmlDocument();

            soapRequest.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <TopGoalScorers xmlns=""http://footballpool.dataaccess.eu"">
                      <iTopN>" + topScorerPercentBox.Text.ToString() + @"</iTopN>
                    </TopGoalScorers>
                  </soap:Body>
                </soap:Envelope>");

            string result = getSoapResponse(soapRequest);
        }

        protected void stadiumNamesButton_Click(object sender, EventArgs e)
        {
            mTcpClient = new TcpClient();
            mHttpWebRequest = createFootballRequest();
            XmlDocument soapRequest = new XmlDocument();

            soapRequest.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <StadiumNames xmlns=""http://footballpool.dataaccess.eu"">
                    </StadiumNames>
                  </soap:Body>
                </soap:Envelope>");

            string result = getSoapResponse(soapRequest);
        }

        protected void getCountryNamesByNameButton_Click(object sender, EventArgs e)
        {
            mTcpClient = new TcpClient();
            mHttpWebRequest = createCountryInfoRequest();
            XmlDocument soapRequest = new XmlDocument();

            soapRequest.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <ListOfCountryNamesByName xmlns=""http://www.oorsprong.org/websamples.countryinfo"">
                    </ListOfCountryNamesByName>
                  </soap:Body>
                </soap:Envelope>");

            string result = getSoapResponse(soapRequest);
        }

        private HttpWebRequest createFootballRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://footballpool.dataaccess.eu/data/info.wso");
            webRequest.Headers.Add(@"SOAP:action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private HttpWebRequest createCountryInfoRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso");
            webRequest.Headers.Add(@"SOAP:action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private string getSoapResponse(XmlDocument soapRequest)
        {
            WebResponse response;
            string result;
            Stream requestStream = mHttpWebRequest.GetRequestStream();

            soapRequest.Save(requestStream);
            response = mHttpWebRequest.GetResponse();

            StreamReader sr = new StreamReader(response.GetResponseStream());
            result = sr.ReadToEnd();

            return result;
        }
    }
}