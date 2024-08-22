---
title: "sp_MSchange_logreader_agent_properties (T-SQL)"
description: Describes the sp_MSchange_logreader_agent_properties stored procedure used to change the properties of the Log Reader Agent for a SQL Server Replication topology.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_MSchange_logreader_agent_properties_TSQL"
  - "sp_MSchange_logreader_agent_properties"
helpviewer_keywords:
  - "sp_MSchange_logreader_agent_properties"
dev_langs:
  - "TSQL"
---
# sp_MSchange_logreader_agent_properties (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the properties of a Log Reader Agent job that runs at a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] or later version Distributor. This stored procedure is used to change properties when the Publisher runs on an instance of [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)]. This stored procedure is executed at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_MSchange_logreader_agent_properties
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publisher_security_mode = ] publisher_security_mode
    , [ @publisher_login = ] N'publisher_login'
    , [ @publisher_password = ] N'publisher_password'
    , [ @job_login = ] N'job_login'
    , [ @job_password = ] N'job_password'
    , [ @publisher_type = ] N'publisher_type'
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with no default.

#### [ @publisher_security_mode = ] *publisher_security_mode*

The security mode used by the agent when connecting to the Publisher. *@publisher_security_mode* is **int**, with no default.

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication
- `1` specifies Windows authentication

#### [ @publisher_login = ] N'*publisher_login*'

The login used when connecting to the Publisher. *@publisher_login* is **sysname**, with no default. *@publisher_login* must be specified when *@publisher_security_mode* is `0`. If *@publisher_login* is `NULL` and *@publisher_security_mode* is `1`, then the Windows account specified in *@job_login* is used when connecting to the Publisher.

#### [ @publisher_password = ] N'*publisher_password*'

The password used when connecting to the Publisher. *@publisher_password* is **nvarchar(524)**, with no default.

#### [ @job_login = ] N'*job_login*'

The login for the Windows account under which the agent runs. *@job_login* is **nvarchar(257)**, with no default. *This property can't be changed for a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.*

#### [ @job_password = ] N'*job_password*'

The password for the Windows account under which the agent runs. *@job_password* is **sysname**, with no default.

#### [ @publisher_type = ] N'*publisher_type*'

Specifies the Publisher type when the Publisher isn't running in an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. *@publisher_type* is **sysname**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `MSSQLSERVER` | Specifies a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. |
| `ORACLE` | Specifies a standard Oracle Publisher. |
| `ORACLE GATEWAY` | Specifies an Oracle Gateway Publisher. |

For more information about the differences between an Oracle Publisher and an Oracle Gateway Publisher, see [Oracle Publishing Overview](../replication/non-sql/oracle-publishing-overview.md).

## Remarks

`sp_MSchange_logreader_agent_properties` is used in transactional replication.

You must specify all parameters when executing `sp_MSchange_logreader_agent_properties`. Execute [sp_helplogreader_agent](sp-helplogreader-agent-transact-sql.md) to return the current properties of the Log Reader Agent job.

After changing an agent login or password, you must stop and restart the agent before the change takes effect.

You can use [sp_changelogreader_agent](sp-changelogreader-agent-transact-sql.md) on the Publisher to change properties of the Log Reader Agent.

## Permissions

Only members of the **sysadmin** fixed server role at the Distributor can execute `sp_MSchange_logreader_agent_properties`.

## Related content

- [sp_addlogreader_agent (Transact-SQL)](sp-addlogreader-agent-transact-sql.md)
