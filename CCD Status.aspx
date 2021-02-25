<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CCD Status.aspx.vb" Inherits="CCD_Monitor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=8">
    <title>CCD Status</title>
    <link rel="stylesheet" href="assets/css/main.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left :auto ; margin-right :auto ">
        <br />
        <asp:Button ID="Button1" runat="server" Text="Refresh" Visible="False" />
        <div  style="margin-left :auto ;margin-right :auto ;text-align :center   ">
        <asp:Label ID="Label1" runat="server" Text=" " style="margin-left :auto ; margin-right :auto  "></asp:Label>
            <br />
        <asp:Label ID="Label2" runat="server" Text=" " style="margin-left :auto ; margin-right :auto  "></asp:Label>
        </div>
  
        <asp:Table ID="Table1" runat="server" Width ="85%" style="margin-left :auto ; margin-right :auto "></asp:Table>

    </div>
    
    </form>
</body>
</html>
