---
title: "sp_unprepare (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_cursor_unprepare_TSQL"
  - "sp_cursor_unprepare"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_unprepare"
ms.assetid: 14320251-c551-49d8-b933-057406114978
caps.latest.revision: 15
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sp_unprepare (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Discards the execution plan created by the sp_prepare stored procedure. sp_unprepare is invoked by specifying ID = 15 in a tabular data stream (TDS) packet.  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
sp_unprepare handle           
```  
  
## Arguments  
 *handle*  
 Is the *handle* value returned by sp_prepare.  
  
## Examples  
 The following example prepares, executes, and unprepares a simple statement.  
  
```  
Declare @P1 int;  
Exec sp_prepare @P1 output,   
    N'@P1 nvarchar(128), @P2 nvarchar(100)',  
    N'SELECT database_id, name FROM sys.databases WHERE name=@P1 AND state_desc = @P2';  
Exec sp_execute @P1, N'tempdb', N'ONLINE';  
EXEC sp_unprepare @P1;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example prepares, executes, and unprepares a simple statement.  
  
```  
Declare @P1 int;  
Exec sp_prepare @P1 output,   
    N'@P1 nvarchar(128), @P2 nvarchar(100)',  
    N'SELECT database_id, name FROM sys.databases WHERE name=@P1 AND state_desc = @P2';  
Exec sp_execute @P1, N'tempdb', N'ONLINE';  
EXEC sp_unprepare @P1;  
```  
  
## See Also  
 [sp_prepare &#40;Transact SQL&#41;](../../relational-databases/system-stored-procedures/sp-prepare-transact-sql.md)  
  
  

