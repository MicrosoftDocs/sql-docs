---
title: "Executing a DiffGram by Using ADO (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "providers [SQLXML], SQLOLEDB Provider"
  - "ADO [SQLXML]"
  - "SQLXMLOLEDB Provider, DiffGrams"
  - "data providers [SQLXML], SQLOLEDB Provider"
  - "DiffGrams [SQLXML], ADO"
ms.assetid: 741fce82-de83-4923-86eb-30acb5b9a5e6
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Executing a DiffGram by Using ADO (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Basic application uses ADO to establish a connection to an instance of Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and then executes a DiffGram. In this application, the DiffGram and the XSD schema are stored in a file. The application loads the DiffGram from the specified file. You can use any of the DiffGrams (and the associated XSD schema) described in [DiffGram Examples](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/diffgram/diffgram-examples-sqlxml-4-0.md).  
  
 This is the process for the sample application:  
  
-   The **conn** object (**ADODB.Connection**) establishes a connection to a running instance of SQL Server on a specific server.  
  
-   The **cmd** object (**ADODB.Command**) executes on the established connection.  
  
-   The command dialect is set to DBGUID_MSSQLXML.  
  
-   The DiffGram is copied to the command stream (**strmIn**) from a file.  
  
-   The command's output stream is set to the **StrmOut** object (**ADODB.Stream**) to receive any returned data.  
  
-   When you are using the SQLOLEDB Provider, by default you will get the Microsoft SQLXML functionality provided by Sqlxmlx.dll. To use Sqlxml4.dll with the SQLOLEDB Provider, the **SQLXML Version** property must be set to **SQLXML.4.0** on the SQLOLEDB Provider **Connection** object.  
  
-   The command (DiffGram) is executed.  
  
 The following code is the sample application.  
  
> [!NOTE]  
>  In the code, you must provide the name of the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in the connection string.  
  
```  
Private Sub Command1_Click()  
  Dim cmd As New ADODB.Command  
  Dim conn As New ADODB.Connection  
  Dim strmOut As New ADODB.Stream  
  Dim strmIn As New ADODB.Stream  
  
  'Open a connection to SQL Server.  
  conn.Provider = "SQLOLEDB"  
  conn.Open "server=SqlServerName; database=tempdb; Integrated Security=SSPI; "  
  conn.Properties("SQLXML Version") = "SQLXML.4.0"  
  Set cmd.ActiveConnection = conn  
  strmIn.Open  
  strmIn.Charset = "UTF-8"  
  strmIn.LoadFromFile "C:\SomeFilePath\SampleDiffGram.xml"  
  strmIn.Position = 0  
  Set cmd.CommandStream = strmIn  
  
  strmOut.Open  
  cmd.Properties("Output Stream").Value = strmOut  
  cmd.Properties("Output Encoding").Value = "UTF-8"  
  
  cmd.Dialect = "{5d531cb2-e6ed-11d2-b252-00c04f681b71}"  
  cmd.Properties("Mapping Schema") = "C:\SomeFilePath\SampleDiffGram.xml"  
  cmd.Execute , , adExecuteStream  
  strmOut.Position = 0  
  Set cmd = Nothing  
  strmOut.Charset = "UTF-8"  
  strmOut.SaveToFile "C:\DropIt.txt", adSaveCreateOverWrite  
  strmOut.Close  
  Set strmOut = Nothing  
  
End Sub  
```  
  
### To test the DiffGram  
  
1.  To a folder on your computer, copy any one of the DiffGrams and the corresponding XSD schema from one of the examples in [DiffGram Examples](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/diffgram/diffgram-examples-sqlxml-4-0.md).  
  
2.  Open Visual Basic and create a Standard EXE project.  
  
3.  Add these references to the project:  
  
    ```  
    Microsoft ActiveX Data Objects 2.8 Library  
    ```  
  
4.  In the Toolbox, click **CommandButton**, and then draw a button on the form.  
  
5.  Double-click the button to edit the code, and add the application code that is provided in the topic.  
  
6.  Edit the code to specify the DiffGram and XSD file names. Also edit the connection string as appropriate.  
  
7.  Execute the application. The result of the execution depends on what DiffGram you are executing.  
  
  
