Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBStatistics1

    Sub Main()
        ' <snippetSMO_VBStatistics1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Reference the CreditCard table.
        Dim tb As Table
        tb = db.Tables("CreditCard", "Sales")
        'Define a Statistic object by supplying the parent table and name arguments in the constructor.
        Dim stat As Statistic
        stat = New Statistic(tb, "Test_Statistics")
        'Define a StatisticColumn object variable for the CardType column and add to the Statistic object variable.
        Dim statcol As StatisticColumn
        statcol = New StatisticColumn(stat, "CardType")
        stat.StatisticColumns.Add(statcol)
        'Create the statistic counter on the instance of SQL Server.
        stat.Create()
        ' </snippetSMO_VBStatistics1>
        stat.Drop()
    End Sub

End Module
