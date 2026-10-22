---
title: Permissions Required
titleSuffix: SQL Server Profiler
description: Find out which permissions you need to run SQL Server Profiler and replay traces, and learn which checks are performed during replays.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: concept-article
ms.collection:
  - data-tools
---

# Permissions required to run SQL Server Profiler

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

By default, running [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] requires the same user permissions as the Transact-SQL stored procedures that are used to create traces. To run [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], users must be granted the `ALTER TRACE` permission. For more information, see [GRANT Server Permissions](../../t-sql/statements/grant-server-permissions-transact-sql.md).

> [!NOTE]  
> SQL Trace and [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] are deprecated. The `Microsoft.SqlServer.Management.Trace` namespace that contains the Microsoft SQL Server Trace and Replay objects is also deprecated.
>
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]
>
> Use Extended Events instead. For more information on [Extended Events overview](../../relational-databases/extended-events/extended-events.md), see [Quickstart: Extended Events](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md) and [Use the SSMS XEvent Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md).

## Remarks

- Query plans and query text, captured by SQL Trace as well as by other means, for example, dynamic management views (DMVs), dynamic management functions (DMFs), and Extended Events, can contain sensitive information. Therefore, the permissions `ALTER TRACE`, `SHOWPLAN`, and the covering permission `VIEW SERVER STATE` should be granted to only users who need these permissions to fulfill their job functions, based on the principle of least privilege.

  Additionally, we recommend that you only save Showplan files or trace files that contain Showplan-related events to a location that uses the NTFS file system and restrict access to users who are authorized to view potentially sensitive information.

- [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] for Analysis Services workloads are supported.

- When you try to connect to an Azure SQL Database from [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], it incorrectly throws a misleading error message:

  ```output
  In order to run a trace against SQL Server, you must be a member of **sysadmin** fixed server role or have the ALTER TRACE permission.
  ```

  The message should state that Azure SQL Database isn't supported by [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Permissions used to replay traces

Replaying traces also requires that the user who is replaying the trace have the `ALTER TRACE` permission.

However, during replay, [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] uses the `EXECUTE AS` command if an Audit Login event is encountered in the trace that is being replayed. [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] uses the `EXECUTE AS` command to impersonate the user who is associated with the login event.

If [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] encounters a login event in a trace that is being replayed, the following permission checks are performed:

1. `User1`, who has the `ALTER TRACE` permission, starts replaying a trace.

1. A login event for `User2` is encountered in the replayed trace.

1. [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] uses the `EXECUTE AS` command to impersonate `User2`.

1. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] attempts to authenticate `User2`, and depending on the results, one of the following occurs:

    1. If `User2` can't be authenticated, [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] returns an error, and continues replaying the trace as `User1`.

    1. If `User2` is successfully authenticated, replaying the trace as `User2` continues.

1. Permissions for `User2` are checked on the target database, and depending on the results, one of the following scenarios occurs:

    1. If `User2` has permissions on the target database, impersonation has succeeded, and the trace is replayed as `User2`.

    1. If `User2` doesn't have permissions on the target database, the server checks for a `Guest` user on that database.

1. Existence of a `Guest` user is checked on the target database, and depending on the results, one of the following occurs:

    1. If a `Guest` account exists, the trace is replayed as the `Guest` account.

    1. If no `Guest` account exists on the target database, an error is returned and the trace is replayed as `User1`.

The following diagram shows this process of checking permission when replaying traces:

:::image type="content" source="media/permissions-required-to-run-sql-server-profiler/replay-trace-decision-tree.png" alt-text="Screenshot of SQL Server Profiler replay trace permissions.":::

## Related content

- [SQL Server Profiler stored procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/sql-server-profiler-stored-procedures-transact-sql.md)
- [Replay Traces](replay-traces.md)
- [Create a trace (SQL Server Profiler)](create-a-trace-sql-server-profiler.md)
- [Replay a trace table (SQL Server Profiler)](replay-a-trace-table-sql-server-profiler.md)
- [Replay a trace file (SQL Server Profiler)](replay-a-trace-file-sql-server-profiler.md)
