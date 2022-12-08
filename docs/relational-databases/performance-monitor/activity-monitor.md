---
title: "Activity Monitor"
description: Learn how to use Activity Monitor in SQL Server Management Studio to display information about SQL Server processes and how these processes affect the current instance of SQL Server.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Activity Monitor [SQL Server]"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Activity Monitor
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Activity Monitor in SQL Server Management Studio (SSMS) displays information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] processes and how these processes affect the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. We recommend that you [download and install the latest version of SSMS](../../ssms/download-sql-server-management-studio-ssms.md).
  
Activity Monitor is a tabbed document window with the following expandable and collapsible panes: **Overview**, **Processes**, **Resource Waits**, **Data File I/O**, **Recent Expensive Queries**, and **Active Expensive Queries**. When any pane is expanded, Activity Monitor queries the instance for information. When a pane is collapsed, all querying activity stops for that pane. You can  expand one or more panes at the same time to view different kinds of activity on the instance.  
 
## Customize columns 
For columns included in the **Processes**, **Resource Waits**, **Data File I/O**, **Recent Expensive Queries**, and **Active Expensive Queries** panes, customize the display as follows:  
  
1.  To rearrange column order, select the column heading and drag it to another location in the heading ribbon.  
  
2.  To sort a column, select the column name.  
  
3.  To filter on one or more columns, select the drop-down arrow in the column heading, and then select a value.  

## See also
   
|Description|Topic|  
|-|-|  
|Describes how to open Activity Monitor and how to set the Activity Monitor refresh interval.|[Open Activity Monitor &#40;SQL Server Management Studio&#41;](../../relational-databases/performance-monitor/open-activity-monitor-sql-server-management-studio.md)|  
|Links to topics for server performance and activity monitoring.|[Server Performance and Activity Monitoring](../../relational-databases/performance/server-performance-and-activity-monitoring.md)|  
  
  
