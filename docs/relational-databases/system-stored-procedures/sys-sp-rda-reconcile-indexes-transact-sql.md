---
title: "sys.sp_rda_reconcile_indexes (Transact-SQL)"
description: Queues a schema task to reconcile indexes on the remote table. After this task finishes successfully, the remote table has the same indexes that exist on the local Stretch-enabled table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sp_rda_reconcile_indexes"
  - "sp_rda_reconcile_indexes_TSQL"
helpviewer_keywords:
  - "sys.sp_rda_reconcile_indexes stored procedure"
dev_langs:
  - "TSQL"
---
# sys.sp_rda_reconcile_indexes (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Queues a schema task to reconcile indexes on the remote table. After this task finishes successfully, the remote table has the same indexes that exist on the local Stretch-enabled table.

> [!IMPORTANT]  
> [!INCLUDE [stretch-database-deprecation](../../includes/stretch-database-deprecation.md)]

If another task is queued to reconcile indexes when you call `sp_rda_reconcile_indexes`, this stored procedure doesn't queue a duplicate task.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_rda_reconcile_indexes [ @objname = ] 'objname'
[ ; ]
```

## Arguments

#### [ @objname = ] '*objname*'

The qualified or non-qualified name of the Stretch-enabled table for which you want to reconcile indexes. Quotes are required only if you specify a qualified object.

## Return code values

`0` (success) or `> 0` (failure).

## Related content

- [Stretch Database](../../sql-server/stretch-database/stretch-database.md)
