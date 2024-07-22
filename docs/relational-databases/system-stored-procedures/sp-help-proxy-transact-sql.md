---
title: "sp_help_proxy (Transact-SQL)"
description: sp_help_proxy lists information for one or more proxies.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_proxy"
  - "sp_help_proxy_TSQL"
helpviewer_keywords:
  - "sp_help_proxy"
dev_langs:
  - "TSQL"
---
# sp_help_proxy (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists information for one or more proxies.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_proxy
    [ [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
    [ , [ @subsystem_name = ] N'subsystem_name' ]
    [ , [ @name = ] N'name' ]
[ ; ]
```

## Arguments

#### [ @proxy_id = ] *proxy_id*

The proxy identification number of the proxy to list information for. *@proxy_id* is **int**, with a default of `NULL`. Either the *@proxy_id* or the *@proxy_name* can be specified.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy to list information for. *@proxy_name* is **sysname**, with a default of `NULL`. Either the *@proxy_id* or the *@proxy_name* can be specified.

#### [ @subsystem_name = ] N'*subsystem_name*'

The name of the subsystem to list proxies for. *@subsystem_name* is **sysname**, with a default of `NULL`. When *@subsystem_name* is specified, *@name* must also be specified.

The following table lists the values for each subsystem.

| Value | Description |
| --- | --- |
| `ActiveScripting` | ActiveX script |
| `CmdExec` | Operating System (`CmdExec`) |
| `Snapshot` | Replication Snapshot Agent |
| `LogReader` | Replication Log Reader Agent |
| `Distribution` | Replication Distribution Agent |
| `Merge` | Replication Merge Agent |
| `QueueReader` | Replication Queue Reader Agent |
| `ANALYSISQUERY` | Analysis Services command |
| `ANALYSISCOMMAND` | Analysis Services query |
| `Dts` | SSIS package execution |
| `PowerShell` | PowerShell script |

#### [ @name = ] N'*name*'

The name of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login to list proxies for. *@name* is **nvarchar(256)**, with a default of `NULL`. When *@name* is specified, *@subsystem_name* must also be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `proxy_id` | **int** | Proxy identification number. |
| `name` | **sysname** | The name of the proxy. |
| `credential_identity` | **sysname** | The Microsoft Windows domain name and user name for the credential associated with the proxy. |
| `enabled` | **tinyint** | Specifies whether this proxy is enabled. `0` = not enabled, `1` = enabled. |
| `description` | **nvarchar(1024)** | The description for this proxy. |
| `user_sid` | **varbinary(85)** | The Windows security ID of the Windows user for this proxy. |
| `credential_id` | **int** | The identifier for the credential associated with this proxy. |
| `credential_identity_exists` | **int** | Specifies whether the `credential_identity` exists. `0` = doesn't exist, `1` = exists. |

## Remarks

When no parameters are provided, `sp_help_proxy` lists information for all proxies in the instance.

To determine which proxies a login can use for a given subsystem, specify *@name* and *@subsystem_name*. When these arguments are provided, `sp_help_proxy` lists proxies that the login specified might access and that might be used for the specified subsystem.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

The `credential_identity` and `user_sid` columns are only returned in the result set when members of **sysadmin** execute this stored procedure.

## Examples

### A. List information for all proxies

The following example lists information for all proxies in the instance.

```sql
USE msdb;
GO

EXEC dbo.sp_help_proxy;
GO
```

### B. List information for a specific proxy

The following example lists information for the proxy named `Catalog application proxy`.

```sql
USE msdb;
GO

EXEC dbo.sp_help_proxy
    @proxy_name = N'Catalog application proxy';
GO
```

## Related content

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [sp_add_proxy (Transact-SQL)](sp-add-proxy-transact-sql.md)
- [sp_delete_proxy (Transact-SQL)](sp-delete-proxy-transact-sql.md)
