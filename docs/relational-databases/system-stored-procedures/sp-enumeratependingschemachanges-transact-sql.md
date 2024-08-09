---
title: "sp_enumeratependingschemachanges (Transact-SQL)"
description: sp_enumeratependingschemachanges returns a list of all pending schema changes.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_enumeratependingschemachanges"
  - "sp_enumeratependingschemachanges_TSQL"
helpviewer_keywords:
  - "sp_enumeratependingschemachanges"
dev_langs:
  - "TSQL"
---
# sp_enumeratependingschemachanges (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a list of all pending schema changes. This stored procedure can be used with [sp_markpendingschemachange](sp-markpendingschemachange-transact-sql.md), which enables an administrator to skip selected pending schema changes so that they aren't replicated. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_enumeratependingschemachanges
    [ @publication = ] N'publication'
    [ , [ @starting_schemaversion = ] starting_schemaversion ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @starting_schemaversion = ] *starting_schemaversion*

The lowest number schema change to include in the result set. *@starting_schemaversion* is **int**, with a default of `0`.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `article_name` | **sysname** | Name of the article to which the schema change applies, or `Publication-wide` for schema changes that apply to the entire publication. |
| `schemaversion` | **int** | Number of the pending schema change. |
| `schematype` | **sysname** | A text value that represents the type of schema change. |
| `schematext` | **nvarchar(max)** | [!INCLUDE [tsql](../../includes/tsql-md.md)] that describes the schema change. |
| `schemastatus` | **nvarchar(10)** | Indicates if a schema change is pending for the article, which can be one of the following values:<br /><br />`active` = schema change is pending<br />`inactive` = schema change is inactive<br />`skip` = schema change isn't replicated |
| `schemaguid` | **uniqueidentifier** | Identifies the schema change. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_enumeratependingschemachanges` is used in merge replication.

`sp_enumeratependingschemachanges`, used with [sp_markpendingschemachange](sp-markpendingschemachange-transact-sql.md), is intended for the supportability of merge replication and should be used only when other corrective actions, such as reinitialization, fail to correct the situation.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_enumeratependingschemachanges`.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
- [sysmergeschemachange (Transact-SQL)](../system-tables/sysmergeschemachange-transact-sql.md)
