---
title: "sys.dm_db_log_space_usage (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "sys.dm_db_log_space_usage"
  - "sys.dm_db_log_space_usage_TSQL"
  - "dm_db_log_space_usage"
  - "dm_db_log_space_usage_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_log_space_usage dynamic management view"
ms.assetid: f6b40060-c17d-472f-b0a3-3b350275d487
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_log_space_usage (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

Returns space usage information for the transaction log. 
  
> [!NOTE]
> All transaction log files are combined.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id|**smallint**|Database ID.|  
|total_log_size_in_bytes |**bigint** |The size of the log  |
|used_log_space_in_bytes |**bigint** |The occupied size of the log  |     
|used_log_space_in_percent |**real** |The occupied size of the log as a percent of the total log size |
|log_space_in_bytes_since_last_backup |**bigint** |The amount of space used since the last log backup <br />**Applies to:** [!INCLUDE[sssql14-md](../../includes/sssql14-md.md)] through [!INCLUDE[sscurrent-md](../../includes/sscurrent-md.md)],  [!INCLUDE[ssSDS](../../includes/sssds-md.md)].|
    
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   
  
## Examples  
  
### A. Determine the Amount of Free Log Space in tempdb   
The following query returns the total free log space in megabytes (MB) available in tempdb.

```sql
USE tempdb;  
GO  

SELECT (total_log_size_in_bytes - used_log_space_in_bytes)*1.0/1024/1024 AS [free log space in MB]  
FROM sys.dm_db_log_space_usage;  
```
  
## See Also  
[Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
[Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)   
[sys.dm_db_file_space_usage](../../relational-databases/system-dynamic-management-views/sys-dm-db-file-space-usage-transact-sql.md)    
[sys.dm_db_task_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-task-space-usage-transact-sql.md)   
[sys.dm_db_session_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-session-space-usage-transact-sql.md)  
[sys.dm_db_log_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md)    
[sys.dm_db_log_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-log-stats-transact-sql.md) 



