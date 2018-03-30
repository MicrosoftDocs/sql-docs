Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Module SMO_VBXMLSchema1Cleanup

    Sub Main()
        Dim srv As Server
        srv = New Server
        srv.Databases("AdventureWorks2012").XmlSchemaCollections("MySampleCollection").Drop()

    End Sub

End Module
