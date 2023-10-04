---
title: "Current Record and Size of Recordset"
description: "Current Record and Size of Recordset"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "record location [ADO]"
  - "current record [ADO]"
---
# Current Record and Size of Recordset
This section describes how to locate the current position of the cursor in the sample **Recordset** in [JScript Code Example to Return a Recordset](./jscript-code-example-to-return-a-recordset.md).  
  
## Current Record  
 The current record in the dataset corresponds to that pointed by the position of the cursor of the **Recordset** object. When a **Recordset** object is returned from the data source as the result of calling **Recordset.Open**, **Command.Execute**, or **Connection.Execute** (including **Connection.NamedCommand** and **Connection.StoredProcedure**), the cursor is set to point at the first record. In the sample dataset, the initial current record is the "Uncle Bob's Organic Dried Pears" item.  
  
## Size of Recordset  
 To find out the size of a **Recordset** object, get the value of the **Recordset.RecordCount** property. This value is a long integer that indicates the number of records in the **Recordset**. If the dataset is returned from the OLEDB Provider for Microsoft SQL Server, this value gives the number of rows returned. Reading the **RecordCount** property on a closed **Recordset** causes an error.  
  
 If the number of records cannot be determined, the value of the property is -1.  
  
 The value of the **RecordCount** property also depends on the capabilities of the provider and the type of cursor used. For a forward-only cursor, the value is -1. For a static or keyset cursor, the value is the actual number of records returned in the **Recordset** object. For a dynamic cursor, the value is either -1 or the actual number of records, depending on the data source.  
  
 A cursor that supports **Recordcount** must work harder, and therefore requires more computing power, than a cursor does not support **Recordcount**. If you do not need to know the number of records, using different cursor type might help improve your application's performance, especially if you must deal with a large data set.  
  
 In some cases, a provider or cursor is unable to determine the **RecordCount** value without first fetching all records from the data source. To ensure accurate counting, call the **Recordset**.**MoveLast** method before calling **Recordset.RecordCount**.  
  
 The sample **Recordset** object obtained using the [JScript Code Example](./jscript-code-example-to-return-a-recordset.md) uses a forward-only cursor, so calling **RecordCount** on this object always results in -1. If you change the line of code that calls the **Recordset**.**Open** method as shown in the following example, the **RecordCount** property will return the actual number of records fetched.  
  
```  
oRs.Open sSQL, sCnStr, adOpenStatic, adLockOptimistic, adCmdText   
```  
  
 This is because static cursors with the [Microsoft OLE DB Provider for SQL Server](../appendixes/microsoft-ole-db-provider-for-sql-server.md) support **RecordCount**. In this example, there are five records and thus **RecordCount** should yield the value of 5.  
  
 This section contains the following topic.  
  
 [Boundaries of a Recordset](./boundaries-of-a-recordset.md)