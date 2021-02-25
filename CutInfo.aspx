<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CutInfo.aspx.vb" Inherits="CutInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Cut Info</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8">

    <script src="js/Chart.js"></script>
    <script src="js/utils.js"></script>
    <script src="js/Chart.bundle.js"></script>
    <script src="js/analytics.js"></script>	

    <link rel="stylesheet" href="assets/css/main.css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <asp:Table ID="Table1" runat="server" Width ="900px" style="margin-left :auto ; margin-right :auto " ></asp:Table>
    </div>
    </form>
</body>
</html>
