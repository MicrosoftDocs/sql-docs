---
title: Security Considerations for SQL Server on Linux
description: Learn about SQL Server on Linux security overview, security best practices, restrictions, including how using keys stored in Azure Key Vault and extensible Key Management aren't supported.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/02/2026
ms.service: sql
ms.subservice: linux
ms.topic: best-practice
ms.custom:
  - linux-related-content
---
# Security considerations for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

Securing [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux is an ongoing process because Linux is a heterogeneous and continuously evolving operating system. Our goal is to help our customers improve security incrementally, building on what they already have and refining over time. This page serves as an index of key practices and resources for securing [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux.

## Begin with a secure Linux system

This article assumes that you deployed [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on a hardened and secured Linux system. Security measures vary by Linux distribution. For more information, see [Get started with SQL Server on SELinux](selinux.md).

Security practices vary based on the Linux distribution you're using. For detailed guidance, contact your distribution provider and review their recommended best practices. You can also refer to documentation such as:

- [Red Hat Enterprise Linux security hardening](https://docs.redhat.com/documentation/red_hat_enterprise_linux/9/html/security_hardening/index)
- [Ubuntu: Security suggestions](https://ubuntu.com/server/docs/explanation/security/security_suggestions)

Always validate your chosen platform and configuration in a controlled test environment before deploying to production.

## Apply SQL Server security guidance

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux offers a robust security framework combining multiple layers of protection.

- Create accounts and database users under the principle of least privilege.

- Use advanced features like row-level security and dynamic data masking for granular access control.

- File system security is enforced through strict ownership and permissions under `/var/opt/mssql`, ensuring only the `mssql` user and group have appropriate access.

- For enterprise integration, Active Directory authentication enables Kerberos-based single sign-on (SSO), centralized password policies, and group-based access management.

- Encrypted connections safeguard data in transit using TLS, with options for server or client-initiated encryption, and support for certificates that meet industry standards.

Together, these capabilities deliver a comprehensive approach to securing [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] deployments on Linux. Review and implement recommendations from these key resources:

- [Walkthrough for the security features of SQL Server on Linux](get-started.md)
- [SQL Server on Linux - Security and permissions guide](permissions-guide.md)
- [Active Directory authentication for SQL Server on Linux](authentication/active-directory-overview.md)
- [Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux](authentication/adutil-tutorial.md)
- [Encrypt connections to SQL Server on Linux](encrypted-connections.md)

## SQL Server auditing on Linux

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux supports the built-in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Audit feature, enabling you to track and log server-level and database-level events for compliance and security monitoring.

- [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)

## Common best practices

- Regularly update the Linux operating system and [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].
- Dedicate production servers exclusively to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] workloads.
- Apply the [principle of least privilege](https://techcommunity.microsoft.com/blog/azuresqlblog/security-the-principle-of-least-privilege-polp/2067390) for accounts and services.
- [Disable the SA account as a best practice](#disable-the-sa-account-as-a-best-practice).

For common security best practices on Windows and Linux, refer to [SQL Server security best practices](../../relational-databases/security/sql-server-security-best-practices.md)

## Disable the SA account as a best practice

[!INCLUDE [connect-with-sa](../includes/connect-with-sa.md)]

## Security limitations for SQL Server on Linux

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux currently has the following limitations:

- Starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] on Linux, you can enforce custom password policy. For more information, see [Set custom password policy for SQL logins in SQL Server on Linux](authentication/custom-password-policy.md).

  In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] on Linux and earlier versions, we provide a standard password policy:

  - `MUST_CHANGE` is the only option you can configure.

  - With the `CHECK_POLICY` option enabled, only the default policy provided by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is enforced, and doesn't apply the Windows password policies defined in the Active Directory group policies.

  - Password expiration is hard-coded to 90 days if you use [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] authentication. To work around this issue, consider changing the [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md).

- Extensible Key Management (EKM) is only supported through Azure Key Vault (AKV) in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU12 onward, and isn't available in earlier versions. Third party EKM providers aren't supported for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux operating systems.

- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] authentication mode can't be disabled.

- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] generates its own self-signed certificate for encrypting connections. You can configure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to use a user-provided certificate for TLS.

- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux deployments aren't FIPS compliant.

## Secure SQL Server on Linux container deployments

For information about securing [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] containers, see [Secure SQL Server Linux containers](../containers/security.md).

## Related content

- [Walkthrough for the security features of SQL Server on Linux](get-started.md)
- [Security and permissions guide for SQL Server on Linux](permissions-guide.md)
- [Configure SQL Server on Linux with the mssql-conf tool](../configure/mssql-conf.md)
- [Editions and supported features of SQL Server 2022 on Linux](../sql-server-linux-editions-and-components-2022.md)
- [Security for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)
