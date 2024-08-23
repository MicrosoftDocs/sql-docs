---
title: "sp_helplogreader_agent (Transact-SQL)"
description: Returns properties of the Log Reader Agent job for the publication database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helplogreader_agent"
  - "sp_helplogreader_agent_TSQL"
helpviewer_keywords:
  - "sp_helplogreader_agent"
dev_langs:
  - "TSQL"
---
# sp_helplogreader_agent (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns properties of the Log Reader Agent job for the publication database. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helplogreader_agent [ [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`. This parameter is for non-[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Publishers only. The value of this parameter must be `NULL` for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Publisher.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `id` | **int** | ID of the agent. |
| `name` | **nvarchar(100)** | Name of the agent. |
| `publisher_security_mode` | **smallint** | Security mode used by the agent when connecting to the Publisher, which can be one of the following values:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br />`1` = Windows Authentication |
| `publisher_login` | **sysname** | Login used when connecting to the Publisher. |
| `publisher_password` | **nvarchar(524)** | For security reasons, a value of `**********` is always returned. |
| `job_id` | **uniqueidentifier** | Unique ID of the agent job. |
| `job_login` | **nvarchar(512)** | Is the Windows account under which the Log Reader Agent runs, which is returned in the format `<domain>\<username>`. |
| `job_password` | **sysname** | For security reasons, a value of `**********` is always returned. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helplogreader_agent` is used in transactional replication.

## Permissions

Only members of the **sysadmin** fixed server role at the Publisher or members of the **db_owner** fixed database role on the publication database can execute `sp_helplogreader_agent`.

## Related content

- [View and modify replication security settings](../replication/security/view-and-modify-replication-security-settings.md)
- [sp_addlogreader_agent (Transact-SQL)](sp-addlogreader-agent-transact-sql.md)
- [sp_changelogreader_agent (Transact-SQL)](sp-changelogreader-agent-transact-sql.md)
