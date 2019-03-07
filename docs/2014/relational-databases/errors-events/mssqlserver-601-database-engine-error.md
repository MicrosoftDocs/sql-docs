---
title: "MSSQLSERVER_601 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
f1_keywords: 
  - "601"
helpviewer_keywords: 
  - "601 (Database Engine error)"
ms.assetid: 2039cc0a-9a43-4369-a04a-935e384388b6
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_601
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|601|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|Could not continue scan with NOLOCK due to data movement.|  
  
## Explanation  
 The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] cannot continue executing the query because it is trying to read data that was updated or deleted by another transaction. The query is using either the NOLOCK locking hint or the READ UNCOMMITTED transaction isolation level.  
  
 Typically, access to data that is being changed by another transaction is denied because of locks put on the data. However, the NOLOCK locking hint and READ UNCOMMITTED transaction isolation level let a query read data that is locked by another transaction. This is referred to as a dirty read because you can read values that have not yet been committed and that are subject to change.  
  
## User Action  
 This error cancels the query. Either resubmit the query or remove the NOLOCK locking hint.  
  
## See Also  
 [MSSQLSERVER_605](mssqlserver-605-database-engine-error.md)   
 [Table Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-table)   
 [SELECT &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-transact-sql)   
 [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-transaction-isolation-level-transact-sql)  
  
  
