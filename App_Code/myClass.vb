Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Diagnostics

Public Class Class1
    Public Path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData\"
    Sub Access2CSV(ByVal path_Sou As String, ByVal tbl As String, ByVal path_des As String)
        Dim i, j, k, l As Integer
        Dim conn As New OleDbConnection
        Dim mySql As String
        Dim ds As New DataSet
        Dim tb As New DataTable
        Dim CSVString As String = ""

        mySql = ""
        mySql = mySql & " Select *"
        mySql = mySql & " From " & tbl

        Try
            conn = Class_Sql.CreateConn(path_Sou)
            conn.Open()
            ds = Class_Sql.GetDB(conn, mySql)
            tb = ds.Tables(0)

            For i = 0 To tb.Columns.Count - 1
                CSVString = CSVString & tb.Columns(i).ColumnName
                If i <> tb.Columns.Count - 1 Then CSVString = CSVString & ","
            Next
            CSVString = CSVString & vbNewLine
            For i = 0 To tb.Rows.Count - 1
                For j = 0 To tb.Columns.Count - 1
                    CSVString = Trim(CSVString & tb.Rows(i).Item(j))
                    If i <> tb.Columns.Count - 1 Then CSVString = CSVString & ","
                Next
                CSVString = CSVString & vbNewLine
            Next

            File.WriteAllText(path_des, CSVString)

        Catch ex As Exception
            ' errRecord(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod().ToString())
        Finally
            conn.Close()
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
        Return ""
    End Function
    Public Function eqp2num(ByVal eqp As String) As Integer

        If IsNumeric(eqp) Then Return eqp
        If (Asc(eqp) < Asc("A") Or Asc(eqp) > Asc("F")) Then Return ""
        Return Asc(eqp) - Asc("A") + 10

    End Function
    Function getTargetX(ByVal RecipeNo As String) As Double
        Dim Target As Double = -1
        Try
            If File.Exists("\\101.101.101.203\CCT000\Update\target.up") Then
                Dim ss() = File.ReadAllLines("\\101.101.101.203\CCT000\Update\target.up")

                For Each s As String In ss
                    Dim tmpRecipeNo As String
                    Dim tmpTarget As Double

                    tmpRecipeNo = s.Split(", ")(0)
                    tmpTarget = s.Split(", ")(1)
                    If tmpRecipeNo = RecipeNo Then
                        'printRichTextBoxMain(tmpTarget, main.RichTextBox_Status)
                        Return tmpTarget
                    End If
                Next

            End If
        Catch ex As Exception
            'errRecord(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod().ToString())
            Debug.Print("aaaaaaaaaaaaaaaaaaaaaaaa")
        End Try
        Select Case RecipeNo
            Case "84"
                Target = 0.1'V
            Case "31"
                Target = 0.1'v
            Case "73"
                Target = 0.0668'V
            Case "72"
                Target = 0.1'V
            Case "22"
                Target = 0.05'
            Case "02"
                Target = 0.04'V
            Case "1A"
                Target = 0.03 'V
            Case "4C"
                Target = 0.07 'V
        End Select
        Return Target
    End Function
    Function getTarget(ByVal RecipeNo As String) As Double

        Dim ds As New DataSet
        Dim tb As New DataTable
        Dim conn As New OleDbConnection
        Dim path As String
        Dim mySql As String
        Dim i As Integer
        Dim Target As Double = -1

        Try
            If File.Exists("\\101.101.101.203\CCT000\Update\tmp\RecipeTable.mdb") Then
                path = "\\101.101.101.203\CCT000\Update\tmp\RecipeTable.mdb"
            Else
                Exit Function
            End If

            conn = Class_Sql.CreateConn(path)
            conn.Open()

            mySql = ""
            mySql = mySql & " SELECT * FROM RecipeTable"
            mySql = mySql & " WHERE CCIRecipeNo = '" & RecipeNo & "'"


            ds = Class_Sql.GetDB(conn, mySql)
            tb = ds.Tables(0)

            Target = IIf(IsDBNull(tb.Rows(0).Item("Target")), -1, tb.Rows(0).Item("Target"))


            conn.Close()


        Catch ex As Exception
            ' errRecord(ex.Message, System.Reflection.MethodInfo.GetCurrentMethod().ToString())
        End Try
        Return Target
    End Function
    Function ds2table(ByVal tb As DataTable, ByRef myTable As Table) As Boolean

        Dim i, j As Integer
        Dim myrow As TableRow
        Dim mycell As TableCell

        Try

            myrow = New TableRow
            For j = 0 To tb.Columns.Count - 1
                If tb.Columns(j).Caption.ToString <> DBNull.Value.ToString Then
                    mycell = New TableCell
                    mycell.Text = tb.Columns(j).Caption.ToString
                    myrow.Cells.Add(mycell)
                Else
                    mycell = New TableCell
                    mycell.Text = "" ' tb.Columns(j).Caption.ToString
                    myrow.Cells.Add(mycell)
                End If
            Next

            If myTable.Rows.Count = 0 Then
                myTable.Rows.Add(myrow)
            Else
                If Table.ReferenceEquals(myrow, myTable.Rows(0)) Then
                    myTable.Rows.Add(myrow)
                End If
            End If

            For i = 0 To tb.Rows.Count - 1
                myrow = New TableRow
                For j = 0 To tb.Columns.Count - 1
                    If tb.Rows(i).Item(j).ToString <> DBNull.Value.ToString Then
                        mycell = New TableCell
                        mycell.Text = tb.Rows(i).Item(j)
                        myrow.Cells.Add(mycell)
                    Else
                        mycell = New TableCell
                        mycell.Text = "" ' tb.Rows(i).Item(j)
                        myrow.Cells.Add(mycell)
                    End If
                Next
                myTable.Rows.Add(myrow)
            Next

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Function ds2gridview(ByVal ds As DataSet, ByRef myTable As GridView, Optional _select As String = "") As Boolean

        Dim tb As DataTable
        tb = ds.Tables(0)

        Try
            If _select = "" Then
                myTable.DataSource = ds.Tables(0)
            Else
                myTable.DataSource = ds.Tables(0).Select(_select)
            End If

            myTable.DataBind()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Function toJStext_new(ByVal canvas As String, _
                            ByVal ChartTitel As String, _
                            ByVal Xlabels()() As String, _
                            ByVal ChartTypeA() As String, _
                            ByVal DatalabelsA() As String, _
                            ByVal DataA()() As Nullable(Of Double), _
                            ByVal Is_ratioA() As Boolean, _
                            ByVal BColorA() As String, _
                            ByVal yaxisA() As String, _
                            ByVal isstacked As Boolean, _
                            Optional ratio_max As Integer = 35, _
                            Optional Target1 As Double = -1, _
                            Optional Target2 As Double = -1, _
                            Optional move_Max As Double = -1, _
                            Optional move_min As Double = -1 _
                             ) As String

        Dim i, j As Integer
        Dim iftarget As Integer = 0
        Dim cstext As New StringBuilder()
        Dim ChartType() As String
        Dim Datalabels() As String
        Dim Data()() As Nullable(Of Double)
        Dim Is_ratio() As Boolean
        Dim BColor() As String
        Dim yaxis() As String

        Dim Is_byModel As Boolean
        Dim tmp_T, tmp_F As Integer
        Dim tmpRatio6H As Double
        Dim tmpMove6H As Integer

        For i = 0 To Is_ratioA.Length - 1
            tmp_T = tmp_T + IIf(Is_ratioA(i), 1, 0)
            tmp_F = tmp_F + IIf(Is_ratioA(i), 0, 1)
        Next

        If tmp_T = tmp_F Then Is_byModel = True

        If Is_byModel Then
            For i = DataA.Length - 1 To DataA.Length / 2 Step -1
                For j = 0 To DataA(i).Length - 1
                    If DataA(i)(j) = 0 Or IsNothing(DataA(i)(j)) Then
                        DataA(i)(j) = Nothing
                        DataA(i - DataA.Length / 2)(j) = Nothing ' Nothing
                    End If
                Next
            Next
        End If

        If (Target1 = -1 And Target2 = -1) Then
            iftarget = 0
        Else
            iftarget = 1
        End If

        'ChartType
        ReDim ChartType(ChartTypeA.Length - 1 + iftarget)
        Array.Copy(ChartTypeA, 0, ChartType, iftarget, ChartTypeA.Length)

        'Datalabels
        ReDim Datalabels(DatalabelsA.Length - 1 + iftarget)
        Array.Copy(DatalabelsA, 0, Datalabels, iftarget, DatalabelsA.Length)

        'Data
        ReDim Data(DataA.Length - 1 + iftarget)
        Dim tmp_t1(DataA(0).Length - 1), tmp_t2(DataA(0).Length - 1) As Nullable(Of Double)
        For i = 0 To tmp_t1.Length - 1
            tmp_t1(i) = IIf(Target1 = -1, Nothing, Convert.ToDouble(Target1 * 100))
            tmp_t2(i) = IIf(Target2 = -1, Nothing, Convert.ToDouble(Target2 * 100)) 'Target2
        Next
        Array.Copy(DataA, 0, Data, iftarget, DataA.Length)

        'Is_ratio
        ReDim Is_ratio(Is_ratioA.Length - 1 + iftarget)
        Array.Copy(Is_ratioA, 0, Is_ratio, iftarget, Is_ratioA.Length)

        'BColor
        ReDim BColor(BColorA.Length - 1 + iftarget)
        Array.Copy(BColorA, 0, BColor, iftarget, BColorA.Length)

        'yaxis
        ReDim yaxis(yaxisA.Length - 1 + iftarget)
        Array.Copy(yaxisA, 0, yaxis, iftarget, yaxisA.Length)


        If Not (Target1 = -1 And Target2 = -1) Then
            Array.Copy(New String() {"line"}, 0, ChartType, 0, 1)
            Array.Copy(New String() {"出貨 Target"}, 0, Datalabels, 0, 1)
            Array.Copy(New Nullable(Of Double)()() {tmp_t1}, 0, Data, 0, 1)
            Array.Copy(New Boolean() {False}, 0, Is_ratio, 0, 1)
            Array.Copy(New String() {"window.chartColors.Target1red"}, 0, BColor, 0, 1)
            Array.Copy(New String() {"y1"}, 0, yaxis, 0, 1)
        End If

        cstext.Append("<script>")
        cstext.Append("var data ={type: 'bar',")
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
                cstext.Append("pointRadius: 2,")
                cstext.Append("pointHoverRadius: 3,")
                cstext.Append("borderWidth: 4,")
                cstext.Append("borderColor: " & BColor(i) & ",")
                cstext.Append("backgroundColor: " & BColor(i) & ",")
            Else
                cstext.Append("borderWidth: 1,")
                cstext.Append("backgroundColor: " & BColor(i) & ",")
            End If

            cstext.Append("data: [")
            For j = 0 To Data(i).Length - 1
                Dim tmp_data As Double = IIf(Is_ratio(i), Math.Round(Convert.ToDouble(Data(i)(j) * 100), 2), Data(i)(j))
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
        cstext.Append("          responsive   : false,")
        cstext.Append("          animation    : false,")
        cstext.Append("          title        : { display:true,    text:" & Chr(34) & ChartTitel & Chr(34) & "},")
        cstext.Append("          tooltips     : { mode: 'index',  intersect: true},")
        cstext.Append("          scales       : { ")
        cstext.Append("                        xAxes: [{ ")
        cstext.Append("                                   stacked   : " & IIf(isstacked, "true", "false") & ",")
        cstext.Append("                                   ticks     : {")
        cstext.Append("                                               autoskip : false, ")
        cstext.Append("                                               callback : function(value,index,values){")
        If Xlabels.Length - 1 > 20 Then
            cstext.Append("                                                                                if(index % 4 == 0){ return value ; }else{return " & Chr(34) & Chr(34) & "; }")
        Else
            cstext.Append("                                                                                return value ;")
        End If
        cstext.Append("                                                                                      } ")
        cstext.Append("                                               },")
        cstext.Append("                               }], ") 'xAxes
        cstext.Append("                        yAxes: [{ ")
        cstext.Append("                                   type      : " & Chr(34) & "linear" & Chr(34) & ", ")
        cstext.Append("                                   display   : true,")


        cstext.Append("         plotlines: [{ ")
        cstext.Append("                 color: '#FFFF00', ") '
        cstext.Append("                 value: " & Target1 & ", ")
        cstext.Append("                 width: 10 ")
        cstext.Append("             }],")



        cstext.Append("                                   ticks     : {")
        cstext.Append("                                               min: 0,")
        cstext.Append("                                               max:   " & ratio_max & ", ")
        cstext.Append("                                               callback : function(value){")
        cstext.Append("                                                                            return value + " & Chr(34) & "%" & Chr(34))
        cstext.Append("                                                                         } ")
        cstext.Append("                                               }, ")
        cstext.Append("                                   position  : " & Chr(34) & "right" & Chr(34) & ",")
        cstext.Append("                                   id        : " & Chr(34) & yaxis(0) & Chr(34) & ", ")
        cstext.Append("                                   scaleLabel:{ display :true , labelString : " & Chr(34) & "Ratio %" & Chr(34) & " }")
        cstext.Append("                                }, { ")
        cstext.Append("                                   type      : " & Chr(34) & "linear" & Chr(34) & ", ")
        cstext.Append("                                   stacked   : " & IIf(isstacked, "true", "false") & ", ")
        cstext.Append("                                   display   : true,")
        '---
        cstext.Append("                                   ticks     : {")
        If move_Max <> -1 Then
            cstext.Append("                                               max: " & move_Max)
        End If
        If move_Max <> -1 And move_min <> -1 Then
            cstext.Append("                                                                     ,")
        End If
        If move_Max <> -1 Then
            cstext.Append("                                               min: " & move_min)
        End If
        cstext.Append("                                               }, ")
        '---
        cstext.Append("                                   position  : " & Chr(34) & "left" & Chr(34) & ",")
        cstext.Append("                                   id        : " & Chr(34) & yaxis(yaxis.Length - 1) & Chr(34) & ",  ")
        cstext.Append("                                   scaleLabel:{ display :true , labelString : " & Chr(34) & "Move" & Chr(34) & " },")
        cstext.Append("                                   gridLines : {  drawOnChartArea: false     }")
        cstext.Append("                                }]") 'yAxes
        cstext.Append("                          }") 'scales
        cstext.Append("      }") 'optoin
        cstext.Append("};") 'data

        cstext.Append("var ctx" & canvas & " = document.getElementById(" & Chr(34) & canvas & Chr(34) & ").getContext('2d');  ")
        cstext.Append("var myChart" & canvas & " = new Chart(ctx" & canvas & ",data );")

        cstext.Append("ctx" & canvas & ".fillStyle = ""#FF6384"";")
        cstext.Append("ctx" & canvas & ".font = ""3px Arial"";")
        'cstext.Append("ctx" & canvas & ".fillText(""ASP"", 5, 10);")

        cstext.Append("ctx" & canvas & ".beginPath();")
        cstext.Append("ctx" & canvas & ".arc(20, 20, 10, 0, 2 * Math.PI);")
        cstext.Append("ctx" & canvas & ".lineWidth = 3;")
        cstext.Append("ctx" & canvas & ".strokeStyle = ""#FF6384"";")
        cstext.Append("ctx" & canvas & ".stroke();")



        '''''cstext.Append(" var             ctxImg" & canvas & " = document.getElementById(" & Chr(34) & canvas & Chr(34) & ");")
        '''''cstext.Append(" var dataURL  =  ctxImg" & canvas & ".toDataURL();")
        '''''cstext.Append(" document.getElementById('Img" & canvas & "').src = dataURL ;")


        cstext.Append("</script>")

        Return cstext.ToString

    End Function

    Public Function toJStext_highChart(ByVal divID As String, _
                            ByVal ChartTitel As String, _
                            ByVal Xlabels()() As String, _
                            ByVal ChartTypeA() As String, _
                            ByVal DatalabelsA() As String, _
                            ByVal DataA()() As Nullable(Of Double), _
                            ByVal Is_ratioA() As Boolean, _
                            ByVal BColorA() As String, _
                            ByVal yaxisA() As String, _
                            ByVal isstacked As Boolean, _
                            Optional ratio_max As Integer = 35, _
                            Optional Target1 As Double = -1, _
                            Optional Target2 As Double = -1 _
                             ) As String

        Dim i, j As Integer
        Dim iftarget As Integer = 0
        Dim cstext As New StringBuilder()

        Dim Is_byModel As Boolean
        Dim tmp_T, tmp_F As Integer

        For i = 0 To Is_ratioA.Length - 1
            tmp_T = tmp_T + IIf(Is_ratioA(i), 1, 0)
            tmp_F = tmp_F + IIf(Is_ratioA(i), 0, 1)
        Next

        If tmp_T = tmp_F Then Is_byModel = True

        If Is_byModel Then
            For i = DataA.Length - 1 To DataA.Length / 2 Step -1
                For j = 0 To DataA(i).Length - 1
                    If DataA(i)(j) = 0 Or IsNothing(DataA(i)(j)) Then
                        DataA(i)(j) = Nothing
                        DataA(i - DataA.Length / 2)(j) = Nothing ' Nothing
                    End If
                Next
            Next
        End If

        Target1 = Target1 * 100
        Target2 = Target2 * 100


        ''''''        Highcharts.getOptions().colors = Highcharts.map(Highcharts.getOptions().colors, Function(color) {
        ''''''    return Highcharts.Color(color)
        ''''''        .setOpacity(0.5)
        ''''''        .get('rgba');
        ''''''});

        cstext.Append("<script>")
        cstext.Append(" Highcharts.chart('" & divID & "', { ")
        cstext.Append("     chart: { ")
        'cstext.Append("             zoomType: 'xy' , ") ' animation: false
        cstext.Append("             animation: false ") ' 
        cstext.Append("     }, ")
        cstext.Append("     title: { ")
        cstext.Append("     		align: 'center', ")
        cstext.Append("         text: '" & ChartTitel & "' ")
        cstext.Append("     }, ")
        cstext.Append("     subtitle: { ")
        cstext.Append("     		align: 'center', ")
        cstext.Append("         text: 'minor title' ")
        cstext.Append("     }, ")

        cstext.Append("     xAxis: { ")
        cstext.Append("         categories: [           ")


        For i = 0 To Xlabels.Length - 1
            cstext.Append("'" & Xlabels(i)(0) & "'")
            If Not i = Xlabels.Length - 1 Then
                cstext.Append(",")
            End If
        Next

        cstext.Append("             ] ")
        cstext.Append("     }, ")

        cstext.Append("     yAxis: [{ ")
        cstext.Append("         min: 0, ")
        cstext.Append("         title: { ")
        cstext.Append("             text: 'Move (chips)' ")
        cstext.Append("         } ")
        cstext.Append("     },{ ")
        cstext.Append("         min: 0, ")
        cstext.Append("         max: " & ratio_max & ", ")
        cstext.Append("         plotLines: [{ ")
        cstext.Append("                 value: " & Target1 & ", ")
        cstext.Append("                 color: 'orange', ")
        cstext.Append("                 dashStyle: 'shortdash', ")
        cstext.Append("                 width: 2, ")
        cstext.Append("                 label: { ")
        cstext.Append("                 text: 'warming' ")
        cstext.Append("                 } ")
        cstext.Append("             }, { ")
        cstext.Append("                 value: " & Target2 & ", ")
        cstext.Append("                 color: 'red', ")
        cstext.Append("                 dashStyle: 'shortdash', ")
        cstext.Append("                 width: 2, ")
        cstext.Append("                 label: { ")
        cstext.Append("                     text: 'shippment target' ")
        cstext.Append("                 } ")
        cstext.Append("         }], ")
        cstext.Append("         title: { ")
        cstext.Append("             text: 'Ratio (%)' ")
        cstext.Append("         }, ")
        cstext.Append("         opposite: true ")
        cstext.Append("     }],   ")

        cstext.Append("     legend: { shadow: false }, ")
        cstext.Append("     tooltip: { shared: true }, ")

        cstext.Append("     plotOptions: { ")
        cstext.Append("         column: { ")
        cstext.Append("             grouping: true, ")
        cstext.Append("             shadow: false, ")
        cstext.Append("             stacking: 'normal' ")
        cstext.Append("         } ")
        cstext.Append("     }, ")

        cstext.Append("     series: [")
        For i = DataA.Length - 1 To 0 Step -1

            Dim ccc As String = ChartTypeA(i)
            If ccc = "line" Then
                ccc = "line"
            Else
                ccc = "column"
            End If
            cstext.Append("                { ")
            cstext.Append("                  name: '" & DatalabelsA(i) & "', ")
            cstext.Append("                  type: '" & ccc & "', ")
            cstext.Append("                  color: " & BColorA(i) & ", ")
            Dim yy As String
            yy = yaxisA(i)

            If yy = "y1" Then
                yy = 1
            Else
                yy = 0
            End If

            cstext.Append("                  yAxis: " & yy & ", ")
            cstext.Append("                  data: [")

            For j = 0 To DataA(i).Length - 1
                cstext.Append("                   " & DataA(i)(j) * IIf(Is_ratioA(i), 100, 1) & "")
                If Not j = DataA(i).Length - 1 Then
                    cstext.Append(",")
                End If
            Next
            cstext.Append("                        ], ")
            cstext.Append("                  tooltip: { ")
            cstext.Append("                               valueSuffix: '" & IIf(Is_ratioA(i), "%", "chips") & "' ")
            cstext.Append("                           } ")
            cstext.Append("                }")
            If Not i = 0 Then
                cstext.Append(",")
            End If
        Next
        cstext.Append(" ] ")
        cstext.Append(" }); ")


        cstext.Append("</script>")

        Return cstext.ToString

    End Function

    Public Function JStext6H(ByVal canvas As String, ByVal text As String) As String

        Dim cstext As New StringBuilder()
        cstext.Append("<script>")


        cstext.Append("var ctx" & canvas & " = document.getElementById(" & Chr(34) & canvas & Chr(34) & ").getContext('2d');  ")


        cstext.Append("ctx" & canvas & ".fillStyle = ""#FF6384"";")
        cstext.Append("ctx" & canvas & ".font = ""5px Arial"";")
        cstext.Append("ctx" & canvas & ".fillText(""" & text & """, 5, 10);")

        'cstext.Append("ctx" & canvas & ".beginPath();")
        'cstext.Append("ctx" & canvas & ".arc(20, 20, 10, 0, 2 * Math.PI);")
        'cstext.Append("ctx" & canvas & ".lineWidth = 3;")
        'cstext.Append("ctx" & canvas & ".strokeStyle = ""#FF6384"";")
        'cstext.Append("ctx" & canvas & ".stroke();")

        cstext.Append("</script>")



        Return cstext.ToString
    End Function

    Function CCIEDGE2EDGE(ByVal NUM As Integer) As Integer

        'CCD#3  下上      CCD#1   下上
        '1st    64                20                
        '2nd    75                31
        Select Case NUM
            Case 0
                Return 5 'E
            Case 1
                Return 34 'OLD
            Case 2
                Return 6 ' F
            Case 3
                Return 4 ' D
            Case 4
                Return 1 ' A
            Case 5
                Return 7 ' G
            Case 6
                Return 2 ' B
            Case 7
                Return 8 ' H
        End Select

        Return -1
    End Function
    Function CCIEDGE2EDGECHAR(ByVal NUM As Integer) As String

        'CCD#3  下上      CCD#1   下上
        '1st    64                20                
        '2nd    75                31
        Select Case NUM
            Case 0
                Return "E" 'E
            Case 1
                Return "C" 'OLD
            Case 2
                Return "F" ' F
            Case 3
                Return "D" ' D
            Case 4
                Return "A" ' A
            Case 5
                Return "G" ' G
            Case 6
                Return "B" ' B
            Case 7
                Return "H" ' H
        End Select

        Return ""
    End Function


    Public Function ChippingLoose2tb(ByVal index As Integer, ByVal tmp_path_err As String()) As DataTable

        Dim mysql As String
        Dim conn As OleDbConnection
        Dim ds As New DataSet

        Dim AcerQisda_Spc_73() As Integer = New Integer() {1200, 400, 3000, 1200, 3000, 400, 3000, 400, 3000}
        Dim AcerQisda_In_73 As Integer = 300
        Dim AcerQisda_Out_73 As Integer = 500

        Dim AcerQisda_Spc_02() As Integer = New Integer() {1200, 300, 9000, 1200, 9000, 300, 9000, 300, 9000}
        Dim AcerQisda_In_02 As Integer = 300
        Dim AcerQisda_Out_02 As Integer = 800

        Dim AcerQisda_Spc_Test() As Integer = New Integer() {2000, 300, 3000, 1200, 3000, 300, 3000, 300, 3000}
        Dim AcerQisda_In_Test As Integer = 300
        Dim AcerQisda_Out_Test As Integer = 800

        Dim AcerQisdaSpc = New Integer()() {AcerQisda_Spc_Test, AcerQisda_Spc_02, AcerQisda_Spc_73, AcerQisda_Spc_Test, AcerQisda_Spc_Test}
        Dim AcerQisdaIn = New Integer() {AcerQisda_In_Test, AcerQisda_In_02, AcerQisda_In_73, AcerQisda_In_Test, AcerQisda_In_Test}
        Dim AcerQisdaOut = New Integer() {AcerQisda_Out_Test, AcerQisda_Out_02, AcerQisda_Out_73, AcerQisda_Out_Test, AcerQisda_Out_Test}
        Dim AcerQisdaAllowCount As Integer = 1
        Try

            mysql = ""
            mysql = mysql & " "
            mysql = mysql & " Select Time,RecipeNo,RecipeName,mid(Time,5,4) as T , PanelID , mid(Time,9,2) as TT "
            mysql = mysql & " From"
            mysql = mysql & " ("
            mysql = mysql & "    Select move.PanelID,NGITEM,Time,RecipeNo,RecipeName,Cnt_A_in,Cnt_A_out,Cnt_E_in,Cnt_E_Out,Cnt_G_In,Cnt_G_Out,Cnt_other from "

            mysql = mysql & "    ("
            mysql = mysql & "      ("
            mysql = mysql & "         ("
            mysql = mysql & "              (Select PanelID,NGITEM,Time,RecipeNo,RecipeName from DefectT1 where RecipeNo in ('02'))as move"
            mysql = mysql & "              left join"
            mysql = mysql & "              " & Sql_a("A", "04", AcerQisdaIn(index), AcerQisdaOut(index))
            mysql = mysql & "              On Move.panelID = A.panelID"

            mysql = mysql & "         )"
            mysql = mysql & "         Left Join"
            mysql = mysql & "         " & Sql_a("E", "00", AcerQisdaIn(index), AcerQisdaOut(index))
            mysql = mysql & "         on move.PanelID=E.PanelID"

            mysql = mysql & "      )"
            mysql = mysql & "      left join"
            mysql = mysql & "      " & Sql_a("G", "05", AcerQisdaIn(index), AcerQisdaOut(index))
            mysql = mysql & "      on move.PanelID=G.PanelID"
            mysql = mysql & "    )"
            mysql = mysql & "    left join"
            mysql = mysql & "    ("
            mysql = mysql & "       Select PanelID,Count(PanelID)as Cnt_other from DefectT2"
            mysql = mysql & "       Where "
            'C
            mysql = mysql & "          Val(Depth) > " & AcerQisdaSpc(index)(0) & " And Mid([position], 1, 2) = '01' and  mid([position],17,6)>334400"
            mysql = mysql & "       OR Val(Depth) > " & AcerQisdaSpc(index)(3) & " And Mid([position], 1, 2) = '01' and  mid([position],17,6)<=334400"
            'F                                                                                                                                                                                                                                                                             
            mysql = mysql & "       OR Val(Depth) > " & AcerQisdaSpc(index)(2) & " And Mid([position], 1, 2) = '06'"
            'D
            mysql = mysql & "       OR Val(Depth) > " & AcerQisdaSpc(index)(4) & " And Mid([position], 1, 2) = '03'"
            'B
            mysql = mysql & "       OR Val(Depth) > " & AcerQisdaSpc(index)(6) & " And Mid([position], 1, 2) = '02'"
            'H
            mysql = mysql & "       OR Val(Depth) > " & AcerQisdaSpc(index)(8) & " And Mid([position], 1, 2) = '07'"
            mysql = mysql & "       Group By PanelID"

            mysql = mysql & "    )as other"
            mysql = mysql & "    on Move.PanelID = other.PanelID"
            mysql = mysql & " )"
            mysql = mysql & " Where           "
            mysql = mysql & "           iif(isnull(Cnt_A_in),0,Cnt_A_in) <=" & AcerQisdaAllowCount & " And  iif(isnull(Cnt_A_out),0,Cnt_A_out) <=0 "
            mysql = mysql & " And       iif(isnull(Cnt_E_in),0,Cnt_E_in) <=" & AcerQisdaAllowCount & " And  iif(isnull(Cnt_E_out),0,Cnt_E_out) <=0 "
            mysql = mysql & " And       iif(isnull(Cnt_G_in),0,Cnt_G_in) <=" & AcerQisdaAllowCount & " And  iif(isnull(Cnt_G_out),0,Cnt_G_out) <=0 "
            mysql = mysql & " And       (iif(isnull(Cnt_A_in),0,Cnt_A_in)+iif(isnull(Cnt_E_in),0,Cnt_E_in)+iif(isnull(Cnt_G_in),0,Cnt_G_in))>0 "
            mysql = mysql & " And       iif(isnull(Cnt_other),0,Cnt_other) <=0 "
            mysql = mysql & " And       mid(NGITEM,3,1) = '1' "
            mysql = mysql & " And       RecipeNo in  ("
            'mysql = mysql & "'" &  分貨RecipeNo(index) & "'"
            mysql = mysql & "                        '02'"
            mysql = mysql & "                        ) "

            ds.Reset()

            Dim date_shift As Integer = 0

            For date_shift = 0 To tmp_path_err.Length - 1
                If File.Exists(tmp_path_err(date_shift)) Then
                    conn = Class_Sql.CreateConn(tmp_path_err(date_shift))

                    ds = Class_Sql.GetDB(conn, mysql, ds)
                    conn.Close()

                End If
            Next
        Catch ex As Exception


        End Try
        Return ds.Tables(0)

    End Function
    Public Function Sql_a(ByVal _edge As String, ByVal _edge_value As String, ByVal _in As Integer, ByVal _out As Integer) As String

        Dim mySql As String

        mySql = ""
        mySql = mySql & "("
        mySql = mySql & "     Select " & _edge & "_In.PanelID as PanelID,Cnt_" & _edge & "_In,Cnt_" & _edge & "_Out"
        mySql = mySql & "     From"
        mySql = mySql & "     ("
        mySql = mySql & "           ("
        mySql = mySql & "           Select PanelID,Count(PanelID) as Cnt_" & _edge & "_In From DefectT2" 'A in
        mySql = mySql & "           Where Mid([position], 1, 2) = '" & _edge_value & "'"
        mySql = mySql & "           And val(Depth) < " & _out
        mySql = mySql & "           And val(Depth) >=" & _in
        mySql = mySql & "           Group by PanelID "
        mySql = mySql & "           ) as " & _edge & "_In"
        mySql = mySql & "           Left Join"
        mySql = mySql & "           ("
        mySql = mySql & "           Select PanelID,Count(PanelID) as Cnt_" & _edge & "_Out From DefectT2" 'A out
        mySql = mySql & "           Where Mid([position], 1, 2) = '" & _edge_value & "'"
        mySql = mySql & "           And val(Depth) > " & _out
        mySql = mySql & "           Group by PanelID "
        mySql = mySql & "           ) as " & _edge & "_Out"
        mySql = mySql & "           On " & _edge & "_In.PanelID = " & _edge & "_Out.PanelID"
        mySql = mySql & "     )"
        mySql = mySql & ") as " & _edge & ""

        Return mySql

    End Function



    Public Sub cstext2Canvas(ByVal p As Page, ByVal cstext As StringBuilder, ByVal canvas As String, ByVal mytb As Table)

        Dim cell As New TableCell
        Dim li As New Literal

        'li.Text = " <div id=" & Chr(34) & "canvasX" & RecipeNo_Matrix(Recipe_shift) & Chr(34) & "></diV>></div>"
        li.Text = " <canvas id=" & Chr(34) & canvas & Chr(34) & " style=""height: 400px; width: 989px;"" ></canvas>" & _
        "" '  " <img id=" & Chr(34) & "Img" & canvas & Chr(34) & "src = """" />"
        cell.Controls.Add(li)

        cell.Width = 1000
        cell.height = 200
        Dim row As New TableRow
        row.Cells.Add(cell)
        row.HorizontalAlign = HorizontalAlign.Center

        'If eqp_count >= 6 Then
        row.Height = 200
        'Else
        '    row.Height = 400
        'End If
        mytb.Rows.Add(row)

        p.ClientScript.RegisterStartupScript(Me.GetType, canvas, cstext.ToString, False)


    End Sub

    Public Sub distribution(ByVal p As Page, ByVal ds As DataSet, ByVal mytb As Table, ByVal eqp As String)

        Dim cell As New TableCell
        Dim li As New Literal

        Dim tb, tb_RecipeNo As DataTable
        Dim shiftRecipe As Integer
        tb = ds.Tables(0)

        tb_RecipeNo = tb.DefaultView.ToTable(True, "RecipeNo")


        For shiftRecipe = 0 To tb_RecipeNo.Rows.Count - 1

            Dim tmprecipe As String = tb_RecipeNo.Rows(shiftRecipe).Item("RecipeNo")
            Dim tmpdiv As String = "Div" & eqp & "_" & tmprecipe
            Dim tmpcstext As New StringBuilder

            'cf
            tmpcstext.Append(ds2ChipDistribution(ds, tmpdiv & "_CF", tmprecipe, True))
            tmpcstext.Append(ds2ChipDistribution(ds, tmpdiv & "_TFT", tmprecipe, False))

            'li.Text = li.Text & "<div id = " & tmpdiv & " style ="" margin-left: auto ; margin-right: auto ; "">"
            li.Text = li.Text & " <div id =" & Chr(34) & tmpdiv & "_CF" & Chr(34) & " style =""  display: inline-block ; height : 400px ; min-width : 310px ; width : 600px ; margin : 0 auto ; background-color : LightPink ""></div>"
            li.Text = li.Text & " <div id =" & Chr(34) & tmpdiv & "_TFT" & Chr(34) & " style ="" display: inline-block ; height : 400px ; min-width : 310px ; width : 600px ; margin : 0 auto ; background-color : LightPink ""></div>"
            'li.Text = li.Text & "</div>"

            cell.Controls.Add(li)

            'cell.Width = 1000
            Dim row As New TableRow
            row.Cells.Add(cell)
            row.HorizontalAlign = HorizontalAlign.Center

            row.Height = 450
            mytb.Rows.Add(row)

            p.ClientScript.RegisterStartupScript(Me.GetType, tmpdiv, tmpcstext.ToString, False)

        Next

    End Sub

    Public Sub cstext2Div(ByVal p As Page, ByVal cstext As StringBuilder, ByVal div As String, ByVal mytb As Table)

        Dim cell As New TableCell
        Dim li As New Literal


        li.Text = "<div id =" & Chr(34) & div & Chr(34) & " style =""height :400px ; min-width :310px ; max-width :600px ;margin :0 auto; background-color :lightskyblue""></div>"
        cell.Controls.Add(li)

        'cell.Width = 1000
        Dim row As New TableRow
        row.Cells.Add(cell)
        row.HorizontalAlign = HorizontalAlign.Center

        row.Height = 450
        mytb.Rows.Add(row)

        p.ClientScript.RegisterStartupScript(Me.GetType, div, cstext.ToString, False)


    End Sub

    Public Function ds2ChipDistribution(ByVal ds As DataSet, ByVal div As String, ByVal RecipeNo As String, ByVal IsCF As Boolean) As String

        Dim i, j, k, l As Integer
        Dim cstext As New StringBuilder()
        Dim tb As DataTable = ds.Tables(0)
        Dim rowsNG() As DataRow
        Dim rowsPass() As DataRow
        Dim conn_string, mySql, RecipeName As String
        Dim panelSizeW As Double, panelSizeL As Double, panelOLB As Double, OLBlength As Double, OLBShift As Integer = 50
        Dim pathCCIStandard As String = "\\101.101.101.203\cci\Format\CCIStandardValue.mdb"
        Dim colorNG, colorPass As String

        '
        '┌─────────────┐    ┬
        '│  ↑TFT 外推 OLBShiftOut  │OLBlength
        '├─────────────┤    ┴
        '│                          │
        '│                          │
        '│                          │
        '│                          │
        '└─────────────┘
        '


        Dim conn As OleDbConnection
        Dim adapt As OleDbDataAdapter
        Dim tmpds As New DataSet
        Dim tmptb As DataTable

        colorNG = "'rgba(230,50,5,0.3)'"
        colorPass = "'rgba(246,236,138,0.3)'"

        conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pathCCIStandard & ";"
        conn = New OleDbConnection(conn_string)
        conn.Open()

        mySql = ""
        mySql = mySql & " Select * "
        mySql = mySql & " FROM MapPCRecipe"
        mySql = mySql & " Where RecipeNo in ('" & RecipeNo & "')"

        adapt = New OleDbDataAdapter(mySql, conn)
        adapt.Fill(tmpds)
        adapt.Dispose()
        conn.Close()

        mySql = ""
        mySql = mySql & " Select ([D_Value]-[C_Value]) as OLBlength"
        mySql = mySql & " FROM StandardValue"
        mySql = mySql & " Where Recipe_No_16 in ('" & RecipeNo & "')"
        tmptb = tmpds.Tables(0)
        RecipeName = tmptb.Rows(0).Item("RecipeName")
        panelSizeW = tmptb.Rows(0).Item("panelSizeW")
        panelSizeL = tmptb.Rows(0).Item("panelSizeL")

        tmpds.Reset()
        adapt = New OleDbDataAdapter(mySql, conn)
        adapt.Fill(tmpds)
        adapt.Dispose()
        conn.Close()
        tmptb = tmpds.Tables(0)

        'unit : mm

        OLBlength = tmptb.Rows(0).Item("OLBlength")
        panelOLB = panelSizeL - OLBlength
        'StandardValue
        'D_Value-C_Value
        If IsCF Then
            rowsNG = tb.Select("ISPass = 0 and Edge in ('A','C','E','G')")
            rowsPass = tb.Select("ISPass = 1 and Edge in ('A','C','E','G')")
        Else
            rowsNG = tb.Select("ISPass = 0 and Edge in ('B','D','F','H')")
            rowsPass = tb.Select("ISPass = 1 and Edge in ('B','D','F','H')")
        End If

        cstext.Append("<script>")

        cstext.Append("// div = " & div & " RecipeNo " & RecipeNo & vbNewLine)
        cstext.Append(" var panelSizeX=" & panelSizeW & "; ")
        cstext.Append(" var panelSizeY=" & panelSizeL & "; ")
        cstext.Append(" var panelOLB= " & panelSizeL - OLBShift & "; ")


        cstext.Append(" var ddata =[{ ")
        cstext.Append("     	    name : ""Pass"", ")
        cstext.Append("     	    color : " & colorPass & ", ")
        cstext.Append("             data: [  ")

        For i = 0 To rowsPass.GetUpperBound(0)

            Dim x As Double = rowsPass(i).Item("X") / 1000
            Dim y As Double = rowsPass(i).Item("Y") / 1000

            cstext.Append(" {")
            cstext.Append(" x: " & x & ", ")

            If (rowsPass(i).Item("edge") = "C") And (y < panelOLB + 0.5) Then 'OLB TFT 外推
                cstext.Append(" y: " & y - OLBShift & ", ") 'OLB CF
                cstext.Append(" edge: '" & rowsPass(i).Item("edge") & "_TFT' , ")
            ElseIf (rowsPass(i).Item("edge") = "C") Then
                cstext.Append(" y: " & y & ", ") 'OLB CF
                cstext.Append(" edge: '" & rowsPass(i).Item("edge") & "_CF' , ")
            Else
                cstext.Append(" y: " & y & ", ")
                cstext.Append(" edge: '" & rowsPass(i).Item("edge") & "' , ")
            End If

            cstext.Append(" z: " & rowsPass(i).Item("Depth") & ", ")
            cstext.Append(" PanelID: '" & rowsPass(i).Item("PanelID") & "' , ")
            cstext.Append(" Time: '" & rowsPass(i).Item("Time") & "' , ")
            cstext.Append(" Wide: " & rowsPass(i).Item("Wide") & "  ")
            cstext.Append("  } ")
            If i <> rowsPass.GetUpperBound(0) Then
                cstext.Append(" ,")
            End If
        Next
        cstext.Append("         ], ")
        cstext.Append("         zMin: 0, ")
        cstext.Append("         zMax: 5000 ")


        cstext.Append("     },{ ")


        cstext.Append("             name  : ""DefectNG"", ")
        cstext.Append("     	    color : " & colorNG & ", ")
        cstext.Append("             data: [ ")
        For i = 0 To rowsNG.GetUpperBound(0)

            Dim x As Double = rowsNG(i).Item("X") / 1000
            Dim y As Double = rowsNG(i).Item("Y") / 1000

            cstext.Append(" {")
            cstext.Append(" x: " & rowsNG(i).Item("X") / 1000 & ", ")

            If (rowsNG(i).Item("edge") = "C") And (y < panelOLB + 0.5) Then 'OLB TFT 外推
                cstext.Append(" y: " & y - OLBShift & ", ") 'OLB CF
                cstext.Append(" edge: '" & rowsNG(i).Item("edge") & "_TFT' , ")
            ElseIf (rowsNG(i).Item("edge") = "C") Then
                cstext.Append(" y: " & y & ", ") 'OLB CF
                cstext.Append(" edge: '" & rowsNG(i).Item("edge") & "_CF' , ")
            Else
                cstext.Append(" y: " & y & ", ")
                cstext.Append(" edge: '" & rowsNG(i).Item("edge") & "' , ")
            End If

            cstext.Append(" z: " & rowsNG(i).Item("Depth") & ", ")
            cstext.Append(" PanelID: '" & rowsNG(i).Item("PanelID") & "' , ")
            cstext.Append(" Time: '" & rowsNG(i).Item("Time") & "' , ")
            cstext.Append(" Wide: " & rowsNG(i).Item("Wide") & "  ")
            cstext.Append("  } ")
            If i <> rowsNG.GetUpperBound(0) Then
                cstext.Append(" ,")
            End If
        Next
        cstext.Append("             ")
        cstext.Append("         ], ")
        cstext.Append("         zMin: 0, ")
        cstext.Append("         zMax: 5000         ")

        cstext.Append("     }]; ")


        cstext.Append(" Highcharts.chart('" & div & "', { ")
        cstext.Append("  ")
        cstext.Append("     chart: { ")
        cstext.Append("         type: 'bubble', ")
        cstext.Append("         plotBorderWidth: 1, ")
        cstext.Append("         zoomType: 'xy' ")
        cstext.Append("     }, ")
        cstext.Append("  ")
        cstext.Append("     legend: { ")
        cstext.Append("         enabled: true ")
        cstext.Append("     }, ")
        cstext.Append("  ")
        cstext.Append("     title: { ")
        cstext.Append("         text: 'CCT#" & Replace(div, "Div", "") & "_" & RecipeName & "' ")
        cstext.Append("     }, ")
        cstext.Append("  ")
        cstext.Append("     subtitle: { ")
        cstext.Append("         text: 'defect distribution' ")
        cstext.Append("     }, ")
        cstext.Append("  ")
        cstext.Append("     xAxis: { ")
        cstext.Append("         gridLineWidth: 1, ")
        cstext.Append("         min : 0 - 50 , ")
        cstext.Append("         max : panelSizeX+50 , ")
        cstext.Append("         title: { ")
        cstext.Append("             text: 'X' ")
        cstext.Append("         }, ")
        cstext.Append("         labels: { ")
        cstext.Append("             format: '{value} mm' ")
        cstext.Append("         }, ")
        cstext.Append("         plotLines: [{ ")
        cstext.Append("             color: 'black', ")
        cstext.Append("             dashStyle: 'dot', ")
        cstext.Append("             width: 1, ")
        cstext.Append("             value: 0, ")
        cstext.Append("             label: { ")
        cstext.Append("                 rotation: 0, ")
        cstext.Append("                 y: 5, ")
        cstext.Append("                 style: { ")
        cstext.Append("                     fontStyle: 'italic' ")
        cstext.Append("                 }, ")
        cstext.Append("                 text: '' ")
        cstext.Append("             }, ")
        cstext.Append("             zIndex: 10 ")
        cstext.Append("         },{ ")
        cstext.Append("             color: 'black', ")
        cstext.Append("             dashStyle: 'dot', ")
        cstext.Append("             width: 1, ")
        cstext.Append("             value: panelSizeX, ")
        cstext.Append("             zIndex: 10 ")
        cstext.Append("         }] ")
        cstext.Append("     }, ")
        cstext.Append("  ")
        cstext.Append("     yAxis: { ")
        cstext.Append("         startOnTick: false, ")
        cstext.Append("         endOnTick: false, ")
        cstext.Append("    		min:-50, ")
        cstext.Append("         max : panelSizeY +  50 , ")
        cstext.Append("         title: { ")
        cstext.Append("             text: 'Y' ")
        cstext.Append("         }, ")
        cstext.Append("         labels: { ")
        cstext.Append("             format: '{value} mm' ")
        cstext.Append("         }, ")
        cstext.Append("         maxPadding: 0.2, ")
        '------------------------------------------------
        cstext.Append("         plotLines: [{ ")
        cstext.Append("             color: 'black', ")
        cstext.Append("             dashStyle: 'dot', ")
        cstext.Append("             width: 1, ")
        cstext.Append("             value: 0, ")
        cstext.Append("             label: { ")
        cstext.Append("                 align: 'right', ")
        cstext.Append("                 style: { ")
        cstext.Append("                     fontStyle: 'italic' ")
        cstext.Append("                 }, ")
        cstext.Append("                 text: '', ")
        cstext.Append("                 x: 5 ")
        cstext.Append("             }, ")
        cstext.Append("             zIndex: 10 ")
        cstext.Append("         },{ ")
        cstext.Append("             color: 'black', ")
        cstext.Append("             dashStyle: 'dot', ")
        cstext.Append("             width: 1, ")
        cstext.Append("             value: panelSizeY , ")
        cstext.Append("             zIndex: 10 ")
        cstext.Append("         },{ ")
        cstext.Append("             color: 'black', ")
        cstext.Append("             dashStyle: 'dot', ")
        cstext.Append("             width: 1, ")
        cstext.Append("             value: panelOLB, ")
        cstext.Append("             zIndex: 10 ")
        cstext.Append("         }] ")
        '--------------------------------------------------------------------
        cstext.Append("     }, ")

        cstext.Append("     tooltip: { ")
        cstext.Append("         useHTML: true, ")
        cstext.Append("         headerFormat: '<table>', ")
        cstext.Append("         pointFormat:   '<tr><th>X:</th><td>{point.x}mm</td></tr>' + ")
        cstext.Append("                        '<tr><th>Y:</th><td>{point.y}mm</td></tr>' + ")
        cstext.Append("             '<tr><th>Depth:</th><td>{point.z}mm</td></tr>' + ")
        cstext.Append("             '<tr><th>Wide:</th><td>{point.Wide}mm</td></tr>' + ")
        cstext.Append("             '<tr><th>Edge:</th><td>{point.edge}</td></tr>' + ")
        cstext.Append("             '<tr><th>PanelID:</th><td>{point.PanelID}</td></tr>' + ")
        cstext.Append("             '<tr><th>Time:</th><td>{point.Time}</td></tr>', ")
        cstext.Append("         footerFormat: '</table>', ")
        cstext.Append("         followPointer: true ")
        cstext.Append("     }, ")


        cstext.Append("     series: ddata, ")

        cstext.Append("     plotOptions: { ")
        cstext.Append("         series: { ")
        cstext.Append("             dataLabels: { ")
        cstext.Append("                 enabled: false, ")
        cstext.Append("                 format: '{point.PanelID} ' ")
        cstext.Append("             } ")
        cstext.Append("         }, ")
        cstext.Append("         bubble: {       ")
        cstext.Append("             minSize:2, ")
        cstext.Append("             maxSize: 20 ")
        cstext.Append("         } ")
        cstext.Append("     } ")
        cstext.Append(" }); ")

        cstext.Append(" ")
        cstext.Append(" ")
        cstext.Append(" ")
        cstext.Append(" ")
        cstext.Append(" ")
        cstext.Append(" ")
        cstext.Append(" ")


        cstext.Append("</script>")


        Return cstext.ToString
    End Function
    Public Function ds2SheetDistribution(ByVal ds As DataSet, ByVal div As String, ByVal RecipeNo As String, ByVal IsCF As Boolean) As String

        Dim i As Integer
        Dim cstext As New StringBuilder()
        Dim tb As DataTable = ds.Tables(0)
        Dim rowsNG() As DataRow
        Dim rowsPass() As DataRow
        Dim conn_string, mySql, RecipeName As String
        Dim panelSizeW As Double, panelSizeL As Double, panelOLB As Double, OLBlength As Double, OLBShift As Integer = 50
        Dim panelX As Integer, panelY As Integer
        Dim pathCCIStandard As String = "\\101.101.101.203\cci\Format\CCIStandardValue.mdb"
        Dim colorNG, colorPass As String

        '
        '┌─────────────┐    ┬
        '│  ↑TFT 外推 OLBShiftOut  │OLBlength
        '├─────────────┤    ┴
        '│                          │
        '│                          │
        '│                          │
        '│                          │
        '└─────────────┘
        '


        Dim conn As OleDbConnection
        Dim adapt As OleDbDataAdapter
        Dim tmpds As New DataSet
        Dim tmptb As DataTable

        colorNG = "'rgba(230,50,5,0.3)'"
        colorPass = "'rgba(246,236,138,0.3)'"

        conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pathCCIStandard & ";"
        conn = New OleDbConnection(conn_string)
        conn.Open()

        mySql = ""
        mySql = mySql & " Select * "
        mySql = mySql & " FROM MapPCRecipe"
        mySql = mySql & " Where RecipeNo in ('" & RecipeNo & "')"

        adapt = New OleDbDataAdapter(mySql, conn)
        adapt.Fill(tmpds)
        adapt.Dispose()
        conn.Close()

        tmptb = tmpds.Tables(0)
        RecipeName = tmptb.Rows(0).Item("RecipeName")
        panelSizeW = tmptb.Rows(0).Item("panelSizeW")
        panelSizeL = tmptb.Rows(0).Item("panelSizeL")
        panelX = tmptb.Rows(0).Item("panelX") ' 39" : 3
        panelY = tmptb.Rows(0).Item("panelY") ' 39" : 2



        mySql = ""
        mySql = mySql & " Select ([D_Value]-[C_Value]) as OLBlength"
        mySql = mySql & " FROM StandardValue"
        mySql = mySql & " Where Recipe_No_16 in ('" & RecipeNo & "')"

        tmpds.Reset()
        adapt = New OleDbDataAdapter(mySql, conn)
        adapt.Fill(tmpds)
        adapt.Dispose()
        conn.Close()
        tmptb = tmpds.Tables(0)

        'unit : mm

        OLBlength = tmptb.Rows(0).Item("OLBlength")
        panelOLB = panelSizeL - OLBlength
        'StandardValue
        'D_Value-C_Value
        If IsCF Then
            rowsNG = tb.Select("ISPass = 0 and Edge in ('A','C','E','G')")
            rowsPass = tb.Select("ISPass = 1 and Edge in ('A','C','E','G')")
        Else
            rowsNG = tb.Select("ISPass = 0 and Edge in ('B','D','F','H')")
            rowsPass = tb.Select("ISPass = 1 and Edge in ('B','D','F','H')")
        End If

        cstext.Append("<script>")

        cstext.Append("// div = " & div & " RecipeNo " & RecipeNo & vbNewLine)
        cstext.Append(" var panelSizeX=" & panelSizeW & "; ")
        cstext.Append(" var panelSizeY=" & panelSizeL & "; ")
        cstext.Append(" var panelOLB= " & panelSizeL - OLBShift & "; ")


        cstext.Append(" var ddata =[{ ")
        cstext.Append("     	    name : ""Pass"", ")
        cstext.Append("     	    color : " & colorPass & ", ")
        cstext.Append("             data: [  ")

        For i = 0 To rowsPass.GetUpperBound(0)

            Dim x As Double = rowsPass(i).Item("X") / 1000
            Dim y As Double = rowsPass(i).Item("Y") / 1000

            cstext.Append(" {")
            cstext.Append(" x: " & x & ", ")

            If (rowsPass(i).Item("edge") = "C") And (y < panelOLB + 0.5) Then 'OLB TFT 外推
                cstext.Append(" y: " & y - OLBShift & ", ") 'OLB CF
                cstext.Append(" edge: '" & rowsPass(i).Item("edge") & "_TFT' , ")
            ElseIf (rowsPass(i).Item("edge") = "C") Then
                cstext.Append(" y: " & y & ", ") 'OLB CF
                cstext.Append(" edge: '" & rowsPass(i).Item("edge") & "_CF' , ")
            Else
                cstext.Append(" y: " & y & ", ")
                cstext.Append(" edge: '" & rowsPass(i).Item("edge") & "' , ")
            End If

            cstext.Append(" z: " & rowsPass(i).Item("Depth") & ", ")
            cstext.Append(" PanelID: '" & rowsPass(i).Item("PanelID") & "' , ")
            cstext.Append(" Time: '" & rowsPass(i).Item("Time") & "' , ")
            cstext.Append(" Wide: " & rowsPass(i).Item("Wide") & "  ")
            cstext.Append("  } ")
            If i <> rowsPass.GetUpperBound(0) Then
                cstext.Append(" ,")
            End If
        Next
        cstext.Append("         ], ")
        cstext.Append("         zMin: 0, ")
        cstext.Append("         zMax: 5000 ")


        cstext.Append("     },{ ")


        cstext.Append("             name  : ""DefectNG"", ")
        cstext.Append("     	    color : " & colorNG & ", ")
        cstext.Append("             data: [ ")
        For i = 0 To rowsNG.GetUpperBound(0)

            Dim x As Double = rowsNG(i).Item("X") / 1000
            Dim y As Double = rowsNG(i).Item("Y") / 1000

            cstext.Append(" {")
            cstext.Append(" x: " & rowsNG(i).Item("X") / 1000 & ", ")

            If (rowsNG(i).Item("edge") = "C") And (y < panelOLB + 0.5) Then 'OLB TFT 外推
                cstext.Append(" y: " & y - OLBShift & ", ") 'OLB CF
                cstext.Append(" edge: '" & rowsNG(i).Item("edge") & "_TFT' , ")
            ElseIf (rowsNG(i).Item("edge") = "C") Then
                cstext.Append(" y: " & y & ", ") 'OLB CF
                cstext.Append(" edge: '" & rowsNG(i).Item("edge") & "_CF' , ")
            Else
                cstext.Append(" y: " & y & ", ")
                cstext.Append(" edge: '" & rowsNG(i).Item("edge") & "' , ")
            End If

            cstext.Append(" z: " & rowsNG(i).Item("Depth") & ", ")
            cstext.Append(" PanelID: '" & rowsNG(i).Item("PanelID") & "' , ")
            cstext.Append(" Time: '" & rowsNG(i).Item("Time") & "' , ")
            cstext.Append(" Wide: " & rowsNG(i).Item("Wide") & "  ")
            cstext.Append("  } ")
            If i <> rowsNG.GetUpperBound(0) Then
                cstext.Append(" ,")
            End If
        Next
        cstext.Append("             ")
        cstext.Append("         ], ")
        cstext.Append("         zMin: 0, ")
        cstext.Append("         zMax: 5000         ")

        cstext.Append("     }]; ")


        cstext.Append(" Highcharts.chart('" & div & "', { ")
        cstext.Append("  ")
        cstext.Append("     chart: { ")
        cstext.Append("         type: 'bubble', ")
        cstext.Append("         plotBorderWidth: 1, ")
        cstext.Append("         zoomType: 'xy' ")
        cstext.Append("     }, ")
        cstext.Append("  ")
        cstext.Append("     legend: { ")
        cstext.Append("         enabled: true ")
        cstext.Append("     }, ")
        cstext.Append("  ")
        cstext.Append("     title: { ")
        cstext.Append("         text: 'CCT#" & Replace(div, "Div", "") & "_" & RecipeName & "' ")
        cstext.Append("     }, ")
        cstext.Append("  ")
        cstext.Append("     subtitle: { ")
        cstext.Append("         text: 'defect distribution' ")
        cstext.Append("     }, ")
        cstext.Append("  ")
        cstext.Append("     xAxis: { ")
        cstext.Append("         gridLineWidth: 1, ")
        cstext.Append("         min : 0 - 50 , ")
        cstext.Append("         max : panelSizeX+50 , ")
        cstext.Append("         title: { ")
        cstext.Append("             text: 'X' ")
        cstext.Append("         }, ")
        cstext.Append("         labels: { ")
        cstext.Append("             format: '{value} mm' ")
        cstext.Append("         }, ")
        cstext.Append("         plotLines: [{ ")
        cstext.Append("             color: 'black', ")
        cstext.Append("             dashStyle: 'dot', ")
        cstext.Append("             width: 1, ")
        cstext.Append("             value: 0, ")
        cstext.Append("             label: { ")
        cstext.Append("                 rotation: 0, ")
        cstext.Append("                 y: 5, ")
        cstext.Append("                 style: { ")
        cstext.Append("                     fontStyle: 'italic' ")
        cstext.Append("                 }, ")
        cstext.Append("                 text: '' ")
        cstext.Append("             }, ")
        cstext.Append("             zIndex: 10 ")
        cstext.Append("         },{ ")
        cstext.Append("             color: 'black', ")
        cstext.Append("             dashStyle: 'dot', ")
        cstext.Append("             width: 1, ")
        cstext.Append("             value: panelSizeX, ")
        cstext.Append("             zIndex: 10 ")
        cstext.Append("         }] ")
        cstext.Append("     }, ")
        cstext.Append("  ")
        cstext.Append("     yAxis: { ")
        cstext.Append("         startOnTick: false, ")
        cstext.Append("         endOnTick: false, ")
        cstext.Append("    		min:-50, ")
        cstext.Append("         max : panelSizeY +  50 , ")
        cstext.Append("         title: { ")
        cstext.Append("             text: 'Y' ")
        cstext.Append("         }, ")
        cstext.Append("         labels: { ")
        cstext.Append("             format: '{value} mm' ")
        cstext.Append("         }, ")
        cstext.Append("         maxPadding: 0.2, ")
        '------------------------------------------------
        cstext.Append("         plotLines: [{ ")
        cstext.Append("             color: 'black', ")
        cstext.Append("             dashStyle: 'dot', ")
        cstext.Append("             width: 1, ")
        cstext.Append("             value: 0, ")
        cstext.Append("             label: { ")
        cstext.Append("                 align: 'right', ")
        cstext.Append("                 style: { ")
        cstext.Append("                     fontStyle: 'italic' ")
        cstext.Append("                 }, ")
        cstext.Append("                 text: '', ")
        cstext.Append("                 x: 5 ")
        cstext.Append("             }, ")
        cstext.Append("             zIndex: 10 ")
        cstext.Append("         },{ ")
        cstext.Append("             color: 'black', ")
        cstext.Append("             dashStyle: 'dot', ")
        cstext.Append("             width: 1, ")
        cstext.Append("             value: panelSizeY , ")
        cstext.Append("             zIndex: 10 ")
        cstext.Append("         },{ ")
        cstext.Append("             color: 'black', ")
        cstext.Append("             dashStyle: 'dot', ")
        cstext.Append("             width: 1, ")
        cstext.Append("             value: panelOLB, ")
        cstext.Append("             zIndex: 10 ")
        cstext.Append("         }] ")
        '--------------------------------------------------------------------
        cstext.Append("     }, ")

        cstext.Append("     tooltip: { ")
        cstext.Append("         useHTML: true, ")
        cstext.Append("         headerFormat: '<table>', ")
        cstext.Append("         pointFormat:   '<tr><th>X:</th><td>{point.x}mm</td></tr>' + ")
        cstext.Append("                        '<tr><th>Y:</th><td>{point.y}mm</td></tr>' + ")
        cstext.Append("             '<tr><th>Depth:</th><td>{point.z}mm</td></tr>' + ")
        cstext.Append("             '<tr><th>Wide:</th><td>{point.Wide}mm</td></tr>' + ")
        cstext.Append("             '<tr><th>Edge:</th><td>{point.edge}</td></tr>' + ")
        cstext.Append("             '<tr><th>PanelID:</th><td>{point.PanelID}</td></tr>' + ")
        cstext.Append("             '<tr><th>Time:</th><td>{point.Time}</td></tr>', ")
        cstext.Append("         footerFormat: '</table>', ")
        cstext.Append("         followPointer: true ")
        cstext.Append("     }, ")


        cstext.Append("     series: ddata, ")

        cstext.Append("     plotOptions: { ")
        cstext.Append("         series: { ")
        cstext.Append("             dataLabels: { ")
        cstext.Append("                 enabled: false, ")
        cstext.Append("                 format: '{point.PanelID} ' ")
        cstext.Append("             } ")
        cstext.Append("         }, ")
        cstext.Append("         bubble: {       ")
        cstext.Append("             minSize:2, ")
        cstext.Append("             maxSize: 20 ")
        cstext.Append("         } ")
        cstext.Append("     } ")
        cstext.Append(" }); ")

        cstext.Append(" ")
        cstext.Append(" ")
        cstext.Append(" ")
        cstext.Append(" ")
        cstext.Append(" ")
        cstext.Append(" ")
        cstext.Append(" ")


        cstext.Append("</script>")


        Return cstext.ToString
    End Function

    Function RecipeNo2RecipeName(ByVal RecipeNo As String) As String


        Dim conn As OleDbConnection
        Dim ds As DataSet
        Dim mySql As String
        Dim cla As New Class_Sql
        Dim tmppath As String

        tmppath = Path_ServerData & "RecipeTable.mdb"
        tmppath = "\\101.101.101.203\cct000\update\Tmp\RecipeTable.mdb"


        mySql = ""
        mySql = mySql & " select * from RecipeTable"
        mySql = mySql & " where CCIRecipeNo ='" & RecipeNo & "'"
        mySql = mySql & ""
        mySql = mySql & ""

        conn = cla.CreateConn(tmppath)
        ds = cla.GetDB(conn, mySql)
        Try
            Return ds.Tables(0).Rows(0).Item("CCIRecipeName")
        Catch ex As Exception
            Return ""
        End Try

    End Function
End Class
