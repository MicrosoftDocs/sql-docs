---
title: "sp_syscollector_delete_collector_type (Transact-SQL)"
description: Deletes the definition of a collector type.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_delete_collector_type"
  - "sp_syscollector_delete_collector_type_TSQL"
helpviewer_keywords:
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_delete_collector_type"
dev_langs:
  - "TSQL"
---
# sp_syscollector_delete_collector_type (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes the definition of a collector type.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_delete_collector_type
    [ [ @collector_type_uid = ] 'collector_type_uid' ]
    [ , [ @name = ] N'name' ]
[ ; ]
```

## Arguments

#### [ @collector_type_uid = ] '*collector_type_uid*'

The GUID for the collector type. *@collector_type_uid* is **uniqueidentifier**, with a default of `NULL`, and must have a value if *@name* is `NULL`.

#### [ @name = ] N'*name*'

The name of the collector type. *@name* is **sysname**, and must have a value if *@collector_type_uid* is `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Either *@collector_type_uid* or *@name* must have a value; both can't be `NULL`.

This procedure throws an error if collection items of this collection type exist.

## Permissions

Requires membership in the **dc_admin** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

This example deletes the Generic T-SQL Query collector type.

```sql
USE msdb;
GO
EXEC sp_syscollector_delete_collector_type
    @collector_type_uid = '302E93D1-3424-4be7-AA8E-84813ECF2419';
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
