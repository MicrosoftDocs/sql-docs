---
title: "sp_changeqreader_agent (Transact-SQL)"
description: sp_changeqreader_agent changes security properties of a Queue Reader agent.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changeqreader_agent_TSQL"
  - "sp_changeqreader_agent"
helpviewer_keywords:
  - "sp_changeqreader_agent"
dev_langs:
  - "TSQL"
---
# sp_changeqreader_agent (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes security properties of a Queue Reader agent. This stored procedure is executed at the Distributor on the distribution database or at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changeqreader_agent
    [ [ @job_login = ] N'job_login' ]
    [ , [ @job_password = ] N'job_password' ]
    [ , [ @frompublisher = ] frompublisher ]
[ ; ]
```

## Arguments

#### [ @job_login = ] N'*job_login*'

The login for the Windows account under which the agent runs. *@job_login* is **nvarchar(257)**, with a default of `NULL`.

#### [ @job_password = ] N'*job_password*'

The password for the Windows account under which the agent runs. *@job_password* is **sysname**, with a default of `NULL`.

#### [ @frompublisher = ] *frompublisher*

Specifies whether the procedure is being executed at the Publisher. *@frompublisher* is **bit**, with a default of `0`. A value of `1` means that the procedure is being executed from the Publisher on the publication database.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changeqreader_agent` is used in transactional replication.

`sp_changeqreader_agent` is used to change the Windows account under which a Queue Reader agent runs. You can change the password of an existing Windows login or supply a new Windows login and password.

After changing an agent login or password, you must stop and restart the agent before the change takes effect.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_changeqreader_agent`.

## Related content

- [View and modify replication security settings](../replication/security/view-and-modify-replication-security-settings.md)
- [sp_addqreader_agent (Transact-SQL)](sp-addqreader-agent-transact-sql.md)
