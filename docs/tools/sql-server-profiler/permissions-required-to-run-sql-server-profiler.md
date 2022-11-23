---
title: Permissions Required
titleSuffix: SQL Server Profiler
description: Find out which permissions you need to run SQL Server Profiler and replay traces, and learn which checks are performed during replays.
ms.service: sql
ms.subservice: profiler
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# Permissions Required to Run SQL Server Profiler

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

By default, running [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] requires the same user permissions as the Transact-SQL stored procedures that are used to create traces. To run [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], users must be granted the ALTER TRACE permission. For more information, see [GRANT Server Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-server-permissions-transact-sql.md).

> [!IMPORTANT]
> Query Plans and Query Text, captured by SQL Trace as well as by other means, for example, Dynamic Management Views and Functions (DMVs, DMFs), Extended Events, can contain sensitive information. Therefore, the permissions ALTER TRACE, SHOWPLAN, and the covering permission VIEW SERVER STATE should be granted to only those who need these to fulfill their job functions, based on the principle of least privilege.
>
> Additionally, we recommend that you only save Showplan files or trace files that contain Showplan-related events to a location that uses the NTFS file system and restrict access to users who are authorized to view potentially sensitive information.

> [!IMPORTANT]
> SQL Trace and [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] are deprecated. The *Microsoft.SqlServer.Management.Trace* namespace that contains the Microsoft SQL Server Trace and Replay objects are also deprecated.
> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]
> Use Extended Events instead. For more information on [Extended Events](../../relational-databases/extended-events/extended-events.md), see [Quick Start: Extended events in SQL Server](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md) and [SSMS XEvent Profiler](../../relational-databases/extended-events/use-the-ssms-xe-profiler.md).

> [!NOTE]
> [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] for Analysis Services workloads are supported.

> [!NOTE]
> When you try to connect to a Azure SQL Database from SQL server profiler, it incorrectly throws a misleading error message as follows:
>
> - In order to run a trace against SQL Server, you must be a member of sysadmin fixed server role or have the ALTER TRACE permission.
>
> The message should have explained that Azure SQL Database is not supported by SQL Server profiler.

## Permissions Used to Replay Traces  
Replaying traces also requires that the user who is replaying the trace have the ALTER TRACE permission.  

However, during replay, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] uses the EXECUTE AS command if an Audit Login event is encountered in the trace that is being replayed. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] uses the EXECUTE AS command to impersonate the user who is associated with the login event.  

If [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] encounters a login event in a trace that is being replayed, the following permission checks are performed:

1. User1, who has the ALTER TRACE permission, starts replaying a trace.

2. A login event for User2 is encountered in the replayed trace.

3. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] uses the EXECUTE AS command to impersonate User2.

4. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] attempts to authenticate User2, and depending on the results, one of the following occurs:

    1. If User2 cannot be authenticated, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] returns an error, and continues replaying the trace as User1.
  
    2. If User2 is successfully authenticated, replaying the trace as User2 continues.
  
5. Permissions for User2 are checked on the target database, and depending on the results, one of the following occurs:
  
    1. If User2 has permissions on the target database, impersonation has succeeded, and the trace is replayed as User2.
  
    2. If User2 does not have permissions on the target database, the server checks for a Guest user on that database.

6. Existence of a Guest user is checked on the target database, and depending on the results, one of the following occurs:
 
    1.  If a Guest account exists, the trace is replayed as the Guest account.
  
    2.  If no Guest account exists on the target database, an error is returned and the trace is replayed as User1.
 
The following diagram shows this process of checking permission when replaying traces:

![SQL Server Profiler replay trace permissions.](../../tools/sql-server-profiler/media/replaytracedecisiontree.gif)

## See Also
- [SQL Server Profiler Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-profiler-stored-procedures-transact-sql.md)
- [Replay Traces](../../tools/sql-server-profiler/replay-traces.md)
- [Create a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/create-a-trace-sql-server-profiler.md)
- [Replay a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-trace-table-sql-server-profiler.md)
- [Replay a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/replay-a-trace-file-sql-server-profiler.md)
