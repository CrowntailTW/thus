
Imports System.Data.OleDb
Imports System.Data
Imports System.Data.DataSetExtensions
Imports System.IO
Imports System.Math
Imports System.ComponentModel
Imports System.Drawing

Partial Class EtE_Dim30_Monitor

    Inherits System.Web.UI.Page

    Public myCla As New Class1
    Public Path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData\"

    Public IF_LOGIN As Boolean
    'Public IF_DEF_CAL, IF_DEF_CAL_ALL, IF_NODATA, isRecip_Data_initail_UseCCTRecipe As Boolean
    Public IF_NODATA, isRecip_Data_initail_UseCCTRecipe As Boolean
    Public isRefreshDefectChart, isRefreshDefectChart_ForAEG As Boolean
    Public UpdatingErrInfo As Boolean = False
    Public IsInitial As Boolean

    Public RECIPE_NO As String
    Public RECIPE_NAME As String
    Public Fromtime, Totime As Date

    Public U_mileage As Integer
    Public L_mileage As Integer

    'Public Err_Cnt
    Dim time_stamp As Integer = 0
    'Dim time_next
    Dim Panel_Count As Integer

    Public Const Exlude_Percent_1 As Integer = 0 ' 15
    Public Const Exlude_Percent_2 As Integer = 0 '(100 * Exlude_Percent_1) / (100 - Exlude_Percent_1)
    Public Const Exlude_Err_value As Integer = 350

    Public Const PASS_WORD As String = "8310"

    Public Const NotEnoughSheet As Integer = 5 '多少sheet以下不採用
    Public Const RefreshTime As Integer = 5 'Mins
    Public Const Cut_eff_time As Integer = 6 ' CutLog 回推時間
    Public timeduration As Integer = 12 'Defect Dim Chart

    Dim TotalIn24H As Integer
    Dim TotalDefectIn24H As Integer
    Dim TotalIn30In24H As Integer
    Dim TotalOut30In24H As Integer

    Dim Ratio_Defect() As Double
    Dim Ratio_Dim() As Double
    Dim Ratio_Fail() As Double
    Dim Total() As Double
    Dim TotalInspc() As Double '每小時In spc
    Dim TotalInSpc4Hr() As Double '每四小時In spc
    Dim Ratio_DimSpc()() As Double '各Spc各時間Ratio
    Dim Ratio_段差() As Double '各時間段差Ratio
    Public M270DTN01Line() As Integer = {4, 7, 8, 10, 11, 14} ' 8 A  '有run M270DTN01/M240UAN02 的線別 

    Dim chip_Fail_ratio() As Double

    Public E2ERecipe() As String '= {"73", "1C", "61", "02", "1A", "1B"}
    Public E2E_index As Integer = -1
    Public Spc()()() As Integer
    Public 段差Recipe() As String '= {"73", "1C", "02"}
    Public 段差_index As Integer = -1
    Public 段差Spc As Integer = 50
    Public 分貨Recipe() As String ' = {"73","02"}
    Public 分貨Recipe_Name() As String ' = {"73","02"}
    'Public 分貨_index As Integer = -1
    Public 分貨Spc()()() As Integer

    Dim chip_Defect_count() As Integer
    Dim chip_Dim_count() As Integer
    Dim chip_Fail_count() As Integer

    Dim chip_Defect_ratio() As Double 'onsheet
    Dim chip_Dim_ratio() As Double


    Public Const shift_W As Integer = 3 'sheet weight
    Public Const shift_OLB_W As Integer = 5 'OLB width weight
    Public Const closemsg As String = "非工程人員請勿隨意關閉" & Chr(13) & "確認是否關閉程式? " & Chr(13) & "回查關閉人員"

    Public ERR_RECIPE_DATE As Date
    Public CN As String = Environ("ComputerName")

    Public PATH_IntelICCC As String = "C:\Intel\Logs\IntelICCC.ini"
    Public PATH_IntelIGFX As String = "C:\Intel\Logs\IntelGFX.log"
    Public Const NUM As Integer = 256
    Dim HD = Environment.GetLogicalDrives
    Dim Date_buffer As Date = "2016/12/31"
    Dim Date_ini As Date = "2018/12/31"

    'Dim
    Dim pl_Dim As Pen = New Pen(Color.LightSeaGreen, 2)
    Dim Bs_Dim As SolidBrush = Brushes.DarkGreen
    Dim fs_Dim As Font = New Font("Arial", 8, FontStyle.Regular)
    'Fail
    Dim pl_Fail As Pen = New Pen(Color.Orange, 2)
    Dim Bs_Fail As SolidBrush = Brushes.DarkOrange
    Dim fs_Fail As Font = New Font("Arial", 8, FontStyle.Regular)
    'Defect
    Dim pl_Defect As Pen = New Pen(Color.IndianRed, 2)
    Dim Bs_Defect As SolidBrush = Brushes.DarkRed
    Dim fs_Defect As Font = New Font("Arial", 8, FontStyle.Regular)
    '段差
    Dim pl_段差 As Pen = New Pen(Color.FromArgb(150, Color.MidnightBlue), 2)
    Dim Bs_段差 As SolidBrush = New SolidBrush(Color.FromArgb(225, Color.MidnightBlue))
    'Dim pl_段差 As Pen = New Pen(Color.LightCoral, 2)
    'Dim Bs_段差 As SolidBrush = Brushes.Coral
    Dim fs_段差 As Font = New Font("Arial", 8, FontStyle.Regular)


    Public BsBar As SolidBrush = Brushes.YellowGreen
    Public plmaxtri() As Pen = {pl_Dim, pl_Fail, pl_Defect, pl_段差}
    Public Bsmaxtri() As SolidBrush = {Bs_Dim, Bs_Fail, Bs_Defect, Bs_段差}
    Public Fsmaxtri() As Font = {fs_Dim, fs_Fail, fs_Defect, fs_段差}



    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Table1.Width = 400
        'Table1.Caption = "動態生成表格"
        Table1.GridLines = GridLines.Both '//設置儲存格的框線
        Table1.HorizontalAlign = HorizontalAlign.Left '//設置表格相對頁面居中
        Table1.CellPadding = 20 ' //設置儲存格內間距
        Table1.CellSpacing = 20 ' //設置儲存格之間的距離
        Table1.Visible = True  '

        Table1.GridLines = GridLines.None  '//設置儲存格的框線
        Table1.HorizontalAlign = HorizontalAlign.Center  '//設置表格相對頁面居中
        Table1.CellPadding = 20 ' //設置儲存格內間距
        Table1.CellSpacing = 20 ' //設置儲存格之間的距離
        Table1.Visible = True  '

        datepicker_S.Text = IIf(datepicker_S.Text = "", Format(DateAdd(DateInterval.Day, -1, Now()), "MM/dd/yyyy"), datepicker_S.Text)
        datepicker_E.Text = IIf(datepicker_E.Text = "", Format(Now(), "MM/dd/yyyy"), datepicker_E.Text)
        'datepicker_S.Text = datepicker_E.Text ' Format(Now, "MM/dd/yyyy")

    End Sub

    Function img() As Image
        Dim b As Bitmap = New Bitmap(400, 400)
        Dim g As Graphics = Graphics.FromImage(b)

        g.DrawLine(Pens.Black, 0, 0, 400, 400)
        b.Save(Path_ServerData & "text.jpg")

    End Function

    Protected Sub Button_Click(sender As Object, e As EventArgs) Handles EtE_Hourly_ByLine.Click, EtE_Daily_ByLine.Click, Nor_Hourly_ByLine.Click, Nor_Daily_ByLine.Click, EtE_Houly_model.Click, EtE_Daily_model.Click, Nor_Hour_model.Click, Nor_Daily_model.Click, Button_M24U2_段差.Click, Button_M24U2Loose.Click

        Dim S_date, E_date As Date
        Dim NN As Date = Now

        E_date = New Date(Mid(datepicker_E.Text, 7, 4), Mid(datepicker_E.Text, 1, 2), Mid(datepicker_E.Text, 4, 2), 23, 59, 59)
        S_date = New Date(Mid(datepicker_S.Text, 7, 4), Mid(datepicker_S.Text, 1, 2), Mid(datepicker_S.Text, 4, 2), 0, 0, 0)

        If Format(E_date, "yyyyMMdd") = Format(NN, "yyyyMMdd") Then
            E_date = New Date(Mid(datepicker_E.Text, 7, 4), Mid(datepicker_E.Text, 1, 2), Mid(datepicker_E.Text, 4, 2), NN.Hour, 59, 59)
        End If


        Select Case sender.TabIndex

            Case 1
                DefectDim_new(False, True, S_date, E_date)
            Case 2
                DefectDim_new(True, True, S_date, E_date)
            Case 3
                DefectDim_new(False, False, S_date, E_date)
            Case 4
                DefectDim_new(True, False, S_date, E_date)


            Case 5
                DefectDim_ByModel_new(False, True, S_date, E_date)
            Case 6
                DefectDim_ByModel_new(True, True, S_date, E_date)

            Case 7
                DefectDim_ByModel_new(False, False, S_date, E_date)
            Case 8
                DefectDim_ByModel_new(True, False, S_date, E_date)

            Case 98
                DefectDim_ByModel_new_M24U2Loose(False, False, S_date, E_date)
            Case 99
                DefectDim_M240UAN02(True, S_date, E_date)
        End Select


    End Sub


    Protected Sub DefectDim_M240UAN02(ByVal Isdaily As Boolean, ByVal S_Date As Date, ByVal E_Date As Date)

        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim Target As Integer
        Dim nn As Date = Now
        Dim date_shift, eqp_shift As Integer
        Dim date_diff As Integer
        Dim conn_string, sql As String
        Dim i, j, k, l As Integer
        Dim path As String
        Dim eqp() As String = New String() {"CCT700", "CCT400", "CCTA00", "CCTB00"}
        Dim myrow As TableRow
        Dim mycell As TableCell
        Dim S_Date_String, E_Date_String As String

        Dim datalabels() As String
        Dim Is_ratio() As Boolean
        Dim BColor_ind() As String = New String() {"window.chartColors.green", "window.chartColors.red", "window.chartColors.blue", "window.chartColors.orange", "window.chartColors.yellow", "window.chartColors.purple", "window.chartColors.grey", "window.chartColors.yellowgreen"}
        Dim BColor() As String
        Dim chartType() As String
        Dim yaxis() As String

        Dim Xlabel()() As String
        Dim data()() As Nullable(Of Double)

        Dim cstext As New StringBuilder()

        date_diff = DateDiff(DateInterval.Day, S_Date, E_Date)
        If date_diff < 0 Then Exit Sub

        Try

            sql = ""
            sql = sql & " Select eqpXXX as eqp,mid(Time,1,10) as DDTT,mid(Time,1,8) as Date_T,mid(Time,9,2) as Time_T  ,(count(panelID) *1 )as move ,sum(iif(   (Val(B) - Val(A) > 0)and( Val(F) - Val(E) > 0)and(Val(H) - Val(G) > 0)      ,1,0))as Cnt,round((cnt/move *100),2)as OKRatio"
            sql = sql & " From DefectT1"
            sql = sql & " Where   mid(NGitem,3,1) in ('0','2')"
            sql = sql & " And   RecipeNo in ('02')"
            sql = sql & " Group by mid(Time,1,8),mid(Time,9,2),mid(Time,1,10)"
            sql = sql & " Order by eqpXXX,mid(Time,1,8),mid(Time,9,2)"
            sql = sql & " "

            For date_shift = 0 To date_diff
                Dim date_tmp As Date = DateAdd(DateInterval.Day, date_shift, S_Date)
                For eqp_shift = 0 To eqp.Length - 1

                    path = Path_ServerData & eqp(eqp_shift) & "\" & Format(date_tmp, "yyyyMM") & "\" & Format(date_tmp, "dd") & "\" & Format(date_tmp, "yyyyMMdd") & ".mdb"

                    If File.Exists(path) Then
                        conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
                        conn = New OleDbConnection(conn_string)
                        conn.Open()

                        Dim tmp_sql As String = Replace(sql, "eqpXXX", "'" & eqp(eqp_shift) & "'")
                        adapter = New OleDbDataAdapter(tmp_sql, conn)

                        adapter.Fill(ds)
                        conn.Close()
                    End If
                Next
            Next
            tb = ds.Tables(0)

            If 1 = 1 Then
                Dim tb_eqp As DataTable = tb.DefaultView.ToTable(True, "eqp")
                Dim tb_DDTT As DataTable = tb.DefaultView.ToTable(True, "DDTT")


                ReDim datalabels(tb_eqp.Rows.Count * 2 - 1) ' Bar Chart name
                ReDim data(tb_eqp.Rows.Count * 2 - 1) ' Bar Chart Value
                ReDim chartType(tb_eqp.Rows.Count * 2 - 1) 'Bar Chart type
                ReDim Is_ratio(tb_eqp.Rows.Count * 2 - 1) '
                ReDim BColor(tb_eqp.Rows.Count * 2 - 1)
                ReDim yaxis(tb_eqp.Rows.Count * 2 - 1)

                ReDim Xlabel(tb_DDTT.Rows.Count - 1)

                For j = 0 To tb_DDTT.Rows.Count - 1

                    ReDim Xlabel(j)(1)

                    If j = 0 Then
                        Xlabel(j)(0) = Mid(tb_DDTT.Rows(j).Item(0), 5, 2) & "/" & Mid(tb_DDTT.Rows(j).Item(0), 7, 2)
                    ElseIf Mid(tb_DDTT.Rows(j).Item(0), 5, 2) & "/" & Mid(tb_DDTT.Rows(j).Item(0), 7, 2) <> Mid(tb_DDTT.Rows(j - 1).Item(0), 5, 2) & "/" & Mid(tb_DDTT.Rows(j - 1).Item(0), 7, 2) Then
                        Xlabel(j)(0) = Mid(tb_DDTT.Rows(j).Item(0), 5, 2) & "/" & Mid(tb_DDTT.Rows(j).Item(0), 7, 2)
                    Else
                        Xlabel(j)(0) = ""
                    End If
                    Xlabel(j)(0) = Mid(tb_DDTT.Rows(j).Item(0), 5, 2) & "/" & Mid(tb_DDTT.Rows(j).Item(0), 7, 2)
                    Xlabel(j)(1) = Mid(tb_DDTT.Rows(j).Item(0), 9, 2)

                Next

                For i = 0 To tb_eqp.Rows.Count - 1

                    datalabels(i) = tb_eqp.Rows(i).Item(0) & "OK Ratio"
                    datalabels(i + tb_eqp.Rows.Count) = tb_eqp.Rows(i).Item(0)

                    Is_ratio(i) = True
                    Is_ratio(i + tb_eqp.Rows.Count) = False

                    BColor(i) = BColor_ind(i Mod BColor_ind.Length) & "A"
                    BColor(i + tb_eqp.Rows.Count) = BColor_ind(i Mod BColor_ind.Length)

                    chartType(i) = "line"
                    chartType(i + tb_eqp.Rows.Count) = "bar"

                    yaxis(i) = "y1"
                    yaxis(i + tb_eqp.Rows.Count) = "y2"

                    ReDim data(i)(tb_DDTT.Rows.Count - 1)
                    ReDim data(i + tb_eqp.Rows.Count)(tb_DDTT.Rows.Count - 1)

                    For k = 0 To tb.Rows.Count - 1
                        For j = 0 To tb_DDTT.Rows.Count - 1

                            If (tb.Rows(k).Item("DDTT") = tb_DDTT.Rows(j).Item(0)) And (tb.Rows(k).Item("eqp") = datalabels(i + tb_eqp.Rows.Count)) Then

                                data(i + tb_eqp.Rows.Count)(j) = Convert.ToDouble(tb.Rows(k).Item("move"))
                                data(i)(j) = (tb.Rows(k).Item("OKRatio") / 100)
                                j = tb_DDTT.Rows.Count - 1 + 30

                            End If
                        Next
                    Next
                Next

                cstext.Append(myCla.toJStext_new("canvasX", "M240UAN02 齊邊段差 OK Ratio Chart", _
                                                   Xlabel, _
                                                   chartType, _
                                                   datalabels, _
                                                   data, _
                                                   Is_ratio, _
                                                   BColor, _
                                                   yaxis, _
                                                   True, 100, 70, ))

                Dim cell As New TableCell
                Dim li As New Literal
                'li.Text = " <canvas id=" & Chr(34) & "canvasX" & Chr(34) & " style=" & Chr(34) & "width:100%;height:100%" & Chr(34) & "></canvas>"
                li.Text = " <canvas id=" & Chr(34) & "canvasX" & Chr(34) & " ></canvas>"
                cell.Controls.Add(li)

                Dim row As New TableRow
                row.Cells.Add(cell)
                row.HorizontalAlign = HorizontalAlign.Center
                row.Height = 450

                Table2.Rows.Add(row)

                Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp_M240", cstext.ToString, False)


            End If

            myCla.ds2table(tb, Table1)


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DefectDim_new(ByVal Isdaily As Boolean, ByVal IsEtE As Boolean, ByVal S_Date As Date, ByVal E_Date As Date)

        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim Target As Double
        Dim nn As Date = Now

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


        Dim Xlabel()() As String

        Dim cstext As New StringBuilder()

        If datepicker_E.Text = "" Or datepicker_S.Text = "" Then
            Exit Sub
        Else
            S_Date_String = Format(S_Date, "yyyyMMddHH00")
            E_Date_String = Format(E_Date, "yyyyMMdd2359")
        End If

        month_diff = DateDiff(DateInterval.Month, S_Date, E_Date)

        If Isdaily Then
            mySql = ""
            If IsEtE Then
                mySql = mySql & " Select D,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim30)/sum(Move)  As Dim_Ratio30, sum(Fail)/sum(Move)As Fail_Ratio,eqp_tool,RecipeNo"
            Else
                mySql = mySql & " Select D,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool,RecipeNo"
            End If
            mySql = mySql & " From history "
            mySql = mySql & " Where val(TimeA) >= " & S_Date_String & " "
            mySql = mySql & " And   val(TimeA) <= " & E_Date_String & " "
            mySql = mySql & " Group by Eqp_tool,D,RecipeNo"
            mySql = mySql & " Order by Eqp_tool,D"
        Else
            mySql = ""
            mySql = mySql & " Select * "
            mySql = mySql & " From history "
            mySql = mySql & " Where val(TimeA) >= " & S_Date_String & " "
            mySql = mySql & " And val(TimeA) <= " & E_Date_String & " "
            mySql = mySql & " Order by Eqp_tool,val(TimeA) "
        End If

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
        Dim tb_eqp As DataTable = dv.ToTable(True, "Eqp_tool")
        Dim eqp_count As Integer = tb_eqp.Rows.Count
        Dim Xlabel_count As Integer

        If Isdaily Then
            Xlabel_count = DateDiff(DateInterval.Day, S_Date, E_Date) + 1
        Else
            Xlabel_count = DateDiff(DateInterval.Hour, S_Date, E_Date) + 1
        End If

        ReDim Xlabel(Xlabel_count - 1)

        Dim Xlabel_tmpdate As Date

        ReDim data(Datalabels.Length - 1)

        For i = 0 To Datalabels.Length - 1
            ReDim data(i)(Xlabel_count - 1)
        Next

        For i = 0 To Xlabel_count - 1

            If Isdaily Then
                Xlabel_tmpdate = DateAdd(DateInterval.Day, i, S_Date)
            Else
                Xlabel_tmpdate = DateAdd(DateInterval.Hour, i, S_Date)
            End If

            ReDim Xlabel(i)(1)
            Xlabel(i)(0) = Format(Xlabel_tmpdate, "MM/dd") & " " & IIf(Isdaily, "", Format(Xlabel_tmpdate, "HH"))
        Next



        For eqp_shift = 0 To eqp_count - 1

            Dim eqp_tmp As String = tb_eqp.Rows(eqp_shift).Item("eqp_tool")

            ReDim data(Datalabels.Length - 1)

            For i = 0 To Datalabels.Length - 1
                ReDim data(i)(Xlabel_count - 1)
            Next

            For i = 0 To tb.Rows.Count - 1

                If tb.Rows(i).Item("eqp_tool") = eqp_tmp Then

                    Dim T As String ' = tb.Rows(i).Item("TimeA")
                    Dim TT As Date '= New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)

                    If Isdaily Then
                        T = tb.Rows(i).Item("D")
                        TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), 0, 0, 0)
                    Else
                        T = tb.Rows(i).Item("TimeA")
                        TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)
                    End If

                    Dim X0 As String = Format(TT, "MM/dd")
                    Dim X1 As String = Format(TT, "HH")

                    '  For j = 0 To tb.Rows.Count - 1

                    l = IIf(Isdaily, DateDiff(DateInterval.Day, S_Date, TT), DateDiff(DateInterval.Hour, S_Date, TT))

                    If IsEtE Then
                        data(1)(l) = Convert.ToDouble(tb.Rows(i).Item(Datalabels(1) & "30"))
                    Else
                        data(1)(l) = Convert.ToDouble(tb.Rows(i).Item(Datalabels(1)))
                    End If

                    data(0)(l) = Convert.ToDouble(tb.Rows(i).Item(Datalabels(0)))
                    data(2)(l) = Convert.ToDouble(tb.Rows(i).Item(Datalabels(2)))
                    data(3)(l) = IIf(Convert.ToDouble(tb.Rows(i).Item(Datalabels(3))) = 0, Nothing, Convert.ToDouble(tb.Rows(i).Item(Datalabels(3))))
                    Xlabel(l)(1) = tb.Rows(i).Item("RecipeNo")
                    Target = myCla.getTarget(tb.Rows(i).Item("RecipeNo"))

                    '  Next
                End If
            Next
            '
            'cstext.Append(myCla.toJStext_highChart("canvasX" & eqp_tmp, eqp_tmp & " Defect Dim Fail Ratio  Chart", _
            cstext.Append(myCla.toJStext_new("canvasX" & eqp_tmp, eqp_tmp & " Defect Dim Fail Ratio  Chart", _
                                                           Xlabel, _
                                                           chartType, _
                                                           Datalabels, _
                                                           data, _
                                                           Is_ratio, _
                                                           BColor, _
                                                           yaxis, _
                                                           True, IIf(IsEtE, 100, 35), Target, Target))

            Dim cell As New TableCell
            Dim li As New Literal

            li.Text = " <canvas id=" & Chr(34) & "canvasX" & eqp_tmp & Chr(34) & "></canvas>"
            'li.Text = " <div id=" & Chr(34) & "canvasX" & eqp_tmp & Chr(34) & "></div>"
            cell.Controls.Add(li)
            Dim row As New TableRow
            row.Cells.Add(cell)
            row.HorizontalAlign = HorizontalAlign.Center

            If eqp_count >= 6 Then
                row.Height = 450
            Else
                row.Height = 400
            End If

            Table2.Rows.Add(row)

            Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp" & eqp_tmp, cstext.ToString, False)

            'cstext = New StringBuilder
            'cstext.Append(myCla.JStext6H("canvasX" & eqp_tmp, "test text"))
            'Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp" & eqp_tmp, cstext.ToString, False)

            If CheckBox1.Checked Then
                myCla.ds2table(tb, Table1)
            End If

        Next


        '建立RecipeNo vs. Recipe Table 
        Try

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DefectDim_ByModel_new(ByVal Isdaily As Boolean, ByVal IsEtE As Boolean, ByVal S_Date As Date, ByVal E_Date As Date)

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

        Dim myrow As TableRow
        Dim mycell As TableCell
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

        If datepicker_E.Text = "" Or datepicker_S.Text = "" Then
            E_Date_String = Format(nn, "yyyyMMddHH00")
            S_Date_String = Format(DateAdd(DateInterval.Day, -1, nn), "yyyyMMddHH00")
        Else
            S_Date_String = Format(S_Date, "yyyyMMddHH00")
            E_Date_String = Format(E_Date, "yyyyMMddHH00")
        End If

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

            Dim Recipe_shift As Integer

            For Recipe_shift = 0 To Recipe_count - 1

                Dim RecipeNo_tmp As String = RecipeNo_Matrix(Recipe_shift)

                If Isdaily Then

                    mySql = ""
                    If IsEtE Then
                        mySql = mySql & " Select RecipeNo,D,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim30)/sum(Move)  As Dim_Ratio30, sum(Fail)/sum(Move)As Fail_Ratio,eqp_tool"
                    Else
                        'mySql = mySql & " Select RecipeNo,D,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                        mySql = mySql & " Select RecipeNo,D,sum(Move) As Move ,((sum(Defect) + sum(Fail))/sum(Move)) As Defect_N_Fail_Ratio,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                    End If
                    mySql = mySql & " From history "
                    mySql = mySql & " Where RecipeNo in ('" & RecipeNo_Matrix(Recipe_shift) & "')"
                    mySql = mySql & " And val(TimeA) >= " & S_Date_String & " "
                    mySql = mySql & " And val(TimeA) <= " & E_Date_String & " "
                    mySql = mySql & " Group by Eqp_tool,D,RecipeNo"
                    mySql = mySql & " Order by Eqp_tool,D"

                Else
                    mySql = ""

                    '---
                    If IsEtE Then
                        mySql = mySql & " Select * "
                    Else

                        mySql = mySql & " Select TimeA,RecipeNo,D, Move ,(Defect + Fail)/Move As Defect_N_Fail_Ratio,Defect/Move As Defect_Ratio, Dim/Move As Dim_Ratio, Fail/Move As Fail_Ratio,eqp_tool"
                    End If
                    '---
                    mySql = mySql & " From history "
                    mySql = mySql & " Where RecipeNo in ('" & RecipeNo_Matrix(Recipe_shift) & "')"
                    mySql = mySql & " And val(TimeA) >= " & S_Date_String & " "
                    mySql = mySql & " And val(TimeA) <= " & E_Date_String & " "
                    mySql = mySql & " Order by Eqp_tool,val(TimeA) "
                End If

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

                    End If

                    conn.Close()

                Next

                Dim dv As DataView
                tb = ds.Tables(0)

                dv = tb.DefaultView
                Dim tb_eqp As DataTable = dv.ToTable(True, "Eqp_tool")
                Dim eqp_count As Integer = tb_eqp.Rows.Count
                Dim Xlabel_count As Integer

                If Isdaily Then
                    Xlabel_count = DateDiff(DateInterval.Day, S_Date, E_Date) + 1
                Else
                    Xlabel_count = DateDiff(DateInterval.Hour, S_Date, E_Date) + 1
                End If

                ReDim datalabels(eqp_count * 2 - 1)
                ReDim Is_ratio(eqp_count * 2 - 1)
                ReDim BColor(eqp_count * 2 - 1)
                ReDim chartType(eqp_count * 2 - 1)
                ReDim yaxis(eqp_count * 2 - 1)
                ReDim data(eqp_count * 2 - 1)

                ReDim Xlabel(Xlabel_count - 1)

                For i = 0 To eqp_count - 1
                    datalabels(i) = tb_eqp.Rows(i).Item("Eqp_tool") & " Defect & Fail Ratio-"
                    datalabels(i + eqp_count) = tb_eqp.Rows(i).Item("Eqp_tool")

                    Is_ratio(i) = True
                    Is_ratio(i + eqp_count) = False 'tb_eqp.Rows(i).Item("Eqp_tool")

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

                    If Isdaily Then
                        Xlabel_tmpdate = DateAdd(DateInterval.Day, i, S_Date)
                    Else
                        Xlabel_tmpdate = DateAdd(DateInterval.Hour, i, S_Date)
                    End If

                    ReDim Xlabel(i)(1)
                    Xlabel(i)(0) = Format(Xlabel_tmpdate, "MM/dd") & " " & IIf(Isdaily, "", Format(Xlabel_tmpdate, "HH"))
                    Xlabel(i)(1) = IIf(Isdaily, "", Format(Xlabel_tmpdate, "HH"))
                Next

                For i = 0 To tb.Rows.Count - 1
                    Dim T As String ' = tb.Rows(i).Item("TimeA")
                    Dim TT As Date '= New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)

                    If Isdaily Then
                        T = tb.Rows(i).Item("D")
                        TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), 0, 0, 0)
                    Else
                        T = tb.Rows(i).Item("TimeA")
                        TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)
                    End If

                    Dim X0 As String = Format(TT, "MM/dd")
                    Dim X1 As String = Format(TT, "HH")

                    For j = 0 To eqp_count - 1

                        If tb.Rows(i).Item("eqp_tool") = tb_eqp.Rows(j).Item("eqp_tool") Then

                            k = IIf(Isdaily, DateDiff(DateInterval.Day, S_Date, TT), DateDiff(DateInterval.Hour, S_Date, TT))

                            If IsEtE Then
                                data(j)(k) = Convert.ToDouble(tb.Rows(i).Item("Dim_Ratio30"))
                            Else
                                data(j)(k) = Convert.ToDouble(tb.Rows(i).Item("Defect_N_Fail_Ratio"))
                            End If

                            data(j + eqp_count)(k) = IIf(Convert.ToDouble(tb.Rows(i).Item("Move")) = 0, Nothing, Convert.ToDouble(tb.Rows(i).Item("Move")))

                            j = eqp_count + 30

                        End If
                    Next
                Next

                Target = myCla.getTarget(RecipeNo_Matrix(Recipe_shift))
                'toJStext_highChart
                'cstext.Append(myCla.toJStext_highChart("canvasX" & RecipeNo_Matrix(Recipe_shift), RecipeNo_Matrix(Recipe_shift) & " - " & Recipe_matrix(Recipe_shift) & " Defect Ratio Chart", _
                cstext.Append(myCla.toJStext_new("canvasX" & RecipeNo_Matrix(Recipe_shift), RecipeNo_Matrix(Recipe_shift) & " - " & Recipe_matrix(Recipe_shift) & " Defect Ratio Chart", _
                                            Xlabel, _
                                           chartType, _
                                           datalabels, _
                                           data, _
                                           Is_ratio, _
                                           BColor, _
                                           yaxis, _
                                           True, IIf(IsEtE, 100, 35), Target, Target))

                Dim cell As New TableCell
                Dim li As New Literal

                li.Text = " <canvas id=" & Chr(34) & "canvasX" & RecipeNo_Matrix(Recipe_shift) & Chr(34) & "></canvas>"
                'li.Text = " <div id=" & Chr(34) & "canvasX" & RecipeNo_Matrix(Recipe_shift) & Chr(34) & "></div>"

                cell.Controls.Add(li)

                Dim row As New TableRow
                row.Cells.Add(cell)
                row.HorizontalAlign = HorizontalAlign.Center

                If eqp_count >= 6 Then
                    row.Height = 450
                Else
                    row.Height = 400
                End If
                Table2.Rows.Add(row)

                Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp" & RecipeNo_Matrix(Recipe_shift), cstext.ToString, False)

                If CheckBox1.Checked Then
                    myCla.ds2table(tb, Table1)
                End If

            Next


            '建立RecipeNo vs. Recipe Table 


        Catch ex As Exception

        End Try

    End Sub

    'DefectDim_ByModel_new_M24U2Loose
    Protected Sub DefectDim_ByModel_new_M24U2Loose(ByVal Isdaily As Boolean, ByVal IsEtE As Boolean, ByVal S_Date As Date, ByVal E_Date As Date)

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

        Dim myrow As TableRow
        Dim mycell As TableCell
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

        If datepicker_E.Text = "" Or datepicker_S.Text = "" Then
            E_Date_String = Format(nn, "yyyyMMddHH00")
            S_Date_String = Format(DateAdd(DateInterval.Day, -1, nn), "yyyyMMddHH00")
        Else
            S_Date_String = Format(S_Date, "yyyyMMddHH00")
            E_Date_String = Format(E_Date, "yyyyMMddHH00")
        End If

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

            Dim Recipe_shift As Integer

            For Recipe_shift = 0 To Recipe_count - 1

                Dim RecipeNo_tmp As String = RecipeNo_Matrix(Recipe_shift)
                If RecipeNo_tmp = "02" Then


                    If Isdaily Then

                        mySql = ""
                        If IsEtE Then
                            mySql = mySql & " Select RecipeNo,D,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim30)/sum(Move)  As Dim_Ratio30, sum(Fail)/sum(Move)As Fail_Ratio,eqp_tool"
                        Else
                            'mySql = mySql & " Select RecipeNo,D,sum(Move) As Move ,sum(Defect)/sum(Move) As Defect_Ratio, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                            mySql = mySql & " Select RecipeNo,D,sum(Move) As Move ,((sum(Defect) + sum(Fail))/sum(Move)) As Defect_N_Fail_Ratio,sum(Defect)/sum(Move) As Defect_Ratio,sum(Defect) As Defect, sum(Dim)/sum(Move) As Dim_Ratio, sum(Fail)/sum(Move) As Fail_Ratio,eqp_tool"
                        End If
                        mySql = mySql & " From history "
                        mySql = mySql & " Where RecipeNo in ('" & RecipeNo_Matrix(Recipe_shift) & "')"
                        mySql = mySql & " And val(TimeA) >= " & S_Date_String & " "
                        mySql = mySql & " And val(TimeA) <= " & E_Date_String & " "
                        mySql = mySql & " Group by Eqp_tool,D,RecipeNo"
                        mySql = mySql & " Order by Eqp_tool,D"

                    Else
                        mySql = ""

                        '---
                        mySql = mySql & " Select TimeA,RecipeNo,D, Move ,(Defect + Fail)/Move As Defect_N_Fail_Ratio,(Defect + Fail) As Defect_N_Fail,Defect/Move As Defect_Ratio,Defect , Dim/Move As Dim_Ratio, Fail/Move As Fail_Ratio,eqp_tool"
                        '---
                        mySql = mySql & " From history "
                        mySql = mySql & " Where RecipeNo in ('" & RecipeNo_Matrix(Recipe_shift) & "')"
                        mySql = mySql & " And val(TimeA) >= " & S_Date_String & " "
                        mySql = mySql & " And val(TimeA) <= " & E_Date_String & " "
                        mySql = mySql & " Order by Eqp_tool,val(TimeA) "
                    End If

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

                        End If

                        conn.Close()

                    Next

                    Dim dv As DataView
                    tb = ds.Tables(0)

                    dv = tb.DefaultView
                    Dim tb_eqp As DataTable = dv.ToTable(True, "Eqp_tool")
                    Dim eqp_count As Integer = tb_eqp.Rows.Count
                    Dim Xlabel_count As Integer

                    If Isdaily Then
                        Xlabel_count = DateDiff(DateInterval.Day, S_Date, E_Date) + 1
                    Else
                        Xlabel_count = DateDiff(DateInterval.Hour, S_Date, E_Date) + 1
                    End If

                    ReDim datalabels(eqp_count * 2 - 1)
                    ReDim Is_ratio(eqp_count * 2 - 1)
                    ReDim BColor(eqp_count * 2 - 1)
                    ReDim chartType(eqp_count * 2 - 1)
                    ReDim yaxis(eqp_count * 2 - 1)
                    ReDim data(eqp_count * 2 - 1)

                    ReDim Xlabel(Xlabel_count - 1)

                    For i = 0 To eqp_count - 1
                        datalabels(i) = tb_eqp.Rows(i).Item("Eqp_tool") & " Defect & Fail Ratio-"
                        datalabels(i + eqp_count) = tb_eqp.Rows(i).Item("Eqp_tool")

                        Is_ratio(i) = True
                        Is_ratio(i + eqp_count) = False 'tb_eqp.Rows(i).Item("Eqp_tool")

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

                        If Isdaily Then
                            Xlabel_tmpdate = DateAdd(DateInterval.Day, i, S_Date)
                        Else
                            Xlabel_tmpdate = DateAdd(DateInterval.Hour, i, S_Date)
                        End If

                        ReDim Xlabel(i)(1)
                        Xlabel(i)(0) = Format(Xlabel_tmpdate, "MM/dd") & " " & IIf(Isdaily, "", Format(Xlabel_tmpdate, "HH"))
                        Xlabel(i)(1) = IIf(Isdaily, "", Format(Xlabel_tmpdate, "HH"))
                    Next

                    For i = 0 To tb.Rows.Count - 1
                        Dim T As String ' = tb.Rows(i).Item("TimeA")
                        Dim TT As Date '= New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)

                        If Isdaily Then
                            T = tb.Rows(i).Item("D")
                            TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), 0, 0, 0)
                        Else
                            T = tb.Rows(i).Item("TimeA")
                            TT = New Date(Mid(T, 1, 4), Mid(T, 5, 2), Mid(T, 7, 2), Mid(T, 9, 2), Mid(T, 11, 2), 0)
                        End If

                        Dim X0 As String = Format(TT, "MM/dd")
                        Dim X1 As String = Format(TT, "HH")

                        For j = 0 To eqp_count - 1

                            If tb.Rows(i).Item("eqp_tool") = tb_eqp.Rows(j).Item("eqp_tool") Then
                                Dim tmpeqp As String = Mid(tb.Rows(i).Item("eqp_tool"), 6, 1)
                                Dim looseCnt As Integer
                                Dim tmppath1 As String = Path_ServerData & "CCT" & tmpeqp & "00\" & Format(TT, "yyyyMM") & "\" & Format(TT, "dd") & "\" & Format(TT, "yyyyMMdd") & ".mdb"
                                Dim tmp_path As String() = New String() {tmppath1}
                                Dim Defect_N_Fail_Ratio As Double


                                looseCnt = myCla.ChippingLoose2tb(1, tmp_path).Compute("Count(PanelID)", "TT = '" & Mid(T, 9, 2) & "'")



                                k = IIf(Isdaily, DateDiff(DateInterval.Day, S_Date, TT), DateDiff(DateInterval.Hour, S_Date, TT))

                                If Convert.ToDouble(tb.Rows(i).Item("Move")) = 0 Then
                                    data(j + eqp_count)(k) = Nothing 'IIf(Convert.ToDouble(tb.Rows(i).Item("Move")) = 0, Nothing, Convert.ToDouble(tb.Rows(i).Item("Move")))
                                    Defect_N_Fail_Ratio = 0 '(tb.Rows(i).Item("Defect_N_Fail") - looseCnt) / data(j + eqp_count)(k)
                                Else
                                    data(j + eqp_count)(k) = Convert.ToDouble(tb.Rows(i).Item("Move"))
                                    'Defect_N_Fail_Ratio = (tb.Rows(i).Item("Defect_N_Fail") - looseCnt) / data(j + eqp_count)(k)
                                    Defect_N_Fail_Ratio = (tb.Rows(i).Item("Defect") - looseCnt) / data(j + eqp_count)(k)
                                End If
                                data(j)(k) = Convert.ToDouble(Defect_N_Fail_Ratio)


                                j = eqp_count + 30

                            End If
                        Next
                    Next

                    Target = myCla.getTarget(RecipeNo_Matrix(Recipe_shift))
                    'toJStext_highChart
                    cstext.Append(myCla.toJStext_highChart("canvasX" & RecipeNo_Matrix(Recipe_shift), _
                                               RecipeNo_Matrix(Recipe_shift) & " - " & Recipe_matrix(Recipe_shift) & " Defect Ratio Chart (Loose 300~800/edge)", _
                                               Xlabel, _
                                               chartType, _
                                               datalabels, _
                                               data, _
                                               Is_ratio, _
                                               BColor, _
                                               yaxis, _
                                               True, IIf(IsEtE, 100, 25), Target, Target))

                    Dim cell As New TableCell
                    Dim li As New Literal

                    'li.Text = " <canvas id=" & Chr(34) & "canvasX" & RecipeNo_Matrix(Recipe_shift) & Chr(34) & "></canvas>"
                    li.Text = " <div id=" & Chr(34) & "canvasX" & RecipeNo_Matrix(Recipe_shift) & Chr(34) & "></div>"

                    cell.Controls.Add(li)

                    Dim row As New TableRow
                    row.Cells.Add(cell)
                    row.HorizontalAlign = HorizontalAlign.Center

                    If eqp_count >= 6 Then
                        row.Height = 450
                    Else
                        row.Height = 400
                    End If
                    Table2.Rows.Add(row)

                    Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp" & RecipeNo_Matrix(Recipe_shift), cstext.ToString, False)

                    If CheckBox1.Checked Then
                        myCla.ds2table(tb, Table1)
                    End If
                End If
            Next


            '建立RecipeNo vs. Recipe Table 


        Catch ex As Exception

        End Try

    End Sub
End Class


