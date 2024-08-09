---
title: "sp_grant_login_to_proxy (Transact-SQL)"
description: sp_grant_login_to_proxy grants a security principal access to a proxy.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_grant_login_to_proxy"
  - "sp_grant_login_to_proxy_TSQL"
helpviewer_keywords:
  - "sp_grant_login_to_proxy"
dev_langs:
  - "TSQL"
---
# sp_grant_login_to_proxy (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Grants a security principal access to a proxy.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_grant_login_to_proxy
    [ [ @login_name = ] N'login_name' ]
    [ , [ @fixed_server_role = ] N'fixed_server_role' ]
    [ , [ @msdb_role = ] N'msdb_role' ]
    [ , [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
[ ; ]
```

## Arguments

#### [ @login_name = ] N'*login_name*'

The login name to grant access to. *@login_name* is **nvarchar(256)**, with a default of `NULL`.

One of *@login_name*, *@fixed_server_role*, or *@msdb_role* must be specified, or the stored procedure fails.

#### [ @fixed_server_role = ] N'*fixed_server_role*'

The fixed server role to grant access to. *@fixed_server_role* is **nvarchar(256)**, with a default of `NULL`.

One of *@login_name*, *@fixed_server_role*, or *@msdb_role* must be specified, or the stored procedure fails.

#### [ @msdb_role = ] N'*msdb_role*'

The database role in the `msdb` database to grant access to. *@msdb_role* is **nvarchar(256)**, with a default of `NULL`.

One of *@login_name*, *@fixed_server_role*, or *@msdb_role* must be specified, or the stored procedure fails.

#### [ @proxy_id = ] *proxy_id*

The identifier for the proxy to grant access for. *@proxy_id* is **int**, with a default of `NULL`.

One of *@proxy_id* or *@proxy_name* must be specified, or the stored procedure fails.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy to grant access for. *@proxy_name* is **sysname**, with a default of `NULL`.

One of *@proxy_id* or *@proxy_name* must be specified, or the stored procedure fails.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_grant_login_to_proxy` must be run from the `msdb` database.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example allows the login `adventure-works\terrid` to use the proxy `Catalog application proxy`.

```sql
USE msdb;
GO

EXEC dbo.sp_grant_login_to_proxy
    @login_name = N'adventure-works\terrid',
    @proxy_name = N'Catalog application proxy';
GO
```

## Related content

- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [sp_add_proxy (Transact-SQL)](sp-add-proxy-transact-sql.md)
- [sp_revoke_login_from_proxy (Transact-SQL)](sp-revoke-login-from-proxy-transact-sql.md)
