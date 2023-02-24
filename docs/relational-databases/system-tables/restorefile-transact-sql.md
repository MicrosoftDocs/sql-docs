---
title: "restorefile (Transact-SQL)"
description: restorefile (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "restorefile"
  - "restorefile_TSQL"
helpviewer_keywords:
  - "restorefile system table"
  - "restoring files [SQL Server], restorefile system table"
  - "file restores [SQL Server], restorefile system table"
dev_langs:
  - "TSQL"
ms.assetid: 8e40145a-8559-4abe-8e2a-39b818928009
---
# restorefile (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each restored file, including files restored indirectly by filegroup name. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**restore_history_id**|**int**|Unique identification number that identifies the corresponding restore operation. References **restorehistory(restore_history_id)**.|  
|**file_number**|**numeric(10,0)**|File identification number of the restored file. This number must be unique within each database. Can be NULL.<br /><br /> When a database is reverted to a database snapshot, this value is populated in the same way as for a full restore.|  
|**destination_phys_drive**|**nvarchar(260)**|Drive or partition to which the file was restored. Can be NULL.<br /><br /> When a database is reverted to a database snapshot, this value is populated in the same way as for a full restore.|  
|**destination_phys_name**|**nvarchar(260)**|Name of the file, without the drive or partition information, where the file was restored. Can be NULL.<br /><br /> When a database is reverted to a database snapshot, this value is populated in the same way as for a full restore.|  
  
## Remarks  
 To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../../relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.  
  
## See Also  
 [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)   
 [restorefilegroup &#40;Transact-SQL&#41;](../../relational-databases/system-tables/restorefilegroup-transact-sql.md)   
 [restorehistory &#40;Transact-SQL&#41;](../../relational-databases/system-tables/restorehistory-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
