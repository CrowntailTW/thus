
Imports System.Data.OleDb
Imports System.Data
Imports System.IO

Partial Class CutInfo
    Inherits System.Web.UI.Page
    Public path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData\"
    Private Sub CutInfo_Load(sender As Object, e As EventArgs) Handles Me.Load, Button1.Click

        update_Stdev()
    End Sub


    Public Sub update_Stdev()
        Dim i, j, k As Integer
        Dim cut() As String = New String() {"AX", "AY", "BX", "BY"}

        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim path As String
        Dim Sql As String
        Dim conn_string As String

        Dim Datalabels(3) As String '= New String() {"20", "下刀"}
        Dim Is_ratio(3) As Boolean '= New Boolean() {False, False}
        Dim BColor() As String = New String() {"window.chartColors.press_AX", "window.chartColors.press_AY", "window.chartColors.press_BX", "window.chartColors.press_BY"}
        Dim chartType() As String = New String() {"bar", "bar", "bar", "bar"}
        Dim yaxis() As String = New String() {"y1", "y1", "y1", "y1"}

        Dim Xlabel(39)() As String
        Dim data(3)() As Nullable(Of Double)

        Dim cstext As New StringBuilder()

        For i = 0 To Xlabel.Length - 1
            ReDim Xlabel(i)(1)
        Next
        For i = 0 To data.Length - 1
            ReDim data(i)(39)
        Next

        For i = 0 To 14

            path = path_ServerData & "CCT" & num2eqp(i + 1) & "00\" & Format(Now, "yyyyMM") & "\" & Format(Now, "yyyyMM") & "StdevInfo.mdb"

            If File.Exists(path) Then
                conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";"
                conn = New OleDbConnection(conn_string)
                conn.Open()

                Sql = " "
                Sql = Sql & " Select top 1 *"
                Sql = Sql & " From History "
                Sql = Sql & " Order by (TimeA) desc  "

                ds.Reset()
                adapter = New OleDbDataAdapter(Sql, conn)
                adapter.Fill(ds)
                adapter.Dispose()

                tb = ds.Tables(0)

                For j = 0 To 3
                    Datalabels(j) = (cut(j))
                    For k = 0 To 19
                        'PresureAX1
                        If j Mod 2 = 0 Then
                            data(j)(19 - k) = Convert.ToDouble(tb.Rows(0).Item("StDev" & cut(j) & k + 1) * IIf(j \ 2 = 0, 1, -1))
                        Else
                            data(j)(k + 20) = Convert.ToDouble(tb.Rows(0).Item("StDev" & cut(j) & k + 1) * IIf(j \ 2 = 0, 1, -1))
                        End If

                    Next
                Next

                For j = 1 To 40
                    Xlabel(j - 1)(0) = IIf(j <= 20, 21 - j, j - 20)
                    Xlabel(j - 1)(1) = IIf(j <= 20, 21 - j, j - 20)
                Next
                conn.Close()
            End If

            Dim mycls As New Class1
            cstext = New StringBuilder
            cstext.Append(mycls.toJStext_new("canvasX" & i + 1, _
                                          "Cut Cutter StDevErr CCT#" & num2eqp(i + 1), _
                                          Xlabel, _
                                          chartType, _
                                          Datalabels, _
                                          data, _
                                          Is_ratio, _
                                          BColor, _
                                          yaxis, _
                                          True))

            mycls.cstext2Canvas(Page, cstext, "canvasX" & i + 1, Table1)

            For j = 0 To data.Length - 1

                ReDim data(j)(39)

            Next

            ''''''''Dim cell As New TableCell
            ''''''''Dim li As New Literal
            ''''''''li.Text = " <canvas id=" & Chr(34) & "canvasX" & i + 1 & Chr(34) & " style=" & Chr(34) & "width:100%;height:100%" & Chr(34) & "></canvas>"
            ''''''''cell.Controls.Add(li)
            ''''''''Dim row As New TableRow
            ''''''''row.Cells.Add(cell)
            ''''''''row.Height = 400
            ''''''''Table1.Rows.Add(row)

            ''''''''Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp" & i + 1, cstext.ToString, False)

            ''''''''cstext = New StringBuilder


            ''''''''cstext.Append("<Script>")

            ''''''''cstext.Append("")

            ''''''''cstext.Append("</Script>")


            ''''''''Page.ClientScript.RegisterStartupScript(Me.GetType, "key_eqp" & i + 1, cstext.ToString, False)
        Next
    End Sub

    Public Function toJStext(ByVal canvas As String, _
                            ByVal ChartTitel As String, _
                            ByVal Xlabels()() As String, _
                            ByVal ChartType() As String, _
                            ByVal Datalabels() As String, _
                            ByVal Data()() As Double, _
                            ByVal Is_ratio() As Boolean, _
                            ByVal BColor() As String, _
                            ByVal yaxis() As String, _
                            ByVal isstacked As Boolean, _
    Optional ByVal ratio_max As Integer = 35) As String

        Dim i, j As Integer
        Dim cstext As New StringBuilder()

        cstext.Append("<script>    var ctx" & canvas & " = document.getElementById(" & Chr(34) & canvas & Chr(34) & ");  var myChart" & canvas & " = new Chart(ctx" & canvas & ",")
        cstext.Append("{ type: 'bar',")
        cstext.Append("data: {")

        cstext.Append("labels: [")
        For i = 0 To Xlabels.Length - 1
            cstext.Append("[")
            cstext.Append(Chr(34) & Xlabels(i)(0) & Chr(34))
            cstext.Append(",")
            cstext.Append(Chr(34) & Xlabels(i)(1) & Chr(34))
            cstext.Append("]")
            If i <> Xlabels.Length - 1 Then cstext.Append(",")
        Next
        cstext.Append("],")

        cstext.Append("datasets: [")

        For i = 0 To Data.Length - 1
            cstext.Append("{")

            cstext.Append("yAxisID :" & Chr(34) & yaxis(i) & Chr(34) & " ,")
            cstext.Append("type: '" & ChartType(i) & "',")
            cstext.Append("label: '" & Datalabels(i) & "',")
            cstext.Append("lineTension: 0,")

            If ChartType(i) = "line" Then
                cstext.Append("fill: false,")
                cstext.Append("pointRadius: 1,")
                cstext.Append("pointHoverRadius: 2,")
                cstext.Append("borderWidth: 4,")
                cstext.Append("borderColor: " & BColor(i) & ",")
                cstext.Append("backgroundColor: " & BColor(i) & ",")
            Else
                cstext.Append("borderWidth: 1,")
                cstext.Append("backgroundColor: " & BColor(i) & ",") 'color(chartColors.red).alpha(0.5).rgbString()
            End If

            cstext.Append("data: [")
            For j = 0 To Data(i).Length - 1

                Dim tmp_data As Double = IIf(Is_ratio(i), Data(i)(j) * 100, Data(i)(j))
                cstext.Append(tmp_data)
                If j <> Data(i).Length - 1 Then cstext.Append(",")
            Next
            cstext.Append("]")

            cstext.Append("}")

            If i <> Data.Length - 1 Then cstext.Append(",")
        Next

        cstext.Append("]")
        cstext.Append("}")
        cstext.Append(",")


        cstext.Append(" options: {")
        cstext.Append("          responsive: false,")
        cstext.Append("          animation : false,")

        cstext.Append("          title:{")
        cstext.Append("                  display:true,")
        cstext.Append("                  text:" & Chr(34) & ChartTitel & Chr(34))
        cstext.Append("                }, ")
        cstext.Append("          tooltips: { mode: 'index',  intersect: true},")
        cstext.Append("          scales: { ")

        If isstacked Then
            cstext.Append("               xAxes: [{ ")
            cstext.Append("                       stacked : true")
            cstext.Append("                      }], ")
        End If

        cstext.Append("                   yAxes: [ ")
        cstext.Append("                            { ")
        cstext.Append("                            type: " & Chr(34) & "linear" & Chr(34) & ", ")
        cstext.Append("                            display: true,")
        cstext.Append("                            ticks:  ")
        cstext.Append("                                 { ")
        cstext.Append("                                  min: 0,  ")
        cstext.Append("                                  max:  " & ratio_max)
        cstext.Append("                                 }, ")
        cstext.Append("                            position: " & Chr(34) & "right" & Chr(34) & ",")
        cstext.Append("                            id: " & Chr(34) & yaxis(0) & Chr(34) & " ")

        cstext.Append("                            }, { ")

        cstext.Append("                            type: " & Chr(34) & "linear" & Chr(34) & ", ")

        If isstacked Then
            cstext.Append("                        stacked : true, ")
        End If

        cstext.Append("                            display: true,")

        cstext.Append("                            ticks:{brginAtZero : false},")

        cstext.Append("                            position: " & Chr(34) & "left" & Chr(34) & ",")
        cstext.Append("                            id: " & Chr(34) & yaxis(yaxis.Length - 1) & Chr(34) & ",  ")
        cstext.Append("                            gridLines: {  drawOnChartArea: false     }")
        cstext.Append("                           }")
        cstext.Append("                         ]")
        cstext.Append("                    }}});myChart" & canvas & ".fillStyle='green'; myChart" & canvas & ".fillRect = (10,10,160,160); myChart" & canvas & ".stroke();</script>")

        Return cstext.ToString

    End Function

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

End Class
