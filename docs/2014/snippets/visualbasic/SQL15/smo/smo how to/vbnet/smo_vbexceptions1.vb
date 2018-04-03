Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo.Agent
Module SMO_VBExceptions1

    Sub Main()
        ' <snippetSMO_VBExceptions1>
        'This sample requires the Microsoft.SqlServer.Management.Smo.Agent namespace is included.
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Define an Operator object variable by supplying the parent SQL Agent and the name arguments in the constructor.
        'Note that the Operator type requires [] parenthesis to differentiate it from a Visual Basic key word.
        Dim op As [Operator]
        op = New [Operator](srv.JobServer, "Test_Operator")
        op.Create()
        'Start exception handling.
        Try
            'Create the operator again to cause an SMO exception.
            Dim opx As OperatorCategory
            opx = New OperatorCategory(srv.JobServer, "Test_Operator")
            opx.Create()
            'Catch the SMO exception
        Catch smoex As SmoException
            Console.WriteLine("This is an SMO Exception")
            'Display the SMO exception message.
            Console.WriteLine(smoex.Message)
            'Display the sequence of non-SMO exceptions that caused the SMO exception.
            Dim ex As Exception
            ex = smoex.InnerException
            Do While ex.InnerException IsNot (Nothing)
                Console.WriteLine(ex.InnerException.Message)
                ex = ex.InnerException
            Loop
            'Catch other non-SMO exceptions.
        Catch ex As Exception
            Console.WriteLine("This is not an SMO exception.")
        End Try
        ' </snippetSMO_VBExceptions1>
        op.Drop()



    End Sub

End Module
   