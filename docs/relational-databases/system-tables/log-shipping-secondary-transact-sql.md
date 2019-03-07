---
title: "log_shipping_secondary (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "log_shipping_secondary"
  - "log_shipping_secondary_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "log_shipping_secondary system table"
ms.assetid: 69723419-4544-49c6-a517-adb30ffa5741
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# log_shipping_secondary (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Stores one record per secondary ID. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**secondary_id**|**uniqueidentifier**|The ID for the secondary server in the log shipping configuration.|  
|**primary_server**|**sysname**|The name of the primary instance of the SQL Server Database Engine in the log shipping configuration.|  
|**primary_database**|**sysname**|The name of the primary database in the log shipping configuration.|  
|**backup_source_directory**|**nvarchar(500)**|The directory where transaction log backup files from the primary server are stored.|  
|**backup_destination_directory**|**nvarchar(500)**|The directory on the secondary server where backup files are copied to.|  
|**file_retention_period**|**int**|The length of time, in minutes, that a backup file is retained on the secondary server before being deleted.|  
|**copy_job_id**|**uniqueidentifier**|The ID associated with the copy job on the secondary server.|  
|**restore_job_id**|**uniqueidentifier**|The ID associated with the restore job on the secondary server.|  
|**monitor_server**|**sysname**|The name of the instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] being used as a monitor server in the log shipping configuration.|  
|**monitor_server_security_mode**|**bit**|The security mode used to connect to the monitor server.<br /><br /> 1 = Windows Authentication.<br /><br /> 0 = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**last_copied_file**|**nvarchar(500)**|The filename of the last backup file copied to the secondary server.|  
|**last_copied_date**|**datetime**|The time and date of the last copy operation to the secondary server.|  
  
## Remarks  
 Multiple secondary databases on the same secondary server for a given primary database share some settings in the **log_shipping_secondary** table. If a shared setting is altered for one of them, the setting is altered for all of them.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [sp_add_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-log-shipping-secondary-database-transact-sql.md)   
 [sp_change_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-log-shipping-secondary-database-transact-sql.md)   
 [sp_delete_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-secondary-database-transact-sql.md)   
 [sp_help_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-log-shipping-secondary-database-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
