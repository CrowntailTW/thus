<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eEDS.aspx.vb" Inherits="eEDS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" DataSourceID="AccessDataSource1" EnableModelValidation="True" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="DD" HeaderText="DD" SortExpression="DD" />
                <asp:BoundField DataField="DDTT" HeaderText="DDTT" SortExpression="DDTT" />
                <asp:BoundField DataField="RecipeNo" HeaderText="RecipeNo" SortExpression="RecipeNo" />
                <asp:BoundField DataField="Tool" HeaderText="Tool" SortExpression="Tool" />
                <asp:BoundField DataField="PanelID" HeaderText="PanelID" SortExpression="PanelID" />
                <asp:BoundField DataField="Defect" HeaderText="Defect" SortExpression="Defect" />
                <asp:BoundField DataField="CFTFT" HeaderText="CFTFT" SortExpression="CFTFT" />
                <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                <asp:BoundField DataField="WorkID" HeaderText="WorkID" SortExpression="WorkID" />
                <asp:BoundField DataField="WorkName" HeaderText="WorkName" SortExpression="WorkName" />
            </Columns>
        </asp:GridView>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/ServerData/eEDS/201801GDeEDS.mdb" SelectCommand="SELECT * FROM [HistoryCheck]"></asp:AccessDataSource>
    
    </div>
    </form>
</body>
</html>
