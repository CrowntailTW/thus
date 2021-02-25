Imports System.Collections.Generic
Imports System.IO
Imports System.Net

Partial Class FullImage_new
    Inherits System.Web.UI.Page
    Protected Sub Button1_Click(sender As Button, e As EventArgs) Handles Button1.Click, Button_Download.Click

        Dim i, j, k As Integer
        Dim ss() As String
        Dim ids As New List(Of String)

        Dim CB() As CheckBox = {CB0, CB1, CB2, CB3, CB4, CB5, CB6, CB7}
        Dim CB_count As Integer
        Dim S_date, E_date As Date
        Dim Date_Shift, Date_Diff As Integer
        Dim path_sou, path_des, path_desB As String
        Dim eqp_list As String
        Dim Eqp_matrix() As String = {"1", "2", "3", "4", "5" _
                                   , "6", "7", "8", "9", "A" _
                                   , "B", "C", "D", "E", "F"}
        Dim cla As New Class1
        E_date = DateAdd(DateInterval.Day, 1, DateSerial(CInt(Mid(datepicker_E.Text, 7, 4)), CInt(Mid(datepicker_E.Text, 1, 2)), CInt(Mid(datepicker_E.Text, 4, 2))))
        S_date = DateSerial(CInt(Mid(datepicker_S.Text, 7, 4)), CInt(Mid(datepicker_S.Text, 1, 2)), CInt(Mid(datepicker_S.Text, 4, 2)))
        Date_Diff = DateDiff(DateInterval.Day, S_date, E_date)

        eqp_list = ""
        For i = 0 To CheckBoxList1.Items.Count - 1
            If CheckBoxList1.Items(i).Selected Then
                eqp_list = eqp_list & Eqp_matrix(CheckBoxList1.Items(i).Value - 1)
            End If
        Next

        For i = 0 To CB.Length - 1
            If CB(i).Checked Then
                CB_count = CB_count + 1
            End If
        Next

        If CB_count = 0 Or datepicker_E.Text = "" Or datepicker_S.Text = "" Then
            Exit Sub
        End If

        Label1.Text = ""

        ss = TextBox1.Text.Split(vbNewLine)
        For Each s In ss
            If s <> "" And Not ids.Contains(Trim(s)) Then ids.Add(Trim(s).Replace(vbLf, ""))
        Next

        Panel1.Controls.Clear()

        Dim isFound As Boolean
        isFound = False
        Try
            i = 0

            For Date_Shift = 0 To Date_Diff '時間內
                Dim date_tmp As Date = DateAdd(DateInterval.Day, Date_Shift, S_date)

                For eqp_shift = 0 To eqp_list.Length - 1
                    Dim tmpeqp As String = (Mid(eqp_list, eqp_shift + 1, 1))

                    For Each id As String In ids.ToArray '每一片ID
                        isFound = False
                        For j = 0 To CB.Length - 1 '每一個邊
                            If CB(j).Checked Then

                                path_sou = EqpNum2Path全圖備份(cla.eqp2num(tmpeqp)) & Format(date_tmp, "yyyyMMdd") & "\" & id & j & ".jpg"

                                path_des = "E:\WebSite_new_WithCanvas\images\A\" & id & CB(j).TabIndex & ".jpg"
                                'path_des = "D:\WebSite_new_WithCanvas\images\A\" & id & CB(j).TabIndex & ".jpg"

                                path_desB = "~\images\A\" & id & CB(j).TabIndex & ".jpg"

                                If File.Exists(path_sou) Then

                                    File.Copy(path_sou, path_des, True)

                                    If sender.TabIndex = 0 Then ' 顯示image
                                        Dim tmpimg As Image = New Image
                                        With tmpimg
                                            .ImageUrl = path_desB
                                            .Width = 500
                                            .ID = "img_" & id & "_" & CB(j).TabIndex
                                        End With

                                        Panel1.Controls.Add(tmpimg)
                                        i = i + 1
                                    ElseIf sender.TabIndex = 1 Then 'download
                                        Response.Write("download.aspx ? filename=" & id & j & ".jpg")
                                        Response.Write("<script>window.open ('" & "download.aspx?filename=" & id & j & ".jpg" & "');</script>")
                                    End If
                                    isFound = True

                                End If 'file.exist

                            End If 'CB(j).Checked
                        Next 'CB(j).Checked
                        If isFound Then ids.Remove(id)

                    Next ' ids
                Next 'eqp
            Next 'date

            If ids.Count <> 0 Then
                For Each id As String In ids
                    Label1.Text = Label1.Text & id & " "
                Next
                Label1.Text = Label1.Text & "not found"
            End If

        Catch ex As Exception
            Label1.Text = ex.Message & vbTab & i & vbTab & j & vbTab & k
        End Try

    End Sub
    Protected Sub Button1_ClickXXX(sender As Button, e As EventArgs)

        Dim i, j, k, l As Integer

        Dim id(59)() As String '最多30
        Dim img()() As Image
        Dim CB() As CheckBox = {CB0, CB1, CB2, CB3, CB4, CB5, CB6, CB7}
        Dim CB_count As Integer
        Dim count, count_tmp As Integer
        Dim S_date, E_date As Date
        Dim Date_Shift, Date_Diff As Integer
        Dim path_sou, path_des, path_desB As String

        E_date = DateAdd(DateInterval.Day, 1, DateSerial(CInt(Mid(datepicker_E.Text, 7, 4)), CInt(Mid(datepicker_E.Text, 1, 2)), CInt(Mid(datepicker_E.Text, 4, 2))))
        S_date = DateSerial(CInt(Mid(datepicker_S.Text, 7, 4)), CInt(Mid(datepicker_S.Text, 1, 2)), CInt(Mid(datepicker_S.Text, 4, 2)))
        Date_Diff = DateDiff(DateInterval.Day, S_date, E_date)

        For i = 0 To CB.Length - 1
            If CB(i).Checked Then
                CB_count = CB_count + 1
            End If
        Next

        If CB_count = 0 Or datepicker_E.Text = "" Or datepicker_S.Text = "" Then
            Exit Sub
        End If

        For i = 0 To id.Length - 1
            ReDim id(i)(1)
        Next
        j = 0
        For i = 1 To TextBox1.Text.Length
            If Not (Mid(TextBox1.Text, i, 1) = vbCr Or Mid(TextBox1.Text, i, 1) = vbLf) Then
                id(j)(0) = id(j)(0) & Mid(TextBox1.Text, i, 1)
                id(j)(1) = 1
            ElseIf Mid(TextBox1.Text, i, 1) = vbCr And id(j)(0) <> "" Then
                j = j + 1
            End If
        Next
        For i = 0 To id.Length - 1
            If id(i)(1) = 1 Then
                count = count + 1
            End If
        Next

        count_tmp = count
        ReDim img(count - 1)
        For i = 0 To img.Length - 1
            ReDim img(i)(CB_count - 1)
        Next
        Panel1.Controls.Clear()

        Try
            For Date_Shift = 0 To Date_Diff
                Dim date_tmp As Date = DateAdd(DateInterval.Day, Date_Shift, S_date)
                For i = 0 To count - 1
                    Dim if_search As Boolean
                    k = 0
                    For j = 0 To CB.Length - 1
                        If CB(j).Checked Then

                            'path_sou = EqpNum2Path全圖備份(DropDownList1.SelectedValue) & Format(date_tmp, "yyyyMMdd") & "\" & id(i)(0) & j & ".jpg"
                            'path_des = "D:\WebSite_new_WithCanvas\images\A\" & id(i)(0) & j & ".jpg"
                            path_des = "E:\WebSite_new_WithCanvas\images\A\" & id(i)(0) & j & ".jpg"
                            path_desB = "~\images\A\" & id(i)(0) & j & ".jpg"

                            If File.Exists(path_sou) Then

                                File.Copy(path_sou, path_des, True)

                                If sender.TabIndex = 0 Then

                                    img(count - count_tmp)(k) = New Image
                                    With img(count - count_tmp)(k)

                                        .ImageUrl = path_desB
                                        .Width = 500
                                        .ID = "img_" & id(i)(0)

                                    End With

                                    Panel1.Controls.Add(img(count - count_tmp)(k))

                                ElseIf sender.TabIndex = 1 Then
                                    Response.Write("download.aspx ? filename=" & id(i)(0) & j & ".jpg")
                                    Response.Write("<script>window.open ('" & "download.aspx?filename=" & id(i)(0) & j & ".jpg" & "');</script>")
                                End If
                                if_search = True
                                k = k + 1

                            Else
                                '  Label1.Text = path_sou & " not exist" 'ex.Message
                            End If
                        End If
                    Next

                    If if_search Then
                        count_tmp = count_tmp - 1
                    End If

                Next
                If count_tmp = 0 Then Exit For
            Next

        Catch ex As Exception
            Label1.Text = ex.Message
        End Try

    End Sub

    Private Sub FullImage_Load(sender As Object, e As EventArgs) Handles Me.Load

        Image2.ImageUrl = "~\images\Chip.png"

    End Sub


    Function EqpNum2Path全圖備份(ByVal eqpnum As Integer) As String

        If Not IsNumeric(eqpnum) Then Return ""
        If eqpnum > 15 Or eqpnum < 0 Then Return ""

        Select Case eqpnum
            Case 1, 2, 3, 4, 5, 6
                Return "\\101.101.101.201\CCT" & Num2Eqp(eqpnum) & "00\全圖備份\"
            Case 7, 8, 9, 10, 11, 12
                Return "\\101.101.101.202\CCT" & Num2Eqp(eqpnum) & "00\全圖備份\"
            Case 13, 14, 15
                Return "\\101.101.101.203\CCT" & Num2Eqp(eqpnum) & "00\全圖備份\"
        End Select

    End Function
    Function Num2Eqp(ByRef eqpnum As String) As String

        If eqpnum > 15 Or eqpnum < 0 Then Return 0
        If eqpnum <= 9 Then Return eqpnum

        Select Case eqpnum
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

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim f As String


        'For Each f In Directory.GetFiles("D:\WebSite_new_WithCanvas\images\A\")
        For Each f In Directory.GetFiles("E:\WebSite_new_WithCanvas\images\A\")

            If Path.GetExtension(f) = ".jpeg" Or Path.GetExtension(f) = ".jpg" Then
                File.Delete(f)
            End If

        Next

    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        comp("D:\201702")
    End Sub


    Sub comp(ByVal path As String)

        Dim exePath As String = "C:\Program Files\7-Zip\7z.exe"
        'C:\Cheat Engine\*
        ' a -tzip archive.zip test1.txt test2.txt test3.txt
        Dim args As String = "a -tzip " & path & "\* "
        Dim Soupath As String = "D:\"
        Dim DesPath As String = ""

        'If Directory.Exists(DesPath) Then Directory.CreateDirectory(DesPath)


        System.Diagnostics.Process.Start(exePath, args)

    End Sub



End Class
