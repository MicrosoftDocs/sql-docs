Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBPartitions1

    Sub Main()
        ' <snippetSMO_VBPartition1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server()
        'Reference the AdventureWorks2012database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define and create three new file groups on the database.
        Dim fg2 As FileGroup
        fg2 = New FileGroup(db, "Second")
        fg2.Create()
        Dim fg3 As FileGroup
        fg3 = New FileGroup(db, "Third")
        fg3.Create()
        Dim fg4 As FileGroup
        fg4 = New FileGroup(db, "Fourth")
        fg4.Create()
        'Define a partition function by supplying the parent database and name arguments in the constructor.
        Dim pf As PartitionFunction
        pf = New PartitionFunction(db, "TransHistPF")
        'Add a partition function parameter that specifies the function uses a DateTime range type.
        Dim pfp As PartitionFunctionParameter
        pfp = New PartitionFunctionParameter(pf, DataType.DateTime)
        pf.PartitionFunctionParameters.Add(pfp)
        'Specify the three dates that divide the data into four partitions.
        Dim val() As Object
        val = New Object() {"1/1/2003", "1/1/2004", "1/1/2005"}
        pf.RangeValues = val
        'Create the partition function.
        pf.Create()
        'Define a partition scheme by supplying the parent database and name arguments in the constructor.
        Dim ps As PartitionScheme
        ps = New PartitionScheme(db, "TransHistPS")
        'Specify the partition function and the filegroups required by the partition scheme.
        ps.PartitionFunction = "TransHistPF"
        ps.FileGroups.Add("PRIMARY")
        ps.FileGroups.Add("second")
        ps.FileGroups.Add("Third")
        ps.FileGroups.Add("Fourth")
        'Create the partition scheme.
        ps.Create()
        ' </snippetSMO_VBPartition1>
        ps.Drop()
        pf.Drop()
        fg2.Drop()
        fg3.Drop()
        fg4.Drop()

    End Sub

End Module
