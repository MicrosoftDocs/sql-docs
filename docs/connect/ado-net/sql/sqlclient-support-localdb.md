---
title: LocalDB and Local Development with Microsoft.Data.SqlClient
description: Install and connect to Windows LocalDB, choose automatic or named instances, attach database files, and understand local-development limitations.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
dev_langs:
  - csharp
ai-usage: ai-assisted
---
# LocalDB and local development with SqlClient

<a id="sqlclient-support-for-localdb"></a>

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

SQL Server Express LocalDB is a lightweight SQL Server engine for local Windows development. It runs under your Windows user identity and starts on demand. LocalDB isn't a remote database service and doesn't run on Linux or macOS.

For cross-platform development, use a supported [SQL Server container](../../../linux/install-upgrade/quickstart-install-docker.md) or a remote SQL endpoint. Installing the `Microsoft.Data.SqlClient` NuGet package installs the client driver, not LocalDB.

## Install LocalDB

Install LocalDB through SQL Server Express setup or the Visual Studio Installer. In Visual Studio, select an appropriate data-development workload or the **SQL Server Express LocalDB** individual component. See [SQL Server Express LocalDB installation](../../../database-engine/configure-windows/sql-server-express-localdb.md#installation-media) for current downloads and installer options.

The LocalDB installation supplies the engine, `SQLUserInstance.dll`, and the `SqlLocalDB.exe` management utility. If `SqlLocalDB` isn't on `PATH`, run it from the installed SQL Server tools directory.

List your installed versions and instances:

```powershell
SqlLocalDB versions
SqlLocalDB info
```

## Connect to the automatic instance

The automatic instance is named `MSSQLLocalDB` and belongs to the current Windows user. Use Windows integrated authentication. LocalDB doesn't support Microsoft Entra authentication.

For a .NET console application with `Microsoft.Data.SqlClient` installed:

```csharp
using Microsoft.Data.SqlClient;

string connectionString =
    @"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=true;Encrypt=true;TrustServerCertificate=true;";

using var connection = new SqlConnection(connectionString);
await connection.OpenAsync();
using var command = new SqlCommand("SELECT DB_NAME();", connection);
Console.WriteLine(await command.ExecuteScalarAsync());
```

The query prints `master`. The first connection can take longer because LocalDB creates or starts the instance. If startup exceeds the connection timeout, inspect instance status, wait for startup to complete, and try again.

This example permits the local development certificate with `TrustServerCertificate=true`. Keep this exception confined to local development. For a remote SQL endpoint, use a verifiable certificate and `TrustServerCertificate=false`; see [Encryption and certificate validation](../encryption-and-certificate-validation.md).

### Backslashes in connection strings

The connection-string value contains one backslash between `(localdb)` and the instance name. Escaping depends on where you write the value:

| Location | Server value |
| --- | --- |
| Plain text, Extensible Markup Language (XML) configuration, or a PowerShell single-quoted string. | `(localdb)\MSSQLLocalDB` |
| C# verbatim string. | `@"(localdb)\MSSQLLocalDB"` |
| C# regular string or a JavaScript Object Notation (JSON) string. | `"(localdb)\\MSSQLLocalDB"` |

Don't copy doubled C# backslashes into a plain-text connection string.

## Named and shared instances

Use a named instance when you want a separate local engine instance for an application. Create it before connecting:

```powershell
SqlLocalDB create SqlClientDev
SqlLocalDB start SqlClientDev
SqlLocalDB info SqlClientDev
```

Connect with this plain-text connection string:

```text
Server=(localdb)\SqlClientDev;Database=master;Integrated Security=true;Encrypt=true;TrustServerCertificate=true;
```

Different Windows users can have private instances with the same name. The name alone doesn't identify another user's instance.

A shared instance has an alias visible to other users on the same computer. An administrator must configure sharing, and other users need an appropriate Windows or SQL login. Sharing doesn't grant database permissions automatically. Use the shared namespace:

```text
Server=(localdb)\.\<shared_instance>;Database=<database>;Integrated Security=true;Encrypt=true;TrustServerCertificate=true;
```

Only share a development instance intentionally. Review local file access and database permissions. See [Shared instances of LocalDB](../../../database-engine/configure-windows/sql-server-express-localdb.md#shared-instances-of-localdb).

## Remarks

Use `AttachDBFilename` to attach an existing database file that the instance owner can access. Also, specify a database name:

```text
Server=(localdb)\MSSQLLocalDB;Database=<database>;AttachDBFilename=C:\Data\<database>.mdf;Integrated Security=true;Encrypt=true;TrustServerCertificate=true;
```

Use an absolute file path or an appropriately configured `|DataDirectory|` value. `AttachDBFilename` doesn't create a missing database file. The engine must support the database file's version, and another instance can't already have the file open.

If you omit `Database` when attaching a file, the database is removed from the LocalDB instance when the application closes. Specify `Database` when you want a persistent database registration.

`User Instance=true` isn't valid for LocalDB. Don't use `MultiSubnetFailover=true` with LocalDB; its local named-pipe connection isn't a Transmission Control Protocol (TCP) failover endpoint.

Protect the database and log files with Windows file permissions. A person who can copy an unprotected database file can potentially attach it to an instance they control. SQL permissions don't replace file-system protection.

## Programmatically create a named instance

[!INCLUDE [dotnet-framework-only](../../../includes/products/applies-plain/dotnet-framework-only.md)]

For a .NET Framework application, declare a named instance and its installed LocalDB engine version in the `system.data.localdb` section of `app.config`. Register the `Microsoft.Data.LocalDBConfigurationSection` configuration-section type from the `Microsoft.Data.SqlClient` assembly, and then add the instance under `localdbinstances`.

Match the instance name in `Server=(localdb)\<instance>` to the configuration entry, and use a LocalDB engine version installed on the computer. Don't copy an old driver assembly version or engine version into a new application.

Modern .NET doesn't use this configuration section to create instances. Use `SqlLocalDB` during developer setup or the [LocalDB management API](../../../relational-databases/sql-server-express-localdb-reference.md) when an application must manage an instance.

## Limitations

LocalDB runs with the instance owner's permissions. It's intended for local development, not a shared production service.

- LocalDB is Windows-only. A managed-networking LocalDB connection on a non-Windows platform throws `PlatformNotSupportedException`.
- Remote management isn't supported.
- FILESTREAM and merge-replication subscriber use aren't supported.
- Service Broker supports local queues only.
- Database size and resource limits follow the installed SQL Server Express engine, not the SqlClient package version.

| Engine | Maximum compute per instance | Maximum buffer pool in megabytes (MB) | Maximum relational database size in gigabytes (GB) |
| --- | --- | --- | --- |
| SQL Server 2022 Express LocalDB. | Lesser of 1 socket or 4 cores. | 1,410 MB. | 10 GB. |
| SQL Server 2025 Express LocalDB. | Lesser of 1 socket or 4 cores. | 1,410 MB. | 50 GB. |

See the edition limits for [SQL Server 2022](../../../sql-server/editions-and-components-of-sql-server-2022.md) and [SQL Server 2025](../../../sql-server/editions-and-components-of-sql-server-2025.md). LocalDB testing doesn't exercise Microsoft Entra authentication, remote Transport Layer Security (TLS) certificate validation, availability group failover, or cloud service limitations. Exercise those separately against the intended deployment.

## Related content

- [SQL Server features and ADO.NET](sql-server-features-adonet.md)
- [SqlLocalDB utility](../../../tools/sqllocaldb-utility.md)
- [SQL Server and Windows authentication](authentication-sql-server.md)
- [Security best practices](application-security-scenarios-sql-server.md)
