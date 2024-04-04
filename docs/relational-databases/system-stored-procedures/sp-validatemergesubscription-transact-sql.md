---
title: "sp_validatemergesubscription (Transact-SQL)"
description: "sp_validatemergesubscription (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/24/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_validatemergesubscription"
  - "sp_validatemergesubscription_TSQL"
helpviewer_keywords:
  - "sp_validatemergesubscription"
dev_langs:
  - "TSQL"
---
# sp_validatemergesubscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Performs a validation for the specified subscription. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_validatemergesubscription
    [ @publication = ] N'publication'
    , [ @subscriber = ] N'subscriber'
    , [ @subscriber_db = ] N'subscriber_db'
    , [ @level = ] level
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with no default.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with no default.

#### [ @level = ] *level*

The type of validation to perform. *@level* is **tinyint**, and can be one of these values.

| Level value | Description |
| --- | --- |
| `1` | Rowcount-only validation. |
| `2` | Rowcount and checksum validation. |
| `3` | Rowcount and binary checksum validation. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_validatemergesubscription` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_validatemergesubscription`.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
- [Validate Replicated Data](../replication/validate-data-at-the-subscriber.md)
- [sp_validatemergepublication (Transact-SQL)](sp-validatemergepublication-transact-sql.md)
