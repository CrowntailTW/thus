
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class 段差NG_ID
    Inherits System.Web.UI.Page

    Public Path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData\"
    Dim mcls As New Class1



    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        datepicker_S.Text = IIf(datepicker_S.Text = "", "08/27/2017", datepicker_S.Text)
        datepicker_E.Text = IIf(datepicker_E.Text = "", Format(Now(), "MM/dd/yyyy"), datepicker_E.Text)
        S_H.Text = IIf(S_H.Text = "", "16", S_H.Text)
        S_M.Text = IIf(S_M.Text = "", "00", S_M.Text)
        E_H.Text = IIf(E_H.Text = "", "20", E_H.Text)
        E_M.Text = IIf(E_M.Text = "", "00", E_M.Text)


    End Sub


    Protected Sub Submit_Click(sender As Object, e As EventArgs) Handles Submit.Click

        Dim S_Date As Date
        Dim E_date As Date

        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable

        Dim nn As Date = Now
        Dim eqp() As String = New String() {"CCT700", "CCT400", "CCTA00", "CCTB00"}
        Dim conn_string, mySql As String
        Dim i, j As Integer
        Dim path As String

        Dim date_shift, eqp_shift, date_diff As Integer

        Dim myrow As TableRow
        Dim mycell As TableCell
        Dim S_Date_String, E_Date_String As String
        Dim Target As Integer

        Try


            E_date = DateAdd(DateInterval.Day, 1, DateSerial(CInt(Mid(datepicker_E.Text, 7, 4)), CInt(Mid(datepicker_E.Text, 1, 2)), CInt(Mid(datepicker_E.Text, 4, 2))))
            S_Date = DateSerial(CInt(Mid(datepicker_S.Text, 7, 4)), CInt(Mid(datepicker_S.Text, 1, 2)), CInt(Mid(datepicker_S.Text, 4, 2)))

            If Not (IsNumeric(S_H.Text) And IsNumeric(S_M.Text) And IsNumeric(E_H.Text) And IsNumeric(E_M.Text)) Then

                MsgBox("Time 務必填入數字" & vbCrLf & "結束程序", MsgBoxStyle.OkOnly)
                Exit Sub
            End If

            S_Date = New DateTime(S_Date.Year, S_Date.Month, S_Date.Day, S_H.Text, S_M.Text, 0)
            E_date = New DateTime(E_date.Year, E_date.Month, E_date.Day, E_H.Text, E_M.Text, 0)

            date_diff = DateDiff(DateInterval.Day, S_Date, E_date)
            If date_diff < 0 Then Exit Sub


            S_Date_String = Format(S_Date, "yyyyMMddHHmmss")
            E_Date_String = Format(E_date, "yyyyMMddHHmmss")

            mySql = ""
            mySql = mySql & " Select eqpXXX as eqp , mid(Time,1,12) as DateTime_ , mid(Time,1,8) as Date_ , mid(Time,9,4) as Time_  , PanelID "
            mySql = mySql & " From DefectT1 "
            mySql = mySql & " Where   mid(NGitem,3,1) in ('0','2') "
            mySql = mySql & " And   "
            mySql = mySql & "      ( "
            mySql = mySql & "      val(A) > val(B) "
            mySql = mySql & "      Or "
            mySql = mySql & "      val(E) > val(F) "
            mySql = mySql & "      Or "
            mySql = mySql & "      val(G) > val(H) "
            mySql = mySql & "      ) "
            mySql = mySql & " And   RecipeNo in ('02')"
            mySql = mySql & " And   val(Time) >= " & S_Date_String
            mySql = mySql & " And   val(Time) <= " & E_Date_String
            mySql = mySql & " Order by mid(Time,1,12)"


            For eqp_shift = 0 To eqp.Length - 1
                For date_shift = 0 To date_diff
                    Dim date_tmp As Date = DateAdd(DateInterval.Day, date_shift, S_Date)
                    path = Path_ServerData & eqp(eqp_shift) & "\" & Format(date_tmp, "yyyyMM") & "\" & Format(date_tmp, "dd") & "\" & Format(date_tmp, "yyyyMMdd") & ".mdb"

                    If File.Exists(path) Then
                        conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
                        conn = New OleDbConnection(conn_string)
                        conn.Open()

                        Dim tmp_sql As String = Replace(mySql, "eqpXXX", "'" & eqp(eqp_shift) & "'")
                        adapter = New OleDbDataAdapter(tmp_sql, conn)

                        adapter.Fill(ds)
                        conn.Close()
                    End If
                Next
            Next
            tb = ds.Tables(0)

            mcls.ds2table(tb, Table1)
        Catch ex As Exception

        End Try
    End Sub

End Class

