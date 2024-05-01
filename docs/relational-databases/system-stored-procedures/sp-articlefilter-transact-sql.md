---
title: "sp_articlefilter (Transact-SQL)"
description: sp_articlefilter filters data that are published based on a table article.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_articlefilter_TSQL"
  - "sp_articlefilter"
helpviewer_keywords:
  - "sp_articlefilter"
dev_langs:
  - "TSQL"
---
# sp_articlefilter (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Filters data that is published based on a table article. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_articlefilter
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    [ , [ @filter_name = ] N'filter_name' ]
    [ , [ @filter_clause = ] N'filter_clause' ]
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication that contains the article. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article. *@article* is **sysname**, with no default.

#### [ @filter_name = ] N'*filter_name*'

The name of the filter stored procedure to be created from the *@filter_name*. *@filter_name* is **nvarchar(517)**, with a default of `NULL`. You must specify a unique name for the article filter.

#### [ @filter_clause = ] N'*filter_clause*'

A restriction (`WHERE`) clause that defines a horizontal filter. When entering the restriction clause, omit the keyword `WHERE`. *@filter_clause* is **nvarchar(max)**, with a default of `NULL`.

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Acknowledges that the action taken by this stored procedure might invalidate an existing snapshot. *@force_invalidate_snapshot* is **bit**, with a default of `0`.

- `0` specifies that changes to the article don't cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.
- `1` specifies that changes to the article might cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Acknowledges that the action taken by this stored procedure might require existing subscriptions to be reinitialized. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that changes to the article don't cause a need for subscriptions to be reinitialized. If the stored procedure detects that the change would require subscriptions to be reinitialized, an error occurs and no changes are made.
- `1` specifies that changes to the article cause existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used with a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_articlefilter` is used in snapshot replication and transactional replication.

Executing `sp_articlefilter` for an article with existing subscriptions requires that those subscriptions to be reinitialized.

`sp_articlefilter` creates the filter, inserts the ID of the filter stored procedure in the `filter` column of the [sysarticles (Transact-SQL)](../system-tables/sysarticles-transact-sql.md) table, and then inserts the text of the restriction clause in the `filter_clause` column.

To create an article with a horizontal filter, execute [sp_addarticle (Transact-SQL)](sp-addarticle-transact-sql.md) with no *@filter_name* parameter. Execute `sp_articlefilter`, providing all parameters including *@filter_clause*, and then execute [sp_articleview (Transact-SQL)](sp-articleview-transact-sql.md), providing all parameters including the identical *@filter_clause*. If the filter already exists and if the `type` in `sysarticles` is `1` (log-based article), the previous filter is deleted and a new filter is created.

If *@filter_name* and *@filter_clause* aren't provided, the previous filter is deleted and the filter ID is set to `0`.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-articlefilter-transac_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_articlefilter`.

## Related content

- [Define an Article](../replication/publish/define-an-article.md)
- [Define and Modify a Static Row Filter](../replication/publish/define-and-modify-a-static-row-filter.md)
- [sp_addarticle (Transact-SQL)](sp-addarticle-transact-sql.md)
- [sp_articleview (Transact-SQL)](sp-articleview-transact-sql.md)
- [sp_changearticle (Transact-SQL)](sp-changearticle-transact-sql.md)
- [sp_droparticle (Transact-SQL)](sp-droparticle-transact-sql.md)
- [sp_helparticle (Transact-SQL)](sp-helparticle-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
