---
title: "sp_syscollector_start_collection_set (Transact-SQL)"
description: Starts a collection set if the collector is already enabled and the collection set isn't running.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_start_collection_set_TSQL"
  - "sp_syscollector_start_collection_set"
helpviewer_keywords:
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_start_collection_set"
dev_langs:
  - "TSQL"
---
# sp_syscollector_start_collection_set (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Starts a collection set if the collector is already enabled and the collection set isn't running. If the collector isn't enabled, enable the collector by running [sp_syscollector_enable_collector](sp-syscollector-enable-collector-transact-sql.md) and then use this stored procedure to start a collection set.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_start_collection_set
    [ [ @collection_set_id = ] collection_set_id ]
    [ , [ @name = ] N'name' ]
[ ; ]
```

## Arguments

#### [ @collection_set_id = ] *collection_set_id*

The unique local identifier for the collection set. *@collection_set_id* is **int**, with a default of `NULL`. *@collection_set_id* must have a value if *@name* is `NULL`.

#### [ @name = ] N'*name*'

The name of the collection set. *@name* is **sysname**, with a default of `NULL`. *@name* must have a value if *@collection_set_id* is `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_syscollector_create_collection_set` must be run in the context of the `msdb` system database and SQL Server Agent must be enabled.

This procedure fails when run against a collection set that doesn't have a schedule. If the collection set doesn't have a schedule (because its collection mode is set to non-cached, for example), use the [sp_syscollector_run_collection_set](sp-syscollector-run-collection-set-transact-sql.md) stored procedure to start the collection set.

This procedure enables the collection and upload jobs for the specified collection set, and immediately starts the collection agent job if the collection set has its collection mode set to cached (`0`). For more information, see [sp_syscollector_create_collection_set](sp-syscollector-create-collection-set-transact-sql.md).

If the collection set doesn't contain any collection items, this operation has no effect. Error 14685 is returned as a warning.

## Permissions

Requires membership in the **dc_operator** fixed database role to execute this procedure. If the collection set doesn't have a proxy account, membership in the **sysadmin** fixed server role is required.

## Examples

The following example starts a collection set using its identifier.

```sql
USE msdb;
GO
EXEC sp_syscollector_start_collection_set @collection_set_id = 1;
```

## Related content

- [Data collector stored procedures (Transact-SQL)](data-collector-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
- [syscollector_collection_sets (Transact-SQL)](../system-catalog-views/syscollector-collection-sets-transact-sql.md)
