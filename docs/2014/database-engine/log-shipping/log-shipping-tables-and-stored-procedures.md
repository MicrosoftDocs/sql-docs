---
title: "Log Shipping Tables and Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "secondary servers [SQL Server]"
  - "monitor servers [SQL Server]"
  - "log shipping [SQL Server], system tables"
  - "log shipping [SQL Server], stored procedures"
  - "primary servers [SQL Server]"
ms.assetid: 03420810-4c38-4c0c-adf0-913eb044c50a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Log Shipping Tables and Stored Procedures
  This topic describes all of the tables and stored procedures associated with a log shipping configuration. All log shipping tables are stored in **msdb** on each server. The tables below describe which tables and stored procedures are used on which servers in a log shipping configuration.  
  
## Primary Server Tables  
  
|Table|Description|  
|-----------|-----------------|  
|[log_shipping_monitor_alert](/sql/relational-databases/system-tables/log-shipping-monitor-alert-transact-sql)|Stores alert job ID. This table is only used on the primary server if a remote monitor server has not been configured.|  
|[log_shipping_monitor_error_detail](/sql/relational-databases/system-tables/log-shipping-monitor-error-detail-transact-sql)|Stores error detail for log shipping jobs associated with this primary server.|  
|[log_shipping_monitor_history_detail](/sql/relational-databases/system-tables/log-shipping-monitor-history-detail-transact-sql)|Stores history detail for log shipping jobs associated with this primary server.|  
|[log_shipping_monitor_primary](/sql/relational-databases/system-tables/log-shipping-monitor-primary-transact-sql)|Stores one monitor record for this primary database.|  
|[log_shipping_primary_databases](/sql/relational-databases/system-tables/log-shipping-primary-databases-transact-sql)|Contains configuration information for primary databases on a given server. Stores one row per primary database.|  
|[log_shipping_primary_secondaries](/sql/relational-databases/system-tables/log-shipping-primary-secondaries-transact-sql)|Maps primary databases to secondary databases.|  
  
## Primary Server Stored Procedures  
  
|Stored Procedure|Description|  
|----------------------|-----------------|  
|[sp_add_log_shipping_primary_database](/sql/relational-databases/system-stored-procedures/sp-add-log-shipping-primary-database-transact-sql)|Sets up the primary database for a log shipping configuration, including the backup job, local monitor record, and remote monitor record.|  
|[sp_add_log_shipping_primary_secondary](/sql/relational-databases/system-stored-procedures/sp-add-log-shipping-primary-secondary-transact-sql)|Adds a secondary database name to an existing primary database.|  
|[sp_change_log_shipping_primary_database](/sql/relational-databases/system-stored-procedures/sp-change-log-shipping-primary-database-transact-sql)|Changes primary database settings including local and remote monitor record.|  
|[sp_cleanup_log_shipping_history](/sql/relational-databases/system-stored-procedures/sp-cleanup-log-shipping-history-transact-sql)|Cleans up history locally and on the monitor based on retention period.|  
|[sp_delete_log_shipping_primary_database](/sql/relational-databases/system-stored-procedures/sp-delete-log-shipping-primary-database-transact-sql)|Removes log shipping of primary database including backup job as well as local and remote history.|  
|[sp_delete_log_shipping_primary_secondary](/sql/relational-databases/system-stored-procedures/sp-delete-log-shipping-primary-secondary-transact-sql)|Removes a secondary database name from a primary database.|  
|[sp_help_log_shipping_primary_database](/sql/relational-databases/system-stored-procedures/sp-help-log-shipping-primary-database-transact-sql)|Retrieves primary database settings and displays the values from the **log_shipping_primary_databases** and **log_shipping_monitor_primary** tables.|  
|[sp_help_log_shipping_primary_secondary](/sql/relational-databases/system-stored-procedures/sp-help-log-shipping-primary-secondary-transact-sql)|Retrieves secondary database names for a primary database.|  
|[sp_refresh_log_shipping_monitor](/sql/relational-databases/system-stored-procedures/sp-refresh-log-shipping-monitor-transact-sql)|Refreshes the monitor with the latest information for the specified log shipping agent.|  
  
## Secondary Server Tables  
  
|Table|Description|  
|-----------|-----------------|  
|[log_shipping_monitor_alert](/sql/relational-databases/system-tables/log-shipping-monitor-alert-transact-sql)|Stores alert job ID. This table is only used on the secondary server if a remote monitor server has not been configured.|  
|[log_shipping_monitor_error_detail](/sql/relational-databases/system-tables/log-shipping-monitor-error-detail-transact-sql)|Stores error detail for log shipping jobs associated with this secondary server.|  
|[log_shipping_monitor_history_detail](/sql/relational-databases/system-tables/log-shipping-monitor-history-detail-transact-sql)|Stores history detail for log shipping jobs associated with this secondary server.|  
|[log_shipping_monitor_secondary](/sql/relational-databases/system-tables/log-shipping-monitor-secondary-transact-sql)|Stores one monitor record per secondary database associated with this secondary server.|  
|[log_shipping_secondary](/sql/relational-databases/system-tables/log-shipping-secondary-transact-sql)|Contains configuration information for the secondary databases on a given server. Stores one row per secondary ID.|  
|[log_shipping_secondary_databases](/sql/relational-databases/system-tables/log-shipping-secondary-databases-transact-sql)|Stores configuration information for a given secondary database. Stores one row per secondary database.|  
  
> [!NOTE]  
>  Secondary databases on the same secondary server for a given primary database share the settings in the **log_shipping_secondary** table. If a shared setting is altered for one secondary database, the setting is altered for all of them.  
  
## Secondary Server Stored Procedures  
  
|Stored Procedure|Description|  
|----------------------|-----------------|  
|[sp_add_log_shipping_secondary_database](/sql/relational-databases/system-stored-procedures/sp-add-log-shipping-secondary-database-transact-sql)|Sets up a secondary database for log shipping.|  
|[sp_add_log_shipping_secondary_primary](/sql/relational-databases/system-stored-procedures/sp-add-log-shipping-secondary-primary-transact-sql)|Sets up the primary information, adds local and remote monitor links, and creates copy and restore jobs on the secondary server for the specified primary database.|  
|[sp_change_log_shipping_secondary_database](/sql/relational-databases/system-stored-procedures/sp-change-log-shipping-secondary-database-transact-sql)|Changes secondary database settings including local and remote monitor records.|  
|[sp_change_log_shipping_secondary_primary](/sql/relational-databases/system-stored-procedures/sp-change-log-shipping-secondary-primary-transact-sql)|Changes secondary database settings such as source and destination directory, and file retention period.|  
|[sp_cleanup_log_shipping_history](/sql/relational-databases/system-stored-procedures/sp-cleanup-log-shipping-history-transact-sql)|Cleans up history locally and on the monitor based on retention period.|  
|[sp_delete_log_shipping_secondary_database](/sql/relational-databases/system-stored-procedures/sp-delete-log-shipping-secondary-database-transact-sql)|Removes a secondary database and the local history and remote history.|  
|[sp_delete_log_shipping_secondary_primary](/sql/relational-databases/system-stored-procedures/sp-delete-log-shipping-secondary-primary-transact-sql)|Removes the information about the specified primary server from the secondary server.|  
|[sp_help_log_shipping_secondary_database](/sql/relational-databases/system-stored-procedures/sp-help-log-shipping-secondary-database-transact-sql)|Retrieves secondary database settings from the **log_shipping_secondary**, **log_shipping_secondary_databases**, and **log_shipping_monitor_secondary** tables.|  
|[sp_help_log_shipping_secondary_primary](/sql/relational-databases/system-stored-procedures/sp-help-log-shipping-secondary-primary-transact-sql)|This stored procedure retrieves the settings for a given primary database on the secondary server.|  
|[sp_refresh_log_shipping_monitor](/sql/relational-databases/system-stored-procedures/sp-refresh-log-shipping-monitor-transact-sql)|Refreshes the monitor with the latest information for the specified log shipping agent.|  
  
## Monitor Server Tables  
  
|Table|Description|  
|-----------|-----------------|  
|[log_shipping_monitor_alert](/sql/relational-databases/system-tables/log-shipping-monitor-alert-transact-sql)|Stores alert job ID.|  
|[log_shipping_monitor_error_detail](/sql/relational-databases/system-tables/log-shipping-monitor-error-detail-transact-sql)|Stores error detail for log shipping jobs.|  
|[log_shipping_monitor_history_detail](/sql/relational-databases/system-tables/log-shipping-monitor-history-detail-transact-sql)|Stores history detail for log shipping jobs.|  
|[log_shipping_monitor_primary](/sql/relational-databases/system-tables/log-shipping-monitor-primary-transact-sql)|Stores one monitor record per primary database associated with this monitor server.|  
|[log_shipping_monitor_secondary](/sql/relational-databases/system-tables/log-shipping-monitor-secondary-transact-sql)|Stores one monitor record per secondary database associated with this monitor server.|  
  
## Monitor Server Stored Procedures  
  
|Stored Procedure|Description|  
|----------------------|-----------------|  
|[sp_add_log_shipping_alert_job](/sql/relational-databases/system-stored-procedures/sp-add-log-shipping-alert-job-transact-sql)|Creates a log shipping alert job if one has not already been created.|  
|[sp_delete_log_shipping_alert_job](/sql/relational-databases/system-stored-procedures/sp-delete-log-shipping-alert-job-transact-sql)|Removes a log shipping alert job if there are no associated primary databases.|  
|[sp_help_log_shipping_alert_job](/sql/relational-databases/system-stored-procedures/sp-help-log-shipping-alert-job-transact-sql)|Returns the job ID of the alert job.|  
|[sp_help_log_shipping_monitor_primary](/sql/relational-databases/system-stored-procedures/sp-help-log-shipping-monitor-primary-transact-sql)|Returns monitor records for the specified primary database from the **log_shipping_monitor_primary** table.|  
|[sp_help_log_shipping_monitor_secondary](/sql/relational-databases/system-stored-procedures/sp-help-log-shipping-monitor-secondary-transact-sql)|Returns monitor records for the specified secondary database from the **log_shipping_monitor_secondary** table.|  
  
  
