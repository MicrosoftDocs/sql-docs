---
title: "sp_change_log_shipping_primary_database (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_change_log_shipping_primary_database"
  - "sp_change_log_shipping_primary_database_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_change_log_shipping_primary_database"
ms.assetid: 8c9dce6b-d2a3-4ca7-a832-8f59a5adb214
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sp_change_log_shipping_primary_database (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the primary database settings.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_change_log_shipping_primary_database [ @database = ] 'database'  
[, [ @backup_directory = ] 'backup_directory']   
[, [ @backup_share = ] 'backup_share']   
[, [ @backup_retention_period = ] 'backup_retention_period']  
[, [ @monitor_server_security_mode = ] 'monitor_server_security_mode']  
[, [ @monitor_server_login = ] 'monitor_server_login']  
[, [ @monitor_server_password = ] 'monitor_server_password']  
[, [ @backup_threshold = ] 'backup_threshold']   
[, [ @threshold_alert = ] 'threshold_alert']   
[, [ @threshold_alert_enabled = ] 'threshold_alert_enabled']   
[, [ @history_retention_period = ] 'history_retention_period']  
[, [ @backup_compression = ] backup_compression_option ]   
```  
  
## Arguments  
`[ @database = ] 'database'`
 Is the name of the database on the primary server. *primary_database* is **sysname**, with no default.  
  
`[ @backup_directory = ] 'backup_directory'`
 Is the path to the backup folder on the primary server. *backup_directory* is **nvarchar(500)**, with no default, and cannot be NULL.  
  
`[ @backup_share = ] 'backup_share'`
 Is the network path to the backup directory on the primary server. *backup_share* is **nvarchar(500)**, with no default, and cannot be NULL.  
  
`[ @backup_retention_period = ] 'backup_retention_period'`
 Is the length of time, in minutes, to retain the log backup file in the backup directory on the primary server. *backup_retention_period* is **int**, with no default, and cannot be NULL.  
  
`[ @monitor_server_security_mode = ] 'monitor_server_security_mode'`
 The security mode used to connect to the monitor server.  
  
 1 = Windows Authentication.  
  
 0 = SQL Server Authentication.  
  
 *monitor_server_security_mode* is **bit** and cannot be NULL.  
  
`[ @monitor_server_login = ] 'monitor_server_login'`
 Is the username of the account used to access the monitor server.  
  
`[ @monitor_server_password = ] 'monitor_server_password'`
 Is the password of the account used to access the monitor server.  
  
`[ @backup_threshold = ] 'backup_threshold'`
 Is the length of time, in minutes, after the last backup before a *threshold_alert* error is raised. *backup_threshold* is **int**, with a default of 60 minutes.  
  
`[ @threshold_alert = ] 'threshold_alert'`
 The alert to be raised when the backup threshold is exceeded. *threshold_alert* is **int** and cannot be NULL.  
  
`[ @threshold_alert_enabled = ] 'threshold_alert_enabled'`
 Specifies whether an alert is raised when *backup_threshold* is exceeded.  
  
 1 = enabled.  
  
 0 = disabled.  
  
 *threshold_alert_enabled* is **bit** and cannot be NULL.  
  
`[ @history_retention_period = ] 'history_retention_period'`
 Is the length of time in minutes in which the history is retained. *history_retention_period* is **int**. A value of 14420 is used if none is specified.  
  
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
 **sp_change_log_shipping_primary_database** must be run from the **master** database on the primary server. This stored procedure does the following:  
  
1.  Changes the settings in the **log_shipping_primary_database** record, if necessary.  
  
2.  Changes the local record in **log_shipping_monitor_primary** on the primary server using supplied arguments, if necessary.  
  
3.  If the monitor server is different from the primary server, changes record in **log_shipping_monitor_primary** on the monitor server using supplied arguments, if necessary.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can run this procedure.  
  
## Examples  
 This example illustrates the use of **sp_change_log_shipping_primary_database** to update the settings associated with the primary database [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)].  
  
```  
EXEC master.dbo.sp_change_log_shipping_primary_database   
 @database = N'AdventureWorks'   
, @backup_directory = N'c:\LogShipping'   
, @backup_share = N'\\tribeca\LogShipping'   
, @backup_retention_period = 1440   
, @backup_threshold = 60   
, @threshold_alert = 0   
, @threshold_alert_enabled = 1   
, @history_retention_period = 1440   
,@monitor_server_security_mode = 1  
,@backup_compression = 1;  
```  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [log_shipping_primary_databases &#40;Transact-SQL&#41;](../../relational-databases/system-tables/log-shipping-primary-databases-transact-sql.md)  
  
  
