---
title: "sp_syscollector_run_collection_set (Transact-SQL)"
description: Starts a collection set if the collector is already enabled, and the collection set is configured for non-cached collection mode.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_run_collection_set_TSQL"
  - "sp_syscollector_run_collection_set"
helpviewer_keywords:
  - "sp_syscollector_run_collection_set"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# sp_syscollector_run_collection_set (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Starts a collection set if the collector is already enabled, and the collection set is configured for non-cached collection mode.

> [!NOTE]  
> This procedure fails if it's run against a collection set that is configured for cached collection mode.

`sp_syscollector_run_collection_set` enables a user to take on-demand data snapshots.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_run_collection_set
    [ [ @collection_set_id = ] collection_set_id ]
    [ , [ @name = ] N'name' ]
[ ; ]
```

## Arguments

#### [ @collection_set_id = ] *collection_set_id*

The unique local identifier for the collection set. *@collection_set_id* is **int**, with a default of `NULL`, and must have a value if *@name* is `NULL`.

#### [ @name = ] N'*name*'

The name of the collection set. *@name* is **sysname**, with a default of `NULL`, and must have a value if *@collection_set_id* is `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Either *@collection_set_id* or *@name* must have a value, both can't be `NULL`.

This procedure starts the collection and upload jobs for the specified collection set, and immediately starts the collection agent job if the collection set has its *@collection_mode* set to non-cached (`1`). For more information, see [sp_syscollector_create_collection_set](sp-syscollector-create-collection-set-transact-sql.md).

`sp_syscollector_run_collection_set` can also be used to run a collection set that doesn't have a schedule.

## Permissions

Requires membership in the **dc_operator** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

Start a collection set using its identifier.

```sql
USE msdb;
GO
EXEC sp_syscollector_run_collection_set
    @collection_set_id = 1;
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
