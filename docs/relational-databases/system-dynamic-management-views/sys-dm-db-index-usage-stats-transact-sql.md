---
title: "sys.dm_db_index_usage_stats (Transact-SQL)"
description: sys.dm_db_index_usage_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/12/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_db_index_usage_stats_TSQL"
  - "sys.dm_db_index_usage_stats"
  - "sys.dm_db_index_usage_stats_TSQL"
  - "dm_db_index_usage_stats"
helpviewer_keywords:
  - "sys.dm_db_index_usage_stats dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_index_usage_stats (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns counts of different types of index operations and the time each type of operation was last performed.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out.  
  
> [!NOTE]  
> The DMV `sys.dm_db_index_usage_stats` does not return information about memory-optimized indexes or spatial indexes. For information about memory-optimized index use, see [sys.dm_db_xtp_index_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-index-stats-transact-sql.md).  
  
> [!NOTE]  
>  To call this view from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use `sys.dm_pdw_nodes_db_index_usage_stats`. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**smallint**|ID of the database on which the table or view is defined.|  
|**object_id**|**int**|ID of the table or view on which the index is defined|  
|**index_id**|**int**|ID of the index.|  
|**user_seeks**|**bigint**|Number of seeks by user queries.|  
|**user_scans**|**bigint**|Number of scans by user queries that did not use 'seek' predicate.|  
|**user_lookups**|**bigint**|Number of bookmark lookups by user queries.|  
|**user_updates**|**bigint**|Number of updates by user queries. This includes Insert, Delete, and Updates representing number of operations done not the actual rows affected. For example, if you delete 1000 rows in one statement, this count increments by 1|  
|**last_user_seek**|**datetime**|Time of last user seek|  
|**last_user_scan**|**datetime**|Time of last user scan.|  
|**last_user_lookup**|**datetime**|Time of last user lookup.|  
|**last_user_update**|**datetime**|Time of last user update.|  
|**system_seeks**|**bigint**|Number of seeks by system queries.|  
|**system_scans**|**bigint**|Number of scans by system queries.|  
|**system_lookups**|**bigint**|Number of lookups by system queries.|  
|**system_updates**|**bigint**|Number of updates by system queries.|  
|**last_system_seek**|**datetime**|Time of last system seek.|  
|**last_system_scan**|**datetime**|Time of last system scan.|  
|**last_system_lookup**|**datetime**|Time of last system lookup.|  
|**last_system_update**|**datetime**|Time of last system update.|  
|pdw_node_id|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Remarks  
 Every individual seek, scan, lookup, or update on the specified index by one query execution is counted as a use of that index and increments the corresponding counter in this view. Information is reported both for operations caused by user-submitted queries, and for operations caused by internally generated queries, such as scans for gathering statistics.  
  
 The `user_updates` column is a counter of maintenance on the index caused by insert, update, or delete operations on the underlying table or view. You can use this view to determine which indexes are used only lightly by your applications. You can also use the view to determine which indexes are incurring maintenance overhead. You may want to consider dropping indexes that incur maintenance overhead, but are not used for queries, or are only infrequently used for queries.  
  
 The counters are initialized to empty whenever the database engine is started. Use the `sqlserver_start_time` column in [sys.dm_os_sys_info](sys-dm-os-sys-info-transact-sql.md) to find the last database engine startup time. In addition, whenever a database is detached or is shut down (for example, because AUTO_CLOSE is set to ON), all rows associated with the database are removed.  
  
 When an index is used, a row is added to `sys.dm_db_index_usage_stats` if a row does not already exist for the index. When the row is added, its counters are initially set to zero.  
  
 During upgrade to [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], entries in `sys.dm_db_index_usage_stats` are removed. Beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], entries are retained as they were prior to [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)].  
  
## Permissions  
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.  
  
## See Also  

 [Index Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/index-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)   
 [sys.dm_db_index_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.dm_os_sys_info  &#40;Transact-SQL&#41;](sys-dm-os-sys-info-transact-sql.md)    
 [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)    
