---
title: "sp_helparticlecolumns (Transact-SQL)"
description: sp_helparticlecolumns returns all columns in the underlying table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helparticlecolumns"
  - "sp_helparticlecolumns_TSQL"
helpviewer_keywords:
  - "sp_helparticlecolumns"
dev_langs:
  - "TSQL"
---
# sp_helparticlecolumns (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns all columns in the underlying table. This stored procedure is executed at the Publisher on the publication database. For Oracle Publishers, this stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helparticlecolumns
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication that contains the article. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article that has its columns returned. *@article* is **sysname**, with no default.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be specified when the requested article is published by a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (columns that aren't published) or `1` (columns that are published).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `column id` | **int** | Identifier for the column. |
| `column` | **sysname** | Name of the column. |
| `published` | **bit** | Whether column is published:<br /><br />`0` = No<br />`1` = Yes |
| `publisher type` | **sysname** | Data type of the column at the Publisher. |
| `subscriber type` | **sysname** | Data type of the column at the Subscriber. |

## Remarks

`sp_helparticlecolumns` is used in snapshot and transactional replication.

`sp_helparticlecolumns` is useful in checking a vertical partition.

## Permissions

Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the publication access list for the current publication can execute `sp_helparticlecolumns`.

## Related content

- [Define and Modify a Column Filter](../replication/publish/define-and-modify-a-column-filter.md)
- [sp_addarticle (Transact-SQL)](sp-addarticle-transact-sql.md)
- [sp_articlecolumn (Transact-SQL)](sp-articlecolumn-transact-sql.md)
- [sp_changearticle (Transact-SQL)](sp-changearticle-transact-sql.md)
- [sp_droparticle (Transact-SQL)](sp-droparticle-transact-sql.md)
- [sp_droppublication (Transact-SQL)](sp-droppublication-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
