---
title: "CREATE LOGIN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 81e59651-d418-4e48-8129-03a40eab976d
caps.latest.revision: 62
author: BarbKess
---
# CREATE LOGIN (SQL Server PDW)
Creates a login account to access a SQL Server PDW appliance. Use this statement for managing appliance security.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE LOGIN loginName { WITH <option_list1> | FROM WINDOWS }  
  
<option_list1> ::=   
    PASSWORD = { 'password' } [ MUST_CHANGE ]  
    [ , <option_list2> [ ,... ] ]  
  
<option_list2> ::=    
      CHECK_EXPIRATION = { ON | OFF}  
    | CHECK_POLICY = { ON | OFF}  
```  
  
## Arguments  
*login_name*  
Specifies the name of the login that is created. SQL Server PDW supports two types of logins: SQL Server logins and Windows logins. When you are creating logins that are mapped from a Windows domain account, you must use the pre-Windows 2000 user logon name in the format `[`*appliance_domain*`\`*<login_name>*`]`. You cannot use a UPN in the format `login_name@DomainName`. SQL Server authentication logins are type **sysname** and must conform to the rules for [Object Naming Rules &#40;SQL Server PDW&#41;](../sqlpdw/object-naming-rules-sql-server-pdw.md) and cannot contain a '**\\**'. Windows logins can contain a '**\\**'.  
  
*password*  
Applies to SQL Server logins only. The password to use for the initial login. Passwords are case-sensitive. Passwords should always be at least 8 characters long, and cannot exceed 128 characters.  Passwords can include a-z, A-Z, 0-9, and most non-alphanumeric characters. Passwords cannot contain single quotes, or the *login_name*. Additionally, passwords must contain at least three of the following:  
  
-   An uppercase character (A-Z).  
  
-   A lowercase character (a-z).  
  
-   A digit (0-9).  
  
-   One of the non-alphanumeric characters, such as a space, _, @, *, ^, %, !, $, #, or &.  
  
> [!WARNING]  
> The characters **^ % &** are permitted in passwords, however PowerShell regards these as special characters. If these characters are used in passwords for the system administrator or SQL Server**sa** accounts (the **AdminPassword** and **PdwSAPassword** parameters during setup) then setup, including INSTALL, UPGRADE, REPLACENODE, and PATCHING, will fail. To ensure a successful upgrade when current passwords contain unsupported characters, change these passwords so that they do not contain such characters before running upgrade. After upgrade completes, you can set these passwords back to their original values.  
  
MUST_CHANGE  
Applies to SQL Server logins only. If this option is included, SQL Server prompts the user for a new password the first time the new login is used.  
  
CHECK_EXPIRATION **=** { ON | **OFF** }  
Applies to SQL Server logins only. Specifies whether password expiration policy should be enforced on this login. The default value is OFF.  
  
Applies to SQL Server logins only. CHECK_POLICY **=** { **ON** | OFF }  
Specifies that the Windows password policies of the SQL Server PDW system on which SQL Server is running should be enforced on this login. The default value is ON.  
  
WINDOWS  
Specifies that the login be mapped to a Windows login.  
  
## Permissions  
Requires **ALTER ANY LOGIN** permission.  
  
## General Remarks  
Creating a login automatically enables the new login and grants the login **CONNECT SQL** permission to the SQL Server PDW. To connect to a database, create a database user for the login. For more information, see [CREATE USER &#40;SQL Server PDW&#41;](../sqlpdw/create-user-sql-server-pdw.md). For more information about granting and denying permissions to logins and users, see [Permissions: GRANT, DENY, REVOKE &#40;SQL Server PDW&#41;](../sqlpdw/permissions-grant-deny-revoke-sql-server-pdw.md).  
  
If **MUST_CHANGE** is specified, **CHECK_EXPIRATION** and **CHECK_POLICY** must be set to **ON**. Otherwise, the statement will fail.  
  
A combination of `CHECK_POLICY = OFF` and `CHECK_EXPIRATION = ON` is not supported.  
  
When **CHECK_POLICY** is set to **OFF**, the lockout_time is reset and **CHECK_EXPIRATION** is set to **OFF**.  
  
### Default Windows Policies  
The following policies are inherited from the Windows domain.  
  
-   Password Policies  
  
    -   Enforced password history: 24 passwords remembered  
  
    -   Maximum password age: 42 days  
  
    -   Minimum password age: 1 day  
  
    -   Minimum password length: 7 characters  
  
        > [!IMPORTANT]  
        > Passwords are encrypted using blocks of 7 characters (plus a check character). Passwords should always be at least 8 characters long so that at least two encryption blocks are required, greatly increasing the complexity of the encryption.  
  
    -   Password must meet complexity requirements: Enabled  
  
    -   Store password using reversible encryption: Disabled  
  
-   Account Lockout Policies  
  
    -   Account lockout duration: 30 minutes  
  
    -   Account lockout threshold: 10 invalid login attempts  
  
    -   Reset account lockout counter after: 30 minutes  
  
-   Password complexity policies are listed at [Password Policy](http://msdn.microsoft.com/en-us/library/ms161959(SQL.100).aspx).  
  
> [!NOTE]  
> Changing the Analytics Platform System appliance domain account lockout and password policies is not supported.  
  
## Examples  
  
### A. Creating a SQL Server authentication login with a password  
The following example creates the login `Mary7` with password `A2c3456`.  
  
```Transact-SQL  
CREATE LOGIN Mary7 WITH PASSWORD = 'A2c3456$#' ;  
```  
  
### B. Using Options  
The following example creates the login `Mary8` with password some of the optional arguments.  
  
```  
CREATE LOGIN Mary8 WITH PASSWORD = 'A2c3456$#' MUST_CHANGE,  
CHECK_EXPIRATION = ON,  
CHECK_POLICY = ON;  
```  
  
### C. Creating a login from a Windows domain account  
The following example creates a login from a Windows domain account named `Mary` in the `Contoso` domain.  
  
```  
CREATE LOGIN [Contoso\Mary] FROM WINDOWS;  
GO  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[DROP LOGIN &#40;SQL Server PDW&#41;](../sqlpdw/drop-login-sql-server-pdw.md)  
[ALTER LOGIN &#40;SQL Server PDW&#41;](../sqlpdw/alter-login-sql-server-pdw.md)  
  
