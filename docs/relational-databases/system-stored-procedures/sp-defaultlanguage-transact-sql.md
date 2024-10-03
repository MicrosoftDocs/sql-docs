---
title: "sp_defaultlanguage (Transact-SQL)"
description: sp_defaultlanguage changes the default language of for a SQL Server login.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_defaultlanguage"
  - "sp_defaultlanguage_TSQL"
helpviewer_keywords:
  - "sp_defaultlanguage"
dev_langs:
  - "TSQL"
---
# sp_defaultlanguage (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the default language of for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_defaultlanguage
    [ @loginame = ] N'loginame'
    [ , [ @language = ] N'language' ]
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The login name. *@loginame* is **sysname**, with no default. *@loginame* can be an existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, or a Windows user or group.

#### [ @language = ] N'*language*'

The default language of the login. *@language* is **sysname**, with a default of `NULL`. *@language* must be a valid language on the server. If *@language* isn't specified, *@language* is set to the server default language (defined by the [default language](../../database-engine/configure-windows/configure-the-default-language-server-configuration-option.md) server configuration option).

Changing the server default language doesn't change the default language for existing logins.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_defaultlanguage` calls `ALTER LOGIN`, which supports extra options. For information about changing other login defaults, see [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md).

Use the `SET LANGUAGE` statement to change the language of the current session. Use the `@@LANGUAGE` function to show the current language setting.

If the default language of a login is dropped from the server, the login acquires the default language of the server. `sp_defaultlanguage` can't be executed within a user-defined transaction.

Information about languages installed on the server is visible in the `sys.syslanguages` catalog view.

## Permissions

Requires `ALTER ANY LOGIN` permission.

## Examples

The following example uses `ALTER LOGIN` to change the default language for login `Fathima` to Arabic. This is the preferred method.

```sql
ALTER LOGIN Fathima WITH DEFAULT_LANGUAGE = Arabic;
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [ALTER LOGIN (Transact-SQL)](../../t-sql/statements/alter-login-transact-sql.md)
- [&#x40;&#x40;LANGUAGE (Transact-SQL)](../../t-sql/functions/language-transact-sql.md)
- [SET Statements (Transact-SQL)](../../t-sql/statements/set-statements-transact-sql.md)
- [sys.syslanguages (Transact-SQL)](../system-compatibility-views/sys-syslanguages-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
