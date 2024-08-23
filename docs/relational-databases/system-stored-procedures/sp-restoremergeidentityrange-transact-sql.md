---
title: "sp_restoremergeidentityrange (Transact-SQL)"
description: sp_restoremergeidentityrange ensures that automatic identity range management functions properly after a Publisher is restored from a backup.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_restoremergeidentityrange_TSQL"
  - "sp_restoremergeidentityrange"
helpviewer_keywords:
  - "sp_restoremergeidentityrange"
dev_langs:
  - "TSQL"
---
# sp_restoremergeidentityrange (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure is used to update identity range assignments. It ensures that automatic identity range management functions properly after a Publisher is restored from a backup. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_restoremergeidentityrange
    [ [ @publication = ] N'publication' ]
    [ , [ @article = ] N'article' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `all`. When specified, only identity ranges for that publication are restored.

#### [ @article = ] N'*article*'

The name of the article. *@article* is **sysname**, with a default of `all`. When specified, only identity ranges for that article are restored.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_restoremergeidentityrange` is used with merge replication.

`sp_restoremergeidentityrange` gets maximum identity range allocation information from the Distributor, and updates values in the `max_used` column of [MSmerge_identity_range_allocations](../system-tables/msmerge-identity-range-allocations-transact-sql.md) for the articles that use automatic identity range management.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_restoremergeidentityrange`.

## Related content

- [sp_addmergearticle (Transact-SQL)](sp-addmergearticle-transact-sql.md)
- [sp_changemergearticle (Transact-SQL)](sp-changemergearticle-transact-sql.md)
- [Replicate Identity Columns](../replication/publish/replicate-identity-columns.md)
