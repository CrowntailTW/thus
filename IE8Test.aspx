<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IE8Test.aspx.vb" Inherits="IE8Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>IE8 test</title>

    <!--[if lte IE 8]><script type="text/javascript" src="excanvas.js"></script><![endif]-->
    
    <script  type="text/JavaScript" src="js/excanvas.js"></script>    
    <script  type="text/JavaScript" src="js/jquery.min.js"></script>      
    
    <script type="text/JavaScript" src="js/Chart.js"></script>    
      <script type="text/JavaScript" src="js/Chart.min.js"></script>    
      <script type="text/JavaScript" src="js/Chart.bundle.js"></script>    
      <script type="text/JavaScript" src="js/Chart.bundle.min.js"></script>
      <script type="text/JavaScript" src="js/utils.js"></script>
      <script type="text/JavaScript" src="js/analytics.js"></script>	

</head>

<body>
    <form id="form1" runat="server">

    <div id = "canvasA"></div>
    <div id = "dvCanvas"></div>                 
                   
            <iframe src="http://l6al2-pc04.corpnet.auo.com/other_asp/CELL_MOVE/move.asp?stage=CCT" width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://tw100048882.corpnet.auo.com/Smart_Fab/H1WIP.aspx"width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://cmtcwa8:8082/spcreport/ooc/OOC_Main.jsp?oocReportType=OOC/OOS&authkey=907073F53148997163869"width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://apc/KPI_TRUE_ALARM_WEB/Default.aspx"width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://tw100048882.corpnet.auo.com/other_asp/CCT_YIELD.aspx"width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://l6al2-pc04.corpnet.auo.com/Other_asp/CSTPOSITION.asp?pos=STOCKER"width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://l6al2-pc04.corpnet.auo.com/other_asp/BEOLChipHis.asp"width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://tw100048882.corpnet.auo.com/other_asp/CCT_Chart.aspx"width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://l6ac1-pc01/lac3/celltestlowdata/chipsort.asp"width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://l6ac1-pc01/lac3/celltestlowdata/celltestrowdata.asp"width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://l6ac1-pc01/lac3/1_GRADE%20REPORT%20v2/index.htm"width ="100%" height ="800px" frameborder ="0" ></iframe>
            <iframe src="http://10.96.44.22:81/lac3/LINE%20YIELD%20CHART/QUERY.ASPX"width ="100%" height ="800px" frameborder ="0" ></iframe>
       
         <canvas id ="myChart" ></canvas>
        <!--
        <script type="text/JavaScript" >

            window.onload = function () {
                //Fix for IE 7 and 8
                var canvas = document.createElement('canvas');
                canvas.height = "800";
                canvas.width = "800";
                document.getElementById("dvCanvas").appendChild(canvas);

                if ($.browser.msie && ($.browser.version == "7.0" || $.browser.version == "8.0")) {
                    G_vmlCanvasManager.initElement(canvas);
                }

                var context = canvas.getContext('2d');
                var centerX = canvas.width / 2;
                var centerY = canvas.height / 2;
                var radius = 80;

                context.fillStyle = "#0090CB";
                context.fillRect(0, 0, canvas.width, canvas.height);
                
                context.beginPath();
                context.strokeStyle = "#AA2222";
                context.strokeRect(50, 50, canvas.width - 100, canvas.height - 100);
                context.moveTo(50, 100);
                context.lineTo(canvas.width - 50, 100);
                context.lineWidth = 5;
                context.stroke();
                
                              
                context.fillStyle = "#FFF";
                context.font = "50px Arial";
                context.fillText("ASP", 50, 120);

                context.beginPath();            
                context.arc(centerX, centerY, radius, 0, 2 * Math.PI, false);
                context.lineWidth = 5;
                context.strokeStyle = "#fff";
                context.stroke();
            }
        </script> 
     -->
         <script type="text/JavaScript" >
            var sun = new Image();
            var moon = new Image();
            var earth = new Image();
            //////////////////////////////
            var canvasa = document.createElement('canvas');
            canvasa.height = "600";
            canvasa.width = "600";
            document.getElementById("canvasA").appendChild(canvasa);
            var ctx = canvasa.getContext('2d');
            //////////////////////////////

            window.onload = function init() {
                sun.src = 'images/galacy/Canvas_sun.png';
                moon.src = 'images/galacy/Canvas_moon.png';
                earth.src = 'images/galacy/Canvas_earth.png';
                setInterval(draw, 100);
            }

            function draw() {
                //var ctx = document.getElementById('canvas').getContext('2d');

                ctx.globalCompositeOperation = 'destination-over';
                ctx.clearRect(0, 0, 300, 300); // clear canvas

                ctx.fillStyle = 'rgba(0,0,0,0.4)';
                ctx.strokeStyle = 'rgba(0,153,255,0.4)';
                ctx.save();
                ctx.translate(150, 150);

                // Earth
                var time = new Date();
                ctx.rotate(((2 * Math.PI) / 60) * time.getSeconds() + ((2 * Math.PI) / 60000) * time.getMilliseconds());
                ctx.translate(105, 0);
                ctx.fillRect(0, -12, 50, 24); // Shadow
                ctx.drawImage(earth, -12, -12);

                // Moon
                ctx.save();
                ctx.rotate(((2 * Math.PI) / 6) * time.getSeconds() + ((2 * Math.PI) / 6000) * time.getMilliseconds());
                ctx.translate(0, 28.5);
                ctx.drawImage(moon, -3.5, -3.5);
                ctx.restore();

                ctx.restore();

                ctx.beginPath();
                ctx.arc(150, 150, 105, 0, Math.PI * 2, false); // Earth orbit
                ctx.stroke();

                ctx.drawImage(sun, 0, 0, 300, 300);
            }
        </script>
        
                

    </form>

</body>

</html>
