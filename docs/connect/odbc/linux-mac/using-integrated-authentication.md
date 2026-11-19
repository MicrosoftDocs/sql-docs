---
title: "Use Integrated Authentication"
description: Configure Kerberos integrated authentication for Microsoft ODBC Driver connections to SQL Server on Linux and macOS.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, sunilbs, mcimfl
ms.date: 11/05/2026
ms.service: sql
ms.subservice: connectivity
ms.custom: linux-related-content
ms.topic: concept-article
ai-usage: ai-assisted
helpviewer_keywords:
  - "integrated authentication"
---

# Use integrated authentication

<a id="using-integrated-authentication"></a>

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The Microsoft ODBC Driver for SQL Server on Linux and macOS supports Kerberos integrated authentication. The driver uses the Generic Security Service Application Program Interface (GSSAPI) and Kerberos version 5 libraries, and supports the MIT Kerberos Key Distribution Center (KDC).

This article describes SQL Server connections that use `Trusted_Connection=yes`. For Microsoft Entra ID authentication, use `Authentication=ActiveDirectoryIntegrated` with Driver 17.6 and later versions, including Driver 18. Review the [federated-account requirements and platform restrictions](../using-azure-active-directory.md#considerations-for-using-adfs-federated-accounts-on-linuxmacos), including Kerberos library requirements for alternate user principal name (UPN) suffixes.

<a id="using-integrated-authentication-to-connect-to-sql-server-from-an-odbc-application"></a>

## Connect from an ODBC application

Before connecting, obtain a Kerberos ticket for the application's principal. For `SQLDriverConnect`, specify `Trusted_Connection=yes` in the connection string:

```text
Driver={ODBC Driver 18 for SQL Server};Server=<server>;Encrypt=yes;TrustServerCertificate=no;Trusted_Connection=yes;
```

Replace `<server>` with the server name. For Driver 17, change the driver name to `ODBC Driver 17 for SQL Server`. Configure certificate trust and the expected server name as described in [Configure ODBC connections to Microsoft SQL](../configure-connections.md#configure-encryption-and-certificate-validation).

For `SQLConnect`, configure `Trusted_Connection=yes` in the data source name (DSN) entry in `odbc.ini`. `SQLConnect` accepts a DSN, not a connection string. Remove `UID`, `PWD`, and incompatible `Authentication` settings from the DSN and application configuration when selecting Kerberos integrated authentication.

To supply a service principal name (SPN) instead of the name that the driver generates, use `ServerSPN`. Current Driver 17 and Driver 18 releases support this setting on Linux and macOS. Configure the matching Kerberos service registration before using an explicit SPN. `FailoverPartnerSPN` remains Windows-only.

<a id="deploying-a-linux-or-macos-odbc-driver-application-designed-to-run-as-a-service"></a>

## Run an application as a service

Configure Kerberos on the client and make the default principal's credential cache available to the service process. Obtain a ticket-granting ticket (TGT) through one of these methods:

- Run `kinit` with the principal name and enter the password at its prompt. Don't place the password in a command line.
- Run `kinit` with a keytab provisioned for the service principal. Restrict access to the keytab as you would a password.
- Use a login configured with a Kerberos pluggable authentication module (PAM) that obtains the principal's ticket.

Kerberos credentials expire, and the ODBC driver doesn't renew them. Arrange ticket renewal or reacquisition before expiration, for example through a scheduled service task. Run that task under the service identity and make its credential cache available to the application.

<a id="tracking-access-to-a-database"></a>

## Audit database access

The driver authenticates with the default Kerberos principal available to the process. When an application connects with a shared service principal, the database login identifies that principal, not each person using the application.

If the application needs to execute under a different database user's permissions, use [EXECUTE AS](../../../t-sql/statements/execute-as-transact-sql.md) with the required impersonation permissions. This changes the database execution context; it doesn't authenticate the application user or obtain a new Kerberos identity. Use [REVERT](../../../t-sql/statements/revert-transact-sql.md) to restore the previous context before returning the connection to a pool.

When using connection pooling, isolate connections by the required authenticated identity and reset session state before reuse. Don't assume that changing the process's Kerberos credential cache changes the login of an already authenticated pooled connection. For pool configuration, see [unixODBC connection pooling](http://www.unixodbc.org/doc/conn_pool.html).

Use the pool's supported connection-reset mechanism rather than treating a call to `sp_reset_connection` as a new login. The driver supports the ODBC 3.8 Driver Manager reset request through `SQL_ATTR_RESET_CONNECTION`. Resetting a session doesn't acquire credentials or reauthenticate it as another principal. Verify both the login identity and execution context when testing pooled access.

<a id="using-active-directory-to-manage-user-identities"></a>

## Manage identities with Active Directory

You can use Active Directory as the Kerberos KDC for SQL Server integrated authentication instead of maintaining separate SQL passwords. Active Directory domain authentication and Microsoft Entra ID authentication are different connection methods. For more information about domain authentication, see [Microsoft Kerberos](/windows/desktop/SecAuthN/microsoft-kerberos).

<a id="using-linked-server-and-distributed-queries"></a>

## Connect through a linked server

Integrated authentication on the client connection doesn't automatically configure credential forwarding to a linked server. Choose the identity for each hop and configure the required SPNs, Kerberos delegation, and linked-server login mappings:

1. The user authenticates to the application.
1. The application connects to the first SQL Server instance using its service identity or a user identity it can delegate.
1. The first instance connects to the linked server using the configured login mapping. Passing the user's identity through this additional hop requires an appropriate Kerberos delegation configuration.

Test which identity reaches each server. For login mappings and delegation requirements, see [Create linked servers](../../../relational-databases/linked-servers/create-linked-servers-sql-server-database-engine.md).

## Integrated authentication and sqlcmd

Use the `-E` option of **sqlcmd** for integrated authentication. Run the utility with access to the intended principal's Kerberos credential cache. For more information, see [sqlcmd utility](../../../tools/sqlcmd/sqlcmd-utility.md).

## Integrated authentication and bcp

Use the `-T` option of **bcp** for integrated authentication. Run the utility with access to the intended principal's Kerberos credential cache. Don't combine `-T` with `-U` or `-P`. For more information, see [bcp utility](../../../tools/bcp/bcp-utility.md).

<a id="supported-syntax-for-an-spn-registered-by-sql-server"></a>

## Specify a server SPN

For Transmission Control Protocol (TCP) connections, the driver generates the following SPN by default:

| Syntax | Description |
| --- | --- |
| `MSSQLSvc/<fqdn>:<port>` | Replace `<fqdn>` with the server's fully qualified domain name and `<port>` with its TCP port. |

<a id="authenticating-a-linux-or-macos-computer-with-active-directory"></a>
<a id="configure-kerberos"></a>

## Configure Kerberos for Active Directory

Configure the client's realm and domain mappings in `krb5.conf`, usually in `/etc`. To use another file, set `KRB5_CONFIG` to its path. The following example retains two domain-to-realm mappings; replace the example domains and realms with your deployment's values:

```ini
[libdefaults]
default_realm = YYYY.CORP.CONTOSO.COM
dns_lookup_realm = false
dns_lookup_kdc = true
ticket_lifetime = 24h
forwardable = yes

[domain_realm]
.yyyy.corp.contoso.com = YYYY.CORP.CONTOSO.COM
.zzzz.corp.contoso.com = ZZZZ.CORP.CONTOSO.COM
```

With `dns_lookup_kdc=true`, configure the client to use Domain Name System (DNS) servers that can resolve the realm's KDC records. Obtain a ticket with `kinit` using the intended principal, such as `alias@YYYY.CORP.CONTOSO.COM`, and enter its password at the prompt. Principal names are case-sensitive. The SQL Server instance must also grant the intended identity access.

Keep the client and KDC clocks synchronized, for example through Network Time Protocol (NTP). A forwarding-enabled ticket alone doesn't establish that the domain permits delegation through every server in a linked-server path.

If Kerberos authentication fails, the driver on Linux and macOS doesn't fall back to NT LAN Manager (NTLM) authentication.

For configuration and ticket-management details, see the [MIT Kerberos documentation](https://web.mit.edu/kerberos/krb5-latest/doc/).

## Related content

- [Programming guidelines](programming-guidelines.md).
- [Release notes for the Microsoft ODBC Driver for SQL Server on Linux and macOS](release-notes-odbc-sql-server-linux-mac.md).
- [Use Microsoft Entra ID with the ODBC driver](../using-azure-active-directory.md).
