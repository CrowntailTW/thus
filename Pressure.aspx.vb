Partial Class Pressure

    Inherits System.Web.UI.Page

    Protected Sub Button_Click(sender As Button, e As EventArgs) Handles Button_CCT100.Click, Button_CCT200.Click, Button_CCT300.Click, Button_CCT400.Click, Button_CCT500.Click, Button_CCT600.Click, Button_CCT700.Click, Button_CCT800.Click, Button_CCT900.Click, Button_CCTA00.Click, Button_CCTB00.Click, Button_CCTC00.Click, Button_CCTD00.Click, Button_CCTE00.Click, Button_CCTF00.Click


        Dim EqpID As String
        Dim S_date, E_date As Date

        E_date = New Date(Mid(datepicker_E.Text, 7, 4), Mid(datepicker_E.Text, 1, 2), Mid(datepicker_E.Text, 4, 2), 23, 59, 59)
        S_date = New Date(Mid(datepicker_S.Text, 7, 4), Mid(datepicker_S.Text, 1, 2), Mid(datepicker_S.Text, 4, 2), 0, 0, 0)
        E_date = Format(E_date, "yyyy/MM/dd")
        S_date = Format(S_date, "yyyy/MM/dd")

        EqpID = sender.Text
        Response.Write("<script>window.open ('" & "Pressure_Info.aspx?FromDate=" & S_date & "&ToDate=" & E_date & "&EqpID=" & EqpID & "');</script>")

    End Sub

    Private Sub Pressure_Load(sender As Object, e As EventArgs) Handles Me.Load
        If datepicker_E.Text = "" Then
            datepicker_E.Text = Format(Now, "MM/dd/yyyy")
            datepicker_S.Text = Format(Now, "MM/dd/yyyy")
        End If
    End Sub

End Class

