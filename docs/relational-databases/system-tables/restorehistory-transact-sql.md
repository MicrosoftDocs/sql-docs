---
title: "restorehistory (Transact-SQL)"
description: restorehistory (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "restorehistory"
  - "restorehistory_TSQL"
helpviewer_keywords:
  - "restorehistory system table"
dev_langs:
  - "TSQL"
ms.assetid: 9140ecc1-d912-4d76-ae70-e2a857da6d44
---
# restorehistory (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each restore operation. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**restore_history_id**|**int**|Unique identification number that identifies each restore operation. Identity, primary key.|  
|**restore_date**|**datetime**|Date and time of the start of the restore operation. Can be NULL.|  
|**destination_database_name**|**nvarchar(128)**|Name of the destination database for the restore operation. Can be NULL.|  
|**user_name**|**nvarchar(128)**|Name of the user who performed the restore operation. Can be NULL.|  
|**backup_set_id**|**int**|Unique identification number identifying the backup set being restored. References **backupset(backup_set_id)**.|  
|**restore_type**|**char(1)**|Type of restore operation:<br /><br /> D = Database<br /><br /> F = File<br /><br /> G = Filegroup<br /><br /> I = Differential<br /><br /> L = Log<br /><br /> V = Verifyonly<br /><br /> Can be NULL.|  
|**replace**|**bit**|Indicates whether the restore operation specified the REPLACE option:<br /><br /> 1 = Specified<br /><br /> 0 = Not specified<br /><br /> Can be NULL.<br /><br /> When a database is reverted to a database snapshot, 0 is the only option.|  
|**recovery**|**bit**|Indicates whether the restore operation specified the RECOVERY or NORECOVERY option:<br /><br /> 1 = RECOVERY<br /><br /> Can be NULL.<br /><br /> When a database is reverted to a database snapshot, 1 is the only option.<br /><br /> 0 = NORECOVERY|  
|**restart**|**bit**|Indicates whether the restore operation specified the RESTART option:<br /><br /> 1 = Specified<br /><br /> 0 = Not specified<br /><br /> Can be NULL.<br /><br /> When a database is reverted to a database snapshot, 0 is the only option.|  
|**stop_at**|**datetime**|Point in time to which the database was recovered. Can be NULL.|  
|**device_count**|**tinyint**|Number of devices involved in the restore operation. This number can be less than the number of media families for the backup. Can be NULL.<br /><br /> When a database is reverted to a database snapshot, the number is always 1.|  
|**stop_at_mark_name**|**nvarchar(128)**|Indicates recovery to the transaction containing the named mark. Can be NULL.<br /><br /> When a database is reverted to a database snapshot, this value is NULL.|  
|**stop_before**|**bit**|Indicates whether the transaction containing the named mark was included in the recovery:<br /><br /> 0 = Recovery halted before marked transaction.<br /><br /> 1 = Recovery included marked transaction.<br /><br /> Can be NULL.<br /><br /> When a database is reverted to a database snapshot, this value is NULL.|  
  
## Remarks  
 To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../../relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.  
  
## See Also  
 [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)   
 [restorefile &#40;Transact-SQL&#41;](../../relational-databases/system-tables/restorefile-transact-sql.md)   
 [restorefilegroup &#40;Transact-SQL&#41;](../../relational-databases/system-tables/restorefilegroup-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
