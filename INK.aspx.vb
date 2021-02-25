
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Partial Class INK
    Inherits System.Web.UI.Page

    Public myCla As New Class1
    ' Public Path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData\"
    Public Path_Access_INK As String = "\\101.101.101.203\cct000\update\Tmp\INK.mdb"
    Dim byDuration() As String = New String() {"Hourly", "Daily", "Monthly"}

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        datepicker_S.Text = IIf(datepicker_S.Text = "", Format(DateAdd(DateInterval.Day, -1, Now()), "MM/dd/yyyy"), datepicker_S.Text)
        datepicker_E.Text = IIf(datepicker_E.Text = "", Format(Now(), "MM/dd/yyyy"), datepicker_E.Text)

    End Sub
    Private Sub defect_count(ByVal ByDur As String, ByVal S_Date As Date, ByVal E_date As Date)

        Dim i, j, k As Integer
        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim path As String
        Dim mySql As String
        Dim conn_string As String

        Dim myCla As New Class1

        Dim Datalabels() As String = New String() {"r_pinhole", "r_leaklight", "r_break", "r_particle", "Move"}
        Dim Is_ratio() As Boolean = New Boolean() {True, True, True, True, False}
        Dim BColor() As String = New String() {"window.chartColors.red", _
                                                "window.chartColors.yellowA", _
                                                "window.chartColors.blue", _
                                                "window.chartColors.orange", _
                                                "window.chartColors.green"}
        Dim chartType() As String = New String() {"line", "line", "line", "line", "bar"}
        Dim yaxis() As String = New String() {"y1", "y1", "y1", "y1", "y2"}

        Dim Xlabel()() As String
        Dim data()() As Nullable(Of Double)
        Dim Xlabel_count As Integer
        Dim cstext As New StringBuilder()
        Dim Xlabel_tmpdate As Date

        '"Hourly", "Daily", "Monthly"
        Select Case ByDur
            Case "Hourly"
                Xlabel_count = DateDiff(DateInterval.Hour, S_Date, E_date) + 1
            Case "Daily"
                Xlabel_count = DateDiff(DateInterval.Day, S_Date, E_date) + 1
            Case "Monthly"
                Xlabel_count = DateDiff(DateInterval.Month, S_Date, E_date) + 1
        End Select

        ReDim Xlabel(Xlabel_count - 1)
        ReDim data(Datalabels.Length - 1)

        For i = 0 To Datalabels.Length - 1
            ReDim data(i)(Xlabel_count - 1)
        Next

        For i = 0 To Xlabel.Length - 1

            ReDim Xlabel(i)(1)

            '"Hourly", "Daily", "Monthly"
            Select Case ByDur
                Case "Hourly"
                    Xlabel_tmpdate = DateAdd(DateInterval.Hour, i, S_Date)
                    Xlabel(i)(0) = Format(Xlabel_tmpdate, "yy/MM/dd-HH")
                Case "Daily"
                    Xlabel_tmpdate = DateAdd(DateInterval.Day, i, S_Date)
                    Xlabel(i)(0) = Format(Xlabel_tmpdate, "yy/MM/dd")
                Case "Monthly"
                    Xlabel_tmpdate = DateAdd(DateInterval.Month, i, S_Date)
                    Xlabel(i)(0) = Format(Xlabel_tmpdate, "yy/MM")
            End Select

            Xlabel(i)(1) = ""
        Next

        'path = Path_ServerData & "CCT" & num2eqp(i + 1) & "00\" & Format(Now, "yyyyMM") & "\" & Format(Now, "yyyyMM") & "PresureMileageInfo.mdb"
        path = Path_Access_INK
        If File.Exists(path) Then
            Dim datestyle As String
            '"Hourly", "Daily", "Monthly"
            Select Case ByDur
                Case "Hourly"
                    datestyle = "yy/MM/dd-HH"
                Case "Daily"
                    datestyle = "yy/MM/dd"
                Case "Monthly"
                    datestyle = "yy/MM"
            End Select

            Dim mySql_totalmove, mySql_p_ng, mySql_leaklight_ng, mySql_pinhole_ng, mySql_break_ng As String
            mySql_totalmove = ""
            mySql_totalmove = mySql_totalmove & " SELECT     Count(cglassid)                     AS move,  "
            mySql_totalmove = mySql_totalmove & "            Format(dstarttime,""" & datestyle & """) AS thehour  "
            mySql_totalmove = mySql_totalmove & " FROM       dbo_hsglass  "
            mySql_totalmove = mySql_totalmove & " GROUP BY   Format(dstarttime,""" & datestyle & """)  "

            mySql_p_ng = ""
            mySql_p_ng = mySql_p_ng & "    SELECT   Count(cglassid)                     AS gcnt,  "
            mySql_p_ng = mySql_p_ng & "             Format(dstarttime,""" & datestyle & """) AS thehour  "
            mySql_p_ng = mySql_p_ng & "    FROM     dbo_hsglass  "
            mySql_p_ng = mySql_p_ng & "    WHERE    idspipo <> 0  "
            mySql_p_ng = mySql_p_ng & "    OR       idspipl <> 0  "
            mySql_p_ng = mySql_p_ng & "    OR       idspipm <> 0  "
            mySql_p_ng = mySql_p_ng & "    OR       idspips <> 0  "
            mySql_p_ng = mySql_p_ng & "    OR       idspino <> 0  "
            mySql_p_ng = mySql_p_ng & "    OR       idspinl <> 0  "
            mySql_p_ng = mySql_p_ng & "    OR       idspinm <> 0  "
            mySql_p_ng = mySql_p_ng & "    OR       idspins <> 0  "
            mySql_p_ng = mySql_p_ng & "    GROUP BY Format(dstarttime,""" & datestyle & """)  "
            mySql_pinhole_ng = ""
            mySql_pinhole_ng = mySql_pinhole_ng & "   SELECT   Count(cglassid)    AS gcnt,  "
            mySql_pinhole_ng = mySql_pinhole_ng & "            Format(dstarttime,""" & datestyle & """) AS thehour  "
            mySql_pinhole_ng = mySql_pinhole_ng & "   FROM     dbo_hsglass  "
            mySql_pinhole_ng = mySql_pinhole_ng & "   WHERE    idskip <> 0  "
            mySql_pinhole_ng = mySql_pinhole_ng & "   OR       idskip_n <> 0  "
            mySql_pinhole_ng = mySql_pinhole_ng & "   GROUP BY Format(dstarttime,""" & datestyle & """)    "
            mySql_break_ng = ""
            mySql_break_ng = mySql_break_ng & "   SELECT   count(cglassid)    AS gcnt,  "
            mySql_break_ng = mySql_break_ng & "            Format(dstarttime,""" & datestyle & """) AS thehour  "
            mySql_break_ng = mySql_break_ng & "   FROM     dbo_hsglass  "
            mySql_break_ng = mySql_break_ng & "   WHERE    idskib <> 0  "
            mySql_break_ng = mySql_break_ng & "   OR       idskib_n <> 0  "
            mySql_break_ng = mySql_break_ng & "   GROUP BY Format(dstarttime,""" & datestyle & """)   "
            mySql_leaklight_ng = ""
            mySql_leaklight_ng = mySql_leaklight_ng & "   SELECT   count(cglassid)                     AS gcnt,  "
            mySql_leaklight_ng = mySql_leaklight_ng & "            Format(dstarttime,""" & datestyle & """) AS thehour  "
            mySql_leaklight_ng = mySql_leaklight_ng & "   FROM     dbo_hsglass  "
            mySql_leaklight_ng = mySql_leaklight_ng & "   WHERE    idskil <> 0  "
            mySql_leaklight_ng = mySql_leaklight_ng & "   OR       idskil_n <> 0  "
            mySql_leaklight_ng = mySql_leaklight_ng & "   GROUP BY Format(dstarttime,""" & datestyle & """)  "

            mySql = ""
            mySql = mySql & " SELECT    Format(totalmove.thehour,""" & datestyle & """) as theHour ,  "
            mySql = mySql & "           totalmove.move    as Move,  "
            mySql = mySql & "           p_ng.gcnt         as particle,  "
            mySql = mySql & "           pinhole_ng.gcnt   as pinhole,  "
            mySql = mySql & "           break_ng.gcnt     as break,  "
            mySql = mySql & "           leaklight_ng.gcnt as leaklight,  "
            mySql = mySql & "           round( ( iif(isnull(p_ng.gcnt),0,p_ng.gcnt)                 /totalmove.move ) ,4) AS r_particle,  "
            mySql = mySql & "           round( ( iif(isnull(pinhole_ng.gcnt),0,pinhole_ng.gcnt)     /totalmove.move ) ,4) AS r_pinhole,  "
            mySql = mySql & "           round( ( iif(isnull(break_ng.gcnt),0,break_ng.gcnt)         /totalmove.move ) ,4) AS r_break,  "
            mySql = mySql & "           round( ( iif(isnull(leaklight_ng.gcnt),0,leaklight_ng.gcnt) /totalmove.move ) ,4) AS r_leaklight  "
            mySql = mySql & " FROM      ((((  "
            mySql = mySql & "           ( " & mySql_totalmove & "    ) AS totalmove  "
            mySql = mySql & " LEFT JOIN ( " & mySql_p_ng & "         ) AS p_ng         ON  totalmove.thehour = p_ng.thehour         )"
            mySql = mySql & " LEFT JOIN ( " & mySql_pinhole_ng & "   ) AS pinhole_ng   ON  totalmove.thehour = pinhole_ng.thehour   )"
            mySql = mySql & " LEFT JOIN ( " & mySql_break_ng & "     ) AS break_ng     ON  totalmove.thehour = break_ng.thehour     )"
            mySql = mySql & " LEFT JOIN ( " & mySql_leaklight_ng & " ) AS leaklight_ng ON  totalmove.thehour = leaklight_ng.thehour )"

            mySql = mySql & " WHERE     totalmove.thehour >= '" & Format(S_Date, "" & datestyle.Replace("HH", "00") & "") & " '"
            mySql = mySql & " AND       totalmove.thehour <= '" & Format(E_date, "" & datestyle & "") & " '"
            mySql = mySql & " ORDER BY  totalmove.thehour "

            conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
            conn = New OleDbConnection(conn_string)
            conn.Open()

            ds.Reset()
            adapter = New OleDbDataAdapter(mySql, conn)
            adapter.Fill(ds)
            adapter.Dispose()
            conn.Close()
            tb = ds.Tables(0)

            For i = 0 To tb.Rows.Count - 1
                For j = 0 To Xlabel.Length - 1
                    If Xlabel(j)(0) = tb.Rows(i).Item(0) Then
                        For k = 0 To Datalabels.Length - 1
                            data(k)(j) = Convert.ToDouble(IIf(IsDBNull(tb.Rows(i).Item(Datalabels(k))), 0, tb.Rows(i).Item(Datalabels(k))))
                        Next
                        Exit For
                    End If
                Next

            Next
            myCla.ds2table(tb, Table2)
            conn.Close()
        End If

        cstext = New StringBuilder
        cstext.Append(myCla.toJStext_new("canvasX", "Ink Defect Ratio", Xlabel, chartType, Datalabels, data, Is_ratio, BColor, yaxis, False, 100))
        myCla.cstext2Canvas(Page, cstext, "canvasX", Table1)

    End Sub

    Private Sub defect_GoYield(ByVal ByDur As String, ByVal S_Date As Date, ByVal E_date As Date)

        Dim i, j, k As Integer
        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim path As String
        Dim mySql As String
        Dim conn_string As String

        Dim myCla As New Class1

        Dim Datalabels() As String = New String() {"Yield", "Move"}
        Dim Is_ratio() As Boolean = New Boolean() {True, True, True, True, False}
        Dim BColor() As String = New String() {"window.chartColors.blue", _
                                                "window.chartColors.green"}
        Dim chartType() As String = New String() {"line", "bar"}
        Dim yaxis() As String = New String() {"y1", "y2"}

        Dim Xlabel()() As String
        Dim data()() As Nullable(Of Double)
        Dim Xlabel_count As Integer
        Dim cstext As New StringBuilder()
        Dim Xlabel_tmpdate As Date

        '"Hourly", "Daily", "Monthly"
        Select Case ByDur
            Case "Hourly"
                Xlabel_count = DateDiff(DateInterval.Hour, S_Date, E_date) + 1
            Case "Daily"
                Xlabel_count = DateDiff(DateInterval.Day, S_Date, E_date) + 1
            Case "Monthly"
                Xlabel_count = DateDiff(DateInterval.Month, S_Date, E_date) + 1
        End Select

        ReDim Xlabel(Xlabel_count - 1)
        ReDim data(Datalabels.Length - 1)

        For i = 0 To Datalabels.Length - 1
            ReDim data(i)(Xlabel_count - 1)
        Next

        For i = 0 To Xlabel.Length - 1

            ReDim Xlabel(i)(1)

            '"Hourly", "Daily", "Monthly"
            Select Case ByDur
                Case "Hourly"
                    Xlabel_tmpdate = DateAdd(DateInterval.Hour, i, S_Date)
                    Xlabel(i)(0) = Format(Xlabel_tmpdate, "yy/MM/dd-HH")
                Case "Daily"
                    Xlabel_tmpdate = DateAdd(DateInterval.Day, i, S_Date)
                    Xlabel(i)(0) = Format(Xlabel_tmpdate, "yy/MM/dd")
                Case "Monthly"
                    Xlabel_tmpdate = DateAdd(DateInterval.Month, i, S_Date)
                    Xlabel(i)(0) = Format(Xlabel_tmpdate, "yy/MM")
            End Select

            Xlabel(i)(1) = ""
        Next

        path = Path_Access_INK
        If File.Exists(path) Then
            Dim datestyle As String
            '"Hourly", "Daily", "Monthly"
            Select Case ByDur
                Case "Hourly"
                    datestyle = "yy/MM/dd-HH"
                Case "Daily"
                    datestyle = "yy/MM/dd"
                Case "Monthly"
                    datestyle = "yy/MM"
            End Select

            Dim mySql_totalmove, mySql_defect As String
            mySql_totalmove = ""
            mySql_totalmove = mySql_totalmove & " SELECT     Count(cglassid)                     AS move,  "
            mySql_totalmove = mySql_totalmove & "            Format(dstarttime,""" & datestyle & """) AS thehour  "
            mySql_totalmove = mySql_totalmove & " FROM       dbo_hsglass  "
            mySql_totalmove = mySql_totalmove & " GROUP BY   Format(dstarttime,""" & datestyle & """)  "

            mySql_defect = ""
            mySql_defect = mySql_defect & "    SELECT   Count(cglassid)                     AS DefcDistinctCnt,  "
            mySql_defect = mySql_defect & "             Format(dstarttime,""" & datestyle & """) AS thehour  "
            mySql_defect = mySql_defect & "    FROM     dbo_hsglass  "
            mySql_defect = mySql_defect & "    WHERE    idspipo    <> 0  "
            mySql_defect = mySql_defect & "    OR       idspipl    <> 0  "
            mySql_defect = mySql_defect & "    OR       idspipm    <> 0  "
            mySql_defect = mySql_defect & "    OR       idspips    <> 0  "
            mySql_defect = mySql_defect & "    OR       idspino    <> 0  "
            mySql_defect = mySql_defect & "    OR       idspinl    <> 0  "
            mySql_defect = mySql_defect & "    OR       idspinm    <> 0  "
            mySql_defect = mySql_defect & "    OR       idspins    <> 0  "
            mySql_defect = mySql_defect & "    OR       idskip     <> 0  "
            mySql_defect = mySql_defect & "    OR       idskip_n   <> 0  "
            mySql_defect = mySql_defect & "    OR       idskib     <> 0  "
            mySql_defect = mySql_defect & "    OR       idskib_n   <> 0  "
            mySql_defect = mySql_defect & "    OR       idskil     <> 0  "
            mySql_defect = mySql_defect & "    OR       idskil_n   <> 0  "
            mySql_defect = mySql_defect & "    GROUP BY Format(dstarttime,""" & datestyle & """)  "

            mySql = ""
            mySql = mySql & " SELECT    Format(totalmove.thehour,""" & datestyle & """) as theHour ,  "
            mySql = mySql & "           totalmove.move                                  as Move,  "
            mySql = mySql & "           def.DefcDistinctCnt                             as defect,  "
            mySql = mySql & "              round( ( ( iif(isnull(def.DefcDistinctCnt),0,def.DefcDistinctCnt)) /totalmove.move ) ,4) as r_defect , "
            mySql = mySql & "           1 -round( ( ( iif(isnull(def.DefcDistinctCnt),0,def.DefcDistinctCnt)) /totalmove.move ) ,4) as Yield  "
            mySql = mySql & " FROM      (  "
            mySql = mySql & "           ( " & mySql_totalmove & "    ) AS totalmove  "
            mySql = mySql & " LEFT JOIN ( " & mySql_defect & "       ) AS def       ON  totalmove.thehour = def.thehour         )"

            mySql = mySql & " WHERE     totalmove.thehour >= '" & Format(S_Date, "" & datestyle.Replace("HH", "00") & "") & " '"
            mySql = mySql & " AND       totalmove.thehour <= '" & Format(E_date, "" & datestyle & "") & " '"
            mySql = mySql & " ORDER BY  totalmove.thehour "

            conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
            conn = New OleDbConnection(conn_string)
            conn.Open()

            ds.Reset()
            adapter = New OleDbDataAdapter(mySql, conn)
            adapter.Fill(ds)
            adapter.Dispose()
            conn.Close()
            tb = ds.Tables(0)

            For i = 0 To tb.Rows.Count - 1
                For j = 0 To Xlabel.Length - 1
                    If Xlabel(j)(0) = tb.Rows(i).Item(0) Then
                        For k = 0 To Datalabels.Length - 1
                            data(k)(j) = Convert.ToDouble(IIf(IsDBNull(tb.Rows(i).Item(Datalabels(k))), 0, tb.Rows(i).Item(Datalabels(k))))
                        Next
                        Exit For
                    End If
                Next

            Next
            myCla.ds2table(tb, Table2)
            conn.Close()
        End If

        cstext = New StringBuilder
        cstext.Append(myCla.toJStext_new("canvasXY", "Ink Yield", Xlabel, chartType, Datalabels, data, Is_ratio, BColor, yaxis, False, 100))

        myCla.cstext2Canvas(Page, cstext, "canvasXY", Table1)

    End Sub
    Private Sub defect_looose_Ratio(ByVal S_Date As Date, ByVal E_date As Date)

        Dim i, j, k As Integer
        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim path As String
        Dim mySql As String
        Dim conn_string As String

        Dim myCla As New Class1

        Dim Datalabels() As String = New String() {"Ratio1", "Ratio2", "Yield", "Move"}
        Dim Is_ratio() As Boolean = New Boolean() {True, True, True, False}
        Dim BColor() As String = New String() {"window.chartColors.red", _
                                                "window.chartColors.yellowA", _
                                                "window.chartColors.blue", _
                                                "window.chartColors.green"}
        Dim chartType() As String = New String() {"line", "line", "line", "bar"}
        Dim yaxis() As String = New String() {"y1", "y1", "y1", "y2"}

        Dim Xlabel()() As String
        Dim data()() As Nullable(Of Double)
        Dim Xlabel_count As Integer
        Dim cstext As New StringBuilder()
        Dim Xlabel_tmpdate As Date


        Xlabel_count = DateDiff(DateInterval.Hour, S_Date, E_date) + 1
        ReDim Xlabel(Xlabel_count - 1)
        ReDim data(Datalabels.Length - 1)

        For i = 0 To Datalabels.Length - 1
            ReDim data(i)(Xlabel_count - 1)
        Next

        For i = 0 To Xlabel.Length - 1
            Xlabel_tmpdate = DateAdd(DateInterval.Hour, i, S_Date)
            ReDim Xlabel(i)(1)
            Xlabel(i)(0) = Format(Xlabel_tmpdate, "dd-HH")
            Xlabel(i)(1) = ""
        Next

        'path = Path_ServerData & "CCT" & num2eqp(i + 1) & "00\" & Format(Now, "yyyyMM") & "\" & Format(Now, "yyyyMM") & "PresureMileageInfo.mdb"
        path = Path_Access_INK
        If File.Exists(path) Then

            Dim mySql_totalmove, mySql_loosedefect1, mySql_loosedefect2 As String
            mySql_totalmove = ""
            mySql_totalmove = mySql_totalmove & "  SELECT   Count(iglassid)   AS move,  "
            mySql_totalmove = mySql_totalmove & "           Format(dstarttime,""yyyy/MM/dd-HH"") AS thehour  "
            mySql_totalmove = mySql_totalmove & "  FROM     LooseDefect  "
            mySql_totalmove = mySql_totalmove & "  WHERE    Format(dstarttime,""yyyy/MM/dd-HH"") > '" & Format(S_Date, "yyyy/MM/dd-00") & " '"
            mySql_totalmove = mySql_totalmove & "  AND      Format(dstarttime,""yyyy/MM/dd-HH"") <= '" & Format(E_date, "yyyy/MM/dd-HH") & " '"
            mySql_totalmove = mySql_totalmove & "  GROUP BY Format(dstarttime,""yyyy/MM/dd-HH"")  "
            mySql_loosedefect1 = ""
            mySql_loosedefect1 = mySql_loosedefect1 & "      Select   Count(iglassid)                      AS cnt,  "
            mySql_loosedefect1 = mySql_loosedefect1 & "               Format(dstarttime,""yyyy/MM/dd-HH"") AS thehour  "
            mySql_loosedefect1 = mySql_loosedefect1 & "      From     LooseDefect"
            mySql_loosedefect1 = mySql_loosedefect1 & "      WHERE  ( cnt200 + cnt300    ) >  10 "
            mySql_loosedefect1 = mySql_loosedefect1 & "      AND    ( cnt400 + cnt500plus) <= 0 "
            mySql_loosedefect1 = mySql_loosedefect1 & "      AND      Format(dstarttime,""yyyy/MM/dd-HH"") > '" & Format(S_Date, "yyyy/MM/dd-00") & " '"
            mySql_loosedefect1 = mySql_loosedefect1 & "      AND      Format(dstarttime,""yyyy/MM/dd-HH"") <= '" & Format(E_date, "yyyy/MM/dd-HH") & " '"
            mySql_loosedefect1 = mySql_loosedefect1 & "      GROUP BY Format(dstarttime,""yyyy/MM/dd-HH"") "
            mySql_loosedefect2 = ""
            mySql_loosedefect2 = mySql_loosedefect2 & "      Select   Count(iglassid)                      AS cnt,  "
            mySql_loosedefect2 = mySql_loosedefect2 & "               Format(dstarttime,""yyyy/MM/dd-HH"") AS thehour  "
            mySql_loosedefect2 = mySql_loosedefect2 & "      From     LooseDefect"
            mySql_loosedefect2 = mySql_loosedefect2 & "      WHERE    (cnt400 + cnt500plus) >  0 "
            mySql_loosedefect2 = mySql_loosedefect2 & "      AND      Format(dstarttime,""yyyy/MM/dd-HH"") > '" & Format(S_Date, "yyyy/MM/dd-00") & " '"
            mySql_loosedefect2 = mySql_loosedefect2 & "      AND      Format(dstarttime,""yyyy/MM/dd-HH"") <= '" & Format(E_date, "yyyy/MM/dd-HH") & " '"
            mySql_loosedefect2 = mySql_loosedefect2 & "      GROUP BY Format(dstarttime,""yyyy/MM/dd-HH"") "

            mySql = ""
            mySql = mySql & " SELECT    Format(totalmove.thehour,""dd-HH"") as theHour    , "
            mySql = mySql & "           totalmove.move                      as move       , "
            mySql = mySql & "           1-round( iif(isnull(def1.cnt),0,def1.cnt)/totalmove.move ,3)- round( iif(isnull(def2.cnt),0,def2.cnt)/totalmove.move ,3) as Yield       , "
            mySql = mySql & "           iif(isnull(def1.cnt),0,def1.cnt)                            as defCnt1    , "
            mySql = mySql & "           round( iif(isnull(def1.cnt),0,def1.cnt)/totalmove.move ,3)  as Ratio1  , "
            mySql = mySql & "           iif(isnull(def2.cnt),0,def2.cnt)                            as defCnt2    , "
            mySql = mySql & "           round( iif(isnull(def2.cnt),0,def2.cnt)/totalmove.move ,3)  as Ratio2   "
            mySql = mySql & " FROM      ((  "
            mySql = mySql & "           ( " & mySql_totalmove & "      ) AS totalmove  "
            mySql = mySql & " LEFT JOIN ( " & mySql_loosedefect1 & "   ) AS def1      ON  totalmove.thehour = def1.thehour   )"
            mySql = mySql & " LEFT JOIN ( " & mySql_loosedefect2 & "   ) AS def2      ON  totalmove.thehour = def2.thehour   )"

            conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
            conn = New OleDbConnection(conn_string)
            conn.Open()
            Try
                ds.Reset()
                adapter = New OleDbDataAdapter(mySql, conn)
                adapter.Fill(ds)
                adapter.Dispose()
            Catch ex As Exception
            Finally
                conn.Close()
            End Try

            conn.Close()
            tb = ds.Tables(0)
            myCla.ds2table(tb, Table2)
            conn.Close()

            For i = 0 To tb.Rows.Count - 1
                For j = 0 To Xlabel.Length - 1
                    If Xlabel(j)(0) = tb.Rows(i).Item(0).ToString.Split("/")(2) Then
                        For k = 0 To Datalabels.Length - 1
                            data(k)(j) = Convert.ToDouble(IIf(IsDBNull(tb.Rows(i).Item(Datalabels(k))), 0, tb.Rows(i).Item(Datalabels(k))))
                        Next
                        Exit For
                    End If
                Next

            Next
            myCla.ds2table(tb, Table2)
            conn.Close()
        End If




        cstext = New StringBuilder
        cstext.Append(myCla.toJStext_new("canvasX", "Ink Defect Ratio", Xlabel, chartType, Datalabels, data, Is_ratio, BColor, yaxis, False, 100))
        myCla.cstext2Canvas(Page, cstext, "canvasX", Table1)

    End Sub

    Private Sub defect_hour_Vs_cntsize(ByVal S_Date As Date, ByVal E_date As Date)

        Dim i, j, k As Integer
        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim path As String
        Dim mySql As String
        Dim conn_string As String

        Dim myCla As New Class1

        Dim Datalabels() As String = New String() {"Ratio1", "Ratio2", "Yield", "Move"}
        Dim Is_ratio() As Boolean = New Boolean() {True, True, True, False}
        Dim BColor() As String = New String() {"window.chartColors.red", _
                                                "window.chartColors.yellowA", _
                                                "window.chartColors.blue", _
                                                "window.chartColors.green"}
        Dim chartType() As String = New String() {"line", "line", "line", "bar"}
        Dim yaxis() As String = New String() {"y1", "y1", "y1", "y2"}

        Dim Xlabel()() As String
        Dim data()() As Nullable(Of Double)
        Dim Xlabel_count As Integer
        Dim cstext As New StringBuilder()
        Dim Xlabel_tmpdate As Date


        Xlabel_count = DateDiff(DateInterval.Hour, S_Date, E_date) + 1
        ReDim Xlabel(Xlabel_count - 1)
        ReDim data(Datalabels.Length - 1)

        For i = 0 To Datalabels.Length - 1
            ReDim data(i)(Xlabel_count - 1)
        Next

        For i = 0 To Xlabel.Length - 1
            Xlabel_tmpdate = DateAdd(DateInterval.Hour, i, S_Date)
            ReDim Xlabel(i)(1)
            Xlabel(i)(0) = Format(Xlabel_tmpdate, "dd-HH")
            Xlabel(i)(1) = ""
        Next

        'path = Path_ServerData & "CCT" & num2eqp(i + 1) & "00\" & Format(Now, "yyyyMM") & "\" & Format(Now, "yyyyMM") & "PresureMileageInfo.mdb"
        path = Path_Access_INK
        If File.Exists(path) Then


            mySql = ""
            mySql = mySql & " select    Format(dstarttime,""yyyy/MM/dd-HH"") as theHour , sum(cnt200)as c200 , sum(cnt300) as c300 , sum(cnt400) as c400 , sum(cnt500plus) as c500plus "
            mySql = mySql & " from      LooseDefect "
            mySql = mySql & " WHERE     Format(dstarttime,""yyyy/MM/dd-HH"") > '" & Format(S_Date, "yyyy/MM/dd-00") & " '"
            mySql = mySql & " AND       Format(dstarttime,""yyyy/MM/dd-HH"") <= '" & Format(E_date, "yyyy/MM/dd-HH") & " '"
            mySql = mySql & " GROUP by  Format(dstarttime,""yyyy/MM/dd-HH"") "
            mySql = mySql & " ORDER BY  Format(dstarttime,""yyyy/MM/dd-HH"") "

            conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
            conn = New OleDbConnection(conn_string)
            conn.Open()
            Try
                ds.Reset()
                adapter = New OleDbDataAdapter(mySql, conn)
                adapter.Fill(ds)
                adapter.Dispose()
            Catch ex As Exception
            Finally
                conn.Close()
            End Try

            conn.Close()
            tb = ds.Tables(0)
            myCla.ds2table(tb, Table2)
            conn.Close()
            Exit Sub

            For i = 0 To tb.Rows.Count - 1
                For j = 0 To Xlabel.Length - 1
                    If Xlabel(j)(0) = tb.Rows(i).Item(0).ToString.Split("/")(2) Then
                        For k = 0 To Datalabels.Length - 1
                            data(k)(j) = Convert.ToDouble(IIf(IsDBNull(tb.Rows(i).Item(Datalabels(k))), 0, tb.Rows(i).Item(Datalabels(k))))
                        Next
                        Exit For
                    End If
                Next

            Next
            myCla.ds2table(tb, Table2)
            conn.Close()
        End If




        cstext = New StringBuilder
        cstext.Append(myCla.toJStext_new("canvasX", "Ink Defect Ratio", Xlabel, chartType, Datalabels, data, Is_ratio, BColor, yaxis, False, 100))
        myCla.cstext2Canvas(Page, cstext, "canvasX", Table1)

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim E_date, S_date As Date
        E_date = New Date(Mid(datepicker_E.Text, 7, 4), Mid(datepicker_E.Text, 1, 2), Mid(datepicker_E.Text, 4, 2), 23, 59, 59)
        S_date = New Date(Mid(datepicker_S.Text, 7, 4), Mid(datepicker_S.Text, 1, 2), Mid(datepicker_S.Text, 4, 2), 0, 0, 0)

        defect_count(byDuration(RadioButtonList1.SelectedIndex), S_date, E_date)
        defect_GoYield(byDuration(RadioButtonList1.SelectedIndex), S_date, E_date)
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim E_date, S_date As Date
        E_date = New Date(Mid(datepicker_E.Text, 7, 4), Mid(datepicker_E.Text, 1, 2), Mid(datepicker_E.Text, 4, 2), 23, 59, 59)
        S_date = New Date(Mid(datepicker_S.Text, 7, 4), Mid(datepicker_S.Text, 1, 2), Mid(datepicker_S.Text, 4, 2), 0, 0, 0)

        defect_looose_Ratio(S_date, E_date)
    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim E_date, S_date As Date
        E_date = New Date(Mid(datepicker_E.Text, 7, 4), Mid(datepicker_E.Text, 1, 2), Mid(datepicker_E.Text, 4, 2), 23, 59, 59)
        S_date = New Date(Mid(datepicker_S.Text, 7, 4), Mid(datepicker_S.Text, 1, 2), Mid(datepicker_S.Text, 4, 2), 0, 0, 0)
        defect_hour_Vs_cntsize(S_date, E_date)
    End Sub
End Class
