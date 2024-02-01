---
title: "sp_validatemergepublication (Transact-SQL)"
description: Performs a publication-wide validation for which all subscriptions (push, pull, and anonymous) are validated once.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/24/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_validatemergepublication"
  - "sp_validatemergepublication_TSQL"
helpviewer_keywords:
  - "sp_validatemergepublication"
dev_langs:
  - "TSQL"
---
# sp_validatemergepublication (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Performs a publication-wide validation for which all subscriptions (push, pull, and anonymous) are validated once. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_validatemergepublication
    [ @publication = ] N'publication'
    , [ @level = ] level
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @level = ] *level*

The type of validation to perform. *@level* is **tinyint**, and can be one of the following values.

| Level value | Description |
| --- | --- |
| `1` | Rowcount-only validation. |
| `2` | Rowcount and checksum validation. For [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] Subscribers, this is automatically set to `3`. |
| `3` | This is the recommended value. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_validatemergepublication` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_validatemergepublication`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Validate Replicated Data](../replication/validate-data-at-the-subscriber.md)
- [sp_validatemergesubscription (Transact-SQL)](sp-validatemergesubscription-transact-sql.md)
