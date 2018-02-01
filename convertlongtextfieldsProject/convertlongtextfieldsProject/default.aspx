<script runat="server">
    '
    '==============================================================================
    ''' <summary>
    ''' run during page load
    ''' </summary>
    Sub Page_Load()
        Dim cp As New Contensive.Processor.CPClass
        Dim doc As String = convertlongtextfieldsProject.cpApiClass.getContensivePage(cp, Page, Context, False)
        If cp.Response.isOpen() Then
            'doc = Replace(doc, "$myCustomTag$", "<div>cp.user.name = " & cp.User.Name & "</div>")
        End If
        Response.Write(doc)
        cp.Dispose()
    End Sub
</script>
