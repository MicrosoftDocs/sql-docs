---
title: "sys.dm_db_missing_index_columns (Transact-SQL)"
description: The sys.dm_db_missing_index_columns dynamic management function returns information about database table columns that are missing an index.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/8/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_db_missing_index_columns_TSQL"
  - "sys.dm_db_missing_index_columns_TSQL"
  - "sys.dm_db_missing_index_columns"
  - "dm_db_missing_index_columns"
helpviewer_keywords:
  - "sys.dm_db_missing_index_columns dynamic management function"
  - "missing indexes feature [SQL Server], sys.dm_db_missing_index_columns dynamic management function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_missing_index_columns (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns information about database table columns that are missing an index. `sys.dm_db_missing_index_columns` is a dynamic management function.  


## Syntax  
  
```syntaxsql  
sys.dm_db_missing_index_columns(index_handle)  
```  
  
## Arguments  
 *index_handle*  
 An integer that uniquely identifies a missing index. It can be obtained from the following dynamic management objects:  
  
 [sys.dm_db_missing_index_details &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-details-transact-sql.md)  
  
 [sys.dm_db_missing_index_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-groups-transact-sql.md)  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**column_id**|**int**|ID of the column.|  
|**column_name**|**sysname**|Name of the table column.|  
|**column_usage**|**varchar(20)**|How the column is used by the query. The possible values and their descriptions are:<br /><br /> EQUALITY: Column contributes to a predicate that expresses equality, of the form: <br />                        *table.column* = *constant_value*<br /><br /> INEQUALITY: Column contributes to a predicate that expresses inequality, for example, a predicate of the form: *table.column* > *constant_value*. Any comparison operator other than "=" expresses inequality.<br /><br /> INCLUDE: Column is not used to evaluate a predicate, but is used for another reason, for example, to cover a query.|  
  
## Remarks  
 Information returned by `sys.dm_db_missing_index_columns` is updated when a query is optimized by the query optimizer, and is not persisted. Missing index information is kept only until the database engine is restarted. Database administrators should periodically make backup copies of the missing index information if they want to keep it after server recycling. Use the `sqlserver_start_time` column in [sys.dm_os_sys_info](sys-dm-os-sys-info-transact-sql.md) to find the last database engine startup time.   

  
## Transaction Consistency  
 If a transaction creates or drops a table, the rows containing missing index information about the dropped objects are removed from this dynamic management object, preserving transaction consistency.  
  
## Permissions  
 Users must be granted the VIEW SERVER STATE permission or any permission that implies the VIEW SERVER STATE permission to query this dynamic management function.  
  
## Examples  
 The following example runs a query against the `Address` table and then runs a query using the `sys.dm_db_missing_index_columns` dynamic management view to return the table columns that are missing an index.  
  
```sql
USE AdventureWorks2012;  
GO  
SELECT City, StateProvinceID, PostalCode  
FROM Person.Address  
WHERE StateProvinceID = 9;  
GO  
SELECT mig.*, statement AS table_name,  
    column_id, column_name, column_usage  
FROM sys.dm_db_missing_index_details AS mid  
CROSS APPLY sys.dm_db_missing_index_columns (mid.index_handle)  
INNER JOIN sys.dm_db_missing_index_groups AS mig ON mig.index_handle = mid.index_handle  
ORDER BY mig.index_group_handle, mig.index_handle, column_id;  
GO  
```

Missing index suggestions should be combined when possible with one another, and with existing indexes in the current database. Learn how to apply these suggestions in [tune nonclustered indexes with missing index suggestions](../indexes/tune-nonclustered-missing-index-suggestions.md).


## Next steps

Learn more about the missing index feature in the following articles:

- [Tune nonclustered indexes with missing index suggestions](../indexes/tune-nonclustered-missing-index-suggestions.md)
- [sys.dm_db_missing_index_details &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-details-transact-sql.md)   
- [sys.dm_db_missing_index_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-groups-transact-sql.md)   
- [sys.dm_db_missing_index_group_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-transact-sql.md)  
- [sys.dm_db_missing_index_group_stats_query &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-query-transact-sql.md)     
- [sys.dm_os_sys_info  &#40;Transact-SQL&#41;](sys-dm-os-sys-info-transact-sql.md) 
