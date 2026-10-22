---
title: "Broadcast a Shutdown Message (Command Prompt)"
description: Find out how to use the net send command to broadcast a message in SQL Server. See how to determine which users are currently connected to SQL Server.
author: adbadram
ms.author: adbadram
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "SQL Server, stopping"
  - "named instances [SQL Server], broadcasting shutdown messages"
  - "shutdown message broadcast"
  - "broadcasting shutdown message"
  - "command prompt [SQL Server], broadcasting shutdown messages"
  - "default instances [SQL Server], broadcasting shutdown messages"
  - "stopping SQL Server"
---
# Broadcast a shutdown message from the command prompt

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to broadcast a shutdown message in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using the `net send` command. In the message, include the time the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is going to stop, so that users can finish their tasks.

<a id="SSMSProcedure"></a>

## Broadcast a shutdown message

1. From a command prompt, enter:

   ```console
   net send /users "message"
   ```

   The `/users` option specifies that the message is sent to all users connected to the server.

> [!NOTE]  
> The `net send` command requires the messenger service to be running on both the sending and the receiving computers. The messenger service is disabled by default on Windows Server 2003. For information about `net send`, see the Windows documentation.

On your network, it might be more appropriate to contact users by e-mail or the telephone. To determine which users are currently connected to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], use the Activity Monitor. For information on the Activity Monitor, see [Activity Monitor](../../relational-databases/performance-monitor/activity-monitor.md) and [Open Activity Monitor in SQL Server Management Studio (SSMS)](../../relational-databases/performance-monitor/open-activity-monitor-sql-server-management-studio.md).

## Related content

- [Start, stop, pause, resume, and restart SQL Server services](start-stop-pause-resume-restart-sql-server-services.md)
