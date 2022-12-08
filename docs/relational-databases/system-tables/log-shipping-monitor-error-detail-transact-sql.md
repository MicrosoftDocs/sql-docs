---
title: "log_shipping_monitor_error_detail (Transact-SQL)"
description: log_shipping_monitor_error_detail (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "log_shipping_monitor_error_detail_TSQL"
  - "log_shipping_monitor_error_detail"
helpviewer_keywords:
  - "log_shipping_monitor_error_detail system table"
dev_langs:
  - "TSQL"
ms.assetid: 0c38a625-60d2-4ee2-bcf3-2ba367914220
---
# log_shipping_monitor_error_detail (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Stores error detail for log shipping jobs. This table is stored in the **msdb** database.  
  
 The tables related to history and monitoring are also used at the primary server and the secondary servers.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**agent_id**|**uniqueidentifier**|The primary ID for backup or the secondary ID for copy or restore.|  
|**agent_type**|**tinyint**|The type of log shipping job.<br /><br /> 0 = Backup.<br /><br /> 1 = Copy.<br /><br /> 2 = Restore.|  
|**session_id**|**int**|The session ID for the backup/copy/restore/ job.|  
|**database_name**|**sysname**|The name of the database associated with this error record. Primary database for backup, secondary database for restore, or empty for copy.|  
|**sequence_number**|**int**|An incremental number indicating the correct order of information for errors that span multiple records.|  
|**log_time**|**datetime**|The date and time at which the record was created.|  
|**log_time_utc**|**datetime**|The date and time at which the record was created, expressed in Coordinated Universal Time.|  
|**message**|**nvarchar**|Message text.|  
|**source**|**nvarchar**|The source of the error message or event.|  
|**help_url**|**nvarchar**|The URL, if available, where more information about the error can be found.|  
  
## Remarks  
 This table contains error detail for the log shipping agents. Each error is logged as a sequence of exceptions. There can be multiple errors (sequences) for each agent session.  
  
 In addition to being stored on the remote monitor server, the information related to the primary server is stored on the primary server in its **log_shipping_monitor_error_detail** table, and information related to a secondary server is also stored on the secondary server in its **log_shipping_monitor_error_detail** table.  
  
 To identify an agent session, use columns **agent_id**, **agent_type**, and **session_id**. Sort by **log_time** to see the errors in the order in which they were logged.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [log_shipping_monitor_history_detail &#40;Transact-SQL&#41;](../../relational-databases/system-tables/log-shipping-monitor-history-detail-transact-sql.md)   
 [sp_cleanup_log_shipping_history &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cleanup-log-shipping-history-transact-sql.md)   
 [sp_delete_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-primary-database-transact-sql.md)   
 [sp_delete_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-secondary-database-transact-sql.md)   
 [sp_refresh_log_shipping_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-refresh-log-shipping-monitor-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
