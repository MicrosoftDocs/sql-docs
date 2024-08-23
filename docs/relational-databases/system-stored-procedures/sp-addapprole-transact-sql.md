---
title: "sp_addapprole (Transact-SQL)"
description: Adds an application role to the current database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addapprole_TSQL"
  - "sp_addapprole"
helpviewer_keywords:
  - "sp_addapprole"
dev_langs:
  - "TSQL"
---
# sp_addapprole (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds an application role to the current database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE APPLICATION ROLE](../../t-sql/statements/create-application-role-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addapprole
    [ @rolename = ] N'rolename'
    , [ @password = ] N'password'
[ ; ]
```

## Arguments

#### [ @rolename = ] N'*rolename*'

The name of the new application role. *@rolename* is **sysname**, with no default. *@rolename* must be a valid identifier and can't already exist in the current database.

Application role names can contain from 1 up to 128 characters, including letters, symbols, and numbers. Role names can't contain a backslash (`\`) nor be `NULL` or an empty string ('').

#### [ @password = ] N'*password*'

The password required to activate the application role. *@password* is **sysname**, with no default. *@password* can't be `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

In earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], users (and roles) aren't fully distinct from schemas. Beginning with [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)], schemas are fully distinct from roles. This architecture is reflected in the behavior of `CREATE APPLICATION ROLE`. This statement supersedes `sp_addapprole`.

To maintain backward compatibility with earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], `sp_addapprole` does the following checks:

- If a schema with the same name as the application role doesn't already exist, the schema is created. The new schema is owned by the application role, and it's the default schema of the application role.

- If a schema of the same name as the application role already exists, the procedure fails.

- `sp_addapprole` doesn't check password complexity. Password complexity is checked by `CREATE APPLICATION ROLE`.

The parameter *@password* is stored as a one-way hash.

The `sp_addapprole` stored procedure can't be executed from within a user-defined transaction.

> [!IMPORTANT]  
> The Microsoft ODBC `encrypt` option isn't supported by **SqlClient**. When you can, prompt users to enter application role credentials at run time. Avoid storing credentials in a file. If you must persist credentials, encrypt them by using the CryptoAPI functions.

## Permissions

Requires ALTER ANY APPLICATION ROLE permission on the database. If a schema with the same name and owner as the new role doesn't already exist, also requires CREATE SCHEMA permission on the database.

## Examples

The following example adds the new application role `SalesApp` with the password `x97898jLJfcooFUYLKm387gf3` to the current database.

```sql
EXEC sp_addapprole 'SalesApp', 'x97898jLJfcooFUYLKm387gf3';
GO
```

## Related content

- [CREATE APPLICATION ROLE (Transact-SQL)](../../t-sql/statements/create-application-role-transact-sql.md)
