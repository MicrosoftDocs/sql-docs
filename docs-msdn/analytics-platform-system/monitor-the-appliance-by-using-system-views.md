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
|Overall status of the appliance|[sys.dm_pdw_sys_info](https://msdn.microsoft.com/library/mt203900.aspx)|  
|Alerts|[sys.pdw_health_alerts](https://msdn.microsoft.com/library/mt631623.aspx)<br /><br />[sys.dm_pdw_component_health_alerts](https://msdn.microsoft.com/library/mt631629.aspx)<br /><br />[sys.dm_pdw_component_health_active_alerts](https://msdn.microsoft.com/library/mt631630.aspx)<br /><br />[sys.dm_pdw_component_health_status](https://msdn.microsoft.com/library/mt631628.aspx)|  
|Appliance components and their status|[sys.pdw_health_component_groups](https://msdn.microsoft.com/library/mt631620.aspx)<br /><br />[sys.pdw_health_components](https://msdn.microsoft.com/library/mt631622.aspx)<br /><br />[sys.pdw_health_component_properties](https://msdn.microsoft.com/library/mt631621.aspx)<br /><br />[sys.pdw_health_component_status_mappings](https://msdn.microsoft.com/library/mt631624.aspx)<br /><br />[sys.dm_pdw_nodes](https://msdn.microsoft.com/library/mt203907.aspx)|  
|Monitor requests (including queries, loads, backups, and restores)|[sys.dm_pdw_exec_sessions](https://msdn.microsoft.com/library/mt203883.aspx)<br /><br />[sys.dm_pdw_exec_requests](https://msdn.microsoft.com/library/mt203887.aspx)<br /><br />[sys.dm_pdw_request_steps](https://msdn.microsoft.com/library/mt203913.aspx)<br /><br />[sys.dm_pdw_sql_requests](https://msdn.microsoft.com/library/mt203889.aspx)<br /><br />[sys.dm_pdw_dms_workers](https://msdn.microsoft.com/library/mt203878.aspx)<br /><br />[sys.dm_pdw_waits](https://msdn.microsoft.com/library/mt203893.aspx)<br /><br />[sys.dm_pdw_errors](https://msdn.microsoft.com/library/mt203904.aspx)<br /><br />[sys.pdw_distributions](https://msdn.microsoft.com/library/mt203892.aspx)|  
|Monitor additional information for loads, backups, and restores.|[sys.pdw_loader_backup_runs](https://msdn.microsoft.com/library/mt203884.aspx)<br /><br />[sys.pdw_loader_backup_run_details](https://msdn.microsoft.com/library/mt203877.aspx)<br /><br />[sys.pdw_loader_run_stages](https://msdn.microsoft.com/library/mt203879.aspx)|  
|OS-level logs and performance information|[sys.dm_pdw_os_performance_counters](https://msdn.microsoft.com/library/mt203875.aspx)<br /><br />[sys.dm_pdw_os_event_logs](https://msdn.microsoft.com/library/mt203910.aspx)<br /><br />[sys.dm_pdw_os_threads](https://msdn.microsoft.com/library/mt203917.aspx)|  
  
## See Also  
<!-- MISSING LINKS [Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  -->  
[Appliance Monitoring &#40;Analytics Platform System&#41;](appliance-monitoring.md)  
  
