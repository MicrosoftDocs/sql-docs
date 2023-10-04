---
title: "Using a Connection Object"
description: "Using a Connection Object"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "connections [ADO]"
---
# Using a Connection Object
Before opening a **Connection** object, you must define certain information about the data source and type of connection. Most of this information is held by the *ConnectionString* parameter of the [Open method](../../../ado/reference/ado-api/open-method-ado-connection.md) on the **Connection** object, or by the [ConnectionString property](../../../ado/reference/ado-api/connectionstring-property-ado.md) on the **Connection** object. A connection string consists of a list of argument/value pairs separated by semi-colons, with the values enclosed within single quotes. For example:  
  
```  
Dim sConn As String  
sConn = "Provider='SQLOLEDB';Data Source='MySqlServer';" & _  
             "Initial Catalog='Northwind';Integrated Security='SSPI';"  
```  
  
> [!NOTE]
>  You can also specify an ODBC Data Source Name (DSN) or a Data Link (UDL) file in a connection string. For more information about DSNs, see [Managing Data Sources](../../../odbc/admin/managing-data-sources.md) in the ODBC Programmer's Reference. For more information about UDLs, see [Data Link API Overview](/previous-versions/windows/desktop/ms718102(v=vs.85)) in the OLE DB Programmer's Reference.  
  
 Typically, you establish a connection by calling the **Connection.Open** method with an appropriate a *connection string* as its parameter. An example is shown in the following Visual Basic code snippet:  
  
```  
Dim oConn As ADODB.Connection  
Dim oRs As ADODB.Recordset  
Dim sConn As String  
Dim sSQL as String  
  
' Open a connection.  
Set oConn = New ADODB.Connection  
.Open   
  
' Make a query over the connection.  
sSQL = "SELECT ProductID, ProductName, CategoryID, UnitPrice " & _  
             "FROM Products"  
Set oRs = New ADODB.Recordset  
oRs.Open sSQL, , adOpenStatic, adLockBatchOptimistic, adCmdText  
  
MsgBox oRs.RecordCount  
  
' Close the connection.  
oConn.Close  
Set oConn = Nothing  
  
```  
  
 Here **oRs.Open** takes a **Connection** object (*oConn*) variable as the value of its *ActiveConnection* parameter. Also, the **Connection.CursorLocation** property assumes the default value of **adUseServer**. Contrast this to the [HelloData](../../../ado/guide/data/hellodata-a-simple-ado-application.md) example in the preceding section. The following instruction would result in run-time errors.  
  
```  
oRs.MarshalOptions = adMarshalModifiedOnly  
' Disconnect the Recordset.  
Set oRs.ActiveConnection = Nothing  
```