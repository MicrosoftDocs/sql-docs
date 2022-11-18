---
title: "log_shipping_primary_databases (Transact-SQL)"
description: log_shipping_primary_databases (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "log_shipping_primary_databases"
  - "log_shipping_primary_databases_TSQL"
helpviewer_keywords:
  - "log_shipping_primary_databases system table"
dev_langs:
  - "TSQL"
ms.assetid: 56888756-a798-42be-9b5e-0f9aa05a2cc6
---
# log_shipping_primary_databases (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Stores one record for the primary database in a log shipping configuration. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**primary_id**|**uniqueidentifier**|The ID of the primary database for the log shipping configuration.|  
|**primary_database**|**sysname**|The name of the primary database in the log shipping configuration.|  
|**backup_directory**|**nvarchar(500)**|The directory where transaction log backup files from the primary server are stored.|  
|**backup_share**|**nvarchar(500)**|The network or UNC path to the backup directory.|  
|**backup_retention_period**|**int**|The length of time, in minutes, that a log backup file is retained in the backup directory before being deleted.|  
|**backup_job_id**|**uniqueidentifier**|The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job ID associated with the backup job on the primary server.|  
|**monitor_server**|**sysname**|The name of the instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] being used as a monitor server in the log shipping configuration.|  
|**monitor_server_security_mode**|**bit**|The security mode used to connect to the monitor server.<br /><br /> 1 = Windows Authentication.<br /><br /> 0 = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**last_backup_file**|**nvarchar(500)**|The absolute path of the most recent transaction log backup.|  
|**last_backup_date**|**datetime**|The time and date of the last log backup operation.|  
|**user_specified_monitor**|**bit**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]<br /><br /> **sp_help_log_shipping_primary_database** and **sp_help_log_shipping_secondary_primary** use this column to control the display of monitor settings in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].<br /><br /> 0 = When invoking either of these two stored procedures, the user did not specify an explicit value for the **\@monitor_server** parameter.<br /><br /> 1 = An explicit value was specified by the user.|  
|**backup_compression**|**tinyint**|Indicates whether the log shipping configuration overrides the server-level backup compression behavior.<br /><br /> 0 = Disabled. Log backups are never compressed, regardless of the server-configured backup compression settings.<br /><br /> 1 = Enabled. Log backups are always compressed, regardless of the server-configured backup compression settings.<br /><br /> 2 = Uses the server configuration for the [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md) server-configuration option. This is the default value.<br /><br /> Backup compression is supported only in the Enterprise edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [sp_add_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-log-shipping-primary-database-transact-sql.md)   
 [sp_delete_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-primary-database-transact-sql.md)   
 [sp_help_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-log-shipping-primary-database-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
