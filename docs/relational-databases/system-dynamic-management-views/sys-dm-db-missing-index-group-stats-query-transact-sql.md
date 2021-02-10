---
description: "sys.dm_db_missing_index_group_stats_query (Transact-SQL)"
title: "sys.dm_db_missing_index_group_stats_query (Transact-SQL)"
ms.custom: ""
ms.date: "02/09/2021"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.dm_db_missing_index_group_stats_query_TSQL"
  - "sys.dm_db_missing_index_group_stats_query"
  - "dm_db_missing_index_group_stats_query_TSQL"
  - "dm_db_missing_index_group_stats_query"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_missing_index_group_stats_query dynamic management view"
  - "missing indexes feature [SQL Server], sys.dm_db_missing_index_group_stats_query dynamic management view"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# sys.dm_db_missing_index_group_stats_query (Transact-SQL)
[!INCLUDE [SQL Server 2019 SQL Database](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi.md)]

  Returns information about queries that needed a missing index from groups of missing indexes, excluding spatial indexes. More than one query may be returned per missing index group. One missing index group may have several queries that needed the same index.
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out.  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_handle**|**int**|Identifies a group of missing indexes. This identifier is unique across the server.<br /><br /> The other columns provide information about all queries for which the index in the group is considered missing.<br /><br /> An index group contains only one index.|  
|**query_hash**|**Binary(8)**|Binary hash value calculated on the query and used to identify queries with similar logic. You can use the query hash to determine the aggregate resource usage for queries that differ only by literal values.|  
|**query_plan_hash**|**binary(8)**|Binary hash value calculated on the query execution plan and used to identify similar query execution plans. You can use query plan hash to find the cumulative cost of queries with similar execution plans.<br /><br /> Will always be 0x000 when a natively compiled stored procedure queries a memory-optimized table.|  
|**last_sql_handle**| |SQL handle of the last compiled statement that needed this index.|
|**last_statement_start_offset**| |Beginning of the offset of the last compiled statement that needed this index in its SQL batch.|
|**last_statement_end_offset**| |End of the offset of the last compiled statement that needed this index in its SQL batch.|
|**last_statement_sql_handle**| |SQL handle of the last compiled statement that needed this index.<BR>If Query Store was not enabled when the query was compiled, returns 0.|
| **\<inherited columns\>** | | Remaining columns are inherited from [sys.dm_db_missing_index_group_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-transact-sql.md). |
  
## Remarks  
 Information returned by **sys.dm_db_missing_index_group_stats_query** is updated by every query execution, not by every query compilation or recompilation. Usage statistics are not persisted and are kept only until [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is restarted. Database administrators should periodically make backup copies of the missing index information if they want to keep the usage statistics after server recycling.  
 
 This DMV inherits most columns from [sys.dm_db_missing_index_group_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-transact-sql.md), with the exception of **unique_compiles**.

  >[!NOTE]
  >The result set for this DMV is limited to 600 rows. Each row contains one missing index. If you have more than 600 missing indexes, you should address the existing missing indexes so you can then view the newer ones.

## Permissions  
 To query this dynamic management view, users must be granted the VIEW SERVER STATE permission or any permission that implies the VIEW SERVER STATE permission.  
  
## Examples  
 The following examples illustrate how to use the **sys.dm_db_missing_index_group_stats_query** dynamic management view.  
  
  
### A. Find the latest query text for the top 10 highest anticipated improvement for user queries 
 The following query returns the last recorded query text for the 10 missing indexes that would produce the highest anticipated cumulative improvement, in descending order.  

```sql
SELECT TOP 10 
    SUBSTRING
    (
            sql_text.text,
            misq.last_statement_start_offset / 2 + 1,
            (
            CASE misq.last_statement_Start_offset
                WHEN -1 THEN datalength(sql_text.text)
                ELSE misq.last_statement_end_offset
            END - misq.last_statement_start_offset
            ) / 2 + 1
    ),
    misq.*
FROM sys.dm_db_missing_index_group_stats_query misq
CROSS APPLY sys.dm_exec_sql_text(last_sql_handle) sql_text
ORDER BY avg_total_user_cost * avg_user_impact * (user_seeks + user_scans) DESC; 
```
  
## See Also  
 [sys.dm_db_missing_index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-columns-transact-sql.md)   
 [sys.dm_db_missing_index_details &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-details-transact-sql.md)   
 [sys.dm_db_missing_index_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-groups-transact-sql.md)   
 [sys.dm_db_missing_index_group_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-transact-sql.md)   
 [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)  
