---
title: "sp_posttracertoken (Transact-SQL)"
description: sp_posttracertoken posts a tracer token into the transaction log at the Publisher.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "posttracerttoken"
  - "posttracerttoken_TSQL"
  - "sp_posttracertoken"
  - "sp_posttracertoken_TSQL"
helpviewer_keywords:
  - "sp_posttracertoken"
dev_langs:
  - "TSQL"
---
# sp_posttracertoken (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This procedure posts a tracer token into the transaction log at the Publisher and begins the process of tracking latency statistics.

Information is recorded:

- when the tracer token is written to the transaction log;
- when the Log Reader Agent picks it up; and
- when the Distribution Agent applies it.

This stored procedure is executed at the Publisher on the publication database. For more information, see [Measure Latency and Validate Connections for Transactional Replication](../replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_posttracertoken
    [ @publication = ] N'publication'
    [ , [ @tracer_token_id = ] tracer_token_id OUTPUT ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication for which latency is being measured. *@publication* is **sysname**, with no default.

#### [ @tracer_token_id = ] *tracer_token_id* OUTPUT

The ID of the tracer token inserted. *@tracer_token_id* is an OUTPUT parameter of type **int**. This value can be used to execute [sp_helptracertokenhistory](sp-helptracertokenhistory-transact-sql.md) or [sp_deletetracertokenhistory](sp-deletetracertokenhistory-transact-sql.md) without first executing [sp_helptracertokens](sp-helptracertokens-transact-sql.md).

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`. This parameter shouldn't be specified for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_posttracertoken` is used in transactional replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-posttracertoken-trans_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_posttracertoken`.

## Related content

- [Measure Latency and Validate Connections for Transactional Replication](../replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)
