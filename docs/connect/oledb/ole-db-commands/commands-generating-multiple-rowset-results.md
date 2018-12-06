---
title: "Commands Generating Multiple-Rowset Results | Microsoft Docs"
description: "Commands generating multiple-rowset results"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "multiple rowsets"
  - "rowsets [OLE DB], multiple"
  - "OLE DB Driver for SQL Server, commands"
  - "OLE DB Driver for SQL Server, multiple rowsets"
  - "commands [OLE DB]"
  - "multiple-rowset results"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Commands Generating Multiple-Rowset Results
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server can return multiple rowsets from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] statements. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] statements return multiple-rowset results under the following conditions:  
  
-   Batched SQL statements are submitted as a single command.  
  
-   Stored procedures implement a batch of SQL statements.  
  
## Batches  
 The OLE DB Driver for SQL Server recognizes the semicolon character as a batch delimiter for SQL statements:  
  
```  
WCHAR*       wSQLString = L"SELECT * FROM Categories; "  
                          L"SELECT * FROM Products";  
```  
  
 Sending multiple SQL statements in one batch is more efficient than executing each SQL statement separately. Sending one batch reduces the network round trips from the client to the server.  
  
## Stored Procedures  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] returns a result set for each statement in a stored procedure, so most [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] stored procedures return multiple result sets.  
  
## In This Section  
  
-   [Using IMultipleResults to Process Multiple Result Sets](../../oledb/ole-db-commands/using-imultipleresults-to-process-multiple-result-sets.md)  
  
## See Also  
 [Commands](../../oledb/ole-db-commands/commands.md)  
  
  
