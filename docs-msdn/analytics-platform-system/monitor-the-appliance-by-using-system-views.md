---
title: "Monitor the Appliance by Using System Views (Analytics Platform System)"
ms.custom: na
ms.date: 01/05/2017
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d6e34c16-12ba-4a6f-80a7-391bca5ba534
caps.latest.revision: 8
author: BarbKess
---
# Monitor the Appliance by Using System Views
This topic lists the system views that you can use for monitoring SQL Server PDW.  
  
## To Monitor the Appliance by Using System Views  
SQL Server PDW includes comprehensive system views that enable you to obtain detailed information about the appliance health, state, and performance. This table provides links to system views that can be used for each monitoring capability.  
  
![PDW system views alerts](./media/monitor-the-appliance-by-using-system-views/PDW_system_views_alerts.png "PDW_system_views_alerts")  
  
|||  
|-|-|  
|**Information Type**|**Related System Views**|  
|Overall status of the appliance|[sys.dm_pdw_sys_info](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-sys-info-transact-sql)|  
|Alerts|[sys.pdw_health_alerts](/sql-docs/docs/relational-databases/system-catalog-views/sys-pdw-health-alerts-transact-sql)<br /><br />[sys.dm_pdw_component_health_alerts](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-component-health-alerts-transact-sql)<br /><br />[sys.dm_pdw_component_health_active_alerts](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-component-health-active-alerts-transact-sql)<br /><br />[sys.dm_pdw_component_health_status](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-component-health-status-transact-sql)|  
|Appliance components and their status|[sys.pdw_health_component_groups](/sql-docs/docs/relational-databases/system-catalog-views/sys-pdw-health-component-groups-transact-sql)<br /><br />[sys.pdw_health_components](/sql-docs/docs/relational-databases/system-catalog-views/sys-pdw-health-components-transact-sql)<br /><br />[sys.pdw_health_component_properties](/sql-docs/docs/relational-databases/system-catalog-views/sys-pdw-health-component-properties-transact-sql)<br /><br />[sys.pdw_health_component_status_mappings](/sql-docs/docs/relational-databases/system-catalog-views/sys-pdw-health-component-status-mappings-transact-sql)<br /><br />[sys.dm_pdw_nodes](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-transact-sql)|  
|Monitor requests (including queries, loads, backups, and restores)|[sys.dm_pdw_exec_sessions](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-sessions-transact-sql)<br /><br />[sys.dm_pdw_exec_requests](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql)<br /><br />[sys.dm_pdw_request_steps](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-request-steps-transact-sql)<br /><br />[sys.dm_pdw_sql_requests](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-sql-requests-transact-sql)<br /><br />[sys.dm_pdw_dms_workers](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-dms-workers-transact-sql)<br /><br />[sys.dm_pdw_waits](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-waits-transact-sql)<br /><br />[sys.dm_pdw_errors](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-errors-transact-sql)<br /><br />[sys.pdw_distributions](/sql-docs/docs/relational-databases/system-catalog-views/sys-pdw-distributions-transact-sql)|  
|Monitor additional information for loads, backups, and restores.|[sys.pdw_loader_backup_runs](/sql-docs/docs/relational-databases/system-catalog-views/sys-pdw-loader-backup-runs-transact-sql)<br /><br />[sys.pdw_loader_backup_run_details](/sql-docs/docs/relational-databases/system-catalog-views/sys-pdw-loader-backup-run-details-transact-sql)<br /><br />[sys.pdw_loader_run_stages](/sql-docs/docs/relational-databases/system-catalog-views/sys-pdw-loader-run-stages-transact-sql)|  
|OS-level logs and performance information|[sys.dm_pdw_os_performance_counters](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-os-performance-counters-transact-sql)<br /><br />[sys.dm_pdw_os_event_logs](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-os-event-logs-transact-sql)<br /><br />[sys.dm_pdw_os_threads](/sql-docs/docs/relational-databases/system-dynamic-management-views/sys-dm-pdw-os-threads-transact-sql)|  
  
## See Also  
<!-- MISSING LINKS [Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  -->  
[Appliance Monitoring &#40;Analytics Platform System&#41;](appliance-monitoring.md)  
  
