---
title: "sp_dropmergepullsubscription (Transact-SQL)"
description: "Drops a merge pull subscription. This stored procedure is executed at the Subscriber on the subscription database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropmergepullsubscription"
  - "sp_dropmergepullsubscription_TSQL"
helpviewer_keywords:
  - "sp_dropmergepullsubscription"
dev_langs:
  - "TSQL"
---
# sp_dropmergepullsubscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops a merge pull subscription. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropmergepullsubscription
    [ [ @publication = ] N'publication' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @reserved = ] reserved ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `NULL`. This parameter is required. Specify a value of `all` to remove subscriptions to all publications.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`. This parameter is required.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `NULL`. This parameter is required.

#### [ @reserved = ] *reserved*

Reserved for future use. *@reserved* is **bit**, with a default of `0`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropmergepullsubscription` is used in merge replication.

`sp_dropmergepullsubscription` drops the Merge Agent for this merge pull subscription, although the Merge Agent isn't created in `sp_addmergepullsubscription`.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-dropmergepullsubscrip_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or the user that created the merge pull subscription can execute `sp_dropmergepullsubscription`. The **db_owner** fixed database role is only able to execute `sp_dropmergepullsubscription` if the user that created the merge pull subscription belongs to this role.

## Related content

- [Delete a Pull Subscription](../replication/delete-a-pull-subscription.md)
- [sp_addmergepullsubscription (Transact-SQL)](sp-addmergepullsubscription-transact-sql.md)
- [sp_changemergepullsubscription (Transact-SQL)](sp-changemergepullsubscription-transact-sql.md)
- [sp_dropmergesubscription (Transact-SQL)](sp-dropmergesubscription-transact-sql.md)
- [sp_helpmergepullsubscription (Transact-SQL)](sp-helpmergepullsubscription-transact-sql.md)
