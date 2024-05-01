---
title: Track appliance alerts
description: Track appliance alerts in Analytics Platform System.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
---

# Track appliance alerts in Analytics Platform System
This article explains how to use the Admin Console and system views to track alerts in a SQL Server PDW appliance.  
  
## <a id="to-track-appliance-alerts"></a> Track appliance alerts
SQL Server PDW creates alerts for hardware and software issues that need attention. Each alert contains a title and a description of the issue.  
  
SQL Server PDW logs alerts in the [sys.dm_pdw_component_health_alerts](../relational-databases/system-dynamic-management-views/sys-dm-pdw-component-health-alerts-transact-sql.md) DMV. The system retains a limit of 10,000 alerts and deletes the oldest alert first when the limit is exceeded.  
  
### View Alerts by using the Admin Console
There is an **Alerts** tab for the PDW region and for the fabric region of the appliance. After failover occurs, the failover event is included in the number of alerts on the page. There is a page for the PDW region and for the fabric region of the appliance. Each Health page has a tab. To learn more about an alert, select the **Health** page, the **Alerts** tab, and then select an alert.  
  
:::image type="content" source="./media/track-appliance-alerts/SQL_Server_PDW_AdminConsole_AlertsV2.png" alt-text="A screenshot of the Microsoft Analytics Platform System Configuration Manager, showing the PDW Admin Console Alerts.":::
  
On the **Alerts** page:  
  
-   To view the alert history, select on the **Review Alert History** link.  
  
-   To view the alert component and its current property values, select on the alert row.  
  
-   To view details about the node that raised the alert, select on the node name.  
  
### View Alerts by Using the System Views
To view alerts by using system views, query [sys.dm_pdw_component_health_active_alerts](../relational-databases/system-dynamic-management-views/sys-dm-pdw-component-health-active-alerts-transact-sql.md). This DMV shows alerts that have not been corrected. For help with triaging alerts and errors, use the [sys.dm_pdw_errors](../relational-databases/system-dynamic-management-views/sys-dm-pdw-errors-transact-sql.md) DMV.  
  
The following example is a common query for viewing the current alerts.  
  
```sql  
SELECT   
    aa.[pdw_node_id],  
    n.[name] AS [node_name],  
    g.[group_name] ,  
    c.[component_name] ,  
    aa.[component_instance_id] ,   
    a.[alert_name] ,  
    a.[state] ,  
    a.[severity] ,  
    aa.[current_value] ,  
    aa.[previous_value] ,  
    aa.[create_time] ,  
    a.[description]   
FROM [sys].[dm_pdw_component_health_active_alerts] AS aa  
    INNER JOIN sys.dm_pdw_nodes AS n   
        ON aa.[pdw_node_id] = n.[pdw_node_id]  
    INNER JOIN [sys].[pdw_health_components] AS c   
        ON aa.[component_id] = c.[component_id]  
    INNER JOIN [sys].[pdw_health_component_groups] AS g   
        ON c.[group_id] = g.[group_id]  
    INNER JOIN [sys].[pdw_health_alerts] AS a   
        ON aa.[alert_id] = a.[alert_id] and aa.[component_id] = c.[component_id]  
ORDER BY  
    a.alert_id ,  
    aa.[pdw_node_id];  
```  
  
## Related content

- [Monitor the appliance with system views - Analytics Platform System](monitor-the-appliance-by-using-system-views.md)
- [Appliance monitoring for Analytics Platform System](appliance-monitoring.md)
