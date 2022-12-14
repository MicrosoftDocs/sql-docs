---
title: "PWDCOMPARE (Transact-SQL)"
description: "PWDCOMPARE (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "PWDCOMPARE"
  - "PWDCOMPARE_TSQL"
helpviewer_keywords:
  - "sa account"
  - "passwords [SQL Server], blank"
  - "PWDCOMPARE function [Transact-SQL]"
dev_langs:
  - "TSQL"
---
# PWDCOMPARE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Hashes a password and compares the hash to the hash of an existing password. PWDCOMPARE can be used to search for blank [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login passwords or common weak passwords.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
PWDCOMPARE ( 'clear_text_password'  
   , password_hash   
   [ , version ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 **'** *clear_text_password* **'**  
 Is the unencrypted password. *clear_text_password* is **sysname** (**nvarchar(128)**).  
  
 *password_hash*  
 Is the encryption hash of a password. *password_hash* is **varbinary(128)**.  
  
 *version*  
 Obsolete parameter that can be set to 1 if *password_hash* represents a value from a login earlier than [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] that was migrated to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later but never converted to the [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] system. *version* is **int**.  
  
> [!CAUTION]  
>  This parameter is provided for backwards compatibility, but is ignored because password hash blobs now contain their own version descriptions. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
## Return Types  
 **int**  
  
 Returns 1 if the hash of the *clear_text_password* matches the *password_hash* parameter, and 0 if it does not.  
  
## Remarks  
 The PWDCOMPARE function is not a threat against the strength of password hashes because the same test could be performed by trying to log in using the password provided as the first parameter.  
  
 **PWDCOMPARE** cannot be used with the passwords of contained database users. There is no contained database equivalent.  
  
## Permissions  
 PWDENCRYPT is available to public.  
  
 CONTROL SERVER permission is required to examine the password_hash column of sys.sql_logins.  
  
## Examples  
  
### A. Identifying logins that have no passwords  
 The following example identifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins that have no passwords.  
  
```sql  
SELECT name FROM sys.sql_logins   
WHERE PWDCOMPARE('', password_hash) = 1 ;  
```  
  
### B. Searching for common passwords  
 To search for common passwords that you want to identify and change, specify the password as the first parameter. For example, execute the following statement to search for a password specified as `password`.  
  
```sql  
SELECT name FROM sys.sql_logins   
WHERE PWDCOMPARE('password', password_hash) = 1 ;  
```  
  
## See Also  
 [PWDENCRYPT &#40;Transact-SQL&#41;](../../t-sql/functions/pwdencrypt-transact-sql.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)  
  
  
