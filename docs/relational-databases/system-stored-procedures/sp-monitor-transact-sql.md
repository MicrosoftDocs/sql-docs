---
title: "sp_monitor (Transact-SQL)"
description: sp_monitor displays statistics about SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_monitor_TSQL"
  - "sp_monitor"
helpviewer_keywords:
  - "sp_monitor"
dev_langs:
  - "TSQL"
---
# sp_monitor (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays statistics about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_monitor
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Description |
| --- | --- |
| `last_run` | Time `sp_monitor` was last run. |
| `current_run` | Time `sp_monitor` is being run. |
| `seconds` | Number of elapsed seconds since `sp_monitor` was run. |
| `cpu_busy` | Number of seconds that the server computer's CPU has done [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] work. |
| `io_busy` | Number of seconds that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] spent doing input and output operations. |
| `idle` | Number of seconds that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] was idle. |
| `packets_received` | Number of input packets read by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `packets_sent` | Number of output packets written by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `packet_errors` | Number of errors encountered by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] while reading and writing packets. |
| `total_read` | Number of reads by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `total_write` | Number of writes by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `total_errors` | Number of errors encountered by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] while reading and writing. |
| `connections` | Number of logins or attempted logins to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |

## Remarks

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] keeps track, through a series of functions, of how much work was done. Executing `sp_monitor` displays the current values returned by these functions and shows how much they have changed since the last time the procedure was run.

For each column, the statistic is printed in the form *number*(*number*)-*number*% or *number*(*number*). The first *number* refers to the number of seconds (for `cpu_busy`, `io_busy`, and `idle`) or the total number (for the other variables) since [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] was restarted. The *number* in parentheses refers to the number of seconds or total number since the last time `sp_monitor` was run. The percentage is the percentage of time since `sp_monitor` was last run. For example, if the report shows `cpu_busy` as `4250(215)-68%`, the CPU was busy 4,250 seconds since [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] was last started up, 215 seconds since `sp_monitor` was last run, and 68 percent of the total time since `sp_monitor` was last run.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example reports information about how busy [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] has been.

```sql
USE master;
GO

EXEC sp_monitor;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
last_run                   current_run                seconds
-----------------------    -----------------------    ---------
2024-05-01 15:27:51.287    2024-08-21 17:20:34.097    9683563

cpu_busy           io_busy         idle
---------------    -------------   --------------------
14452(14451)-0%    2555(2554)-0%   4371742(4371629)-45%

packets_received       packets_sent    packet_errors
----------------       ------------    -------------
18032(17993)           64572(64533)    0(0)

total_read     total_write   total_errors    connections
-----------    -----------   -------------   --------------
1593(1593)     4687(4687)    0(0)            155625(155557)
```

## Related content

- [sp_who (Transact-SQL)](sp-who-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
