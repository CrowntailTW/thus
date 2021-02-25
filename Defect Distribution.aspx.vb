
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class Defect_Distribution
    Inherits System.Web.UI.Page


    Dim myCla As New Class1

    Public path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData\"
    Public Eqp_matrix() As String = {"1", "2", "3", "4", "5" _
                                    , "6", "7", "8", "9", "A" _
                                    , "B", "C", "D", "E", "F"}

    Protected Sub Button_Submit_Click(sender As Object, e As EventArgs) Handles Button_Submit.Click

        Dim i, j, k, l As Integer
        Dim eqp_list As String = ""

        Dim eqp_shift As Integer
        Dim day_shift As Integer
        Dim PanelID As String
        Dim nn As Date = Now
        Dim S_Date, E_Date As Date
        Dim diff_date As Integer

        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim conn_string As String = ""
        Dim mySql As String = ""
        Dim myPath As String

        E_Date = DateAdd(DateInterval.Day, 1, DateSerial(CInt(Mid(datepicker_E.Text, 7, 4)), CInt(Mid(datepicker_E.Text, 1, 2)), CInt(Mid(datepicker_E.Text, 4, 2))))
        S_Date = DateSerial(CInt(Mid(datepicker_S.Text, 7, 4)), CInt(Mid(datepicker_S.Text, 1, 2)), CInt(Mid(datepicker_S.Text, 4, 2)))
        diff_date = Math.Abs(DateDiff(DateInterval.Day, E_Date, S_Date))

        For i = 0 To CheckBoxList1.Items.Count - 1
            If CheckBoxList1.Items(i).Selected Then
                eqp_list = eqp_list & Eqp_matrix(CheckBoxList1.Items(i).Value - 1)
            End If
        Next

        j = 0

        PanelID = TextBox1.Text

        mySql = ""
        mySql = mySql & " Select * from DefectT2"
        mySql = mySql & " Where PanelID in " & PanelID
        mySql = mySql & ""
        mySql = mySql & ""
        mySql = mySql & ""

        For eqp_shift = 0 To eqp_list.Length - 1
            For day_shift = 0 To diff_date - 1
                Dim date_tmp As Date = DateAdd(DateInterval.Day, day_shift, S_Date)
                myPath = path_ServerData & "CCT" & (Mid(eqp_list, eqp_shift + 1, 1)) & "00\" & Format(date_tmp, "yyyyMM") & "\" & Format(date_tmp, "dd") & "\" & Format(date_tmp, "yyyyMMdd") & ".mdb"

                If File.Exists(myPath) Then

                    conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & myPath & ";"
                    conn = New OleDbConnection(conn_string)
                    conn.Open()

                    adapter = New OleDbDataAdapter(mySql, conn)
                    adapter.Fill(ds)
                    adapter.Dispose()

                    tb = ds.Tables(0)
                    If tb.Rows.Count <> 0 Then
                        eqp_shift = eqp_list.Length + 30
                        day_shift = diff_date + 30
                    End If
                End If

            Next
        Next

    End Sub

End Class
