---
title: "Using XML Schemas | Microsoft Docs"
ms.custom: ""
ms.date: 01/11/2019
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
helpviewer_keywords: 
  - "XML [SMO]"
ms.assetid: 9d04de01-efeb-4b2d-8c28-3234bc7ff2f3
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using XML Schemas

[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

  XML programming in SMO is limited to providing XML data types, XML namespaces, and simple indexing on XML data type columns.  
  
 [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides native storage for XML document instances. XML schemas let you define complex XML data types, which can be used to validate XML documents to ensure data integrity. The XML schema is defined in the <xref:Microsoft.SqlServer.Management.Smo.XmlSchemaCollection> object.  
  
## Example  
 To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
## Creating an XML Schema in Visual Basic  
 This code example shows how to create an XML schema by using the <xref:Microsoft.SqlServer.Management.Smo.XmlSchemaCollection> object. The <xref:Microsoft.SqlServer.Management.Smo.XmlSchemaCollection.Text%2A> property, which defines the XML schema collection, contains several double quotation marks. These are replaced by the `chr(34)` string.  
  
```VBNET
'Connect to the local, default instance of SQL Server.
Dim srv As Server
srv = New Server()
'Reference the AdventureWorks2012 2008R2 database.
Dim db As Database
db = srv.Databases("AdventureWorks2012")
'Define an XmlSchemaCollection object by supplying the parent database and name arguments in the constructor.
Dim xsc As XmlSchemaCollection
xsc = New XmlSchemaCollection(db, "MySampleCollection")
xsc.Text = "\<schema xmlns=" + Chr(34) + "http://www.w3.org/2001/XMLSchema" + Chr(34) + "  xmlns:ns=" + Chr(34) + "http://ns" + Chr(34) + ">\<element name=" + Chr(34) + "e" + Chr(34) + " type=" + Chr(34) + "dateTime" + Chr(34) + "/></schema>"
'Create the XML schema collection on the instance of SQL Server.
xsc.Create()
```
  
## Creating an XML Schema in Visual C#  
 This code example shows how to create an XML schema by using the <xref:Microsoft.SqlServer.Management.Smo.XmlSchemaCollection> object. The <xref:Microsoft.SqlServer.Management.Smo.XmlSchemaCollection.Text%2A> property, which defines the XML schema collection, contains several double quotation marks. These are replaced by the `chr(34)` string.  
  
```csharp  
{  
            //Connect to the local, default instance of SQL Server.   
            Server srv = default(Server);  
            srv = new Server();  
            //Reference the AdventureWorks2012 database.   
            Database db = default(Database);  
            db = srv.Databases["AdventureWorks2012"];  
            //Define an XmlSchemaCollection object by supplying the parent  
            // database and name arguments in the constructor.   
            XmlSchemaCollection xsc = default(XmlSchemaCollection);  
            xsc = new XmlSchemaCollection(db, "MySampleCollection");  
            xsc.Text = "\<schema xmlns=" + Strings.Chr(34) + "http://www.w3.org/2001/XMLSchema" + Strings.Chr(34) + " xmlns:ns=" + Strings.Chr(34) + "http://ns" + Strings.Chr(34) + ">\<element name=" + Strings.Chr(34) + "e" + Strings.Chr(34) + " type=" + Strings.Chr(34) + "dateTime" + Strings.Chr(34) + "/></schema>";  
            //Create the XML schema collection on the instance of SQL Server.   
            xsc.Create();  
        }  
```  
  
## Creating an XML Schema in PowerShell  
 This code example shows how to create an XML schema by using the <xref:Microsoft.SqlServer.Management.Smo.XmlSchemaCollection> object. The <xref:Microsoft.SqlServer.Management.Smo.XmlSchemaCollection.Text%2A> property, which defines the XML schema collection, contains several double quotation marks. These are replaced by the `chr(34)` string.  
  
```powershell   
#Get a server object which corresponds to the default instance replace LocalMachine with the physical server  
cd \sql\LocalHost  
$srv = get-item default  
  
#Reference the AdventureWorks database.  
$db = $srv.Databases["AdventureWorks2012"]  
  
#Create a new schema collection  
$xsc = New-Object -TypeName Microsoft.SqlServer.Management.SMO.XmlSchemaCollection `  
-argumentlist $db,"MySampleCollection"  
  
#Add the xml  
$dq = '"' # the double quote character  
$xsc.Text = "<schema xmlns=" + $dq + "http://www.w3.org/2001/XMLSchema" + $dq + `  
"  xmlns:ns=" + $dq + "http://ns" + $dq + "><element name=" + $dq + "e" + $dq +`  
 " type=" + $dq + "dateTime" + $dq + "/></schema>"  
  
#Create the XML schema collection on the instance of SQL Server.  
$xsc.Create()  
```  
  
  
