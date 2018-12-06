---
title: "MSSQL_REPL020011 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_REPL020011 error"
ms.assetid: f72072d7-bbb6-48ad-ac88-afa74aeb4d58
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_REPL020011
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|20011|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|The process could not execute '%1' on '%2'.|  
  
## Explanation  
 This error can be raised in a number of circumstances during transactional replication processing, such as when the Log Reader Agent executes **sp_replcmds** (The process could not execute 'sp_replcmds' on \<ServerName>) or **sp_repldone** (The process could not execute 'sp_repldone' on \<ServerName>).  
  
## User Action  
 If this error is raised in a database that you have just restored from a backup, ensure you have followed the steps outlined in the backup and restore documentation, including executing **sp_replrestart** if appropriate. For more information, see [Strategies for Backing Up and Restoring Snapshot and Transactional Replication](administration/strategies-for-backing-up-and-restoring-snapshot-and-transactional-replication.md).  
  
 This error is an internal processing error and if it is raised in circumstances other than a restore, it typically indicates that replication must be removed and reconfigured. If you cannot remove replication, contact customer support for assistance.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)   
 [sp_replcmds &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-replcmds-transact-sql)   
 [sp_repldone &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-repldone-transact-sql)  
  
  
