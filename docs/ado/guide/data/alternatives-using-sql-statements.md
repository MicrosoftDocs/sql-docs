---
title: "Alternatives: Using SQL Statements"
description: "Alternatives: Using SQL Statements"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "SQL statements [ADO]"
  - "editing data [ADO], sql statements"
  - "ADO, SQL statements"
---
# Alternatives: Using SQL Statements
ADO also allows using commands as alternatives to its built-in properties and methods for editing data. Depending upon your provider, all operations mentioned in this section could also be accomplished by passing commands to your data source. For example, SQL UPDATE statements can be used to modify data without using the **Value** property of a **Field**. SQL INSERT statements can be used to add new records to a data source, rather than the ADO method **AddNew**. For more information about SQL or the data-manipulation language of your provider, see the documentation of your data source.  
  
 For example, you can pass a SQL string containing a DELETE statement to a database, as shown in the following code:  
  
```  
'BeginSQLDelete  
strSQL = "DELETE FROM Shippers WHERE ShipperID = " & intId  
objConn1.Execute strSQL, , adCmdText + adExecuteNoRecords  
'EndSQLDelete  
```
