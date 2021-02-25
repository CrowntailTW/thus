
Partial Class download
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'Dim default_file_root As String = "D:\WebSite_new_WithCanvas\images\A\"
            Dim default_file_root As String = "E:\WebSite_new_WithCanvas\images\A\"
            Dim user_request As String = Context.Request.QueryString("filename")

            If user_request <> "" Then
                Dim filep As String = default_file_root & user_request
                If System.IO.File.Exists(filep) Then
                    With Response
                        .ContentType = "application/save-as"
                        .AddHeader("content-disposition", "attachment; filename=" & user_request)
                        .WriteFile(filep)
                    End With
                Else
                    Response.Write("無檔案")
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


End Class
