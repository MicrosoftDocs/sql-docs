---
title: "sp_syscollector_delete_collection_item (Transact-SQL)"
description: Deletes a collection item from a collection set.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_delete_collection_item"
  - "sp_syscollector_delete_collection_item_TSQL"
helpviewer_keywords:
  - "sp_syscollector_delete_collecton_item"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# sp_syscollector_delete_collection_item (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes a collection item from a collection set.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_delete_collection_item
    [ [ @collection_item_id = ] collection_item_id ]
    [ , [ @name = ] N'name' ]
[ ; ]
```

## Arguments

#### [ @collection_item_id = ] *collection_item_id*

The unique identifier for the collection item. *@collection_item_id* is **int**, with a default of `NULL`. *@collection_item_id* must have a value if *@name* is `NULL`.

#### [ @name = ] N'*name*'

The name of the collection item. *@name* is **sysname**, with a default of an empty string. *@name* must have a value if *@collection_item_id* is `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_syscollector_delete_collection_item` must be run in the context of the `msdb` system database. Collection items can't be deleted from system collection sets.

The collection set that contains the collection item is stopped and restarted during this operation.

## Permissions

Requires membership in the **dc_admin** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

The following example deletes a collection item named `MyCollectionItem1`.

```sql
USE msdb;
GO
EXEC sp_syscollector_delete_collection_item @name = 'MyCollectionItem1';
```

## Related content

- [Data collection](../data-collection/data-collection.md)
- [sp_syscollector_create_collection_item (Transact-SQL)](sp-syscollector-create-collection-item-transact-sql.md)
- [Data Collector stored procedures (Transact-SQL)](data-collector-stored-procedures-transact-sql.md)
- [syscollector_collection_items (Transact-SQL)](../system-catalog-views/syscollector-collection-items-transact-sql.md)
