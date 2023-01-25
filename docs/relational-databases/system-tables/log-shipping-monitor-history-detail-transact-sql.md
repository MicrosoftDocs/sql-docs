---
title: "log_shipping_monitor_history_detail (Transact-SQL)"
description: log_shipping_monitor_history_detail (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "log_shipping_monitor_history_detail_TSQL"
  - "log_shipping_monitor_history_detail"
helpviewer_keywords:
  - "log_shipping_monitor_history_detail system table"
dev_langs:
  - "TSQL"
ms.assetid: 7080c888-323b-4206-a1ab-e6c51f9e2579
---
# log_shipping_monitor_history_detail (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Stores history details for log shipping jobs. This table is stored in the **msdb** database.  
  
 The tables related to history and monitoring are also used at the primary server and the secondary servers.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**agent_id**|**uniqueidentifier**|The primary ID for backup or the secondary ID for copy or restore.|  
|**agent_type**|**tinyint**|The type of log shipping job.<br /><br /> 0 = Backup.<br /><br /> 1 = Copy.<br /><br /> 2 = Restore.|  
|**session_id**|**int**|The session ID for the backup/copy/restore/ job.|  
|**database_name**|**sysname**|The name of the database associated with this record. Primary database for backup, secondary database for restore, or empty for copy.|  
|**session_status**|**tinyint**|The status of the session.<br /><br /> 0 = Starting.<br /><br /> 1 = Running.<br /><br /> 2 = Success.<br /><br /> 3 = Error.<br /><br /> 4 = Warning.|  
|**log_time**|**datetime**|The date and time at which the record was created.|  
|**log_time_utc**|**datetime**|The date and time at which the record was created, expressed in Coordinated Universal Time.|  
|**message**|**nvarchar(max)**|Message text.|  
  
## Remarks  
 This table contains history details for the log shipping agents. To identify an agent session, use columns **agent_id**, **agent_type**, and **session_id**. To see the history detail for the agent session, sort by **log_time**.  
  
 In addition to being stored on the remote monitor server, the information related to the primary server is stored on the primary server in its **log_shipping_monitor_history_detail** table, and information related to a secondary server is also stored on the secondary server in its **log_shipping_monitor_history_detail** table.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [sp_delete_log_shipping_primary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-primary-database-transact-sql.md)   
 [sp_cleanup_log_shipping_history &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cleanup-log-shipping-history-transact-sql.md)   
 [sp_refresh_log_shipping_monitor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-refresh-log-shipping-monitor-transact-sql.md)   
 [sp_delete_log_shipping_secondary_database &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-secondary-database-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)   
 [log_shipping_monitor_error_detail &#40;Transact-SQL&#41;](../../relational-databases/system-tables/log-shipping-monitor-error-detail-transact-sql.md)  
  
  
