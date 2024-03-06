---
title: "sp_articlecolumn (Transact-SQL)"
description: Specify columns included in an article to vertically filter data in a published table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_articlecolumn"
  - "sp_articlecolumn_TSQL"
helpviewer_keywords:
  - "sp_articlecolumn"
dev_langs:
  - "TSQL"
---
# sp_articlecolumn (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Used to specify columns included in an article to vertically filter data in a published table. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_articlecolumn
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    [ , [ @column = ] N'column' ]
    [ , [ @operation = ] N'operation' ]
    [ , [ @refresh_synctran_procs = ] refresh_synctran_procs ]
    [ , [ @ignore_distributor = ] ignore_distributor ]
    [ , [ @change_active = ] change_active ]
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @internal = ] internal ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication that contains this article. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article. *@article* is **sysname**, with no default.

#### [ @column = ] N'*column*'

The name of the column to be added or dropped. *@column* is **sysname**, with a default of `NULL`. If `NULL`, all columns are published.

#### [ @operation = ] N'*operation*'

Specifies whether to add or drop columns in an article. *@operation* is **nvarchar(5)**, with a default of `add`.

- `add` marks the column for replication.
- `drop` unmarks the column.

#### [ @refresh_synctran_procs = ] *refresh_synctran_procs*

Specifies whether the stored procedures supporting immediate updating subscriptions are regenerated to match the number of columns replicated. *@refresh_synctran_procs* is **bit**, with a default of `1`. If `1`, the stored procedures are regenerated.

#### [ @ignore_distributor = ] *ignore_distributor*

Indicates if this stored procedure executes without connecting to the Distributor. *@ignore_distributor* is **bit**, with a default of `0`.

- If `0`, the database must be enabled for publishing, and the article cache should be refreshed to reflect the new columns replicated by the article.
- If `1`, allows article columns to be dropped for articles that reside in an unpublished database; should be used only in recovery situations.

#### [ @change_active = ] *change_active*

Allows modifying the columns in publications that have subscriptions. *@change_active* is **int**, with a default of `0`.

- If `0`, columns aren't modified.
- If `1`, columns can be added or dropped from active articles that have subscriptions.

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Acknowledges that the action taken by this stored procedure might invalidate an existing snapshot. *@force_invalidate_snapshot* is **bit**, with a default of `0`.

- `0` specifies that changes to the article don't cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.
- `1` specifies that changes to the article might cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Acknowledges that the action taken by this stored procedure might require existing subscriptions to be reinitialized. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that changes to the article don't cause the subscription to be reinitialized. If the stored procedure detects that the change would require subscriptions to be reinitialized, an error occurs and no changes are made.
- `1` specifies that changes to the article cause existing subscriptions to be reinitialized, and gives permission for the subscription reinitialization to occur.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used with a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

#### [ @internal = ] *internal*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_articlecolumn` is used in snapshot replication and transactional replication.

Only an unsubscribed article can be filtered using `sp_articlecolumn`.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-articlecolumn-transac_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_articlecolumn`.

## Related content

- [Define an Article](../replication/publish/define-an-article.md)
- [Define and Modify a Column Filter](../replication/publish/define-and-modify-a-column-filter.md)
- [Filter Published Data](../replication/publish/filter-published-data.md)
- [sp_addarticle (Transact-SQL)](sp-addarticle-transact-sql.md)
- [sp_articleview (Transact-SQL)](sp-articleview-transact-sql.md)
- [sp_changearticle (Transact-SQL)](sp-changearticle-transact-sql.md)
- [sp_droparticle (Transact-SQL)](sp-droparticle-transact-sql.md)
- [sp_helparticle (Transact-SQL)](sp-helparticle-transact-sql.md)
- [sp_helparticlecolumns (Transact-SQL)](sp-helparticlecolumns-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
