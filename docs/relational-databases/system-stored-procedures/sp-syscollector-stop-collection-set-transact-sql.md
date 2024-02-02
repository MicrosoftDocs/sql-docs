---
title: "sp_syscollector_stop_collection_set (Transact-SQL)"
description: Stops a collection set.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_stop_collection_set_TSQL"
  - "sp_syscollector_stop_collection_set"
helpviewer_keywords:
  - "sp_syscollector_stop_collection_set"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# sp_syscollector_stop_collection_set (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Stops a collection set.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_stop_collection_set
    [ [ @collection_set_id = ] collection_set_id ]
    [ , [ @name = ] N'name' ]
    [ , [ @stop_collection_job = ] stop_collection_job ]
[ ; ]
```

## Arguments

#### [ @collection_set_id = ] *collection_set_id*

The unique local identifier for the collection set. *@collection_set_id* is **int**, with a default of `NULL`. *@collection_set_id* must have a value if *@name* is `NULL`.

#### [ @name = ] N'*name*'

The name of the collection set. *@name* is **sysname**, with a default of `NULL`. *@name* must have a value if *@collection_set_id* is `NULL`.

#### [ @stop_collection_job = ] *stop_collection_job*

Specifies that the collection job for the collection set should be stopped if it is running. *@stop_collection_job* is **bit**, with a default `1`.

*@stop_collection_job* applies only to collection sets with collection mode set to cached. For more information, see [sp_syscollector_create_collection_set (Transact-SQL)](sp-syscollector-create-collection-set-transact-sql.md).

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_syscollector_create_collection_set` must be run in the context of the `msdb` system database.

## Permissions

Requires membership in the **dc_operator** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

The following example stops a collection set using its identifier.

```sql
USE msdb;
GO
EXEC sp_syscollector_stop_collection_set
    @collection_set_id = 1;
```

## Related content

- [Data collection](../data-collection/data-collection.md)
- [Data Collector stored procedures (Transact-SQL)](data-collector-stored-procedures-transact-sql.md)
