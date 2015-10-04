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
            <asp:DropDownList ID="serviceDropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
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
        <asp:Button ID="Button1" runat="server" Text="Get All Player Names" />
        <br />
        <br />
        
        Get top <asp:TextBox ID="TextBox3" runat="server" Width="24px" TextMode="Number">0</asp:TextBox>
&nbsp;% goal scorers&nbsp;&nbsp;
        
        <asp:Button ID="Button2" runat="server" Text="Submit" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="Get Stadium Names" />
        <br />
        <br />
        <asp:TextBox ID="TextBox2" runat="server">Stadium name</asp:TextBox>
&nbsp;<asp:Button ID="Button4" runat="server" Text="Get Stadium Info" />
        <br />
        <br />
        <asp:Button ID="Button5" runat="server" Text="Get Team Info" />
        <br />

    </div>

    <div id="countryInformationDiv" runat="server">
        <h2>Country Information Service</h2>
        <asp:Button ID="Button6" runat="server" Text="Get List Of Country Names By Name" />
        <br />
        <br />
        Get capital by country code
        <asp:TextBox ID="TextBox4" runat="server" Width="22px">CA</asp:TextBox>
&nbsp;<asp:Button ID="Button7" runat="server" Text="Submit" />
        <br />
        <br />
        Get currencies by country code
        <asp:TextBox ID="TextBox5" runat="server" Width="19px">CA</asp:TextBox>
&nbsp;<asp:Button ID="Button8" runat="server" Text="Submit" />
        <br />
        <br />
        Find ISO for country:
        <asp:TextBox ID="TextBox6" runat="server">Canada</asp:TextBox>
&nbsp;<asp:Button ID="Button9" runat="server" Text="Submit" />
        <br />
        <br />
    </div>

    <div id="movieInformationDiv" runat="server">
        <h2>Movie Information</h2>
        Get theatres and movies: zip code
        <asp:TextBox ID="TextBox7" runat="server" Width="42px" TextMode="Number">90210</asp:TextBox>
&nbsp;radius
        <asp:TextBox ID="TextBox8" runat="server" Width="41px" TextMode="Number">50</asp:TextBox>
&nbsp;<asp:Button ID="Button10" runat="server" Text="Submit" />
        <br />
        <br />
        Get upcoming movies: month
        <asp:TextBox ID="TextBox9" runat="server" Width="25px" TextMode="Number">11</asp:TextBox>
&nbsp;year
        <asp:TextBox ID="TextBox10" runat="server" Width="31px" TextMode="Number">2015</asp:TextBox>
&nbsp;<asp:Button ID="Button11" runat="server" Text="Submit" />
        <br />

    </div>

    <div id="calculatorDiv" runat="server">
        <h2>Calculator</h2>
        <asp:TextBox ID="TextBox11" runat="server" TextMode="Number">1</asp:TextBox>
&nbsp;<asp:TextBox ID="TextBox12" runat="server" TextMode="Number">1</asp:TextBox>
        <br />
        <br />
        <asp:Button ID="addButton" runat="server" Text="+" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="divideButton" runat="server" Text="/" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="multiplyButton" runat="server" Text="*" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="subtractButton" runat="server" Text="-" />
    </div>

    </form>
</body>
</html>
