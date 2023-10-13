---
title: "sp_droppublication (Transact-SQL)"
description: Drops a publication and its associated Snapshot Agent. This stored procedure runs at the Publisher on the publication database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_droppublication_TSQL"
  - "sp_droppublication"
helpviewer_keywords:
  - "sp_droppublication"
dev_langs:
  - "TSQL"
---
# sp_droppublication (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Drops a publication and its associated Snapshot Agent. All subscriptions must be dropped before dropping a publication. The articles in the publication are dropped automatically. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_droppublication
    [ @publication = ] N'publication'
    [ , [ @ignore_distributor = ] ignore_distributor ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @from_backup = ] from_backup ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication to be dropped. *@publication* is **sysname**, with no default. If `all` is specified, all publications are dropped from the publication database, except for publications with subscriptions.

#### [ @ignore_distributor = ] *ignore_distributor*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @publisher = ] N'*publisher*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @from_backup = ] *from_backup*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_droppublication` is used in snapshot replication and transactional replication.

`sp_droppublication` recursively drops all articles associated with a publication and then drops the publication itself. A publication can't be removed if it's one or more subscriptions to it. For information about how to remove subscriptions, see [Delete a Push Subscription](../replication/delete-a-push-subscription.md) and [Delete a Pull Subscription](../replication/delete-a-pull-subscription.md).

Executing `sp_droppublication` to drop a publication doesn't remove published objects from the publication database or the corresponding objects from the subscription database. Use `DROP <object>` to remove these objects manually if necessary.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_droppublication`.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-droppublication-trans_1.sql":::

## Related content

- [Delete a Publication](../replication/publish/delete-a-publication.md)
- [sp_addpublication (Transact-SQL)](sp-addpublication-transact-sql.md)
- [sp_changepublication (Transact-SQL)](sp-changepublication-transact-sql.md)
- [sp_helppublication (Transact-SQL)](sp-helppublication-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
