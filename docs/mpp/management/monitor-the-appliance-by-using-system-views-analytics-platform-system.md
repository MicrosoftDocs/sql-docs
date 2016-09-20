---
title: "Monitor the Appliance by Using System Views (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d6e34c16-12ba-4a6f-80a7-391bca5ba534
caps.latest.revision: 8
author: BarbKess
---
# Monitor the Appliance by Using System Views (Analytics Platform System)
This topic lists the system views that you can use for monitoring SQL Server PDW.  
  
## To Monitor the Appliance by Using System Views  
SQL Server PDW includes comprehensive system views that enable you to obtain detailed information about the appliance health, state, and performance. This table provides links to system views that can be used for each monitoring capability.  
  
![PDW system views alerts](../../mpp/management/media/PDW_system_views_alerts.png "PDW_system_views_alerts")  
  
|||  
|-|-|  
|**Information Type**|**Related System Views**|  
|Overall status of the appliance|[sys.dm_pdw_sys_info &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-sys-info-sql-server-pdw.md)|  
|Alerts|[sys.pdw_health_alerts &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-health-alerts-sql-server-pdw.md)<br /><br />[sys.dm_pdw_component_health_alerts &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-component-health-alerts-sql-server-pdw.md)<br /><br />[sys.dm_pdw_component_health_active_alerts &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-component-health-active-alerts-sql-server-pdw.md)<br /><br />[sys.dm_pdw_component_health_status &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-component-health-status-sql-server-pdw.md)|  
|Appliance components and their status|[sys.pdw_health_component_groups &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-health-component-groups-sql-server-pdw.md)<br /><br />[sys.pdw_health_components &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-health-components-sql-server-pdw.md)<br /><br />[sys.pdw_health_component_properties &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-health-component-properties-sql-server-pdw.md)<br /><br />[sys.pdw_health_component_status_mappings &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-health-component-status-mappings-sql-server-pdw.md)<br /><br />[sys.dm_pdw_nodes &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-nodes-sql-server-pdw.md)|  
|Monitor requests (including queries, loads, backups, and restores)|[sys.dm_pdw_exec_sessions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-exec-sessions-sql-server-pdw.md)<br /><br />[sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md)<br /><br />[sys.dm_pdw_request_steps &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-request-steps-sql-server-pdw.md)<br /><br />[sys.dm_pdw_sql_requests &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-sql-requests-sql-server-pdw.md)<br /><br />[sys.dm_pdw_dms_workers &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-dms-workers-sql-server-pdw.md)<br /><br />[sys.dm_pdw_waits &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-waits-sql-server-pdw.md)<br /><br />[sys.dm_pdw_errors &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-errors-sql-server-pdw.md)<br /><br />[sys.pdw_distributions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-distributions-sql-server-pdw.md)|  
|Monitor additional information for loads, backups, and restores.|[sys.pdw_loader_backup_runs &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-loader-backup-runs-sql-server-pdw.md)<br /><br />[sys.pdw_loader_backup_run_details &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-loader-backup-run-details-sql-server-pdw.md)<br /><br />[sys.pdw_loader_run_stages &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-loader-run-stages-sql-server-pdw.md)|  
|OS-level logs and performance information|[sys.dm_pdw_os_performance_counters &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-os-performance-counters-sql-server-pdw.md)<br /><br />[sys.dm_pdw_os_event_logs &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-os-event-logs-sql-server-pdw.md)<br /><br />[sys.dm_pdw_os_threads &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-os-threads-sql-server-pdw.md)|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Appliance Monitoring &#40;Analytics Platform System&#41;](../../mpp/management/appliance-monitoring-analytics-platform-system.md)  
  
