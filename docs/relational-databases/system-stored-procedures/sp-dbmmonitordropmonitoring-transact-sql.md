---
title: "sp_dbmmonitordropmonitoring (Transact-SQL)"
description: sp_dbmmonitordropmonitoring stops and deletes the mirroring monitor job for all the databases on the server instance.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/23/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbmmonitordropmonitoring_TSQL"
  - "sp_dbmmonitordropmonitoring"
helpviewer_keywords:
  - "sp_dbmmonitordropmonitoring"
  - "database mirroring [SQL Server], monitoring"
dev_langs:
  - "TSQL"
---
# sp_dbmmonitordropmonitoring (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Stops and deletes the mirroring monitor job for all the databases on the server instance.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dbmmonitordropmonitoring
[ ; ]
```

## Arguments

None.

## Return code values

None.

## Result set

None.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example drops database mirroring monitoring on all of the mirrored databases on the server instance.

```sql
EXECUTE sp_dbmmonitordropmonitoring;
```

## Related content

- [Monitoring Database Mirroring (SQL Server)](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)
- [sys.sp_dbmmonitorchangemonitoring (Transact-SQL)](sp-dbmmonitorchangemonitoring-transact-sql.md)
- [sys.sp_dbmmonitoraddmonitoring (Transact-SQL)](sp-dbmmonitoraddmonitoring-transact-sql.md)
- [sp_dbmmonitorhelpmonitoring (Transact-SQL)](sp-dbmmonitorhelpmonitoring-transact-sql.md)
- [sys.sp_dbmmonitorresults (Transact-SQL)](sp-dbmmonitorresults-transact-sql.md)
