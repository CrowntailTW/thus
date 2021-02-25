<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CCI History.aspx.vb" Inherits="CCI_History" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8">
    <title>CCI History</title>

    <link rel="stylesheet" href="assets/css/main.css" />

    <!-- datepicker-->
    <link type="text/css" href="assets/css/overcast/jquery-ui-1.9.2.custom.css" rel="stylesheet" />    
    <script type="text/javascript" src="js/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.9.2.custom.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.9.2.custom.min.js"></script>
    <!--<script type="text/javascript" src="js/jquery.ui.datepicker-zh-TW.js"></script>    -->

    
    <script type="text/javascript" src="/js/highcharts/exporting.js"></script>
    <script type="text/javascript" src="/js/highcharts/highcharts.js"></script>
    <script type="text/javascript" src="/js/highcharts/boost.js"></script>
    <script type="text/javascript" src="/js/highcharts/highcharts-more.js"></script>

    <script  type="text/javascript" >
      $(function () {
          $("#datepicker_S").datepicker();
          $("#datepicker_E").datepicker();
      });




      function checkall(elementId, TorF)
      {
          var cbl = document.getElementById(elementId);
           
          for(i=0;cbl.rows.length;i++)
          {
              for (j=0;cbl.rows[i].cells.length ;j++)
              {
                  table.rows[i].cells[j].childNodes[0].checked = TorF;
              }
          }
      }

    </script>
    <!-- datepicker-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <table  border="1" style ="width : 40% ; height :auto ; margin-left :auto ;margin-right :auto  " >
             <br />
              <tr  style="text-align :center " > 
             
                  <td style="vertical-align :middle "> 
                        <div style ="width :8em ; text-align : right ">
                            From : 
                            <asp:TextBox ID="datepicker_S"  runat="server" Width="94px"  ></asp:TextBox>
                        </div>     
                        <div style ="width :8em ; text-align : right ">
                            To:                 
                            <asp:TextBox  ID="datepicker_E" runat="server" Width="94px"></asp:TextBox>                   
                        </div>             
                  </td> 
                  <td colspan="2" style ="text-align :left ;vertical-align :central ">

                          <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns ="4"  >                                    
                                    <asp:ListItem Value ="1">CCT100</asp:ListItem>
                                    <asp:ListItem Value ="2">CCT200</asp:ListItem>
                                    <asp:ListItem Value ="3">CCT300</asp:ListItem>
                                    <asp:ListItem Value ="4">CCT400</asp:ListItem>
                                    <asp:ListItem Value ="5">CCT500</asp:ListItem>
                                    <asp:ListItem Value ="6">CCT600</asp:ListItem>
                                    <asp:ListItem Value ="7">CCT700</asp:ListItem>
                                    <asp:ListItem Value ="8">CCT800</asp:ListItem>
                                    <asp:ListItem Value ="9">CCT900</asp:ListItem>
                                    <asp:ListItem Value ="10">CCTA00</asp:ListItem>
                                    <asp:ListItem Value ="11">CCTB00</asp:ListItem>
                                    <asp:ListItem Value ="12">CCTC00</asp:ListItem>
                                    <asp:ListItem Value ="13">CCTD00</asp:ListItem>
                                    <asp:ListItem Value ="14">CCTE00</asp:ListItem>
                                    <asp:ListItem Value ="15">CCTF00</asp:ListItem>
                             </asp:CheckBoxList>
                                              
                  </td>

          
             </tr> 
             <tr>
                 
                 <td>
                     <div style ="width :8em ;height :100% ;text-align :left  ;vertical-align :central ">
                         篩選Chipping 規格 <br />
                         <div style ="text-align :left ">
                            <asp:TextBox ID="TextBox_min" runat="server" Text ="0" BorderStyle ="Solid " Width="3em" style ="margin-left :auto " ></asp:TextBox>
                            ~
                            <asp:TextBox ID="TextBox_MAX" runat="server" Text ="9999" BorderStyle ="Solid"  Width="3em" style ="margin-left :auto " ></asp:TextBox>
                            <asp:CheckBox ID="CheckBox_exculde" Text ="Exculde" runat="server" Checked="True" />
                             RecipeNo :                            
                             <asp:TextBox ID="TextBox_RecipeNo" runat="server"></asp:TextBox>
                         </div>                        
                                                            
                     </div>                     
                 </td>
                 <td colspan ="2" valign ="middle" >
                     <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatColumns ="4">
                             <asp:ListItem Selected ="True" >edge A</asp:ListItem>
                             <asp:ListItem Selected ="True" >edge B</asp:ListItem>
                             <asp:ListItem Selected ="True" >edge C</asp:ListItem>
                             <asp:ListItem Selected ="True" >edge D</asp:ListItem>
                             <asp:ListItem Selected ="True" >edge E</asp:ListItem>
                             <asp:ListItem Selected ="True" >edge F</asp:ListItem>
                             <asp:ListItem Selected ="True" >edge G</asp:ListItem>
                             <asp:ListItem Selected ="True" >edge H</asp:ListItem>
                         </asp:CheckBoxList>
                 </td>
               
              </tr>
              <tr>
                  <td colspan="2" style ="text-align  :left  ; vertical-align :middle  ">
                 
                      <div style ="text-align :left ">
                      
                      <asp:CheckBox ID="CheckBox1" runat="server" text ="勾選查詢全部ID" />
                    
                        <br />ID:
                        <asp:TextBox ID="TextBox1" runat="server" TextMode ="MultiLine" Width ="100%" height="6em" BorderStyle="Dashed" BorderWidth="1px" Rows="3" ></asp:TextBox>
                        </div>                  
                  </td>
             
                  <td>
                      <div style ="height :100% ;text-align :center ;vertical-align :central ">

                          <asp:Button ID="Button_DefectT1" TabIndex="1" runat="server" Text="精度" Width ="100%" />   
                          <asp:Button ID="Button_DefectT2" TabIndex="2" runat="server" Text="Defect" Width ="100%"/>                           
                          <asp:Button ID="Button_Distribution" TabIndex="3" runat="server" Text="Defect 分佈" Width ="100%"/>                           
                            
                      </div>                        
                  </td>
              </tr>  
              <tr style ="height :1px">
                  <td></td>
                  <td></td>
                  <td style ="width : 5px"></td>
              </tr> 
          </table> 
        
        <asp:Table ID="Table1" runat="server"></asp:Table>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>

    </div>
    </form>
</body>
</html>
