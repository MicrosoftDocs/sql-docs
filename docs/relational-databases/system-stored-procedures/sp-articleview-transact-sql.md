---
title: "sp_articleview (Transact-SQL)"
description: Creates the view that defines the published article when a table is filtered vertically or horizontally.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_articleview"
  - "sp_articleview_TSQL"
helpviewer_keywords:
  - "sp_articleview"
dev_langs:
  - "TSQL"
---
# sp_articleview (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates the view that defines the published article when a table is filtered vertically or horizontally. This view is used as the filtered source of the schema and data for the destination tables. Only unsubscribed articles can be modified by this stored procedure. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_articleview
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    [ , [ @view_name = ] N'view_name' ]
    [ , [ @filter_clause = ] N'filter_clause' ]
    [ , [ @change_active = ] change_active ]
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @refreshsynctranprocs = ] refreshsynctranprocs ]
    [ , [ @internal = ] internal ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication that contains the article. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article. *@article* is **sysname**, with no default.

#### [ @view_name = ] N'*view_name*'

The name of the view that defines the published article. *@view_name* is **nvarchar(386)**, with a default of `NULL`.

#### [ @filter_clause = ] N'*filter_clause*'

A restriction (`WHERE`) clause that defines a horizontal filter. When entering the restriction clause, omit the `WHERE` keyword. *@filter_clause* is **nvarchar(max)**, with a default of `NULL`.

#### [ @change_active = ] *change_active*

Allows modifying the columns in publications that have subscriptions. *@change_active* is **int**, with a default of `0`.

- If `0`, columns aren't changed.
- If `1`, views can be created or re-created on active articles that have subscriptions.

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Acknowledges that the action taken by this stored procedure might invalidate an existing snapshot. *@force_invalidate_snapshot* is **bit**, with a default of `0`.

- `0` specifies that changes to the article don't cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.
- `1` specifies that changes to the article might cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Acknowledges that the action taken by this stored procedure might require existing subscriptions to be reinitialized. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that changes to the article don't cause the subscription to be reinitialized. If the stored procedure detects that the change would require subscriptions to be reinitialized, an error occurs and no changes are made.
- `1` specifies that changes to the article cause existing subscription to be reinitialized, and gives permission for the subscription reinitialization to occur.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used when publishing from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

#### [ @refreshsynctranprocs = ] *refreshsynctranprocs*

Specifies whether the stored procedures used to synchronize replication are automatically recreated. *@refreshsynctranprocs* is **bit**, with a default of `1`.

- `1` means that the stored procedures are recreated.
- `0` means that the stored procedures aren't recreated.

#### [ @internal = ] *internal*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_articleview` creates the view that defines the published article and inserts the ID of this view in the `sync_objid` column of the [sysarticles](../system-tables/sysarticles-transact-sql.md) table, and inserts the text of the restriction clause in the `filter_clause` column. If all columns are replicated and there's no `filter_clause`, the `sync_objid` in the [sysarticles](../system-tables/sysarticles-transact-sql.md) table is set to the ID of the base table, and the use of `sp_articleview` isn't required.

To publish a vertically filtered table (that is, to filter columns) first run `sp_addarticle` with no *@sync_object* parameter, run [sp_articlecolumn](sp-articlecolumn-transact-sql.md) once for each column to be replicated (defining the vertical filter), and then run `sp_articleview` to create the view that defines the published article.

To publish a horizontally filtered table (that is, to filter rows), run [sp_addarticle](sp-addarticle-transact-sql.md) with no *@filter_name* parameter. Run [sp_articlefilter](sp-articlefilter-transact-sql.md), providing all parameters including *@filter_clause*. Then run `sp_articleview`, providing all parameters including the identical *@filter_clause*.

To publish a vertically and horizontally filtered table, run [sp_addarticle](sp-addarticle-transact-sql.md) with no *@sync_object* or *@filter_name* parameters. Run [sp_articlecolumn](sp-articlecolumn-transact-sql.md) once for each column to be replicated, and then run [sp_articlefilter](sp-articlefilter-transact-sql.md) and `sp_articleview`.

If the article already has a view that defines the published article, `sp_articleview` drops the existing view and creates a new one automatically. If the view was created manually (`type` in [sysarticles](../system-tables/sysarticles-transact-sql.md) is `5`), the existing view isn't dropped.

If you create a custom filter stored procedure and a view that defines the published article manually, don't run `sp_articleview`. Instead, provide these values as the *@filter_name* and *@sync_object* parameters to [sp_addarticle](sp-addarticle-transact-sql.md), along with the appropriate *@type* value.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-articleview-transact-_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_articleview`.

## Related content

- [Define an Article](../replication/publish/define-an-article.md)
- [Define and Modify a Static Row Filter](../replication/publish/define-and-modify-a-static-row-filter.md)
- [sp_addarticle (Transact-SQL)](sp-addarticle-transact-sql.md)
- [sp_articlefilter (Transact-SQL)](sp-articlefilter-transact-sql.md)
- [sp_changearticle (Transact-SQL)](sp-changearticle-transact-sql.md)
- [sp_droparticle (Transact-SQL)](sp-droparticle-transact-sql.md)
- [sp_helparticle (Transact-SQL)](sp-helparticle-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
