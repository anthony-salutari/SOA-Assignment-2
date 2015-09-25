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
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                <asp:ListItem>Worldcup 2010 Football Championship</asp:ListItem>
                <asp:ListItem>Country Information Web Service</asp:ListItem>
                <asp:ListItem>Bible - King James Version</asp:ListItem>
            </asp:DropDownList>
    </div>

    <div id="footballDiv" runat="server">
        <h2>Worldcup 2010 Football Championship</h2>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Get All Player Names" />
        <br />
        <br />
        
        Get top <asp:TextBox ID="TextBox3" runat="server" Width="24px">0</asp:TextBox>
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
        Get capitol by country code
        <asp:TextBox ID="TextBox4" runat="server" Width="22px">CA</asp:TextBox>
&nbsp;<asp:Button ID="Button7" runat="server" Text="Submit" />
        <br />
    </div>

    <div id="bibleDiv" runat="server">

    </div>

    </form>
</body>
</html>
