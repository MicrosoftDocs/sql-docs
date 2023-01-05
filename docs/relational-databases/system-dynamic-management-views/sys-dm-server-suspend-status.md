---
title: "sys.dm_server_suspend_status (Transact-SQL)"
description: Reference documentation to explain sys.dm_server_suspend_status (Transact-SQL) dynamic management view.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "09/29/2022"
ms.service: sql
ms.subservice: system-objects
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
|**db_id**|**int**|ID of the database that is suspended for snapshot backup.|  
|**db_name**|**sysname**|Name of the database suspended for snapshot backup.|  
|**suspend_session_id**|**tinyint**|Identifies the session that suspended the database for snapshot backup.|  
|**suspend_time_ms**|**bigint**|Time elapsed (in milliseconds) since the database has been suspended for snapshot backup.|  
|**is_diff_map_cleared**|**bit**|`false` if the database has been suspended for snapshot backup in COPY_ONLY mode, `true` otherwise.|  
|**is_write_io_frozen**|**bit**|`true` if the write io on the database has been frozen when the database is suspended for snapshot backup, `false` otherwise.|  
  
## Permissions  

Principals must have the **VIEW SERVER STATE** permission.  
  
[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also

 [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
 [DATABASEPROPERTYEX (Transact-SQL)](../../t-sql/functions/databasepropertyex-transact-sql.md)
 [SERVERPROPERTY (Transact-SQL)](../../t-sql/functions/serverproperty-transact-sql.md)
