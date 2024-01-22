---
title: "sp_adddistributor (Transact-SQL)"
description: "Adds an entry in the sys.servers and marks the server entry as a Distributor, storing property information."
author: mashamsft
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_adddistributor"
  - "sp_adddistributor_TSQL"
helpviewer_keywords:
  - "sp_adddistributor"
dev_langs:
  - "TSQL"
---
# sp_adddistributor (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates an entry in the [sys.servers](../system-catalog-views/sys-servers-transact-sql.md) table (if there isn't one), marks the server entry as a Distributor, and stores property information. This stored procedure is executed at the Distributor on the `master` database to register and mark the server as a distributor. In the case of a remote distributor, it's also executed at the Publisher from the `master` database to register the remote distributor.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_adddistributor
    [ @distributor = ] N'distributor'
    [ , [ @heartbeat_interval = ] heartbeat_interval ]
    [ , [ @password = ] N'password' ]
    [ , [ @from_scripting = ] from_scripting ]
[ ; ]
```

## Arguments

#### [ @distributor = ] N'*distributor*'

The distribution server name. *@distributor* is **sysname**, with no default. This parameter is only used if setting up a remote Distributor. It adds entries for the Distributor properties in the `msdb..MSdistributor` table.

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15"
[!INCLUDE [custom-port](includes/custom-port.md)]

::: moniker-end

#### [ @heartbeat_interval = ] *heartbeat_interval*

The maximum number of minutes that an agent can go without logging a progress message. *@heartbeat_interval* is **int**, with a default of `10` minutes. A [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent job is created that runs on this interval to check the status of the replication agents that are running.

#### [ @password = ] N'*password*'

The password of the **distributor_admin** login. *@password* is **sysname**, with a default of `NULL`. If the password is `NULL` or an empty string, *@password* is reset to a random value. The password must be configured when the first remote distributor is added. **distributor_admin** login and *@password* are stored for linked server entry used for a *distributor* RPC connection, including local connections. If *distributor* is local, the password for **distributor_admin** is set to a new value. For Publishers with a remote Distributor, the same value for *@password* must be specified when executing `sp_adddistributor` at both the Publisher and Distributor. [sp_changedistributor_password](sp-changedistributor-password-transact-sql.md) can be used to change the Distributor password.

> [!IMPORTANT]  
> When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @from_scripting = ] *from_scripting*

*@from_scripting* is **bit**, with a default of `0`. [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_adddistributor` is used in snapshot replication, transactional replication, and merge replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-adddistributor-transa_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_adddistributor`.

## Related content

- [Configure Publishing and Distribution](../replication/configure-publishing-and-distribution.md)
- [sp_changedistributor_property (Transact-SQL)](sp-changedistributor-property-transact-sql.md)
- [sp_dropdistributor (Transact-SQL)](sp-dropdistributor-transact-sql.md)
- [sp_helpdistributor (Transact-SQL)](sp-helpdistributor-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Configure Distribution](../replication/configure-distribution.md)
