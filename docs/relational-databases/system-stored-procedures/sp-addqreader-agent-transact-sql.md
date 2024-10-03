---
title: "sp_addqreader_agent (Transact-SQL)"
description: sp_addqreader_agent adds a Queue Reader agent for a given Distributor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addqreader_agent_TSQL"
  - "sp_addqreader_agent"
helpviewer_keywords:
  - "sp_addqreader_agent"
dev_langs:
  - "TSQL"
---
# sp_addqreader_agent (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds a Queue Reader agent for a given Distributor. This stored procedure is executed at the Distributor on the distribution database or at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addqreader_agent
    [ [ @job_login = ] N'job_login' ]
    [ , [ @job_password = ] N'job_password' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @frompublisher = ] frompublisher ]
[ ; ]
```

## Arguments

#### [ @job_login = ] N'*job_login*'

The login for the Windows account under which the agent runs. *@job_login* is **nvarchar(257)**, with a default of `NULL`. This Windows account is always used for agent connections to the Distributor.

#### [ @job_password = ] N'*job_password*'

The password for the Windows account under which the agent runs. *@job_password* is **sysname**, with no default.

> [!IMPORTANT]  
> Don't store authentication information in script files. For best security, login names and passwords should be supplied at runtime.

#### [ @job_name = ] N'*job_name*'

The name of an existing agent job. *@job_name* is **sysname**, with a default of `NULL`. This parameter is only specified when the agent is created using an existing job instead of a newly created job (the default).

#### [ @frompublisher = ] *frompublisher*

Specifies whether the procedure is being executed at the Publisher. *@frompublisher* is **bit**, with a default of `0`.

A value of `1` means that the procedure is being executed from the Publisher on the publication database.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addqreader_agent` is used in transactional replication.

`sp_addqreader_agent` must be executed at least once at a Distributor that supports queued updating after [sp_adddistributiondb](sp-adddistributiondb-transact-sql.md) but before [sp_addpublication](sp-addpublication-transact-sql.md).

The Queue Reader Agent job is removed when you execute [sp_dropdistributiondb](sp-dropdistributiondb-transact-sql.md).

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_addqreader_agent`.

## Related content

- [Enable Updating Subscriptions for Transactional Publications](../replication/publish/enable-updating-subscriptions-for-transactional-publications.md)
- [Upgrade Replication Scripts (Replication Transact-SQL Programming)](../replication/administration/upgrade-replication-scripts-replication-transact-sql-programming.md)
- [Updatable Subscriptions - For Transactional Replication](../replication/transactional/updatable-subscriptions-for-transactional-replication.md)
- [sp_changeqreader_agent (Transact-SQL)](sp-changeqreader-agent-transact-sql.md)
- [sp_helpqreader_agent (Transact-SQL)](sp-helpqreader-agent-transact-sql.md)
