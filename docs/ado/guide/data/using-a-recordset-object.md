---
title: "Using a Recordset Object"
description: "Using a Recordset Object"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "connections [ADO]"
---
# Using a Recordset Object
Alternatively, you can use **Recordset.Open** to implicitly establish a connection and issue a command over that connection in a single operation. For example, in Visual Basic:  
  
```  
Dim oRs As ADODB.Recordset  
Dim sConn As String  
Dim sSQL as String  
  
sConn = "Provider='SQLOLEDB';Data Source='MySqlServer';" & _             "Initial Catalog='Northwind';Integrated Security='SSPI';"  
  
sSQL = "SELECT ProductID, ProductName, CategoryID, UnitPrice " & _  
             "FROM Products"  
  
' Create and Open the Recordset object.  
Set oRs = New ADODB.Recordset  
oRs.CursorLocation = adUseClient  
oRs.Open sSQL, sConn, adOpenStatic, _  
               adLockBatchOptimistic, adCmdText  
  
MsgBox oRs.RecordCount  
  
oRs.MarshalOptions = adMarshalModifiedOnly  
' Disconnect the Recordset.  
Set oRs.ActiveConnection = Nothing  
oRs.Close          
Set oRs = Nothing  
```  
  
 Notice that **oRs.Open** takes a connection string (*sConn*), in place of a **Connection** object (*oConn*), as the value of its **ActiveConnection** parameter. Also the client-side cursor type is enforced by setting the **CursorLocation** property on the **Recordset** object. Again, contrast this with the **HelloData** example.
