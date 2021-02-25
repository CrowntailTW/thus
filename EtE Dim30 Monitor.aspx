<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EtE Dim30 Monitor.aspx.vb" Inherits="EtE_Dim30_Monitor" %>

<!DOCTYPE html>
<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8">
    <title>EtE Dim30 Monitor</title>

  
    <script type="text/javascript" src="/js/highcharts/exporting.js"></script>
    <script type="text/javascript"  src="/js/highcharts/highcharts.js" ></script>
    <script type="text/javascript"  src="/js/highcharts/boost.js" ></script>
  

    <link rel="stylesheet" href="assets/css/main1.css" />
    <!--[if lte IE 8]><link rel="stylesheet" href="/assets/css/ie8.css" /><![endif]-->

    <script type ="text/javascript" src="js/Chart.js"></script>    
    <script type ="text/javascript" src="js/Chart.min.js"></script>   
    
    <!-- 
    <script type ="text/javascript" src="js/Chart.bundle.js"></script>    
    <script type ="text/javascript" src="js/Chart.bundle.min.js"></script>
    -->
    
    <script type ="text/javascript" src="js/utils.js"></script>
    <script type ="text/javascript" src="js/analytics.js"></script>	

          
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
  
            <div  >
       
            <br />
                
        <table  border="1" style ="width : 100% ; height :auto ; margin-left :auto ;margin-right :auto  " >
            <tbody >
            
            <tr > 
                <td rowspan ="3" style ="text-align : right;width :10em " >
                    <br />
                    
                    <div style ="width :8em">From : 
                        <asp:TextBox ID="datepicker_S"  runat="server" Width="94px" align="center" ></asp:TextBox>
                    </div>
                    
                    <div style ="width :8em">To :
                        <asp:TextBox  ID="datepicker_E" runat="server" Width="94px"></asp:TextBox>
                    </div>
                    <div style ="width :8em">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="生成表格" />
                    </div>
                    </td>
                <th class="auto-style1"></th>
                <th colspan ="2" class="auto-style1">EtE model <br />Dim +-30 Ratio</th>
                <th class="auto-style1">Normal<br />Defect Ratio</th>
            </tr>
            <tr  align ="center"> 
                <th>By Line</th>
                <td>
                      Dim +/- 30 Ratio <br />
                    <asp:Button CssClass ="but" ID="EtE_Hourly_ByLine" runat="server" Text="Hourly" TabIndex="1"  />
                    <asp:Button CssClass ="but" ID="EtE_Daily_ByLine" runat="server" Text="Daily" TabIndex="2" />
                </td>
                <td  style ="vertical-align :middle ">
                    <!--
                    M240UAN02 有段差Ratio <br/> 
                    <asp:Button CssClass ="but" ID="Button_M24U2_段差" runat="server" Text="M24U2&#010;段差" TabIndex="99"  />                 
                    -->
                </td>
                <td>
                    Defect / Fail / Dim Ratio <br />
                    <asp:Button ID="Nor_Hourly_ByLine" runat="server" Text="Hourly" TabIndex="3" />
                    <asp:Button ID="Nor_Daily_ByLine" runat="server" Text="Daily" TabIndex="4"/>
                </td>
            </tr>
            <tr  align ="center"> 
                <th>By Model</th>
                <td>
                    Dim +/- 30 Ratio <br />
                    <asp:Button CssClass ="but" ID="EtE_Houly_model" runat="server" Text="Hourly" TabIndex="5"/>
                    <asp:Button CssClass ="but" ID="EtE_Daily_model" runat="server" Text="Daily" TabIndex="6" />
                </td>
                <td>
                    <!--
                    M240UAN02 考慮300~800後 Chipping Ratio <br/>
                    <asp:Button CssClass ="but"  ID="Button_M24U2Loose" runat="server" Text="M24U2chipping" TabIndex="98"  />                  
                    -->
                </td>
                <td>
                    (Defect + Fail) Ratio <br />
                    <asp:Button ID="Nor_Hour_model" runat="server" Text="Hourly" TabIndex="7" />
                    <asp:Button ID="Nor_Daily_model" runat="server" Text="Daily" TabIndex="8"/>
                </td>
            </tr>
            </tbody>
        </table>
          
        <asp:Table ID="Table2" runat="server"  style="margin-left :auto ; margin-right :auto " ></asp:Table>
        <br />
        <br />
        <asp:Table ID="Table1" runat="server"  style="margin-left :auto ; margin-right :auto "></asp:Table>
        <br />
    
        </div>
        
    </form>

</body>
</html>

