---
title: "sp_schemafilter (Transact-SQL)"
description: Modifies and displays information on the schema that is excluded when listing Oracle tables eligible for publishing.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_schemafilter_TSQL"
  - "sp_schemafilter"
helpviewer_keywords:
  - "sp_schemafilter"
dev_langs:
  - "TSQL"
---
# sp_schemafilter (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Modifies and displays information on the schema that is excluded when listing Oracle tables eligible for publishing.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_schemafilter
    [ @publisher = ] N'publisher'
    [ , [ @schema = ] N'schema' ]
    [ , [ @operation = ] N'operation' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.*@publisher* is **sysname**, with no default.

#### [ @schema = ] N'*schema*'

The name of the schema. *@schema* is **sysname**, with a default of `NULL`.

#### [ @operation = ] N'*operation*'

The action to be taken on this schema. *@operation* is **nvarchar(4)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `add` | Adds the specified schema to the list of schemas that aren't eligible for publication. |
| `drop` | Drops the specified schema from the list of schemas that aren't eligible for publication. |
| `help` (default) | Returns the list of schemas that aren't eligible for publication. |

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `schemaname` | **sysname** | The name of the schema not eligible for publication. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_schemafilter` should only be used for heterogeneous publishers.

## Permissions

Only members of the **sysadmin** fixed server role at the Distributor can execute `sp_schemafilter`.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
