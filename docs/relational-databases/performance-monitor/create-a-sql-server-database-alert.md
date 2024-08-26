---
title: "Create a SQL Server database alert"
description: You can use Windows System Monitor to create a SQL Server database alert that is raised when a threshold value for a System Monitor counter has been reached.
author: MikeRayMSFT
ms.author: mikeray
ms.date: 12/04/2023
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "database performance [SQL Server], alerts"
  - "alerts [SQL Server], creating"
  - "thresholds [SQL Server]"
  - "database alerts [SQL Server]"
  - "tuning databases [SQL Server], alerts"
  - "monitoring performance [SQL Server], alerts"
  - "monitoring server performance [SQL Server], alerts"
  - "database monitoring [SQL Server], alerts"
  - "server performance [SQL Server], alerts"
---
# Create a SQL Server database alert
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sql-windows-only.md)]

  If you are running Microsoft Windows server operating system, use the [Performance Monitor](monitor-resource-usage-system-monitor.md) to measure the performance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can use Performance Monitor to create an alert that is raised when a threshold value for a Performance Monitor counter has been reached. In response to the alert, Performance Monitor launches an application, such as a custom application written to handle the alert condition. For example, you could create an alert that is raised when the number of deadlocks exceeds a specific value.  
  
 Alerts also can be defined by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent. For more information, see [Alerts](../../ssms/agent/alerts.md).  
  
 For more information about using Performance Monitor to set up a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database alert, see [Set Up a SQL Server Database Alert (Windows)](../../relational-databases/performance/set-up-a-sql-server-database-alert-windows.md) .  
  
## Related content

- [sp_add_alert (Transact-SQL)](../system-stored-procedures/sp-add-alert-transact-sql.md)
- [sys.sysperfinfo (Transact-SQL)](../system-compatibility-views/sys-sysperfinfo-transact-sql.md)
