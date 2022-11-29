---
title: "SqlClient troubleshooting guide"
description: "Page that provides resolutions to commonly observed problems."
author: David-Engel
ms.author: v-davidengel
ms.date: "03/03/2021"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
  - "vb"
---
# SqlClient troubleshooting guide

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

## Exceptions when connecting to SQL Server

There are various reasons why connection can fail to be established. Below are some troubleshooting tips that can be used as a guide to analyze and solve many of the problems.

### Unable to load native SNI (Server Name Indication) library

#### Issues in .NET Framework applications

Stacktrace observed:

```log
TypeInitializationException: The type initializer for 'Microsoft.Data.SqlClient.SNILoadHandle' threw an exception.
DllNotFoundException: Unable to load DLL 'Microsoft.Data.SqlClient.SNI.x64.dll': The specified module could not be found. (Exception from HRESULT: 0x8007007E)
```

```log
TypeInitializationException: The type initializer for 'Microsoft.Data.SqlClient.SNILoadHandle' threw an exception.
DllNotFoundException: Unable to load DLL 'Microsoft.Data.SqlClient.SNI.x86.dll': The specified module could not be found. (Exception from HRESULT: 0x8007007E)
```

SNI is the native C++ library that SqlClient depends on for various network operations when running on Windows. In .NET Framework applications that are built with the MSBuild Project SDK, native DLLs aren't managed with restore commands. So a ".targets" file is included in the "Microsoft.Data.SqlClient.SNI" NuGet package that defines the necessary "Copy" operations.

The included ".targets" file is auto-referenced when a direct dependency is made to the "Microsoft.Data.SqlClient" library. In scenarios where a transitive (indirect) reference is made, this ".targets" file should be manually referenced to ensure "Copy" operations can execute when necessary.

**Recommended Solution:** Make sure the ".targets" file is referenced in the application's ".csproj" file to ensure "Copy" operations are executed.

These targets cover Microsoft's well-known and commonly used targets only. If an external tool or application defines custom targets to copy binaries, new targets must be defined by tool maintainers to ensure native SNI DLLs are copied along-side the Microsoft.Data.SqlClient.dll binaries and are available when executing client applications.

#### Issues in .NET Core applications

Stacktrace observed:

```log
System.TypeInitializationException: The type initializer for 'Microsoft.Data.SqlClient.TdsParser' threw an exception.
---> System.TypeInitializationException: The type initializer for 'Microsoft.Data.SqlClient.SNILoadHandle' threw an exception.
---> System.DllNotFoundException: Unable to load shared library 'Microsoft.Data.SqlClient.SNI.dll' or one of its dependencies.
```

> [!NOTE]
> This error may occur on Windows applications only. If it occurs in a Unix environment, you must ensure your application is built to appropriately target a Unix runtime and not for Windows.

SNI is the native C++ library that SqlClient depends on for various network operations when running on Windows. Microsoft.Data.SqlClient doesn't manage loading/unloading of this library in .NET Core.

**Recommended Solution:** Ensure "Execute" permissions are granted on the filesystem where native runtime libraries are loaded in the .NET Core process. If that doesn't solve the issue, you can file an issue in the [dotnet/runtime](https://github.com/dotnet/runtime) repository for further support.

#### Native SNI (pdb not found) errors

Stacktrace observed:

```log
An assembly specified in the application dependencies manifest (sql2csv.deps.json) was not found:
  package: 'Microsoft.Data.SqlClient.SNI.runtime', version: '2.0.0'
  path: 'runtimes/win-x64/native/Microsoft.Data.SqlClient.SNI.pdb'
```

**Recommended Solution:** Ensure client application references minimum [v2.1.0](https://www.nuget.org/packages/Microsoft.Data.SqlClient/2.1.0) version of Microsoft.Data.SqlClient package. When using EF Core, add a reference to this package version of Microsoft.Data.SqlClient directly to override dependency.

### Hostname resolution errors

Stacktrace observed:

```log
Microsoft.Data.SqlClient.SqlException (0x80131904): A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible.
Verify that the instance name is correct and that SQL Server is configured to allow remote connections.
(provider: TCP Provider, error: 0 - No such host is known.)
```

```log
Microsoft.Data.SqlClient.SqlException (0x80131904): A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible.
Verify that the instance name is correct and that SQL Server is configured to allow remote connections.
(provider: TCP Provider, error: 35 - An internal exception was caught)
```

```log
Microsoft.Data.SqlClient.SqlException (0x80131904): A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible.
Verify that the instance name is correct and that SQL Server is configured to allow remote connections.
(provider: TCP Provider, error: 35 - An internal exception was caught)
 ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException (00000005, 0xFFFDFFFF): Name does not resolve
```

#### Possible reasons

- TCP/Named Pipes Protocol isn't enabled on SQL Server

  **Recommended Solution:** Enable the TCP/Named Pipes Protocol on the SQL Server instance from the SQL Server Configuration Manager console.

- Hostname not known

  **Recommended Solution:** Ensure the hostname resolves to the Server's IP address from the client where the connection is being initiated.


### Login-phase errors

Stacktraces observed:

```log
Microsoft.Data.SqlClient.SqlException (0x80131904): A connection was successfully established with the server, but then an error occurred during the pre-login handshake.
(provider: SSL Provider, error: 31 - Encryption(ssl/tls) handshake failed)
System.IO.EndOfStreamException: End of stream reached
```

```log
A connection was successfully established with the server, but then an error occurred during the login process.
(provider: SSL Provider, error: 0 - The target principal name is incorrect.)
```

```log
Microsoft.Data.SqlClient.SqlException (0x80131904): Connection Timeout Expired. The timeout period elapsed during the post-login phase. The connection could have timed out while waiting for server to complete the login process and respond; Or it could have timed out while attempting to create multiple active connections.
The duration spent while attempting to connect to this server was - [Pre-Login] initialization=837; handshake=394; [Login] initialization=3; authentication=15; [Post-Login] complete=1027;
---> System.ComponentModel.Win32Exception (258): Unknown error 258
at Microsoft.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action1 wrapCloseInAction)
```

#### Possible reasons and solutions

- SQL Server doesn't support TLS 1.2

  This error typically occurs in client environments like docker image containers, Unix clients, or Windows clients where TLS 1.2 is the minimum supported TLS protocol.

  **Recommended Solution:** Install the latest updates on supported versions of SQL Server<sup>1</sup> and ensure the TLS 1.2 protocol is enabled on the server.

  _<sup>1</sup> View [SqlClient driver support lifecycle](sqlclient-driver-support-lifecycle.md) for the list of supported SQL Server versions with different versions of Microsoft.Data.SqlClient._

  **Insecure solution:** Configure TLS/SSL settings in the docker image/client environment to connect with TLS 1.0.

  ```docker
  MinProtocol = TLSv1
  CipherString = DEFAULT@SECLEVEL=1
  ```

  > [!NOTE]
  > When connecting with Microsoft.Data.SqlClient v2.0+ from a Windows/Linux environment with TLS 1.0 or TLS 1.1, a security warning message will be thrown if the target SQL Server and client cannot negotiate a minimum of TLS version 1.2 when establishing the connection:
  `Security Warning: The negotiated <TLS1.0 | TLS1.1> is an insecure protocol and is supported for backward compatibility only. The recommended protocol version is TLS 1.2 and later.`

- SQL Server enforced encryption

  If the target Server is an Azure SQL instance or an on-premises SQL Server with the "Force Encryption" property turned on, an encrypted connection will be made, for which the client must establish trust with the server.

  **Recommended Solution:** There are two available options to fix this issue:

    1. Install the target SQL Server's TLS/SSL certificate in the client environment. It will be validated if encryption is needed.
    2. Set the "TrustServerCertificate=true" property in the connection string.

  **Insecure solution:** Disable the "Force Encryption" setting on SQL Server.

- TLS/SSL Certificates not signed with SHA-256 or above.

  **Recommended Solution:** Generate a new TLS/SSL Certificate for the server whose hash is signed with at-least the SHA-256 hashing algorithm.

- Tightly restricted cipher suites on Linux with .NET 5+

  .NET 5 introduced a breaking change for Linux clients, where a tightly restricted list of permitted cipher suites is used by default. You may need to expand the default cipher suite list to accept legacy clients (or to contact legacy servers) by either specify a `CipherSuitePolicy` value or changing the _OpenSSL_ configuration file.
  
  Read more on [Default TLS cipher suites for .NET on Linux
](/dotnet/core/compatibility/cryptography/5.0/default-cipher-suites-for-tls-on-linux) for recommended action.

### Connection Pool exhaustion errors

Stacktrace observed:

```log
System.InvalidOperationException: Timeout expired. The timeout period elapsed prior to obtaining a connection from the pool.
This may have occurred because all pooled connections were in use and max pool size was reached.
```

#### Possible reasons and solutions

Client application is opening more connections than the connection pool can hold active at a given time.

**Recommended Solution:** Configure "Max Pool Size" connection property to a higher value and close unused connections in timely manner.

## Contact Support

If this guide doesn't solve your connectivity issues, you may view existing issues in the [dotnet/sqlclient](https://github.com/dotnet/SqlClient) repository and open a new issue if needed.
