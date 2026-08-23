---
title: "DROP RESOURCE POOL (Transact-SQL)"
description: DROP RESOURCE POOL (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 02/17/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP RESOURCE POOL"
  - "DROP_RESOURCE_POOL_TSQL"
helpviewer_keywords:
  - "DROP RESOURCE POOL"
dev_langs:
  - "TSQL"
---

# DROP RESOURCE POOL (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

Deletes a user-defined [resource governor](../../relational-databases/resource-governor/resource-governor.md) resource pool for a [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance.

> [!NOTE]
> To modify resource governor configuration in [!INCLUDE[ssazuremi-md.md](../../includes/ssazuremi-md.md)], you must be in the context of the `master` database on the primary replica.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DROP RESOURCE POOL pool_name
[ ; ]
```

## Arguments

#### *pool_name*

Is the name of an existing user-defined resource pool.

## Remarks

You can't drop a resource pool if it contains any workload groups.

You can't drop the built-in `default` and `internal` pools.

For more information, see [Resource governor](../../relational-databases/resource-governor/resource-governor.md) and [Resource governor resource pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md).

## Permissions

Requires the `CONTROL SERVER` permission.

## Examples

The following example deletes the resource pool named `big_pool` and makes the new configuration effective.

```sql
DROP RESOURCE POOL big_pool;

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

## Related content

- [Resource governor](../../relational-databases/resource-governor/resource-governor.md)
- [Resource governor resource pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)
- [Delete a resource pool](../../relational-databases/resource-governor/delete-a-resource-pool.md)
- [CREATE RESOURCE POOL (Transact-SQL)](create-resource-pool-transact-sql.md)
- [ALTER RESOURCE POOL (Transact-SQL)](alter-resource-pool-transact-sql.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](create-workload-group-transact-sql.md)
- [ALTER WORKLOAD GROUP (Transact-SQL)](alter-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP (Transact-SQL)](drop-workload-group-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](alter-resource-governor-transact-sql.md)
