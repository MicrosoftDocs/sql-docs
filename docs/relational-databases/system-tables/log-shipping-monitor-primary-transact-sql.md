---
title: "log_shipping_monitor_primary (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "log_shipping_monitor_primary"
  - "log_shipping_monitor_primary_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "log_shipping_monitor_primary system table"
ms.assetid: 5f629a29-1a62-40e6-ae33-6f6b7dd09a36
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# log_shipping_monitor_primary (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Stores one monitor record per primary database in each log shipping configuration. This table is stored in the **msdb** database.  
  
 The tables related to history and monitoring are also used at the primary server and the secondary servers.   
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**primary_id**|**uniqueidentifier**|The ID of the primary database for the log shipping configuration.|  
|**primary_server**|**sysname**|The name of the primary instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration.|  
|**primary_database**|**sysname**|The name of the primary database in the log shipping configuration.|  
|**backup_threshold**|**int**|The number of minutes allowed to elapse between backup operations before an alert is generated.|  
|**threshold_alert**|**int**|The alert to be raised when the backup threshold is exceeded.|  
|**threshold_alert_enabled**|**bit**|Determines if backup threshold alerts are enabled. 1 = Enabled.<br /><br /> 0 = Disabled.|  
|**last_backup_file**|**nvarchar(500)**|The absolute path of the most recent transaction log backup.|  
|**last_backup_date**|**datetime**|The time and date of the last transaction log backup operation on the primary database.|  
|**last_backup_date_utc**|**datetime**|The time and date of the last transaction log backup operation on the primary database, expressed in Coordinated Universal Time.|  
|**history_retention_period**|**int**|The amount of time, in minutes, that log shipping history records are retained for a given primary database before being deleted.|  
  
## Remarks  
 In addition to being stored on the remote monitor server, the information related to the primary server is stored on the primary server in its **log_shipping_monitor_primary** table.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [sp_add_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-log-shipping-primary-database-transact-sql.md)   
 [sp_change_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-log-shipping-primary-database-transact-sql.md)   
 [sp_delete_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-primary-database-transact-sql.md)   
 [sp_help_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-log-shipping-primary-database-transact-sql.md)   
 [sp_refresh_log_shipping_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-refresh-log-shipping-monitor-transact-sql.md)   
 [sp_help_log_shipping_monitor_primary &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-log-shipping-monitor-primary-transact-sql.md)   
 [sp_delete_log_shipping_alert_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-alert-job-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
