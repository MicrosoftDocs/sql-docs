---
title: "DBCC TRACEON (Transact-SQL)"
description: DBCC TRACEON enables the specified trace flags.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC_TRACEON_TSQL"
  - "TRACEON"
  - "DBCC TRACEON"
  - "TRACEON_TSQL"
helpviewer_keywords:
  - "DBCC TRACEON statement"
  - "trace flags [SQL Server], enabling"
dev_langs:
  - "TSQL"
---
# DBCC TRACEON (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Enables the specified trace flags.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC TRACEON ( trace# [ , ...n ] [ , -1 ] ) [ WITH NO_INFOMSGS ]
```

## Arguments

#### *trace#*

The number of the trace flag to turn on.

#### *n*

A placeholder that indicates multiple trace flags can be specified.

#### `-1`

Switches on the specified trace flags globally. This argument is required in Azure SQL Managed Instance.

#### WITH NO_INFOMSGS

Suppresses all informational messages.

## Remarks

On a production server, to avoid unpredictable behavior, we recommend that you only enable trace flags server-wide by using one of the following methods:

- Use the `-T` command-line startup option of `sqlservr.exe`. This is a recommended best practice because it makes sure that all statements will run with the trace flag enabled. These include commands in startup scripts. For more information, see [sqlservr Application](../../tools/sqlservr-application.md).
- Use `DBCC TRACEON` only while users or applications aren't concurrently running statements on the system.

Trace flags are used to customize certain characteristics by controlling how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] operates. Trace flags, after they are enabled, remain enabled in the server until disabled by executing a `DBCC TRACEOFF` statement. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], there are two types of trace flags: session and global. Session trace flags are active for a connection and are visible only for that connection. Global trace flags are set at the server level and are visible to every connection on the server. To determine the status of trace flags, use `DBCC TRACESTATUS`. To disable trace flags, use `DBCC TRACEOFF`.

After turning on a trace flag that affects query plans, execute `DBCC FREEPROCCACHE;` so that cached plans are recompiled using the new plan-affecting behavior.

Azure SQL Managed Instance supports the following global Trace Flags: 460, 2301, 2389, 2390, 2453, 2467, 7471, 8207, 9389, 10316, and 11024.

## Result sets

`DBCC TRACEON` returns the following message:

```output
DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

## Permissions

Requires membership in the **sysadmin** fixed server role.

## Examples

The following example disables hardware compression for tape drivers, by switching on Trace Flag 3205. This flag is switched on only for the current connection.

```sql
DBCC TRACEON (3205);
GO
```

The following example switches on Trace Flag 3205 globally.

```sql
DBCC TRACEON (3205, -1);
GO
```

The following example switches on Trace Flags 3205 and 260 globally.

```sql
DBCC TRACEON (3205, 260, -1);
GO
```

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
- [DBCC TRACEOFF (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceoff-transact-sql.md)
- [DBCC TRACESTATUS (Transact-SQL)](../../t-sql/database-console-commands/dbcc-tracestatus-transact-sql.md)
- [Trace Flags (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
- [Enable plan-affecting SQL Server query optimizer behavior that can be controlled by different trace flags on a specific-query level](https://support.microsoft.com/help/2801413)
