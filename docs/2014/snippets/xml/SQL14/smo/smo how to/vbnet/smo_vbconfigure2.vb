Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo

Module SMO_VBConfigure2

    Sub Main()
        ' <snippetSMO_VBConfigure2>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Display all the configuration options.
        Dim p As ConfigProperty
        For Each p In srv.Configuration.Properties
            Console.WriteLine(p.DisplayName)
        Next
        Console.WriteLine("There are " & srv.Configuration.Properties.Count.ToString & " configuration options.")
        'Display the maximum and minimum values for ShowAdvancedOptions.
        Dim min As Integer
        Dim max As Integer
        min = srv.Configuration.ShowAdvancedOptions.Minimum
        max = srv.Configuration.ShowAdvancedOptions.Maximum
        Console.WriteLine("Minimum and Maximum values are " & min & " and " & max & ".")
        'Modify the value of ShowAdvancedOptions and run the Alter method.
        srv.Configuration.ShowAdvancedOptions.ConfigValue = 0
        srv.Configuration.Alter()
        'Display when the change takes place according to the IsDynamic property.
        If srv.Configuration.ShowAdvancedOptions.IsDynamic = True Then
            Console.WriteLine("Configuration option has been updated.")
        Else
            Console.WriteLine("Configuration option will be updated when SQL Server is restarted.")
        End If
        ' </snippetSMO_VBConfigure2>
    End Sub

End Module
