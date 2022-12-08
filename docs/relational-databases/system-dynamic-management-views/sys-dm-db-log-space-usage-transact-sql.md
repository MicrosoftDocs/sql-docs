---
title: "sys.dm_db_log_space_usage (Transact-SQL)"
description: "The sys.dm_db_log_space_usage dynamic management view returns space usage information for the transaction log."
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/20/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: conceptual
f1_keywords:
  - "sys.dm_db_log_space_usage"
  - "sys.dm_db_log_space_usage_TSQL"
  - "dm_db_log_space_usage"
  - "dm_db_log_space_usage_TSQL"
helpviewer_keywords:
  - "sys.dm_db_log_space_usage dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# sys.dm_db_log_space_usage (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns space usage information for the transaction log.
  
> [!NOTE]
> All transaction log files are combined.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id|**smallint**|Database ID.|  
|total_log_size_in_bytes |**bigint** |The size of the log  |
|used_log_space_in_bytes |**bigint** |The occupied size of the log  |
|used_log_space_in_percent |**real** |The occupied size of the log as a percent of the total log size |
|log_space_in_bytes_since_last_backup |**bigint** |The amount of space used since the last log backup <br />**Applies to:** [!INCLUDE[sssql14-md](../../includes/sssql14-md.md)] and later,  [!INCLUDE[ssSDS](../../includes/sssds-md.md)].|
    
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
  
## Examples  
  
### A. Determine the amount of free log space in tempdb   
The following query returns the total free log space in megabytes (MB) available in `tempdb`.

```sql
USE tempdb;  
GO  
SELECT 
(total_log_size_in_bytes - used_log_space_in_bytes)*1.0/1024/1024 AS [free log space in MB]  
FROM sys.dm_db_log_space_usage;  
```
  
## Next steps

- [Dynamic Management Views and Functions](system-dynamic-management-views.md)
- [Database Related Dynamic Management Views](database-related-dynamic-management-views-transact-sql.md)
- [sys.dm_db_file_space_usage](sys-dm-db-file-space-usage-transact-sql.md)
- [sys.dm_db_task_space_usage &#40;Transact-SQL&#41;](sys-dm-db-task-space-usage-transact-sql.md)   
- [sys.dm_db_session_space_usage &#40;Transact-SQL&#41;](sys-dm-db-session-space-usage-transact-sql.md)  
- [sys.dm_db_log_info &#40;Transact-SQL&#41;](sys-dm-db-log-info-transact-sql.md)    
- [sys.dm_db_log_stats &#40;Transact-SQL&#41;](sys-dm-db-log-stats-transact-sql.md)
