---
title: "sp_polybase_leave_group (Transact-SQL)"
description: Removes a SQL Server instance from a PolyBase group for scale-out computation.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
f1_keywords:
  - "sp_polybase_leave_group"
  - "sp_polybase_leave_group_TSQL"
helpviewer_keywords:
  - "sp_polybase_leave_group"
dev_langs:
  - "TSQL"
---
# sp_polybase_leave_group (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Removes a SQL Server instance from a PolyBase group for scale-out computation.

The SQL Server instance must have the [PolyBase Guide](../polybase/polybase-guide.md) feature installed. PolyBase enables the integration of non-SQL Server data sources, such as Hadoop and Azure Blob Storage. See also [sp_polybase_join_group](polybase-stored-procedures-sp-polybase-join-group.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_polybase_leave_group;
```

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires CONTROL SERVER permission.

## Remarks

You can only remove a compute node from a group.

After running the stored procedure, restart the PolyBase engine and PolyBase Data Movement Service on the machine. To verify, run the following DMV on the head node:

```sql
EXEC sys.dm_exec_compute_nodes;
```

## Examples

The example removes the current machine from a PolyBase group.

```sql
EXEC sp_polybase_leave_group;
```

## Related content

- [Get started with PolyBase](../polybase/polybase-guide.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
