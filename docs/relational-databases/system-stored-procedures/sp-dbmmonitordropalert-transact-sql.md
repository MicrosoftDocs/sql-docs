---
title: "sp_dbmmonitordropalert (Transact-SQL)"
description: sp_dbmmonitordropalert drops the warning for a specified performance metric, by setting the threshold to NULL.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbmmonitordropalert_TSQL"
  - "sp_dbmmonitordropalert"
helpviewer_keywords:
  - "database mirroring [SQL Server], monitoring"
  - "sp_dbmmonitordropalert"
dev_langs:
  - "TSQL"
---
# sp_dbmmonitordropalert (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops the warning for a specified performance metric, by setting the threshold to `NULL`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dbmmonitordropalert
    [ @database_name = ] N'database_name'
    [ , [ @alert_id = ] alert_id ]
[ ; ]
```

## Arguments

#### [ @database_name = ] N'*database_name*'

Specifies the database for which to drop the specified warning threshold. *@database_name* is **sysname**, with no default.

#### [ @alert_id = ] *alert_id*

An integer value that identifies the warning to be dropped. *@alert_id* is **int**, and can be one of the following values:

| Value | Performance metric | Warning threshold |
| --- | --- | --- |
| `1` | Oldest unsent transaction | Specifies the number of minutes worth of transactions that can accumulate in the send queue before a warning is generated on the principal server instance. This warning helps measure the potential for data loss in terms of time, and is particularly relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected. |
| `2` | Unsent log | Specifies how many kilobytes (KB) of unsent log generate a warning on the principal server instance. This warning helps measure the potential for data loss in terms of KB, and is particularly relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected. |
| `3` | Unrestored log | Specifies how many KB of unrestored log generate a warning on the mirror server instance. This warning helps measure *failover time*. Failover time consists mainly of the time that the former mirror server requires to roll forward any log remaining in its redo queue, plus a short additional time. |
| `4` | Mirror commit overhead | Specifies the number of milliseconds of average delay per transaction that are tolerated before a warning is generated on the principal server. This delay is the amount of overhead incurred while the principal server instance waits for the mirror server instance to write the transaction's log record into the redo queue. This value is relevant only in high-safety mode. |
| `5` | Retention period | Metadata that controls how long rows in the database mirroring status table are preserved. |

> [!NOTE]  
> This procedure drops warning thresholds, regardless of whether they were specified using `sp_dbmmonitorchangealert` or Database Mirroring Monitor.

For information about the event IDs corresponding to the warnings, see [Use Warning Thresholds and Alerts on Mirroring Performance Metrics (SQL Server)](../../database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md).

## Return code values

None.

## Result set

None.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example drops the retention period setting of the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_dbmmonitordropalert AdventureWorks2022, 5;
```

The following example drops all of the warning thresholds and the retention period of the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_dbmmonitordropalert AdventureWorks2022;
```

## Related content

- [Monitoring Database Mirroring (SQL Server)](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)
- [sp_dbmmonitorchangealert (Transact-SQL)](sp-dbmmonitorchangealert-transact-sql.md)
