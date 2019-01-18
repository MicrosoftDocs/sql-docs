---
title: "sp_help_log_shipping_monitor_primary (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_log_shipping_monitor_primary"
  - "sp_help_log_shipping_monitor_primary_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_log_shipping_monitor_primary"
ms.assetid: d9dfcb8f-1da6-49ca-a2c8-411574915434
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sp_help_log_shipping_monitor_primary (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information regarding a primary database from the monitor tables.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_log_shipping_monitor_primary  
[ @primary_server = ] 'primary_server',   
[ @primary_database = ] 'primary_database'  
```  
  
## Arguments  
 [ **@primary_server =** ] '*primary_server*'  
 The name of the primary instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration. *primary_server* is **sysname** and cannot be NULL.  
  
 [ **@primary_database =** ] '*primary_database*'  
 Is the name of the database on the primary server. *primary_database* is **sysname**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Description|  
|-----------------|-----------------|  
|**primary_id**|The ID of the primary database for the log shipping configuration.|  
|**primary_server**|The name of the primary instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration.|  
|**primary_database**|The name of the primary database in the log shipping configuration.|  
|**backup_threshold**|The number of minutes allowed to elapse between backup operations before an alert is generated.|  
|**threshold_alert**|The alert to be raised when the backup threshold is exceeded.|  
|**threshold_alert_enabled**|Determines if backup threshold alerts are enabled. 1 = enabled; 0 = disabled.|  
|**last_backup_file**|The absolute path of the most recent transaction log backup.|  
|**last_backup_date**|The time and date of the last transaction log backup operation on the primary database.|  
|**last_backup_date_utc**|The time and date of the last transaction log backup operation on the primary database, expressed in Coordinated Universal Time.|  
|**history_retention_period**|The amount of time, in minutes, that log shipping history records are retained for a given primary database before being deleted.|  
  
## Remarks  
 **sp_help_log_shipping_monitor_primary** must be run from the **master** database on the monitor server.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can run this procedure.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
