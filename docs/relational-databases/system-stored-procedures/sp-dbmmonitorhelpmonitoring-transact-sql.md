---
title: "sp_dbmmonitorhelpmonitoring (Transact-SQL)"
description: sp_dbmmonitorhelpmonitoring returns the current update period.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbmmonitorhelpmonitoring"
  - "sp_dbmmonitorhelpmonitoring_TSQL"
helpviewer_keywords:
  - "sp_dbmmonitorhelpmonitoring"
  - "database mirroring [SQL Server], monitoring"
dev_langs:
  - "TSQL"
---
# sp_dbmmonitorhelpmonitoring (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the current update period.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dbmmonitorhelpmonitoring
[ ; ]
```

## Arguments

None.

## Return code values

None.

## Result set

Returns the current update period, that is, the number of minutes between updates of database mirroring status table. This value ranges from 1 to 120 minutes.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example returns the current update period.

```sql
EXEC sp_dbmmonitorhelpmonitoring;
```

## Related content

- [Monitoring Database Mirroring (SQL Server)](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)
- [sp_dbmmonitorresults (Transact-SQL)](sp-dbmmonitorresults-transact-sql.md)
