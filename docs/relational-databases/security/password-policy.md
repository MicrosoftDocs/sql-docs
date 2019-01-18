---
title: "Password Policy | Microsoft Docs"
ms.custom: ""
ms.date: "01/16/2019"
ms.prod: sql
ms.prod_service: security
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "ALTER LOGIN statement"
  - "passwords [SQL Server], policy enforcement"
  - "logins [SQL Server], passwords"
  - "CHECK_EXPIRATION option"
  - "complex passwords [SQL Server]"
  - "passwords [SQL Server], expiration"
  - "manual bad password count resets"
  - "MUST_CHANGE option"
  - "expiration [SQL Server]"
  - "expired password [SQL Server]"
  - "symbols [SQL Server]"
  - "NetValidatePasswordPolicy() API"
  - "passwords [SQL Server]"
  - "password policy [SQL Server]"
  - "resetting bad password counts"
  - "security [SQL Server], passwords"
  - "CHECK_POLICY option"
  - "passwords [SQL Server], symbols"
  - "bad password counts"
  - "passwords [SQL Server], complexity"
  - "characters [SQL Server], password policies"
ms.assetid: c0040c0a-a18f-45b9-9c40-0625685649b1
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Password Policy

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can use Windows password policy mechanisms. The password policy applies to a login that uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication, and to a contained database user with password.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can apply the same complexity and expiration policies used in Windows to passwords used inside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This functionality depends on the `NetValidatePasswordPolicy` API.  
  
> [!NOTE]
> [!INCLUDE[ssSDS](../../includes/sssds-md.md)] enforces password complexity. The password expiration and  policy enforcement sections do not apply to [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
## Password Complexity  

 Password complexity policies are designed to deter brute force attacks by increasing the number of possible passwords. When password complexity policy is enforced, new passwords must meet the following guidelines:  
  
- The password does not contain the account name of the user.  
  
- The password is at least eight characters long.  
  
- The password contains characters from three of the following four categories:  
  
  - Latin uppercase letters (A through Z)  
  
  - Latin lowercase letters (a through z)  
  
  - Base 10 digits (0 through 9)  
  
  - Non-alphanumeric characters such as: exclamation point (!), dollar sign ($), number sign (#), or percent (%).  
  
 Passwords can be up to 128 characters long. Use passwords that are as long and complex as possible.  
  
## Password Expiration  

 Password expiration policies are used to manage the lifespan of a password. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] enforces password expiration policy, users are reminded to change old passwords, and accounts that have expired passwords are disabled.  
  
## Policy Enforcement  

 The enforcement of password policy can be configured separately for each SQL Server login. Use [ALTER LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/alter-login-transact-sql.md) to configure the password policy options of a SQL Server login. The following rules apply to the configuration of password policy enforcement:  
  
- When CHECK_POLICY is changed to ON, the following behaviors occur:  
  
  - CHECK_EXPIRATION is also set to ON unless it is explicitly set to OFF.  
  
  - The password history is initialized with the value of the current password hash.  
  
  - **Account lockout duration**, **account lockout threshold**, and **reset account lockout counter after** are also enabled.  
  
- When CHECK_POLICY is changed to OFF, the following behaviors occur:  
  
  - CHECK_EXPIRATION is also set to OFF.  
  
  - The password history is cleared.  
  
  - The value of `lockout_time` is reset.  
  
 Some combinations of policy options are not supported.  
  
- If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement fails.  
  
- If CHECK_POLICY is set to OFF, CHECK_EXPIRATION cannot be set to ON. An ALTER LOGIN statement that has this combination of options will fail.  
  
- Setting CHECK_POLICY = ON prevents the creation of passwords that are:  
  
  - Null or empty  
  
  - Same as name of computer or login  
  
  - Any of the following: "password", "admin", "administrator", "sa", "sysadmin"  
  
 The security policy might be set in Windows, or might be received from the domain. To view the password policy on the computer, use the Local Security Policy MMC snap-in (**secpol.msc**).  
  
## Related Tasks  

 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)  
  
 [ALTER LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/alter-login-transact-sql.md)  
  
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)  
  
 [ALTER USER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-user-transact-sql.md)  
  
 [Create a Login](../../relational-databases/security/authentication-access/create-a-login.md)  
  
 [Create a Database User](../../relational-databases/security/authentication-access/create-a-database-user.md)  
  
## Related Content  

 [Strong Passwords](../../relational-databases/security/strong-passwords.md)  
 