---
title: "Diagnostic Connection for Database Administrators"
description: "Find out about the dedicated administrator connection (DAC). View its restrictions, instructions on how to establish it, and examples demonstrating its use."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/12/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "server management [SQL Server], connections"
  - "administrator connections [SQL Server]"
  - "ports [SQL Server], DAC"
  - "DAC"
  - "network connections [SQL Server], dedicated administrator"
  - "diagnostic connections [SQL Server]"
  - "connections [SQL Server], dedicated administrator"
  - "ports [SQL Server]"
  - "dedicated administrator connections [SQL Server]"
---
# Diagnostic connection for database administrators

[!INCLUDE[sql-asdb](../../includes/applies-to-version/sql-asdb.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a special diagnostic connection for administrators when standard connections to the server aren't possible. This diagnostic connection allows an administrator to access [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to execute diagnostic queries and troubleshoot problems even when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] isn't responding to standard connection requests.

This dedicated administrator connection (DAC) supports encryption and other security features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The DAC only allows changing the user context to another admin user.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] makes every attempt to make DAC connect successfully, but under extreme situations it may not be successful.

## Connect with DAC

By default, the connection is only allowed from a client running on the server. Network connections aren't permitted unless they're configured by using the `sp_configure` stored procedure with the [remote admin connections option](../../database-engine/configure-windows/remote-admin-connections-server-configuration-option.md).

Only members of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sysadmin role can connect using the DAC.

The DAC is available and supported through the `sqlcmd` command-prompt utility using a special administrator switch (`-A`). For more information about using `sqlcmd`, see [Use sqlcmd with Scripting Variables](../../ssms/scripting/sqlcmd-use-with-scripting-variables.md). You can also connect prefixing `admin:` to the instance name in the format `sqlcmd -S admin:<instance_name>`. You can also initiate a DAC from a [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Query Editor by connecting to `admin:<instance_name>`.

To establish a DAC from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]:

- Disconnect all connections to the related SQL Server instance, including the Object Explorer and all open query windows.

- From the menu, select **File > New > Database Engine Query**

- From the connection dialog box in the Server Name field, enter `admin:<server_name>` if using the default instance or `admin:<server_name>\<instance_name>` if using a named instance.

## Restrictions

Because the DAC exists solely for diagnosing server problems in rare circumstances, there are some restrictions on the connection:

- To guarantee that there are resources available for the connection, only one DAC is allowed per instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If a DAC connection is already active, any new request to connect through the DAC is denied with error 17810.

- To conserve resources, [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] doesn't listen on the DAC port unless started with a trace flag 7806.

- The DAC initially attempts to connect to the default database associated with the login. After it is successfully connected, you can connect to the `master` database. If the default database is offline or otherwise not available, the connection will return error 4060. However, it will succeed if you override the default database to connect to the `master` database instead using the following command:

   ```powershell
   sqlcmd -A -d master
   ```

   We recommend that you connect to the `master` database with the DAC because `master` is guaranteed to be available if the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is started.

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prohibits running parallel queries or commands with the DAC. For example, error 3637 is generated if you execute either of the following statements with the DAC:

  - `RESTORE...`

  - `BACKUP...`

- Only limited resources are guaranteed to be available with the DAC. Don't use the DAC to run resource-intensive queries (for example. a complex join on large table) or queries that may block. This helps prevent the DAC from compounding any existing server problems. To avoid potential blocking scenarios, if you have to run queries that may block, run the query under snapshot-based isolation levels if possible; otherwise, set the transaction isolation level to READ UNCOMMITTED and set the LOCK_TIMEOUT value to a short value such as 2000 milliseconds, or both. This will prevent the DAC session from getting blocked. However, depending on the state that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is in, the DAC session might get blocked on a latch. You might be able to terminate the DAC session using CTRL-C but it isn't guaranteed. In that case, your only option may be to restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

- To guarantee connectivity and troubleshooting with the DAC, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] reserves limited resources to process commands run on the DAC. These resources are typically only enough for simple diagnostic and troubleshooting functions, such as those listed below.

Although you can theoretically run any [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that doesn't have to execute in parallel on the DAC, we strongly recommend that you restrict usage to the following diagnostic and troubleshooting commands:

- Querying dynamic management views for basic diagnostics such as [sys.dm_tran_locks](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md) for the locking status, [sys.dm_os_memory_cache_counters](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-cache-counters-transact-sql.md) to check the health of caches, and [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) and [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md) for active sessions and requests. Avoid dynamic management views that are resource intensive (for example, [sys.dm_tran_version_store](../../relational-databases/system-dynamic-management-views/sys-dm-tran-version-store-transact-sql.md) scans the full version store and can cause extensive I/O) or that use complex joins. For information about performance implications, see the documentation for the specific [dynamic management view](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md).

- Querying catalog views.

- Basic DBCC commands such as [DBCC FREEPROCCACHE](../..//t-sql/database-console-commands/dbcc-freeproccache-transact-sql.md), [DBCC FREESYSTEMCACHE](../../t-sql/database-console-commands/dbcc-freesystemcache-transact-sql.md), [DBCC DROPCLEANBUFFERS](../../t-sql/database-console-commands/dbcc-dropcleanbuffers-transact-sql.md), and [DBCC SQLPERF](../../t-sql/database-console-commands/dbcc-sqlperf-transact-sql.md). Avoid resource-intensive commands such as [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md), [DBCC DBREINDEX](../../t-sql/database-console-commands/dbcc-dbreindex-transact-sql.md), or [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).

- [!INCLUDE[tsql](../../includes/tsql-md.md)] `KILL <spid>` command. Depending on the state of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the KILL command might not always succeed; then the only option may be to restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The following are some general guidelines:

  - Verify that the SPID was killed by querying `SELECT * FROM sys.dm_exec_sessions WHERE session_id = <spid>`. If it returns no rows, it means the session was killed.

  - If the session is still there, verify whether there are tasks assigned to this session by running the query `SELECT * FROM sys.dm_os_tasks WHERE session_id = <spid>`. If you see the task there, most likely your session is currently being killed. This may take considerable amount of time and may not succeed at all.

  - If there are no tasks in the sys.dm_os_tasks associated with this session, but the session remains in sys.dm_exec_sessions after executing the KILL command, it means that you don't have a worker available. Select one of the currently running tasks (a task listed in the sys.dm_os_tasks view with a `sessions_id <> NULL`), and kill the session associated with it to free up the worker. It may not be enough to kill a single session: you may have to kill multiple ones.

## DAC port

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] listens for the DAC on TCP port 1434 if available or a TCP port dynamically assigned upon [!INCLUDE[ssDE](../../includes/ssde-md.md)] startup. The [error log](../../relational-databases/performance/view-the-sql-server-error-log-sql-server-management-studio.md) contains the port number the DAC is listening on. By default the DAC listener accepts connection on only the local port. For a code sample that activates remote administration connections, see [remote admin connections Server Configuration Option](../../database-engine/configure-windows/remote-admin-connections-server-configuration-option.md).

After the remote administration connection is configured, the DAC listener is enabled without restarting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a client can now connect to the DAC remotely. You can enable the DAC listener to accept connections remotely even if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is unresponsive by first connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the DAC locally, and then executing the `sp_configure` stored procedure to accept connection from remote connections.

On cluster configurations, the DAC will be off by default. Users can execute the remote admin connection option of `sp_configure` to enable the DAC listener to access a remote connection. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is unresponsive and the DAC listener isn't enabled, you might have to restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to connect with the DAC. Therefore, we recommend that you enable the remote admin connections configuration option on clustered systems.

The DAC port is assigned dynamically by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] during startup. When connecting to the default instance, the DAC avoids using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Resolution Protocol (SSRP) request to the SQL Server Browser Service when connecting. It first connects over TCP port 1434. If that fails, it makes an SSRP call to get the port. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser isn't listening for SSRP requests, the connection request returns an error. Refer to the error log to find the port number DAC is listening on. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to accept remote administration connections, the DAC must be initiated with an explicit port number:

`sqlcmd -S tcp:<server>,<port>`

The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log lists the port number for the DAC, which is 1434 by default. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to accept local DAC connections only, connect using the loopback adapter using the following command:

```powershell
sqlcmd -S 127.0.0.1,1434
```

> [!TIP]  
> When connecting to the [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] with the DAC, you must also specify the database name in the connection string by using the `-d` option.

## Example

In this example, an administrator notices that server `URAN123` isn't responding and wants to diagnose the problem. To do this, the user activates the `sqlcmd` command prompt utility and connects to server `URAN123` using `-A` to indicate the DAC.

```powershell
sqlcmd -S URAN123 -U sa -P <StrongPassword> -A
```

The administrator can now execute queries to diagnose the problem and possibly terminate the unresponsive sessions.

A similar example connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] would use the following command including the -d parameter to specify the database:

```powershell
sqlcmd -S serverName.database.windows.net,1434 -U sa -P <StrongPassword> -d AdventureWorks
```

## See also

- [Use sqlcmd with Scripting Variables](../../ssms/scripting/sqlcmd-use-with-scripting-variables.md)  
- [sqlcmd Utility](../../tools/sqlcmd-utility.md)  
- [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)  
- [sp_who &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)  
- [sp_lock &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-lock-transact-sql.md)  
- [KILL &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-transact-sql.md)  
- [DBCC CHECKALLOC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkalloc-transact-sql.md)  
- [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)  
- [DBCC OPENTRAN &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-opentran-transact-sql.md)  
- [DBCC INPUTBUFFER &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-inputbuffer-transact-sql.md)  
- [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)  
- [Transaction Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)  
- [Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
