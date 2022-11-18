---
title: "Set up a SQL Server database alert (Windows) | Microsoft Docs"
description: Learn how to create an alert that is raised when a System Monitor counter reaches a threshold value. In response, System Monitor can launch an application.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "alerts [SQL Server], creating"
ms.assetid: 65d2c5c1-921f-4eff-9ef7-149170ab61e8
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Set up a SQL Server database alert (Windows)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  You can use System Monitor to create an alert that is raised when a System Monitor counter reaches a threshold value. In response to the alert, System Monitor can launch an application, such as a custom application written to handle the alert condition. For example, you can create an alert that is raised when the number of deadlocks exceeds a specific value. 
  
 Alerts also can be defined by using Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. For more information, see [Alerts](../../ssms/agent/alerts.md).  
  
## Set up a SQL Server database alert  
  
1. On the navigation tree of the **Performance** window, expand **Performance Logs and Alerts**.  
  
2. Right-click **Alerts**, and then select **New Alert Settings**.
  
3. In the **New Alert Settings** dialog box, type a name for the new alert, and then select **OK**.  
  
4. On the **General** tab of the dialog box for the new alert, add a **Comment**. Select **Add** to add a counter to the alert.  
  
     All alerts must have at least one counter.  
  
5. In the **Add Counters** dialog box, select a SQL Server object from the **Performance Object** list. Select a counter from the **Select counters from** list.  
  
6. To add the counter to the alert, select **Add**. You can continue to add counters, or you can select **Close** to return to the dialog box for the new alert.  
  
7. In the new alert dialog box, select either **Over** or **Under** in the **Alert when the value is** list. Then enter a threshold value in **Limit**.  
  
     The alert is generated when the value for the counter is more than or less than the threshold value (depending on whether you selected **Over** or **Under**).  
  
8. In the **Sample data every** boxes, set the sampling frequency.  
  
9. On the **Action** tab, set actions to occur every time the alert is triggered.  
  
10. On the **Schedule** tab, set the start and stop schedule for the alert scan.  
  
## See also  
 [Create a SQL Server database alert](../../relational-databases/performance-monitor/create-a-sql-server-database-alert.md)  
  
  
