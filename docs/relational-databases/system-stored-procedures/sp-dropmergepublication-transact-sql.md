---
title: "sp_dropmergepublication (Transact-SQL)"
description: "Drops a merge publication and its associated Snapshot Agent."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropmergepublication"
  - "sp_dropmergepublication_TSQL"
helpviewer_keywords:
  - "sp_dropmergepublication"
dev_langs:
  - "TSQL"
---
# sp_dropmergepublication (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops a merge publication and its associated Snapshot Agent. All subscriptions must be dropped before dropping a merge publication. The articles in the publication are dropped automatically. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropmergepublication
    [ @publication = ] N'publication'
    [ , [ @ignore_distributor = ] ignore_distributor ]
    [ , [ @reserved = ] reserved ]
    [ , [ @ignore_merge_metadata = ] ignore_merge_metadata ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication to drop. *@publication* is **sysname**, with no default. If `all`, all existing merge publications are removed as well as the Snapshot Agent job associated with them. If you specify a particular value for *@publication*, only that publication and its associated Snapshot Agent job are dropped.

#### [ @ignore_distributor = ] *ignore_distributor*

Used to drop a publication without doing cleanup tasks at the Distributor. *@ignore_distributor* is **bit**, with a default of `0`. This parameter is also used when reinstalling the Distributor.

#### [ @reserved = ] *reserved*

Reserved for future use. *@reserved* is **bit**, with a default of `0`.

#### [ @ignore_merge_metadata = ] *ignore_merge_metadata*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropmergepublication` is used in merge replication.

`sp_dropmergepublication` recursively drops all articles that are associated with a publication and then drops the publication itself. A publication can't be removed if it's one or more subscriptions to it. For information about how to remove subscriptions, see [Delete a Push Subscription](../replication/delete-a-push-subscription.md) and [Delete a Pull Subscription](../replication/delete-a-pull-subscription.md).

Executing `sp_dropmergepublication` to drop a publication doesn't remove published objects from the publication database or the corresponding objects from the subscription database. Use `DROP <object>` to remove these objects manually if necessary.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-dropmergepublication-_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_dropmergepublication`.

## Related content

- [Delete a Publication](../replication/publish/delete-a-publication.md)
- [sp_addmergepublication (Transact-SQL)](sp-addmergepublication-transact-sql.md)
- [sp_changemergepublication (Transact-SQL)](sp-changemergepublication-transact-sql.md)
- [sp_helpmergepublication (Transact-SQL)](sp-helpmergepublication-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
