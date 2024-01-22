---
title: "sp_article_validation (Transact-SQL)"
description: sp_article_validation initiates a data validation request for the specified article.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_article_validation_TSQL"
  - "sp_article_validation"
helpviewer_keywords:
  - "sp_article_validation"
dev_langs:
  - "TSQL"
---
# sp_article_validation (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Initiates a data validation request for the specified article. This stored procedure is executed at the Publisher on the publication database and at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_article_validation
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    [ , [ @rowcount_only = ] rowcount_only ]
    [ , [ @full_or_fast = ] full_or_fast ]
    [ , [ @shutdown_agent = ] shutdown_agent ]
    [ , [ @subscription_level = ] subscription_level ]
    [ , [ @reserved = ] reserved ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication in which the article exists. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article to validate. *@article* is **sysname**, with no default.

#### [ @rowcount_only = ] *rowcount_only*

Specifies if only the rowcount for the table is returned. *@rowcount_only* is **smallint**, with a default of `1`.

- If `0`, perform a rowcount and a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 compatible checksum.
- If `1`, perform a rowcount check only.
- If `2`, perform a rowcount and binary checksum.

#### [ @full_or_fast = ] *full_or_fast*

The method used to calculate the rowcount. *@full_or_fast* is **tinyint**, and can be one of these values:

| Value | Description |
| --- | --- |
| `0` | Performs full count using `COUNT(*)`. |
| `1` | Performs fast count from `sysindexes.rows`. Counting rows in `sysindexes` is faster than counting rows in the actual table. However, `sysindexes` is updated lazily, and the rowcount might not be accurate. |
| `2` (default) | Performs conditional fast counting by first trying the fast method. If fast method shows differences, reverts to full method. If `expected_rowcount` is `NULL` and the stored procedure is being used to get the value, a full `COUNT(*)` is always used. |

#### [ @shutdown_agent = ] *shutdown_agent*

Specifies if the Distribution agent should shut down immediately upon completion of the validation. *@shutdown_agent* is **bit**, with a default of `0`.

- If `0`, the Distribution Agent doesn't shut down.
- If `1`, the Distribution Agent shuts down after the article is validated.

#### [ @subscription_level = ] *subscription_level*

Specifies whether or not the validation is picked up by a set of subscribers. *@subscription_level* is **bit**, with a default of `0`.

- If `0`, validation is applied to all Subscribers.
- If `1`, validation is only applied to a subset of the Subscribers specified by calls to `sp_marksubscriptionvalidation` in the current open transaction.

#### [ @reserved = ] *reserved*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used when requesting validation on a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_article_validation` is used in transactional replication.

`sp_article_validation` causes validation information to be gathered on the specified article and posts a validation request to the transaction log. When the Distribution Agent receives this request, the Distribution Agent compares the validation information in the request to the Subscriber table. The results of the validation are displayed in the Replication Monitor and in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts.

## Permissions

Only users with `SELECT ALL` permissions on the source table for the article being validated can execute `sp_article_validation`.

## Related content

- [Validate Replicated Data](../replication/validate-data-at-the-subscriber.md)
- [sp_marksubscriptionvalidation (Transact-SQL)](sp-marksubscriptionvalidation-transact-sql.md)
- [sp_publication_validation (Transact-SQL)](sp-publication-validation-transact-sql.md)
- [sp_table_validation (Transact-SQL)](sp-table-validation-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
