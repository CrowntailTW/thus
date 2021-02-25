<%@ Page Language="VB" AutoEventWireup="false" CodeFile="INK.aspx.vb" Inherits="INK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>INK Monitor</title>
      <link rel="stylesheet" href="assets/css/main1.css" />
    <!--[if lte IE 8]><link rel="stylesheet" href="/assets/css/ie8.css" /><![endif]-->

    <script type="text/javascript" src="js/Chart.js"></script>
    <script type="text/javascript" src="js/Chart.min.js"></script>

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
    <div>
         <BR/>

        <table style="margin-left: auto; margin-right: auto">
            <tr>
                <td style ="text-align :center ;vertical-align :middle ">
                    <div style="width: 10em">
                        From : 
                        <asp:TextBox ID="datepicker_S" runat="server" Width="94px" align="center"></asp:TextBox>
                    </div>
                    <div style="width: 10em">
                        To :
                        <asp:TextBox ID="datepicker_E" runat="server" Width="94px"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div style="width: 150px">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="120" >
                            <asp:ListItem Selected="True">Houly</asp:ListItem>
                            <asp:ListItem>Daily</asp:ListItem>                        
                            <asp:ListItem>Monthly</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </td>
                <td style ="text-align :center ;vertical-align :middle ">
                    <div>
                    <asp:Button ID="Button1" runat="server" Text="Defect Chart" Width="200px" />
                    <asp:Button ID="Button2" runat="server" Text="Real Chart" Width="200px" />
                    </div>
                    <div>
                    <asp:Button ID="Button3" runat="server" Text="Hour v.s size (pinhole)" Width="200px" />
                    <asp:Button ID="Button4" runat="server" Text="Defect Position" Width="200px" Enabled="False" />
                    </div>
                </td>
            </tr>
        </table>      
        
        <asp:Table ID="Table1" runat="server" Style="margin-left: auto; margin-right: auto"></asp:Table>
        <asp:Table ID="Table2" runat="server" Style="margin-left: auto; margin-right: auto"></asp:Table>
    </div>
    </form>
</body>
</html>
