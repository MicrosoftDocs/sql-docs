---
title: "Password Policy"
description: This article goes over the Windows password policy mechanisms applying to a login that uses SQL Server authentication and to a contained database user with a password.
author: VanMSFT
ms.author: vanto
ms.date: 05/24/2024
ms.service: sql
ms.subservice: security
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
---
# Password Policy

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can use Windows password policy mechanisms. The password policy applies to a login that uses [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication, and to a contained database user with password.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can apply the same complexity and expiration policies used in Windows to passwords used inside [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This functionality depends on the `NetValidatePasswordPolicy` API.

> [!NOTE]  
> Azure SQL Database enforces [password complexity](#password-complexity). The password expiration and policy enforcement sections do not apply to Azure SQL Database.
>
> For information on password policy for Azure SQL Managed Instance, see our [SQL Managed Instance FAQ](/azure/azure-sql/managed-instance/frequently-asked-questions-faq#password-policy-).

## Password Complexity

Password complexity policies are designed to deter brute force attacks by increasing the number of possible passwords. When password complexity policy is enforced, new passwords must meet the following guidelines:

- The password doesn't contain the account name of the user.

- The password is at least eight characters long.

- The password contains characters from three of the following four categories:

  - Latin uppercase letters (A through Z)

  - Latin lowercase letters (a through z)

  - Base 10 digits (0 through 9)

  - Nonalphanumeric characters such as: exclamation point (!), dollar sign ($), number sign (#), or percent (%).

Passwords can be up to 128 characters long. Use passwords that are as long and complex as possible.

## Password Expiration

Password expiration policies are used to manage the lifespan of a password. When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] enforces password expiration policy, users are reminded to change old passwords, and accounts that have expired passwords are disabled.

## Policy Enforcement

The enforcement of password policy can be configured separately for each SQL Server login. Use [ALTER LOGIN (Transact-SQL)](../../t-sql/statements/alter-login-transact-sql.md) to configure the password policy options of a SQL Server login. The following rules apply to the configuration of password policy enforcement:

- When CHECK_POLICY is changed to ON, the following behaviors occur:

  - CHECK_EXPIRATION is also set to ON unless it's explicitly set to OFF.

  - The password history is initialized with the value of the current password hash.

  - **Account lockout duration**, **account lockout threshold**, and **reset account lockout counter after** are also enabled.

- When CHECK_POLICY is changed to OFF, the following behaviors occur:

  - CHECK_EXPIRATION is also set to OFF.

  - The password history is cleared.

  - The value of `lockout_time` is reset.

Some combinations of policy options aren't supported.

- If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement fails.

- If CHECK_POLICY is set to OFF, CHECK_EXPIRATION can't be set to ON. An ALTER LOGIN statement that has this combination of options will fail.

- Setting CHECK_POLICY = ON prevents the creation of passwords that are:

  - Null or empty

  - Same as name of computer or login

  - Any of the following: `password`, `admin`, `administrator`, `sa`, `sysadmin`

The security policy might be set in Windows, or might be received from the domain. To view the password policy on the computer, use the Local Security Policy MMC snap-in (**secpol.msc**).

> [!NOTE]  
> For SQL Server logins that have CHECK_POLICY enabled, if you run [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) and do not include OLD_PASSWORD in the command to change the password, then [Enforce Password History](/windows/security/threat-protection/security-policy-settings/enforce-password-history) is ignored. This is a by-design behavior to allow password resets, despite any previously used passwords. Other checks associated with CHECK_POLICY, including length and complexity are checked regardless of whether OLD_PASSWORD is used.

You can review SQL user password policies and expiration dates in Azure SQL Database, using the following query:

```sql
SELECT name,
    is_policy_checked,
    is_expiration_checked,
    LOGINPROPERTY(name, 'IsMustChange') AS IsMustChange,
    LOGINPROPERTY(name, 'IsLocked') AS IsLocked,
    LOGINPROPERTY(name, 'LockoutTime') AS LockoutTime,
    LOGINPROPERTY(name, 'PasswordLastSetTime') AS PasswordLastSetTime,
    LOGINPROPERTY(name, 'IsExpired') AS IsExpired,
    LOGINPROPERTY(name, 'BadPasswordCount') AS BadPasswordCount,
    LOGINPROPERTY(name, 'BadPasswordTime') AS BadPasswordTime,
    LOGINPROPERTY(name, 'HistoryLength') AS HistoryLength,
    modify_date
FROM sys.sql_logins;
```

## Related tasks

[CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)

[ALTER LOGIN (Transact-SQL)](../../t-sql/statements/alter-login-transact-sql.md)

[CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)

[ALTER USER (Transact-SQL)](../../t-sql/statements/alter-user-transact-sql.md)

[Create a Login](../../relational-databases/security/authentication-access/create-a-login.md)

[Create a Database User](../../relational-databases/security/authentication-access/create-a-database-user.md)

## Related content

- [Strong Passwords](../../relational-databases/security/strong-passwords.md)
