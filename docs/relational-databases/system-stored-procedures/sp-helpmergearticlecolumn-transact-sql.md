---
title: "sp_helpmergearticlecolumn (Transact-SQL)"
description: "Returns the list of columns in the specified table or view article for a merge publication."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpmergearticlecolumn"
  - "sp_helpmergearticlecolumn_TSQL"
helpviewer_keywords:
  - "sp_helpmergearticlecolumn"
dev_langs:
  - "TSQL"
---
# sp_helpmergearticlecolumn (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the list of columns in the specified table or view article for a merge publication. Because stored procedures don't have columns, this stored procedure returns an error if a stored procedure is specified as the article. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpmergearticlecolumn
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of a table or view that is the article to retrieve information on. *@article* is **sysname**, with no default.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `column_id` | **sysname** | Identifies the column. |
| `column_name` | **sysname** | The name of the column for a table or view. |
| `published` | **bit** | Specifies if the column name is published.<br /><br />`1` specifies that the column is being published.<br /><br />`0` specifies that it isn't published. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpmergearticlecolumn` is used in merge replication.

## Permissions

Only members of the **replmonitor** fixed database role in the distribution database or the publication access list for the publication can execute `sp_helpmergearticlecolumn`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
