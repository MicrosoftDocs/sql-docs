---
title: "sp_msx_get_account (Transact-SQL)"
description: sp_msx_get_account lists information on the credential that the target server uses to log in to the master server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_msx_get_account_TSQL"
  - "sp_msx_get_account"
helpviewer_keywords:
  - "sp_msx_get_account"
dev_langs:
  - "TSQL"
---
# sp_msx_get_account (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists information on the credential that the target server uses to sign in to the master server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_msx_get_account
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Result set

Returns the following result set:

| Column name | Type | Description |
| --- | --- | --- |
| `msx_connection` | **int** | Master server connection number. |
| `msx_credential_id` | **int** | ID of the credential used for this master server connection. |
| `msx_credential_name` | **sysname** | Name of the credential used for this master server connection. |
| `msx_login_name` | **nvarchar(4000)** | Domain name and user name of the Windows user for the credential. |

## Remarks

Returns an empty result set if there's no credential specified for this target server. To set the credential, use `sp_msx_set_account`.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example lists information for the credential that this target server uses to connect to the master server.

```sql
USE msdb;
GO

EXECUTE dbo.sp_msx_get_account;
GO
```

Here's a sample result set:

```output
msx_connection msx_credential_id msx_credential_name  msx_login_name
-------------- ----------------- -------------------- -----------------------------
1              65538             MsxAccount           AdventureWorks2022\MsxAccount
```

## Related content

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-credential-transact-sql.md)
- [sp_msx_set_account (Transact-SQL)](sp-msx-set-account-transact-sql.md)
