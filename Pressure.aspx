<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Pressure.aspx.vb" Inherits="Pressure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Cut Presure History</title>
    
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

    <style>
    .box{width:30%; float:left;}
    .zone{width:30%; height:30%;}
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br /><br /><br />
        
        <table  border="1" style ="width : 40% ; height :auto ; margin-left :auto ;margin-right :auto  " >
             <br />
              <tr  style="text-align :center " > 
             
                    <td style="vertical-align :middle " rowspan="5"> 
                        <div style ="width :10em ; text-align : right ">
                            From : 
                            <asp:TextBox ID="datepicker_S"  runat="server" Width="94px"  ></asp:TextBox>
                        </div>     
                        <div style ="width :10em ; text-align : right ">
                            To:                 
                            <asp:TextBox  ID="datepicker_E" runat="server" Width="94px"></asp:TextBox>                   
                        </div>             
                    </td> 
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCT100" runat="server" Text="CCT100" />                                 
                        </div>                        
                    </td>
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCT200" runat="server" Text="CCT200" />                                 
                               </div>                        
                    </td>
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCT300" runat="server" Text="CCT300" />                                 
                               </div>                        
                    </td>
                   <tr  style="text-align :center " > 
                  
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCT400" runat="server" Text="CCT400" />                                 
                        </div>                        
                    </td>
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCT500" runat="server" Text="CCT500" />                                 
                        </div>                        
                    </td>
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCT600" runat="server" Text="CCT600" />                                 
                        </div>                        
                    </td>
                    </tr>
                    <tr  style="text-align :center " > 
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCT700" runat="server" Text="CCT700" />                                 
                               </div>                        
                    </td>
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCT800" runat="server" Text="CCT800" />                                 
                               </div>                        
                    </td>
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCT900" runat="server" Text="CCT900" />                                 
                               </div>                        
                    </td>
                        </tr>
                    <tr  style="text-align :center " > 
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCTA00" runat="server" Text="CCTA00" />                                 
                               </div>                        
                    </td>
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCTB00" runat="server" Text="CCTB00" />                                 
                               </div>                        
                    </td>
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCTC00" runat="server" Text="CCTC00" />                                 
                               </div>                        
                    </td>
                        </tr>
                    <tr  style="text-align :center " > 
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCTD00" runat="server" Text="CCTD00" />                                 
                               </div>                        
                    </td>
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCTE00" runat="server" Text="CCTE00" />                                 
                               </div>                        
                    </td>
                    <td style ="text-align :left ;vertical-align :central ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_CCTF00" runat="server" Text="CCTF00" />                                 
                            
                        </div>  
                                                                   
                    </td>
             </tr> 
             </table>

        <canvas id="myChart_CF" style ="height :100%"></canvas>
        <canvas id="myChart_TFT" style ="height :100%"></canvas>
    </div>
    </form>
</body>
</html>
