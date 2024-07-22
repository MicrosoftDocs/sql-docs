---
title: "sp_dbmmonitorchangealert (Transact-SQL)"
description: sp_dbmmonitorchangealert adds or changes warning threshold for a specified mirroring performance metric.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbmmonitorchangealert_TSQL"
  - "sp_dbmmonitorchangealert"
helpviewer_keywords:
  - "sp_dbmmonitorchangealert"
  - "database mirroring [SQL Server], monitoring"
dev_langs:
  - "TSQL"
---
# sp_dbmmonitorchangealert (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds or changes warning threshold for a specified mirroring performance metric.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sp_dbmmonitorchangealert
    [ @database_name = ] N'database_name'
    , [ @alert_id = ] alert_id
    , [ @threshold = ] threshold
    [ , [ @enabled = ] enabled ]
[ ; ]
```

## Arguments

#### [ @database_name = ] N'*database_name*'

Specifies the database for which to add or change the specified warning threshold. *@database_name* is **sysname**, with no default.

#### [ @alert_id = ] *alert_id*

An integer value that identifies the warning to be added or changed. *@alert_id* is **int**, and must be one of the following values:

| Value | Performance metric | Warning threshold |
| --- | --- | --- |
| `1` | Oldest unsent transaction | Specifies the number of minutes worth of transactions that can accumulate in the send queue before a warning is generated on the principal server instance. This warning helps measure the potential for data loss in terms of time, and it can be relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected. |
| `2` | Unsent log | Specifies how many kilobytes (KB) of unsent log generate a warning on the principal server instance. This warning helps measure the potential for data loss in terms of KB, and it can be relevant for high-performance mode. However, the warning is also relevant for high-safety mode when mirroring is paused or suspended because the partners become disconnected. |
| `3` | Unrestored log | Specifies how many KB of unrestored log generate a warning on the mirror server instance. This warning helps measure *failover time*. Failover time consists mainly of the time that the former mirror server requires to roll forward any log remaining in its redo queue, plus a short extra time. |
| `4` | Mirror commit overhead | Specifies the number of milliseconds of average delay per transaction that are tolerated before a warning is generated on the principal server. This delay is the amount of overhead incurred while the principal server instance waits for the mirror server instance to write the transaction's log record into the redo queue. This value is relevant only in high-safety mode. |
| `5` | Retention period | Metadata that controls how long rows in the database mirroring status table are preserved. |

For information about the event IDs corresponding to the warnings, see [Use Warning Thresholds and Alerts on Mirroring Performance Metrics (SQL Server)](../../database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md).

#### [ @threshold = ] *threshold*

The threshold value for the warning. *@threshold* is **int**, with no default. If a value above this threshold is returned when the mirroring status is updated, an entry is entered into the Windows event log. This value represents KB, minutes, or milliseconds, depending on the performance metric.

> [!NOTE]  
> To view the current values, run the [sp_dbmmonitorresults](sp-dbmmonitorresults-transact-sql.md) stored procedure.

#### [ @enabled = ] *enabled*

Specifies whether the warning is enabled. *@enabled* is **bit**, with a default of `1`. Retention period is always enabled.

- `0` = Warning is disabled.
- `1` = Warning is enabled.

## Return code values

None.

## Result set

None.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example sets thresholds for each of the performance metrics and the retention period for the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The following table shows the values used in the example.

| *alert_id* | Performance metric | Warning threshold | Warning enabled? |
| --- | --- | --- | --- |
| `1` | Oldest unsent transaction | 30 minutes | Yes |
| `2` | Unsent log | 10,000 KB | Yes |
| `3` | Unrestored log | 10,000 KB | Yes |
| `4` | Mirror commit overhead | 1,000 milliseconds | No |
| `5` | Retention period | Eight hours | Yes |

```sql
EXEC sp_dbmmonitorchangealert AdventureWorks2022, 1, 30, 1;
EXEC sp_dbmmonitorchangealert AdventureWorks2022, 2, 10000, 1;
EXEC sp_dbmmonitorchangealert AdventureWorks2022, 3, 10000, 1;
EXEC sp_dbmmonitorchangealert AdventureWorks2022, 4, 1000, 0;
EXEC sp_dbmmonitorchangealert AdventureWorks2022, 5, 8, 1;
```

## Related content

- [Monitoring Database Mirroring (SQL Server)](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)
- [sp_dbmmonitorhelpalert (Transact-SQL)](sp-dbmmonitorhelpalert-transact-sql.md)
- [sp_dbmmonitordropalert (Transact-SQL)](sp-dbmmonitordropalert-transact-sql.md)
