---
title: "log_shipping_secondary_databases (Transact-SQL)"
description: log_shipping_secondary_databases (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "log_shipping_secondary_databases_TSQL"
  - "log_shipping_secondary_databases"
helpviewer_keywords:
  - "log_shipping_secondary_databases system table"
dev_langs:
  - "TSQL"
ms.assetid: ba2374af-86b8-480c-a10c-51e7c4e3ae23
---
# log_shipping_secondary_databases (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Stores one record per secondary database in a log shipping configuration. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**secondary_database**|**sysname**|The name of the secondary database in the log shipping configuration.|  
|**secondary_id**|**uniqueidentifier**|The ID for the secondary server in the log shipping configuration.|  
|**restore_delay**|**int**|The amount of time, in minutes, that the secondary server will wait before restoring a given backup file. The default is 0 minutes.|  
|**restore_all**|**bit**|If set to 1, the secondary server will restore all available transaction log backups when the restore job runs. Otherwise, it stops after one file has been restored.|  
|**restore_mode**|**bit**|The restore mode for the secondary database.<br /><br /> 0 = Restore log with NORECOVERY.<br /><br /> 1 = Restore log with STANDBY.|  
|**disconnect_users**|**bit**|If set to 1, users will be disconnected from the secondary database when a restore operation is performed. The default = 0.|  
|**block_size**|**int**|The size, in bytes, that is used as the block size for the backup device.|  
|**buffer_count**|**int**|The total number of buffers used by the backup or restore operation.|  
|**max_transfer_size**|**int**|The size, in bytes, of the maximum input or output request which is issued by [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the backup device.|  
|**last_restored_file**|**nvarchar(500)**|The filename of the last backup file restored to the secondary database.|  
|**last_restored_date**|**datetime**|The time and date of the last restore operation on the secondary database.|  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [sp_add_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-log-shipping-secondary-database-transact-sql.md)   
 [sp_delete_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-secondary-database-transact-sql.md)   
 [sp_help_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-log-shipping-secondary-database-transact-sql.md)   
 [log_shipping_secondary &#40;Transact-SQL&#41;](../../relational-databases/system-tables/log-shipping-secondary-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
