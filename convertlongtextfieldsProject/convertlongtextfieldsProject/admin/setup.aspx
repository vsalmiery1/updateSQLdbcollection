<script runat="server">
    '
    '==============================================================================
    ''' <summary>
    ''' convert preferences to aspx
    ''' </summary>
    Sub Page_Load()
        Dim cp As New Contensive.Processor.CPClass
        Dim doc As String = cpApiClass.getContensivePage(cp, Page, True)
        If cp.Response.isOpen() Then
            If cp.User.IsDeveloper Then
                Call cp.Db.ExecuteSQL("update ccsetup set fieldvalue='default.aspx' where name='SERVERPAGEDEFAULT'")
                Call cp.Db.ExecuteSQL("update ccsetup set fieldvalue='/default.aspx' where name='SECTIONLANDINGLINK'")
                Call cp.Db.ExecuteSQL("update ccsetup set fieldvalue='/admin/default.aspx' where name='adminUrl'")
            End If
        End If
        Response.Write(doc)
        cp.Dispose()
    End Sub
</script>
