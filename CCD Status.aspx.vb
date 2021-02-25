
Imports System.Data.OleDb
Imports System.Data

Partial Class CCD_Monitor
    Inherits System.Web.UI.Page
    Public path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData"
    Private Sub CCD_Monitor_Load(sender As Object, e As EventArgs) Handles Me.Load ', Button1.Click
        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable

        Dim nn As Date = Now

        Dim conn_string, sql As String
        Dim i, j As Integer
        Dim path As String

        Dim myrow As TableRow
        Dim mycell As TableCell
        '    Dim S_Time, E_TIme As String

        Table1.Width = 400
        'Table1.Caption = "動態生成表格"
        'Table1.GridLines = GridLines.Horizontal  '//設置儲存格的框線
        '  Table1.HorizontalAlign = HorizontalAlign.Left   '//設置表格相對頁面居中
        Table1.CellPadding = 5 ' //設置儲存格內間距
        Table1.CellSpacing = 5 ' //設置儲存格之間的距離
        Table1.CaptionAlign = TableCaptionAlign.Bottom
        Table1.Visible = True  '


        path = path_ServerData & "CCDStatus.xls"
        conn_string = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" & path & "'; Extended Properties = ""Excel 8.0; HDR=YES; IMEX=1; """

        sql = ""
        sql = sql & " select *"
        sql = sql & " from  [CCDStatus$A:E]"

        Try

            conn = New OleDbConnection(conn_string)
            conn.Open()
            adapter = New OleDbDataAdapter(sql, conn)
            adapter.Fill(ds)
            adapter.Dispose()

            tb = ds.Tables(0)

            myrow = New TableRow
            For i = 0 To tb.Columns.Count - 1 'S_tmp.Length - 1

                mycell = New TableCell
                With mycell
                    .Wrap = False
                    .Text = tb.Columns.Item(i).ColumnName   'S_tmp(i) 
                    .HorizontalAlign = HorizontalAlign.Center
                    .Font.Bold = True
                End With
                myrow.Cells.Add(mycell)
            Next

            Table1.Rows.Add(myrow)

            For i = 0 To tb.Rows.Count - 1 - 1

                myrow = New TableRow

                For j = 0 To tb.Columns.Count - 1
                    If tb.Rows(i).Item(j).ToString <> DBNull.Value.ToString Then

                        mycell = New TableCell

                        With mycell
                            .Text = tb.Rows(i).Item(j)
                            .Wrap = False
                            .HorizontalAlign = HorizontalAlign.Center
                            .Font.Bold = True

                            If .Text = "Off" Then
                                '.BackColor = Drawing.Color.FromArgb(0.5, Drawing.Color.OrangeRed)
                                .ForeColor = Drawing.Color.FromArgb(1, 255, 64, 84)
                                '.BackColor = Drawing.Color.FromArgb(0.2, 255, 64, 84)
                            ElseIf .Text = "On" Then
                                '.BackColor = Drawing.Color.FromArgb(0.5, Drawing.Color.YellowGreen)
                                .ForeColor = Drawing.Color.FromArgb(1, 91, 161, 63)
                                '.BackColor = Drawing.Color.FromArgb(1, 91, 161, 63)
                                '.ForeColor = Drawing.Color.YellowGreen
                            End If

                        End With
                        myrow.Cells.Add(mycell)
                    End If
                Next
                Table1.Rows.Add(myrow)
            Next
        Catch ex As Exception

        Finally
            conn.Close()
        End Try

        Label1.Text = "CCI 下CCD 開啟狀態"
        '        If tb .
        ' Label2.Text = tb.Rows(i).Item(0)
    End Sub

End Class
