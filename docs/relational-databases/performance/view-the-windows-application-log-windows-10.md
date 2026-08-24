---
title: "View the Windows Application Log (Windows)"
description: When SQL Server is configured to use the Windows application log, each session writes events to that log. Learn how to view the Windows application log.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 12/26/2024
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "viewing logs"
  - "application logs [SQL Server]"
  - "logs [SQL Server], application"
  - "monitoring Windows NT application log [SQL Server]"
  - "Windows NT application logs [SQL Server]"
  - "displaying logs"
  - "monitoring [SQL Server], events"
  - "logs [SQL Server], viewing"
---

# View the Windows application log

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is configured to use the Windows application log, each [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] session writes new events to that log. Unlike the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log, a new application log is not created each time you start an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

  This article covers Windows 10 operating systems and later.

## To view the Windows application log

1. On the **Search bar**, type **Event Viewer**, and then select the **Event Viewer** desktop app.

1. In **Event Viewer**, expand the **Windows Logs** folder, and select the **Application** event log.

1. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] events are identified by the entry **MSSQLSERVER** (named instances are identified with **MSSQL$**_<instance_name>_) in the **Source** column. SQL Server Agent events are identified by the entry SQLSERVERAGENT (for named instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent events are identified with **SQLAgent$**\<*instance_name*>). Microsoft Search service events are identified by the entry **Microsoft Search**.

1. To view the log of a different computer, right-click **Event Viewer (local)**. Select **Connect to another computer**, and fill in the fields to complete the **Select Computer** dialog box.

1. Optionally, to display only [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] events, on the **View** menu, select **Filter**. In the **Event source** list, select **MSSQLSERVER**. To view only [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent events, instead select **SQLSERVERAGENT** in the **Event source** list.

1. To view more information about an event, double-click the event.

## Related content

- [View the SQL Server error log in SQL Server Management Studio (SSMS)](view-the-sql-server-error-log-sql-server-management-studio.md)
