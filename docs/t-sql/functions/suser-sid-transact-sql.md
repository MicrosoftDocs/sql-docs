---
title: "SUSER_SID (Transact-SQL)"
description: "SUSER_SID returns the security identification number (SID) for the specified login name."
author: VanMSFT
ms.author: vanto
ms.date: 09/26/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SUSER_SID"
  - "SUSER_SID_TSQL"
helpviewer_keywords:
  - "logins [SQL Server], users"
  - "SIDs [SQL Server]"
  - "security identifiers [SQL Server]"
  - "users [SQL Server], logins"
  - "15401 (Database Engine error)"
  - "IDs [SQL Server], logins"
  - "identification numbers [SQL Server], logins"
  - "SUSER_SID function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# SUSER_SID (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

 Returns the security identification number (SID) for the specified login name.

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SUSER_SID ( [ 'login' ] [ , Param2 ] )
```

## Arguments

#### '* *login* *'

**Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions

The login name of the user. *login* is **sysname**. *login*, which is optional, can be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user or group. If *login* is not specified, information about the current security context is returned. If the parameter contains `NULL`, `SUSER_SID` returns `NULL`.

#### *Param2*

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions

Specifies whether the login name is validated. *Param2* is of type **int** and is optional. When *Param2* is 0, the login name is not validated. When *Param2* is not specified as 0, the Windows login name is verified to be exactly the same as the login name stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Return types

**varbinary(85)**

## Remarks

 `SUSER_SID` can be used as a `DEFAULT` constraint in either `ALTER TABLE` or `CREATE TABLE`. `SUSER_SID` can be used in a select list, in a `WHERE` clause, and anywhere an expression is allowed. `SUSER_SID` must always be followed by parentheses, even if no parameter is specified.

 When called without an argument, `SUSER_SID` returns the SID of the current security context. When called without an argument within a batch that has switched context by using `EXECUTE AS`, `SUSER_SID` returns the SID of the impersonated context. When called from an impersonated context, `SUSER_SID(ORIGINAL_LOGIN())` returns the SID of the original context.

 When the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation and the Windows collation are different, `SUSER_SID` can fail when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows store the login in a different format. For example, if the Windows computer `TestComputer` has the login `User` and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stores the login as `TESTCOMPUTER\User`, the lookup of the login `TestComputer\User` might fail to resolve the login name correctly. To skip this validation of the login name, use *Param2*. Differing collations is often a cause of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error 15401: `Windows NT user or group '%s' not found. Check the name again.`

<a id="-remarks"></a>

### Remarks for Azure SQL Database, SQL database in Fabric

 `SUSER_SID` always return the login SID for the current security context. Use [sys.database_principals](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md) to obtain the SID of a different login.

 The `SUSER_SID` statement does not support execution using an impersonated security context through `EXECUTE AS`.

## Examples

<a id="a-using-suser_sid"></a>

### A. Use SUSER_SID

 The following example returns the security identification number (SID) for the current security context.

```sql
SELECT SUSER_SID();
```

<a id="b-using-suser_sid-with-a-specific-login"></a>

### B. Use SUSER_SID with a specific login

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions

 The following example returns the security identification number for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] `sa` login.

```sql
SELECT SUSER_SID('sa');
GO
```

<a id="c-using-suser_sid-with-a-windows-user-name"></a>

### C. Use SUSER_SID with a Windows user name

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions

 The following example returns the security identification number for the Windows user `London\Workstation1`.

```sql
SELECT SUSER_SID('London\Workstation1');
GO
```

<a id="d-using-suser_sid-as-a-default-constraint"></a>

### D. Use SUSER_SID as a DEFAULT constraint

 The following example uses `SUSER_SID` as a `DEFAULT` constraint in a `CREATE TABLE` statement.

```sql
USE AdventureWorks2022;
GO
CREATE TABLE sid_example
(
login_sid   VARBINARY(85) DEFAULT SUSER_SID(),
login_name  VARCHAR(30) DEFAULT SYSTEM_USER,
login_dept  VARCHAR(10) DEFAULT 'SALES',
login_date  DATETIME DEFAULT GETDATE()
);  
GO
INSERT sid_example DEFAULT VALUES;
GO
```

<a id="e-comparing-the-windows-login-name-to-the-login-name-stored-in-sql-server"></a>

### E. Compare the Windows login name to the login name stored in SQL Server

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions

 The following example shows how to use *Param2* to obtain the SID from Windows and uses that SID as an input to the `SUSER_SNAME` function. The example provides the login in the format in which it is stored in Windows (`TestComputer\User`), and returns the login in the format in which it is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (`TESTCOMPUTER\User`).

```sql
SELECT SUSER_SNAME(SUSER_SID('TestComputer\User', 0));
```    

## Related content

- [ORIGINAL_LOGIN (Transact-SQL)](original-login-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../statements/create-table-transact-sql.md)
- [binary and varbinary (Transact-SQL)](../data-types/binary-and-varbinary-transact-sql.md)
- [System Functions by category for Transact-SQL](../../relational-databases/system-functions/system-functions-category-transact-sql.md)
