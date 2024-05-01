---
title: "sp_reinitmergepullsubscription (Transact-SQL)"
description: Marks a merge pull subscription for reinitialization the next time the Merge Agent runs.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_reinitmergepullsubscription"
  - "sp_reinitmergepullsubscription_TSQL"
helpviewer_keywords:
  - "sp_reinitmergepullsubscription"
dev_langs:
  - "TSQL"
---
# sp_reinitmergepullsubscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Marks a merge pull subscription for reinitialization the next time the Merge Agent runs. This stored procedure is executed at the Subscriber in the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_reinitmergepullsubscription
    [ [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @publication = ] N'publication' ]
    [ , [ @upload_first = ] N'upload_first' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `all`.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `all`.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `all`.

#### [ @upload_first = ] N'*upload_first*'

Specifies whether changes at the Subscriber are uploaded before the subscription is reinitialized. *@upload_first* is **nvarchar(5)**, with a default of `false`.

- If `true`, changes are uploaded before the subscription is reinitialized.
- If `false`, changes aren't uploaded.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_reinitmergepullsubscription` is used in merge replication.

If you add, drop, or change a parameterized filter, pending changes at the Subscriber can't be uploaded to the Publisher during reinitialization. If you want to upload pending changes, synchronize all subscriptions before changing the filter.

## Examples

### A. Reinitialize the pull subscription and lose pending changes

:::code language="sql" source="../replication/codesnippet/tsql/sp-reinitmergepullsubscr_1.sql":::

### B. Reinitialize the pull subscription and upload pending changes

:::code language="sql" source="../replication/codesnippet/tsql/sp-reinitmergepullsubscr_2.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_reinitmergepullsubscription`.

## Related content

- [Reinitialize a Subscription](../replication/reinitialize-a-subscription.md)
- [Reinitialize Subscriptions](../replication/reinitialize-subscriptions.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
