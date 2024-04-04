---
title: "sp_helpmergefilter (Transact-SQL)"
description: "Returns information about merge filters. This stored procedure is executed at the Publisher on any database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpmergefilter"
  - "sp_helpmergefilter_TSQL"
helpviewer_keywords:
  - "sp_helpmergefilter"
dev_langs:
  - "TSQL"
---
# sp_helpmergefilter (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about merge filters. This stored procedure is executed at the Publisher on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpmergefilter
    [ @publication = ] N'publication'
    [ , [ @article = ] N'article' ]
    [ , [ @filtername = ] N'filtername' ]
    [ , [ @filter_type_bm = ] filter_type_bm ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article. *@article* is **sysname**, with a default of `%`, which returns the names of all articles.

#### [ @filtername = ] N'*filtername*'

The name of the filter about which to return information. *@filtername* is **sysname**, with a default of `%`, which returns information about all the filters defined on the article or publication.

#### [ @filter_type_bm = ] *filter_type_bm*

Bitmap filter for the filter type, using merge filters from `dbo.sysmergesubsetfilters`. *@filter_type_bm* is **binary(1)**, and can be one of the following values:

| Value | Description |
| --- | --- |
| `1` (default) | Return the filters that have a `filter_type` of `1` or `3` (join filters) |
| `2` | Return the filters that have a `filter_type` of `2` or `3` (logical record filters, or filters that are both logical record filter and join filter) |
| `3` | Return the filters that have a `filter_type` of `1`, `2`, or `3` (filters that are either join filter or logical record filter, or both) |

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `join_filterid` | **int** | ID of the join filter. |
| `filtername` | **sysname** | Name of the filter. |
| `join article name` | **sysname** | Name of the join article. |
| `join_filterclause` | **nvarchar(2000)** | Filter clause qualifying the join. |
| `join_unique_key` | **int** | Specifies whether the join is on a unique key. |
| `base table owner` | **sysname** | Name of the owner of the base table. |
| `base table name` | **sysname** | Name of the base table. |
| `join table owner` | **sysname** | Name of the owner of the table being joined to the base table. |
| `join table name` | **sysname** | Name of the table being joined to the base table. |
| `article name` | **sysname** | Name of the table article being joined to the base table. |
| `filter_type` | **tinyint** | Type of merge filter, which can be one of the following values:<br /><br />`1` = join filter only<br /><br />`2` = logical record relationship<br /><br />`3` = both |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpmergefilter` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute `sp_helpmergefilter`.

## Related content

- [sp_addmergefilter (Transact-SQL)](sp-addmergefilter-transact-sql.md)
- [sp_changemergefilter (Transact-SQL)](sp-changemergefilter-transact-sql.md)
- [sp_dropmergefilter (Transact-SQL)](sp-dropmergefilter-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
