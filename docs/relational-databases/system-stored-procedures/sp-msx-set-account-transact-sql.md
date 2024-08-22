---
title: "sp_msx_set_account (Transact-SQL)"
description: sp_msx_set_account sets the SQL Server Agent master server account name and password on the target server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_msx_set_account"
  - "sp_msx_set_account_TSQL"
helpviewer_keywords:
  - "sp_msx_set_account"
dev_langs:
  - "TSQL"
---
# sp_msx_set_account (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent master server account name and password on the target server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_msx_set_account
    [ [ @credential_name = ] N'credential_name' ]
    [ , [ @credential_id = ] credential_id ]
[ ; ]
```

## Arguments

#### [ @credential_name = ] N'*credential_name*'

The name of the credential to use to sign in to the master server. *@credential_name* is **sysname**, with a default of `NULL`. The name provided must be the name of an existing credential.

Either *@credential_name* or *@credential_id* must be specified.

#### [ @credential_id = ] *credential_id*

The identifier for the credential to use to sign in to the master server. *@credential_id* is **int**, with a default of `NULL`. The identifier must be an identifier for an existing credential.

Either *@credential_name* or *@credential_id* must be specified.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses credentials to store the user name and password information that a target server uses to sign in to a master server. This procedure sets the credential that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent for this target server uses to sign in to the master server.

The credential specified must be an existing credential. For more information about creating a credential, see [CREATE CREDENTIAL](../../t-sql/statements/create-credential-transact-sql.md).

## Permissions

Execute permissions for `sp_msx_set_account` default to members of the **sysadmin** fixed server role.

## Examples

The following example sets this server to use the credential `MsxAccount` to connect to the master server.

```sql
USE msdb;
GO

EXECUTE dbo.sp_msx_set_account @credential_name = MsxAccount;
GO
```

## Related content

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-credential-transact-sql.md)
- [sp_msx_get_account (Transact-SQL)](sp-msx-get-account-transact-sql.md)
