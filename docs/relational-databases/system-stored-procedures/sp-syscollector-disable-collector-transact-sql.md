---
title: "sp_syscollector_disable_collector (Transact-SQL)"
description: Disables the data collector.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: vanto
ms.date: 06/23/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_disable_collector_TSQL"
  - "sp_syscollector_disable_collector"
helpviewer_keywords:
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_disable_collector"
dev_langs:
  - "TSQL"
---
# sp_syscollector_disable_collector (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Disables the data collector. Because there's only one data collector per server, no parameters are required.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_disable_collector
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

The following example disables the data collector.

```sql
EXECUTE dbo.sp_syscollector_disable_collector;
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
