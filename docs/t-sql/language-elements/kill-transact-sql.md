---
title: "KILL (Transact-SQL)"
description: The KILL statement ends a user process that is based on the session ID or unit of work (UOW).
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/09/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "KILL_TSQL"
  - "KILL"
helpviewer_keywords:
  - "WITH STATUSONLY option"
  - "terminating distributed transactions"
  - "distributed transactions [SQL Server], terminating"
  - "in-doubt transactions"
  - "IDs [SQL Server], user processes"
  - "ending distributed transactions [SQL Server]"
  - "stopping distributed transactions"
  - "session IDs [SQL Server]"
  - "system process termination [SQL Server]"
  - "stopping processes"
  - "ending processes [SQL Server]"
  - "UOW [SQL Server]"
  - "orphaned transactions [SQL Server]"
  - "unit of work [SQL Server]"
  - "process termination [SQL Server]"
  - "KILL statement"
  - "terminating process"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# KILL (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Ends a user process that is based on the session ID or unit of work (UOW). If the specified session ID or UOW has much work to undo, the `KILL` statement can take some time to complete. The process takes longer to complete particularly when the process involves rolling back a long transaction.

`KILL` ends a normal connection, which internally stops the transactions that are associated with the specified session ID. At times, [!INCLUDE [msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) might be in use. If MS DTC is in use, you can also use the statement to end orphaned and in-doubt distributed transactions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, and Azure SQL Managed Instance:

```syntaxsql
KILL { session_id [ WITH STATUSONLY ] | UOW [ WITH STATUSONLY | COMMIT | ROLLBACK ] }
[ ; ]
```

Syntax for Azure Synapse Analytics, Analytics Platform System (PDW), and Microsoft Fabric:

```syntaxsql
KILL 'session_id'
[ ; ]
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *session_id*

The session ID of the process to end. `session_id` is a unique **int** that is assigned to each user connection when the connection is made. The session ID value is tied to the connection for the duration of the connection. When the connection ends, the integer value is released and can be reassigned to a new connection.

The following query can help you identify the `session_id` that you want to kill:

```sql
 SELECT conn.session_id, host_name, program_name,
     nt_domain, login_name, connect_time, last_request_end_time
FROM sys.dm_exec_sessions AS sess
JOIN sys.dm_exec_connections AS conn
    ON sess.session_id = conn.session_id;
```

#### *UOW*

Identifies the unit of work ID (UOW) of distributed transactions. *UOW* is a GUID that might be obtained from the `request_owner_guid` column of the `sys.dm_tran_locks` dynamic management view. *UOW* also can be obtained from the error log or through the MS DTC monitor. For more information about monitoring distributed transactions, see the MS DTC documentation.

Use `KILL <UOW>` to stop unresolved distributed transactions. These transactions aren't associated with any real session ID, but instead are associated artificially with session ID = `-2`. This session ID makes it easier to identify unresolved transactions by querying the session ID column in `sys.dm_tran_locks`, `sys.dm_exec_sessions`, or `sys.dm_exec_requests` dynamic management views.

#### WITH STATUSONLY

Used to generate a progress report for a specified *UOW* or `session_id` that is being rolled back because of an earlier `KILL` statement. `KILL WITH STATUSONLY` doesn't end or roll back the UOW or session ID. The command only displays the current progress of the rollback.

#### WITH COMMIT

Used to kill an unresolved distributed transaction with commit. Only applicable to distributed transactions, you must specify a *UOW* to use this option. For more information, see [distributed transactions](../../database-engine/availability-groups/windows/configure-availability-group-for-distributed-transactions.md#manage-unresolved-transactions).

#### WITH ROLLBACK

Used to kill an unresolved distributed transaction with rollback. Only applicable to distributed transactions, you must specify a *UOW* to use this option. For more information, see [distributed transactions](../../database-engine/availability-groups/windows/configure-availability-group-for-distributed-transactions.md#manage-unresolved-transactions).

## Remarks

`KILL` is commonly used to end a process that is blocking other important processes with locks. `KILL` can also be used to stop a process that is executing a query that is using necessary system resources. System processes and processes running an extended stored procedure can't be ended.

Use `KILL` carefully, especially when critical processes are running. You can't kill your own process. You also shouldn't kill the following processes:

- `AWAITING COMMAND`
- `CHECKPOINT SLEEP`
- `LAZY WRITER`
- `LOCK MONITOR`
- `SIGNAL HANDLER`

Use `@@SPID` to display the session ID value for the current session.

To obtain a report of active session ID values, query the `session_id` column of the `sys.dm_tran_locks`, `sys.dm_exec_sessions`, and `sys.dm_exec_requests` dynamic management views. You can also view the `SPID` column that the `sp_who` system stored procedure returns. If a rollback is in progress for a specific SPID, the `cmd` column in the `sp_who` result set for that SPID indicates `KILLED/ROLLBACK`.

When a particular connection has a lock on a database resource and blocks the progress of another connection, the session ID of the blocking connection shows up in the `blocking_session_id` column of `sys.dm_exec_requests` or the `blk` column returned by `sp_who`.

The `KILL` command can be used to resolve in-doubt distributed transactions. These transactions are unresolved distributed transactions that occur because of unplanned restarts of the database server or MS DTC coordinator. For more information about in-doubt transactions, see the "Two-Phase Commit" section in [Use Marked Transactions to Recover Related Databases Consistently](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md).

## Use WITH STATUSONLY

`KILL WITH STATUSONLY` generates a report if the session ID or UOW rolls back because of a previous `KILL <session ID>` or `KILL <UOW>` statement. The progress report states the amount of rollback completed (in percent) and the estimated length of time left (in seconds). The report states it in the following form:

`Spid|UOW <xxx>: Transaction rollback in progress. Estimated rollback completion: <yy>% Estimated time left: <zz> seconds`

If the rollback of the session ID or UOW finishes before the `KILL <session ID> WITH STATUSONLY` or `KILL <UOW> WITH STATUSONLY` statement runs, `KILL ... WITH STATUSONLY` returns the following error:

```output
"Msg 6120, Level 16, State 1, Line 1"
"Status report cannot be obtained. Rollback operation for Process ID <session ID> is not in progress."
```

This error also occurs if no session ID or UOW is being rolled back.

The same status report can be obtained by repeating the same `KILL` statement without using the `WITH STATUSONLY` option. However, we don't recommend repeating the option this way. If you repeat a `KILL <session_id>` statement, the new process might stop if the rollback finishes and the session ID is reassigned to a new task before the new `KILL` statement runs. Prevent the new process from stopping by specifying `WITH STATUSONLY`.

## Permissions

**[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:** Requires the `ALTER ANY CONNECTION` permission. `ALTER ANY CONNECTION` is included with membership in the **sysadmin** or **processadmin** fixed server roles.

**[!INCLUDE [ssSDS](../../includes/sssds-md.md)]:** Requires the `KILL DATABASE CONNECTION` permission. The server-level principal login has the `KILL DATABASE CONNECTION` permission.

**[!INCLUDE [fabric](../../includes/fabric.md)]:** Requires Admin permissions.

**Azure Synapse Analytics:** Requires Admin permissions.

## Examples

### A. Use KILL to stop a session

The following example shows how to stop session ID `53`.

```sql
KILL 53;
GO
```

### B. Use KILL session ID WITH STATUSONLY to obtain a progress report

The following example generates a status of the rollback process for the specific session ID.

```sql
KILL 54;
KILL 54 WITH STATUSONLY;
GO
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
spid 54: Transaction rollback in progress. Estimated rollback completion: 80% Estimated time left: 10 seconds.
```

### C. Use KILL to stop an orphaned distributed transaction

The following example shows how to stop an orphaned distributed transaction (session ID = `-2`) with a *UOW* of `D5499C66-E398-45CA-BF7E-DC9C194B48CF`.

```sql
KILL 'D5499C66-E398-45CA-BF7E-DC9C194B48CF';
```

## Related content

- [KILL STATS JOB (Transact-SQL)](kill-stats-job-transact-sql.md)
- [KILL QUERY NOTIFICATION SUBSCRIPTION (Transact-SQL)](kill-query-notification-subscription-transact-sql.md)
- [What are the SQL database functions?](../functions/functions.md)
- [SHUTDOWN (Transact-SQL)](shutdown-transact-sql.md)
- [&#x40;&#x40;SPID (Transact-SQL)](../functions/spid-transact-sql.md)
- [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)
- [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)
- [sys.dm_tran_locks](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md)
- [sp_lock](../../relational-databases/system-stored-procedures/sp-lock-transact-sql.md)
- [sp_who](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)
