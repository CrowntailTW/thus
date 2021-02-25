<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Parts.aspx.vb" Inherits="Parts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>VL6AH1 Y類備品</title>
    
    <style type="text/css">
        .auto-style1 {
            height: 22px;
            width: 900px;
        }
        .auto-style2 {
            width: 900px;
        }
    </style>
    
</head>
<body >
    <form id="form1" runat="server" style="margin-left :auto ; margin-right :auto ">
    <div style="margin-left :auto ; margin-right :auto ">
        <table border ="1" style="margin-left :auto ; margin-right :auto  ; width : 900px">
            <tr >
                <td colspan ="2" class="auto-style1" style ="margin-left :auto ; margin-right :auto ; text-align  :center  ">
                    Y類備品管理
                </td>
            </tr>
            <tr>
                <td style="text-align :center " class="auto-style2">

                    <asp:DropDownList ID = "DropDownList1" runat="server" DataSourceID="AccessDataSourceDDL" DataTextField="位置" DataValueField="位置" AutoPostBack="True" Width="100px"  ></asp:DropDownList> 

                 
                    <asp:GridView ID="GridView1" runat="server" EnableModelValidation="True" BorderWidth ="1px" AutoGenerateColumns="False" DataSourceID="AccessDataSourceDGV" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" CellPadding="3" Width ="886px"  AutoPostBack="True" >
                        <Columns>                            
                            <asp:BoundField DataField="項次" HeaderText="項次" SortExpression="項次" ItemStyle-Width="50px"/>
                            <asp:BoundField DataField="位置" HeaderText="位置" SortExpression="位置" ItemStyle-Width="100px"/>
                            <asp:BoundField DataField="開始存放時間" HeaderText="開始存放時間" SortExpression="開始存放時間" ItemStyle-Width="200px"/>
                            <asp:BoundField DataField="品名" HeaderText="品名" SortExpression="品名" ItemStyle-Width="300px"/>
                            <asp:BoundField DataField="用途" HeaderText="用途" SortExpression="用途" ItemStyle-Width="300px"/>
                            <asp:BoundField DataField="數量" HeaderText="庫存" SortExpression="數量" ItemStyle-Width="50px"/>                           
                            
                            <asp:TemplateField HeaderText="數量" ItemStyle-Width="50px">
                                <ItemTemplate> 
                                    <asp:TextBox ID="TextBox_Amount" runat="server" Text ="1" style="width:inherit" conmmandname="TextBox_Amount" CommandArgument ="<%# CType(Container, GridViewRow).RowIndex %>" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="上機" ItemStyle-Width="50px" >
                                <ItemTemplate>
                                    <asp:Button ID="Button_OnBoard" runat="server" Text="上機"  CommandName="Button_OnBoard" AutoPostBack="True" CommandArgument ="<%# CType(Container, GridViewRow).RowIndex %>"  BorderStyle="Dashed" />                                    
                                    <asp:Button ID="Button_OnShelve" runat="server" Text="上架"  CommandName="Button_OnShelve" AutoPostBack="True" CommandArgument ="<%# CType(Container, GridViewRow).RowIndex %>"  BorderStyle="Dashed" />                                    
                                </ItemTemplate>
                            </asp:TemplateField>                           
                               
                        </Columns>

                        <FooterStyle BackColor="White" ForeColor="#AA0066" />
                        <HeaderStyle BackColor="#f2849e" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />

                    </asp:GridView>

                    <asp:AccessDataSource ID="AccessDataSourceDGV" runat="server" DataFile="E:\WebSite_new_WithCanvas\ServerData\Parts.mdb" SelectCommand="SELECT * FROM [Y類] WHERE ([位置] = ?)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-" Name="位置" Type="String" />
                        </SelectParameters>
                    </asp:AccessDataSource>

                    <asp:AccessDataSource ID="AccessDataSourceDDL" runat="server" DataFile="E:\WebSite_new_WithCanvas\ServerData\Parts.mdb" SelectCommand="SELECT DISTINCT [位置] FROM [Y類]"></asp:AccessDataSource>
                                    
                </td>               
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Table ID="Table1" runat="server" BorderColor="#FFCCCC" BorderStyle="None" BorderWidth="1px" GridLines="Horizontal" Height="24px" Width="882px"></asp:Table>                  
                </td>                
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
