---
title: sp_dropmergefilter (Transact-SQL)
description: "sp_dropmergefilter drops all the merge filter columns defined on the merge filter that is to be dropped."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropmergefilter_TSQL"
  - "sp_dropmergefilter"
helpviewer_keywords:
  - "sp_dropmergefilter"
dev_langs:
  - "TSQL"
---
# sp_dropmergefilter (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops a merge filter. `sp_dropmergefilter` drops all the merge filter columns defined on the merge filter that is to be dropped. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropmergefilter
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    , [ @filtername = ] N'filtername'
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article. *@article* is **sysname**, with no default.

#### [ @filtername = ] N'*filtername*'

The name of the filter to be dropped. *@filtername* is **sysname**, with no default.

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Enables or disables the ability to have a snapshot invalidated. *@force_invalidate_snapshot* is **bit**, with a default of `0`.

- `0` specifies that changes to the merge article don't cause the snapshot to be invalid.

- `1` means that changes to the merge article might cause the snapshot to be invalid. If that is the case, a value of `1` gives permission for the new snapshot to occur.

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Enables or disables the ability to mark a subscription as not valid. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that changes to the merge article filter don't cause the subscriptions to be invalid.

- `1` means that changes to the merge article filter causes the subscriptions to be invalid.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropmergefilter` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_dropmergefilter`.

## Related content

- [Change Publication and Article Properties](../replication/publish/change-publication-and-article-properties.md)
- [sp_addmergefilter (Transact-SQL)](sp-addmergefilter-transact-sql.md)
- [sp_changemergefilter (Transact-SQL)](sp-changemergefilter-transact-sql.md)
- [sp_helpmergefilter (Transact-SQL)](sp-helpmergefilter-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
