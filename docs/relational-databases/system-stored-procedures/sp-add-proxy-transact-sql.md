---
title: "sp_add_proxy (Transact-SQL)"
description: "Adds the specified SQL Server Agent proxy."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_proxy"
  - "sp_add_proxy_TSQL"
helpviewer_keywords:
  - "CREATE PROXY statement"
  - "sp_add_proxy"
dev_langs:
  - "TSQL"
---
# sp_add_proxy (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds the specified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_proxy
    [ @proxy_name = ] 'proxy_name'
    , [ @enabled = ] is_enabled
    , [ @description = ] 'description'
    , [ @credential_name = ] 'credential_name'
    , [ @credential_id = ] credential_id
    , [ @proxy_id = ] id OUTPUT
[ ; ]
```

## Arguments

#### [ @proxy_name = ] '*proxy_name*'

The name of the proxy to create. The *@proxy_name* is **sysname**, with a default of `NULL`. When the *@proxy_name* is `NULL` or an empty string, the name of the proxy defaults to the *@credential_name* or *@credential_id* supplied.

#### [ @enabled = ] *is_enabled*

Specifies whether the proxy is enabled. The *@enabled* flag is **tinyint**, with a default of `1`. When *@enabled* is `0`, the proxy isn't enabled, and can't be used by a job step.

#### [ @description = ] '*description*'

A description of the proxy. The description is **nvarchar(512)**, with a default of `NULL`. The description allows you to document the proxy, but isn't otherwise used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent. Therefore, this argument is optional.

#### [ @credential_name = ] '*credential_name*'

The name of the credential for the proxy. The *@credential_name* is **sysname**, with a default of `NULL`. Either *@credential_name* or *@credential_id* must be specified.

#### [ @credential_id = ] *credential_id*

The identification number of the credential for the proxy. The *@credential_id* is **int**, with a default of `NULL`. Either *@credential_name* or *@credential_id* must be specified.

#### [ @proxy_id = ] *proxy_id* OUTPUT

The proxy identification number assigned to the proxy if created successfully.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

This stored procedure must be run in the `msdb` database.

A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy manages security for job steps that involve subsystems other than the [!INCLUDE [tsql](../../includes/tsql-md.md)] subsystem. Each proxy corresponds to a security credential. A proxy might have access to any number of subsystems.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Members of the **sysadmin** fixed security role can create job steps that use any proxy. Use the stored procedure [sp_grant_login_to_proxy](sp-grant-login-to-proxy-transact-sql.md) to grant other logins access to the proxy.

## Examples

This example creates a proxy for the credential `CatalogApplicationCredential`. The code assumes that the credential already exists. For more information about credentials, see [CREATE CREDENTIAL](../../t-sql/statements/create-credential-transact-sql.md).

```sql
USE msdb;
GO

EXEC dbo.sp_add_proxy
    @proxy_name = 'Catalog application proxy',
    @enabled = 1,
    @description = 'Maintenance tasks on catalog application.',
    @credential_name = 'CatalogApplicationCredential';
GO
```

## Related content

- [CREATE CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-credential-transact-sql.md)
- [sp_grant_login_to_proxy (Transact-SQL)](sp-grant-login-to-proxy-transact-sql.md)
- [sp_revoke_login_from_proxy (Transact-SQL)](sp-revoke-login-from-proxy-transact-sql.md)
