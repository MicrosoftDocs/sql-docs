---
title: "log_shipping_monitor_secondary (Transact-SQL)"
description: log_shipping_monitor_secondary (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "log_shipping_monitor_secondary_TSQL"
  - "log_shipping_monitor_secondary"
helpviewer_keywords:
  - "log_shipping_monitor_secondary system table"
dev_langs:
  - "TSQL"
ms.assetid: afbe1bb7-89a7-4020-9408-0af64a043c2e
---
# log_shipping_monitor_secondary (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Stores one monitor record per secondary database in a log shipping configuration. This table is stored in the **msdb** database.  
  
 The tables related to history and monitoring are also used at the primary server and the secondary servers.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**secondary_server**|**sysname**|The name of the secondary instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration.|  
|**secondary_database**|**sysname**|The name of the secondary database in the log shipping configuration.|  
|**secondary_id**|**uniqueidentifier**|The ID for the secondary server in the log shipping configuration.|  
|**primary_server**|**sysname**|The name of the primary instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration.|  
|**primary_database**|**sysname**|The name of the primary database in the log shipping configuration.|  
|**restore_threshold**|**int**|The number of minutes allowed to elapse between restore operations before an alert is generated.|  
|**threshold_alert**|**int**|The alert to be raised when the restore threshold is exceeded.|  
|**threshold_alert_enabled**|**bit**|Determines if restore threshold alerts are enabled. 1 = Enabled.<br /><br /> 0 = Disabled.|  
|**last_copied_file**|**nvarchar(500)**|The filename of the last backup file copied to the secondary server.|  
|**last_copied_date**|**datetime**|The time and date of the last copy operation to the secondary server.|  
|**last_copied_date_utc**|**datetime**|The time and date of the last copy operation to the secondary server, expressed in Coordinated Universal Time.|  
|**last_restored_file**|**nvarchar(500)**|The filename of the last backup file restored to the secondary database.|  
|**last_restored_date**|**datetime**|The time and date of the last restore operation on the secondary database.|  
|**last_restored_date_utc**|**datetime**|The time and date of the last restore operation on the secondary database, expressed in Coordinated Universal Time.|  
|**last_restored_latency**|**int**|The amount of time, in minutes, that elapsed between when the log backup was created on the primary and when it was restored on the secondary.<br /><br /> The initial value is NULL.|  
|**history_retention_period**|**int**|The amount of time, in minutes, that log shipping history records are retained for a given secondary database before being deleted.|  
  
## Remarks  
 In addition to being stored on the remote monitor server, and information related to a secondary server is also stored on the secondary server in its **log_shipping_monitor_secondary** table.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [sp_refresh_log_shipping_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-refresh-log-shipping-monitor-transact-sql.md)   
 [sp_add_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-log-shipping-secondary-database-transact-sql.md)   
 [sp_change_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-log-shipping-secondary-database-transact-sql.md)   
 [sp_delete_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-secondary-database-transact-sql.md)   
 [sp_help_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-log-shipping-secondary-database-transact-sql.md)   
 [sp_help_log_shipping_monitor_secondary &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-log-shipping-monitor-secondary-transact-sql.md)   
 [sp_refresh_log_shipping_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-refresh-log-shipping-monitor-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
