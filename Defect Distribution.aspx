<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Defect Distribution.aspx.vb" Inherits="Defect_Distribution" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Defect Distribution</title>

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
        .auto-style1 {
            height: 47px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br /><br /><br />
        
        <table  border="1" style ="width : 40% ; height :auto ; margin-left :auto ;margin-right :auto  " >
             <br />
              <tr  style="text-align :center " > 
             
                    <td style="vertical-align :middle "> 
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

                    <td rowspan="2" style ="text-align :center ;vertical-align :middle ">
                        <div style ="height :100% ;text-align :center ;vertical-align :central ">
                            <asp:Button ID="Button_Submit" runat="server" Text="Submit" />                                 
                        </div>
                    </td>
             </tr> 
             <tr>
                    <td colspan ="2"  style ="text-align  :left  ; vertical-align :middle  ">
                 
                    ID:                   
                    <asp:TextBox ID="TextBox1" runat="server"  Width ="100%" BorderStyle="Dashed" BorderWidth="1px" ></asp:TextBox>
                    
                </td>
             </tr>   
          </table>

        <canvas id="myChart_CF" style ="height :100%"></canvas>
        <canvas id="myChart_TFT" style ="height :100%"></canvas>
    </div>
    </form>
    <script>

        var dataa = [{ x: 20, y: 5.245, r: 15 },{ x: 40, y: 15.245, r: 30 }];
        var X_max = 350;
        var Y_max = 240;
        var ctx = document.getElementById("myChart_CF")

        Chart.types.Line.extend({
            name: "LineWithLine",
            draw: function () {
                Chart.types.Line.prototype.draw.apply(this, arguments);
                var point = this.datasets[0].points[this.options.lineAtIndex];
                var scale = this.scale;

                // draw line
                this.chart.ctx.beginPath();
                this.chart.ctx.moveTo(point.x, scale.startPoint + 24);
                this.chart.ctx.strokeStyle = '#ff0000';
                this.chart.ctx.lineTo(point.x, scale.endPoint);
                this.chart.ctx.stroke();

                // write TODAY
                this.chart.ctx.textAlign = 'center';
                this.chart.ctx.fillText("TODAY", point.x, scale.startPoint + 12);
            }
        });

        new Chart(ctx, {
            type: 'bubble',
            data: {
                labels: "",
                datasets: [
                  {
                      label: ["C"],
                      backgroundColor: "rgba(255,221,50,0.2)",
                      borderColor: "rgba(255,221,50,1)",                      
                      data: dataa
                  }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: '-Defect Distribution-'
                }, scales: {
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: "Y",                            
                        },
                        ticks: {
                            suggestedMin: -5, suggestedMax: Y_max
                        }
                    }],
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: "X"                            
                        },
                        ticks: {
                            suggestedMin: -5, suggestedMax: X_max
                        }
                    }]
                }
            }
        });


    </script>
</body>
</html>
