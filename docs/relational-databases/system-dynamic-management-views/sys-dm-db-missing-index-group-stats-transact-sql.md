---
title: "sys.dm_db_missing_index_group_stats (Transact-SQL)"
description: The sys.dm_db_missing_index_group_stats dynamic management view returns summary information about groups of missing indexes.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/8/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_missing_index_group_stats_TSQL"
  - "sys.dm_db_missing_index_group_stats"
  - "dm_db_missing_index_group_stats_TSQL"
  - "dm_db_missing_index_group_stats"
helpviewer_keywords:
  - "sys.dm_db_missing_index_group_stats dynamic management view"
  - "missing indexes feature [SQL Server], sys.dm_db_missing_index_group_stats dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_missing_index_group_stats (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns summary information about groups of missing indexes, excluding spatial indexes.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out.  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_handle**|**int**|Identifies a group of missing indexes. This identifier is unique across the server.<br /><br /> The other columns provide information about all queries for which the index in the group is considered missing.<br /><br /> An index group contains only one index.<BR><BR>Can be joined to `index_group_handle` in [sys.dm_db_missing_index_groups](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-groups-transact-sql.md).|  
|**unique_compiles**|**bigint**|Number of compilations and recompilations that would benefit from this missing index group. Compilations and recompilations of many different queries can contribute to this column value.|  
|**user_seeks**|**bigint**|Number of seeks caused by user queries that the recommended index in the group could have been used for.|  
|**user_scans**|**bigint**|Number of scans caused by user queries that the recommended index in the group could have been used for.|  
|**last_user_seek**|**datetime**|Date and time of last seek caused by user queries that the recommended index in the group could have been used for.|  
|**last_user_scan**|**datetime**|Date and time of last scan caused by user queries that the recommended index in the group could have been used for.|  
|**avg_total_user_cost**|**float**|Average cost of the user queries that could be reduced by the index in the group.|  
|**avg_user_impact**|**float**|Average percentage benefit that user queries could experience if this missing index group was implemented. The value means that the query cost would on average drop by this percentage if this missing index group was implemented.|  
|**system_seeks**|**bigint**|Number of seeks caused by system queries, such as auto stats queries, that the recommended index in the group could have been used for. For more information, see [Auto Stats Event Class](../../relational-databases/event-classes/auto-stats-event-class.md).|  
|**system_scans**|**bigint**|Number of scans caused by system queries that the recommended index in the group could have been used for.|  
|**last_system_seek**|**datetime**|Date and time of last system seek caused by system queries that the recommended index in the group could have been used for.|  
|**last_system_scan**|**datetime**|Date and time of last system scan caused by system queries that the recommended index in the group could have been used for.|  
|**avg_total_system_cost**|**float**|Average cost of the system queries that could be reduced by the index in the group.|  
|**avg_system_impact**|**float**|Average percentage benefit that system queries could experience if this missing index group was implemented. The value means that the query cost would on average drop by this percentage if this missing index group was implemented.|  
  
## Remarks  
 Information returned by `sys.dm_db_missing_index_group_stats` is updated by every query execution, not by every query compilation or recompilation. Usage statistics are not persisted and are kept only until the database engine is restarted. Database administrators should periodically make backup copies of the missing index information if they want to keep the usage statistics after server recycling. Use the `sqlserver_start_time` column in [sys.dm_os_sys_info](sys-dm-os-sys-info-transact-sql.md) to find the last database engine startup time.   

  >[!NOTE]
  >The result set for this DMV is limited to 600 rows. Each row contains one missing index. If you have more than 600 missing indexes, you should address the existing missing indexes so you can then view the newer ones.

 One missing index group may have several queries that needed the same index. For more information about individual queries that needed a specific index in this DMV, see [sys.dm_db_missing_index_group_stats_query](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-query-transact-sql.md).
  
## Permissions  
 To query this dynamic management view, users must be granted the VIEW SERVER STATE permission or any permission that implies the VIEW SERVER STATE permission.  
  
## Examples  
 The following examples illustrate how to use the `sys.dm_db_missing_index_group_stats` dynamic management view. Learn more about guidance for using missing indexes in [tune nonclustered indexes with missing index suggestions](../indexes/tune-nonclustered-missing-index-suggestions.md).
  
### A. Find the 10 missing indexes with the highest anticipated improvement for user queries  
 The following query determines which 10 missing indexes would produce the highest anticipated cumulative improvement, in descending order, for user queries.  
  
```sql
SELECT TOP 10 *  
FROM sys.dm_db_missing_index_group_stats  
ORDER BY avg_total_user_cost * avg_user_impact * (user_seeks + user_scans)DESC;  
```  
  
### B. Find the individual missing indexes and their column details for a particular missing index group  
 The following query determines which missing indexes comprise a particular missing index group, and displays their column details. For the sake of this example, the missing index `group_handle` is 24.  
  
```sql
SELECT migs.group_handle, mid.*  
FROM sys.dm_db_missing_index_group_stats AS migs  
INNER JOIN sys.dm_db_missing_index_groups AS mig  
    ON (migs.group_handle = mig.index_group_handle)  
INNER JOIN sys.dm_db_missing_index_details AS mid  
    ON (mig.index_handle = mid.index_handle)  
WHERE migs.group_handle = 24;  
```  
  
 This query provides the name of the database, schema, and table where an index is missing. It also provides the names of the columns that should be used for the index key. When writing the CREATE INDEX DDL statement to implement missing indexes, list equality columns first and then inequality columns in the ON \<*table_name*> clause of the CREATE INDEX statement. Included columns should be listed in the INCLUDE clause of the CREATE INDEX statement. To determine an effective order for the equality columns, order them based on their selectivity, listing the most selective columns first (leftmost in the column list). Learn how to [apply missing index suggestions](../indexes/tune-nonclustered-missing-index-suggestions.md#apply-missing-index-suggestions).
  
## Next steps

Learn more about the missing index feature in the following articles:

- [Tune nonclustered indexes with missing index suggestions](../indexes/tune-nonclustered-missing-index-suggestions.md)
- [sys.dm_db_missing_index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-columns-transact-sql.md)
- [sys.dm_db_missing_index_details &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-details-transact-sql.md)
- [sys.dm_db_missing_index_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-groups-transact-sql.md)
- [sys.dm_db_missing_index_group_stats_query &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-query-transact-sql.md)   
- [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md) 
- [sys.dm_os_sys_info  &#40;Transact-SQL&#41;](sys-dm-os-sys-info-transact-sql.md)  
  
