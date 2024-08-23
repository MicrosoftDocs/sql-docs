---
title: "sp_refreshsubscriptions (Transact-SQL)"
description: sp_refreshsubscriptions adds subscriptions to new articles for all the existing Subscribers to an immediate-updating publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_refreshsubscriptions"
  - "sp_refreshsubscriptions_TSQL"
helpviewer_keywords:
  - "sp_refreshsubscriptions"
dev_langs:
  - "TSQL"
---
# sp_refreshsubscriptions (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Add subscriptions to new articles for all the existing Subscribers to an immediate-updating publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_refreshsubscriptions
    [ @publication = ] N'publication'
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

Specifies the publication for which to refresh subscriptions. *@publication* is **sysname**, with no default.

#### [ @publisher = ] N'*publisher*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_refreshsubscriptions` is used in snapshot, transactional, and merge replication.

`sp_refreshsubscriptions` is called by `sp_addarticle` for an immediate-updating publication.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_refreshsubscriptions`.

## Related content

- [sp_addarticle (Transact-SQL)](sp-addarticle-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
