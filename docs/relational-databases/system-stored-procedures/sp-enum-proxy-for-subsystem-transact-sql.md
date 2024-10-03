---
title: "sp_enum_proxy_for_subsystem (Transact-SQL)"
description: sp_enum_proxy_for_subsystem lists permissions for SQL Server Agent proxies to access subsystems.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_enum_proxy_for_subsystem_TSQL"
  - "sp_enum_proxy_for_subsystem"
helpviewer_keywords:
  - "sp_enum_proxy_for_subsystems"
dev_langs:
  - "TSQL"
---
# sp_enum_proxy_for_subsystem (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists permissions for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxies to access subsystems.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_enum_proxy_for_subsystem
    [ [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
    [ , [ @subsystem_id = ] subsystem_id ]
    [ , [ @subsystem_name = ] N'subsystem_name' ]
[ ; ]
```

## Arguments

#### [ @proxy_id = ] *proxy_id*

The identification number of the proxy to list information for. *@proxy_id* is **int**, with a default of `NULL`.

Either the *@proxy_id* or the *@proxy_name* can be specified.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy to list information for. *@proxy_name* is **sysname**, with a default of `NULL`.

Either the *@proxy_id* or the *@proxy_name* can be specified.

#### [ @subsystem_id = ] *subsystem_id*

The identification number of the subsystem to list information for. *@subsystem_id* is **int**, with a default of `NULL`.

Either the *@subsystem_id* or the *@subsystem_name* can be specified.

#### [ @subsystem_name = ] N'*subsystem_name*'

The name of the subsystem to list information for. *@subsystem_name* is **sysname**, with a default of `NULL`.

Either the *@subsystem_id* or the *@subsystem_name* can be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `subsystem_id` | **int** | Subsystem identification number. |
| `subsystem_name` | **sysname** | The name of the subsystem. |
| `proxy_id` | **int** | Proxy identification number. |
| `proxy_name` | **sysname** | The name of the proxy. |

## Remarks

When no parameters are provided, `sp_enum_proxy_for_subsystem` lists information about all proxies in the instance for every subsystem.

When a *@proxy_id* or *@proxy_name* is provided, `sp_enum_proxy_for_subsystem` lists subsystems that the proxy has access to. When a *@subsystem_id* or *@subsystem_name* is provided, `sp_enum_proxy_for_subsystem` lists proxies that have access to that subsystem.

When information for both proxy and subsystem is provided, the result set returns a row if the proxy specified has access to the subsystem specified.

This stored procedure is located in `msdb`.

## Permissions

Execution permissions for this procedure default to members of the **sysadmin** fixed server role.

## Examples

### A. List all associations

The following example lists all permissions established between proxies and subsystems for the current instance.

```sql
USE msdb;
GO

EXEC dbo.sp_enum_proxy_for_subsystem;
GO
```

### B. Determine if a proxy has access to a specific subsystem

The following example returns a row if the proxy `Catalog application proxy` has access to the `ActiveScripting` subsystem. Otherwise, the example returns an empty result set.

```sql
USE msdb;
GO

EXEC dbo.sp_enum_proxy_for_subsystem
    @subsystem_name = 'ActiveScripting',
    @proxy_name = 'Catalog application proxy';
GO
```

## Related content

- [sp_grant_proxy_to_subsystem (Transact-SQL)](sp-grant-proxy-to-subsystem-transact-sql.md)
