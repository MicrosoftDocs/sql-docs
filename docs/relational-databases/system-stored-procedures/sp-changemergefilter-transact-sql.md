---
title: "sp_changemergefilter (Transact-SQL)"
description: "Changes some merge filter properties. This stored procedure is executed at the Publisher on the publication database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changemergefilter_TSQL"
  - "sp_changemergefilter"
helpviewer_keywords:
  - "sp_changemergefilter"
dev_langs:
  - "TSQL"
---
# sp_changemergefilter (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes some merge filter properties. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changemergefilter
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    , [ @filtername = ] N'filtername'
    , [ @property = ] N'property'
    , [ @value = ] N'value'
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

The current name of the filter. *@filtername* is **sysname**, with no default.

#### [ @property = ] N'*property*'

The name of the property to change. *@property* is **sysname**, with no default.

#### [ @value = ] N'*value*'

The new value for the specified property. *@value* is **nvarchar(1000)**, with no default.

The following table describes the properties of articles, and the values for those properties.

| Property | Value | Description |
| --- | --- | --- |
| `filter_type` | `1` | Join filter.<br /><br />This option is required to support [!INCLUDE [ssEW](../../includes/ssew-md.md)] Subscribers. |
| | `2` | Logical record relationship. |
| | `3` | Join filter is also a logical record relationship. |
| `filtername` | | Name of the filter. |
| `join_articlename` | | Name of the join article. |
| `join_filterclause` | | Filter clause. |
| `join_unique_key` | `true` | Join is on a unique key |
| | `false` | Join isn't on a unique key. |

#### [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*

Acknowledges that the action taken by this stored procedure might invalidate an existing snapshot. *@force_invalidate_snapshot* is **bit**, with a default of `0`.

- `0` specifies that changes to the merge article don't cause the snapshot to be invalid. If the stored procedure detects that the change does require a new snapshot, an error occurs and no changes are made.

- `1` means that changes to the merge article might cause the snapshot to be invalid, and if there are existing subscriptions that would require a new snapshot, gives permission for the existing snapshot to be marked as obsolete and a new snapshot generated.

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Acknowledges that the action taken by this stored procedure might require existing subscriptions to be reinitialized. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that changes to the merge article don't cause the subscription to be reinitialized. If the stored procedure detects that the change would require existing subscriptions to be reinitialized, an error occurs and no changes are made.

- `1` means that changes to the merge article reinitialize existing subscriptions, and gives permission for the subscription reinitialization to occur.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changemergefilter` is used in merge replication.

Changing the filter on a merge article requires the snapshot, if one exists, to be recreated. This is performed by setting the *@force_invalidate_snapshot* to `1`. If there are subscriptions to this article, the subscriptions need to be reinitialized, which is done by setting the *@force_reinit_subscription* to `1`.

To use logical records, the publication and articles must meet several requirements. For more information, see [Group Changes to Related Rows with Logical Records](../replication/merge/group-changes-to-related-rows-with-logical-records.md).

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_changemergefilter`.

## Related content

- [Change Publication and Article Properties](../replication/publish/change-publication-and-article-properties.md)
- [sp_addmergefilter (Transact-SQL)](sp-addmergefilter-transact-sql.md)
- [sp_dropmergefilter (Transact-SQL)](sp-dropmergefilter-transact-sql.md)
- [sp_helpmergefilter (Transact-SQL)](sp-helpmergefilter-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
