---
title: Microsoft.Data.SqlClient for SQL Server
description: Microsoft.Data.SqlClient is the .NET data provider for connecting applications to SQL Server, Azure SQL, and SQL database in Microsoft Fabric.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 07/29/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---
# Microsoft.Data.SqlClient for SQL Server

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Microsoft.Data.SqlClient is the supported .NET data provider for SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, and SQL database in Microsoft Fabric. It's distributed as a NuGet package, evolves independently of the .NET runtime, and replaces <xref:System.Data.SqlClient> for new development. Use it to open connections, execute commands, process results, manage transactions, bulk load data, and use SQL Server-specific features from .NET applications.

## Choose your starting point

- To set up a project and run your first query, start with [Getting started with the SqlClient driver](get-started-sqlclient-driver.md).
- To add the driver to a .NET project, go to [Download Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md).
- To connect to Azure SQL with passwordless authentication, start with [Microsoft Entra authentication](sql/azure-active-directory-authentication.md) and [Connection strings](connection-strings.md).
- To make an existing application resilient to transient failures, go to [Configurable retry logic](configurable-retry-logic.md) and [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md).
- To move large data sets efficiently, go to [Bulk copy operations](sql/bulk-copy-operations-sql-server.md).
- To migrate from `System.Data.SqlClient`, start with [Introduction to the Microsoft.Data.SqlClient namespace](introduction-microsoft-data-sqlclient-namespace.md).
- To diagnose a connection or query problem, go to [SqlClient troubleshooting guide](sqlclient-troubleshooting-guide.md) and [Enable event source tracing](enable-eventsource-tracing.md).

## Production baseline for Azure SQL

Use this snippet as a starting point for a production-oriented Azure SQL data access path. It reads the server and database names from <xref:Microsoft.Extensions.Configuration.IConfiguration>, so the values come from whatever configuration providers the host wires up (`appsettings.json`, environment variables, Azure App Configuration, Key Vault-backed settings, and so on). The configuration combines Transport Layer Security (TLS), managed identity, idle-connection resiliency, initial-connect retry through configurable retry logic (CRL) with structured logging, command-level retry for transient errors that fire mid-query, and fast failover-group recovery.

For higher security and to support configuration across environments, keep connection information outside your code. In production, store connection information in your application's configuration system, and use Azure Key Vault for sensitive values. For more information, see [Protect connection information](protecting-connection-information.md).

The C# snippet in this article omits `using` directives and class wrappers for brevity.

```csharp
public static void QuerySalesWithResilience(IConfiguration config, ILogger logger)
{
    string server = config["Sql:Server"]
        ?? throw new InvalidOperationException("Missing configuration value 'Sql:Server'.");
    string database = config["Sql:Database"]
        ?? throw new InvalidOperationException("Missing configuration value 'Sql:Database'.");

    var builder = new SqlConnectionStringBuilder
    {
        DataSource = server,
        InitialCatalog = database,
        Authentication = SqlAuthenticationMethod.ActiveDirectoryManagedIdentity,
        Encrypt = SqlConnectionEncryptOption.Strict, // TDS 8.0 encryption (SqlClient 5.0 and later versions; server must support it)
        ConnectTimeout = 30,                         // per-attempt connect timeout in seconds
        // Idle connection resiliency: reconnect a dropped idle connection after Open() succeeded.
        // This is separate from the initial-connect retry provider defined next.
        ConnectRetryCount = 3,
        ConnectRetryInterval = 10,
        MultiSubnetFailover = true,                  // recommended for any target; enables parallel connect
        // ApplicationIntent = ApplicationIntent.ReadOnly, // uncomment to route to a readable secondary
    };

    // Retry the initial Open() on transient failures with exponential backoff and jitter.
    // TransientErrors is null, so the provider uses the driver's built-in transient error list.
    var openRetry = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(
        new SqlRetryLogicOption
        {
            NumberOfTries = 5,
            DeltaTime = TimeSpan.FromSeconds(3),
            MaxTimeInterval = TimeSpan.FromSeconds(60),
        });
    openRetry.Retrying += (_, args) =>
    {
        Exception last = args.Exceptions[^1];
        logger.LogWarning(
            last,
            "Retrying SQL connection to {Server}/{Database} (attempt {Attempt}) after {Delay}",
            server, database, args.RetryCount, args.Delay);
    };

    // Retry commands that hit deadlocks, lock timeouts, or common Azure SQL transient errors
    // mid-query on an established connection. Only attach this provider to commands whose
    // effect is safe to repeat.
    var commandRetry = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(
        new SqlRetryLogicOption
        {
            NumberOfTries = 4,
            DeltaTime = TimeSpan.FromSeconds(5),
            MaxTimeInterval = TimeSpan.FromSeconds(30),
            // Deadlock victim, lock-request timeout, and common Azure SQL transient errors.
            TransientErrors = new[] { 1205, 1222, 10928, 10929, 40197, 40501, 40613, 49918 },
        });
    commandRetry.Retrying += (_, args) =>
    {
        Exception last = args.Exceptions[^1];
        logger.LogWarning(
            last,
            "Retrying SQL command (attempt {Attempt}) after {Delay}",
            args.RetryCount, args.Delay);
    };

    try
    {
        using var connection = new SqlConnection(builder.ConnectionString)
        {
            RetryLogicProvider = openRetry,
        };
        connection.Open();

        using var command = new SqlCommand(
            "SELECT TOP (100) SalesOrderId, OrderDate, TotalDue FROM Sales.SalesOrderHeader ORDER BY OrderDate DESC",
            connection)
        {
            RetryLogicProvider = commandRetry,
            CommandTimeout = 30,
        };

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            logger.LogInformation(
                "Order {SalesOrderId} placed {OrderDate:d} total ${TotalDue:N2}",
                reader.GetInt32(0), reader.GetDateTime(1), reader.GetDecimal(2));
        }
    }
    catch (SqlException ex)
    {
        logger.LogError(
            ex,
            "Query against {Server}/{Database} failed after retries (SQL error {ErrorNumber})",
            server, database, ex.Number);
        throw;
    }
}
```

This snippet is tuned for Azure SQL Database failover groups and Azure SQL Managed Instance.

`Encrypt = SqlConnectionEncryptOption.Strict` selects TDS 8.0 encryption. It requires Microsoft.Data.SqlClient 5.0 and later versions and a server that supports TDS 8.0 (SQL Server 2022 and later versions, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric). Fall back to `SqlConnectionEncryptOption.Mandatory` when you connect to older servers.

`ConnectRetryCount` and `ConnectRetryInterval` enable *idle connection resiliency*: after `Open()` succeeds, the driver transparently reconnects a dropped idle connection on the next command. They don't retry the initial `Open()`. Initial-connect retries come from the `openRetry` provider assigned to <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider%2A?displayProperty=nameWithType>. The two features are complementary.

The <xref:Microsoft.Data.SqlClient.SqlRetryLogicBaseProvider.Retrying> event on each provider fires before each retry attempt and carries the retry count, the delay before the next attempt, and the exceptions observed so far. Route it to <xref:Microsoft.Extensions.Logging.ILogger> or your telemetry pipeline to keep the retry loop visible in production.

Set `MultiSubnetFailover = true` for any SQL Server target. It selects a parallel-connect code path that completes login with the first responsive endpoint, avoiding the slow sequential per-IP walk that can otherwise stall connects to Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, availability group listeners, and failover cluster instances. On single-IP targets, the setting is safe. For more information, see [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md) and [Disabling Transparent Network IP Resolution](appcontext-switches.md#disabling-transparent-network-ip-resolution).

If the target is [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, raise `ConnectTimeout` to at least 60 seconds. An auto-paused database resumes on the first `Open()`, and the resume can take 30 to 60 seconds or more. Client-side timeouts surface as error `-2`, which isn't in the built-in transient error list, so `openRetry` won't rescue an `Open()` that times out mid-resume. The individual connect attempt must be long enough to cover the resume.

Command-level retry is the caller's decision, per command. Attach `commandRetry` to <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider%2A?displayProperty=nameWithType> only when replaying the command is safe: reads, `MERGE` guarded by a natural key, upserts through a stored procedure, and other idempotent operations. The built-in command provider skips retry when a transaction is active, so multi-statement transactions must be retried by application code that can reopen the transaction. Setting `TransientErrors` replaces the driver's built-in error list; to extend the built-in baseline instead, use <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.BaselineTransientErrors%2A?displayProperty=nameWithType> (Microsoft.Data.SqlClient 7.0 and later).

For more information about each part of this configuration, see:

- [Connection strings](connection-strings.md)
- [Microsoft Entra authentication](sql/azure-active-directory-authentication.md)
- [Encryption and certificate validation](encryption-and-certificate-validation.md)
- [Configurable retry logic](configurable-retry-logic.md)
- [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md)

## Key features

- **Modern .NET support**: Runs on current .NET and .NET Framework versions. For the per-version breakdown, see [Support lifecycle](sqlclient-driver-support-lifecycle.md).
- **Encrypted by default**: TLS-encrypted connections with `Encrypt=true` as the default. Set `Encrypt=Strict` for TDS 8.0 encryption on Microsoft.Data.SqlClient 5.0 and later.
- **Microsoft Entra ID authentication**: Passwordless connections with managed identity, service principal, interactive, integrated, default credential chain, and access-token flows.
- **Kerberos and NTLM**: Integrated Windows authentication for on-premises Active Directory and legacy scenarios.
- **Always Encrypted**: Client-side encryption for sensitive columns, with optional secure enclaves for in-place operations.
- **Bulk copy**: High-throughput inserts with <xref:Microsoft.Data.SqlClient.SqlBulkCopy>.
- **Connection resiliency**: Built-in connection retries (`ConnectRetryCount` and `ConnectRetryInterval`) plus opt-in configurable retry logic for connections and commands.
- **Rich SQL Server data types**: `datetimeoffset`, `sql_variant`, JSON, vector, spatial, XML, and table-valued parameters.
- **Diagnostics**: Event source tracing, diagnostic counters, provider statistics, and a dedicated troubleshooting guide.

## Get started

| Article | Description |
| --- | --- |
| [Getting started with the SqlClient driver](get-started-sqlclient-driver.md) | Set up a project, create a database, connect, query, and add connection resiliency. |
| [Overview of the SqlClient driver](overview-sqlclient-driver.md) | Learn how Microsoft.Data.SqlClient fits into ADO.NET. |
| [Download Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md) | Install the NuGet package and find source releases. |
| [Support lifecycle](sqlclient-driver-support-lifecycle.md) | Review supported driver versions and support dates. |
| [Microsoft.Data.SqlClient namespace](introduction-microsoft-data-sqlclient-namespace.md) | Migrate from System.Data.SqlClient and review namespace differences. |

## Configure and connect

| Article | Description |
| --- | --- |
| [Connect to a data source](connecting-to-data-source.md) | Open and manage connections to SQL Server and Azure SQL. |
| [Connection strings](connection-strings.md) | Configure server, database, authentication, encryption, and connection behavior. |
| [Encryption and certificate validation](encryption-and-certificate-validation.md) | Configure encrypted connections and server certificate validation. |
| [SQL Server connection pooling](sql-server-connection-pooling.md) | Reuse physical connections efficiently. |
| [Connection events](connection-events.md) | Respond to connection state and informational messages. |

## Authenticate and secure

| Article | Description |
| --- | --- |
| [SQL Server security](sql/sql-server-security.md) | Review authentication, authorization, and application security guidance. |
| [Microsoft Entra authentication](sql/azure-active-directory-authentication.md) | Connect with managed identity, service principal, password, and interactive flows. |
| [Protect connection information](protecting-connection-information.md) | Keep credentials and connection settings out of application code. |
| [Always Encrypted](sql/sqlclient-support-always-encrypted.md) | Protect sensitive column values from the database system. |
| [Always Encrypted with secure enclaves](sql/tutorial-always-encrypted-enclaves-develop-net-apps.md) | Run rich operations on encrypted data with a secure enclave. |

## Retrieve and update data

| Article | Description |
| --- | --- |
| [Commands and parameters](commands-parameters.md) | Execute parameterized SQL statements and stored procedures. |
| [DataAdapters and DataReaders](dataadapters-datareaders.md) | Stream result sets or populate disconnected data structures. |
| [Transactions and concurrency](transactions-and-concurrency.md) | Use local and distributed transactions and concurrency controls. |
| [Retrieve database schema information](retrieving-database-schema-information.md) | Discover schema collections and restrictions. |
| [Bulk copy operations](sql/bulk-copy-operations-sql-server.md) | Load large data sets efficiently with <xref:Microsoft.Data.SqlClient.SqlBulkCopy>. |
| [Table-valued parameters](sql/table-valued-parameters.md) | Send multiple rows to a parameterized statement or stored procedure. |
| [Asynchronous programming](asynchronous-programming.md) | Use asynchronous connection, command, and data operations. |
| [Multiple Active Result Sets (MARS)](sql/multiple-active-result-sets-mars.md) | Interleave multiple batches on one connection. |

## Data types

| Article | Description |
| --- | --- |
| [ADO.NET data type mappings](data-type-mappings-ado-net.md) | Map common language runtime types to provider and SQL Server types. |
| [SQL Server data types](sql/sql-server-data-types.md) | Work with SQL Server-specific values and <xref:System.Data.SqlTypes> types. |
| [JSON data](sql/json-data-sql-server.md) | Send and retrieve the SQL Server `json` data type. |
| [Vector data](sql/vector-data-sql-server.md) | Send and retrieve vector values. |
| [XML data](sql/xml-data-sql-server.md) | Read, write, and parameterize XML values. |
| [Binary and large-value data](sql/sql-server-binary-large-value-data.md) | Stream and update binary, FILESTREAM, and large-value data. |

## Reliability and diagnostics

| Article | Description |
| --- | --- |
| [Configurable retry logic](configurable-retry-logic.md) | Retry transient connection and command failures with bounded policies. |
| [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md) | Connect to availability group listeners and failover partners. |
| [Diagnostic counters](diagnostic-counters.md) | Monitor active connections, pooled connections, and other driver metrics. |
| [Enable event source tracing](enable-eventsource-tracing.md) | Capture detailed driver events for diagnosis. |
| [Data tracing](data-tracing.md) | Trace ADO.NET operations and data access. |
| [SqlClient troubleshooting guide](sqlclient-troubleshooting-guide.md) | Diagnose common connection and driver problems. |
| [Query notifications](sql/query-notifications-sql-server.md) | Receive notifications when query results change. |

## SQL Server features

| Article | Description |
| --- | --- |
| [SQL Server features and ADO.NET](sql/sql-server-features-adonet.md) | Browse SQL Server-specific features available through SqlClient. |
| [LocalDB](sql/sqlclient-support-localdb.md) | Connect to SQL Server Express LocalDB instances. |
| [Data discovery and classification](sql/data-classification.md) | Read sensitivity classification metadata from result sets. |

## Reference and resources

| Article | Description |
| --- | --- |
| [Microsoft.Data.SqlClient API reference](/dotnet/api/microsoft.data.sqlclient) | Browse .NET API reference for the driver. |
| [AppContext switches](appcontext-switches.md) | Configure compatibility and security behavior. |
| [Find additional SqlClient information](find-additional-sqlclient-driver-information.md) | Find source code, support, and community resources. |

## Related content

- [Microsoft.Data.SqlClient on GitHub](https://github.com/dotnet/SqlClient)
- [Microsoft.Data.SqlClient on NuGet](https://www.nuget.org/packages/Microsoft.Data.SqlClient)
- [Release notes](https://github.com/dotnet/SqlClient/blob/main/release-notes/README.md)
- [ADO.NET overview](/dotnet/framework/data/adonet/)
- [Connection modules for Microsoft SQL Database](../sql-connection-libraries.md)
