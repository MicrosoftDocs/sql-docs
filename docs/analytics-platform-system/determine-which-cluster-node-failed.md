---
title: Determine failed cluster node - Analytics Platform System | Microsoft Docs
description: This article describes how to determine the name of the Analytics Platform System (APS) node that failed after a cluster failover has occurred and a cluster failover alert has been raised. As part of troubleshooting a cluster failover, you must determine the name of the node that failed before contacting Microsoft to help resolve the problem.  
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Determine which cluster node failed for Analytics Platform System
This topic describes how to determine the name of the Analytics Platform System (APS) node that failed after a cluster failover has occurred and a cluster failover alert has been raised. As part of troubleshooting a cluster failover, you must determine the name of the node that failed before contacting Microsoft to help resolve the problem.  
  
## <a name="Background"></a>Background  
For high availability in SQL Server PDW, the Control node and the Compute nodes are configured as active or passive components of Windows failover clusters. When an active server fails to respond to critical system requests, the passive server fails over and performs the functions of the server that failed.  
  
After a cluster failover, when SQL Server PDW reports on node status, the passive server has a failed over status. However, it is not obvious which server or node failed, especially if the server that failed is still online. To troubleshoot the cluster failure, you must determine the name of the node that failed over.  
  
## <a name="AdminConsoleSolution"></a>Admin Console Solution  
  
#### To find the name of the node that failed  
  
1.  Open the Admin Console. For more information about the Admin Console, see [Monitor the Appliance by Using the Admin Console &#40;Analytics Platform System&#41;](monitor-the-appliance-by-using-the-admin-console.md). After failover occurs, the failover event is included in the number of alerts on the **HEALTH** page. There is an **HEALTH** page for the PDW region and for the fabric region of the appliance. Each Health page has an **ALERTS** tab. To learn more about an alert, click the Health page, the Alerts tab, and then click an alert.  
  
## <a name="SystemView"></a>System View Solution  
The following SQL statement shows how to use the [sys.dm_pdw_component_health_active_alerts](../relational-databases/system-dynamic-management-views/sys-dm-pdw-component-health-active-alerts-transact-sql.md) system view to find the name of the server that failed.  
  
```sql  
SELECT  
SUBSTRING( component_instance_id, 2, charindex(' ', component_instance_id, 1)-2) AS failed_node_name,  
create_time AS failover_time  
FROM sys.dm_pdw_component_health_active_alerts  
WHERE alert_id = 500139  
ORDER BY failed_node_name;  
```  
  
