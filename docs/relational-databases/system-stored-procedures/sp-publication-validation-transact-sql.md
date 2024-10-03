---
title: "sp_publication_validation (Transact-SQL)"
description: sp_publication_validation initiates an article validation request for each article in the specified publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_publication_validation"
  - "sp_publication_validation_TSQL"
helpviewer_keywords:
  - "sp_publication_validation"
dev_langs:
  - "TSQL"
---
# sp_publication_validation (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Initiates an article validation request for each article in the specified publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_publication_validation
    [ @publication = ] N'publication'
    [ , [ @rowcount_only = ] rowcount_only ]
    [ , [ @full_or_fast = ] full_or_fast ]
    [ , [ @shutdown_agent = ] shutdown_agent ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @rowcount_only = ] *rowcount_only*

Specifies whether to return only the rowcount for the table. *@rowcount_only* is **smallint**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `0` | Perform a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 compatible checksum.<br /><br />**Note:** When an article is horizontally filtered, a rowcount operation is performed instead of a checksum operation. |
| `1` (default) | Perform a rowcount check only. |
| `2` | Perform a rowcount and binary checksum. |

#### [ @full_or_fast = ] *full_or_fast*

The method used to calculate the rowcount. *@full_or_fast* is **tinyint**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `0` | Does full count using `COUNT(*)`. |
| `1` | Does fast count from `sysindexes.rows`. Counting rows in [sys.sysindexes](../system-compatibility-views/sys-sysindexes-transact-sql.md) is faster than counting rows in the actual table. However, because [sys.sysindexes](../system-compatibility-views/sys-sysindexes-transact-sql.md) is lazily updated, the rowcount might not be accurate. |
| `2` (default) | Does conditional fast counting by first trying the fast method. If fast method shows differences, reverts to full method. If `expected_rowcount` is `NULL` and the stored procedure is being used to get the value, a full `COUNT(*)` is always used. |

#### [ @shutdown_agent = ] *shutdown_agent*

Specifies whether the Distribution Agent should shut down immediately upon completion of the validation. *@shutdown_agent* is **bit**, with a default of `0`.

- If `0`, the replication agent doesn't shut down.
- If `1`, the replication agent shuts down after the last article is validated.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used when requesting validation on a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_publication_validation` is used in transactional replication.

`sp_publication_validation` can be called at any time after the articles associated with the publication are activated. The procedure can be run manually (one time) or as part of a regularly scheduled job that validates the data.

If your application has immediate-updating Subscribers, `sp_publication_validation` might detect spurious errors. `sp_publication_validation` first calculates the rowcount or checksum at the Publisher and then at the Subscriber. Because the immediate-updating trigger could propagate an update from the Subscriber to the Publisher after the rowcount or checksum is completed at the Publisher, but before the rowcount or checksum is completed at the Subscriber, the values could change. To ensure that the values at the Subscriber and Publisher don't change while validating a publication, stop the Microsoft Distributed Transaction Coordinator (MS DTC) service at the Publisher during validation.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_publication_validation`.

## Related content

- [Validate Replicated Data](../replication/validate-data-at-the-subscriber.md)
- [sp_article_validation (Transact-SQL)](sp-article-validation-transact-sql.md)
- [sp_table_validation (Transact-SQL)](sp-table-validation-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
