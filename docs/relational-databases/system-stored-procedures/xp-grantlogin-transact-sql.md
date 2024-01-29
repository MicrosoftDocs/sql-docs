---
title: "xp_grantlogin (Transact-SQL)"
description: "Grants a Windows group or user access to SQL Server."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "xp_grantlogin"
  - "xp_grantlogin_TSQL"
helpviewer_keywords:
  - "xp_grantlogin"
dev_langs:
  - "TSQL"
---
# xp_grantlogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Grants a Windows group or user access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE LOGIN](../../t-sql/statements/create-login-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
xp_grantlogin { [ @loginame = ] 'login' } [ , [ @logintype = ] 'logintype' ]
```

## Arguments

#### [ @loginame = ] '*login*'

The name of the Windows user or group to be added. The Windows user or group must be qualified with a Windows domain name in the form `<domain>\<user>`. *@loginame* is **sysname**, with no default.

#### [ @logintype = ] '*logintype*'

The security level of the login being granted access. *@logintype* is **varchar(5)**, with a default of `NULL`. Only `admin` can be specified. If `admin` is specified, *@loginame* is granted access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and added as a member of the **sysadmin** fixed server role.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`xp_grantlogin` is a system stored procedure instead of an extended stored procedure. `xp_grantlogin` calls `sp_grantlogin` and `sp_addsrvrolemember`.

## Permissions

Requires membership in the **securityadmin** fixed server role. Changing the *@logintype* requires membership in the **sysadmin** fixed server role.

## Related content

- [sp_denylogin (Transact-SQL)](sp-denylogin-transact-sql.md)
- [sp_grantlogin (Transact-SQL)](sp-grantlogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [General extended stored procedures (Transact-SQL)](general-extended-stored-procedures-transact-sql.md)
- [xp_enumgroups (Transact-SQL)](xp-enumgroups-transact-sql.md)
- [xp_loginconfig (Transact-SQL)](xp-loginconfig-transact-sql.md)
- [xp_logininfo (Transact-SQL)](xp-logininfo-transact-sql.md)
- [sp_revokelogin (Transact-SQL)](sp-revokelogin-transact-sql.md)
