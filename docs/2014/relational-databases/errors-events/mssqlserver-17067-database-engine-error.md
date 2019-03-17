---
title: "MSSQLSERVER_17067 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "17067 (Database Engine error)"
ms.assetid: 32c1f0e8-db70-4836-95b2-8833be9e0ad1
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_17067
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|17067|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLASSERT_MESG|  
|Message Text|SQL Server Assertion: File: \<%s>, line = %d %s. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.|  
  
## Explanation  
 This error can be caused by transient, timing-related errors, or by in-memory or on-disk data corruption.  
  
## User Action  
 Rerun the statement that caused the exception to fire. If the error was caused by a timing-related event, the error may not recur. If the problem persists, run DBCC CHECKDB to check for on-disk corruption. Restart the server to ensure that the in-memory data structures are not corrupted.  
  
## See Also  
 [DBCC CHECKDB &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql)  
  
  
