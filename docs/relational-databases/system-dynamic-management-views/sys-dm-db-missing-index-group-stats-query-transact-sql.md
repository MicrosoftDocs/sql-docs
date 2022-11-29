---
title: "sys.dm_db_missing_index_group_stats_query (Transact-SQL)"
description: The sys.dm_db_missing_index_group_stats_query dynamic management view returns information about queries that needed a missing index from groups of missing indexes.
author: rwestMSFT
ms.author: randolphwest
ms.date: "3/7/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_missing_index_group_stats_query_TSQL"
  - "sys.dm_db_missing_index_group_stats_query"
  - "dm_db_missing_index_group_stats_query_TSQL"
  - "dm_db_missing_index_group_stats_query"
helpviewer_keywords:
  - "sys.dm_db_missing_index_group_stats_query dynamic management view"
  - "missing indexes feature [SQL Server], sys.dm_db_missing_index_group_stats_query dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# sys.dm_db_missing_index_group_stats_query (Transact-SQL)
[!INCLUDE [SQL Server 2019 SQL Database](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi.md)]

  Returns information about queries that needed a missing index from groups of missing indexes, excluding spatial indexes. More than one query may be returned per missing index group. One missing index group may have several queries that needed the same index.
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out.  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_handle**|**int**|Identifies a group of missing indexes. This identifier is unique across the server.<br /><br /> The other columns provide information about all queries for which the index in the group is considered missing.<br /><br /> An index group contains only one index.<BR><BR>Can be joined to `index_group_handle` in [sys.dm_db_missing_index_groups](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-groups-transact-sql.md).|  
|**query_hash**|**binary(8)**|Binary hash value calculated on the query and used to identify queries with similar logic. You can use the query hash to determine the aggregate resource usage for queries that differ only by literal values.|  
|**query_plan_hash**|**binary(8)**|Binary hash value calculated on the query execution plan and used to identify similar query execution plans. You can use query plan hash to find the cumulative cost of queries with similar execution plans.<br /><br /> Will always be 0x000 when a natively compiled stored procedure queries a memory-optimized table.|  
|**last_sql_handle**|**varbinary(64)**|Is a token that uniquely identifies the batch or stored procedure of the last compiled statement that needed this index.<BR><BR>The `last_sql_handle` can be used to retrieve the SQL text of the query by calling the dynamic management function [sys.dm_exec_sql_text](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md).|
|**last_statement_start_offset**|**int**|Indicates, in bytes, beginning with 0, the starting position of the query that the row describes within the text of its batch or persisted object for the last compiled statement that needed this index in its SQL batch.|
|**last_statement_end_offset**|**int**|Indicates, in bytes, beginning with 0, the ending position of the query that the row describes within the text of its batch or persisted object for the last compiled statement that needed this index in its SQL batch.|
|**last_statement_sql_handle**|**varbinary(64)**|Is a token that uniquely identifies the batch or stored procedure of the last compiled statement that needed this index. Used by Query Store. Unlike `last_sql_handle`, `sys.query_store_query_text` references the `statement_sql_handle` used by the Query Store catalog view [sys.query_store_query_text](../system-catalog-views/sys-query-store-query-text-transact-sql.md).<BR><BR>If Query Store was not enabled when the query was compiled, returns 0.|
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
 Information returned by `sys.dm_db_missing_index_group_stats_query` is updated by every query execution, not by every query compilation or recompilation. Usage statistics are not persisted and are kept only until the database engine is restarted. 

Database administrators should periodically make backup copies of the missing index information if they want to keep the usage statistics after server recycling. Use the `sqlserver_start_time` column in [sys.dm_os_sys_info](sys-dm-os-sys-info-transact-sql.md) to find the last database engine startup time. You can also [persist missing indexes with Query Store](../indexes/tune-nonclustered-missing-index-suggestions.md#persist-missing-indexes-with-query-store).
 
  >[!NOTE]
  >The result set for this DMV is limited to 600 rows. Each row contains one missing index. If you have more than 600 missing indexes, you should address the existing missing indexes so you can then view the newer ones.

## Permissions  
 To query this dynamic management view, users must be granted the VIEW SERVER STATE permission or any permission that implies the VIEW SERVER STATE permission.  
  
## Examples  
 The following examples illustrate how to use the `sys.dm_db_missing_index_group_stats_query` dynamic management view.  
  
  
### A. Find the latest query text for the top 10 highest anticipated improvements for user queries 
 The following query returns the last recorded query text for the 10 missing indexes that would produce the highest anticipated cumulative improvement, in descending order.  

```sql
SELECT TOP 10 
    SUBSTRING
    (
            sql_text.text,
            misq.last_statement_start_offset / 2 + 1,
            (
            CASE misq.last_statement_start_offset
                WHEN -1 THEN DATALENGTH(sql_text.text)
                ELSE misq.last_statement_end_offset
            END - misq.last_statement_start_offset
            ) / 2 + 1
    ),
    misq.*
FROM sys.dm_db_missing_index_group_stats_query AS misq
CROSS APPLY sys.dm_exec_sql_text(misq.last_sql_handle) AS sql_text
ORDER BY misq.avg_total_user_cost * misq.avg_user_impact * (misq.user_seeks + misq.user_scans) DESC; 
```
  
## Next steps

Learn more about the missing index feature and related concepts in the following articles:

- [Tune nonclustered indexes with missing index suggestions](../indexes/tune-nonclustered-missing-index-suggestions.md)
- [sys.dm_db_missing_index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-columns-transact-sql.md)
- [sys.dm_db_missing_index_details &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-details-transact-sql.md)
- [sys.dm_db_missing_index_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-groups-transact-sql.md)
- [sys.dm_db_missing_index_group_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-transact-sql.md)
- [sys.dm_exec_sql_text &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)
- [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)  
- [sys.dm_os_sys_info  &#40;Transact-SQL&#41;](sys-dm-os-sys-info-transact-sql.md)
- [Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
