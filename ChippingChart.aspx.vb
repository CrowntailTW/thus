
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class ChippingChart
    Inherits System.Web.UI.Page

    Public myCla As New Class1
    Public Path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData"
    Public arrayRecipeNoEtE() As String = New String() {"02", "1A", "22", "31", "72", "84"}
    Public stringRecipeNoEtE As String = "('02','1A','22','31','72','84')"

    Dim byDuration() As String = New String() {"Hourly", "Daily", "Weekly", "Monthly"}

    Private Sub ChippingChart_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim S_date As String = Context.Request.QueryString("S_date")
        Dim E_date As String = Context.Request.QueryString("E_date")
        Dim ByDur As String = Context.Request.QueryString("ByDur")




    End Sub



    'make all non Chipping model as a group
    Protected Sub DefectDim_new_nonChiping(ByVal ByDur As String, ByVal S_Date As Date, ByVal E_Date As Date)
        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim Target As Double
        Dim nn As Date = Now

        '------------------------------
        Dim IsEtE As Boolean = False
        '------------------------------

        Dim conn_string, mySql As String
        Dim i, j, k, l As Integer
        Dim path As String

        Dim S_Date_String, E_Date_String As String
        Dim month_shift, month_diff As Integer

        Dim Datalabels() As String = New String() {"Defect_Ratio", "Dim_Ratio", "Fail_Ratio", "Move"}
        Dim Is_ratio() As Boolean = New Boolean() {True, True, True, False}
        Dim BColor() As String = New String() {"window.chartColors.redA", "window.chartColors.blueA", "window.chartColors.yellowA", "window.chartColors.green"}
        Dim chartType() As String = New String() {"line", "line", "line", "bar"}
        Dim yaxis() As String = New String() {"y1", "y1", "y1", "y2"}

        Dim data()() As Nullable(Of Double)

        Dim tb_eqp As DataTable
        Dim eqp_count As Integer
        Dim Xlabel()() As String
        Dim Xlabel_count As Integer

        Dim cstext As New StringBuilder()

        S_Date_String = Format(S_Date, "yyyyMMddHH00")
            E_Date_String = Format(E_Date, "yyyyMMdd2359")

        month_diff = DateDiff(DateInterval.Month, S_Date, E_Date)

        Select Case ByDur

            Case byDuration(0) 'hourly
                mySql = ""
                mySql = mySql & " Select * "
                mySql = mySql & " From history "
                mySql = mySql & " Where val(TimeA) >= " & S_Date_String & " "
                mySql = mySql & " And val(TimeA) <= " & E_Date_String & " "
                mySql = mySql & " And   RecipeNo in " & stringRecipeNoEtE & ""
                mySql = mySql & " Order by Eqp_tool,val(TimeA) "

            Case byDuration(1) 'Daily'month
                mySql = ""
                If IsEtE Then
                    mySql = mySql & " Select D as DDDTTT,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim30)/sum(Move)  As Dim_Ratio30, sum(Fail)/sum(Move)As Fail_Ratio,eqp_tool"
                Else
                    mySql = mySql & " Select D as DDDTTT,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                End If
                mySql = mySql & " From history "
                mySql = mySql & " Where val(TimeA) >= " & S_Date_String & " "
                mySql = mySql & " And   val(TimeA) <= " & E_Date_String & " "
                mySql = mySql & " And   RecipeNo in " & stringRecipeNoEtE & ""
                mySql = mySql & " Group by Eqp_tool,D"
                mySql = mySql & " Order by Eqp_tool,D"


            Case byDuration(2) 'weekly
                mySql = ""
                If IsEtE Then
                    mySql = mySql & " Select FORMAT(cdate(format( D, ""####/##/##"")),""WW"") as DDDTTT,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim30)/sum(Move)  As Dim_Ratio30, sum(Fail)/sum(Move)As Fail_Ratio,eqp_tool"
                Else
                    mySql = mySql & " Select FORMAT(cdate(format( D, ""####/##/##"")),""WW"") as DDDTTT,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                End If
                mySql = mySql & " From history "
                mySql = mySql & " Where val(TimeA) >= " & S_Date_String & " "
                mySql = mySql & " And   val(TimeA) <= " & E_Date_String & " "
                mySql = mySql & " And   RecipeNo in " & stringRecipeNoEtE & ""
                mySql = mySql & " Group by Eqp_tool,FORMAT(cdate(format( D, ""####/##/##"")),""WW"")"
                mySql = mySql & " Order by Eqp_tool,FORMAT(cdate(format( D, ""####/##/##"")),""WW"")"

            Case byDuration(3) 'month
                mySql = ""
                If IsEtE Then
                    mySql = mySql & " Select mid(D,1,6) as DDDTTT,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim30)/sum(Move)  As Dim_Ratio30, sum(Fail)/sum(Move)As Fail_Ratio,eqp_tool"
                Else
                    mySql = mySql & " Select mid(D,1,6) as DDDTTT,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                End If
                mySql = mySql & " From history "
                mySql = mySql & " Where val(TimeA) >= " & S_Date_String & " "
                mySql = mySql & " And   val(TimeA) <= " & E_Date_String & " "
                mySql = mySql & " And   RecipeNo in " & stringRecipeNoEtE & ""
                mySql = mySql & " Group by Eqp_tool,mid(D,1,6)"
                mySql = mySql & " Order by Eqp_tool,mid(D,1,6)"
        End Select

        ds.Reset()

        For month_shift = 0 To month_diff

            Dim date_tmp As Date = DateAdd(DateInterval.Month, month_shift, S_Date)
            path = Path_ServerData & "" & Format(date_tmp, "yyyyMM") & "DefectDimInfo.mdb"

            If IsEtE Then
                path = Path_ServerData & "" & "DefectDimInfo_EtE.mdb"
            Else
                path = Path_ServerData & "" & Format(date_tmp, "yyyyMM") & "DefectDimInfo.mdb"
            End If
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

        Dim dv As DataView
        tb = ds.Tables(0)

        dv = tb.DefaultView
        tb_eqp = dv.ToTable(True, "Eqp_tool")
        eqp_count = tb_eqp.Rows.Count

        Select Case ByDur
            Case byDuration(0) 'hourly
                Xlabel_count = DateDiff(DateInterval.Hour, S_Date, E_Date) + 1
            Case byDuration(1) 'Daily
                Xlabel_count = DateDiff(DateInterval.Day, S_Date, E_Date) + 1
            Case byDuration(2) 'weekly
                Xlabel_count = DateDiff(DateInterval.WeekOfYear, S_Date, E_Date) + 1
            Case byDuration(3) 'monthly
                Xlabel_count = DateDiff(DateInterval.Month, S_Date, E_Date) + 1
        End Select


        ReDim Xlabel(Xlabel_count - 1)

        Dim Xlabel_tmpdate As Date

        ReDim data(Datalabels.Length - 1)

        For i = 0 To Datalabels.Length - 1
            ReDim data(i)(Xlabel_count - 1)
        Next

        For i = 0 To Xlabel_count - 1

            ReDim Xlabel(i)(1)

            Select Case ByDur
                Case byDuration(0) 'hourly
                    Xlabel_tmpdate = DateAdd(DateInterval.Hour, i, S_Date)
                    Xlabel(i)(0) = Format(Xlabel_tmpdate, "MM/dd")
                    Xlabel(i)(1) = Format(Xlabel_tmpdate, "HH")

                Case byDuration(1) 'Daily
                    Xlabel_tmpdate = DateAdd(DateInterval.Day, i, S_Date)
                    Xlabel(i)(0) = Format(Xlabel_tmpdate, "MM/dd")

                Case byDuration(2) 'weekly
                    Xlabel_tmpdate = DateAdd(DateInterval.WeekOfYear, i, S_Date)
                    Dim ass As Date = Xlabel_tmpdate
                    Xlabel(i)(0) = "W" & Mid(Format(Xlabel_tmpdate, "yy"), 2, 1) & DatePart(DateInterval.WeekOfYear, Xlabel_tmpdate)

                Case byDuration(3) 'monthly
                    Xlabel_tmpdate = DateAdd(DateInterval.Month, i, S_Date)
                    Xlabel(i)(0) = Format(Xlabel_tmpdate, "yyyy/MM")

            End Select

        Next

        For eqp_shift = 0 To eqp_count - 1

            Dim eqp_tmp As String = tb_eqp.Rows(eqp_shift).Item("eqp_tool")

            ReDim data(Datalabels.Length - 1)

            For i = 0 To Datalabels.Length - 1
                ReDim data(i)(Xlabel_count - 1)
            Next

            For i = 0 To tb.Rows.Count - 1

                If tb.Rows(i).Item("eqp_tool") = eqp_tmp Then

                    Dim T As String
                    Dim TT As Date

                    Select Case ByDur
                        Case byDuration(0) 'hourly
                            T = tb.Rows(i).Item("TimeA")
                            TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)
                            l = DateDiff(DateInterval.Hour, S_Date, TT)

                        Case byDuration(1) 'Daily
                            T = tb.Rows(i).Item("DDDTTT")
                            TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), 0, 0, 0)
                            l = DateDiff(DateInterval.Day, S_Date, TT)

                        Case byDuration(2) 'weekly
                            T = tb.Rows(i).Item("DDDTTT")
                            l = CInt(T) - DatePart(DateInterval.WeekOfYear, S_Date)

                        Case byDuration(3) 'monthly
                            T = tb.Rows(i).Item("DDDTTT")
                            TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), 1, 0, 0, 0)
                            l = DateDiff(DateInterval.Month, S_Date, TT)

                    End Select

                    If IsEtE Then
                        data(1)(l) = Convert.ToDouble(tb.Rows(i).Item(Datalabels(1) & "Dim 30"))
                    Else
                        data(1)(l) = Convert.ToDouble(tb.Rows(i).Item(Datalabels(1)))
                    End If

                    data(0)(l) = Convert.ToDouble(tb.Rows(i).Item(Datalabels(0)))
                    data(2)(l) = Convert.ToDouble(tb.Rows(i).Item(Datalabels(2)))
                    data(3)(l) = IIf(Convert.ToDouble(tb.Rows(i).Item(Datalabels(3))) = 0, Nothing, Convert.ToDouble(tb.Rows(i).Item(Datalabels(3))))
                    'Xlabel(l)(1) = tb.Rows(i).Item("RecipeNo")
                    'Target = myCla.getTarget(tb.Rows(i).Item("RecipeNo"))

                End If
            Next
            cstext = New StringBuilder
            '
            'cstext.Append(myCla.toJStext_highChart("canvasX" & eqp_tmp, eqp_tmp & " Defect Dim Fail Ratio  Chart", _
            'cstext.Append(myCla.toJStext_new("canvasX" & eqp_tmp, eqp_tmp & " Defect Dim Fail Ratio  Chart", _
            cstext.Append(myCla.toJStext_new("canvasX" & eqp_tmp & "nonChipping", eqp_tmp & " Defect Dim Fail Ratio  Chart", _
                                                        Xlabel, _
                                                        chartType, _
                                                        Datalabels, _
                                                        data, _
                                                        Is_ratio, _
                                                        BColor, _
                                                        yaxis, _
                                                        True, IIf(IsEtE, 100, 35), , ))

            Dim cell As New TableCell
            Dim li As New Literal

            li.Text = "<canvas id=" & Chr(34) & "canvasX" & eqp_tmp & "nonChipping" & Chr(34) & "></canvas>"
            'li.Text = " <div id=" & Chr(34) & "canvasX" & eqp_tmp & Chr(34) & " ></div>"
            cell.Controls.Add(li)
            cell.Width = 1000

            Dim row As New TableRow
            row.Cells.Add(cell)
            row.HorizontalAlign = HorizontalAlign.Center

            If eqp_count >= 6 Then
                row.Height = 450
            Else
                row.Height = 400
            End If

            'Table2.Rows.Add(row)

            Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp" & eqp_tmp, cstext.ToString, False)

        Next

        Try

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub DefectDim_ByModel_new_nonChipping(ByVal ByDur As String, ByVal S_Date As Date, ByVal E_Date As Date)

        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim tb_RecipeNo As DataTable
        Dim Target As Double
        Dim nn As Date = Now

        Dim conn_string, mySql As String
        Dim i, j, k, l As Integer
        Dim path As String
        Dim isEtE As Boolean = False
        'Dim myrow As TableRow
        'Dim mycell As TableCell
        Dim S_Date_String, E_Date_String As String
        Dim month_shift, month_diff As Integer

        Dim Recipe_count As Integer
        Dim Recipe_matrix() As String, RecipeNo_Matrix() As String

        Dim datalabels() As String
        Dim Is_ratio() As Boolean
        Dim BColor_ind() As String = New String() {"window.chartColors.green", "window.chartColors.red", "window.chartColors.blue", "window.chartColors.orange", "window.chartColors.yellow", "window.chartColors.purple", "window.chartColors.grey", "window.chartColors.yellowgreen"}
        Dim BColor() As String
        Dim chartType() As String
        Dim yaxis() As String

        Dim Xlabel()() As String
        Dim data()() As Nullable(Of Double)

        Dim cstext As New StringBuilder()


        S_Date_String = Format(S_Date, "yyyyMMddHH00")
        E_Date_String = Format(E_Date, "yyyyMMddHH00")


        month_diff = DateDiff(DateInterval.Month, S_Date, E_Date)

        mySql = ""
        mySql = mySql & " Select RecipeNo,Recipe"
        mySql = mySql & " From History"
        mySql = mySql & " Where val(timeA) >= " & S_Date_String
        mySql = mySql & " And   val(timeA) <= " & E_Date_String
        mySql = mySql & " Group by RecipeNo,Recipe "
        mySql = mySql & " Order by RecipeNo,Recipe "
        Try
            For month_shift = 0 To month_diff
                Dim date_tmp As Date = DateAdd(DateInterval.Month, month_shift, S_Date)

                If isEtE Then
                    path = Path_ServerData & "" & "DefectDimInfo_EtE.mdb"
                Else
                    path = Path_ServerData & "" & Format(date_tmp, "yyyyMM") & "DefectDimInfo.mdb"
                End If
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

            tb = ds.Tables(0)

            tb_RecipeNo = tb.DefaultView.ToTable(True, "recipeNo")

            Recipe_count = tb_RecipeNo.Rows.Count
            ReDim Recipe_matrix(Recipe_count - 1)
            ReDim RecipeNo_Matrix(Recipe_count - 1)

            For i = 0 To tb_RecipeNo.Rows.Count - 1
                RecipeNo_Matrix(i) = tb_RecipeNo.Rows(i).Item("RecipeNo")
                For j = 0 To tb.Rows.Count - 1
                    If tb.Rows(j).Item("RecipeNo") = tb_RecipeNo.Rows(i).Item("RecipeNo") Then
                        Recipe_matrix(i) = Trim(tb.Rows(j).Item("Recipe"))
                    End If
                Next
            Next


            Select Case ByDur
                Case byDuration(0) 'Hourly
                    mySql = ""

                    If isEtE Then
                        mySql = mySql & " Select * "
                    Else

                        mySql = mySql & " Select TimeA,RecipeNo,D, Move ,(Defect + Fail)/Move As Defect_N_Fail_Ratio,Defect/Move As Defect_Ratio, Dim/Move As Dim_Ratio, Fail/Move As Fail_Ratio,eqp_tool"
                    End If

                    mySql = mySql & " From history "
                    mySql = mySql & " Where RecipeNo in " & stringRecipeNoEtE & ""
                    mySql = mySql & " And val(TimeA) >= " & S_Date_String & " "
                    mySql = mySql & " And val(TimeA) <= " & E_Date_String & " "
                    mySql = mySql & " And   RecipeNo in " & stringRecipeNoEtE & ""
                    mySql = mySql & " Order by Eqp_tool,val(TimeA) "

                Case byDuration(1) 'Daily
                    mySql = ""
                    If isEtE Then
                        mySql = mySql & " Select RecipeNo,D,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim30)/sum(Move)  As Dim_Ratio30, sum(Fail)/sum(Move)As Fail_Ratio,eqp_tool"
                    Else
                        'mySql = mySql & " Select RecipeNo,D,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                        mySql = mySql & " Select RecipeNo,D,sum(Move) As Move ,((sum(Defect) + sum(Fail))/sum(Move)) As Defect_N_Fail_Ratio,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                    End If
                    mySql = mySql & " From history "
                    mySql = mySql & " Where RecipeNo in " & stringRecipeNoEtE & ""
                    mySql = mySql & " And val(TimeA) >= " & S_Date_String & " "
                    mySql = mySql & " And val(TimeA) <= " & E_Date_String & " "
                    mySql = mySql & " And   RecipeNo in " & stringRecipeNoEtE & ""
                    mySql = mySql & " Group by Eqp_tool,D,RecipeNo"
                    mySql = mySql & " Order by Eqp_tool,D"

                Case byDuration(2) 'Weekly

                    mySql = ""
                    If isEtE Then
                        mySql = mySql & " Select RecipeNo, FORMAT(cdate(format( D, ""####/##/##"")),""WW"") as DDDTTT,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim30)/sum(Move)  As Dim_Ratio30, sum(Fail)/sum(Move)As Fail_Ratio,eqp_tool"
                    Else
                        'mySql = mySql & " Select RecipeNo,D,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                        mySql = mySql & " Select RecipeNo, FORMAT(cdate(format( D, ""####/##/##"")),""WW"") as DDDTTT,sum(Move) As Move ,((sum(Defect) + sum(Fail))/sum(Move)) As Defect_N_Fail_Ratio,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                    End If
                    mySql = mySql & " From history "
                    mySql = mySql & " Where RecipeNo in " & stringRecipeNoEtE & ""
                    mySql = mySql & " And val(TimeA) >= " & S_Date_String & " "
                    mySql = mySql & " And val(TimeA) <= " & E_Date_String & " "
                    mySql = mySql & " And   RecipeNo in " & stringRecipeNoEtE & ""
                    mySql = mySql & " Group by Eqp_tool, FORMAT(cdate(format( D, ""####/##/##"")),""WW""),RecipeNo"
                    mySql = mySql & " Order by Eqp_tool, FORMAT(cdate(format( D, ""####/##/##"")),""WW"")"

                Case byDuration(3) 'Month

                    mySql = ""
                    If isEtE Then
                        mySql = mySql & " Select RecipeNo, mid(D,1,6) as DDDTTT,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim30)/sum(Move)  As Dim_Ratio30, sum(Fail)/sum(Move)As Fail_Ratio,eqp_tool"
                    Else
                        mySql = mySql & " Select RecipeNo, mid(D,1,6) as DDDTTT,sum(Move) As Move ,((sum(Defect) + sum(Fail))/sum(Move)) As Defect_N_Fail_Ratio,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                    End If
                    mySql = mySql & " From history "
                    mySql = mySql & " Where RecipeNo in " & stringRecipeNoEtE & ""
                    mySql = mySql & " And val(TimeA) >= " & S_Date_String & " "
                    mySql = mySql & " And val(TimeA) <= " & E_Date_String & " "
                    mySql = mySql & " And   RecipeNo in " & stringRecipeNoEtE & ""
                    mySql = mySql & " Group by Eqp_tool, mid(D,1,6),RecipeNo"
                    mySql = mySql & " Order by Eqp_tool, mid(D,1,6)"

            End Select

            ds.Reset()

            For month_shift = 0 To month_diff
                Dim date_tmp As Date = DateAdd(DateInterval.Month, month_shift, S_Date)
                path = Path_ServerData & "" & Format(date_tmp, "yyyyMM") & "DefectDimInfo.mdb"
                If isEtE Then
                    path = Path_ServerData & "" & "DefectDimInfo_EtE.mdb"
                Else
                    path = Path_ServerData & "" & Format(date_tmp, "yyyyMM") & "DefectDimInfo.mdb"
                End If

                If File.Exists(path) Then

                    conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
                    conn = New OleDbConnection(conn_string)
                    conn.Open()

                    adapter = New OleDbDataAdapter(mySql, conn)
                    adapter.Fill(ds)
                    adapter.Dispose()

                End If

                conn.Close()

            Next

            Dim dv As DataView
            tb = ds.Tables(0)

            dv = tb.DefaultView
            Dim tb_eqp As DataTable = dv.ToTable(True, "Eqp_tool")
            Dim eqp_count As Integer = tb_eqp.Rows.Count
            Dim Xlabel_count As Integer

            Select Case ByDur
                Case byDuration(0) 'Hourly
                    Xlabel_count = DateDiff(DateInterval.Hour, S_Date, E_Date) + 1

                Case byDuration(1) 'Daily
                    Xlabel_count = DateDiff(DateInterval.Day, S_Date, E_Date) + 1

                Case byDuration(2) 'Weekly
                    Xlabel_count = DateDiff(DateInterval.Weekday, S_Date, E_Date) + 1

                Case byDuration(3) 'Month
                    Xlabel_count = DateDiff(DateInterval.Month, S_Date, E_Date) + 1

            End Select

            ReDim datalabels(eqp_count * 2 - 1)
            ReDim Is_ratio(eqp_count * 2 - 1)
            ReDim BColor(eqp_count * 2 - 1)
            ReDim chartType(eqp_count * 2 - 1)
            ReDim yaxis(eqp_count * 2 - 1)
            ReDim data(eqp_count * 2 - 1)

            ReDim Xlabel(Xlabel_count - 1)

            For i = 0 To eqp_count - 1
                datalabels(i) = Replace(tb_eqp.Rows(i).Item("Eqp_tool"), "CA", "") & " Ratio"
                datalabels(i + eqp_count) = Replace(tb_eqp.Rows(i).Item("Eqp_tool"), "CA", "") & " Move"

                Is_ratio(i) = True
                Is_ratio(i + eqp_count) = False

                BColor(i) = BColor_ind(i Mod BColor_ind.Length) & "A"
                BColor(i + eqp_count) = BColor_ind(i Mod BColor_ind.Length)

                yaxis(i) = "y1"
                yaxis(i + eqp_count) = "y2"

                chartType(i) = "line"
                chartType(i + eqp_count) = "bar"

                ReDim data(i)(Xlabel_count - 1)
                ReDim data(i + eqp_count)(Xlabel_count - 1)
            Next

            Dim Xlabel_tmpdate As Date

            For i = 0 To Xlabel_count - 1
                ReDim Xlabel(i)(1)

                Select Case ByDur
                    Case byDuration(0) 'Hourly
                        Xlabel_tmpdate = DateAdd(DateInterval.Hour, i, S_Date)
                        Xlabel(i)(0) = Format(Xlabel_tmpdate, "MM/dd") & " " & Format(Xlabel_tmpdate, "HH")
                        Xlabel(i)(1) = Format(Xlabel_tmpdate, "HH")

                    Case byDuration(1) 'Day
                        Xlabel_tmpdate = DateAdd(DateInterval.Day, i, S_Date)
                        Xlabel(i)(0) = Format(Xlabel_tmpdate, "MM/dd")

                    Case byDuration(2) 'Weekly

                        Xlabel_tmpdate = DateAdd(DateInterval.WeekOfYear, i, S_Date)
                        Dim ass As Date = Xlabel_tmpdate
                        Xlabel(i)(0) = "W" & Mid(Format(Xlabel_tmpdate, "yy"), 2, 1) & DatePart(DateInterval.WeekOfYear, Xlabel_tmpdate)

                    Case byDuration(3) 'Month
                        Xlabel_tmpdate = DateAdd(DateInterval.Month, i, S_Date)
                        Xlabel(i)(0) = Format(Xlabel_tmpdate, "yyyy/MM")

                End Select

            Next

            For i = 0 To tb.Rows.Count - 1

                Dim T As String ' = tb.Rows(i).Item("TimeA")
                Dim TT As Date '= New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)

                Select Case ByDur
                    Case byDuration(0) 'Hourly
                        T = tb.Rows(i).Item("TimeA")
                        TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)

                    Case byDuration(1) 'Daily
                        T = tb.Rows(i).Item("D")
                        TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), 0, 0, 0)

                    Case byDuration(2) 'Weekly
                        T = tb.Rows(i).Item("DDDTTT")

                    Case byDuration(3) 'Month
                        T = tb.Rows(i).Item("DDDTTT")
                        TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), 1, 0, 0, 0)

                End Select

                For j = 0 To eqp_count - 1

                    If tb.Rows(i).Item("eqp_tool") = tb_eqp.Rows(j).Item("eqp_tool") Then

                        Select Case ByDur
                            Case byDuration(0) 'Hourly
                                k = DateDiff(DateInterval.Hour, S_Date, TT)

                            Case byDuration(1) 'Daily
                                k = DateDiff(DateInterval.Day, S_Date, TT)

                            Case byDuration(2) 'Weekly
                                'Dim as
                                k = CInt(T) - DatePart(DateInterval.WeekOfYear, S_Date)   'IIf(Isdaily, DateDiff(DateInterval.Day, S_Date, TT), DateDiff(DateInterval.Hour, S_Date, TT))

                            Case byDuration(3) 'Month
                                k = DateDiff(DateInterval.Month, S_Date, TT)

                        End Select

                        If isEtE Then
                            data(j)(k) = Convert.ToDouble(tb.Rows(i).Item("Dim_Ratio30"))
                        Else
                            data(j)(k) = Convert.ToDouble(tb.Rows(i).Item("Defect_N_Fail_Ratio"))
                        End If

                        data(j + eqp_count)(k) = IIf(Convert.ToDouble(tb.Rows(i).Item("Move")) = 0, Nothing, Convert.ToDouble(tb.Rows(i).Item("Move")))

                        j = eqp_count + 30

                    End If
                Next
            Next

            cstext.Append(myCla.toJStext_new("canvasX" & "nonChipping", "nonChipping model Defect Ratio Chart", _
                                               Xlabel, _
                                               chartType, _
                                               datalabels, _
                                               data, _
                                               Is_ratio, _
                                               BColor, _
                                               yaxis, _
                                               True, IIf(isEtE, 100, 35), , ))

            Dim cell As New TableCell
            Dim li As New Literal

            li.Text = " <canvas id=" & Chr(34) & "canvasX" & "nonChipping" & Chr(34) & "></canvas>"
            'li.Text = " <div id=" & Chr(34) & "canvasX" & RecipeNo_Matrix(Recipe_shift) & Chr(34) & "></diV>></div>"

            cell.Controls.Add(li)
            cell.Width = 1000
            Dim row As New TableRow
            row.Cells.Add(cell)
            row.HorizontalAlign = HorizontalAlign.Center

            If eqp_count >= 6 Then
                row.Height = 450
            Else
                row.Height = 400
            End If
            'Table2.Rows.Add(row)

            Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp nonChipping", cstext.ToString, False)

        Catch ex As Exception

        End Try

    End Sub



End Class
