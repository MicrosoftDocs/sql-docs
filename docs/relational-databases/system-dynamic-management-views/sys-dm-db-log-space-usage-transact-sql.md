---
title: "sys.dm_db_log_space_usage (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/29/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
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
caps.latest.revision: 4
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.dm_db_log_space_usage (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns space usage information for the database log. (All log files are combined.) 
  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id|**smallint**|Database ID.|  
|total_log_size_in_bytes |**bigint** |The size of the log  |
|used_log_space_in_bytes |**bigint** |The occupied size of the log  |     
|used_log_space_in_percent |**real** |The occupied size of the log as a percent of the total log size |
|log_space_in_bytes_since_last_backup |**bigint** |The amount of space used since the last log backup <br />**Applies to:** [!INCLUDE[sssql14-md](../../includes/sssql14-md.md)] through [!INCLUDE[sscurrent-md](../../includes/sscurrent-md.md)],  [!INCLUDE[ssSDS](../../includes/sssds-md.md)].|
    
  
## Permissions  
 On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires `VIEW SERVER STATE` permission on the server.  
  
 On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Premium Tiers requires the `VIEW DATABASE STATE` permission in the database. On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Standard and Basic Tiers requires the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] admin account.  
  
## Examples  
  
### A. Determing the Amount of Free Log Space in tempdb   
The following query returns the total free log space in megabytes (MB) available in tempdb.

```tsql
USE tempdb;  
GO  

SELECT (total_log_size_in_bytes - used_log_space_in_bytes*1.0/1024/1024) AS [free log space in MB]  
FROM sys.dm_db_log_space_usage;  
```
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)   
 [sys.dm_db_file_space_usage](../../relational-databases/system-dynamic-management-views/sys-dm-db-file-space-usage-transact-sql.md)    
 [sys.dm_db_task_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-task-space-usage-transact-sql.md)   
 [sys.dm_db_session_space_usage &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-session-space-usage-transact-sql.md)  
  
  



