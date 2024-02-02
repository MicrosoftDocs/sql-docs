---
title: "sp_syscollector_enable_collector (Transact-SQL)"
description: Enables the data collector.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_enable_collector"
  - "sp_syscollector_enable_collector_TSQL"
helpviewer_keywords:
  - "sp_syscollector_enable_collector"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# sp_syscollector_enable_collector (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Enables the data collector. Because there is only one data collector per server, no parameters are required.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_enable_collector
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Defaults to the data collector on the server.

## Permissions

Requires membership in the **dc_admin** or **dc_operator** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

The following example enables the data collector.

```sql
USE msdb;
GO
EXEC dbo.sp_syscollector_enable_collector;
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
