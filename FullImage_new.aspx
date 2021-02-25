<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FullImage_new.aspx.vb" Inherits="FullImage_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Full Image</title>

    <link rel="stylesheet" href="assets/css/main.css" />

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
    <style type="text/css">
        .auto-style1 {
            height: 82px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button2" runat="server" Text="Button" />
            <br />
            <table border="1" style="width: 40%; height: auto; margin-left: auto; margin-right: auto">
                <tr align="center">

                    <td style="vertical-align: middle">
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
                        <div style="width: 12em; text-align: right">
                            From : 
                        <asp:TextBox ID="datepicker_S" runat="server" Width="94px"></asp:TextBox>
                        </div>
                        <div style="width: 12em; text-align: right">
                            To:                 
                        <asp:TextBox ID="datepicker_E" runat="server" Width="94px"></asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <table class="asd" style="width: 100%;">
                            <tr>
                                <td colspan="3" style="text-align: center; vertical-align: middle">

                                    <asp:CheckBox ID="CB1" Text="上CCD" runat="server" TabIndex="1" />
                                    <asp:CheckBox ID="CB3" Text="下CCD" runat="server" TabIndex="3" />

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; vertical-align: middle" class="auto-style1">
                                    <asp:CheckBox ID="CB4" Text="上" runat="server" TabIndex="4" /><br />
                                    <asp:CheckBox ID="CB6" Text="下" runat="server" TabIndex="6" />
                                </td>
                                <td class="auto-style1">
                                    <asp:Image ID="Image2" runat="server" />
                                </td>
                                <td style="text-align: center; vertical-align: middle" class="auto-style1">
                                    <asp:CheckBox ID="CB0" Text="上" runat="server" /><br />
                                    <asp:CheckBox ID="CB2" Text="下" runat="server" TabIndex="2" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center; vertical-align: middle">
                                    <asp:CheckBox ID="CB5" Text="上CCD" runat="server" TabIndex="5" />
                                    <asp:CheckBox ID="CB7" Text="下CCD" runat="server" TabIndex="7" />
                                </td>
                            </tr>

                        </table>
                    </td>
                    <td style="text-align: left; vertical-align: middle ;width: 5em;">ID:
                    <asp:TextBox style="white-space: pre-wrap" ID="TextBox1" runat="server" TextMode="MultiLine" Width="8em" Height="15em" BorderStyle="Dashed" BorderWidth="1px" ></asp:TextBox>

                    </td>
                    <td style="align-content: center; width: 5em; vertical-align: middle">
                        <div style="width: 15em; height: 100%">
                            <asp:Button ID="Button1" runat="server" Text="Submit" Height="4em" width= "15em" TabIndex='0' />
                            <br/>
                            <asp:Button ID="Button_Download" runat="server" Text="Download" Height="4em" width= "15em" TabIndex='1' />
                            <br/>
                            <asp:Button ID="Button3" runat="server" Height="4em" width= "15em" Text="Button" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan ="4">
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>

          
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>

        </div>
    </form>
</body>
</html>
