---
title: "sp_xp_cmdshell_proxy_account (Transact-SQL)"
description: Creates a proxy credential for xp_cmdshell.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/06/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_xp_cmdshell_proxy_account"
  - "sp_xp_cmdshell_proxy_account_TSQL"
helpviewer_keywords:
  - "sp_xp_cmdshell_proxy_account"
  - "xp_cmdshell"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_xp_cmdshell_proxy_account (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Creates a proxy credential for `xp_cmdshell`.

> [!NOTE]  
> `xp_cmdshell` is disabled by default. To enable `xp_cmdshell`, see [xp_cmdshell (server configuration option)](../../database-engine/configure-windows/xp-cmdshell-server-configuration-option.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_xp_cmdshell_proxy_account [ NULL | { 'account_name' , 'password' } ]
[ ; ]
```

## Arguments

- **NULL**

  Specifies that the proxy credential should be deleted.

- **'*account_name*'**

  Specifies the Windows account to be the proxy.

- **'*password*'**

  Specifies the password of the Windows account.

## Return code values

`0` (success) or `1` (failure).

## Remarks

The proxy credential is called **##xp_cmdshell_proxy_account##**.

When it is executed using the NULL option, `sp_xp_cmdshell_proxy_account` deletes the proxy credential.

## Permissions

Requires CONTROL SERVER permission.

## Examples

### A. Create the proxy credential

The following example shows how to create a proxy credential for a Windows account called `ADVWKS\Max04` with password `ds35efg##65`.

```sql
EXEC sp_xp_cmdshell_proxy_account 'ADVWKS\Max04', 'ds35efg##65';
GO
```

### B. Drop the proxy credential

The following example removes the proxy credential from the credential store.

```sql
EXEC sp_xp_cmdshell_proxy_account NULL;
GO
```

## Related content

- [xp_cmdshell (Transact-SQL)](xp-cmdshell-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-credential-transact-sql.md)
- [sys.credentials (Transact-SQL)](../system-catalog-views/sys-credentials-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
