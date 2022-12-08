---
description: "sys.sp_xtp_checkpoint_force_garbage_collection (Transact-SQL)"
title: "sys.sp_xtp_checkpoint_force_garbage_collection (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/02/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.sp_xtp_checkpoint_force_garbage_collection_TSQL"
  - "sys.sp_xtp_checkpoint_force_garbage_collection"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_xtp_checkpoint_force_garbage_collection"
ms.assetid: 82b35b2b-edbd-44ac-9fc8-80695f2fd1df
author: markingmyname
ms.author: maghan
---
# sys.sp_xtp_checkpoint_force_garbage_collection (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Marks source files used in the merge operation with the log sequence number (LSN) after which they are not needed and can be garbage collected. Also, it moves the files whose associated LSN is lower than the log truncation point to filestream garbage collection.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
 
## Syntax  
  
```  
sys.sp_xtp_checkpoint_force_garbage_collection [ @dbname=database_name ]  
```  
  
## Arguments  
 *database_name*  
 The database to run garbage collection on. The default is the current database.  
  
## Return Code Values  
 0 for success. Nonzero for failure.  
  
## Result Set  
 A returned row contains the following information:  
  
|Column|Description|  
|------------|-----------------|  
|num_collected_items|Indicates the number of files that have been moved to filestream garbage collection. These are the files whose log sequence number (LSN) is less than the LSN of log truncation point|  
|num_marked_for_collection_items|Indicates the number of data/delta files whose LSN has been updated with the log blockID of the end-of-log LSN.|  
|last_collected_xact_seqno|Returns the last corresponding LSN up to which the files have been moved to filestream garbage collection.|  
  
## Permissions  
 Requires database owner permission.  
  
## Sample  
  
```  
exec [sys].[sp_xtp_checkpoint_force_garbage_collection] hkdb1  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md)  
  
