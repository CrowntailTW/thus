
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As EventArgs) ' Handles Button1.Click
        Dim cstext As New StringBuilder

        cstext.Append("<script>  var ctx = document.getElementById('canvas');var myChart = New Chart(ctx, {")
        cstext.Append("Type: 'bar',")
        cstext.Append("data: {")
        cstext.Append("labels: [""一月"", ""二月"", ""三月""],")
        cstext.Append("datasets: [{")
        cstext.Append("backgroundColor: [")
        cstext.Append(" 'rgba(255, 99, 132, 0.2)',")
        cstext.Append("   'rgba(54, 162, 235, 0.2)',")
        cstext.Append("    'rgba(255, 206, 86, 0.2)'")
        cstext.Append("  ],")
        cstext.Append("    borderColor: [")
        cstext.Append("     'rgba(255,99,132,1)',")
        cstext.Append("     'rgba(54, 162, 235, 1)',")
        cstext.Append("     'rgba(255, 206, 86, 1)',")
        cstext.Append("   'rgba(75, 192, 192, 1)'")
        cstext.Append(" ],")
        cstext.Append("borderWidth: 1,")
        cstext.Append("label: ""銷售業績(百萬)"",")
        cstext.Append("data: [60, 49, 72]")
        cstext.Append(" }]")
        cstext.Append("}")
        cstext.Append("}); </script>")

        Page.ClientScript.RegisterStartupScript(Page.GetType, "Typsd", cstext.ToString)

    End Sub
End Class
