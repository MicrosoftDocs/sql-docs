---
description: "sp_add_log_shipping_primary_database (Transact-SQL)"
title: "sp_add_log_shipping_primary_database (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_add_log_shipping_primary_database"
  - "sp_add_log_shipping_primary_database_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_log_shipping_primary_database"
ms.assetid: 69531611-113f-46b5-81a6-7bf496d0353c
author: MashaMSFT
ms.author: mathoma
---
# sp_add_log_shipping_primary_database (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Sets up the primary database for a log shipping configuration, including the backup job, local monitor record, and remote monitor record.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_log_shipping_primary_database [ @database = ] 'database',   
[ @backup_directory = ] 'backup_directory',   
[ @backup_share = ] 'backup_share',   
[ @backup_job_name = ] 'backup_job_name',   
[, [ @backup_retention_period = ] backup_retention_period]  
[, [ @monitor_server = ] 'monitor_server']  
[, [ @monitor_server_security_mode = ] monitor_server_security_mode]  
[, [ @monitor_server_login = ] 'monitor_server_login']  
[, [ @monitor_server_password = ] 'monitor_server_password']  
[, [ @backup_threshold = ] backup_threshold ]   
[, [ @threshold_alert = ] threshold_alert ]   
[, [ @threshold_alert_enabled = ] threshold_alert_enabled ]   
[, [ @history_retention_period = ] history_retention_period ]  
[, [ @backup_job_id = ] backup_job_id OUTPUT ]  
[, [ @primary_id = ] primary_id OUTPUT]  
[, [ @backup_compression = ] backup_compression_option ]  
  
```  
  
## Arguments  
`[ @database = ] 'database'`
 Is the name of the log shipping primary database. *database* is **sysname**, with no default, and cannot be NULL.  
  
`[ @backup_directory = ] 'backup_directory'`
 Is the path to the backup folder on the primary server. *backup_directory* is **nvarchar(500)**, with no default, and cannot be NULL.  
  
`[ @backup_share = ] 'backup_share'`
 Is the network path to the backup directory on the primary server. *backup_share* is **nvarchar(500)**, with no default, and cannot be NULL.  
  
`[ @backup_job_name = ] 'backup_job_name'`
 Is the name of the SQL Server Agent job on the primary server that copies the backup into the backup folder. *backup_job_name* is **sysname** and cannot be NULL.  
  
`[ @backup_retention_period = ] backup_retention_period`
 Is the length of time, in minutes, to retain the log backup file in the backup directory on the primary server. *backup_retention_period* is **int**, with no default, and cannot be NULL.  
  
`[ @monitor_server = ] 'monitor_server'`
 Is the name of the monitor server. *Monitor_server* is **sysname**, with no default, and cannot be NULL.  
  
`[ @monitor_server_security_mode = ] monitor_server_security_mode`
 The security mode used to connect to the monitor server.  
  
 1 = Windows Authentication.  
  
 0 = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. *monitor_server_security_mode* is **bit** and cannot be NULL.  
  
`[ @monitor_server_login = ] 'monitor_server_login'`
 Is the username of the account used to access the monitor server.  
  
`[ @monitor_server_password = ] 'monitor_server_password'`
 Is the password of the account used to access the monitor server.  
  
`[ @backup_threshold = ] backup_threshold`
 Is the length of time, in minutes, after the last backup before a *threshold_alert* error is raised. *backup_threshold* is **int**, with a default of 60 minutes.  
  
`[ @threshold_alert = ] threshold_alert`
 Is the alert to be raised when the backup threshold is exceeded. *threshold_alert* is **int**, with a default of 14,420.  
  
`[ @threshold_alert_enabled = ] threshold_alert_enabled`
 Specifies whether an alert will be raised when *backup_threshold* is exceeded. The value of zero (0), the default, means that the alert is disabled and will not be raised. *threshold_alert_enabled* is **bit**.  
  
`[ @history_retention_period = ] history_retention_period`
 Is the length of time in minutes in which the history will be retained. *history_retention_period* is **int**, with a default of NULL. A value of 14420 will be used if none is specified.  
  
`[ @backup_job_id = ] backup_job_id OUTPUT`
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job ID associated with the backup job on the primary server. *backup_job_id* is **uniqueidentifier** and cannot be NULL.  
  
`[ @primary_id = ] primary_id OUTPUT`
 The ID of the primary database for the log shipping configuration. *primary_id* is **uniqueidentifier** and cannot be NULL.  
  
`[ @backup_compression = ] backup_compression_option`
 Specifies whether a log shipping configuration uses [backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md). This parameter is supported only in [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] (or a later version).  
  
 0 = Disabled. Never compress log backups.  
  
 1 = Enabled. Always compress log backups.  
  
 2 = Use the setting of the [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md). This is the default value.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_add_log_shipping_primary_database** must be run from the **master** database on the primary server. This stored procedure performs the following functions:  
  
1.  Generates a primary ID and adds an entry for the primary database in the table **log_shipping_primary_databases** using the supplied arguments.  
  
2.  Creates a backup job for the primary database that is disabled.  
  
3.  Sets the backup job ID in the **log_shipping_primary_databases** entry to the job ID of the backup job.  
  
4.  Adds a local monitor record in the table **log_shipping_monitor_primary** on the primary server using supplied arguments.  
  
5.  If the monitor server is different from the primary server, adds a monitor record in **log_shipping_monitor_primary** on the monitor server using supplied arguments.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can run this procedure.  
  
## Examples  
 This example adds the database [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] as the primary database in a log shipping configuration.  
  
```  
DECLARE @LS_BackupJobId AS uniqueidentifier ;  
DECLARE @LS_PrimaryId AS uniqueidentifier ;  
  
EXEC master.dbo.sp_add_log_shipping_primary_database   
@database = N'AdventureWorks'   
,@backup_directory = N'c:\lsbackup'   
,@backup_share = N'\\tribeca\lsbackup'   
,@backup_job_name = N'LSBackup_AdventureWorks'   
,@backup_retention_period = 1440  
,@monitor_server = N'rockaway'   
,@monitor_server_security_mode = 1   
,@backup_threshold = 60   
,@threshold_alert = 0   
,@threshold_alert_enabled = 0   
,@history_retention_period = 1440   
,@backup_job_id = @LS_BackupJobId OUTPUT   
,@primary_id = @LS_PrimaryId OUTPUT   
,@overwrite = 1   
,@backup_compression = 0;  
GO  
```  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
