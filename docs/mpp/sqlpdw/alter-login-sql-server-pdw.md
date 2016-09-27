---
title: "ALTER LOGIN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 95853d02-3482-4ea1-be33-174f2e5bf5ed
caps.latest.revision: 48
author: BarbKess
---
# ALTER LOGIN (SQL Server PDW)
Changes the properties of a login account.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER LOGIN login_name   
    {   
    <status_option>   
    | WITH <set_option> [ ,... ]  
    }   
  
<status_option> ::=ENABLE | DISABLE  
  
<set_option> ::=              
    PASSWORD ='password'   
    [   
      OLD_PASSWORD ='oldpassword'  
      | <password_option> [<password_option> ]   
    ]  
    | NAME = login_name  
    | CHECK_POLICY = { ON | OFF }  
    | CHECK_EXPIRATION = { ON | OFF }   
<password_option> ::=   
    MUST_CHANGE | UNLOCK  
```  
  
## Arguments  
*login_name*  
Specifies the name of the login that is being changed.  
  
ENABLE | DISABLE  
Enables or disables this login. Disabling a login does not affect the behavior of logins that are already connected. Disabled logins retain their permissions and can still be impersonated.  
  
PASSWORD **='***password***'**  
Applies only to SQL Server authentication logins. Specifies the new password for the login that is being changed. Passwords are case-sensitive. Passwords should always be at least 8 characters long, and cannot exceed 128 characters.  Passwords can include a-z, A-Z, 0-9, and most non-alphanumeric characters. Passwords cannot contain single quotes, or the *login_name*. Additionally, passwords must contain at least three of the following:  
  
-   An uppercase character (A-Z).  
  
-   A lowercase character (a-z).  
  
-   A digit (0-9).  
  
-   One of the non-alphanumeric characters, such as a space, _, @, *, ^, %, !, $, #, or &.  
  
> [!WARNING]  
> The characters **^ % &** are permitted in passwords, however PowerShell regards these as special characters. If these characters are used in passwords for the system administrator or SQL Server**sa** accounts (the **AdminPassword** and **PdwSAPassword** parameters during setup) then setup, including INSTALL, UPGRADE, REPLACENODE, and PATCHING, will fail. To ensure a successful upgrade when current passwords contain unsupported characters, change these passwords so that they do not contain such characters before running upgrade. After upgrade completes, you can set these passwords back to their original values.  
  
OLD_PASSWORD **='***oldpassword***'**  
Applies only to SQL Server authentication logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.  
  
MUST_CHANGE  
Applies only to SQL Server authentication logins. If this option is included, SQL Server will prompt for an updated password the first time the altered login is used.  
  
NAME = *login_name*  
The new name of the login that is being renamed. The new name of a login must conform to the [Object Naming Rules &#40;SQL Server PDW&#41;](../sqlpdw/object-naming-rules-sql-server-pdw.md). If this is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in SQL Server. The new name of a SQL Server login cannot contain a backslash character (\\).  
  
CHECK_EXPIRATION = { ON | OFF }  
Applies only to SQL Server authentication logins. Specifies whether password expiration policy should be enforced on this login.  
  
CHECK_POLICY **=** { ON | OFF }  
Applies only to SQL Server authentication logins. Specifies that the Windows password policies of the SQL Server PDW system on which SQL Server is running should be enforced on this login.  
  
UNLOCK  
Applies only to SQL Server authentication logins. Specifies that a login that is locked out should be unlocked.  
  
## General Remarks  
When CHECK_POLICY is changed to ON, the following behavior occurs:  
  
-   The password history is initialized with the value of the current password hash.  
  
When CHECK_POLICY is changed to OFF, the following behavior occurs:  
  
-   CHECK_EXPIRATION is also set to OFF.  
  
-   The password history is cleared.  
  
-   The value of *lockout_time* is reset.  
  
If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement will fail.  
  
If CHECK_POLICY is set to OFF, CHECK_EXPIRATION cannot be set to ON. An ALTER LOGIN statement that has this combination of options will fail.  
  
When a password has expired, the login can log in, but cannot perform any actions other than changing the password with the **ALTER LOGIN** statement.  
  
For a list of policies inherited from Windows, see [CREATE LOGIN &#40;SQL Server PDW&#41;](../sqlpdw/create-login-sql-server-pdw.md).  
  
You cannot use **ALTER_LOGIN** with the **DISABLE** argument to deny access to a Windows group. For example, `ALTER_LOGIN [`*domain\group*`] DISABLE` will return the following error message:  
  
"Msg 15151, Level 16, State 1, Line 1  
  
"Cannot alter the login '*Domain\Group*', because it does not exist or you do not have permission."  
  
This is by design.  
  
## Permissions  
Requires **ALTER** permission on the login. A principal can change the password and default datase for its own login.  
  
## Examples  
  
### A. Enabling a disabled login  
The following example enables the login `Mary5`.  
  
```  
ALTER LOGIN Mary5 ENABLE;  
```  
  
### B. Changing the password of a login  
The following example changes the password of login `Mary5` to a strong password.  
  
```  
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';  
```  
  
### C. Unlocking a login  
The following example unlocks the login `Mary5`.  
  
```  
ALTER LOGIN Mary8 WITH PASSWORD = 'jlkfdjl;ied51' MUST_CHANGE UNLOCK;  
```  
  
To unlock a login without changing the password, turn the check policy off and then on again.  
  
```  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = OFF;  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = ON;  
GO  
```  
  
### C. Changing the name of a login  
The following example changes the name of login `Mary5` to `John2`.  
  
```  
ALTER LOGIN Mary5 WITH NAME = John2;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE LOGIN &#40;SQL Server PDW&#41;](../sqlpdw/create-login-sql-server-pdw.md)  
[DROP LOGIN &#40;SQL Server PDW&#41;](../sqlpdw/drop-login-sql-server-pdw.md)  
  
