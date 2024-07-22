---
title: "sp_helpqreader_agent (Transact-SQL)"
description: sp_helpqreader_agent returns properties of the Queue Reader agent.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpqreader_agent_TSQL"
  - "sp_helpqreader_agent"
helpviewer_keywords:
  - "sp_helpqreader_agent"
dev_langs:
  - "TSQL"
---
# sp_helpqreader_agent (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns properties of the Queue Reader agent. This stored procedure is executed at the Distributor on the distribution database or at the Publisher on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpqreader_agent [ [ @frompublisher = ] frompublisher ]
[ ; ]
```

## Arguments

#### [ @frompublisher = ] *frompublisher*

Specifies whether the stored procedure is called at the Publisher or at the Distributor. *@frompublisher* is **bit**, with a default of `0`.

- `1` means that the stored procedure is called from the Publisher.
- `0` means that the stored procedure is called from the Distributor.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `id` | **int** | ID of the agent. |
| `name` | **nvarchar(100)** | Name of the agent. |
| `job_id` | **uniqueidentifier** | Unique ID of the agent job. |
| `job_login` | **nvarchar(512)** | Is the Windows account under which the Distribution agent runs, which is returned in the format `<domain>\<username>`. |
| `job_password` | **sysname** | For security reasons, a value of `**********` is always returned. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpqreader_agent` is used in transactional replication.

## Permissions

When the value of *frompublisher* is `1`, only members of the **sysadmin** fixed server role at the Publisher or members of the **db_owner** fixed database role on the publication database can execute `sp_helpqreader_agent`. Otherwise, only members of the **sysadmin** fixed server role at the Distributor or members of the **db_owner** fixed database role on the distribution database can execute `sp_helpqreader_agent`.

## Related content

- [Enable Updating Subscriptions for Transactional Publications](../replication/publish/enable-updating-subscriptions-for-transactional-publications.md)
