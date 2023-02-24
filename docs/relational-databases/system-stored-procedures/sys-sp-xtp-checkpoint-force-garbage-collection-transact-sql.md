---
description: "sys.sp_xtp_checkpoint_force_garbage_collection (Transact-SQL)"
title: "sys.sp_xtp_checkpoint_force_garbage_collection (Transact-SQL)"
ms.custom: ""
ms.date: 02/24/2023
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
author: markingmyname
ms.author: maghan
---
# sys.sp_xtp_checkpoint_force_garbage_collection (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Marks source files used in the merge operation with the log sequence number (LSN) after which they are not needed and can be garbage collected. Also, `sys.sp_xtp_checkpoint_force_garbage_collection` moves the files whose associated LSN is lower than the log truncation point to filestream garbage collection.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

Contrast with [sys.sp_xtp_force_gc](sys-sp-xtp-force-gc-transact-sql.md), which causes the in-memory engine to release memory related to deleted rows of in-memory data which are eligible for garbage collection, which have not yet been released by the process.
 
## Syntax  
  
```syntaxsql  
sys.sp_xtp_checkpoint_force_garbage_collection [ @dbname=database_name ]  
```  
  
## Arguments  

#### *database_name*  
 The database to run garbage collection on. The default is the current database.  
  
## Return Code Values  
 0 for success. Nonzero for failure.  
  
## Result Set  
 A returned row contains the following information:  
  
|Column|Description|  
|------------|-----------------|  
|num_collected_items|Indicates the number of files that have been moved to filestream garbage collection. The log sequence number (LSN) of these files is less than the LSN of log truncation point.|  
|num_marked_for_collection_items|Indicates the number of data/delta files whose LSN has been updated with the log blockID of the end-of-log LSN.|  
|last_collected_xact_seqno|Returns the last corresponding LSN up to which the files have been moved to filestream garbage collection.|  
  
## Remarks

You can manually trigger garbage collection with another system stored procedure, `sys.sp_xtp_force_gc`. You can observe the reduction in memory cleanup in [sys.dm_xtp_system_memory_consumers](../system-dynamic-management-views/sys-dm-xtp-system-memory-consumers-transact-sql.md). 

In [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], the [sys.dm_xtp_system_memory_consumers](../../relational-databases/system-dynamic-management-views/sys-dm-xtp-system-memory-consumers-transact-sql.md) dynamic management view has improved insights specific to [memory-optimized tempdb metadata](../databases/tempdb-database.md#memory-optimized-tempdb-metadata). 

## Permissions  
 Requires database owner permission.  
  
## Example
  
To mark unneeded source files for garbage collection in the `tempdb` database, use the following sample script:

```sql  
exec [sys].[sp_xtp_checkpoint_force_garbage_collection] tempdb;
```  
  
## See also

- [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   

## Next steps

- [sys.sp_xtp_force_gc (Transact-SQL)](sys-sp-xtp-force-gc-transact-sql.md)
- [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md)  
- [sys.dm_xtp_system_memory_consumers](../../relational-databases/system-dynamic-management-views/sys-dm-xtp-system-memory-consumers-transact-sql.md)
  
