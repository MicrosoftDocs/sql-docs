---
title: "sp_deletetracertokenhistory (Transact-SQL)"
description: Removes tracer token records from the MStracer_tokens and MStracer_history system tables.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_deletetracertokenhistory"
  - "sp_deletetracertokenhistory_TSQL"
helpviewer_keywords:
  - "sp_deletetracertokenhistory"
dev_langs:
  - "TSQL"
---
# sp_deletetracertokenhistory (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Removes tracer token records from the [MStracer_tokens (Transact-SQL)](../system-tables/mstracer-tokens-transact-sql.md) and [MStracer_history (Transact-SQL)](../system-tables/mstracer-history-transact-sql.md) system tables. This stored procedure is executed at the Publisher on the publication database or at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_deletetracertokenhistory
    [ @publication = ] N'publication'
    [ , [ @tracer_id = ] tracer_id ]
    [ , [ @cutoff_date = ] cutoff_date ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication in which the tracer token was inserted. *@publication* is **sysname**, with no default. This parameter is required.

#### [ @tracer_id = ] *tracer_id*

The ID of the tracer token to delete. *@tracer_id* is **int**, with a default of `NULL`. If `NULL`, all tracer tokens belonging to the publication are deleted.

#### [ @cutoff_date = ] *cutoff_date*

Tracer tokens inserted into the publication before this date are deleted. *@cutoff_date* is **datetime**, with a default of `NULL`.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`.

> [!NOTE]  
> This parameter should only be specified for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers or when executing the stored procedure from distributor.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with a default of `NULL`. This parameter is ignored if the stored procedure is executed at the Publisher.

> [!NOTE]  
> This parameter should be specified when executing the stored procedure from distributor.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_deletetracertokenhistory` is used in transactional replication.

An error occurs if you specify both parameters *@tracer_id* and *@cutoff_date*.

If you don't execute `sp_deletetracertokenhistory` to delete tracer token metadata, the information is deleted when the regularly scheduled history cleanup occurs.

Tracer token IDs can be determined by executing [sp_helptracertokens (Transact-SQL)](sp-helptracertokens-transact-sql.md) or by querying the [MStracer_tokens (Transact-SQL)](../system-tables/mstracer-tokens-transact-sql.md) system table.

## Permissions

Only the following personnel have the authority to execute `sp_deletetracertokenhistory`:

- Members of the **replmonitor** roles, in the distribution database.
- Members of the **sysadmin** fixed server role.
- Members of the **db_owner** fixed database role, in the publication database.
- The **db_owner** of the fixed database.

## Related content

- [Measure Latency and Validate Connections for Transactional Replication](../replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)
- [sp_helptracertokenhistory (Transact-SQL)](sp-helptracertokenhistory-transact-sql.md)
