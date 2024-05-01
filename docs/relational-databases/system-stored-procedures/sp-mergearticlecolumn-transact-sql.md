---
title: "sp_mergearticlecolumn (Transact-SQL)"
description: "Partitions a merge publication vertically. This stored procedure is executed at the Publisher on the publication database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_mergearticlecolumn"
  - "sp_mergearticlecolumn_TSQL"
helpviewer_keywords:
  - "sp_mergearticlecolumn"
dev_langs:
  - "TSQL"
---
# sp_mergearticlecolumn (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Partitions a merge publication vertically. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_mergearticlecolumn
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    [ , [ @column = ] N'column' ]
    [ , [ @operation = ] N'operation' ]
    [ , [ @schema_replication = ] N'schema_replication' ]
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article in the publication. *@article* is **sysname**, with no default.

#### [ @column = ] N'*column*'

Identifies the columns on which to create the vertical partition. *@column* is **sysname**, with a default of `NULL`. If `NULL` and *@operation* is set to `add`, all columns in the source table are added to the article by default. *@column* can't be `NULL` when *@operation* is set to `drop`. To exclude columns from an article, execute `sp_mergearticlecolumn` and specify *@column*, and set `@operation` to `drop` for each column to be removed from the specified *@article*.

#### [ @operation = ] N'*operation*'

The replication status. *@operation* is **nvarchar(4)**, with a default of `add`.

- `add` marks the column for replication.
- `drop` clears the column.

#### [ @schema_replication = ] N'*schema_replication*'

Specifies that a schema change is propagated when the Merge Agent runs. *@schema_replication* is **nvarchar(5)**, with a default of `false`.

Only `false` is supported for *@schema_replication*.

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Enables or disables the ability to have a snapshot invalidated. *@force_invalidate_snapshot* is **bit**, with a default of `0`.

- `0` specifies that changes to the merge article don't cause the snapshot to be invalid.

- `1` specifies that changes to the merge article might cause the snapshot to be invalid, and if that is the case, a value of `1` gives permission for the new snapshot to occur.

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Enables or disables the ability to have the subscription reinitialized. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that changes to the merge article don't cause the subscription to be reinitialized.

- `1` specifies that changes to the merge article might cause the subscription to be reinitialized, and if that is the case, a value of `1` gives permission for the subscription reinitialization to occur.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_mergearticlecolumn` is used in merge replication.

An identity column can't be dropped from the article if automatic identity range management is being used. For more information, see [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md).

If an application sets a new vertical partition after the initial snapshot is created, a new snapshot must be generated and reapplied to each subscription. Snapshots are applied when the next scheduled snapshot and distribution or merge agent run.

If row tracking is used for conflict detection (the default), the base table can include a maximum of 1,024 columns, but columns must be filtered from the article so that a maximum of 246 columns is published. If column tracking is used, the base table can include a maximum of 246 columns.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-mergearticlecolumn-tr_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_mergearticlecolumn`.

## Related content

- [Define and Modify a Join Filter Between Merge Articles](../replication/publish/define-and-modify-a-join-filter-between-merge-articles.md)
- [Define and Modify a Parameterized Row Filter for a Merge Article](../replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md)
- [Filter Published Data](../replication/publish/filter-published-data.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
