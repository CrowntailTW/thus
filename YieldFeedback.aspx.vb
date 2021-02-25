Imports System.Data.OleDb

Partial Class YieldFeedback

    Inherits System.Web.UI.Page
    Public Path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData\"

    Private Sub YieldFeedback_Load(sender As Object, e As EventArgs) Handles Me.Load



    End Sub


    Protected Sub Submit_Click(sender As Object, e As EventArgs) Handles Submit.Click

        Dim conn As New OleDbConnection
        Dim sqlcmd As OleDbCommand
        Dim mySql As String
        Dim connString As String
        Dim path As String = Path_ServerData & "Yield.mdb"

        mySql = ""
        mySql = mySql & " insert into A  values(""" & TextBox1.Text & """)"

        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
        conn = New OleDbConnection(connString)
        conn.Open()
        sqlcmd = New OleDbCommand(mySql, conn)
        sqlcmd.ExecuteNonQuery()

        conn.Close()
    End Sub
End Class
