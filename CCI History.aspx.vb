Imports System.Data.OleDb
Imports System.Data
Imports System.IO
Imports System.Math
Imports System.ComponentModel
Imports System.Drawing

Imports Class1
Partial Class CCI_History
    Inherits System.Web.UI.Page

    Dim myCla As New Class1

    Public path_ServerData As String = "E:\WebSite_new_WithCanvas\ServerData\"
    Public Eqp_matrix() As String = {"1", "2", "3", "4", "5" _
                                   , "6", "7", "8", "9", "A" _
                                   , "B", "C", "D", "E", "F"}

    Protected Sub Button_Submit_Click(sender As Object, e As EventArgs) Handles Button_DefectT1.Click, Button_DefectT2.Click, Button_Distribution.Click

        Dim i, j As Integer
        Dim eqp_list As String = ""

        Dim eqp_shift As Integer
        Dim day_shift As Integer
        Dim Sql_PanelID As String
        Dim PanelID() As String
        Dim nn As Date = Now
        Dim S_Date, E_Date As Date
        Dim diff_date As Integer
        Dim cstext As New StringBuilder
        Dim conn As OleDbConnection
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim tb As DataTable
        Dim conn_string As String = ""
        Dim mySql As String = ""
        Dim myPath As String
        Try

            GridView1.Columns.Clear()

            E_Date = New Date(CInt(Mid(datepicker_E.Text, 7, 4)), CInt(Mid(datepicker_E.Text, 1, 2)), CInt(Mid(datepicker_E.Text, 4, 2)), 23, 59, 59)
            S_Date = New Date(CInt(Mid(datepicker_S.Text, 7, 4)), CInt(Mid(datepicker_S.Text, 1, 2)), CInt(Mid(datepicker_S.Text, 4, 2)), 0, 0, 0)

            diff_date = Math.Abs(DateDiff(DateInterval.Day, E_Date, S_Date))

            For i = 0 To CheckBoxList1.Items.Count - 1
                If CheckBoxList1.Items(i).Selected Then
                    eqp_list = eqp_list & Eqp_matrix(CheckBoxList1.Items(i).Value - 1)
                End If
            Next

            j = 0

            Dim regxNewLine As Regex
            regxNewLine = New Regex("\n", RegexOptions.Multiline)
            Dim objMatchCollection As MatchCollection = regxNewLine.Matches(TextBox1.Text)

            ReDim PanelID(objMatchCollection.Count)

            For i = 1 To TextBox1.Text.Length
                If Not (Mid(TextBox1.Text, i, 1) = vbCr Or Mid(TextBox1.Text, i, 1) = vbLf) Then
                    PanelID(j) = PanelID(j) & Mid(TextBox1.Text, i, 1)
                ElseIf Mid(TextBox1.Text, i, 1) = vbCr And PanelID(j) <> "" Then
                    j = j + 1
                End If
            Next
            Sql_PanelID = "("

            i = 0

            For i = 0 To PanelID.Length - 1
                Sql_PanelID = Sql_PanelID & "'"
                Sql_PanelID = Sql_PanelID & PanelID(i)
                Sql_PanelID = Sql_PanelID & "'"

                If i + 1 <= PanelID.Length - 1 Then
                    Sql_PanelID = Sql_PanelID & ","
                End If
            Next
            Sql_PanelID = Sql_PanelID & ")"

            For eqp_shift = 0 To eqp_list.Length - 1
                Dim tmpeqp As String = (Mid(eqp_list, eqp_shift + 1, 1))

                For day_shift = 0 To diff_date
                    Dim date_tmp As Date = DateAdd(DateInterval.Day, day_shift, S_Date)
                    myPath = path_ServerData & "CCT" & (Mid(eqp_list, eqp_shift + 1, 1)) & "00\" & Format(date_tmp, "yyyyMM") & "\" & Format(date_tmp, "dd") & "\" & Format(date_tmp, "yyyyMMdd") & ".mdb"

                    If File.Exists(myPath) Then

                        Dim min As Integer = Convert.ToInt32(TextBox_min.Text)
                        Dim MAX As Integer = Convert.ToInt32(TextBox_MAX.Text)

                        Select Case sender.tabindex
                            Case 1

                                mySql = ""
                                mySql = mySql & " Select * ,'CCT" & (Mid(eqp_list, eqp_shift + 1, 1)) & "00' as eqp "
                                mySql = mySql & " from DefectT1"
                                mySql = mySql & IIf(CheckBox1.Checked, "", " WHERE PanelID in " & Sql_PanelID)
                            Case 2, 3

                                Dim sqledge As String = ""

                                For i = 0 To CheckBoxList2.Items.Count - 1
                                    sqledge = sqledge & "'"
                                    If CheckBoxList2.Items(i).Selected Then
                                        sqledge = sqledge & Chr(65 + i) 'CheckBoxList2.Items(i).Value
                                    End If
                                    sqledge = sqledge & "'" & IIf(i <> CheckBoxList2.Items.Count - 1, ",", "")
                                Next

                                mySql = ""
                                mySql = mySql & " Select a.*,b.RecipeNo,b.RecipeName From "
                                mySql = mySql & " ("

                                mySql = mySql & " ("
                                mySql = mySql & " Select [eqp],[edge],[PanelID], [Item], "
                                mySql = mySql & "        [position] , MID([position],5,8)as X , MID([position],15,8)as Y , "
                                mySql = mySql & "        [Depth], [Wide], [Image], [Time], iif([Item_pass],1,0) as ISpass from ("

                                mySql = mySql & "            Select * ,'CCT" & (Mid(eqp_list, eqp_shift + 1, 1)) & "00' as eqp , "
                                mySql = mySql & "            Switch("
                                mySql = mySql & "            mid([position] ,1,2) = '00' , '" & myCla.CCIEDGE2EDGECHAR(0) & "' , "
                                mySql = mySql & "            mid([position] ,1,2) = '01' , '" & myCla.CCIEDGE2EDGECHAR(1) & "' , "
                                mySql = mySql & "            mid([position] ,1,2) = '02' , '" & myCla.CCIEDGE2EDGECHAR(2) & "' , "
                                mySql = mySql & "            mid([position] ,1,2) = '03' , '" & myCla.CCIEDGE2EDGECHAR(3) & "' , "
                                mySql = mySql & "            mid([position] ,1,2) = '04' , '" & myCla.CCIEDGE2EDGECHAR(4) & "' , "
                                mySql = mySql & "            mid([position] ,1,2) = '05' , '" & myCla.CCIEDGE2EDGECHAR(5) & "' , "
                                mySql = mySql & "            mid([position] ,1,2) = '06' , '" & myCla.CCIEDGE2EDGECHAR(6) & "' , "
                                mySql = mySql & "            mid([position] ,1,2) = '07' , '" & myCla.CCIEDGE2EDGECHAR(7) & "'   "
                                mySql = mySql & "            ) as edge "
                                mySql = mySql & "    from DefectT2"
                                mySql = mySql & "    Where val(Depth) >" & min & " AND val(Depth) < " & MAX & ""

                                If TextBox_RecipeNo.Text <> "" Then
                                    mySql = mySql & "    And PanelID  not in ( select PanelID from DefectT1 where RecipeNo not in " & Sql_PanelID & "   )"
                                End If

                                mySql = mySql & "    And PanelID not in"
                                mySql = mySql & "    ("

                                If CheckBox_exculde.Checked = True Then
                                    mySql = mySql & "        Select PanelID From DefectT2 "
                                    mySql = mySql & "        Where val(Depth) < " & min & " Or val(Depth) > " & MAX
                                Else
                                    mySql = mySql & "        ''"
                                End If

                                mySql = mySql & "    )"
                                mySql = mySql & IIf(CheckBox1.Checked, "", " AND PanelID in " & Sql_PanelID)
                                mySql = mySql & " )"
                                mySql = mySql & " where edge in(" & sqledge & ")"
                                mySql = mySql & " ) as a"

                                mySql = mySql & " left join"


                                mySql = mySql & " (Select [PanelID],[RecipeNo],[RecipeName] FROM DefectT1  ) as b"


                                mySql = mySql & " on a.panelID = b.panelID"

                                mySql = mySql & ") "
                        End Select

                        Try
                            conn_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & myPath & ";"
                            conn = New OleDbConnection(conn_string)
                            conn.Open()
                            adapter = New OleDbDataAdapter(mySql, conn)
                            adapter.Fill(ds)
                            adapter.Dispose()
                            conn.Close()
                        Catch ex As Exception
                            TextBox1.Text = TextBox1.Text & ex.Message & vbNewLine & mySql
                        End Try
                    End If
                Next ' shiftDate
                If sender.tabindex = 3 Then

                    myCla.distribution(Page, ds, Table1, tmpeqp)
                    ds.Reset()
                End If
            Next ' shitEqp

            myCla.ds2gridview(ds, GridView1)
        Catch ex As Exception
            TextBox1.Text = TextBox1.Text & ex.Message & vbNewLine & mySql
        End Try
    End Sub



End Class
