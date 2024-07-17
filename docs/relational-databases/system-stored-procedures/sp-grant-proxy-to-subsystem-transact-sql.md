---
title: "sp_grant_proxy_to_subsystem (Transact-SQL)"
description: sp_grant_proxy_to_subsystem grants a proxy access to a subsystem.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_grant_login_to_subsystem_TSQL"
  - "sp_grant_login_to_subsystem"
helpviewer_keywords:
  - "sp_grant_proxy_to_subsystem"
dev_langs:
  - "TSQL"
---
# sp_grant_proxy_to_subsystem (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Grants a proxy access to a subsystem.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_grant_proxy_to_subsystem
    [ [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
    [ , [ @subsystem_id = ] subsystem_id ]
    [ , [ @subsystem_name = ] N'subsystem_name' ]
[ ; ]
```

## Arguments

#### [ @proxy_id = ] *proxy_id*

The proxy identification number of the proxy to grant access for. *@proxy_id* is **int**, with a default of `NULL`.

Either *@proxy_id* or *@proxy_name* must be specified, but both can't be specified.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy to grant access for. *@proxy_name* is **sysname**, with a default of `NULL`.

Either *@proxy_id* or *@proxy_name* must be specified, but both can't be specified.

#### [ @subsystem_id = ] *subsystem_id*

The ID number of the subsystem to grant access to. *@subsystem_id* is **int**, with a default of `NULL`.

Either *@subsystem_id* or *@subsystem_name* must be specified, but both can't be specified.

The following table lists the values for each subsystem.

| Value | Description |
| --- | --- |
| `2` | ActiveX Script <sup>1</sup> |
| `3` | Operating System (`CmdExec`) |
| `4` | Replication Snapshot Agent |
| `5` | Replication Log Reader Agent |
| `6` | Replication Distribution Agent |
| `7` | Replication Merge Agent |
| `8` | Replication Queue Reader Agent |
| `9` | Analysis Services Query |
| `10` | Analysis Services Command |
| `11` | [!INCLUDE [ssIS](../../includes/ssis-md.md)] package execution |
| `12` | PowerShell Script |

<sup>1</sup> [!INCLUDE [ssnotedepfutureavoid-md](../../includes/ssnotedepfutureavoid-md.md)]

#### [ @subsystem_name = ] N'*subsystem_name*'

The name of the subsystem to grant access to. *@subsystem_name* is **sysname**, with a default of `NULL`.

Either *@subsystem_id* or *@subsystem_name* must be specified, but both can't be specified.

The following table lists the values for each subsystem.

| Value | Description |
| --- | --- |
| `ActiveScripting` | ActiveX Script |
| `CmdExec` | Operating System (`CmdExec`) |
| `Snapshot` | Replication Snapshot Agent |
| `LogReader` | Replication Log Reader Agent |
| `Distribution` | Replication Distribution Agent |
| `Merge` | Replication Merge Agent |
| `QueueReader` | Replication Queue Reader Agent |
| `ANALYSISQUERY` | Analysis Services Query |
| `ANALYSISCOMMAND` | Analysis Services Command |
| `Dts` | SSIS package execution |
| `PowerShell` | PowerShell Script |

## Remarks

Granting a proxy access to a subsystem doesn't change the permissions for the principal specified in the proxy.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

### A. Grant access to a subsystem by ID

The following example grants the proxy `Catalog application proxy` access to the ActiveX Scripting subsystem.

```sql
USE msdb;
GO

EXEC dbo.sp_grant_proxy_to_subsystem
    @proxy_name = 'Catalog application proxy',
    @subsystem_id = 2;
GO
```

### B. Grant access to a subsystem by name

The following example grants the proxy `Catalog application proxy` access to the SSIS package execution subsystem.

```sql
USE msdb;
GO

EXEC dbo.sp_grant_proxy_to_subsystem
    @proxy_name = N'Catalog application proxy',
    @subsystem_name = N'Dts' ;
GO
```

## Related content

- [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md)
- [sp_revoke_proxy_from_subsystem (Transact-SQL)](sp-revoke-proxy-from-subsystem-transact-sql.md)
- [sp_add_proxy (Transact-SQL)](sp-add-proxy-transact-sql.md)
- [sp_delete_proxy (Transact-SQL)](sp-delete-proxy-transact-sql.md)
- [sp_update_proxy (Transact-SQL)](sp-update-proxy-transact-sql.md)
