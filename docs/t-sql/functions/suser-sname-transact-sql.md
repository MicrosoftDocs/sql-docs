---
title: "SUSER_SNAME (Transact-SQL)"
description: "SUSER_SNAME returns the login name associated with a security identification number (SID)."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/04/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SUSER_SNAME_TSQL"
  - "SUSER_SNAME"
helpviewer_keywords:
  - "security identification names [SQL Server]"
  - "logins [SQL Server], users"
  - "SIDs [SQL Server]"
  - "SUSER_SNAME function"
  - "users [SQL Server], logins"
  - "logins [SQL Server], names"
  - "IDs [SQL Server], logins"
  - "identification numbers [SQL Server], logins"
  - "names [SQL Server], logins"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current || =fabric"
---
# SUSER_SNAME (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns the login name associated with a security identification number (SID).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SUSER_SNAME ( [ server_user_sid ] )
```

## Arguments

#### *server_user_sid*

The optional login security identification number. *server_user_sid* is **varbinary(85)**. *server_user_sid* can be the security identification number of any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user or group. Refer to the `sid` column in `sys.server_principals` or `sys.sql_logins` catalog views. If *server_user_sid* isn't specified, information about the current user is returned. If the parameter contains the word `NULL`, `SUSER_SNAME` returns `NULL`.

*server_user_sid* is not supported on [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

## Return type

**nvarchar(128)**

## Remarks

`SUSER_SNAME` can be used as a DEFAULT constraint in either `ALTER TABLE` or `CREATE TABLE`. `SUSER_SNAME` can be used in a select list, in a WHERE clause, and anywhere an expression is allowed. `SUSER_SNAME` must always be followed by parentheses, even if no parameter is specified.

When called without an argument, `SUSER_SNAME` returns the name of the current security context. When called without an argument within a batch that has switched context by using `EXECUTE AS`, `SUSER_SNAME` returns the name of the impersonated context. When called from an impersonated context, `ORIGINAL_LOGIN` returns the name of the original context.

## [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] remarks

`SUSER_SNAME` always returns the login name for the current security context.

The `SUSER_SNAME` statement doesn't support execution using an impersonated security context through EXECUTE AS.

`SUSER_SNAME` doesn't support the *server_user_id* argument.

## Examples

### A. Use SUSER_SNAME

The following example returns the login name for the current security context.

```sql
SELECT SUSER_SNAME();
GO
```

### B. Use SUSER_SNAME with a Windows user security ID

The following example returns the login name associated with a Windows security identification number.

```sql
SELECT SUSER_SNAME(0x010500000000000515000000a065cf7e784b9b5fe77c87705a2e0000);
GO
```

### C. Use SUSER_SNAME as a DEFAULT constraint

The following example uses `SUSER_SNAME` as a DEFAULT constraint in a `CREATE TABLE` statement.

```sql
USE AdventureWorks2022;
GO

CREATE TABLE sname_example (
    login_sname SYSNAME DEFAULT SUSER_SNAME(),
    employee_id UNIQUEIDENTIFIER DEFAULT NEWID(),
    login_date DATETIME DEFAULT GETDATE()
    );
GO

INSERT sname_example DEFAULT
VALUES;
GO
```

### D. Call SUSER_SNAME in combination with EXECUTE AS

This example shows the behavior of `SUSER_SNAME` when called from an impersonated context.

```sql
SELECT SUSER_SNAME();
GO

EXECUTE AS LOGIN = 'WanidaBenShoof';
SELECT SUSER_SNAME();

REVERT;
GO

SELECT SUSER_SNAME();
GO
```

Here is the result.

```output
sa
WanidaBenShoof
sa
```

## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

### E. Use SUSER_SNAME

The following example returns the login name for the security identification number with a value of `0x01`.

```sql
SELECT SUSER_SNAME(0x01);
GO
```

### F. Return the current login

The following example returns the login name of the current login.

```sql
SELECT SUSER_SNAME() AS CurrentLogin;
GO
```

## Related content

- [SUSER_SID (Transact-SQL)](../../t-sql/functions/suser-sid-transact-sql.md)
- [Principals (Database Engine)](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [sys.server_principals (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)
- [EXECUTE AS (Transact-SQL)](../../t-sql/statements/execute-as-transact-sql.md)
