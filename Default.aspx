﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE HTML>
<!--
	Phantom by HTML5 UP
	html5up.net | @ajlkn
	Free for personal and commercial use under the CCA 3.0 license (html5up.net/license)
-->
<html>
<head>
    <title>VL6AH1 Web</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8">
    <!-- Scripts -->
    <script type="text/javascript" src="assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/skel.min.js"></script>
    <script type="text/javascript" src="assets/js/util.js"></script>
    <!--[if lte IE 8]><script src="assets/js/ie/respond.min.js"></script><![endif]-->
    <script type="text/javascript" src="assets/js/main.js"></script>

    <!--[if lte IE 8]><script src="assets/js/ie/html5shiv.js"></script><![endif]-->
    <link rel="stylesheet" href="assets/css/main1.css" />
    <!--[if lte IE 9]><link rel="stylesheet" href="assets/css/ie9.css" /><![endif]-->
    <!--[if lte IE 8]><link rel="stylesheet" href="assets/css/ie8.css" /><![endif]-->
    <style type="text/css">
        .auto-style1 {
            height: 147px;
        }
    </style>
</head>
<body>
    <!-- Wrapper -->
    <div id="wrapper">
         <header id="header">
            <div class="inner">

                <!-- Logo -->
<%--                <a href="a" class="logo">
                    <span class="symbol">
                        <img src="images/logo3.png" alt="" /></span><span class="title">VL6AH1</span>
                </a>
                <a href="tunneler.html" class="logo" target="_blank" title=".">.</a>
                <!-- Nav -->--%>
                <nav>
                    <ul>
                        <li><a href="#menu">Menu</a></li>
                    </ul>
                </nav>
            </div>
        </header>
    </div>
        <!-- Menu -->
        <nav id="menu">
            <h2>值班常用連結</h2>
            <ul>
                <li><a href="http://l6al2-pc04.corpnet.auo.com/other_asp/CELL_MOVE/move.asp?stage=CCT','CGL','SCT&of=Off&model=ALL" target="_blank">Cell Move</a></li>
                <li><a href="http://tw100048882.corpnet.auo.com/Smart_Fab/H1WIP.aspx" target="_blank">WIP</a></li>
                <li><a href="http://cmtcwa8:8082/spcreport/ooc/OOC_Main.jsp?oocReportType=OOC/OOS&authkey=907073F53148997163869" target="_blank">SPC</a></li>
                <li><a href="http://apc/KPI_TRUE_ALARM_WEB/Default.aspx" target="_blank">APC</a></li>
                <li><a href="http://tw100048882.corpnet.auo.com/other_asp/CCT_YIELD.aspx" target="_blank">X-short</a></li>
                <li><a href="http://l6al2-pc04.corpnet.auo.com/Other_asp/CSTPOSITION.asp?pos=STOCKER" target="_blank">Ca7 CST</a></li>
                <li><a href="http://l6al2-pc04.corpnet.auo.com/other_asp/tact_time20150723.asp" target="_blank">Beol tact time</a></li>
                <li><a href="http://l6al2-pc04.corpnet.auo.com/other_asp/BEOLChipHis.asp" target="_blank">Chip History</a></li>
                <li><a href="http://10.96.18.165/MyESH/MyESH.aspx" target="_blank">ESH</a></li>
                <li><a href="http://tw100048882.corpnet.auo.com/other_asp/CCT_Chart.aspx" target="_blank">Pre-Yield</a></li>
                <li><a href="http://l6ac1-pc01/lac3/celltestlowdata/chipsort.asp" target="_blank">Location Query</a></li>
                <li><a href="http://l6ac1-pc01/lac3/celltestlowdata/celltestrowdata.asp" target="_blank">Test Query</a></li>

                <li><a href="http://l6ac1-pc01/lac3/1_GRADE%20REPORT%20v2/index.htm" target="_blank">2 grade chart</a></li>
                <li><a href="http://10.96.44.22:81/lac3/LINE%20YIELD%20CHART/QUERY.ASPX" target="_blank">Tool Sort Chat</a></li>

            </ul>
        </nav>
        <!-- Main -->

        <div id="main">
            <div class="inner">
               <div style="text-align: center">
                    <div style="text-align: center">
                        <canvas id="myCanvas1" width="120" height="120">L</canvas>
                        <canvas id="myCanvas2" width="120" height="120">6</canvas>
                        <canvas id="myCanvas3" width="120" height="120">A</canvas>
                        <canvas id="myCanvas4" width="120" height="120">H</canvas>
                        <canvas id="myCanvas5" width="120" height="120">1</canvas>
                    </div>
                </div>

                <div>
                </div>

                <table class="tiles" style="margin-left: auto; margin-right: auto; width: 100%; text-wrap: avoid">
                    <tr>
                        <td>

                            <article class="style1">
                                <a href="EtE Dim30 Monitor_new.aspx" target="_blank">
                                    <h2>Defect<br/>Dimension</h2>
                                    <div class="content">
                                        <p>New Chart Option.</p>
                                    </div>
                                </a>
                            </article>
                        </td>
                        <td>
                            <article class="style1">
                                <a href="CCD Status.aspx" target="_blank">
                                    <h2>CCD Status</h2>
                                    <div class="content">
                                        <p>CCI 下CCD 開啟狀態.</p>
                                    </div>
                                </a>
                            </article>
                        </td>
                        <td>
                            <article class="style1">
                                <a href="Parts.aspx" target="_blank">
                                    <h2>Y類 Parts</h2>
                                    <div class="content">
                                        <p>Y類 Parts 上/下機系統</p>
                                    </div>
                                </a>
                            </article>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <article class="style1">
                                <a href="mileage.aspx" target="_blank">
                                    <h2>刀輪里程</h2>
                                    <div class="content">
                                        <p>各線刀輪里程</p>
                                    </div>
                                </a>
                            </article>
                        </td>
                        <td>
                            <article class="style1">
                                <a href="CutInfo.aspx" target="_blank">
                                    <h2>Cut 刀壓 / 里程</h2>
                                    <div class="content">
                                        <p>Cut 各線 刀輪壓力與使用里程數.</p>
                                    </div>
                                </a>
                            </article>
                        </td>
                        <td>
                            <article class="style1">
                                <a href="CutInfo2.aspx" target="_blank">
                                    <h2>Cut 標準差</h2>
                                    <div class="content">
                                        <p>Cut 各線 各刀標準差.</p>
                                    </div>
                                </a>
                            </article>
                        </td>
                    </tr>
                    <tr>
                       <td>
                            <article class="style1">
                                <a href="Pressure.aspx" target="_blank">
                                    <h2>Cut Pressure</h2>
                                    <div class="content">
                                        <p>刀壓查詢</p>
                                    </div>
                                </a>
                            </article>
                        </td>
                        <td>
                            <article class="style1">
                                <a href="FullImage_new.aspx" target="_blank">
                                    <h2>Full Image</h2>
                                    <div class="content">
                                        <p>全圖影像查詢</p>
                                    </div>
                                </a>
                            </article>
                        </td>
                        <td>
                            <article class="style1">
                                <a href="CCI History.aspx" target="_blank">
                                    <h2>CCI History</h2>
                                    <div class="content">
                                        <p>CCI 判片紀錄.</p>
                                    </div>
                                </a>
                            </article>
                        </td>
<%--                        <td>
                            <article class="style1">
                                <a href="段差NG ID.aspx" target="_blank">
                                    <h2>段差NG ID</h2>
                                    <div class="content">
                                        <p>段差NG ID</p>
                                    </div>
                                </a>
                            </article>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <article class="style1">
                                <a href="INK.aspx" target="_blank">
                                    <h2>INK monitor</h2>
                                    <div class="content">
                                        <p>INK monitor.</p>
                                    </div>
                                </a>
                            </article>
                        </td>
                        <td>
                            <article class="style1">
                                <a href="BLL.aspx" target="_blank">
                                    <h2>BLL Monitor</h2>
                                    <div class="content">
                                        <p>BLL monitor.</p>
                                    </div>
                                </a>
                            </article>
                        </td>
                        <td>
                            <article class="style1">
                                <a href="Data.aspx" target="_blank">
                                    <h2>Data</h2>
                                    <div class="content">
                                        <p>CSV data .</p>
                                    </div>
                                </a>
                            </article>                        </td>
                    </tr>
                </table>
            </div>

            <!-- Footer -->
            <!--
					<footer id="footer">
						<div class="inner">
							<section>
								<h2>Get in touch</h2>
								<form method="post" action="#">
									<div class="field half first">
										<input type="text" name="name" id="name" placeholder="Name" />
									</div>
									<div class="field half">
										<input type="email" name="email" id="email" placeholder="Email" />
									</div>
									<div class="field">
										<textarea name="message" id="message" placeholder="Message"></textarea>
									</div>
									<ul class="actions">
										<li><input type="submit" value="Send" class="special" /></li>
									</ul>
								</form>
							</section>
							<section>
								<h2>Follow</h2>
								<ul class="icons">
									<li><a href="#" class="icon style2 fa-twitter"><span class="label">Twitter</span></a></li>
									<li><a href="#" class="icon style2 fa-facebook"><span class="label">Facebook</span></a></li>
									<li><a href="#" class="icon style2 fa-instagram"><span class="label">Instagram</span></a></li>
									<li><a href="#" class="icon style2 fa-dribbble"><span class="label">Dribbble</span></a></li>
									<li><a href="#" class="icon style2 fa-github"><span class="label">GitHub</span></a></li>
									<li><a href="#" class="icon style2 fa-500px"><span class="label">500px</span></a></li>
									<li><a href="#" class="icon style2 fa-phone"><span class="label">Phone</span></a></li>
									<li><a href="#" class="icon style2 fa-envelope-o"><span class="label">Email</span></a></li>
								</ul>
							</section>
							<ul class="copyright">
								<li>&copy; Untitled. All rights reserved</li><li>Design: <a href="http://html5up.net">HTML5 UP</a></li>
							</ul>
						</div>
					</footer>
          -->
        </div>

        <script type="text/javascript">

            var ctx1 = document.getElementById("myCanvas1").getContext("2d");
            var ctx2 = document.getElementById("myCanvas2").getContext("2d");
            var ctx3 = document.getElementById("myCanvas3").getContext("2d");
            var ctx4 = document.getElementById("myCanvas4").getContext("2d");
            var ctx5 = document.getElementById("myCanvas5").getContext("2d");
            var ctxM = [ctx1, ctx2, ctx3, ctx4, ctx5];


            var start;
            var startString = "";
            var speed = 150;
            //----player----
            //蛇初始位置
            var r = [{ x: 10, y: 5 }, { x: 10, y: 4 }]; 
            //果子位置
            var e = null;

            var co = 40;
            //----AI----
            var rAI = [{ x: 10, y: 5 }, { x: 10, y: 4 }];
            var eAI = null;
            var coAI = 40;

            var cnt = 0;
            var cnt_L6AH1 = 3;
            //ctx1.shadowBlur = 3;
            //ctx1.shadowColor = "black";
            //ctx2.shadowColor = "black";
            //ctx3.shadowColor = "black";
            //ctx4.shadowColor = "black";
            //ctx5.shadowColor = "black";          

            for (var i = 0 ; i <= ctxM.length - 1; i++) {
                ctxM[i].fillStyle = "#98B898";
            }
            var colors = ["red", "green"];
            var L6AH1_L = [
                [0, 0, 0, 0, 0, 0, 0, 0],
                [0, 1, 0, 0, 0, 0, 0, 0],
                [0, 1, 0, 0, 0, 0, 0, 0],
                [0, 1, 0, 0, 0, 0, 0, 0],
                [0, 1, 0, 0, 0, 0, 0, 0],
                [0, 1, 0, 0, 0, 0, 0, 0],
                [0, 1, 1, 1, 1, 1, 1, 0],
                [0, 0, 0, 0, 0, 0, 0, 0]
            ];
            var L6AH1_6 = [
                [0, 0, 0, 0, 0, 0, 0, 0],
                [0, 0, 1, 1, 1, 1, 0, 0],
                [0, 1, 0, 0, 0, 0, 0, 0],
                [0, 1, 1, 1, 1, 1, 0, 0],
                [0, 1, 0, 0, 0, 0, 1, 0],
                [0, 1, 0, 0, 0, 0, 1, 0],
                [0, 0, 1, 1, 1, 1, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 0]
            ];
            var L6AH1_A = [
                [0, 0, 0, 0, 0, 0, 0, 0],
                [0, 0, 0, 1, 1, 0, 0, 0],
                [0, 0, 1, 0, 0, 1, 0, 0],
                [0, 0, 1, 0, 0, 1, 0, 0],
                [0, 1, 1, 1, 1, 1, 1, 0],
                [0, 1, 0, 0, 0, 0, 1, 0],
                [0, 1, 0, 0, 0, 0, 1, 0],
                [0, 0, 0, 0, 0, 0, 0, 0]
            ];
            var L6AH1_H = [
                [0, 0, 0, 0, 0, 0, 0, 0],
                [0, 1, 0, 0, 0, 0, 1, 0],
                [0, 1, 0, 0, 0, 0, 1, 0],
                [0, 1, 1, 1, 1, 1, 1, 0],
                [0, 1, 0, 0, 0, 0, 1, 0],
                [0, 1, 0, 0, 0, 0, 1, 0],
                [0, 1, 0, 0, 0, 0, 1, 0],
                [0, 0, 0, 0, 0, 0, 0, 0]
            ];
            var L6AH1_1 = [
                [0, 0, 0, 0, 0, 0, 0, 0],
                [0, 0, 0, 1, 1, 0, 0, 0],
                [0, 0, 1, 0, 1, 0, 0, 0],
                [0, 0, 0, 0, 1, 0, 0, 0],
                [0, 0, 0, 0, 1, 0, 0, 0],
                [0, 0, 0, 0, 1, 0, 0, 0],
                [0, 0, 1, 1, 1, 1, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 0]
            ];

            function slider() {
                if (start | cnt_L6AH1 <= 0) { return; }
                if (cnt == 0) {
                    ctx1.clearRect(0, 0, 120, 120);
                    ctx2.clearRect(0, 0, 120, 120);
                    ctx3.clearRect(0, 0, 120, 120);
                    ctx4.clearRect(0, 0, 120, 120);
                    ctx5.clearRect(0, 0, 120, 120);
                }
                if (cnt < 8) {
                    for (var i = 0; i < 8; i++) {
                        if (L6AH1_L[cnt][i] == 1) ctx1.fillRect(i * 15, cnt * 15, 15, 15);
                        if (L6AH1_6[cnt][i] == 1) ctx2.fillRect(i * 15, cnt * 15, 15, 15);
                        if (L6AH1_A[cnt][i] == 1) ctx3.fillRect(i * 15, cnt * 15, 15, 15);
                        if (L6AH1_H[cnt][i] == 1) ctx4.fillRect(i * 15, cnt * 15, 15, 15);
                        if (L6AH1_1[cnt][i] == 1) ctx5.fillRect(i * 15, cnt * 15, 15, 15);
                    }
                } else if (cnt == 10 || cnt == 13) {
                    ctx1.clearRect(0, 0, 120, 120);
                    ctx2.clearRect(0, 0, 120, 120);
                    ctx3.clearRect(0, 0, 120, 120);
                    ctx4.clearRect(0, 0, 120, 120);
                    ctx5.clearRect(0, 0, 120, 120);
                } else {
                    for (var i = 0 ; i < 8; i++) {
                        for (var j = 0; j < 8; j++) {
                            if (L6AH1_L[i][j] == 1) ctx1.fillRect(j * 15, i * 15, 15, 15);
                            if (L6AH1_6[i][j] == 1) ctx2.fillRect(j * 15, i * 15, 15, 15);
                            if (L6AH1_A[i][j] == 1) ctx3.fillRect(j * 15, i * 15, 15, 15);
                            if (L6AH1_H[i][j] == 1) ctx4.fillRect(j * 15, i * 15, 15, 15);
                            if (L6AH1_1[i][j] == 1) ctx5.fillRect(j * 15, i * 15, 15, 15);
                        }
                    }
                }
                cnt = (cnt + 1) % (8 + 9);
                if (cnt == 0) cnt_L6AH1 = cnt_L6AH1 - 1;
            }

            setInterval(slider, 150);
        
            function onframe() {

                if (!start) return;

                speed = 120// + ((r.length - 2) % 2) * 20;
                if (check(r[0], 0) || r[0].x < 0 || r[0].x >= 24 || r[0].y < 0 || r[0].y >= 24) {
                    r = [{ x: 10, y: 5 }, { x: 10, y: 4 }];
                    co = 40;
                }

                if (check(rAI[0], 0) || rAI[0].x < 0 || rAI[0].x >= 24 || rAI[0].y < 0 || rAI[0].y >= 24) {
                    rAI = [{ x: 10, y: 5 }, { x: 10, y: 4 }];
                    coAI = 40;
                }

                if (e) { //吃果子
                    if ((co == 40 && r[0].x == e.x && r[0].y + 1 == e.y)
                        || (co == 38 && r[0].x == e.x && r[0].y - 1 == e.y)
                        || (co == 37 && r[0].x - 1 == e.x && r[0].y == e.y)
                        || (co == 39 && r[0].x + 1 == e.x && r[0].y == e.y)) {
                        r.unshift(e);//添加一個元素到開頭 吃到果子 變長1
                        e = null;
                    }
                }

                if (eAI) {
                    if ((coAI == 40 && rAI[0].x == eAI.x && rAI[0].y + 1 == eAI.y)
                        || (coAI == 38 && rAI[0].x == eAI.x && rAI[0].y - 1 == eAI.y)
                        || (coAI == 37 && rAI[0].x - 1 == eAI.x && rAI[0].y == eAI.y)
                        || (coAI == 39 && rAI[0].x + 1 == eAI.x && rAI[0].y == eAI.y)) {
                        rAI.unshift(eAI);
                        eAI = null;
                    }
                }

                r.unshift(r.pop());
                rAI.unshift(rAI.pop());

                switch (co) {
                    case 37:  //←
                        r[0].x = r[1].x - 1;
                        r[0].y = r[1].y;
                        break;
                    case 38:  //↑
                        r[0].x = r[1].x;
                        r[0].y = r[1].y - 1;
                        break;
                    case 39:  //→
                        r[0].x = r[1].x + 1;
                        r[0].y = r[1].y;
                        break;
                    case 40:  //↓
                        r[0].x = r[1].x;
                        r[0].y = r[1].y + 1;
                        break;
                }

                
                switch (coAI) {
                    case 37: //←
                        rAI[0].x = rAI[1].x - 1;
                        rAI[0].y = rAI[1].y;
                        break;
                    case 38: //↑
                        rAI[0].x = rAI[1].x;
                        rAI[0].y = rAI[1].y - 1;
                        break;
                    case 39://→
                        rAI[0].x = rAI[1].x + 1;
                        rAI[0].y = rAI[1].y;
                        break;
                    case 40: //↓
                        rAI[0].x = rAI[1].x;
                        rAI[0].y = rAI[1].y + 1;
                        break;
                }

                ctx1.clearRect(0, 0, 120, 120);
                ctx3.clearRect(0, 0, 120, 120);

                if (e) ctx1.fillRect(e.x * 5, e.y * 5, 5, 5);
                if (eAI) ctx3.fillRect(eAI.x * 5, eAI.y * 5, 5, 5);
                          
                ctx1.strokeStyle = "#A05555";
                ctx1.strokeRect(0, 0, 120, 120);

                for (var i = 0; i < r.length; i++) {
                    ctx1.fillRect(r[i].x * 5, r[i].y * 5, 5, 5);                   
                }
                for (var i = 0; i < rAI.length; i++) {
                    ctx3.fillRect(rAI[i].x * 5, rAI[i].y * 5, 5, 5);
                }

                var sss = "Score :" + (r.length - 2);
                var sssAI = "Score :" + (rAI.length - 2);
                ctx1.fillText(sss, 10, 100);
                ctx3.fillText(sssAI, 10, 100);

                while (e == null || check(e)) {
                    e = { y: (Math.random() * 24 >>> 0), x: (Math.random() * 24 >>> 0) };
                }
                while (eAI == null || check(eAI)) {
                    eAI = { y: (Math.random() * 24 >>> 0), x: (Math.random() * 24 >>> 0) };
                }
                if (check(r[0], 0) || r[0].x < 0 || r[0].x >= 24 || r[0].y < 0 || r[0].y >= 24) {
                    //alert("game over\nYou get ["+(r.length-2)+"]");  
                }

            }

            setInterval(onframe, speed);

            document.onkeyup = function (event) {

                if (event.keyCode >= 37 && event.keyCode <= 40 && (Math.abs(event.keyCode - co) != 2)) {
                    co = event.keyCode;
                }

                if ((event.keyCode >= 37 && event.keyCode <= 40) || (event.keyCode == 65 || event.keyCode == 66)) {
                    startString = startString + event.keyCode;
                }
                else {
                    startString = "";
                }
                //ctx1.fillText(startString, 10, 10);
                if (startString == "38384040373739396566" && !start) {
                    start = true;
                    setInterval(onframe, speed);
                    co = 40;
                }

            }

            function getAIco() {
                //37 ←
                //38 ↑
                //39 →
                //40 ↓
                coAI = 40;
                if (coAI == 40) { }
            }

            function check(e, j) {
                for (var i = 0; i < r.length; i++) {
                    if (j != i && r[i].x == e.x && r[i].y == e.y) return true;
                }
                return false;
            }

        </script>
</body>
</html>
