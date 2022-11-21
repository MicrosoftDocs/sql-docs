---
title: "LOGINPROPERTY (Transact-SQL)"
description: "LOGINPROPERTY (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "BadPasswordCount_TSQL"
  - "BadPasswordTime_TSQL"
  - "IsLockedIsMustChange"
  - "PasswordLastSetTime"
  - "LOGINPROPERTY_TSQL"
  - "LockoutTime"
  - "BadPasswordCount"
  - "PasswordHash"
  - "HistoryLengthIsExpired"
  - "LockoutTime_TSQL"
  - "PasswordHash_TSQL"
  - "HistoryLengthIsExpired_TSQL"
  - "PasswordLastSetTime_TSQL"
  - "BadPasswordTime"
  - "IsLockedIsMustChange_TSQL"
  - "LOGINPROPERTY"
helpviewer_keywords:
  - "default database"
  - "LOGINPROPERTY function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azure-sqldw-latest"
---
# LOGINPROPERTY (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns information about login policy settings.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
LOGINPROPERTY ( 'login_name' , 'property_name' )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *login_name*  
 Is the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for which login property status will be returned.  
  
 *propertyname*  
 Is an expression that contains the property information to be returned for the login. *propertyname* can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**BadPasswordCount**|Returns the number of consecutive attempts to log in with an incorrect password.|  
|**BadPasswordTime**|Returns the time of the last attempt to log in with an incorrect password.|  
|**DaysUntilExpiration**|Returns the number of days until the password expires.|  
|**DefaultDatabase**|Returns the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login default database as stored in metadata or **master** if no database is specified. Returns NULL for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provisioned users (for example, Windows authenticated users).|  
|**DefaultLanguage**|Returns the login default language as stored in metadata. Returns NULL for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provisioned users, for example, Windows authenticated users.|  
|**HistoryLength**|Returns the number of passwords tracked for the login, using the password-policy enforcement mechanism. 0 if the password policy is not enforced. Resuming password policy enforcement restarts at 1.|  
|**IsExpired**|Indicates whether the login's password has expired.|  
|**IsLocked**|Indicates whether the login is locked.|  
|**IsMustChange**|Indicates whether the login must change its password the next time it connects.|  
|**LockoutTime**|Returns the date when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login was locked out because it had exceeded the permitted number of failed login attempts.|  
|**PasswordHash**|Returns the hash of the password.|  
|**PasswordLastSetTime**|Returns the date when the current password was set.|  
|**PasswordHashAlgorithm**|Returns the algorithm used to hash the password.|  
  
## Returns  
 Data type depends on requested value.  
  
 **IsLocked**, **IsExpired**, and **IsMustChange** are of type **int**.  
  
-   1 if the login is in the specified state.  
  
-   0 if the login is not in the specified state.  
  
 **BadPasswordCount** and **HistoryLength** are of type **int**.  
  
 **BadPasswordTime**, **LockoutTime**, **PasswordLastSetTime** are of type **datetime**.  
  
 **PasswordHash** is of type **varbinary**.  
  
 NULL if the login is not a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 **DaysUntilExpiration** is of type **int**.  
  
-   0 if the login is expired or if it will expire on the day when queried.  
  
-   -1 if the local security policy in Windows never expires the password.  
  
-   NULL if the CHECK_POLICY or CHECK_EXPIRATION is OFF for a login, or if the operating system does not support the password policy.  
  
 **PasswordHashAlgorithm** is of type int.  
  
-   0 if a SQL7.0 hash  
  
-   1 if a SHA-1 hash  
  
-   2 if a SHA-2 hash  
  
-   NULL if the login is not a valid SQL Server login  
  
## Remarks  
 This built-in function returns information about the password policy settings of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The names of the properties are not case sensitive, so property names such as **BadPasswordCount** and **badpasswordcount** are equivalent. The values of the **PasswordHash, PasswordHashAlgorithm**, and **PasswordLastSetTime** properties are available on all supported configurations of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but the other properties are only available when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running on [!INCLUDE[winserver2003](../../includes/winserver2003-md.md)] and both CHECK_POLICY and CHECK_EXPIRATION are enabled. For more information, see [Password Policy](../../relational-databases/security/password-policy.md).  
  
## Permissions  
 Requires VIEW permission on the login. When requesting the password hash, also requires CONTROL SERVER permission.  
  
## Examples  
  
### A. Checking whether a login must change its password  
 The following example checks whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `John3` must change its password the next time it connects to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```sql  
SELECT LOGINPROPERTY('John3', 'IsMustChange');  
GO  
```  
  
### B. Checking whether a login is locked out  
 The following example checks whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `John3` is locked.  
  
```sql  
SELECT LOGINPROPERTY('John3', 'IsLocked');  
GO  
```  
  
## See Also  
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)  
  
  
