---
description: "sp_help_log_shipping_monitor (Transact-SQL)"
title: "sp_help_log_shipping_monitor (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_help_log_shipping_monitor_TSQL"
  - "sp_help_log_shipping_monitor"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_log_shipping_monitor"
ms.assetid: a4e96c45-6dcd-471a-a494-b5c619459855
author: MashaMSFT
ms.author: mathoma
---
# sp_help_log_shipping_monitor (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a result set containing status and other information for registered primary and secondary databases on a primary, secondary, or monitor server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_log_shipping_monitor  
```  
  
## Arguments  
 None.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**status**|**bit**|Collective status of agents for the log shipping database:<br /><br /> **0** = healthy and no-agent failures.<br /><br /> **1** = otherwise.|  
|**is_primary**|**bit**|Indicates whether this row is for a primary database:<br /><br /> **1** = The row is for a primary database.<br /><br /> **0** = The row is for a secondary database.|  
|**server**|**sysname**|The name of the primary or secondary server where this database resides.|  
|**database_name**|**sysname**|The database name.|  
|**time_since_last_backup**|**int**|The length of time, in minutes, since the last log backup.<br /><br /> NULL = The information is not available or is not relevant.|  
|**last_backup_file**|**nvarchar(500)**|The name of the last successful log backup file.<br /><br /> NULL = The information is not available or is not relevant.|  
|**backup_threshold**|**int**|The length of time, in minutes, after the last backup before a threshold_alert error is raised. **backup_threshold** is **int**, with a default of **60 minutes**.<br /><br /> NULL = The information is not available or is not relevant.<br /><br /> This value can be changed using [sp_add_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-log-shipping-primary-database-transact-sql.md).|  
|**is_backup_alert_enabled**|**bit**|Specifies whether an alert will be raised when **backup_threshold** is exceeded. The value of one (**1**), the default, means that the alert will be raised.<br /><br /> NULL = The information is not available or is not relevant.<br /><br /> This value can be changed using [sp_add_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-log-shipping-primary-database-transact-sql.md).|  
|**time_since_last_copy**|**int**|The length of time, in minutes, since the last log backup was copied.<br /><br /> NULL = The information is not available or is not relevant.|  
|**last_copied_file**|**nvarchar(500)**|The name of the last successfully copied log backup file.<br /><br /> NULL = The information is not available or is not relevant.|  
|**time_since_last_restore**|**int**|The length of time, in minutes, since the last log backup was restored.<br /><br /> NULL = The information is not available or is not relevant.|  
|**last_restored_file**|**nvarchar(500).**|The name of the last successfully restored log backup file.<br /><br /> NULL = The information is not available or is not relevant.|  
|**last_restored_latency**|**int**|Duration of time, in minutes, from the creation of the last backup to restore of the backup.<br /><br /> NULL = The information is not available or is not relevant.|  
|**restore_threshold**|**int**|The number of minutes allowed to elapse between restore operations before an alert is generated. **restore_threshold** cannot be NULL.|  
|**is_restore_alert_enabled**|**bit**|Specifies whether an alert is raised when **restore_threshold** is exceeded. The value of one (**1**), the default, means that the alert is raised.<br /><br /> NULL = The information is not available or is not relevant.<br /><br /> To set restore threshold, use [sp_add_log_shipping_secondary_database](../../relational-databases/system-stored-procedures/sp-add-log-shipping-secondary-database-transact-sql.md).|  
  
## Remarks  
 **sp_help_log_shipping_monitor** must be run from the **master** database on the monitor server.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
