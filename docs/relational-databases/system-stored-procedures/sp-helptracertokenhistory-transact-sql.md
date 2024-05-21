---
title: "sp_helptracertokenhistory (Transact-SQL)"
description: sp_helptracertokenhistory returns detailed latency information for specified tracer tokens, with one row being returned for each Subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helptracertokenhistory_TSQL"
  - "sp_helptracertokenhistory"
helpviewer_keywords:
  - "sp_helptracertokenhistory"
dev_langs:
  - "TSQL"
---
# sp_helptracertokenhistory (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns detailed latency information for specified tracer tokens, with one row being returned for each Subscriber. This stored procedure is executed at the Publisher on the publication database or at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helptracertokenhistory
    [ @publication = ] N'publication'
    , [ @tracer_id = ] tracer_id
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication in which the tracer token was inserted. *@publication* is **sysname**, with no default.

#### [ @tracer_id = ] *tracer_id*

The ID of the tracer token in the [MStracer_tokens](../system-tables/mstracer-tokens-transact-sql.md) table, for which history information is returned. *@tracer_id* is **int**, with no default.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* should only be specified for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with a default of `NULL`. This parameter is ignored if the stored procedure is executed at the Publisher.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `distributor_latency` | **bigint** | Number of seconds between the tracer token record being committed at the Publisher and the record being committed at the Distributor. |
| `subscriber` | **sysname** | Name of the Subscriber that received the tracer token. |
| `subscriber_db` | **sysname** | Name of the subscription database into which the tracer token record was inserted. |
| `subscriber_latency` | **bigint** | Number of seconds between the tracer token record being committed at the Distributor and the record being committed at the Subscriber. |
| `overall_latency` | **bigint** | Number of seconds between the tracer token record being committed at the Publisher and token record being committed at the Subscriber. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helptracertokenhistory` is used in transactional replication.

Execute [sp_helptracertokens](sp-helptracertokens-transact-sql.md) to obtain a list of tracer tokens for the publication.

A value of `NULL` in the result set means that latency statistics can't be calculated. This is because the tracer token hasn't been received at the Distributor or one of the Subscribers.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-helptracertokenhistor_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role in the publication database, or **db_owner** fixed database or **replmonitor** roles in the distribution database can execute `sp_helptracertokenhistory`.

## Related content

- [Measure Latency and Validate Connections for Transactional Replication](../replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)
- [sp_deletetracertokenhistory (Transact-SQL)](sp-deletetracertokenhistory-transact-sql.md)
