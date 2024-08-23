---
title: "sp_syscollector_delete_collection_set (Transact-SQL)"
description: Deletes a user-defined collection set and all its collection items.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_delete_collection_set_TSQL"
  - "sp_syscollector_delete_collection_set"
helpviewer_keywords:
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_delete_collecton_set"
dev_langs:
  - "TSQL"
---
# sp_syscollector_delete_collection_set (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes a user-defined collection set and all its collection items.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_delete_collection_set
    [ [ @collection_set_id = ] collection_set_id ]
    [ , [ @name = ] N'name' ]
[ ; ]
```

## Arguments

#### [ @collection_set_id = ] *collection_set_id*

The unique identifier for the collection set. *@collection_set_id* is **int**, with a default of `NULL`. *@collection_set_id* must have a value if *@name* is `NULL`.

#### [ @name = ] N'*name*'

The name of the collection set. *@name* is **sysname**, with a default of `NULL`. *@name* must have a value if *@collection_set_id* is `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_syscollector_delete_collection_set` must be run in the context of the `msdb` system database.

Either *@collection_set_id* or *@name* must have a value, both can't be `NULL`. To obtain these values, query the `syscollector_collection_set` system view.

System-defined collection sets can't be deleted.

## Permissions

Requires membership in the **dc_admin** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

The following example deletes a user-defined collection set specifying the *@collection_set_id*.

```sql
USE msdb;
GO
EXEC dbo.sp_syscollector_delete_collection_set
    @collection_set_id = 4;
```

## Related content

- [Data collector stored procedures (Transact-SQL)](data-collector-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
- [syscollector_collection_sets (Transact-SQL)](../system-catalog-views/syscollector-collection-sets-transact-sql.md)
