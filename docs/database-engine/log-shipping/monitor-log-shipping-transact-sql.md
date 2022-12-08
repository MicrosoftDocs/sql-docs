---
title: "Monitor Log Shipping (Transact-SQL)"
description: Learn which tables store history containing monitoring information and stored procedures for monitoring log shipping in SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: log-shipping
ms.topic: conceptual
helpviewer_keywords:
  - "log shipping [SQL Server], status"
  - "history tables [SQL Server]"
  - "historical information [SQL Server], log shipping"
  - "log shipping [SQL Server], monitoring"
  - "alerts [SQL Server], log shipping"
  - "status information [SQL Server], log shipping"
  - "monitoring log shipping [SQL Server]"
---
# Monitor Log Shipping (Transact-SQL)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  After you have configured log shipping, you can monitor information about the status of all the log shipping servers. The history and status of log shipping operations are always saved locally by the log shipping jobs. The history and status of the backup operation are stored at the primary server, and the history and status of the copy and restore operations are stored at the secondary server. If you have implemented a remote monitor server, this information is also stored on the monitor server.  
  
 You can configure alerts that will fire if log shipping operations fail to occur as scheduled. Errors are raised by an alert job that watches the status of the backup and restore operations. You can define alerts that notify an operator when these errors are raised. If a monitor server is configured, one alert job runs on the monitor server that raises errors for all operations in the log shipping configuration. If a monitor server is not specified, an alert job runs on the primary server instance, which monitors the backup operation. If a monitor server is not specified, an alert job also runs on each secondary server instance to monitor the local copy and restore operations.  
  
> [!IMPORTANT]  
>  To monitor a log shipping configuration, you must add the monitor server when you enable log shipping. If you add a monitor server later, you must remove the log shipping configuration and then replace it with a new configuration that includes a monitor server. For more information, see [Configure Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/configure-log-shipping-sql-server.md). Furthermore, after the monitor server has been configured, it cannot be changed without removing log shipping first.  
  
## History Tables Containing Monitoring Information  
 The monitoring history tables contain metadata that is stored on the monitor server. A copy of information specific to a given primary or secondary server is also stored locally.  
  
 You can query these tables to monitor the status of a log shipping session. For example, to learn status of log shipping, check the status and history of the backup job, copy job, and restore job. You can view specific log shipping history and error details by querying the following monitoring tables.  
  
|Table|Description|  
|-----------|-----------------|  
|[log_shipping_monitor_alert](../../relational-databases/system-tables/log-shipping-monitor-alert-transact-sql.md)|Stores alert job ID.|  
|[log_shipping_monitor_error_detail](../../relational-databases/system-tables/log-shipping-monitor-error-detail-transact-sql.md)|Stores error details for log shipping jobs. You can query this table see the errors for an agent session. Optionally, you can sort the errors by the date and time at which each was logged. Each error is logged as a sequence of exceptions, and multiple errors (sequences) can per agent session.|  
|[log_shipping_monitor_history_detail](../../relational-databases/system-tables/log-shipping-monitor-history-detail-transact-sql.md)|Contains history details for log shipping agents. You can query this table to see the history detail for an agent session.|  
|[log_shipping_monitor_primary](../../relational-databases/system-tables/log-shipping-monitor-primary-transact-sql.md)|Stores one monitor record for the primary database in each log shipping configuration, including information about the last backup file and last restored file that is useful for monitoring.|  
|[log_shipping_monitor_secondary](../../relational-databases/system-tables/log-shipping-monitor-secondary-transact-sql.md)|Stores one monitor record for each secondary database, including information about the last backup file and last restored file that is useful for monitoring.|  
  
## Stored Procedures for Monitoring Log Shipping  
 Monitoring and history information is stored in tables in **msdb**, which can be accessed using log shipping stored procedures. Run these stored procedures on the servers indicated in the following table.  
  
|Stored procedure|Description|Run this procedure on|  
|----------------------|-----------------|---------------------------|  
|[sp_help_log_shipping_monitor_primary](../../relational-databases/system-stored-procedures/sp-help-log-shipping-monitor-primary-transact-sql.md)|Returns monitor records for the specified primary database from the **log_shipping_monitor_primary** table.|Monitor server or primary server|  
|[sp_help_log_shipping_monitor_secondary](../../relational-databases/system-stored-procedures/sp-help-log-shipping-monitor-secondary-transact-sql.md)|Returns monitor records for the specified secondary database from the **log_shipping_monitor_secondary** table.|Monitor server or secondary server|  
|[sp_help_log_shipping_alert_job](../../relational-databases/system-stored-procedures/sp-help-log-shipping-alert-job-transact-sql.md)|Returns the job ID of the alert job.|Monitor server, or primary or secondary server if no monitor is defined|  
|[sp_help_log_shipping_primary_database](../../relational-databases/system-stored-procedures/sp-help-log-shipping-primary-database-transact-sql.md)|Retrieves primary database settings and displays the values from the **log_shipping_primary_databases** and **log_shipping_monitor_primary** tables.|Primary server|  
|[sp_help_log_shipping_primary_secondary](../../relational-databases/system-stored-procedures/sp-help-log-shipping-primary-secondary-transact-sql.md)|Retrieves secondary database names for a primary database.|Primary server|  
|[sp_help_log_shipping_secondary_database](../../relational-databases/system-stored-procedures/sp-help-log-shipping-secondary-database-transact-sql.md)|Retrieves secondary-database settings from the **log_shipping_secondary**, **log_shipping_secondary_databases** and **log_shipping_monitor_secondary** tables.|Secondary server|  
|[sp_help_log_shipping_secondary_primary &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-log-shipping-secondary-primary-transact-sql.md)|This stored procedure retrieves the settings for a given primary database on the secondary server.|Secondary server|  
  
## See Also  
 [View the Log Shipping Report &#40;SQL Server Management Studio&#41;](../../database-engine/log-shipping/view-the-log-shipping-report-sql-server-management-studio.md)   
 [Log Shipping Stored Procedures and Tables](../../database-engine/log-shipping/log-shipping-tables-and-stored-procedures.md)  
  
  
