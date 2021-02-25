<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Mileage.aspx.vb" Inherits="CutInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Mileage Info</title>

    <link rel="stylesheet" href="assets/css/main1.css" />
    <!--[if lte IE 8]><link rel="stylesheet" href="/assets/css/ie8.css" /><![endif]-->

    <script type ="text/javascript" src="js/Chart.js"></script>    
    <script type ="text/javascript" src="js/Chart.min.js"></script>    
    <script type ="text/javascript" src="js/Chart.bundle.js"></script>    
    <script type ="text/javascript" src="js/Chart.bundle.min.js"></script>
    <script type ="text/javascript" src="js/utils.js"></script>
    <script type ="text/javascript" src="js/analytics.js"></script>	

    <link rel="stylesheet" href="assets/css/main.css" />

    <!-- datepicker-->
    <link type="text/css" href="assets/css/overcast/jquery-ui-1.9.2.custom.css" rel="stylesheet" />    
    <script type="text/javascript" src="js/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.9.2.custom.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.9.2.custom.min.js"></script>
    <!--<script type="text/javascript" src="js/jquery.ui.datepicker-zh-TW.js"></script>    -->
    <script  type="text/javascript" >
      $(function () {
          $("#datepicker_S").datepicker();
          $("#datepicker_E").datepicker();
      });
    </script>
    <!-- datepicker-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table >
            <tr>
                <td>
                  
                    <br />
                    
                    <div style ="width :8em">From : 
                        <asp:TextBox ID="datepicker_S"  runat="server" Width="94px" align="center" ></asp:TextBox>
                    </div>
                    
                    <div style ="width :8em">To :
                        <asp:TextBox  ID="datepicker_E" runat="server" Width="94px"></asp:TextBox>
                    </div>
                    <div style ="width :8em">
                        <asp:Button ID="Button1" runat="server" Text="Button" />
                    </div>
                   
                </td>
            </tr>
        </table>
        <div >
            
            <asp:Table ID="Table1" runat="server" style="margin-left :auto ; margin-right :auto " ></asp:Table>
        </div>
        
        
    </div>
    </form>
</body>
</html>
