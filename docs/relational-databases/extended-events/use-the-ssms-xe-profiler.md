---
title: "Use the SSMS XEvent Profiler | Microsoft Docs"
ms.custom: ""
ms.date: "10/02/2016"
ms.prod: "sql-non-specified"
ms.reviewer: "genemi"
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "xevents"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "extended events [SQL Server], system health session"
  - "extended events [SQL Server], system_health session"
  - "system_health session [SQL Server extended events]"
  - "system health session [SQL Server extended events]"
ms.assetid: 1e1fad43-d747-4775-ac0d-c50648e56d78
author: "yualan"
ms.author: "alayu"
manager: "craigg"   
---
# Use the SSMS XEvent Profiler
The XEvent Profiler is a SQL Server Management Studio (SSMS) feature that displays a live viewer window of extended events. This overview describes the reasons for using this profiler, key features, and instructions to get started viewing extended events.

## Why would I use the XEvent Profiler?
As components of SQL Server Profiler get deprecated, the goal is to increase adoption of extended events. Profile-based tracing has server performance problems that extended events overcome by continuously being enhanced with each SQL release. These updates allow for additional features such as compatibility with Azure SQL Database because Azure cannot support Profiler traces.

XEvent Profiler launches a profiler-type XEvent session view as quickly as one can launch SQL Server Profiler. This feature also utilizes the customizable XEvent data viewer window that already exists in SSMS, which allows the user to customize the view window.

## Prerequisites
This feature is only available on SQL Server Management Studio (SSMS) v17.3 or later. Make sure you are using the latest version. You can find the latest version [here.](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

## <a id="getting-started"></a>Getting Started
To access the XEvent Profiler, follow these steps:

1. Open **SQL Server Management Studio**.
2. Connect to an instance of the SQL Server Database Engine or localhost.
3. In Object Explorer, find the XE Profiler menu item and expand it by clicking the '+' sign.

![XEProfiler Menu](media/xevents-xe-profiler-menu.png)

4. Double-click **Standard** if you want to view all extended events in this session. Click **T-SQL** if you want to view the logged SQL statements. If a session is not already created, a session is created for you.

![XEProfiler Session](media/xevents-xe-profiler-start-session.png)

5. You can now view your Extended Events.

![XEProfiler Viewer](media/xevents-xe-profiler-start-viewer.png)

## See Also
[Extended Events](../../relational-databases/extended-events/extended-events.md)  
[Extended Events Tools](../../relational-databases/extended-events/extended-events-tools.md)  
  
  
