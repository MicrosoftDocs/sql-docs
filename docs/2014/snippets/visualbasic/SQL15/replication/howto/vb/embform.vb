Imports Microsoft.SqlServer.MessageBox
Imports system.Windows.Forms
Imports Microsoft.Win32

Public Class EMBForm
    Private alwaysShow As Boolean = True
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '<snippetemb_vb_showOKbutton>
        Try
            ' Do something that may generate an exception.
            Throw New ApplicationException("An error has occured")
        Catch ex As ApplicationException
            ' Define a new top-level error message.
            Dim str As String = "The action failed."

            ' Add the new top-level message to the handled exception.
            Dim exTop As ApplicationException = New ApplicationException(str, ex)
            exTop.Source = Me.Text

            ' Show an exception message box with an OK button (the default).
            Dim box As ExceptionMessageBox = New ExceptionMessageBox(exTop)
            box.Show(Me)
        End Try
        '</snippetemb_vb_showOKbutton>
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        '<snippetemb_vb_showYesNobutton>
        ' Define the message and caption to display.
        Dim str As String = "Are you sure you want to delete file 'c:\somefile.txt'?"
        Dim caption As String = "Confirm File Deletion"

        ' Show the exception message box with Yes and No buttons.
        Dim box As ExceptionMessageBox = New ExceptionMessageBox(str, _
         caption, ExceptionMessageBoxButtons.YesNo, _
         ExceptionMessageBoxSymbol.Question, _
         ExceptionMessageBoxDefaultButton.Button2)

        If Windows.Forms.DialogResult.Yes = box.Show(Me) Then
            ' Delete the file.
        End If
        '</snippetemb_vb_showYesNobutton>

    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        '<snippetemb_vb_showcustombutton>
        Try
            ' Do something that may cause an exception.
            Throw New ApplicationException("An error has occured")
        Catch ex As ApplicationException
            Dim str As String = "Action failed. What do you want to do?"
            Dim exTop As ApplicationException = New ApplicationException(str, ex)
            exTop.Source = Me.Text

            ' Show the exception message box with three custom buttons.
            Dim box As ExceptionMessageBox = New ExceptionMessageBox(exTop)

            ' Set the names of the three custom buttons.
            box.SetButtonText("Skip", "Retry", "Stop Processing")

            ' Set the Retry button as the default.
            box.DefaultButton = ExceptionMessageBoxDefaultButton.Button2
            box.Symbol = ExceptionMessageBoxSymbol.Question
            box.Buttons = ExceptionMessageBoxButtons.Custom

            box.Show(Me)

            ' Do something, depending on the button that the user clicks.
            Select Case box.CustomDialogResult
                Case ExceptionMessageBoxDialogResult.Button1
                    ' Skip action
                Case ExceptionMessageBoxDialogResult.Button2
                    ' Retry action
                Case ExceptionMessageBoxDialogResult.Button3
                    ' Stop processing action
            End Select
        End Try
        '</snippetemb_vb_showcustombutton>
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        '<snippetemb_vb_usecheckbox>
        Try
            ' Do something that may cause an exception.
            Throw New ApplicationException("An error has occured.")
        Catch ex As ApplicationException
            Dim str As String = "The action failed."
            Dim exTop As ApplicationException = New ApplicationException(str, ex)
            exTop.Source = Me.Text

            ' Show a message box if the global variable is true.
            If alwaysShow Then
                Dim box As ExceptionMessageBox = New ExceptionMessageBox(exTop)
                box.ShowCheckBox = True
                box.IsCheckBoxChecked = True
                box.CheckBoxText = "Always show this message"
                box.Show(Me)

                ' Set the global variable.
                alwaysShow = box.IsCheckBoxChecked
            End If
        End Try
        '</snippetemb_vb_usecheckbox>
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        '<snippetemb_vb_useregkey>
        Try
            ' Do something that could generate an exception.
            Throw New ApplicationException("An error has occured.")
        Catch ex As ApplicationException
            Dim str As String = "The action failed. Do you want to continue?"
            Dim exTop As ApplicationException = New ApplicationException(str, ex)
            exTop.Source = Me.Text

            ' Show a message box with Yes and No buttons
            Dim box As ExceptionMessageBox = New ExceptionMessageBox(exTop, _
             ExceptionMessageBoxButtons.YesNo, _
             ExceptionMessageBoxSymbol.Question, _
             ExceptionMessageBoxDefaultButton.Button2)

            ' Enable the check box.
            box.ShowCheckBox = True

            ' Define the registry key to use.
            box.CheckBoxRegistryKey = _
            Microsoft.Win32.Registry.CurrentUser.CreateSubKey( _
             "Software\TestApp")
            box.CheckBoxRegistryValue = "DontShowActionFailedMessage"
            box.CheckBoxRegistryMeansDoNotShowDialog = True
            box.DefaultDialogResult = Windows.Forms.DialogResult.Yes

            ' The message box won't be displayed if the
            ' "DontShowActionFailedMessage" value of the registry key 
            ' contains a non-zero value.
            If box.Show(Me) = Windows.Forms.DialogResult.No Then
                ' Do something if the user clicks the No button.
                Me.Close()
            End If
        End Try
        '</snippetemb_vb_useregkey>

    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        '<snippetemb_vb_moreinfo>
        Try
            ' Do something that you don't expect to generate an exception.
            Throw New ApplicationException("Failed to connect to the server.")
        Catch ex As ApplicationException
            Dim str As String = "An unexpected error occurred. Please call Helpdesk."
            Dim exTop As ApplicationException = New ApplicationException(str, ex)
            exTop.Source = Me.Text

            ' Information in the Data property of an exception that has a name
            ' beginning with "HelpLink.Advanced" is shown when the user
            ' clicks the Advanced Information button of the exception message
            ' box dialog box.
            exTop.Data.Add("AdvancedInformation.FileName", "application.dll")
            exTop.Data.Add("AdvancedInformation.FilePosition", "line 355")
            exTop.Data.Add("AdvancedInformation.UserContext", "single user mode")

            ' Show the exception message box with additional information that 
            ' is helpful when a user calls technical support.
            Dim box As ExceptionMessageBox = New ExceptionMessageBox(exTop)

            box.Show(Me)

        End Try
        '</snippetemb_vb_moreinfo>
    End Sub

    Private Sub EMBForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Clear any existing reg keys.
        Dim key As RegistryKey
        key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software")
        Dim keyname As String
        For Each keyname In key.GetSubKeyNames()
            If keyname = "TestApp" Then
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKey("Software\TestApp")
            End If
        Next
    End Sub
End Class
