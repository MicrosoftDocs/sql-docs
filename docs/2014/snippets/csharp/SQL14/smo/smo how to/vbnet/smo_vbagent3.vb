Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo.Agent
Module SMO_VBAgent3

    Sub Main()
        ' <snippetSMO_VBAgent3>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Define an Alert object variable by supplying the SQL Agent and the name arguments in the constructor.
        Dim al As Alert
        al = New Alert(srv.JobServer, "Test_Alert")
        'Specify the performance condition string to define the alert.
        al.PerformanceCondition = "SQLServer:General Statistics|User Connections||>|3"
        'Create the alert on the SQL Agent.
        al.Create()
        'Define an Operator object variable by supplying the SQL Agent and the name arguments in the constructor.
        Dim op As [Operator]
        op = New [Operator](srv.JobServer, "Test_Operator")
        'Set the net send address.
        op.NetSendAddress = "NetworkPC"
        'Create the operator on the SQL Agent.
        op.Create()
        'Run the AddNotification method to specify the operator is notified when the alert is raised.
        al.AddNotification("Test_Operator", NotifyMethods.NetSend)
        ' </snippetSMO_VBAgent3>
        op.Drop()
        al.Drop()

    End Sub

End Module
