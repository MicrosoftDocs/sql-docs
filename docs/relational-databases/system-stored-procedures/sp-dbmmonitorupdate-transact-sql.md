---
title: "sp_dbmmonitorupdate (Transact-SQL)"
description: Updates the database mirroring monitor status table by inserting a new table row for each mirrored database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbmmonitorupdate"
  - "sp_dbmmonitorupdate_TSQL"
helpviewer_keywords:
  - "sp_dbmmonitorupdate"
  - "database mirroring [SQL Server], monitoring"
dev_langs:
  - "TSQL"
---
# sp_dbmmonitorupdate (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Updates the database mirroring monitor status table by inserting a new table row for each mirrored database, and truncates rows older than the current retention period. The default retention period is seven days (168 hours). When `sp_dbmmonitorupdate` updates the table, it evaluates the performance metrics.

> [!NOTE]  
> The first time `sp_dbmmonitorupdate` runs, it creates the database mirroring status table and the **dbm_monitor** fixed database role in the `msdb` database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dbmmonitorupdate [ [ @database_name = ] N'database_name' ]
[ ; ]
```

## Arguments

#### [ @database_name = ] N'*database_name*'

The name of the database for which to update mirroring status. *@database_name* is **sysname**, with a default of `NULL`. If *database_name* isn't specified, the procedure updates the status table for every mirrored database on the server instance.

## Return code values

None.

## Result set

None.

## Remarks

`sp_dbmmonitorupdate` can be executed only in the context of the `msdb` database.

If a column of the status table doesn't apply to the role of a partner, the value is `NULL` on that partner. A column would also have a `NULL` value if the relevant information is unavailable, such as during a failover or server restart.

After `sp_dbmmonitorupdate` creates the **dbm_monitor** fixed database role in the `msdb` database, members of the **sysadmin** fixed server role can add any user to the **dbm_monitor** fixed database role. The **dbm_monitor** role enables its members to view database mirroring status, but not update it but not view or configure database mirroring events.

When `sp_dbmmonitorupdate` updates the mirroring status of a database, it inspects the latest value of any mirroring performance metric for which a warning threshold is specified. If the value exceeds the threshold, the procedure adds an informational event to the event log. All rates are averages since the last update. For more information, see [Use Warning Thresholds and Alerts on Mirroring Performance Metrics (SQL Server)](../../database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md).

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example updates the mirroring status for just the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE msdb;
EXEC sp_dbmmonitorupdate AdventureWorks2022;
```

## Related content

- [Monitoring Database Mirroring (SQL Server)](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)
- [sp_dbmmonitorchangealert (Transact-SQL)](sp-dbmmonitorchangealert-transact-sql.md)
- [sp_dbmmonitorchangemonitoring (Transact-SQL)](sp-dbmmonitorchangemonitoring-transact-sql.md)
- [sp_dbmmonitordropalert (Transact-SQL)](sp-dbmmonitordropalert-transact-sql.md)
- [sp_dbmmonitorhelpalert (Transact-SQL)](sp-dbmmonitorhelpalert-transact-sql.md)
- [sp_dbmmonitorhelpmonitoring (Transact-SQL)](sp-dbmmonitorhelpmonitoring-transact-sql.md)
- [sp_dbmmonitorresults (Transact-SQL)](sp-dbmmonitorresults-transact-sql.md)
