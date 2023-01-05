---
title: "sys.dm_xtp_transaction_stats (Transact-SQL)"
description: sys.dm_xtp_transaction_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/12/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_xtp_transaction_stats_TSQL"
  - "dm_xtp_transaction_stats"
  - "sys.dm_xtp_transaction_stats_TSQL"
  - "sys.dm_xtp_transaction_stats"
helpviewer_keywords:
  - "sys.dm_xtp_transaction_stats dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_xtp_transaction_stats (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Reports statistics about transactions that have run since the server started.  
  
 For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|total_count|**bigint**|The total number of transactions that have run in the In-Memory OLTP database engine.|  
|read_only_count|**bigint**|The number of read-only transactions.|  
|total_aborts|**bigint**|Total number of transactions that were aborted, either through user or system abort.|  
|system_aborts|**bigint**|Number of aborts initiated by the system. For example, because of write conflicts, validation failures, or dependency failures.|  
|validation_failures|**bigint**|The number of times a transaction has aborted due to a validation failure.|  
|dependencies_taken|**bigint**|Internal use only.|  
|dependencies_failed|**bigint**|The number of times a transaction aborts because a transaction on which it was dependent aborts.|  
|savepoint_create|**bigint**|The number of savepoints created. A new savepoint is created for every atomic block.|  
|savepoint_rollbacks|**bigint**|The number of rollbacks to a previous savepoint.|  
|savepoint_refreshes|**bigint**|Internal use only.|  
|log_bytes_written|**bigint**|Total number of bytes written to the In-Memory OLTP log records.|  
|log_IO_count|**bigint**|Total number of transactions that require log IO. Only considers transactions on durable tables.|  
|phantom_scans_started|**bigint**|Internal use only.|  
|phatom_scans_retries|**bigint**|Internal use only.|  
|phantom_rows_touched|**bigint**|Internal use only.|  
|phantom_rows_expiring|**bigint**|Internal use only.|  
|phantom_rows_expired|**bigint**|Internal use only.|  
|phantom_rows_expired_removed|**bigint**|Internal use only.|  
|scans_started|**bigint**|Internal use only.|  
|scans_retried|**bigint**|Internal use only.|  
|rows_returned|**bigint**|Internal use only.|  
|rows_touched|**bigint**|Internal use only.|  
|rows_expiring|**bigint**|Internal use only.|  
|rows_expired|**bigint**|Internal use only.|  
|rows_expired_removed|**bigint**|Internal use only.|  
|rows_inserted|**bigint**|Internal use only.|  
|rows_updated|**bigint**|Internal use only.|  
|rows_deleted|**bigint**|Internal use only.|  
|write_conflicts|**bigint**|Internal use only.|  
|unique_constraint_violations|**bigint**|Total number of unique constraint violations.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Memory-Optimized Table Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)  
  
