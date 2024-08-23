---
title: "sp_who (Transact-SQL)"
description: Provides information about current users, sessions, and processes in an instance of the SQL Server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_who_TSQL"
  - "sp_who"
helpviewer_keywords:
  - "sp_who"
dev_langs:
  - "TSQL"
---
# sp_who (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Provides information about current users, sessions, and processes in an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. The information can be filtered to return only those processes that aren't idle, that belong to a specific user, or that belong to a specific session.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_who [ [ @loginame = ] { 'login' | *session_id* | 'ACTIVE' } ]
[ ; ]
```

## Arguments

#### [ @loginame = ] { 'login' | *session_id* | 'ACTIVE' }

Used to filter the result set.

- *login* is **sysname** that identifies processes belonging to a particular login.

- *session_id* is a session identification number belonging to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. *session_id* is **smallint**.

- `ACTIVE` excludes sessions that are waiting for the next command from the user.

If no value is provided, the procedure reports all sessions belonging to the instance.

## Return code values

`0` (success) or `1` (failure).

## Result set

`sp_who` returns a result set with the following information.

| Column | Data type | Description |
| --- | --- | --- |
| `spid` | **smallint** | Session ID. |
| `ecid` | **smallint** | Execution context ID of a given thread associated with a specific session ID.<br /><br />ECID = { 0, 1, 2, 3, ...*n* }, where 0 always represents the main or parent thread, and { 1, 2, 3, ...*n* } represent the subthreads. |
| `status` | **nchar(30)** | Process status. The possible values are:<br /><br />- `dormant`. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is resetting the session.<br /><br />- `running`. The session is running one or more batches. When Multiple Active Result Sets (MARS) is enabled, a session can run multiple batches. For more information, see [Using Multiple Active Result Sets (MARS)](../native-client/features/using-multiple-active-result-sets-mars.md).<br /><br />- `background`. The session is running a background task, such as deadlock detection.<br /><br />- `rollback`. The session has a transaction rollback in process.<br /><br />- `pending`. The session is waiting for a worker thread to become available.<br /><br />- `runnable`. The session's task is in the runnable queue of a scheduler while waiting to get a time quantum.<br /><br />- `spinloop`. The session's task is waiting for a spinlock to become free.<br /><br />- `suspended`. The session is waiting for an event, such as I/O, to complete. |
| `loginame` | **nchar(128)** | Login name associated with the particular process. |
| `hostname` | **nchar(128)** | Host or computer name for each process. |
| `blk` | **char(5)** | Session ID for the blocking process, if one exists. Otherwise, this column is `0`.<br /><br />When a transaction associated with a specified session ID is blocked by an orphaned distributed transaction, this column returns a `-2` for the blocking orphaned transaction. |
| `dbname` | **nchar(128)** | Database used by the process. |
| `cmd` | **nchar(16)** | [!INCLUDE [ssDE](../../includes/ssde-md.md)] command ([!INCLUDE [tsql](../../includes/tsql-md.md)] statement, internal [!INCLUDE [ssDE](../../includes/ssde-md.md)] process, and so on) executing for the process. In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, the data type is **nchar(26)**. |
| `request_id` | **int** | ID for requests running in a specific session. |

With parallel processing, subthreads are created for the specific session ID. The main thread is indicated as `spid = <xxx>` and `ecid = 0`. The other subthreads have the same `spid = <xxx>`, but with `ecid > 0`.

## Remarks

A blocking process, which might have an exclusive lock, is one that is holding resources that another process needs.

All orphaned distributed transactions are assigned the session ID value of `-2`. Orphaned distributed transactions are distributed transactions that aren't associated with any session ID. For more information, see [Use Marked Transactions to Recover Related Databases Consistently](../backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md).

Query the `is_user_process` column of `sys.dm_exec_sessions` to separate system processes from user processes.

## Permissions

Requires VIEW SERVER STATE permission on the server to see all executing sessions on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Otherwise, the user sees only the current session.

## Examples

### A. List all current processes

The following example uses `sp_who` without parameters to report all current users.

```sql
USE master;
GO
EXEC sp_who;
GO
```

### B. List a specific user's process

The following example shows how to view information about a single current user by login name.

```sql
USE master;
GO
EXEC sp_who 'janetl';
GO
```

### C. Display all active processes

```sql
USE master;
GO
EXEC sp_who 'active';
GO
```

### D. Display a specific process identified by a session ID

```sql
USE master;
GO
EXEC sp_who '10' --specifies the process_id;
GO
```

## Related content

- [sp_lock (Transact-SQL)](sp-lock-transact-sql.md)
- [sys.sysprocesses (Transact-SQL)](../system-compatibility-views/sys-sysprocesses-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
