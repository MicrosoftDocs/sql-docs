---
title: "View the Windows Application Log (Windows) | Microsoft Docs"
ms.custom: ""
ms.date: "02/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "viewing logs"
  - "application logs [SQL Server]"
  - "logs [SQL Server], application"
  - "monitoring Windows NT application log [SQL Server]"
  - "Windows NT application logs [SQL Server]"
  - "displaying logs"
  - "monitoring [SQL Server], events"
  - "logs [SQL Server], viewing"
ms.assetid: 168a6c6e-12df-46a9-9904-55d63ca8fe14
caps.latest.revision: 25
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# View the Windows Application Log (Windows 10)
  When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to use the Windows application log, each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] session writes new events to that log. Unlike the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, a new application log is not created each time you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### View the Windows application log  
  
1.  On the **Search bar** , type **Event Viewer**, then select the Event Viewer desktop app.
  
2.  In Event Viewer, open the **Applications and Services Logs**.    
3.  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events are identified by the entry **MSSQLSERVER** (named instances are identified with **MSSQL$***<instance_name>*) in the **Source** column. SQL Server Agent events are identified by the entry SQLSERVERAGENT (for named instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent events are identified with **SQLAgent$**\<*instance_name*>). Microsoft Search service events are identified by the entry **Microsoft Search**.  
  
4.  To view the log of a different computer, right-click **Event Viewer (local)**, select **Connect to another computer,** and complete the **Select Computer**dialog box.  
  
5.  Optionally, to display only [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events, on the **View** menu click **Filter**, and in the **Event source** list, select **MSSQLSERVER**. To view only [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent events, instead select **SQLSERVERAGENT** in the **Event source** list.  
  
6.  To view more information about an event, double-click the event.  
  
## See Also  
 [View the SQL Server Error Log &#40;SQL Server Management Studio&#41;](../../relational-databases/performance/view-the-sql-server-error-log-sql-server-management-studio.md)  
  
  
