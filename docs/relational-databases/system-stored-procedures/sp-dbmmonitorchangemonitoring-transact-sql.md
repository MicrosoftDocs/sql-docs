---
title: "sp_dbmmonitorchangemonitoring (Transact-SQL)"
description: sp_dbmmonitorchangemonitoring changes the value of a database mirroring monitoring parameter.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbmmonitorchangemonitoring"
  - "sp_dbmmonitorchangemonitoring_TSQL"
helpviewer_keywords:
  - "sp_dbmmonitorchangemonitoring"
  - "database mirroring [SQL Server], monitoring"
dev_langs:
  - "TSQL"
---
# sp_dbmmonitorchangemonitoring (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the value of a database mirroring monitoring parameter.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dbmmonitorchangemonitoring
    [ @parameter_id = ] parameter_id
    , [ @value = ] value
[ ; ]
```

## Arguments

#### [ @parameter_id = ] *parameter_id*

Specifies the identifier of the parameter to be changed. *@parameter_id* is **int**, with no default. Currently, only the following parameter is available:

| Parameter | Description of value |
| --- | --- |
| `1` | The number of minutes between updates to the database mirroring status table. The default interval is 1 minute. |

#### [ @value = ] *value*

Specifies the new value for the parameter that is being changed. *@value* is **int**, with no default.

| Parameter | Description of value |
| --- | --- |
| `1` | An integer in the range of 1 to 120 that specifies a new update period in minutes. |

## Return code values

None.

## Result set

None.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example changes the update period to 5 minutes.

```sql
EXEC sp_dbmmonitorchangemonitoring 1, 5;
```

## Related content

- [Monitoring Database Mirroring (SQL Server)](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)
- [sp_dbmmonitoraddmonitoring (Transact-SQL)](sp-dbmmonitoraddmonitoring-transact-sql.md)
- [sp_dbmmonitordropmonitoring (Transact-SQL)](sp-dbmmonitordropmonitoring-transact-sql.md)
- [sp_dbmmonitorhelpmonitoring (Transact-SQL)](sp-dbmmonitorhelpmonitoring-transact-sql.md)
- [sp_dbmmonitorresults (Transact-SQL)](sp-dbmmonitorresults-transact-sql.md)
