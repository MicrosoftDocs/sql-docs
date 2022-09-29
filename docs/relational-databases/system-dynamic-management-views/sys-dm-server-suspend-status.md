---
title: "sys.dm_server_suspend_status (Transact-SQL)"
description: Reference documentation to explain sys.dm_server_suspend_status (Transact-SQL) dynamic management view.
author: sravanisaluru
ms.author: srsaluru
ms.date: "09/29/2022"
ms.prod: sql
ms.technology: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_server_suspend_status_TSQL"
  - "sys.dm_server_suspend_status"
  - "dm_server_suspend_status"
  - "sys.dm_server_suspend_status_TSQL"
helpviewer_keywords:
  - "sys.dm_server_suspend_status dynamic management view"
dev_langs:
  - "TSQL"
---

# sys.dm_server_suspend_status (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

Returns a row for each database in a suspended state. For more information, see [Create a Transact-SQL snapshot backup](../backup-restore/create-a-transact-sql-snapshot-backup.md). Introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**db_id**|**int**|ID of the database. Maps to the **database_id** field in the **sys.databases** catalog view|  
|**db_name**|**sysname**|Name of the database. Same as the **name** field in the **sys.databases** catalog view.|  
|**suspend_session_id**|**tinyint**|Identifies the session that initiated the suspension for the database.|  
|**suspend_time_ms**|**bigint**|Identifies the amount of time that the database has been suspended, in milliseconds.|  
|**is_diff_map_cleared**|**bit**||  
|**is_write_io_frozen**|**bit**||  
  
## Permissions  

Principals must have the **VIEW SERVER STATE** permission.  
  
[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also

 [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
 [ALTER SERVER CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-server-configuration-transact-sql.md)
