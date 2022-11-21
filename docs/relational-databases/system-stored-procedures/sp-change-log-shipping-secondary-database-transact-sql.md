---
description: "sp_change_log_shipping_secondary_database (Transact-SQL)"
title: "sp_change_log_shipping_secondary_database (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_change_log_shipping_secondary_database"
  - "sp_change_log_shipping_secondary_database_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_change_log_shipping_secondary_database"
ms.assetid: 3ebcf2f1-980f-4543-a84b-fbaeea54eeac
author: MashaMSFT
ms.author: mathoma
---
# sp_change_log_shipping_secondary_database (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Changes secondary database settings.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_change_log_shipping_secondary_database  
[ @secondary_database = ] 'secondary_database',  
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
`[ @restore_delay = ] 'restore_delay'`
 The amount of time, in minutes, that the secondary server waits before restoring a given backup file. *restore_delay* is **int** and cannot be NULL. The default value is 0.  
  
`[ @restore_all = ] 'restore_all'`
 If set to 1, the secondary server restores all available transaction log backups when the restore job runs. Otherwise, it stops after one file has been restored. *restore_all* is **bit** and cannot be NULL.  
  
`[ @restore_mode = ] 'restore_mode'`
 The restore mode for the secondary database.  
  
 0 = restore log with NORECOVERY.  
  
 1 = restore log with STANDBY.  
  
 *restore* is **bit** and cannot be NULL.  
  
`[ @disconnect_users = ] 'disconnect_users'`
 If set to 1, users is disconnected from the secondary database when a restore operation is performed. Default = 0. *disconnect_users* is **bit** and cannot be NULL.  
  
`[ @block_size = ] 'block_size'`
 The size, in bytes, that is used as the block size for the backup device. *block_size* is **int** with a default value of -1.  
  
`[ @buffer_count = ] 'buffer_count'`
 The total number of buffers used by the backup or restore operation. *buffer_count* is **int** with a default value of -1.  
  
`[ @max_transfer_size = ] 'max_transfer_size'`
 The size, in bytes, of the maximum input or output request which is issued by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the backup device. *max_transfersize* is **int** and can be NULL.  
  
`[ @restore_threshold = ] 'restore_threshold'`
 The number of minutes allowed to elapse between restore operations before an alert is generated. *restore_threshold* is **int** and cannot be NULL.  
  
`[ @threshold_alert = ] 'threshold_alert'`
 Is the alert to be raised when the restore threshold is exceeded. *threshold_alert* is **int**, with a default of 14421.  
  
`[ @threshold_alert_enabled = ] 'threshold_alert_enabled'`
 Specifies whether an alert will be raised when *restore_threshold*is exceeded. 1 = enabled; 0 = disabled. *threshold_alert_enabled* is **bit** and cannot be NULL.  
  
`[ @history_retention_period = ] 'history_retention_period'`
 Is the length of time in minutes in which the history will be retained. *history_retention_period* is **int**. A value of 1440 will be used if none is specified.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_change_log_shipping_secondary_database** must be run from the **master** database on the secondary server. This stored procedure does the following:  
  
1.  Changes the settings in the **log_shipping_secondary_database** records as necessary.  
  
2.  Changes the local monitor record in **log_shipping_monitor_secondary** on the secondary server using supplied arguments, if necessary.  

## Permissions  
 Only members of the **sysadmin** fixed server role can run this procedure.  
  
## Examples  
 This example illustrates using **sp_change_log_shipping_secondary_database** to update secondary database parameters for the database **LogShipAdventureWorks**.  
  
```  
EXEC master.dbo.sp_change_log_shipping_secondary_database   
 @secondary_database =  'LogShipAdventureWorks'  
,  @restore_delay = 0  
,  @restore_all = 1  
,  @restore_mode = 0  
,  @disconnect_users = 0  
,  @threshold_alert = 14420  
,  @threshold_alert_enabled = 1  
,  @history_retention_period = 14420;  
```  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
