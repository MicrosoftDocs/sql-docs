---
description: "sp_unprepare (Transact-SQL)"
title: "sp_unprepare (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_cursor_unprepare_TSQL"
  - "sp_cursor_unprepare"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_unprepare"
ms.assetid: 14320251-c551-49d8-b933-057406114978
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_unprepare (Transact-SQL)
[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

  Discards the execution plan created by the sp_prepare stored procedure. sp_unprepare is invoked by specifying ID = 15 in a tabular data stream (TDS) packet.  
  
## Syntax  
  
```syntaxsql  
-- Syntax for SQL Server, Azure Synapse Analytics, Parallel Data Warehouse  
  
sp_unprepare handle           
```  
  
## Arguments  
 *handle*  
 Is the *handle* value returned by sp_prepare.  
  
## Examples  
 The following example prepares, executes, and unprepares a simple statement.  
  
```SQL  
DECLARE @P1 INT;  
EXEC sp_prepare @P1 OUTPUT,   
    N'@P1 NVARCHAR(128), @P2 NVARCHAR(100)',  
    N'SELECT database_id, name FROM sys.databases WHERE name = @P1 AND state_desc = @P2';  
EXEC sp_execute @P1, N'tempdb', N'ONLINE';  
EXEC sp_unprepare @P1;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example prepares, executes, and unprepares a simple statement.  
  
```SQL  
DECLARE @P1 INT;  
EXEC sp_prepare @P1 OUTPUT,   
    N'@P1 NVARCHAR(128), @P2 NVARCHAR(100)',  
    N'SELECT database_id, name FROM sys.databases WHERE name = @P1 AND state_desc = @P2';  
EXEC sp_execute @P1, N'tempdb', N'ONLINE';  
EXEC sp_unprepare @P1;  
```  
  
## See Also  
 [sp_prepare &#40;Transact SQL&#41;](../../relational-databases/system-stored-procedures/sp-prepare-transact-sql.md)   

