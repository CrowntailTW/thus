Imports System.Data
Imports System.Data.OleDb
Imports System.Diagnostics
Imports System.IO

Partial Class Pressure_Info
    Inherits System.Web.UI.Page
    Dim path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData"
    Private Sub Pressure_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim default_file_root As String = "E:\WebSite_new_WithCanvas\images\A\"
        Dim FromDate As Date = IIf(Context.Request.QueryString("FromDate") = "", Now.ToShortDateString, Context.Request.QueryString("FromDate"))
        Dim ToDate As Date = IIf(Context.Request.QueryString("ToDate") = "", DateAdd(DateInterval.Day, 1, Now).ToShortDateString, Context.Request.QueryString("ToDate"))
        Dim EqpId As String = IIf(Context.Request.QueryString("EqpId") = "", "CCT100", Context.Request.QueryString("EqpId"))

        If EqpId <> "" Then

            update_pressure(FromDate, ToDate, EqpId)

        Else

            Response.Write("Respone Error")
            Exit Sub

        End If

    End Sub

    Public Sub update_pressure(ByVal FromDate As Date, ByVal ToDate As Date, ByVal EqpID As String)
        Dim i, j, k As Integer
        Dim cut() As String = New String() {"AX", "AY", "BX", "BY"}

        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim dv As DataView
        Dim path As String
        Dim Sql As String
        Dim conn_string As String
        Dim updateTime As String = ""

        Dim Datalabels(19) As String
        Dim Is_ratio(19) As Boolean
        Dim BColor() As String = New String() {"window.chartColors.red", "window.chartColors.orange", _
        "window.chartColors.yellow", "window.chartColors.green", _
        "window.chartColors.blue", "window.chartColors.purple", _
        "window.chartColors.grey", "window.chartColors.yellowgreen", _
        "window.chartColors.press_AX", "window.chartColors.press_AY", _
        "window.chartColors.press_BX", "window.chartColors.press_BY", _
        "window.chartColors.red", "window.chartColors.orange", _
        "window.chartColors.yellow", "window.chartColors.green", _
        "window.chartColors.blue", "window.chartColors.purple", _
        "window.chartColors.grey", "window.chartColors.yellowgreen"}

        Dim chartType() As String = New String() {"line", "line", "line", "line", "line", "line", "line", "line", "line", "line", "line", "line", "line", "line", "line", "line", "line", "line", "line", "line"}
        Dim yaxis() As String = New String() {"y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2", "y2"}

        Dim Xlabel()() As String
        Dim data(19)() As Nullable(Of Double)
        Dim mycls As New Class1
        Dim cstext As New StringBuilder()

        path = path_ServerData & "\" & EqpID & "\" & Format(FromDate, "yyyyMM") & "\" & Format(FromDate, "yyyyMM") & "PresureMileageInfo.mdb"
        updateTime = 0
        If File.Exists(path) Then
            conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
            conn = New OleDbConnection(conn_string)
            conn.Open()

            Sql = " "
            Sql = Sql & " Select *"
            Sql = Sql & " From History "
            Sql = Sql & " Where val(TimeA) >= " & Format(FromDate, "yyyyMMddHHmm")
            Sql = Sql & " And   val(TimeA) <= " & Format(ToDate, "yyyyMMddHHmm")
            Sql = Sql & " Order by (TimeA)asc  "

            adapter = New OleDbDataAdapter(Sql, conn)
            adapter.Fill(ds)
            adapter.Dispose()

            tb = ds.Tables(0)

            dv = tb.DefaultView
            Dim xt As DataTable = dv.ToTable(True, "TimeA")

            'Xlabel
            ReDim Xlabel(xt.Rows.Count - 1)
            For i = 0 To xt.Rows.Count - 1
                ReDim Xlabel(i)(1)
                Xlabel(i)(0) = xt.Rows(i).Item("TimeA")
                Debug.Print(Xlabel(i)(0))
            Next

            For i = 0 To 3 'AX AY BX BY
                Dim max As Integer = -99
                Dim min As Integer = 99
                For j = 0 To 20 - 1 'AX1~AX20
                    ReDim data(j)(xt.Rows.Count - 1)
                    Datalabels(j) = cut(i) & j + 1
                    Is_ratio(j) = False
                    For t As Integer = 0 To xt.Rows.Count - 1
                        Dim tmpdata As Nullable(Of Double)
                        tmpdata = Convert.ToDouble(tb.Rows(t).Item("Presure" & cut(i) & j + 1))
                        If tmpdata = 0 Then
                            data(j)(t) = Nothing
                        Else
                            data(j)(t) = tmpdata
                            max = IIf(data(j)(t) > max, data(j)(t), max)
                            min = IIf(data(j)(t) < min, data(j)(t), min)
                        End If
                    Next
                Next

                cstext = New StringBuilder
                cstext.Append(mycls.toJStext_new("canvasXcut" & cut(i), _
                                                   cut(i) & "Presure CCT#" & EqpID, _
                                                  Xlabel, _
                                                  chartType, _
                                                  Datalabels, _
                                                  data, _
                                                  Is_ratio, _
                                                  BColor, _
                                                  yaxis, _
                                                  False,,,, max + 20, min - 20))
                mycls.cstext2Canvas(Page, cstext, "canvasXcut" & cut(i), Table1)
                'Dim cell As New TableCell
                'Dim li As New Literal
                'Dim row As New TableRow
                'li.Text = "<canvas id=" & Chr(34) & "canvasXcut" & cut(i) & Chr(34) & "></canvas>"
                'cell.Controls.Add(li)
                'row.Cells.Add(cell)
                'row.HorizontalAlign = HorizontalAlign.Center
                'row.Height = 400
                'Table1.Rows.Add(row)
                'Page.ClientScript.RegisterStartupScript(Me.GetType, "key_cut" & cut(i), cstext.ToString, False)

            Next
        End If
    End Sub
End Class
