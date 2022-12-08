---
title: "sys.dm_db_missing_index_groups (Transact-SQL)"
description: The sys.dm_db_missing_index_groups dynamic management view returns information about indexes that are missing in a specific index group.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/8/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_missing_index_groups"
  - "sys.dm_db_missing_index_groups_TSQL"
  - "dm_db_missing_index_groups"
  - "dm_db_missing_index_groups_TSQL"
helpviewer_keywords:
  - "sys.dm_db_missing_index_groups dynamic management view"
  - "missing indexes feature [SQL Server], sys.dm_db_missing_index_groups dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_missing_index_groups (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  This DMV returns information about indexes that are missing in a specific index group. 

  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], dynamic management views cannot expose information that would impact database containment or expose information about other databases the user has access to. To avoid exposing this information, every row that contains data that doesn't belong to the connected tenant is filtered out.  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**index_group_handle**|**int**|Identifies a missing index group.|  
|**index_handle**|**int**|Identifies a missing index that belongs to the group specified by **index_group_handle**.<br /><br /> An index group contains only one index.|  
  
## Remarks  
 Information returned by `sys.dm_db_missing_index_groups` is updated when a query is optimized by the query optimizer, and is not persisted. Missing index information is kept only until the database engine is restarted. It may be useful for database administrators to periodically make backup copies of the missing index information if they want to keep it after server recycling. Use the `sqlserver_start_time` column in [sys.dm_os_sys_info](sys-dm-os-sys-info-transact-sql.md) to find the last database engine startup time.   
  
 Neither column of the output result set is a key, but together they form an index key.  

  >[!NOTE]
  >The result set for this DMV is limited to 600 rows. Each row contains one missing index. If you have more than 600 missing indexes, you should address the existing missing indexes so you can then view the newer ones.
  
## Permissions  
 To query this dynamic management view, users must be granted the VIEW SERVER STATE permission or any permission that implies the VIEW SERVER STATE permission.  
  
## Next steps

Learn more about the missing index feature in the following articles:

- [Tune nonclustered indexes with missing index suggestions](../indexes/tune-nonclustered-missing-index-suggestions.md)
- [sys.dm_db_missing_index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-columns-transact-sql.md)   
- [sys.dm_db_missing_index_details &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-details-transact-sql.md)   
- [sys.dm_db_missing_index_group_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-transact-sql.md)  
- [sys.dm_db_missing_index_group_stats_query &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-missing-index-group-stats-query-transact-sql.md)    
- [sys.dm_os_sys_info  &#40;Transact-SQL&#41;](sys-dm-os-sys-info-transact-sql.md)
