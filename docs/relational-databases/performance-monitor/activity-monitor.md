---
title: "Activity Monitor"
description: Learn how to use Activity Monitor in SQL Server Management Studio to display information about SQL Server processes and how these processes affect the current instance of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/04/2023
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
helpviewer_keywords:
  - "Activity Monitor [SQL Server]"
---
# Activity Monitor
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Activity Monitor in SQL Server Management Studio (SSMS) displays information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] processes and how these processes affect the current instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Install the latest version of [SQL Server Management Studio (SSMS)](/ssms/install/install).
  
Activity Monitor is a tabbed document window with the following expandable and collapsible panes: **Overview**, **Processes**, **Resource Waits**, **Data File I/O**, **Recent Expensive Queries**, and **Active Expensive Queries**. When any pane is expanded, Activity Monitor queries the instance for information. When a pane is collapsed, all querying activity stops for that pane. You can  expand one or more panes at the same time to view different kinds of activity on the instance.  
 
## Customize columns
For columns included in the **Processes**, **Resource Waits**, **Data File I/O**, **Recent Expensive Queries**, and **Active Expensive Queries** panes, customize the display as follows:  
  
1. To rearrange column order, select the column heading and drag it to another location in the heading ribbon.  
  
1. To sort a column, select the column name.  
  
1. To filter on one or more columns, select the dropdown list arrow in the column heading, and then select a value.  

## Related content

- [Open Activity Monitor in SQL Server Management Studio (SSMS)](open-activity-monitor-sql-server-management-studio.md)
- [Server Performance and Activity Monitoring](../performance/server-performance-and-activity-monitoring.md)
