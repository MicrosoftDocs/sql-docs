---
title: "Executing Templates That Contain SQL Queries (SQLXMLOLEDB Provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "templates [SQLXML]"
  - "SQLXMLOLEDB Provider, executing template files"
  - "templates [SQLXML], SQL queries"
  - "XML templates [SQLXML]"
  - "SQL queries [SQLXML]"
ms.assetid: ff2bc36f-e3fb-4d8f-8e3a-2680a39eda11
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Executing Templates That Contain SQL Queries (SQLXMLOLEDB Provider)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This example illustrates the use of the SQLXMLOLEDB Provider-specific property ClientSideXML. In this client-side ADO sample application, an XML template that consists of an SQL query is executed on the server.  
  
 Because the ClientSideXML property is set to True, the SELECT statement without the FOR XML clause is sent to the server. The server executes the query and returns a rowset to the client. The client then applies the FOR XML transformation to the rowset and produces an XML document.  
  
 The XML template provides a single top-level root element (\<ROOT>) for the XML document that is generated; therefore, the xml root property is not provided.  
  
 To execute XML templates, the dialect {5d531cb2-e6ed-11d2-b252-00c04f681b71} must be specified.  
  
> [!NOTE]  
>  In the code, you must provide the name of the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the connection string. Also, this example specifies the use of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client (SQLNCLI11) for the data provider which requires additional network client software to be installed. For more information, see [System Requirements for SQL Server Native Client](../../../relational-databases/native-client/system-requirements-for-sql-server-native-client.md).  
  
```  
Option Explicit  
  
Sub Main()  
  Dim oTestStream As New ADODB.Stream  
  Dim oTestConnection As New ADODB.Connection  
  Dim oTestCommand As New ADODB.Command  
  oTestConnection.Open "Provider=SQLXMLOLEDB.4.0;Data Provider=SQLNCLI11;Data Source=SqlServerName;Initial Catalog=AdventureWorks;Integrated Security=SSPI;"  
  
  Set oTestCommand.ActiveConnection = oTestConnection  
  oTestCommand.Properties("ClientSideXML") = True  
  oTestCommand.CommandText = "<ROOT xmlns:sql='urn:schemas-microsoft-com:xml-sql'> " & _  
        " <sql:query> " & _  
        "   SELECT TOP 10 FirstName, LastName FROM Person.Contact FOR XML AUTO " & _  
        "   </sql:query> " & _  
        " </ROOT> "  
  oTestStream.Open  
  ' You need the dialect if you are executing   
  ' XML templates (not for SQL queries).  
  oTestCommand.Dialect = "{5d531cb2-e6ed-11d2-b252-00c04f681b71}"  
  oTestCommand.Properties("Output Stream").Value = oTestStream  
  oTestCommand.Execute , , adExecuteStream  
  
  oTestStream.Position = 0  
  oTestStream.Charset = "utf-8"  
  Debug.Print oTestStream.ReadText(adReadAll)  
End Sub  
  
Sub Form_Load()  
  Main  
End Sub  
```  
  
  
