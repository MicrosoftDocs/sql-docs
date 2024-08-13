---
title: "sp_changelogreader_agent (Transact-SQL)"
description: sp_changelogreader_agent changes security properties of a Log Reader agent.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changelogreader_agent"
  - "sp_changelogreader_agent_TSQL"
helpviewer_keywords:
  - "sp_changelogreader_agent"
dev_langs:
  - "TSQL"
---
# sp_changelogreader_agent (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes security properties of a Log Reader agent. This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *@job_login* and *@job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

## Syntax

```syntaxsql
sp_changelogreader_agent
    [ [ @job_login = ] N'job_login' ]
    [ , [ @job_password = ] N'job_password' ]
    [ , [ @publisher_security_mode = ] publisher_security_mode ]
    [ , [ @publisher_login = ] N'publisher_login' ]
    [ , [ @publisher_password = ] N'publisher_password' ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @job_login = ] N'*job_login*'

The login for the account under which the agent runs. *@job_login* is **nvarchar(257)**, with a default of `NULL`. On [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], use a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] account.

> [!NOTE]  
> This can't be changed for a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher.

#### [ @job_password = ] N'*job_password*'

The password for the account under which the agent runs. *@job_password* is **sysname**, with a default of `NULL`.

> [!IMPORTANT]  
> When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @publisher_security_mode = ] *publisher_security_mode*

The security mode used by the agent when connecting to the Publisher. *@publisher_security_mode* is **smallint**, with a default of `NULL`. The following values define the security mode:

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication.
- `1` specifies Windows authentication.
- `2` specifies Microsoft Entra password authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `3` specifies Microsoft Entra integrated authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `4` specifies Microsoft Entra token authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

#### [ @publisher_login = ] N'*publisher_login*'

The login used when connecting to the Publisher. *@publisher_login* is **sysname**, with a default of `NULL`. *@publisher_login* must be specified when *@publisher_security_mode* is `0`. If *@publisher_login* is `NULL` and *@publisher_security_mode* is `1`, then the Windows account specified in *@job_login* is used when connecting to the Publisher.

#### [ @publisher_password = ] N'*publisher_password*'

The password used when connecting to the Publisher. *@publisher_password* is **sysname**, with a default of `NULL`.

> [!IMPORTANT]  
> Don't use a blank password. Use a strong password. When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`. This parameter is only supported for non-[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Publishers.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changelogreader_agent` is used in transactional replication.

`sp_changelogreader_agent` is used to change the Windows account under which a Log Reader agent runs. You can change the password of an existing Windows login or supply a new Windows login and password.

After changing an agent login or password, you must stop and restart the agent before the change takes effect.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_changelogreader_agent`.

## Related content

- [View and modify replication security settings](../replication/security/view-and-modify-replication-security-settings.md)
- [sp_helplogreader_agent (Transact-SQL)](sp-helplogreader-agent-transact-sql.md)
- [sp_addlogreader_agent (Transact-SQL)](sp-addlogreader-agent-transact-sql.md)
