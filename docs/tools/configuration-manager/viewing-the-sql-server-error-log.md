---
title: "Viewing the SQL Server Error Log"
description: Get help detecting problems in SQL Server by viewing the current error log, or backups of previous logs, to check whether processes finished successfully.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: how-to
ms.collection:
  - data-tools
helpviewer_keywords:
  - "cycling SQL Server error log"
  - "viewing SQL Server error log"
  - "errors [SQL Server], logs"
  - "SQL Server error log"
  - "displaying SQL Server error log"
  - "logs [SQL Server], SQL Server error logs"
monikerRange: ">=sql-server-2017"
---
# View the SQL Server error log

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sqlserver.md)]

View the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log to ensure that processes complete successfully (for example, backup and restore operations, batch commands, or other scripts and processes). This procedure can be helpful to detect any current or potential problem areas, including automatic recovery messages (particularly if an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] stopped and restarted), kernel messages, or other server-level error messages.

## How to view the error log

You can view the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log with [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or any text editor. For more information about how to view the error log, see [Open Log File Viewer](../../relational-databases/logs/open-log-file-viewer.md).

- On Windows, the default location for the error log is `<drive>:\Program Files\Microsoft SQL Server\MSSQL.<n>\MSSQL\LOG\ERRORLOG`.

- On Linux, the default location for the error log is `/var/opt/mssql/log`.

> [!NOTE]  
> The files are called `ERRORLOG.<n>`, where `<n>` is an integer. The current error log has no extension.

A new error log is created each time an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is started, although the [sp_cycle_errorlog](../../relational-databases/system-stored-procedures/sp-cycle-errorlog-transact-sql.md) system stored procedure can be used to cycle the error log files without having to restart the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Typically, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] retains backups of the previous six logs, and gives the most recent log backup the extension `.1`, the second most recent the extension `.2`, and so on. The current error log has no extension.

You can also view the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log on instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are offline or can't start. For more information, see [View Offline Log Files](../../relational-databases/logs/view-offline-log-files.md).

## Related content

- [Open Log File Viewer](../../relational-databases/logs/open-log-file-viewer.md)
- [sp_cycle_errorlog (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-cycle-errorlog-transact-sql.md)
- [View Offline Log Files](../../relational-databases/logs/view-offline-log-files.md)
