
Option Explicit On
Option Strict On

Imports System.Web.Configuration

Public Class cpApiClass
    '
    '==============================================================================
    ''' <summary>
    ''' return the contensive page
    ''' </summary>
    ''' <param name="cp"></param>
    ''' <param name="isAdmin"></param>
    ''' <returns></returns>
    Public Shared Function getContensivePage(cp As Contensive.Processor.CPClass, page As Page, context As HttpContext, isAdmin As Boolean) As String
        Dim result As String = ""
        Try
            Dim c As String
            Dim cName As String
            Dim row As String()
            Dim rowPtr As Integer
            '
            ' -- Setup basic environment
            cp.Context.appName = WebConfigurationManager.AppSettings("ContensiveAppName")
            cp.Context.domain = page.Request.ServerVariables("SERVER_NAME")
            cp.Context.pathPage = CStr(page.Request.ServerVariables("SCRIPT_NAME"))
            cp.Context.referrer = CStr(page.Request.ServerVariables("HTTP_REFERER"))
            cp.Context.isSecure = CBool(page.Request.ServerVariables("SERVER_PORT_SECURE"))
            cp.Context.remoteIp = CStr(page.Request.ServerVariables("REMOTE_ADDR"))
            cp.Context.browserUserAgent = CStr(page.Request.ServerVariables("HTTP_USER_AGENT"))
            cp.Context.acceptLanguage = CStr(page.Request.ServerVariables("HTTP_ACCEPT_LANGUAGE"))
            cp.Context.accept = CStr(page.Request.ServerVariables("HTTP_ACCEPT"))
            cp.Context.acceptCharSet = CStr(page.Request.ServerVariables("HTTP_ACCEPT_CHARSET"))
            cp.Context.profileUrl = CStr(page.Request.ServerVariables("HTTP_PROFILE"))
            cp.Context.xWapProfile = CStr(page.Request.ServerVariables("HTTP_X_WAP_PROFILE"))
            '
            ' -- Create ServerQueryString
            Dim requestUrl As String = page.Request.Url.ToString()
            Dim queryPos As Integer = requestUrl.IndexOf("?")
            If (queryPos > 0) Then
                cp.Context.queryString = requestUrl.Substring(queryPos + 1)
            End If
            '
            ' -- handle files
            Dim formFiles As String = ""
            Dim filePtr As Integer = 0
            Dim formNames As String() = page.Request.Files.AllKeys
            Dim tmpFiles As New List(Of String)
            For Each formName As String In formNames
                Dim file As HttpPostedFile = page.Request.Files(formName)
                If (file IsNot Nothing) Then
                    If (file.ContentLength > 0) And (Not String.IsNullOrEmpty(file.FileName)) Then
                        Dim tmpPathFilename As String = IO.Path.Combine(System.IO.Path.GetTempPath, file.FileName)
                        tmpFiles.Add(tmpPathFilename)
                        file.SaveAs(tmpPathFilename)
                        formFiles &= "&" & filePtr.ToString() & "formname=" & formName
                        formFiles &= "&" & filePtr.ToString() & "filename=" & file.FileName
                        formFiles &= "&" & filePtr.ToString() & "type=" & file.ContentType
                        formFiles &= "&" & filePtr.ToString() & "tmpfile=" & tmpPathFilename
                        formFiles &= "&" & filePtr.ToString() & "error="
                        formFiles &= "&" & filePtr.ToString() & "size=" & file.ContentLength
                    End If
                End If
            Next
            If (Not String.IsNullOrEmpty(formFiles)) Then
                cp.Context.formFiles = formFiles.Substring(1)
            End If
            '
            ' -- handle form
            c = ""
            For Each key As String In page.Request.Form
                If (Not String.IsNullOrEmpty(key)) Then
                    c = c & "&" & encodeAmpEqual(key) & "=" & encodeAmpEqual(page.Request.Unvalidated(key))
                End If
            Next
            If Len(c) > 0 Then
                c = Mid(c, 2)
            End If
            cp.Context.form = c
            '
            ' -- Cookies string
            c = ""
            For Each key As String In page.Request.Cookies
                If (Not String.IsNullOrEmpty(key)) Then
                    c = c & "&" & encodeAmpEqual(key) & "=" & encodeAmpEqual(page.Request.Cookies.Item(key).Value)
                End If
            Next
            If Len(c) > 0 Then
                c = Mid(c, 2)
            End If
            cp.Context.cookies = c
            '
            ' -- build doc
            result = result & cp.getDoc(isAdmin)
            '
            ' -- setup IIS Response
            page.Response.CacheControl = "no-cache"
            page.Response.Expires = -1
            page.Response.Buffer = True
            '
            ' -- delete temp uploaded files
            If (tmpFiles.Count > 0) Then
                For Each tmpFile As String In tmpFiles
                    If (Not String.IsNullOrEmpty(tmpFile)) Then
                        cp.File.Delete(tmpFile)
                    End If
                Next
            End If
            '
            If cp.Context.responseRedirect <> "" Then
                '
                ' -- redirect
                If Not page.Response.IsRequestBeingRedirected Then
                    page.Response.Redirect(cp.Context.responseRedirect, False)
                    context.ApplicationInstance.CompleteRequest()
                End If
            Else
                '
                ' -- concatinate writestream data to the end of the body
                result = result & cp.Context.responseBuffer
                '
                ' -- set content type
                If cp.Context.responseContentType <> "" Then
                    page.Response.ContentType = cp.Context.responseContentType
                End If
                '
                ' -- set cookies
                c = cp.Context.responseCookies
                If c <> "" Then
                    row = Split(c, vbCrLf)
                    Do While (rowPtr + 5) <= UBound(row)
                        cName = row(rowPtr + 0)
                        If cName <> "" Then
                            page.Response.Cookies.Add(New HttpCookie(cName, row(rowPtr + 1)))
                            c = row(rowPtr + 2)
                            If c <> "" Then
                                page.Response.Cookies(cName).Expires = cp.Utils.EncodeDate(c)
                            End If
                            c = row(rowPtr + 3)
                            If c <> "" Then
                                page.Response.Cookies(cName).Domain = c
                            End If
                            c = row(rowPtr + 4)
                            If c <> "" Then
                                page.Response.Cookies(cName).Path = c
                            End If
                            c = row(rowPtr + 5)
                            If c <> "" Then
                                page.Response.Cookies(cName).Secure = cp.Utils.EncodeBoolean(c)
                            End If
                        End If
                        rowPtr = rowPtr + 6
                    Loop
                End If
                '
                ' -- set headers
                c = cp.Context.responseHeaders
                If c <> "" Then
                    row = Split(c, vbCrLf)
                    rowPtr = 0
                    Do While (rowPtr + 1) <= UBound(row)
                        cName = row(rowPtr + 0)
                        If cName <> "" Then
                            Call page.Response.AddHeader(cName, CStr(row(rowPtr + 1)))
                        End If
                        rowPtr = rowPtr + 2
                    Loop
                End If
                '
                ' -- set http status
                c = cp.Context.responseStatus
                If c <> "" Then
                    page.Response.Status = c
                End If
            End If
        Catch ex As Exception
            cp.Site.ErrorReport(ex)
        End Try
        Return result
    End Function
    '
    Private Shared Function encodeAmpEqual(source As String) As String
        Return source.Replace("%", "%25").Replace("=", "%3D").Replace("&", "%26").Replace("+", "%2B")
    End Function

End Class
