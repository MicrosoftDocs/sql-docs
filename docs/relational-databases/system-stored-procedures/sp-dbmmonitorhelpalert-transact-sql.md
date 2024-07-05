---
title: "sp_dbmmonitorhelpalert (Transact-SQL)"
description: Returns information about warning thresholds on one or all of several key database mirroring monitor performance metrics.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbmmonitorhelpalert_TSQL"
  - "sp_dbmmonitorhelpalert"
helpviewer_keywords:
  - "sp_dbmmonitorhelpalert"
  - "database mirroring [SQL Server], monitoring"
dev_langs:
  - "TSQL"
---
# sp_dbmmonitorhelpalert (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about warning thresholds on one or all of several key database mirroring monitor performance metrics.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dbmmonitorhelpalert
    [ @database_name = ] N'database_name'
    [ , [ @alert_id = ] alert_id ]
[ ; ]
```

## Arguments

#### [ @database_name = ] N'*database_name*'

Specifies the database. *@database_name* is **sysname**, with no default.

#### [ @alert_id = ] *alert_id*

An integer value that identifies the warning to be returned. *@alert_id* is **int**, with a default of `NULL`. If this argument is omitted, all the warnings are returned, but not the retention period.

To return a specific warning, specify one of the following values:

| Value | Performance metric | Warning threshold |
| --- | --- | --- |
| `1` | Oldest unsent transaction | Specifies the number of minutes worth of transactions that can accumulate in the send queue before a warning is generated on the principal server instance. This warning helps measure the potential for data loss in terms of time, and it can be relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected. |
| `2` | Unsent log | Specifies how many kilobytes (KB) of unsent log generate a warning on the principal server instance. This warning helps measure the potential for data loss in terms of KB, and it can be relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected. |
| `3` | Unrestored log | Specifies how many KB of unrestored log generate a warning on the mirror server instance. This warning helps measure *failover time*. Failover time consists mainly of the time that the former mirror server requires to roll forward any log remaining in its redo queue, plus a short extra time. |
| `4` | Mirror commit overhead | Specifies the number of milliseconds of average delay per transaction that are tolerated before a warning is generated on the principal server. This delay is the amount of overhead incurred while the principal server instance waits for the mirror server instance to write the transaction's log record into the redo queue. This value is relevant only in high-safety mode. |
| `5` | Retention period | Metadata that controls how long rows in the database mirroring status table are preserved. |

For information about the event IDs corresponding to the warnings, see [Use Warning Thresholds and Alerts on Mirroring Performance Metrics (SQL Server)](../../database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md).

## Return code values

None.

## Result set

For each returned alert, returns a row containing the following columns:

| Column | Data type | Description |
| --- | --- | --- |
| `alert_id` | **int** | The following table lists the `alert_id` value for each performance metric and the unit of measurement of the metric displayed in the `sp_dbmmonitorresults` result set. |
| `threshold` | **int** | The threshold value for the warning. If a value above this threshold is returned when the mirroring status is updated, an entry is entered into the Windows event log. This value represents KB, minutes, or milliseconds, depending on the warning. If the threshold is currently not set, the value is `NULL`.<br /><br />**Note:** To view the current values, run the [sp_dbmmonitorresults](sp-dbmmonitorresults-transact-sql.md) stored procedure. |
| `enabled` | **bit** | `0` = Event is disabled.<br />`1` = Event is enabled.<br /><br />**Note:** Retention period is always enabled. |

| Value | Performance metric | Unit |
| --- | --- | --- |
| `1` | Oldest unsent transaction | Minutes |
| `2` | Unsent log | KB |
| `3` | Unrestored log | KB |
| `4` | Mirror commit overhead | Milliseconds |
| `5` | Retention period | Hours |

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example returns a row that indicates whether a warning is enabled for the oldest unsent transaction performance metric on the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_dbmmonitorhelpalert AdventureWorks2022, 1;
```

The following example returns a row for each performance metric that indicates whether it's enabled on the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_dbmmonitorhelpalert AdventureWorks2022;
```

## Related content

- [Monitoring Database Mirroring (SQL Server)](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)
- [sp_dbmmonitorchangealert (Transact-SQL)](sp-dbmmonitorchangealert-transact-sql.md)
- [sp_dbmmonitorchangemonitoring (Transact-SQL)](sp-dbmmonitorchangemonitoring-transact-sql.md)
- [sp_dbmmonitordropalert (Transact-SQL)](sp-dbmmonitordropalert-transact-sql.md)
- [sp_dbmmonitorupdate (Transact-SQL)](sp-dbmmonitorupdate-transact-sql.md)
- [sp_dbmmonitorhelpmonitoring (Transact-SQL)](sp-dbmmonitorhelpmonitoring-transact-sql.md)
- [sp_dbmmonitorresults (Transact-SQL)](sp-dbmmonitorresults-transact-sql.md)
