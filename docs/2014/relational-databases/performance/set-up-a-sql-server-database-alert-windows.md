---
title: "Set Up a SQL Server Database Alert (Windows) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "alerts [SQL Server], creating"
ms.assetid: 65d2c5c1-921f-4eff-9ef7-149170ab61e8
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Set Up a SQL Server Database Alert (Windows)
  Using System Monitor, you can create an alert to be raised when a threshold value for a System Monitor counter has been reached. In response to the alert, System Monitor can launch an application, such as a custom application written to handle the alert condition. For example, you can create an alert that is raised when the number of deadlocks exceeds a specific value.  
  
 Alerts also can be defined using Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. For more information, see [Alerts](../../ssms/agent/alerts.md).  
  
### To set up a SQL Server database alert  
  
1.  On the navigation tree of the Performance window, expand **Performance Logs and Alerts**.  
  
2.  Right-click **Alerts**, and then click **New Alert Settings**.  
  
3.  In the **New Alert Settings** dialog box, type a name for the new alert, and then click **OK**.  
  
4.  On the **General** tab of the dialog box for the new alert, add a **Comment**, and click **Add** to add a counter to the alert.  
  
     All alerts must have at least one counter.  
  
5.  In the Add Counters dialog box, select a SQL Server object from the **Performance Object** list, and then select a counter from the **Select counters from list**.  
  
6.  To add the counter to the alert, click **Add**. You can continue to add counters, or you can click **Close** to return to the dialog box for the new alert.  
  
7.  In the new alert dialog box, click either **Over** or **Under**in the **Alert when the value is** list, and then enter a threshold value in **Limit**.  
  
     The alert is generated when the value for the counter is more than or less than the threshold value (depending on whether you clicked **Over** or **Under**).  
  
8.  In the **Sample data every** boxes, set the sampling frequency.  
  
9. On the **Action** tab, set actions to occur every time the alert is triggered.  
  
10. On the **Schedule** tab, set the start and stop schedule for the alert scan.  
  
## See Also  
 [Create a SQL Server Database Alert](../performance-monitor/create-a-sql-server-database-alert.md)  
  
  
