---
description: "sp_help_log_shipping_primary_database (Transact-SQL)"
title: "sp_help_log_shipping_primary_database (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_help_log_shipping_primary_database_TSQL"
  - "sp_help_log_shipping_primary_database"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_log_shipping_primary_database"
ms.assetid: e711b01c-ef29-4eb6-a016-0e647e337818
author: MashaMSFT
ms.author: mathoma
---
# sp_help_log_shipping_primary_database (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Retrieves primary database settings.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_log_shipping_primary_database  
[ @database = ] 'database' OR  
[ @primary_id = ] 'primary_id'  
```  
  
## Arguments  
`[ @database = ] 'database'`
 Is the name of the log shipping primary database. *database* is **sysname**, with no default, and cannot be NULL.  
  
`[ @primary_id = ] 'primary_id'`
 The ID of the primary database for the log shipping configuration. *primary_id* is **uniqueidentifier** and cannot be NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Description|  
|-----------------|-----------------|  
|**primary_id**|The ID of the primary database for the log shipping configuration.|  
|**primary_database**|The name of the primary database in the log shipping configuration.|  
|**backup_directory**|The directory where transaction log backup files from the primary server are stored.|  
|**backup_share**|The network or UNC path to the backup directory.|  
|**backup_retention_period**|The length of time, in minutes, that a log backup file is retained in the backup directory before being deleted.|  
|**backup_compression**|Indicates whether the log shipping configuration uses [backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md).<br /><br /> **0** = Disabled. Never compress log backups.<br /><br /> **1** = Enabled. Always compress log backups.<br /><br /> **2** = Use the setting of the [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md). This is the default value.<br /><br /> Backup compression is supported only in [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] (or a later version). In other editions, the value is always 2.|  
|**backup_job_id**|The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job ID associated with the backup job on the primary server.|  
|**monitor_server**|The name of the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] being used as a monitor server in the log shipping configuration.|  
|**monitor_server_security_mode**|The security mode used to connect to the monitor server.<br /><br /> 1 = [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.<br /><br /> 0 = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.|  
|**backup_threshold**|The number of minutes allowed to elapse between backup operations before an alert is generated.|  
|**threshold_alert**|The alert to be raised when the backup threshold is exceeded.|  
|**threshold_alert_enabled**|Determines if backup threshold alerts are enabled.<br /><br /> **1** = Enabled.<br /><br /> **0** = Disabled.|  
|**last_backup_file**|The absolute path of the most recent transaction log backup.|  
|**last_backup_date**|The time and date of the last log backup operation.|  
|**last_backup_date_utc**|The time and date of the last transaction log backup operation on the primary database, expressed in Coordinated Universal Time.|  
|**history_retention_period**|The amount of time, in minutes, that log shipping history records are retained for a given primary database before being deleted.|  
  
## Remarks  
 **sp_help_log_shipping_primary_database** must be run from the **master** database on the primary server.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can run this procedure.  
  
## Examples  
 This example illustrates using **sp_help_log_shipping_primary_database** to retrieve primary database settings for the database [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)].  
  
```  
EXEC master.dbo.sp_help_log_shipping_primary_database @database=N'AdventureWorks2012';  
GO  
```  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
