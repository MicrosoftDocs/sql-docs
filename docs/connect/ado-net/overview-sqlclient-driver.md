---
title: ADO.NET Architecture with Microsoft.Data.SqlClient
description: Learn how Microsoft.Data.SqlClient implements ADO.NET and choose between connected, disconnected, provider-independent, and SQL Server-specific data access.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 09/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# ADO.NET architecture with Microsoft.Data.SqlClient

ADO.NET is the data access model built into .NET. Microsoft.Data.SqlClient implements that model for SQL Server-compatible databases and adds SQL Server-specific capabilities.

The [Microsoft.Data.SqlClient landing page](microsoft-ado-net-sql-server.md) provides the driver feature map and production baseline. This article explains how the driver fits into ADO.NET.

## ADO.NET abstractions and SqlClient types

ADO.NET defines provider-independent abstractions in the <xref:System.Data.Common> namespace. Microsoft.Data.SqlClient supplies concrete implementations.

| ADO.NET abstraction | SqlClient type | Purpose |
| --- | --- | --- |
| <xref:System.Data.Common.DbConnection> | <xref:Microsoft.Data.SqlClient.SqlConnection> | Opens a logical connection to one database. |
| <xref:System.Data.Common.DbCommand> | <xref:Microsoft.Data.SqlClient.SqlCommand> | Executes SQL text or a stored procedure. |
| <xref:System.Data.Common.DbParameter> | <xref:Microsoft.Data.SqlClient.SqlParameter> | Sends a typed value separately from command text. |
| <xref:System.Data.Common.DbDataReader> | <xref:Microsoft.Data.SqlClient.SqlDataReader> | Streams result rows forward from the server. |
| <xref:System.Data.Common.DbTransaction> | <xref:Microsoft.Data.SqlClient.SqlTransaction> | Groups commands into one atomic transaction. |
| <xref:System.Data.Common.DbDataAdapter> | <xref:Microsoft.Data.SqlClient.SqlDataAdapter> | Fills and updates disconnected `DataSet` and `DataTable` objects. |
| <xref:System.Data.Common.DbBatch> | <xref:Microsoft.Data.SqlClient.SqlBatch> | Sends multiple commands as one batch on supported target frameworks. |

Program against the ADO.NET abstractions when a library must support multiple database providers. Use the SqlClient types when an application targets SQL Server and needs provider-specific features.

## Connected data access

Connected access keeps a connection available while a command runs and while the application reads its results.

A common request follows this sequence:

1. Create a `SqlConnection` from a connection string.
1. Open the connection.
1. Create a `SqlCommand` and add `SqlParameter` values.
1. Execute the command.
1. Process a scalar value, affected-row count, or `SqlDataReader`.
1. Dispose the reader, command, and connection.

Use connected access for most web APIs, services, workers, and command-line applications. `SqlDataReader` streams rows and usually uses less memory than loading the entire result into a `DataSet`.

Opening and disposing a `SqlConnection` for each unit of work is the normal pattern. Connection pooling reuses the underlying physical connection. Don't keep one global connection open for the lifetime of an application.

## Disconnected data access

`SqlDataAdapter` transfers data between SQL Server and an in-memory `DataSet` or `DataTable`. The application can close the connection while it reads or changes the in-memory data, then reconnect to submit updates.

Use disconnected access when you need:

- Data binding to `DataSet` or `DataTable`.
- In-memory relations and constraints.
- Offline edits that are reconciled later.
- Compatibility with an existing application built around DataAdapters.

For new request-oriented services, start with `SqlCommand` and `SqlDataReader` unless you need the disconnected object model.

## SQL Server-specific features

Microsoft.Data.SqlClient adds APIs and connection behavior that aren't part of the provider-independent ADO.NET contract:

- Microsoft Entra authentication and access token callbacks.
- TDS 8.0 strict encryption and SQL Server certificate options.
- Always Encrypted and secure enclaves.
- `SqlBulkCopy` for high-throughput data loading.
- Table-valued parameters.
- SQL Server data types, including JSON and vector types.
- Configurable retry logic.
- SQL Server diagnostics, statistics, and counters.
- Availability group and failover connection options.

Using these features ties that code to Microsoft.Data.SqlClient. Keep provider-specific code behind a data access boundary if the rest of the application must remain provider-independent.

## Object lifetime and concurrency

Dispose connections, commands, readers, transactions, and bulk copy objects promptly. Use `await using` with asynchronous code when a type implements `IAsyncDisposable`.

`SqlConnection`, `SqlCommand`, `SqlDataReader`, and `SqlTransaction` don't support concurrent use from multiple threads. Give each concurrent operation its own connection and rely on connection pooling. Multiple Active Result Sets (MARS) permits more than one active result set on a connection, but it doesn't make SqlClient objects thread-safe.

## Packages and namespaces

Install the `Microsoft.Data.SqlClient` NuGet package and import the `Microsoft.Data.SqlClient` namespace. The driver ships independently from .NET, so its feature, release, and support schedules differ from the .NET runtime.

`System.Data.SqlClient` is the older provider. Use Microsoft.Data.SqlClient for new development. For an existing application, follow [Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient](migrate-system-data-sql-client-to-microsoft-data-sql-client.md).

## Related content

- [Microsoft.Data.SqlClient namespace and compatibility](introduction-microsoft-data-sqlclient-namespace.md)
- [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md)
- [Commands and parameters](commands-parameters.md)
- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Connection pooling](sql-server-connection-pooling.md)
- [Microsoft.Data.SqlClient API reference](/dotnet/api/microsoft.data.sqlclient)
