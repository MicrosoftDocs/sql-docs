---
title: "log_shipping_monitor_alert (Transact-SQL)"
description: log_shipping_monitor_alert (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "log_shipping_monitor_alert"
  - "log_shipping_monitor_alert_TSQL"
helpviewer_keywords:
  - "log_shipping_monitor_alert system table"
dev_langs:
  - "TSQL"
ms.assetid: 1c775e48-9898-4149-b9d1-04d465f23438
---
# log_shipping_monitor_alert (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Stores the alert job ID for log shipping. This table is stored in the **msdb** database.   
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**alert_job_id**|**uniqueidentifier**|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job ID of the log shipping alert job.|  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [sp_add_log_shipping_alert_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-log-shipping-alert-job-transact-sql.md)   
 [sp_delete_log_shipping_alert_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-alert-job-transact-sql.md)   
 [sp_help_log_shipping_alert_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-log-shipping-alert-job-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
