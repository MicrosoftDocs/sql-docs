---
description: "sp_help_log_shipping_monitor_secondary (Transact-SQL)"
title: "sp_help_log_shipping_monitor_secondary (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/02/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_help_log_shipping_monitor_secondary"
  - "sp_help_log_shipping_monitor_secondary_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_log_shipping_monitor_secondary"
ms.assetid: 3ac091ea-c9a8-4c05-a0b6-1ccf4e001339
author: MashaMSFT
ms.author: mathoma
---
# sp_help_log_shipping_monitor_secondary (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns information regarding a secondary database from the monitor tables.  
  
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_log_shipping_monitor_secondary  
[ @secondary_server = ] 'secondary_server',  
[ @secondary_database = ] 'secondary_database'  
```  
  
## Arguments  
`[ @secondary_server = ] 'secondary_server'`
 Is the name of the secondary server. *secondary_server* is **sysname**, with no default.  
  
`[ @secondary_database = ] 'secondary_database'`
 Is the name of the secondary database. *secondary_database* is **sysname**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column|Description|  
|------------|-----------------|  
|**secondary_server**|The name of the secondary instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration.|  
|**secondary_database**|The name of the secondary database in the log shipping configuration.|  
|**secondary_id**|The ID for the secondary server in the log shipping configuration.|  
|**primary_server**|The name of the primary instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration.|  
|**primary_database**|The name of the primary database in the log shipping configuration.|  
|**restore_threshold**|The number of minutes allowed to elapse between restore operations before an alert is generated.|  
|**threshold_alert**|The alert to be raised when the restore threshold is exceeded.|  
|**threshold_alert_enabled**|Determines if restore threshold alerts are enabled.<br /><br /> 1 = Enabled.<br /><br /> 0 = Disabled.|  
|**last_copied_file**|The filename of the last backup file copied to the secondary server.|  
|**last_copied_date**|The time and date of the last copy operation to the secondary server.|  
|**last_copied_date_utc**|The time and date of the last copy operation to the secondary server, expressed in Coordinated Universal Time.|  
|**last_restored_file**|The filename of the last backup file restored to the secondary database.|  
|**last_restored_date**|The time and date of the last restore operation on the secondary database.|  
|**last_restored_date_utc**|The time and date of the last restore operation on the secondary database, expressed in Coordinated Universal Time.|  
|**history_retention_period**|The amount of time, in minutes, that log shipping history records are retained for a given secondary database before being deleted.|  
  
## Remarks  
 **sp_help_log_shipping_monitor_secondary** must be run from the **master** database on the monitor server.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can run this procedure.  
  
## See also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
