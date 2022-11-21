---
title: "Start SQL Server in Single-User Mode"
description: "Learn about single-user mode in SQL Server. See when it is useful and how to use the startup option -m to start an instance of SQL Server in this mode."
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "starting SQL Server, single-user mode"
  - "single-user mode [SQL Server]"
---
# Start SQL Server in single-user mode

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Under certain circumstances, you may have to start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode by using the **startup option** `-m`. For example, you may want to change server configuration options or recover a damaged `master` database or other system database. Both actions require starting an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode.

For restoring a `master` database on Linux in single-user mode, see [Restore the master database on Linux in single-user mode](../../linux/sql-server-linux-restore-master-database-in-single-user-mode.md).

Starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode enables any member of the computer's local Administrators group to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a member of the **sysadmin** fixed server role. For more information, see [Connect to SQL Server when system administrators are locked out](connect-to-sql-server-when-system-administrators-are-locked-out.md).

When you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode, note the following:

- Only one user can connect to the server.

- The CHECKPOINT process isn't executed. By default, it is executed automatically at startup.

> [!NOTE]  
> Stop the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service before connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode; otherwise, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service uses the connection, thereby blocking it.

When you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] can connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Object Explorer in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] might fail because it requires more than one connection for some operations. To manage [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode, execute [!INCLUDE[tsql](../../includes/tsql-md.md)] statements by connecting through the Query Editor in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or Azure Data Studio, or use the [sqlcmd utility](../../tools/sqlcmd-utility.md).

When you use the `-m` option with `SQLCMD` or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you can limit the connections to a specified client application.

> [!NOTE]  
> On Linux, `SQLCMD` must be capitalized as shown.

For example, `-m"SQLCMD"` limits connections to a single connection and that connection must identify itself as the **sqlcmd** client program. Use this option when you're starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode and an unknown client application is taking the only available connection. To connect through the Query Editor in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], use `-m"Microsoft SQL Server Management Studio - Query"`.

> [!IMPORTANT]  
> Don't use this option as a security feature. The client application provides the client application name, and can provide a false name as part of the connection string.

The following example starts the SQL Server instance in single-user mode and only allows connection through the SQL Server Management Studio Query Editor.

```console
net start "SQL Server (MSSQLSERVER)" /m"Microsoft SQL Server Management Studio - Query"
```

## Note for clustered installations

For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation in a clustered environment, when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started in single user mode, the cluster resource dll uses up the available connection thereby blocking any other connections to the server. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is in this state, if you try to bring [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent resource online, it may fail over the SQL resource to a different node if the resource is configured to affect the group.

To get around the problem use the following procedure:

1. Remove the `-m` startup parameter from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Advanced Properties.

1. Take the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource offline.

1. From the current owner node of this group, issue the following command from the command prompt:

   ```console
   net start MSSQLSERVER /m
   ```

1. Verify from the cluster administrator or failover cluster management console that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource is still offline.

1. Connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] now using the following command and do the necessary operation: SQLCMD -E -S\<servername>.

1. Once the operation is complete, close the command prompt and bring back the SQL and other resources online through cluster administrator.

## See also

- [Restore the master database on Linux in single-user mode](../../linux/sql-server-linux-restore-master-database-in-single-user-mode.md)
- [Start, Stop, or Pause the SQL Server Agent Service](../../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md)
- [Diagnostic Connection for Database Administrators](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md)
- [sqlcmd Utility](../../tools/sqlcmd-utility.md)
- [CHECKPOINT &#40;Transact-SQL&#41;](../../t-sql/language-elements/checkpoint-transact-sql.md)
- [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md)
