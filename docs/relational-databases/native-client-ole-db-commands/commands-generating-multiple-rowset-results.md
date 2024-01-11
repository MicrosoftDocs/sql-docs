---
title: "Commands generating multiple-rowset results (Native Client OLE DB provider)"
description: "Commands generating multiple-rowset results (Native Client OLE DB provider)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "multiple rowsets"
  - "rowsets [OLE DB], multiple"
  - "SQL Server Native Client OLE DB provider, commands"
  - "SQL Server Native Client OLE DB provider, multiple rowsets"
  - "commands [OLE DB]"
  - "multiple-rowset results"
---
# SQL Server Native Client Commands Generating Multiple-Rowset Results
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider can return multiple rowsets from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statements. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statements return multiple-rowset results under the following conditions:  
  
-   Batched SQL statements are submitted as a single command.  
  
-   Stored procedures implement a batch of SQL statements.  
  
## Batches  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider recognizes the semicolon character as a batch delimiter for SQL statements:  
  
```  
WCHAR*       wSQLString = L"SELECT * FROM Categories; "  
                          L"SELECT * FROM Products";  
```  
  
 Sending multiple SQL statements in one batch is more efficient than executing each SQL statement separately. Sending one batch reduces the network round trips from the client to the server.  
  
## Stored Procedures  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns a result set for each statement in a stored procedure, so most [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedures return multiple result sets.  
  
## In This Section  
  
-   [Using IMultipleResults to Process Multiple Result Sets](../../relational-databases/native-client-ole-db-commands/using-imultipleresults-to-process-multiple-result-sets.md)  
  
## See Also  
 [Commands](../../relational-databases/native-client-ole-db-commands/commands.md)  
  
  
