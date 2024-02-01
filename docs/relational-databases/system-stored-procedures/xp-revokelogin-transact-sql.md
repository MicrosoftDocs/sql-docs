---
title: "xp_revokelogin (Transact-SQL)"
description: "Revokes access from a Windows group or user to SQL Server."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 05/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "xp_revokelogin"
  - "xp_revokelogin_TSQL"
helpviewer_keywords:
  - "xp_revokelogin"
dev_langs:
  - "TSQL"
---
# xp_revokelogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Revokes access from a Windows group or user to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [DROP LOGIN](../../t-sql/statements/drop-login-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
xp_revokelogin { [ @loginame = ] 'login' }
```

## Arguments

#### [ @loginame = ] '*login*'

The name of the Windows user or group from which to revoke access. *@loginame* must include the domain name, for example `[CONTOSO\sylvester1]`. *@loginame* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Use `DROP LOGIN` instead.

## Permissions

Requires ALTER ANY LOGIN permission on the server.

## Related content

- [sp_denylogin (Transact-SQL)](sp-denylogin-transact-sql.md)
- [sp_grantlogin (Transact-SQL)](sp-grantlogin-transact-sql.md)
- [sp_revokelogin (Transact-SQL)](sp-revokelogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [General extended stored procedures (Transact-SQL)](general-extended-stored-procedures-transact-sql.md)
- [xp_loginconfig (Transact-SQL)](xp-loginconfig-transact-sql.md)
- [xp_logininfo (Transact-SQL)](xp-logininfo-transact-sql.md)
