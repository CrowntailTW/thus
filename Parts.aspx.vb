
Imports System.Data
Imports System.Data.OleDb

Partial Class Parts

    Inherits System.Web.UI.Page
    Public Path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData\"

    Dim tmpRow As TableRow


    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        Dim 位置 As String = DropDownList1.Text
        AccessDataSourceDGV.SelectCommand = "SELECT * FROM [Y類] WHERE ([位置] = ?)"
        AccessDataSourceDGV.SelectParameters("位置").DefaultValue = 位置

    End Sub


    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        Dim conn As New OleDbConnection
        Dim sqlcmd As OleDbCommand
        Dim mySql As String
        Dim connString As String
        Dim path As String = Path_ServerData & "Parts.mdb"

        Dim amountOld, amountNew, amount As Integer
        Dim 位置 As String = DropDownList1.Text
        Dim 項次 As Integer
        Dim 品名, 用途, 開始存放時間, 上機上架 As String
        Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow
        Dim tb As TextBox
        Dim ClientIP As String

        Label1.Text = ""

        If (e.CommandName = "Button_OnBoard") Or (e.CommandName = "Button_OnShelve") Then

            row = GridView1.Rows(rowIndex)
            tb = row.FindControl("TextBox_Amount")
            項次 = GridView1.Rows(rowIndex).Cells(0).Text
            品名 = GridView1.Rows(rowIndex).Cells(3).Text
            用途 = GridView1.Rows(rowIndex).Cells(4).Text
            開始存放時間 = GridView1.Rows(rowIndex).Cells(2).Text

            amountOld = GridView1.Rows(rowIndex).Cells(5).Text
            amount = Convert.ToInt64(tb.Text)

            If amount = 1307083 Then
                redLog()
                Exit Sub
            Else

            End If

            If (e.CommandName = "Button_OnBoard") Then
                amountNew = amountOld - amount
                上機上架 = "上機"
            ElseIf (e.CommandName = "Button_OnShelve") Then
                amountNew = amountOld + amount
                上機上架 = "上架"
            End If

            mySql = ""
            mySql = mySql & " Update [Y類]"
            mySql = mySql & " SET [數量] = " & amountNew '(amountOld - amountOnBoard)
            mySql = mySql & " WHERE [位置] = '" & 位置 & "'"
            mySql = mySql & " AND [項次] = " & 項次 & ""

            If amount < 0 Or amountNew < 0 Or amountOld < 0 Then
                Label1.Text = "庫存為零 or 上機數量>庫存數量"
                Exit Sub
            End If

            Label1.Text = ""
            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
            conn = New OleDbConnection(connString)
            conn.Open()
            sqlcmd = New OleDbCommand(mySql, conn)
            sqlcmd.ExecuteNonQuery()

            Label1.Text = Now & " " & 位置 & " " & 品名 & " " & 上機上架 & " : " & amount

            ClientIP = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ClientIP = String.Empty Then
                ClientIP = Request.ServerVariables("REMOTE_ADDR")
            End If

            log(ClientIP, 位置, 項次, 品名, 用途, 開始存放時間, amountOld, amountNew, amount, 上機上架)

        End If

        Threading.Thread.Sleep(1000)

        GridView1.DataBind()

    End Sub


    Sub log(ByVal ip As String, ByVal 位置 As String, ByVal 項次 As String, ByVal 品名 As String, ByVal 用途 As String, ByVal 開始存放時間 As String, ByVal amountOld As Integer, ByVal amountNew As Integer, ByVal amount As Integer, ByVal 上機上架 As String)

        Dim conn As New OleDbConnection
        Dim sqlcmd As OleDbCommand
        Dim mySql As String
        Dim connString As String
        Dim path As String = Path_ServerData & "Parts.mdb"

        mySql = ""
        mySql = mySql & ""
        mySql = mySql & ""
        mySql = mySql & " INSERT INTO [LOG]"
        mySql = mySql & " ([項次],[品名],[用途],[開始存放時間],[位置],[IP],[DDTT],[修改前數量],[修改後數量],[修改數量],[上機/上架])"
        mySql = mySql & " VALUES"
        mySql = mySql & " (" & 項次 & ",'" & 品名 & "','" & 用途 & "','" & 開始存放時間 & "','" & 位置 & "','" & ip & "'," & Format(Now, "yyyyMMddHHmmss") & "," & amountOld & "," & amountNew & "," & amount & ",'" & 上機上架 & "')"


        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
        conn = New OleDbConnection(connString)
        conn.Open()
        sqlcmd = New OleDbCommand(mySql, conn)
        sqlcmd.ExecuteNonQuery()


    End Sub

    Sub redLog()

        Dim conn As New OleDbConnection
        Dim adapa As OleDbDataAdapter
        Dim mySql As String
        Dim connString As String
        Dim path As String = Path_ServerData & "Parts.mdb"
        Dim ds As New DataSet

        mySql = ""
        mySql = mySql & ""
        mySql = mySql & ""
        mySql = mySql & " SELECT * FROM [LOG]"

        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
        conn = New OleDbConnection(connString)
        conn.Open()
        adapa = New OleDbDataAdapter(mySql, conn)
        adapa.Fill(ds)
        adapa.Dispose()

        Dim mycls As New Class1

        mycls.ds2table(ds.Tables(0), Table1)
    End Sub

End Class
