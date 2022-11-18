---
title: "View the Windows application log (Windows) | Microsoft Docs"
description: When SQL Server is configured to use the Windows application log, each session writes events to that log. Learn how to view the Windows application log.
ms.custom: ""
ms.date: 12/13/2021
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing logs"
  - "application logs [SQL Server]"
  - "logs [SQL Server], application"
  - "monitoring Windows NT application log [SQL Server]"
  - "Windows NT application logs [SQL Server]"
  - "displaying logs"
  - "monitoring [SQL Server], events"
  - "logs [SQL Server], viewing"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# View the Windows application log
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to use the Windows application log, each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] session writes new events to that log. Unlike the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, a new application log is not created each time you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

  This article covers Windows 10 operating systems and later.
  
## View the Windows application log  
  
1. On the **Search bar**, type **Event Viewer**, and then select the **Event Viewer** desktop app.
  
2. In **Event Viewer**, expand the **Windows Logs** folder, and select the **Application** event log.

3. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events are identified by the entry **MSSQLSERVER** (named instances are identified with **MSSQL$**_<instance_name>_) in the **Source** column. SQL Server Agent events are identified by the entry SQLSERVERAGENT (for named instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent events are identified with **SQLAgent$**\<*instance_name*>). Microsoft Search service events are identified by the entry **Microsoft Search**.  
  
4. To view the log of a different computer, right-click **Event Viewer (local)**. Select **Connect to another computer**, and fill in the fields to complete the **Select Computer** dialog box.  
  
5. Optionally, to display only [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events, on the **View** menu, select **Filter**. In the **Event source** list, select **MSSQLSERVER**. To view only [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent events, instead select **SQLSERVERAGENT** in the **Event source** list.  
  
6. To view more information about an event, double-click the event.  
  
## See also  
 [View the SQL Server error log &#40;SQL Server Management Studio&#41;](../../relational-databases/performance/view-the-sql-server-error-log-sql-server-management-studio.md)  
  
  
