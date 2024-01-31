---
title: "Open Activity Monitor (SSMS)"
description: Learn how to open Activity Monitor in SQL Server Management Studio (SSMS). Activity Monitor queries the monitored instance to obtain information to display.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/04/2023
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "Activity Monitor [SQL Server], setting the refresh interval"
  - "refresh interval for Activity Monitor"
  - "Activity Monitor [SQL Server], opening"
  - "opening Activity Monitor"
---
# Open Activity Monitor in SQL Server Management Studio (SSMS)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

 [Activity Monitor](activity-monitor.md) runs queries on the monitored instance to obtain information for the Activity Monitor display panes. When the refresh interval is set to less than 10 seconds, the time that is used to run these queries can affect server performance. We recommend that you [download and install the latest version of SSMS](../../ssms/download-sql-server-management-studio-ssms.md).  
  
## <a id="Permissions"></a> Permissions
 To view actual activity, you must have VIEW SERVER STATE permission. To view the Data File I/O section of Activity Monitor, you must have CREATE DATABASE, ALTER ANY DATABASE, or VIEW ANY DEFINITION permission in addition to VIEW SERVER STATE.  
  
 To KILL a process, a user must be a member of the `sysadmin` or `processadmin` fixed server roles.  

## Open Activity Monitor

### Object Explorer

Right-click on the top-level object for a SQL Server connection, and select **Activity Monitor**.

### Toolbar

From the Standard toolbar, select the **Activity Monitor** icon. It is in the middle, just to the right of the undo/redo buttons. To aid in finding it, hover over each icon until you find the **Activity Monitor**. 
  
Complete the **Connect to Server** dialog box if you aren't already connected to an instance of SQL Server you want to monitor.
  
## Launch Activity Monitor and Object Explorer on startup
  
1. From the **Tools** menu, select **Options**.  
  
1. In the **Options** dialog box, expand **Environment**, and then select **Startup**.  
  
1. From the **At startup** dropdown list, select **Open Object Explorer and Activity Monitor**.  

1. Select **OK**.

    :::image type="content" source="media/open-activity-monitor-sql-server-management-studio/open-object-explorer.png" alt-text="Screenshot of the SQL Server Management Studio Options, showing the Startup page.":::

## Set the Activity Monitor refresh interval
  
1. Open the Activity Monitor.  
  
1. Right-click **Overview**, select **Refresh Interval**, and then select the interval in which Activity Monitor should obtain new instance information.  

## Related content

- [Activity Monitor](activity-monitor.md)