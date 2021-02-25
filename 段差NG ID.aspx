<%@ Page Language="VB" AutoEventWireup="false" CodeFile="段差NG ID.aspx.vb" Inherits="段差NG_ID" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>段差NG ID</title>

          <!--[if lte IE 8]><script type="text/javascript" src="excanvas.js"></script><![endif]-->
    
        <script src="js/Chart.js"></script>    
        <script src="js/Chart.min.js"></script>    
        <script src="js/Chart.bundle.js"></script>    
        <script src="js/Chart.bundle.min.js"></script>
        <script src="js/utils.js"></script>
        <script src="js/analytics.js"></script>	
        <!--[if lte IE 8]><link rel="stylesheet" href="/assets/css/ie8.css" /><![endif]-->
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
        <br />
            <table  border="1" style ="width :900px ; height :auto ; margin-left :auto ;margin-right :auto  " >
            <tbody >
            
            <tr > 
                 <td  style ="text-align : right;width :10em " >
                                     
                    <div style ="width :13em ; text-align : left ">
                        From : 
                        <br />
                        Date : 
                        <asp:TextBox ID="datepicker_S"  runat="server" Width="94px" align="center" ></asp:TextBox>
                        <br />
                        Time : 
                        <asp:TextBox ID="S_H" runat="server" Width="94px" align="center" BorderWidth="1px" >16</asp:TextBox>
                        :
                        <asp:TextBox ID="S_M" runat="server" Width="94px" align="center" BorderWidth="1px" >00</asp:TextBox>                        
                    </div>
                </td> 
             
                <td rowspan="2" style ="text-align :center ;vertical-align :middle">
                    <div style ="text-align :center ;vertical-align :middle ;width :100% ;height :100% ">
                           <asp:Button ID="Submit" runat="server" Text="Button" />
                    </div>
                    
                </td>    
                <td rowspan="2" style ="text-align :center ;vertical-align :middle">
                
                    <div style ="text-align :center ;vertical-align :middle ;width :100% ">
                        <a> 撈取時機 </a><br />
                        <a> 每6小時撈取一次 </a><br />
                        <a> 並另存成Excel丟入以下路徑 </a><br />
                        <a> 20 : 00 </a><br />
                        <a> 02 : 00 </a><br />
                        <a> 08 : 00 </a><br />
                        <a> 14 : 00 </a><br />
                      
                    </div>
                    
                </td>                    
            </tr>
            <tr>
                <td rowspan="1">
                        <div style ="width :13em; text-align : left">
                            To :<br />
                            Date : 
                            <asp:TextBox  ID="datepicker_E" runat="server" Width="94px"></asp:TextBox>
                            <br />
                            Time : 
                              <asp:TextBox ID="E_H" runat="server" Width="94px" align="center" BorderWidth="1px" >20</asp:TextBox>
                            :
                            <asp:TextBox ID="E_M" runat="server" Width="94px" align="center" BorderWidth="1px" >00</asp:TextBox>      
                        </div>
                    </td>  
            </tr>
                <tr>
                     <td colspan ="1">
                        公用資料夾路徑 : 
                                    <td colspan ="2">
                        \\auo\GFS\L6000\L6A00\Cell\PUBLIC\■  重要! CUT斷差資訊
                    </td>
    
                </tr>
                <tr>
                    <td colspan ="3">
                       <a> 2017/08/31 修正From time bug</a>
                    </td>
                </tr>
            </tbody>
        </table>
        <div  style ="text-align :center ">
            <asp:Table ID="Table1" runat="server" CaptionAlign ="Left" Width ="900px" style="margin-left :auto ; margin-right :auto "></asp:Table>
        </div>
    </div>
    </form>
</body>
</html>
