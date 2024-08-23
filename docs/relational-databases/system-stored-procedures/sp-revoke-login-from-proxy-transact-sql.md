---
title: "sp_revoke_login_from_proxy (Transact-SQL)"
description: sp_revoke_login_from_proxy removes access to a proxy for a security principal.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_revoke_login_from_proxy_TSQL"
  - "sp_revoke_login_from_proxy"
helpviewer_keywords:
  - "sp_revoke_login_from_proxy"
dev_langs:
  - "TSQL"
---
# sp_revoke_login_from_proxy (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes access to a proxy for a security principal.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_revoke_login_from_proxy
    [ @name = ] N'name'
    [ , [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, server role, or `msdb` database role for which to remove access. *@name* is **nvarchar(256)**, with no default.

#### [ @proxy_id = ] *proxy_id*

The ID of the proxy for which to remove access. *@proxy_id* is **int**, with a default of `NULL`.

Either *@proxy_id* or *@proxy_name* must be specified, but both can't be specified.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy for which to remove access. *@proxy_name* is **sysname**, with a default of `NULL`.

Either *@proxy_id* or *@proxy_name* must be specified, but both can't be specified.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Jobs owned by the login that references this proxy fail to run.

## Permissions

To execute this stored procedure, a user must be a member of the **sysadmin** fixed server role.

## Examples

The following example revokes access for the login `terrid` to access the proxy `Catalog application proxy`.

```sql
USE msdb;
GO

EXEC dbo.sp_revoke_login_from_proxy
    @name = N'terrid',
    @proxy_name = N'Catalog application proxy';
GO
```

## Related content

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [sp_grant_login_to_proxy (Transact-SQL)](sp-grant-login-to-proxy-transact-sql.md)
- [sp_help_proxy (Transact-SQL)](sp-help-proxy-transact-sql.md)
