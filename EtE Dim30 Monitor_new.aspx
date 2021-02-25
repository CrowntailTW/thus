<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EtE Dim30 Monitor_new.aspx.vb" Inherits="EtE_Dim30_Monitor" %>

<!DOCTYPE html>
<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <title>EtE Dim30 Monitor</title>


    <script type="text/javascript" src="/js/highcharts/exporting.js"></script>
    <script type="text/javascript" src="/js/highcharts/highcharts.js"></script>
    <script type="text/javascript" src="/js/highcharts/boost.js"></script>


    <link rel="stylesheet" href="assets/css/main1.css" />
    <!--[if lte IE 8]><link rel="stylesheet" href="/assets/css/ie8.css" /><![endif]-->

    <script type="text/javascript" src="js/Chart.js"></script>
    <script type="text/javascript" src="js/Chart.min.js"></script>

    <!-- 
    <script type ="text/javascript" src="js/Chart.bundle.js"></script>    
    <script type ="text/javascript" src="js/Chart.bundle.min.js"></script>
    -->

    <script type="text/javascript" src="js/utils.js"></script>
    <script type="text/javascript" src="js/analytics.js"></script>


    <!-- datepicker-->
    <link type="text/css" href="assets/css/overcast/jquery-ui-1.9.2.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.9.2.custom.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.9.2.custom.min.js"></script>
    <!--<script type="text/javascript" src="js/jquery.ui.datepicker-zh-TW.js"></script>    -->
    <script type="text/javascript">
        $(function () {
            $("#datepicker_S").datepicker();
            $("#datepicker_E").datepicker();
        });
    </script>
    <!-- datepicker-->
    
</head>
<body>
    <form id="form1" runat="server">
        <!--<div aria-grabbed="undefined"-->
        <div>

            <br />

            <table border="1" style="width: 600px; height: auto; margin-left: auto; margin-right: auto">
                <tbody>

                    <tr>
                        <td  style="text-align: right; width: 10em">
                            <br />

                            <div style="width: 10em">
                                From : 
                                <asp:TextBox ID="datepicker_S" runat="server" Width="94px" align="center"></asp:TextBox>
                            </div>

                            <div style="width: 10em">
                                To :
                                <asp:TextBox ID="datepicker_E" runat="server" Width="94px"></asp:TextBox>
                            </div>
                            <div style="width: 10em">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="生成表格" />
                            </div>
                            <div style="width: 10em">
                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="AccessDataSource1" DataTextField="ass" DataValueField="ass" AutoPostBack="true" Visible="False" ></asp:DropDownList>
                            
                                <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                                                      DataFile="~/ds/RecipeTable.mdb" 
                                                      SelectCommand="SELECT ass FROM (SELECT TOP 1 'ALL' AS ass, '' AS b FROM a a_2 UNION ALL SELECT CCIRecipeNo &amp; ' ' &amp; CCIRecipeName AS ass, [CCIRecipeName ] AS b FROM a a_1) a ORDER BY b">
                                </asp:AccessDataSource>
                            
                            </div>
                        </td>

                        <td >
                            <div style="width: 150px">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="120" >
                                    <asp:ListItem Selected="True">Houly</asp:ListItem>
                                    <asp:ListItem>Daily</asp:ListItem>
                                    <asp:ListItem>Weekly</asp:ListItem>
                                    <asp:ListItem>Monthly</asp:ListItem>
                                </asp:RadioButtonList>

                            </div>
                        </td>

                        <td   style="vertical-align: central; margin-top: 0 ">
                             <div style ="width :400px">
                            <asp:RadioButtonList ID="RadioButtonList_LineModel" runat="server" Width="400"  >
                                <asp:ListItem >By Line (Dim / Defect / Fail Ratio)</asp:ListItem>
                                <asp:ListItem Selected="True">By model Defect + Fail Ratio</asp:ListItem>
                                <asp:ListItem >By model Dim</asp:ListItem>
                                <asp:ListItem >non Chipping model (By Line / all model)</asp:ListItem>
                                <asp:ListItem >CCC (By Line / all model)</asp:ListItem>
                            </asp:RadioButtonList>
                                 <asp:Button ID="Button1" runat="server" Text="For non-Chipping " TabIndex="1" Visible="False" />
                            </div> 
                        </td>
                        <td>
                            <asp:Button ID="Button_Submit" runat="server" Text="Submit" />
                        </td>
                    </tr>
                </tbody>
            </table>
  <%--          <div>
                &nbsp;</div>--%>
            <asp:Table ID="Table2" runat="server" Style="margin-left: auto; margin-right: auto"></asp:Table>
            <br />
            <br />
            <asp:Table ID="Table1" runat="server" Style="margin-left: auto; margin-right: auto"></asp:Table>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />

        </div>

    </form>

</body>
</html>

