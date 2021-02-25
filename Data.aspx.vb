
Partial Class Data
    Inherits System.Web.UI.Page

    Private Sub Data_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim default_file_root As String = "E:\WebSite_new_WithCanvas\images\A\"
        Dim addr As String = Context.Request.QueryString("addr")
        Dim file As String = Context.Request.QueryString("file")
        Dim cla As New Class1

        If addr <> "" And file <> "" Then
            Dim tmppath_sou As String = addr & file
            Dim tmppath_desAcc As String = default_file_root & file
            Dim tmppath_desCSV As String = default_file_root & file

            If System.IO.File.Exists(tmppath_sou) Then
                System.IO.File.Copy(tmppath_sou, tmppath_desAcc, True)

                With Response
                    .ContentType = "application/save-as"
                    .AddHeader("content-disposition", "attachment; filename=")
                    .WriteFile(tmppath_desAcc)
                End With
            End If
        End If
    End Sub
End Class
