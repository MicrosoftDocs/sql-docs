---
title: "SUSER_SID (Transact-SQL)"
description: "SUSER_SID (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "07/29/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
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
---
# SUSER_SID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the security identification number (SID) for the specified login name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SUSER_SID ( [ 'login' ] [ , Param2 ] )   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 **'** *login* **'**  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
 Is the login name of the user. *login* is **sysname**. *login*, which is optional, can be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user or group. If *login* is not specified, information about the current security context is returned. If the parameter contains the word NULL will return NULL.  
  
 *Param2*  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later
  
 Specifies whether the login name is validated. *Param2* is of type **int** and is optional. When *Param2* is 0, the login name is not validated. When *Param2* is not specified as 0, the Windows login name is verified to be exactly the same as the login name stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Return Types  
 **varbinary(85)**  
  
## Remarks  
 SUSER_SID can be used as a DEFAULT constraint in either ALTER TABLE or CREATE TABLE. SUSER_SID can be used in a select list, in a WHERE clause, and anywhere an expression is allowed. SUSER_SID must always be followed by parentheses, even if no parameter is specified.  
  
 When called without an argument, SUSER_SID returns the SID of the current security context. When called without an argument within a batch that has switched context by using EXECUTE AS, SUSER_SID returns the SID of the impersonated context. When called from an impersonated context, SUSER_SID(ORIGINAL_LOGIN()) returns the SID of the original context.  
  
 When the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation and the Windows collation are different, SUSER_SID can fail when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows store the login in a different format. For example, if the Windows computer TestComputer has the login User and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stores the login as TESTCOMPUTER\User, the lookup of the login TestComputer\User might fail to resolve the login name correctly. To skip this validation of the login name, use *Param2*. Differing collations is often a cause of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error 15401:  
  
 `Windows NT user or group '%s' not found. Check the name again.`  
  
## [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] Remarks  
 SUSER_SID always return the login SID for the current security context. Use [sys.database_principals](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md) to obtain the SID of a different login.
  
 The SUSER_SID statement does not support execution using an impersonated security context through EXECUTE AS.  

## Examples  
  
### A. Using SUSER_SID  
 The following example returns the security identification number (SID) for the current security context.  
  
```sql
SELECT SUSER_SID();  
```  
  
### B. Using SUSER_SID with a specific login  
 The following example returns the security identification number for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] `sa` login.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later
  
```sql
SELECT SUSER_SID('sa');  
GO  
```  
  
### C. Using SUSER_SID with a Windows user name  
 The following example returns the security identification number for the Windows user `London\Workstation1`.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later
  
```sql
SELECT SUSER_SID('London\Workstation1');  
GO  
```  
  
### D. Using SUSER_SID as a DEFAULT constraint  
 The following example uses `SUSER_SID` as a `DEFAULT` constraint in a `CREATE TABLE` statement.  
  
```sql
USE AdventureWorks2012;  
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
  
### E. Comparing the Windows login name to the login name stored in SQL Server  
 The following example shows how to use *Param2* to obtain the SID from Windows and uses that SID as an input to the `SUSER_SNAME` function. The example provides the login in the format in which it is stored in Windows (`TestComputer\User`), and returns the login in the format in which it is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (`TESTCOMPUTER\User`).  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later
  
```sql
SELECT SUSER_SNAME(SUSER_SID('TestComputer\User', 0));  
```  
  
## See Also  
 [ORIGINAL_LOGIN &#40;Transact-SQL&#41;](../../t-sql/functions/original-login-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [binary and varbinary &#40;Transact-SQL&#41;](../../t-sql/data-types/binary-and-varbinary-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)  
  
  
