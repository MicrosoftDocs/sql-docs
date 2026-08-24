---
title: "Start SQL Server with Minimal Configuration"
description: Become familiar with the minimal configuration startup option in SQL Server. See when and how to use it, and learn about how it limits functionality.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
helpviewer_keywords:
  - "minimal configuration [SQL Server]"
  - "starting SQL Server, minimal configuration"
---
# Start SQL Server with minimal configuration

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

If you have configuration problems that prevent the server from starting, you can start an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using the minimal configuration startup option. This is the startup option `-f`. Starting an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with minimal configuration automatically puts the server in single-user mode.

Consider the following conditions when you start an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in minimal configuration mode:

- Only a single user can connect, and the `CHECKPOINT` process isn't executed.

- Remote access and read-ahead are disabled.

- Startup stored procedures don't run.

- `tempdb` is configured at the smallest possible size.

- Audit is disabled, but Audit DDL can still be issued. In practice, `-m` should be sufficient for most cases that require SQL Serve Audit reconfiguration. For more information about security in Auditing configuration, see [Auditing in SQL Server](/previous-versions/sql/sql-server-2008/dd392015(v=sql.100)#security).

After the server has been started with minimal configuration, you should change the appropriate server option value or values, stop, and then restart the server.

> [!IMPORTANT]  
> Use the **sqlcmd** utility and the dedicated administrator connection (DAC) to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you use a typical connection, stop the SQL Server Agent service before connecting to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in minimal configuration mode. Otherwise, the SQL Server Agent service uses the connection, which blocks it.

## Related content

- [Start, Stop, or Pause the SQL Server Agent Service](/ssms/agent/start-stop-or-pause-the-sql-server-agent-service)
- [Diagnostic connection for database administrators](diagnostic-connection-for-database-administrators.md)
- [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Database Engine Service startup options](database-engine-service-startup-options.md)
