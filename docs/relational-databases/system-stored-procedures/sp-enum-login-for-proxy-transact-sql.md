---
title: "sp_enum_login_for_proxy (Transact-SQL)"
description: sp_enum_login_for_proxy lists associations between security principals and proxies.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_enum_login_for_proxy_TSQL"
  - "sp_enum_login_for_proxy"
helpviewer_keywords:
  - "sp_enum_login_for_proxy"
dev_langs:
  - "TSQL"
---
# sp_enum_login_for_proxy (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists associations between security principals and proxies.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_enum_login_for_proxy
    [ [ @name = ] N'name' ]
    [ , [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] principal, login, server role, or `msdb` database role to list proxies for. *@name* is **nvarchar(256)**, with a default of `NULL`.

#### [ @proxy_id = ] *proxy_id*

The proxy identification number of the proxy to list information for. *@proxy_id* is **int**, with a default of `NULL`.

Either the *@proxy_id* or the *@proxy_name* can be specified.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy to list information for. *@proxy_name* is **sysname**, with a default of `NULL`.

Either the *@proxy_id* or the *@proxy_name* can be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `proxy_id` | **int** | Proxy identification number. |
| `proxy_name` | **nvarchar(128)** | The name of the proxy. |
| `flags` | **int** | Type of the security principal.<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login<br />`1` = Fixed system role<br />`2` = Database role in `msdb` |
| `name` | **nvarchar(128)** | Name of the security principal for the association. |
| `sid` | **varbinary(85)** | Security identifier (SID) of the security principal for the association. |
| `principal_id` | **int** | Principal ID of the security principal for the association. |

## Remarks

When no parameters are provided, `sp_enum_login_for_proxy` lists information about all logins in the instance for every proxy.

When a *@proxy_id* or *proxy_name* is provided, `sp_enum_login_for_proxy` lists logins that have access to the proxy. When a *@name* is provided, `sp_enum_login_for_proxy` lists the proxies that the login has access to.

When both proxy information and a login name are provided, the result set returns a row if the login specified has access to the proxy specified.

This stored procedure is located in `msdb`.

## Permissions

Execution permissions for this procedure default to members of the **sysadmin** fixed server role.

## Examples

### A. List all associations

The following example lists all permissions established between logins and proxies in the current instance.

```sql
USE msdb;
GO

EXEC dbo.sp_enum_login_for_proxy;
GO
```

### B. List proxies for a specific login

The following example lists the proxies that the login `terrid` has access to.

```sql
USE msdb;
GO

EXEC dbo.sp_enum_login_for_proxy @name = 'terrid';
GO
```

## Related content

- [sp_help_proxy (Transact-SQL)](sp-help-proxy-transact-sql.md)
- [sp_grant_login_to_proxy (Transact-SQL)](sp-grant-login-to-proxy-transact-sql.md)
- [sp_revoke_login_from_proxy (Transact-SQL)](sp-revoke-login-from-proxy-transact-sql.md)
