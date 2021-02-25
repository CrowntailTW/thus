Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Public Class Class_Sql


    Public Shared Function GetCPLTB(ByVal mySql As String) As DataTable

        Dim conn As New SqlConnection
        Dim adapt As New SqlDataAdapter
        Dim ds As New DataSet

        conn.ConnectionString = "Data Source=CACPL106\SQLEXPRESS;Initial Catalog=AOI;Connect Timeout=1200;User ID=User1;Password=user1"
        conn.Open()
        adapt.SelectCommand = New SqlCommand(mySql, conn)
        adapt.Fill(ds)
        adapt.Dispose()
        conn.Dispose()
        conn.Close()

        Return ds.Tables(0)


    End Function

    Public Shared Function CreateConn(ByVal path As String) As OleDbConnection

        Dim SqlConn As OleDbConnection
        Dim SqlConn_String As String

        SqlConn_String = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & path
        SqlConn = New OleDbConnection(SqlConn_String)

        Return SqlConn

    End Function

    Public Shared Function GetDB(ByVal conn As OleDbConnection, ByVal sql As String) As DataSet

        Dim ds As New DataSet
        Dim OleDbAdapter As OleDbDataAdapter

        'conn.Open()
        Try
            OleDbAdapter = New OleDbDataAdapter(sql, conn)
            OleDbAdapter.Fill(ds)
            OleDbAdapter.Dispose()
        Catch ex As Exception

        End Try


        'conn.Close()
        Return ds

    End Function
    Public Shared Function GetDB(ByVal conn As OleDbConnection, ByVal sql As String, ByVal ds As DataSet) As DataSet


        Dim OleDbAdapter As OleDbDataAdapter

        Try
            OleDbAdapter = New OleDbDataAdapter(sql, conn)
            OleDbAdapter.Fill(ds)
            OleDbAdapter.Dispose()
        Catch ex As Exception

        End Try

        Return ds

    End Function
    Public Shared Sub CreateTable(ByVal Path As String, ByVal tablename As String, colname() As String, coltype() As String)

        Dim loCatalog As Object = CreateObject("ADOX.Catalog")
        Dim NewconnetionString As String
        Dim conn As OleDbConnection
        Dim oledbCmd As OleDbCommand
        Dim sql As String
        Dim i As Integer

        NewconnetionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & Path & "';Jet OLEDB:Database Password=''"
        Try

            If Not File.Exists(Path) Then
                loCatalog.Create(NewconnetionString)
                loCatalog.activeconnection.close()
            End If


            If File.Exists(Path) Then

                sql = ""
                sql = sql & " Create Table " & tablename & " "
                sql = sql & " ( "

                For i = 0 To colname.Length - 1
                    sql = sql & "[" & colname(i) & "] " & coltype(i) & IIf(i <> colname.Length - 1, ",", "")
                Next

                sql = sql & " ) "

                conn = CreateConn(Path)
                conn.Open()

                oledbCmd = New OleDbCommand(sql, conn)
                oledbCmd.ExecuteNonQuery()

            End If

        Catch ex As Exception

        End Try
    End Sub

End Class
