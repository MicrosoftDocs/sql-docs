---
title: Configure ODBC Connections to Microsoft SQL
description: Choose DSNs, connection string keywords, authentication, encryption, routing, retries, and application settings for Microsoft ODBC Driver connections.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 11/09/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Configure ODBC connections to Microsoft SQL

Configure an ODBC connection by choosing where to store its settings, which database to connect to, how to authenticate, and how to protect the connection. Add routing, recovery, or application-specific settings only when your deployment needs them.

This guide explains those choices for the Microsoft ODBC Driver for SQL Server on Windows, Linux, and macOS. ODBC Driver 18 is the current major version; ODBC Driver 17 remains a supported target. The examples use Driver 18 and identify settings that differ in Driver 17. Use the [keyword and attribute reference](dsn-connection-string-attribute.md) to look up every accepted keyword, value, and platform.

<a id="choose-a-dsn-or-dsn-less-connection-string"></a>

## Choose a connection method

A connection string supplies `keyword=value;` pairs to the driver. A data source name (DSN) stores connection settings separately so the application can refer to them by name. Both methods use the same driver, authentication methods, and database permissions.

| Method | When to use it | What the application supplies |
| --- | --- | --- |
| DSN-less | Keep configuration with the application, such as in a deployment configuration or a container. You don't need to create a DSN on each client. | `Driver`, `Server`, `Database`, authentication, and other connection settings. The driver must still be installed. |
| User DSN | Configure a desktop application for one operating-system user, or an application that requires a named data source. | `DSN=<data_source_name>` and any settings supplied at connection time, including credentials. Only that user's applications can use the user DSN. |
| System DSN | Let applications running under different identities, such as a service account, use a machine-level configuration. | `DSN=<data_source_name>` and connection-time settings. The process must have access to the system DSN and its driver. |
| File DSN | Use an application that expects connection settings in a `.dsn` file. | `FileDSN=<file_path>` and connection-time settings. The file doesn't install the driver or make its paths portable between operating systems. |

Use one selection method per connection. Don't combine `DSN`, `Driver`, and `FileDSN` to try to change a DSN's driver. A service doesn't automatically use your interactive user's DSN.

In native applications, `SQLConnect` accepts a DSN and separate user ID and password arguments. `SQLDriverConnect` accepts a connection string, including DSN and DSN-less forms. Applications and frameworks that use ODBC determine which input they expose. Follow the application's configuration interface rather than assuming it accepts every ODBC keyword as a command-line option.

<a id="prerequisites"></a>

## Start with a connection string

Choose the connection form first, and then select authentication and any additional options in the following sections. These examples use SQL authentication. Don't retain `UID`, `PWD`, or `Authentication=SqlPassword` when switching to an incompatible authentication method.

Replace `<server>`, `<database>`, `<user_id>`, and `<password>` with your values. For Azure SQL Database, use `<server>.database.windows.net` as the hostname. The examples use Transmission Control Protocol (TCP) port `1433`; replace it with the port configured for your endpoint.

<a id="use-a-dsn-less-connection-string"></a>

### Connect without a DSN

Supply the installed driver name and the connection settings directly:

```text
Driver={ODBC Driver 18 for SQL Server};Server=tcp:<server>,1433;Database=<database>;UID=<user_id>;PWD=<password>;Authentication=SqlPassword;Encrypt=yes;TrustServerCertificate=no;
```

For Driver 17, replace the driver name with `ODBC Driver 17 for SQL Server`. The other settings in this example work with both major versions.

### Connect with a DSN

After you [create the DSN](#create-a-dsn), supply its name and the application's authentication settings:

```text
DSN=MSSQLTest;UID=<user_id>;PWD=<password>;Authentication=SqlPassword;Encrypt=yes;TrustServerCertificate=no;
```

The DSN selects the driver, server, and database. The example repeats the security settings explicitly so the application requests the same protection with either connection form.

Don't save real passwords in a DSN, file DSN, `odbc.ini`, source file, or shell command. Retrieve secrets through the application's credential-management mechanism and supply them at connection time. Treat any connection string containing a password, token, or key-store secret as sensitive data, including in logs and diagnostic captures.

<a id="connection-properties"></a>

## Select the driver, server, and database

These settings determine what the application loads and where it connects.

| Setting | When and how to use it |
| --- | --- |
| `Driver` | Select the installed driver by its registered name for a DSN-less connection. Driver 17 and Driver 18 have different names and can be installed side by side. Installing Driver 18 doesn't redirect a connection that explicitly selects Driver 17. |
| `Server=tcp:<server>,1433` | Use a known TCP endpoint and port. The comma separates the hostname from the port. Linux and macOS support TCP only. An explicit port avoids dependence on named-instance discovery. |
| `Server=<server>\<instance>` | Connect to a named SQL Server instance when instance discovery is available. On Linux and macOS, this requires Driver 17.4 and later versions, including Driver 18. If discovery is unavailable, obtain the instance's TCP port and use the explicit-port form. |
| `Database=<database>` | Select the initial database deliberately, particularly for a contained database user or an application that must not depend on the login's default database. This setting doesn't create the database or grant access. |
| `Address` or `Addr` | Maintain an application that uses a separate network address. Avoid specifying a conflicting `Server`; use one deliberate endpoint rather than relying on precedence between names. |
| `Network` or `Net` | Maintain a Windows configuration that explicitly selects a network library. Prefer the `tcp:` server prefix for a cross-platform TCP connection. Don't combine a protocol prefix with a conflicting network selection. |

A server alias, named instance, Domain Name System (DNS) hostname, and port aren't interchangeable. Use a DNS name that matches the certificate for an encrypted remote connection. An Internet Protocol (IP) address doesn't resolve a certificate-name mismatch.

For Azure SQL Database and other managed services, use the service's published endpoint. For an availability group, use its listener when you want [failover or read-only routing](#configure-routing-and-failover), rather than hard-coding a replica's hostname.

## Choose authentication

Authentication determines which identity connects. It doesn't grant that identity database permissions, and it doesn't replace the encryption settings.

| Method | Connection settings | When to use it and prerequisites |
| --- | --- | --- |
| SQL authentication | `Authentication=SqlPassword`, `UID`, and `PWD`. | The target enables SQL authentication and you have a SQL login or database user with a password. Both Driver 17 and Driver 18 support this form. `UID` is the SQL user name, not a Microsoft Entra ID account. |
| Windows integrated authentication | `Trusted_Connection=yes`; omit `UID` and `PWD`. | Use the process's Windows identity for a target configured for Windows authentication. On Linux and macOS, configure Kerberos and obtain a ticket for the application's identity. This isn't Microsoft Entra integrated authentication. |
| Microsoft Entra integrated authentication | `Authentication=ActiveDirectoryIntegrated`; omit SQL credentials. | Use an existing Windows or Kerberos identity in a Microsoft Entra configuration that supports integrated authentication. Linux and macOS require Driver 17.6 and later versions, including Driver 18. |
| Microsoft Entra interactive authentication | `Authentication=ActiveDirectoryInteractive`; omit `PWD`. | A person can complete sign-in and multifactor authentication. Supported on Windows, including Driver 17.1 and later versions. For Driver 17.1 through 17.7, supply the Microsoft Entra user name in `UID`. Driver 17.8 and later versions, including Driver 18, allow you to omit `UID`. Don't choose this method for an unattended service. |
| Microsoft Entra managed identity | `Authentication=ActiveDirectoryMsi`. | Run on a host that exposes a supported managed identity endpoint. A system-assigned identity doesn't require `UID`. A user-assigned identity requires `UID` with the identifier appropriate to that host. Available in Driver 17.3.1.1 and later versions, including Driver 18. |
| Microsoft Entra service principal | `Authentication=ActiveDirectoryServicePrincipal`; `UID` is the application client ID and `PWD` is its client secret. | An unattended application uses a Microsoft Entra application identity where managed identity isn't suitable. Available in Driver 17.7 and later versions, including Driver 18. Protect and rotate the client secret. |
| Application-supplied Microsoft Entra access token | Set the `SQL_COPT_SS_ACCESS_TOKEN` connection attribute before connecting. | The application acquires the token itself. This is an application programming interface (API) attribute, not a connection string keyword. Omit `UID`, `PWD`, `Trusted_Connection`, and `Authentication` from the connection configuration. |

Choose a Microsoft Entra method only when the target supports it and you've configured the identity's database access. A method supported by the driver isn't necessarily available on every SQL Server deployment or managed service. For setup, host-specific managed identity identifiers, and platform restrictions, see [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md) and [Use integrated authentication on Linux and macOS](linux-mac/using-integrated-authentication.md).

Don't combine `Trusted_Connection=yes` with an `Authentication` value. Don't place an access token in `PWD`. When changing a DSN's authentication method, remove incompatible credentials and application attributes as well as changing the keyword.

`ActiveDirectoryPassword` uses Microsoft Entra user names and passwords and is deprecated. Don't choose it for a new application. Prefer a method appropriate to an interactive user or a workload identity.

<a id="using-tlsssl"></a>

## Configure encryption and certificate validation

Use `Encrypt=yes;TrustServerCertificate=no` for a Transport Layer Security (TLS) connection that validates the server certificate with either Driver 17 or Driver 18. The client must trust the issuing certification authority, and the certificate must match the expected server name.

| Configuration | When to use it | Driver 17 and Driver 18 behavior |
| --- | --- | --- |
| `Encrypt=yes;TrustServerCertificate=no` | Require encryption and ordinary certificate validation. | Supported by both major versions. Driver 18 also accepts `mandatory` as a synonym for `yes`. |
| `Encrypt=strict` | Require Tabular Data Stream (TDS) 8.0 encryption against an endpoint that supports it. | Driver 18 only. The driver validates the certificate and ignores `TrustServerCertificate`. Driver 17 has no equivalent. |
| `Encrypt=yes;TrustServerCertificate=yes` | A deliberately approved exception that encrypts traffic without ordinary certificate validation. | Don't use it as the routine fix for a certificate error. It doesn't establish that the client is talking to the intended server. It doesn't disable validation in Strict mode. |
| `Encrypt=no` | Maintain a configuration that doesn't require full-session encryption from the client. | Not the baseline for remote connections. Driver 18 also accepts `optional`. The server can still require encryption. |

<a id="odbc-driver-18-and-newer"></a>
<a id="odbc-driver-17-and-older"></a>

Driver 18 defaults to requesting encryption. Driver 17 ordinarily defaults to not requesting it, but a nonempty `Authentication` setting or a supplied access token changes Driver 17's encryption default. Set `Encrypt` explicitly rather than relying on authentication-dependent defaults.

For ordinary non-Strict connections with `Encrypt=no` and `TrustServerCertificate=no`, a server-only encryption request causes Driver 18 to validate the certificate, but doesn't select normal certificate validation in Driver 17. Don't rely on server-required encryption alone to obtain the same validation behavior in both drivers. For the complete interaction with server settings, see the [encryption behavior tables](dsn-connection-string-attribute.md#encrypt).

### Choose the expected certificate identity

Most applications need only a trusted certificate and the correct `Server` name. Use the additional certificate keywords for a specific deployment requirement:

| Keyword | When to use it | Applicability and limits |
| --- | --- | --- |
| `HostnameInCertificate` | The deployment requires an expected certificate hostname different from the connection endpoint, such as a configured alias. | Driver 18 only. It doesn't establish trust in an untrusted issuing authority. It's ignored when `ServerCertificate` is used. |
| `ServerCertificate` | Match an explicitly distributed server certificate file instead of ordinary certificate-chain and hostname validation. | Driver 18.1 and later versions, with `Encrypt=strict` only. Use a Privacy Enhanced Mail (PEM) or Distinguished Encoding Rules (DER) certificate. The certificate must match exactly, and validity dates are still checked. Plan how to distribute a replacement before the server certificate rotates. |
| `ClientCertificate` and `ClientKey` | Use the specialized certificate authentication mechanism for loopback connections to SQL Server on Linux. | These aren't general substitutes for SQL or Microsoft Entra authentication, and they aren't the server-certificate trust settings. |

Resolve certificate-chain, hostname, and certificate-file problems instead of disabling validation. On Linux and macOS, also check the driver's supported OpenSSL dependency and the trust store used by that process. For more information, see [Connection encryption troubleshooting](connection-troubleshooting.md).

### Linux and macOS certificate trust

TLS on Linux and macOS uses OpenSSL. Obtain approved trust material and follow your platform's trust-store administration procedure. A certificate downloaded from an unauthenticated endpoint isn't sufficient evidence to trust it. Check [system requirements](linux-mac/system-requirements.md) for the libraries supported by your driver and platform.

The following historical platform inventory records OpenSSL minimum versions and default certificate trust-store locations. It doesn't establish current driver support for these platform versions. Confirm the OpenSSL version and trust-store configuration used by your installed driver and platform before relying on a listed location.

| Platform | Minimum OpenSSL version | Default certificate trust-store location |
| --- | --- | --- |
| Debian 10, 11, 12 | 1.1.1 | `/etc/ssl/certs` |
| Debian 9 | 1.1.0 | `/etc/ssl/certs` |
| Debian 8.71 | 1.0.1 | `/etc/ssl/certs` |
| OS X 10.11, macOS | 1.0.2 | `/usr/local/etc/openssl/certs` |
| Red Hat Enterprise Linux 9 | 3.0.1 | `/etc/pki/tls/cert.pem` |
| Red Hat Enterprise Linux 8 | 1.1.1 | `/etc/pki/tls/cert.pem` |
| Red Hat Enterprise Linux 7 | 1.0.1 | `/etc/pki/tls/cert.pem` |
| Red Hat Enterprise Linux 6 | 1.0.0-10 | `/etc/pki/tls/cert.pem` |
| SUSE Linux Enterprise 15 | 1.1.0 | `/etc/ssl/certs` |
| SUSE Linux Enterprise 11, 12 | 1.0.1 | `/etc/ssl/certs` |
| Ubuntu 22.04, 23.04 | 3.0.2 | `/etc/ssl/certs` |
| Ubuntu 20.04 | 1.1.1 | `/etc/ssl/certs` |
| Ubuntu 18.04 | 1.1.0 | `/etc/ssl/certs` |
| Ubuntu 16.04 | 1.0.2 | `/etc/ssl/certs` |
| Ubuntu 14.04 | 1.0.1 | `/etc/ssl/certs` |
| Alpine 3.17, 3.18 | 3.0.1 | `/etc/ssl/certs` |

## Configure routing and failover

Select these settings according to the target topology and workload. They don't create replicas, configure a listener, or grant access to a secondary.

| Setting | When to use it | Important distinction |
| --- | --- | --- |
| `MultiSubnetFailover=Yes` | Connect to an availability group listener, a failover cluster instance, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric. | The driver attempts TCP connections to resolved addresses in parallel. It also works when the name resolves to one address. It's supported by Driver 17 and Driver 18. Don't combine it with database mirroring's `Failover_Partner`. |
| `ApplicationIntent=ReadOnly` | Request a connection for a read-only workload, including read-only routing through a suitably configured availability group listener. | Specify the participating database and configure server-side routing. This setting isn't a substitute for read-only database permissions. The default is `ReadWrite`. |
| `TransparentNetworkIPResolution` | Maintain or diagnose the driver's legacy fallback across multiple resolved IP addresses. | Values are `Enabled` and `Disabled`; the default is `Enabled`. Don't treat this as another spelling of `MultiSubnetFailover`. |
| `IpAddressPreference` | Choose IPv4-first, IPv6-first, or operating-system address order for a deployment with a specific address-family requirement. | Driver 18.1 and later versions. Values are `IPv4First`, `IPv6First`, and `UsePlatformDefault`. This doesn't change DNS records or repair network routing. |
| `Failover_Partner` | Maintain an existing SQL Server database mirroring connection. | Windows only. It isn't the availability group listener option. Driver 18.7 and later versions also accept the alias `FailoverPartner`; Driver 17 uses `Failover_Partner`. |

For a read-only availability group connection, add `ApplicationIntent=ReadOnly;MultiSubnetFailover=Yes` to the appropriate authenticated connection string and use the listener in `Server`. A direct connection to an arbitrary secondary doesn't configure read-only routing.

After a failover, the application might need to open a new connection. For server prerequisites and application behavior, see [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md).

## Set timeouts, recovery, and retry behavior

Choose a setting for the operation you need to control. A login timeout, query timeout, idle-connection recovery policy, and command retry policy apply to different operations.

| Requirement | Setting | When and how to use it |
| --- | --- | --- |
| Limit connection establishment. | `SQL_ATTR_LOGIN_TIMEOUT`, or `ConnectTimeout` with Driver 18.7 and later versions. | In native applications, set the attribute before connecting. Driver 17 and earlier Driver 18 releases require the attribute rather than this keyword. An explicitly set attribute takes precedence over `ConnectTimeout`. |
| Limit statement execution. | `SQL_ATTR_QUERY_TIMEOUT` statement attribute. | Configure it through the application's API. `ConnectTimeout` doesn't set the query timeout. |
| Recover an eligible connection that dropped while idle. | `ConnectRetryCount` and `ConnectRetryInterval`. | Supported by current Driver 17 and Driver 18 releases. Defaults are one recovery attempt and 10 seconds between attempts. A count of `0` disables recovery. These settings don't retry the application's initial connection or replay its commands. |
| Detect an idle TCP connection that no longer responds. | `KeepAlive` and `KeepAliveInterval`. | Tune only for a measured network requirement. Defaults are 30 seconds and 1 second. Driver 17.4 introduced configuration support; connection string support requires Driver 17.8 and later versions, including Driver 18. Earlier supported releases use DSN or driver configuration. |
| Re-execute selected failed commands. | `RetryExec`. | Driver 18.1 and later versions. Define rules for specific errors and commands only after deciding whether repeating the operation is safe. This isn't idle-connection recovery. |

For example, `ConnectRetryCount=3;ConnectRetryInterval=2` selects up to three idle-recovery attempts with a 2-second interval. It doesn't make an initial login failure succeed or retry an interrupted write.

`ConnectRetryCount` accepts `0` through `255`, and `ConnectRetryInterval` accepts `1` through `60` seconds. With Driver 18.7 and later versions, `ConnectTimeout=30` requests a 30-second login timeout. Its default is 15 seconds; `0` requests an indefinite wait. Avoid an indefinite login wait when the application needs a bounded response time.

Implement a bounded application retry policy for transient initial connection failures. Don't retry incorrect credentials, missing driver registrations, or certificate-validation errors as though they were transient outages. For command retries, account for whether the server might already have completed a write before the connection failed.

For recovery prerequisites and error handling, see [Connection resiliency](connection-resiliency.md). For rule syntax, see [RetryExec](dsn-connection-string-attribute.md#retryexec). Keep connection and query timeout choices consistent with the total delay your application can accept.

<a id="adjusting-the-tcp-keep-alive-settings"></a>

### Linux and macOS keep-alive configuration

`KeepAlive` controls how often TCP sends a packet to check an idle connection. `KeepAliveInterval` controls the interval between retransmissions when no response arrives. Their defaults are 30 seconds and 1 second, respectively.

With Driver 17.4 and later versions, you can configure these values in the driver's section of `odbcinst.ini` or the DSN's section of `odbc.ini`. With a DSN connection, the DSN's settings take precedence over the driver's settings. Without a DSN, the driver uses its section in `odbcinst.ini`. If neither location specifies a value, the driver uses the default.

Driver 17.8 and later versions, including Driver 18, also accept `KeepAlive` and `KeepAliveInterval` in the connection string. Replace an existing keyword instead of appending a duplicate, and review [configuration precedence](#understand-configuration-precedence) when combining connection-time settings with a DSN.

## Configure application and data behavior

Use these options when the application needs the behavior, rather than adding them to every connection string.

| Setting | When to use it | Behavior and limits |
| --- | --- | --- |
| `APP` | Identify the application in server-side session information. | Use a stable application name. It isn't an authenticated identity or an authorization control. |
| `WSID` | Supply a workstation identifier when you need one for session identification. | Driver 18.7 and later versions also accept `WorkstationID`. Driver 17 uses `WSID`. Don't use a client-supplied name as proof of the caller's identity. |
| `MARS_Connection=Yes` | The application needs multiple active result sets (MARS) on one connection. | The default is `No`. Driver 18.7 and later versions also accept `MultipleActiveResultSets=Yes`. Driver 17 uses `MARS_Connection`. |
| `LongAsMax=Yes` | Send long character or binary parameters as **varchar(max)**, **nvarchar(max)**, or **varbinary(max)** instead of legacy large-object types. | Driver 18 only; the default is `No`. Check the application's parameter and schema expectations before changing it. |
| `GetDataExtensions=Yes` | Maintain an application that needs to retrieve column data with `SQLGetData` out of order. | Driver 18 only. This compatibility behavior can increase memory use and reduce performance. Prefer retrieving columns in ascending order in new applications. |
| `Language` | Select the session's server-message language. | Use a language installed on the server. It doesn't select the application's source-code encoding. |
| `AutoTranslate` | Control translation between client and server code pages for non-Unicode character data. | The default is `Yes`. Test with the application's actual character data before changing it; disabling translation can change how characters are interpreted. |
| `Regional` | Maintain an application that expects client regional formatting when converting currency, date, or time data to character data. | The default is `No`. Don't assume that changing output formatting makes those regional strings valid input parameters. |
| `AnsiNPW` | Maintain session behavior for null handling, character padding, and warnings. | The default is `Yes`. Review the corresponding Transact-SQL (T-SQL) session options and server-version behavior before changing it. |
| `QuotedId` | Control whether the session uses double quotation marks for identifiers. | The default is `Yes`. Change it only when the application's T-SQL requires different `QUOTED_IDENTIFIER` behavior. |
| `ConcatNullYieldsNull` | Control null-concatenation behavior where the server supports that session choice. | Driver 18.6 and later versions. Driver 17 doesn't recognize this keyword. |
| `UseFMTONLY` | Maintain a metadata-discovery path that requires `SET FMTONLY` instead of the driver's normal metadata discovery. | The default is `No`. It isn't necessary for an ordinary connection. |

Configure connection pooling through the Driver Manager or application's ODBC environment. Don't copy another provider's `Pooling` keyword into this driver's connection string. For native ODBC configuration, see [Driver Manager connection pooling](../../odbc/reference/develop-app/driver-manager-connection-pooling.md). For Windows-specific reuse behavior, see [Driver-aware connection pooling](windows/driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md).

### Configure specialized features

The following options need feature-specific setup beyond the connection string.

| Settings | When to use them | What else you need |
| --- | --- | --- |
| `ColumnEncryption=Enabled` | Read or write columns protected with Always Encrypted. | Access to the column encryption keys through a supported key store. Always Encrypted protects column values and is separate from network encryption. For enclave connections, use the feature-specific connection settings. |
| `KeystoreAuthentication`, `KeystorePrincipalId`, and `KeystoreSecret` | Authenticate the driver to a key store used for Always Encrypted. | The key-store identity and permissions. These credentials aren't the database login credentials. Protect key-store secrets as you would a password. |
| `ServerSPN` | Specify a service principal name (SPN) for a Kerberos deployment that requires an explicit value. | Current Driver 17 and Driver 18 releases support this setting on Windows, Linux, and macOS. Configure the Kerberos service registration to match the supplied SPN. An SPN isn't a TLS certificate hostname. |
| `FailoverPartnerSPN` | Maintain an explicit Kerberos SPN for a database mirroring partner. | Windows only, with the corresponding mirroring configuration. |
| `AttachDBFileName` | Use a SQL Server workflow that attaches a database file. | The matching `Database` name, server-accessible file, and required permissions. Don't use it to identify an existing database in Azure SQL Database. |
| `Replication=Yes` | Use a connection that deliberately needs replication-agent behavior. | Driver 17.8 and later versions, including Driver 18. It changes behavior for objects marked `NOT FOR REPLICATION`, including triggers; it isn't a general performance option. |
| `QueryLog_On`, `QueryLogFile`, `QueryLogTime`, `StatsLog_On`, and `StatsLogFile` | Diagnose an existing Windows application through driver query or performance logs. | Windows only; these connection settings require a DSN. Set the slow-query threshold with `QueryLogTime`, in milliseconds. Use a controlled logging destination, appropriate permissions, and a retention policy. Don't treat diagnostic logs as free of sensitive data. |

For setup and limits, see [Always Encrypted](using-always-encrypted-with-the-odbc-driver.md), [Kerberos SPNs](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md), and the [keyword reference](dsn-connection-string-attribute.md). Some native capabilities, such as distributed transaction enlistment, require API attributes rather than a connection string keyword.

<a id="create-the-user-dsn"></a>

## Create a DSN

Create a DSN only when you choose a DSN-based connection. A DSN stores configuration; saving it doesn't establish that the application can authenticate or run a query.

### Windows user DSN

The following procedure creates a user DSN for the SQL-authentication example. For a service, deliberately choose **System DSN** instead and arrange access under the service identity.

1. Open the ODBC Data Source Administrator that matches your application's architecture.

1. On **User DSN**, select **Add**.

1. Choose **ODBC Driver 18 for SQL Server** or **ODBC Driver 17 for SQL Server**. Select **Finish** to open the driver wizard.

1. Name the DSN `MSSQLTest`. Enter `tcp:<server>,1433` as the server, and select **Next**.

1. Select SQL authentication. Clear **Login ID** and **Password**, and select **Next**.

1. Enable **Change the default database to**. Type `<database>` without opening the database list, and select **Next**.

1. For Driver 18, select **Mandatory** connection encryption. For Driver 17, select **Use strong encryption for data**. Leave **Trust server certificate** unselected.

1. Select **Finish** and review the configuration summary.

1. Select **OK** to save the DSN without running the wizard's connection test. Confirm that `MSSQLTest` appears on **User DSN**.

The application supplies the SQL credentials at connection time. For other authentication methods and wizard fields, see [ODBC Data Source Administrator DSN options](windows/odbc-administrator-dsn-creation.md).

<a id="connecting-from-linux-or-macos"></a>
<a id="connect-with-odbc-on-linux-or-macos"></a>

### Linux or macOS user DSN

Run `odbcinst -j` to find the **USER DATA SOURCES** and **SYSTEM DATA SOURCES** paths for your unixODBC installation. Add the following section to the user file, replacing the server and database placeholders:

```ini
[MSSQLTest]
Driver = ODBC Driver 18 for SQL Server
Server = tcp:<server>,1433
Database = <database>
Authentication = SqlPassword
Encrypt = yes
TrustServerCertificate = no
```

For Driver 17, change the driver name. Don't store `PWD` in this file. The driver registration in `odbcinst.ini` and the DSN in `odbc.ini` serve different purposes: one locates the driver library, and the other names connection settings.

For a system DSN, add the same settings to the reported **SYSTEM DATA SOURCES** file with administrative privileges. For a service, arrange access under the service identity.

The `Driver` value can be the registered driver name or the path to the driver library specified in the installation template. In `odbc.ini`, use the registered name without connection-string braces. Specify the server port in `Server`; `Port` isn't a valid DSN keyword for this driver on Linux or macOS.

Alternatively, save the DSN section in a template file and register a user DSN with the following command:

```bash
odbcinst -i -s -h -f <template_file>
```

Replace `<template_file>` with the template's path. For `.ini` file and registration details, see the [unixODBC documentation](http://www.unixodbc.org/odbcinst.html). Test the connection through the application that uses the DSN; registration alone doesn't establish a database connection.

### File DSN

Use `FileDSN=<file_path>` when the application expects a file data source. The client still needs an installed driver that can interpret the settings. Keep credentials outside the file.

`SaveFile` requests that connection settings be saved to a file after a successful connection. Use it only when the application deliberately creates a file DSN. For file contents and merge rules, see the [SQLDriverConnect reference](../../odbc/reference/syntax/sqldriverconnect-function.md).

## Understand configuration precedence

An application can receive settings from a DSN, its connection string, and API attributes. Review all three when the effective connection differs from the string you expect.

| Source or syntax | Rule |
| --- | --- |
| Keyword spelling | The driver matches keyword names without regard to case. Accepted values are keyword-specific; don't assume every Boolean option accepts `true` and `false`. |
| Repeated keyword | When parsing driver options, Driver 17 and Driver 18 retain the first occurrence of a recognized keyword. Aliases for the same option don't provide a separate override. This rule doesn't govern the Driver Manager's earlier driver or DSN selection: unixODBC uses the last occurrence of a repeated `Driver` or `DSN` value at that stage. Replace existing values instead of appending duplicates, and specify the selection keyword only once. |
| Connection string versus DSN | Explicit connection string values normally override corresponding DSN fields. The driver's `RegPrecedence` DSN setting can reverse this precedence. Inspect it when an existing DSN behaves unexpectedly. |
| API attributes | Attributes set by the application can change the effective configuration. Check the specific attribute's contract instead of assuming every attribute uses the same merge rule. |
| Braced values | Enclose a value containing a semicolon in braces, and double a closing brace within the value. For example, `Database={database;name}` supplies one database name. Shell or programming-language quoting is separate from ODBC escaping. |

Keep authentication and security settings consistent across the DSN and application. When you change drivers, update the DSN's selected driver or the DSN-less `Driver` value as appropriate. For migration checks, see [Major version differences](major-version-differences.md).

<a id="create-the-verification-application"></a>
<a id="build-the-application"></a>
<a id="set-the-connection-string"></a>
<a id="verify-the-dsn-connection"></a>

## Check the connection in your application

Test under the application's actual operating-system identity and deployment configuration. A successful DSN wizard test doesn't prove that a service can see the same DSN or acquire the same credentials.

1. Connect with the chosen DSN or DSN-less string and the intended authentication method.
1. After the connection succeeds, confirm which driver and version the application loads. In native code, use `SQLGetInfo` with `SQL_DRIVER_NAME` and `SQL_DRIVER_VER`.
1. Run a query in the intended database and check its result. For a complete application example, see [Connect to and query a database with C++](cpp-code-example-app-connect-access-sql-db.md).
1. Check encryption and the application's required behavior, such as read-only routing or recovery. Test each connection form you deploy independently.
1. Capture failures without exposing credentials. For SQLSTATE values, native error codes, and protected diagnostic collection, see [ODBC diagnostics](diagnostics.md).

If a connection fails, check driver registration and DSN visibility before changing authentication. Check the endpoint and reachability before changing retry settings. For a certificate error, fix the trust or name configuration rather than setting `TrustServerCertificate=yes` to make the error disappear.

## Related content

- [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md).
- [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md).
- [Connection encryption troubleshooting](connection-troubleshooting.md).
- [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md).
- [Connection resiliency](connection-resiliency.md).
