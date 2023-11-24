---
title: "sp_reinitmergesubscription (Transact-SQL)"
description: Marks a merge subscription for reinitialization the next time the Merge Agent runs.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_reinitmergesubscription_TSQL"
  - "sp_reinitmergesubscription"
helpviewer_keywords:
  - "sp_reinitmergesubscription"
dev_langs:
  - "TSQL"
---
# sp_reinitmergesubscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Marks a merge subscription for reinitialization the next time the Merge Agent runs. This stored procedure is executed at the Publisher in the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_reinitmergesubscription
    [ [ @publication = ] N'publication' ]
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    [ , [ @upload_first = ] N'upload_first' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `all`.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `all`.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the Subscriber database. *@subscriber_db* is **sysname**, with a default of `all`.

#### [ @upload_first = ] N'*upload_first*'

Specifies whether changes at the Subscriber are uploaded before the subscription is reinitialized. *@upload_first* is **nvarchar(5)**, with a default of `false`.

- If `true`, changes are uploaded before the subscription is reinitialized.
- If `false`, changes aren't uploaded.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_reinitmergesubscription` is used in merge replication.

`sp_reinitmergesubscription` can be called from the Publisher to reinitialize merge subscriptions. You should rerun the Snapshot Agent as well.

If you add, drop, or change a parameterized filter, pending changes at the Subscriber can't be uploaded to the Publisher during reinitialization. If you want to upload pending changes, synchronize all subscriptions before changing the filter.

## Examples

### A. Reinitialize the push subscription and lose pending changes

:::code language="sql" source="../replication/codesnippet/tsql/sp-reinitmergesubscripti_1.sql":::

### B. Reinitialize the push subscription and upload pending changes

:::code language="sql" source="../replication/codesnippet/tsql/sp-reinitmergesubscripti_2.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_reinitmergesubscription`.

## Related content

- [Reinitialize Subscriptions](../replication/reinitialize-subscriptions.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
