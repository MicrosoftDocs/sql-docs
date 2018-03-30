Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBTriggers1

    Sub Main()
        ' <snippetSMO_VBTriggers1>
        'Connect to the local, default instance of SQL Server.
        Dim mysrv As Server
        mysrv = New Server
        'Reference the AdventureWorks2012 2008R2 database.
        Dim mydb As Database
        mydb = mysrv.Databases("AdventureWorks2012")
        'Declare a Table object variable and reference the Customer table.
        Dim mytab As Table
        mytab = mydb.Tables("Customer", "Sales")
        'Define a Trigger object variable by supplying the parent table, schema ,and name in the constructor.
        Dim tr As Trigger
        tr = New Trigger(mytab, "Sales")
        'Set TextMode property to False, then set other properties to define the trigger.
        tr.TextMode = False
        tr.Insert = True
        tr.Update = True
        tr.InsertOrder = Agent.ActivationOrder.First
        Dim stmt As String
        stmt = " RAISERROR('Notify Customer Relations',16,10) "
        tr.TextBody = stmt
        tr.ImplementationType = ImplementationType.TransactSql
        'Create the trigger on the instance of SQL Server.
        tr.Create()
        'Remove the trigger.
        tr.Drop()
        ' </snippetSMO_VBTriggers1>
    End Sub

End Module
