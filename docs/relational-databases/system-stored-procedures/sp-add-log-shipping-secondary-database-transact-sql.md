---
title: "sp_add_log_shipping_secondary_database (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_add_log_shipping_secondary_database"
  - "sp_add_log_shipping_secondary_database_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_log_shipping_secondary_database"
ms.assetid: d29e1c24-3a3c-47a4-a726-4584afa6038a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sp_add_log_shipping_secondary_database (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Sets up a secondary databases for log shipping.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_log_shipping_secondary_database  
[ @secondary_database = ] 'secondary_database',  
[ @primary_server = ] 'primary_server',   
[ @primary_database = ] 'primary_database',  
[, [ @restore_delay = ] 'restore_delay']  
[, [ @restore_all = ] 'restore_all']  
[, [ @restore_mode = ] 'restore_mode']  
[, [ @disconnect_users = ] 'disconnect_users']  
[, [ @block_size = ] 'block_size']  
[, [ @buffer_count = ] 'buffer_count']  
[, [ @max_transfer_size = ] 'max_transfer_size']  
[, [ @restore_threshold = ] 'restore_threshold']   
[, [ @threshold_alert = ] 'threshold_alert']   
[, [ @threshold_alert_enabled = ] 'threshold_alert_enabled']   
[, [ @history_retention_period = ] 'history_retention_period']  
```  
  
## Arguments  
 [ **@secondary_database** = ] '*secondary_database*'  
 Is the name of the secondary database. *secondary_database* is **sysname**, with no default.  
  
 [ **@primary_server** = ] '*primary_server*'  
 The name of the primary instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration. *primary_server* is **sysname** and cannot be NULL.  
  
 [ **@primary_database** = ] '*primary_database*'  
 Is the name of the database on the primary server. *primary_database* is **sysname**, with no default.  
  
 [ **@restore_delay** = ] '*restore_delay*'  
 The amount of time, in minutes, that the secondary server waits before restoring a given backup file. *restore_delay* is **int** and cannot be NULL. The default value is 0.  
  
 [ **@restore_all** = ] '*restore_all*'  
 If set to 1, the secondary server restores all available transaction log backups when the restore job runs. Otherwise, it stops after one file is restored. *restore_all* is **bit** and cannot be NULL.  
  
 [ **@restore_mode** = ] '*restore_mode*'  
 The restore mode for the secondary database.  
  
 0 = Restore log with NORECOVERY.  
  
 1 = restore log with STANDBY.  
  
 *restore* is **bit** and cannot be NULL.  
  
 [ **@disconnect_users** = ] '*disconnect_users*'  
 If set to 1, users are disconnected from the secondary database when a restore operation is performed. Default = 0. *disconnect* users is **bit** and cannot be NULL.  
  
 [ **@block_size** = ] '*block_size*'  
 The size, in bytes, that is used as the block size for the backup device. *block_size* is **int** with a default value of -1.  
  
 [ **@buffer_count** = ] '*buffer_count*'  
 The total number of buffers used by the backup or restore operation. *buffer_count* is **int** with a default value of -1.  
  
 [ **@max_transfer_size** = ] '*max_transfer_size*'  
 The size, in bytes, of the maximum input or output request which is issued by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the backup device. *max_transfersize* is **int** and can be NULL.  
  
 [ **@restore_threshold** = ] '*restore_threshold*'  
 The number of minutes allowed to elapse between restore operations before an alert is generated. *restore_threshold* is **int** and cannot be NULL.  
  
 [ **@threshold_alert** = ] '*threshold_alert*'  
 Is the alert to be raised when the backup threshold is exceeded. *threshold_alert* is **int**, with a default of 14,420.  
  
 [ **@threshold_alert_enabled** = ] '*threshold_alert_enabled*'  
 Specifies whether an alert is raised when *backup_threshold* is exceeded. The value of one (1), the default, means that the alert is raised. *threshold_alert_enabled* is **bit**.  
  
 [ **@history_retention_period** = ] '*history_retention_period*'  
 Is the length of time in minutes in which the history is retained. *history_retention_period* is **int**, with a default of NULL. A value of 14420 is used if none is specified.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_add_log_shipping_secondary_database** must be run from the **master** database on the secondary server. This stored procedure does the following:  
  
1.  **sp_add_log_shipping_secondary_primary** should be called prior to this stored procedure to initialize the primary log shipping database information on the secondary server.  
  
2.  Adds an entry for the secondary database in **log_shipping_secondary_databases** using the supplied arguments.  
  
3.  Adds a local monitor record in **log_shipping_monitor_secondary** on the secondary server using supplied arguments.  
  
4.  If the monitor server is different from the secondary server, adds a monitor record in **log_shipping_monitor_secondary** on the monitor server using supplied arguments.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can run this procedure.  
  
## Examples  
 This example illustrates using the **sp_add_log_shipping_secondary_database** stored procedure to add the database **LogShipAdventureWorks** as a secondary database in a log shipping configuration with the primary database [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] residing on the primary server TRIBECA.  
  
```  
EXEC master.dbo.sp_add_log_shipping_secondary_database   
@secondary_database = N'LogShipAdventureWorks'   
,@primary_server = N'TRIBECA'   
,@primary_database = N'AdventureWorks2012'   
,@restore_delay = 0   
,@restore_mode = 1   
,@disconnect_users = 0   
,@restore_threshold = 45     
,@threshold_alert_enabled = 0   
,@history_retention_period = 1440 ;  
GO  
```  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
