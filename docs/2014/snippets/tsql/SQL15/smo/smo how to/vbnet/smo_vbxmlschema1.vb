Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBXMLSchema1

    Sub Main()
        ' <snippetSMO_VBXMLSchema1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server()
        'Reference the AdventureWorks2012 2008R2 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define an XmlSchemaCollection object by supplying the parent database and name arguments in the constructor.
        Dim xsc As XmlSchemaCollection
        xsc = New XmlSchemaCollection(db, "MySampleCollection")
        xsc.Text = "<schema xmlns=" + Chr(34) + "http://www.w3.org/2001/XMLSchema" + Chr(34) + "  xmlns:ns=" + Chr(34) + "http://ns" + Chr(34) + "><element name=" + Chr(34) + "e" + Chr(34) + " type=" + Chr(34) + "dateTime" + Chr(34) + "/></schema>"
        'Create the XML schema collection on the instance of SQL Server.
        xsc.Create()
        ' </snippetSMO_VBXMLSchema1>

    End Sub

End Module
