---
title: "MSSQLSERVER_2530 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
ms.assetid: 5d4be07a-38a5-4b25-819c-4dcb4636cc15
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2530
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2530|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_INDEX_IS_OFFLINE|  
|Message Text|The index "%.*ls" on table "%.\*ls" is disabled.|  
  
## Explanation  
 The DBCC statement cannot proceed because the specified index is disabled. After an index is disabled, it remains in a disabled state until it is rebuilt or dropped and re-created.  
  
## User Action  
  
1.  Enable the disabled index by using one of the following methods:  
  
    -   ALTER INDEX statement with the REBUILD clause  
  
    -   CREATE INDEX with the DROP_EXISTING clause  
  
    -   DBCC DBREINDEX  
  
2.  Rerun the DBCC statement.  
  
## See Also  
 [Enable Indexes and Constraints](../indexes/enable-indexes-and-constraints.md)   
 [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql)   
 [CREATE INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-index-transact-sql)   
 [DBCC DBREINDEX &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-dbreindex-transact-sql)  
  
  
