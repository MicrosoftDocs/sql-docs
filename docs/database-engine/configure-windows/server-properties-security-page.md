---
title: "Server Properties (Security page)"
description: Become familiar with server security settings in SQL Server. Learn about options that control server authentication, proxy accounts, and other features.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/23/2026
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
f1_keywords:
  - "sql13.swb.serverproperties.security.f1"
---
# Server Properties (Security page)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use this page to view or modify your server security options.

## Server Authentication

### Windows Authentication mode

Uses Windows Authentication to validate attempted connections. If the `sa` password is blank when the security mode is being changed, the user is prompted to enter an `sa` password.

> [!IMPORTANT]  
> Windows Authentication is much more secure than SQL Server Authentication. When possible, you should use Windows Authentication.

### SQL Server and Windows Authentication mode

Uses mixed mode authentication to verify attempted connections, for backward compatibility with earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If the `sa` password is blank when the security mode is being changed, the user is prompted to enter an `sa` password.

> [!NOTE]
> Changing the security configuration requires a restart of the service. When changing the Server Authentication to SQL Server and Windows Authentication mode the SA account isn't automatically enabled. To use the SA account, execute [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) with the `ENABLE` option.

## Authentication modes explained

Choosing the right authentication mode affects the security, manageability, and application compatibility of your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

### Windows Authentication

Windows Authentication uses the security credentials of the Windows operating system to validate user connections. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't store or manage passwords directly — it relies on the Windows domain controller (Active Directory or local accounts) for credential validation.

Key characteristics:

- Uses Kerberos or NTLM protocols for credential validation.
- Supports centralized password policies including complexity requirements, expiration, and account lockout through Active Directory Group Policy.
- Enables single sign-on (SSO) — users don't need to enter separate SQL Server credentials.
- Provides built-in auditing through the Windows Security Event Log.
- Supports Windows group-based access, which simplifies permission management for large numbers of users.

### SQL Server Authentication

SQL Server Authentication uses login accounts stored in the `master` database. Each login has its own username and password that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] manages independently from Windows credentials.

Key characteristics:

- Credentials are stored directly in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], separate from the Windows domain.
- Requires users to provide a username and password in every connection string.
- Doesn't support Kerberos delegation or centralized domain password policies by default.
- Useful when clients aren't part of a Windows domain, such as internet-facing applications or cross-platform environments.

### Comparison of authentication modes

| Feature | Windows Authentication | SQL Server and Windows Authentication (mixed mode) |
|---|---|---|
| **Protocol** | Kerberos or NTLM | Kerberos, NTLM, or SQL password |
| **Password management** | Managed by Active Directory | SQL logins managed by SQL Server; Windows logins managed by Active Directory |
| **Single sign-on** | Yes | Only for Windows logins |
| **Centralized password policy** | Yes (Active Directory Group Policy) | SQL Server enforces its own password policy for SQL logins |
| **Supports non-domain clients** | No | Yes |
| **Best suited for** | Enterprise and intranet environments | Internet-facing applications, cross-platform environments, or mixed scenarios |

### Choose the right authentication mode

Use the following guidelines when you select an authentication mode:

- **Use Windows Authentication mode** when all clients are domain-joined Windows machines, you want centralized credential management through Active Directory, and you don't need to support non-Windows clients.
- **Use SQL Server and Windows Authentication mode** when you need to support applications that can't use Windows Authentication, clients that run on non-Windows operating systems, or legacy applications that require SQL Server logins.

> [!IMPORTANT]
> Even when you use mixed mode, prefer Windows Authentication logins for administrative accounts and internal applications. Reserve SQL Server Authentication for scenarios where Windows Authentication isn't possible.

For more information, see [Choose an authentication mode](../../relational-databases/security/choose-an-authentication-mode.md).

## Login auditing

#### None

Turns off login auditing.

#### Failed logins only

Audits unsuccessful logins only.

#### Successful logins only

Audits successful logins only.

#### Both failed and successful logins

Audits all login attempts.

> [!NOTE]  
> Changing the audit level requires restarting the service.

## Server proxy account

#### Enable server proxy account

Enables an account for use by `xp_cmdshell`. Proxy accounts allow for the impersonation of logins, server roles, and database roles when an operating system command is being executed.

> [!CAUTION]  
> The login used by the server proxy account should have the least privileges required to perform the intended work. Excessive privileges for the proxy account could be used by a malicious user to compromise your system security.

#### Proxy account

Specify the proxy account used.

#### Password

Specify the password for the proxy account.

## Options

#### Enable C2 audit tracing

Audits all attempts to access statements and objects and records them to a file in the `\MSSQL\Data` directory for default instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], or the \MSSQL$*instancename*\Data directory for named instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [c2 audit mode Server Configuration Option](c2-audit-mode-server-configuration-option.md).

#### Cross database ownership chaining

Select to allow the database to be the source or target of a cross-database ownership chain. For more information, see [cross db ownership chaining Server Configuration Option](cross-db-ownership-chaining-server-configuration-option.md).

## Related content

- [Choose an authentication mode](../../relational-databases/security/choose-an-authentication-mode.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [ALTER LOGIN (Transact-SQL)](../../t-sql/statements/alter-login-transact-sql.md)
