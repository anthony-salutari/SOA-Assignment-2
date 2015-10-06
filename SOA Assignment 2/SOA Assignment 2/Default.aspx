<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SOA_Assignment_2.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SOA Assignment 2</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="titleDiv">
        <h1>SOA Assignment 2</h1>
    </div>

    <div id="serviceSelection" runat="server">
        <h2>Select a service:</h2>
            <asp:DropDownList ID="serviceDropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="serviceDropdown_SelectedIndexChanged">
                <asp:ListItem>Choose a service...</asp:ListItem>
                <asp:ListItem>Worldcup 2010 Football Championship</asp:ListItem>
                <asp:ListItem>Country Information Web Service</asp:ListItem>
                <asp:ListItem>Movie Information</asp:ListItem>
                <asp:ListItem>Calculator</asp:ListItem>
            </asp:DropDownList>
    </div>

    <div id="footballDiv" runat="server">
        <h2>Worldcup 2010 Football Championship</h2>
        <br />
        <asp:Button ID="getAllPlayersButton" runat="server" Text="Get All Player Names" OnClick="getAllPlayersButton_Click" />
        <br />
        <br />
        
        Get top <asp:TextBox ID="topScorerPercentBox" runat="server" Width="24px" MaxLength="3" TextMode="Number">0</asp:TextBox>
&nbsp;% goal scorers&nbsp;&nbsp;
        
        <asp:Button ID="topScorersSubmitButton" runat="server" Text="Submit" OnClick="topScorersSubmitButton_Click" />
        <br />
        <br />
        <asp:Button ID="stadiumNamesButton" runat="server" Text="Get Stadium Names" OnClick="stadiumNamesButton_Click" />
        <br />
        <br />
        <asp:TextBox ID="stadiumNameBox" runat="server">Stadium name</asp:TextBox>
&nbsp;<asp:Button ID="getStadiumInfoButton" runat="server" Text="Get Stadium Info" OnClick="getStadiumInfoButton_Click" />
        <br />
        <br />
        <asp:Button ID="getTeamInfoButton" runat="server" Text="Get Team Info" OnClick="getTeamInfoButton_Click" />
        <br />
        <br />

    </div>

    <div id="countryInformationDiv" runat="server">
        <h2>Country Information Service</h2>
        <asp:Button ID="getCountryNamesByNameButton" runat="server" Text="Get List Of Country Names By Name" OnClick="getCountryNamesByNameButton_Click" />
        <br />
        <br />
        Get capital by country code
        <asp:TextBox ID="capitalCodeBox" runat="server" Width="34px" MaxLength="3">CA</asp:TextBox>
&nbsp;<asp:Button ID="capitalByCountryCodeButton" runat="server" Text="Submit" OnClick="capitalByCountryCodeButton_Click" />
        <br />
        <br />
        Get currencies by country code
        <asp:TextBox ID="currenciesCodeBox" runat="server" Width="30px" MaxLength="3">CA</asp:TextBox>
&nbsp;<asp:Button ID="currenciesByCountryCodeButton" runat="server" Text="Submit" OnClick="currenciesByCountryCodeButton_Click" />
        <br />
        <br />
        Find ISO for country:
        <asp:TextBox ID="isoForCountryBox" runat="server">Canada</asp:TextBox>
&nbsp;<asp:Button ID="isoForCountryButton" runat="server" Text="Submit" OnClick="isoForCountryButton_Click" />
        <br />
        <br />
    </div>

    <div id="movieInformationDiv" runat="server">
        <h2>Movie Information</h2>
        Get theatres and movies: zip code
        <asp:TextBox ID="zipCodeMovieBox" runat="server" Width="42px" MaxLength="5" TextMode="Number">90210</asp:TextBox>
&nbsp;radius
        <asp:TextBox ID="radiusMovieBox" runat="server" Width="41px" TextMode="Number">50</asp:TextBox>
&nbsp;<asp:Button ID="theatresAndMoviesButton" runat="server" Text="Submit" OnClick="theatresAndMoviesButton_Click" />
        <br />
        <br />
        Get upcoming movies: month
        <asp:TextBox ID="monthMovieBox" runat="server" Width="25px" MaxLength="2" TextMode="Number">11</asp:TextBox>
&nbsp;year
        <asp:TextBox ID="yearMovieBox" runat="server" Width="31px" MaxLength="4" TextMode="Number">2015</asp:TextBox>
&nbsp;<asp:Button ID="upcomingMoviesButton" runat="server" Text="Submit" OnClick="upcomingMoviesButton_Click" />
        <br />
        <br />

    </div>

    <div id="calculatorDiv" runat="server">
        <h2>Calculator</h2>
        <asp:TextBox ID="firstCalcNumberBox" runat="server" TextMode="Number">1</asp:TextBox>
&nbsp;<asp:TextBox ID="secondCalcNumberBox" runat="server" TextMode="Number">1</asp:TextBox>
        <br />
        <br />
        <asp:Button ID="addButton" runat="server" Text="+" OnClick="addButton_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="divideButton" runat="server" Text="/" OnClick="divideButton_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="multiplyButton" runat="server" Text="*" OnClick="multiplyButton_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="subtractButton" runat="server" Text="-" OnClick="subtractButton_Click" />
        <br />
    </div>

    </form>
</body>
</html>
