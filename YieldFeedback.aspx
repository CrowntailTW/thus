<%@ Page Language="VB" AutoEventWireup="false" CodeFile="YieldFeedback.aspx.vb" Inherits="YieldFeedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        
        我也有話要說<br />
        <br />
        
        <asp:TextBox ID="TextBox1" runat="server" Height="200px" TextMode="MultiLine" Width="500px"></asp:TextBox>
        <br />
        <asp:Button ID="Submit" runat="server" Text="Submit" />
    
    </div>
    </form>
</body>
</html>
