Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBSynonyms1

    Sub Main()
        ' <snippetSMO_VBSynonyms1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server()
        'Reference the AdventureWorks2012 2008R2 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a Synonym object variable by supplying the parent database, name, and schema arguments in the constructor.
        'The name is also a synonym of the name of the base object.
        Dim syn As Synonym
        syn = New Synonym(db, "Shop", "Sales")
        'Specify the base object, which is the object on which the synonym is based.
        syn.BaseDatabase = "AdventureWorks2012"
        syn.BaseSchema = "Sales"
        syn.BaseObject = "Store"
        syn.BaseServer = srv.Name
        'Create the synonym on the instance of SQL Server.
        syn.Create()
        ' </snippetSMO_VBSynonyms1>
        syn.Drop()

    End Sub

End Module
