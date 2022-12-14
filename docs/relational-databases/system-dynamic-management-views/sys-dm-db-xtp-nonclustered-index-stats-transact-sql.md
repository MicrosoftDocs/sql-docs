---
title: "sys.dm_db_xtp_nonclustered_index_stats (Transact-SQL)"
description: sys.dm_db_xtp_nonclustered_index_stats includes statistics about operations on nonclustered indexes in memory-optimized tables.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_db_xtp_nonclustered_index_stats_TSQL"
  - "dm_db_xtp_nonclustered_index_stats"
  - "sys.dm_db_xtp_nonclustered_index_stats_TSQL"
  - "sys.dm_db_xtp_nonclustered_index_stats"
helpviewer_keywords:
  - "sys.dm_db_xtp_nonclustered_index_stats dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_xtp_nonclustered_index_stats (Transact-SQL)
[!INCLUDE[sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  The `sys.dm_db_xtp_nonclustered_index_stats` system dynamic management view includes statistics about operations on nonclustered indexes in [memory-optimized tables](../in-memory-oltp/introduction-to-memory-optimized-tables.md). The result set contains one row for each nonclustered index on a memory-optimized table in the current database.  
  
 The statistics reflected in `sys.dm_db_xtp_nonclustered_index_stats` are collected when the in-memory index structure is created. In-memory index structures are recreated on database restart.  
  
 Use `sys.dm_db_xtp_nonclustered_index_stats` to understand and monitor index activity during DML operations and when a database is brought online. When a database with a memory-optimized table is restarted, the index is built by inserting one row at a time into memory. The count of page splits, merges, and consolidation can help you understand the work done to build the index when a database is brought online. You can also look at these counts before and after a series of DML operations.  
  
 Large numbers of retries are indicative of concurrency issues. 
  
 For more information about memory-optimized indexes, see [SQL Server [!INCLUDE[inmemory](../../includes/inmemory-md.md)] Internals for SQL Server 2016](../in-memory-oltp/sql-server-in-memory-oltp-internals-for-sql-server-2016.md), page 20.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object.|  
|xtp_object_id|**bigint**|ID of the memory-optimized table.|  
|index_id|**int**|ID of the index.|  
|delta_pages|**bigint**|The total number of delta pages for this index in the tree.|  
|internal_pages|**bigint**|For internal use. The total number of internal pages for this index in the tree.|  
|leaf_pages|**bigint**|The total number of leaf pages for this index in the tree.|  
|outstanding_retired_nodes|**bigint**|For internal use. The total number of nodes for this index in the internal structures.|  
|page_update_count|**bigint**|Cumulative number of operations updating a page in the index.|  
|page_update_retry_count|**bigint**|Cumulative number of retries of an operation updating page in the index.|  
|page_consolidation_count|**bigint**|Cumulative number of page consolidations in the index.|  
|page_consolidation_retry_count|**bigint**|Cumulative number of retries of page consolidation operations.|  
|page_split_count|**bigint**|Cumulative number of page split operations in the index.|  
|page_split_retry_count|**bigint**|Cumulative number of retries of page split operations.|  
|key_split_count|**bigint**|Cumulative number of key splits in the index.|  
|key_split_retry_count|**bigint**|Cumulative number of retries of key split operations.|  
|page_merge_count|**bigint**|Cumulative number of page merge operations in the index.|  
|page_merge_retry_count|**bigint**|Cumulative number of retries of page merge operations.|  
|key_merge_count|**bigint**|Cumulative number of key merge operations in the index.|  
|key_merge_retry_count|**bigint**|Cumulative number of retries of key merge operations.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the current database.  
  
## See also

- [Introduction to Memory-Optimized Tables](../in-memory-oltp/introduction-to-memory-optimized-tables.md)
- [Memory-Optimized Table Dynamic Management Views](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)

## Next steps 

- [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] Overview and Usage Scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
- [Optimize performance by using in-memory technologies in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/in-memory-oltp-overview)
