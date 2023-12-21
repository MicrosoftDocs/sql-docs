---
title: Use System Center Operations Manager to monitor APS
description: Use System Center Operations Manager (SCOM) to monitor the Analytics Platform System (APS) appliance.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
---

# Monitor with System Center Operations Manager - Analytics Platform System
Use System Center Operations Manager (SCOM) to monitor the Analytics Platform System (APS) appliance.
  
## Before You Begin
  
### Prerequisites
  
1. System Center Operations Manager 2007 R2, 2012, or 2012 SP1 must be installed and running.  
  
1. SQL Server 2008 R2 Native Client or SQL Server 2012 Native Client must be installed.  
  
1. The management packs to monitor SQL Server PDW must be installed, imported, and configured. Use the following articles for instructions to perform these tasks.  
  
    -   [Install the Operations Manager Management Packs (Analytics Platform System)](install-the-scom-management-packs.md)  
  
    -   [Import the Operations Manager Management Pack for PDW (Analytics Platform System)](import-the-scom-management-pack-for-pdw.md) 
    
    -   [Configure Operations Manager to Monitor Analytics Platform System (Analytics Platform System)](configure-scom-to-monitor-analytics-platform-system.md)
  
<!-- MISSING LINKS    -   [Import the SCOM Management Pack for HDInsight (Analytics Platform System)](import-the-scom-management-pack-for-hdinsight.md)  -->  

  
## <a id="to-monitor-sql-server-pdw-with-scom"></a> Monitor SQL Server PDW with Operations Manager
After configuring the Operations Manager Management Packs, select the Monitoring Pane of Operations Manager and drill down to **SQL Server Appliance** and then **Microsoft SQL Server Parallel Data Warehouse**. Underneath Microsoft SQL Server Parallel Data Warehouse, there are four choices: **Alerts**, **Appliances**, **Appliance Diagram**, and **Nodes**.  
  
### Alerts
Alerts are where you can find the current alerts to manage.  
  
:::image type="content" source="./media/monitor-the-appliance-by-using-system-center-operations-manager/scom-alerts.png" alt-text="A screenshot of the Monitoring window, showing Alerts.":::
  
### Appliances
The Appliances page is where you find the currently discovered and monitored SQL Server PDW Appliances in your environment. If an appliance doesn't show up here and you have created the ODBC connection for it, then there might be something wrong with your PDWWatcher account. If they show up as "Not monitored", there might be something wrong with your PDWMonitor account. Be patient since Operations Manager doesn't make changes in real time, but periodically checks for new appliances to monitor and periodically sends queries to appliances for monitoring.  
  
:::image type="content" source="./media/monitor-the-appliance-by-using-system-center-operations-manager/scom-appliances.png" alt-text="A screenshot of the Monitoring window, showing Appliances.":::
  
### Appliances Diagram
The Appliances Diagram Page is where you can get a look at the health of your appliance with a tree view:  
  
:::image type="content" source="./media/monitor-the-appliance-by-using-system-center-operations-manager/scom-appliances-diagram.png" alt-text="A screenshot of the Appliances diagram.":::
  
### Nodes
Finally, the Nodes view allows you to see the health of your appliance through each node:  

:::image type="content" source="./media/monitor-the-appliance-by-using-system-center-operations-manager/scom-nodes.png" alt-text="A screenshot of the Monitoring window, showing Nodes.":::
  
## Related content

- [Monitor the appliance with system views - Analytics Platform System](monitor-the-appliance-by-using-system-views.md)
- [Admin Console alerts in Analytics Platform System](understanding-admin-console-alerts.md)
