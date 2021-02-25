
Imports System.Data.OleDb
Imports System.Data
Imports System.IO

Partial Class CutInfo
    Inherits System.Web.UI.Page
    Public path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData\"


    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim S_date, E_date As Date

        If datepicker_E.Text <> "" And datepicker_S.Text <> "" Then
            E_date = New Date(Mid(datepicker_E.Text, 7, 4), Mid(datepicker_E.Text, 1, 2), Mid(datepicker_E.Text, 4, 2), 23, 59, 59)
            S_date = New Date(Mid(datepicker_S.Text, 7, 4), Mid(datepicker_S.Text, 1, 2), Mid(datepicker_S.Text, 4, 2), 0, 0, 0)
            mileage(S_date, E_date)
        End If
        update_mileage()

    End Sub

    Public Sub update_mileage()

        Dim i As Integer
        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim path As String
        Dim Sql As String
        Dim conn_string As String

        Dim myCla As New Class1

        Dim Datalabels() As String = New String() {"上刀", "下刀"}
        Dim Is_ratio() As Boolean = New Boolean() {False, False}
        Dim BColor() As String = New String() {"window.chartColors.blue", "window.chartColors.red"}
        Dim chartType() As String = New String() {"bar", "bar"}
        Dim yaxis() As String = New String() {"y1", "y1"}

        Dim Xlabel(14)() As String
        Dim data(1)() As Nullable(Of Double)

        Dim cstext As New StringBuilder()

        For i = 0 To Xlabel.Length - 1
            ReDim Xlabel(i)(1)
        Next
        For i = 0 To data.Length - 1
            ReDim data(i)(14)
        Next

        For i = 0 To 14

            Xlabel(i)(0) = "#" & num2eqp(i + 1)
            Xlabel(i)(1) = ""
            data(0)(i) = 0
            data(1)(i) = 0

            path = path_ServerData & "CCT" & num2eqp(i + 1) & "00\" & Format(Now, "yyyyMM") & "\" & Format(Now, "yyyyMM") & "PresureMileageInfo.mdb"

            If File.Exists(path) Then
                conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
                conn = New OleDbConnection(conn_string)
                conn.Open()

                Sql = " "
                Sql = Sql & " Select top 1 U_mileage,L_mileage,TimeA "
                Sql = Sql & " From History "
                Sql = Sql & " Order by (TimeA) desc  "

                ds.Reset()
                adapter = New OleDbDataAdapter(Sql, conn)
                adapter.Fill(ds)
                adapter.Dispose()

                tb = ds.Tables(0)

                If tb.Rows.Count > 0 Then
                    Xlabel(i)(1) = Mid(tb.Rows(0).Item("TimeA"), 9, 2) & ":" & Mid(tb.Rows(0).Item("TimeA"), 11, 2)

                    data(0)(i) = Convert.ToDouble(tb.Rows(0).Item("U_mileage")) '上刀輪mileage
                    data(1)(i) = Convert.ToDouble(tb.Rows(0).Item("L_mileage")) '下刀輪mileage
                End If

                conn.Close()

            End If

        Next

        cstext = New StringBuilder
        cstext.Append(myCla.toJStext_new("canvasX" & i + 1, _
                                          "Cut Cutter mileage", _
                                          Xlabel, _
                                          chartType, _
                                          Datalabels, _
                                          data, _
                                          Is_ratio, _
                                          BColor, _
                                          yaxis, _
                                          False))
        myCla.cstext2Canvas(Page, cstext, "canvasX" & i + 1, Table1)

        '''''Dim cell As New TableCell
        '''''Dim li As New Literal
        '''''Dim row As New TableRow
        '''''li.Text = " <canvas id=" & Chr(34) & "canvasX" & Chr(34) & " style=" & Chr(34) & "width:100%;height:100%" & Chr(34) & "></canvas>"
        '''''cell.Controls.Add(li)
        '''''row.Cells.Add(cell)
        '''''row.HorizontalAlign = HorizontalAlign.Center
        '''''row.Height = 400
        '''''Table1.Rows.Add(row)

        '''''Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp_mile", cstext.ToString, False)
    End Sub

    Protected Sub mileage(ByVal S_Date As Date, ByVal E_Date As Date)

        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim nn As Date = Now

        Dim conn_string, mySql As String
        Dim i, j, k, l As Integer
        Dim path As String
        Dim eqp_tmp As String
        Dim S_Date_String, E_Date_String As String
        Dim eqp_shift, month_shift, month_diff As Integer

        Dim mycla As New class1

        Dim Datalabels() As String = New String() {"U_mileage", "L_mileage"}
        Dim Is_ratio() As Boolean = New Boolean() {False, False}
        Dim BColor() As String = New String() {"window.chartColors.blueA", "window.chartColors.greenA"}
        Dim chartType() As String = New String() {"line", "line"}
        Dim yaxis() As String = New String() {"y1", "y1"}

        Dim data()() As Nullable(Of Double)


        Dim Xlabel()() As String

        Dim cstext As New StringBuilder()

        If datepicker_E.Text = "" Or datepicker_S.Text = "" Then
            Exit Sub
        Else
            S_Date_String = Format(S_Date, "yyyyMMddHH00")
            E_Date_String = Format(E_Date, "yyyyMMdd2359")
        End If
        E_Date_String = Format(E_Date, "yyyyMMdd2359")

        month_diff = DateDiff(DateInterval.Month, S_Date, E_Date)

        mySql = ""
        mySql = mySql & " Select TimeA,U_mileage,L_mileage,Eqp_tool"
        mySql = mySql & " From History "
        mySql = mySql & " Where val(TimeA) >= " & S_Date_String & " "
        mySql = mySql & " And   val(TimeA) <= " & E_Date_String & " "
        mySql = mySql & " Order by val(TimeA)"

        ds.Reset()

        For month_shift = 0 To month_diff

            Dim date_tmp As Date = DateAdd(DateInterval.Month, month_shift, S_Date)

            For eqp_shift = 1 To 15

                eqp_tmp = "CCT" & num2eqp(eqp_shift) & "00"

                path = path_ServerData & eqp_tmp & "\" & Format(date_tmp, "yyyyMM") & "\" & Format(date_tmp, "yyyyMM") & "PresureMileageInfo.mdb"

                If File.Exists(path) Then

                    conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
                    conn = New OleDbConnection(conn_string)
                    conn.Open()

                    adapter = New OleDbDataAdapter(mySql, conn)
                    adapter.Fill(ds)
                    adapter.Dispose()

                    conn.Close()
                End If
            Next
        Next

        Dim dv As DataView
        tb = ds.Tables(0)

        dv = tb.DefaultView
        Dim tb_eqp As DataTable = dv.ToTable(True, "Eqp_tool")

        Dim eqp_count As Integer = tb_eqp.Rows.Count
        Dim Xlabel_count As Integer

        Xlabel_count = DateDiff(DateInterval.Hour, S_Date, E_Date) + 1

        ReDim Xlabel(Xlabel_count - 1)

        Dim Xlabel_tmpdate As Date

        ReDim data(Datalabels.Length - 1)

        For i = 0 To Datalabels.Length - 1
            ReDim data(i)(Xlabel_count - 1)
        Next

        For i = 0 To Xlabel_count - 1


            Xlabel_tmpdate = DateAdd(DateInterval.Hour, i, S_Date)


            ReDim Xlabel(i)(1)
            Xlabel(i)(0) = Format(Xlabel_tmpdate, "MM/dd") & " " & Format(Xlabel_tmpdate, "HH")
        Next

        For eqp_shift = 0 To eqp_count - 1

            eqp_tmp = tb_eqp.Rows(eqp_shift).Item("eqp_tool")

            ReDim data(Datalabels.Length - 1)

            For i = 0 To Datalabels.Length - 1
                ReDim data(i)(Xlabel_count - 1)
            Next

            For i = 0 To tb.Rows.Count - 1

                If tb.Rows(i).Item("eqp_tool") = eqp_tmp Then

                    Dim T As String ' = tb.Rows(i).Item("TimeA")
                    Dim TT As Date '= New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)


                    T = tb.Rows(i).Item("TimeA")
                    TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)

                    Dim X0 As String = Format(TT, "MM/dd")
                    Dim X1 As String = Format(TT, "HH")

                    l = DateDiff(DateInterval.Hour, S_Date, TT)
                    data(1)(l) = Convert.ToDouble(tb.Rows(i).Item(Datalabels(1)))
                    data(0)(l) = Convert.ToDouble(tb.Rows(i).Item(Datalabels(0)))
                End If
            Next
            cstext = New StringBuilder
            cstext.Append(myCla.toJStext_new("canvasX" & eqp_tmp, eqp_tmp & " Mileage Chart", _
                                                           Xlabel, _
                                                           chartType, _
                                                           Datalabels, _
                                                           data, _
                                                           Is_ratio, _
                                                           BColor, _
                                                           yaxis, _
                                                           False, , , ))

            myCla.cstext2Canvas(Page, cstext, "canvasX" & eqp_tmp, Table1)

            '''''Dim cell As New TableCell
            '''''Dim li As New Literal
            '''''Dim row As New TableRow
            '''''li.Text = " <canvas id=" & Chr(34) & "canvasX" & eqp_tmp & Chr(34) & "></canvas>"
            '''''cell.Controls.Add(li)
            '''''row.Cells.Add(cell)
            '''''row.HorizontalAlign = HorizontalAlign.Center
            '''''row.Height = 400
            '''''Table1.Rows.Add(row)
            '''''Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp" & eqp_tmp, cstext.ToString, False)

        Next


        Try

        Catch ex As Exception

        End Try

    End Sub

    Public Function num2eqp(ByVal num As Integer) As String

        If num > 15 Or num < 0 Then Return ""

        If num <= 9 Then Return num.ToString

        Select Case num
            Case 10
                Return "A"
            Case 11
                Return "B"
            Case 12
                Return "C"
            Case 13
                Return "D"
            Case 14
                Return "E"
            Case 15
                Return "F"

        End Select

    End Function

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        ''''Table1.Width = 400
        '''''Table1.Caption = "動態生成表格"
        ''''Table1.GridLines = GridLines.Both '//設置儲存格的框線
        ''''Table1.HorizontalAlign = HorizontalAlign.Left '//設置表格相對頁面居中
        ''''Table1.CellPadding = 20 ' //設置儲存格內間距
        ''''Table1.CellSpacing = 20 ' //設置儲存格之間的距離
        ''''Table1.Visible = True  '

        ''''Table1.GridLines = GridLines.None  '//設置儲存格的框線
        ''''Table1.HorizontalAlign = HorizontalAlign.Center  '//設置表格相對頁面居中
        ''''Table1.CellPadding = 20 ' //設置儲存格內間距
        ''''Table1.CellSpacing = 20 ' //設置儲存格之間的距離
        ''''Table1.Visible = True  '

        datepicker_S.Text = IIf(datepicker_S.Text = "", Format(DateAdd(DateInterval.Day, -1, Now()), "MM/dd/yyyy"), datepicker_S.Text)
        datepicker_E.Text = IIf(datepicker_E.Text = "", Format(Now(), "MM/dd/yyyy"), datepicker_E.Text)
    End Sub
End Class
