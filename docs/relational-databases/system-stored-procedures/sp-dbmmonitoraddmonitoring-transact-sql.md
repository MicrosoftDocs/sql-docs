---
title: "sp_dbmmonitoraddmonitoring (Transact-SQL)"
description: sp_dbmmonitoraddmonitoring creates a database mirroring monitor job that periodically updates the mirroring status for every mirrored database on the server instance.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbmmonitoraddmonitoring"
  - "sp_dbmmonitoraddmonitoring_TSQL"
helpviewer_keywords:
  - "database mirroring [SQL Server], monitoring"
  - "sp_dbmmonitoraddmonitoring"
dev_langs:
  - "TSQL"
---
# sp_dbmmonitoraddmonitoring (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a database mirroring monitor job that periodically updates the mirroring status for every mirrored database on the server instance.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dbmmonitoraddmonitoring [ [ @update_period = ] update_period ]
[ ; ]
```

## Arguments

#### [ @update_period = ] *update_period*

Specifies the interval between updates in minutes. *@update_period* is **int**, with a default of `1`. This value can be from 1 to 120 minutes.

If update period is set too low, the response time might increase for clients.

## Return code values

None.

## Result set

None.

## Remarks

This procedure requires that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent is allowed to run on the server instance. For the database mirroring monitor job to run, Agent must be running.

If database mirroring is started from [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the `sp_dbmmonitoraddmonitoring` procedure is run automatically. If you start mirroring up manually using `ALTER DATABASE` statements, to monitor mirrored database on the server instance, you must run `sp_dbmmonitoraddmonitoring` manually.

> [!NOTE]  
> If you run `sp_dbmmonitoraddmonitoring` before you set up database mirroring, the monitoring job will run but will not update the status table in which database mirroring monitor history is stored.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example starts monitoring with an update period of `3` minutes.

```sql
EXEC sp_dbmmonitoraddmonitoring 3;
```

## Related content

- [Monitoring Database Mirroring (SQL Server)](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)
- [sp_dbmmonitorchangemonitoring (Transact-SQL)](sp-dbmmonitorchangemonitoring-transact-sql.md)
- [sp_dbmmonitordropmonitoring (Transact-SQL)](sp-dbmmonitordropmonitoring-transact-sql.md)
- [sp_dbmmonitorhelpmonitoring (Transact-SQL)](sp-dbmmonitorhelpmonitoring-transact-sql.md)
- [sp_dbmmonitorresults (Transact-SQL)](sp-dbmmonitorresults-transact-sql.md)
