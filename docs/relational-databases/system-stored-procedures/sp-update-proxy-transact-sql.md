---
title: "sp_update_proxy (Transact-SQL)"
description: Changes the properties of an existing proxy.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_proxy"
  - "sp_update_proxy_TSQL"
helpviewer_keywords:
  - "ALTER PROXY statement"
  - "sp_update_proxy"
dev_langs:
  - "TSQL"
---
# sp_update_proxy (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the properties of an existing proxy.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_update_proxy
    [ [ @proxy_id = ] proxy_id ]
    [ , [ @proxy_name = ] N'proxy_name' ]
    [ , [ @credential_name = ] N'credential_name' ]
    [ , [ @credential_id = ] credential_id ]
    [ , [ @new_name = ] N'new_name' ]
    [ , [ @enabled = ] enabled ]
    [ , [ @description = ] N'description' ]
[ ; ]
```

## Arguments

#### [ @proxy_id = ] *proxy_id*

The proxy identification number of the proxy to change. *@proxy_id* is **int**, with a default of `NULL`.

#### [ @proxy_name = ] N'*proxy_name*'

The name of the proxy to change. *@proxy_name* is **sysname**, with a default of `NULL`.

#### [ @credential_name = ] N'*credential_name*'

The name of the new credential for the proxy. *@credential_name* is **sysname**, with a default of `NULL`. Either *@credential_name* or *@credential_id* must be specified.

#### [ @credential_id = ] *credential_id*

The identification number of the new credential for the proxy. *@credential_id* is **int**, with a default of `NULL`. Either *@credential_name* or *@credential_id* must be specified.

#### [ @new_name = ] N'*new_name*'

The new name of the proxy. *@new_name* is **sysname**, with a default of `NULL`. When provided, the procedure changes the name of the proxy to *@new_name*. When this argument is `NULL`, the name of the proxy remains unchanged.

#### [ @enabled = ] *enabled*

Specifies whether the proxy is enabled. *@enabled* is **tinyint**, with a default of `NULL`. When *@enabled* is `0`, the proxy isn't enabled, and can't be used by a job step. When this argument is `NULL`, the status of the proxy remains unchanged.

#### [ @description = ] N'*description*'

The new description of the proxy. *@description* is **nvarchar(512)**, with a default of `NULL`. When this argument is `NULL`, the description of the proxy remains unchanged.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Either *@proxy_name* or *@proxy_id* must be specified. If both arguments are specified, the arguments must both refer to the same proxy or the stored procedure fails.

Either *@credential_name* or *@credential_id* must be specified to change the credential for the proxy. If both arguments are specified, the arguments must refer to the same credential or the stored procedure fails.

This procedure changes the proxy, but doesn't change access to the proxy. To change access to a proxy, use `sp_grant_login_to_proxy` and `sp_revoke_login_from_proxy`.

## Permissions

Only members of the **sysadmin** fixed security role can execute this procedure.

## Examples

The following example sets the enabled value for the proxy `Catalog application proxy` to `0`.

```sql
USE msdb;
GO

EXEC dbo.sp_update_proxy
    @proxy_name = 'Catalog application proxy',
    @enabled = 0;
GO
```

## Related content

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md)
- [sp_add_proxy (Transact-SQL)](sp-add-proxy-transact-sql.md)
- [sp_delete_proxy (Transact-SQL)](sp-delete-proxy-transact-sql.md)
- [sp_grant_login_to_proxy (Transact-SQL)](sp-grant-login-to-proxy-transact-sql.md)
- [sp_revoke_login_from_proxy (Transact-SQL)](sp-revoke-login-from-proxy-transact-sql.md)
