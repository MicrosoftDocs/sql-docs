---
title: Microsoft.Data.SqlClient for SQL Server
description: Microsoft.Data.SqlClient connects .NET applications to SQL Server, Azure SQL, SQL database in Microsoft Fabric, and Warehouse in Microsoft Fabric. Start here to install the driver, connect, and find every SqlClient article.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 10/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---
# Microsoft.Data.SqlClient for SQL Server

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Microsoft.Data.SqlClient is the supported .NET data provider for SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Microsoft Fabric, and Warehouse in Microsoft Fabric. It's distributed as a NuGet package, evolves independently of the .NET runtime, and replaces <xref:System.Data.SqlClient> for new development. It provides connections, commands, result processing, transactions, bulk copy, and SQL Server-specific features to .NET applications.

## Choose your starting point

<a name="choose-a-database"></a>

- To set up a project and run your first query, start with [Get started with the SqlClient driver](get-started-sqlclient-driver.md).
- To create a database to connect to, see the [prerequisites](get-started-sqlclient-driver.md#prerequisites) in Get started with the SqlClient driver.
- To add or update the driver in a .NET project, go to [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md).
- To connect to Azure SQL with passwordless authentication, start with [Microsoft Entra authentication](sql/azure-active-directory-authentication.md) and [Connection strings](connection-strings.md).
- To make an existing application resilient to transient failures, go to [Configurable retry logic](configurable-retry-logic.md) and [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md).
- To move large data sets efficiently, go to [Bulk copy operations](sql/bulk-copy-operations-sql-server.md).
- To migrate from `System.Data.SqlClient`, start with [Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient](migrate-system-data-sql-client-to-microsoft-data-sql-client.md).
- To diagnose a connection or query problem, go to [SqlClient troubleshooting guide](sqlclient-troubleshooting-guide.md) and [Enable event source tracing](enable-eventsource-tracing.md).

## Production baseline for Azure SQL

Use this snippet as a starting point for a production-oriented Azure SQL data access path. It reads the server and database names from <xref:Microsoft.Extensions.Configuration.IConfiguration>, so the values come from the configuration providers the host wires up, such as `appsettings.json`, environment variables, Azure App Configuration, and Key Vault-backed settings. The configuration combines these features:

- Transport Layer Security (TLS).
- Managed identity.
- Idle-connection resiliency.
- Initial-connect retry through configurable retry logic (CRL), with structured logging.
- Command-level retry for transient errors that occur mid-query.
- Fast failover-group recovery.
- Asynchronous calls that accept a cancellation token.

For higher security and to support configuration across environments, keep connection information outside your code. In production, store connection information in your application's configuration system, and use Azure Key Vault for sensitive values. For more information, see [Protect connection information](protecting-connection-information.md).

The C# snippet in this article omits `using` directives and class wrappers for brevity.

The snippet assumes a project that targets .NET 8 or later versions and references the `Microsoft.Data.SqlClient` and `Microsoft.Data.SqlClient.Extensions.Azure` 7.1.1 packages. The host, such as the .NET Generic Host or ASP.NET Core, supplies the <xref:Microsoft.Extensions.Logging.ILogger> and the `IConfiguration`, and configuration supplies the server name in `Sql:Server` and the database name in `Sql:Database`.

```csharp
public static async Task<string> QueryDatabaseNameAsync(
    IConfiguration config, ILogger logger, CancellationToken cancellationToken = default)
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
        Encrypt = SqlConnectionEncryptOption.Strict, // TDS 8.0 encryption (server must support it)
        ConnectTimeout = 30,                         // per-attempt connect timeout in seconds
        // Retry transient failures during Open() and reconnect a dropped idle connection.
        // The configurable provider below adds another policy around Open().
        ConnectRetryCount = 3,
        ConnectRetryInterval = 10,
        MultiSubnetFailover = true,                  // recommended for TCP endpoints; enables parallel connect
        // ApplicationIntent = ApplicationIntent.ReadOnly, // uncomment to route to a readable secondary
    };

    // Add exponential backoff and jitter around Open().
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

    await using var connection = new SqlConnection(builder.ConnectionString)
    {
        RetryLogicProvider = openRetry,
    };
    await connection.OpenAsync(cancellationToken);

    await using var command = new SqlCommand("SELECT DB_NAME()", connection)
    {
        RetryLogicProvider = commandRetry,
        CommandTimeout = 30,
    };

    var name = (string)await command.ExecuteScalarAsync(cancellationToken);
    logger.LogInformation("Connected to database {DatabaseName}", name);
    return name;
}
```

The method returns the name of the database it connected to and logs `Connected to database <database>`. When a retry happens, it also logs one warning per retry with the exception that triggered it.

How each part of the snippet works:

- **Targets**: Any [SQL Database Engine](../../database-engine/sql-database-engine.md) endpoint configured for Microsoft Entra authentication: Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, and SQL Server 2022 and later versions on Azure Virtual Machines or enabled by Azure Arc.
- **`Encrypt = SqlConnectionEncryptOption.Strict`**: Selects TDS 8.0 encryption. It requires SQL Server 2022 and later versions, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric. Use `SqlConnectionEncryptOption.Mandatory` for older servers.
- **`ActiveDirectoryManagedIdentity`**: Works only where a managed identity is available, such as Azure App Service, Azure Functions, or an Azure virtual machine. For local development, use `SqlAuthenticationMethod.ActiveDirectoryDefault`, which tries the Azure CLI, Visual Studio sign-in, and other credential sources. Keep it out of production because probing each source slows the connection. For more information, see [Use default authentication](sql/azure-active-directory-authentication.md#use-default-authentication).
- **`MultiSubnetFailover = true`**: Attempts TCP connections to all resolved IP addresses in parallel and uses the first connection that succeeds, avoiding the slow sequential per-IP walk. Set it for Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, availability group listeners, and failover cluster instances. It's safe on single-IP targets. It isn't supported for named instances, protocols other than TCP, instances with more than 64 IP addresses, or database mirroring, which is deprecated. Use Always On availability groups instead. For more information, see [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md) and [Disabling Transparent Network IP Resolution](appcontext-switches.md#disabling-transparent-network-ip-resolution).
- **`ConnectRetryCount = 3` and `ConnectRetryInterval = 10`**: The driver retries qualifying transient failures during `Open()`, and reconnects a dropped idle connection on the next command. The default count is 1. Three attempts 10 seconds apart outlast a longer reconfiguration or failover. The driver raises 1 to 2 only when the server name ends in a known Azure SQL domain, such as `.database.windows.net`, so a custom DNS alias gets 1.
- **`openRetry`**: Assigned to <xref:Microsoft.Data.SqlClient.SqlConnection.RetryLogicProvider%2A?displayProperty=nameWithType>, it adds exponential backoff around `Open()`. The driver stops its internal retries when `ConnectTimeout` expires, so the worst case before `OpenAsync` throws is five attempts of up to 30 seconds each, plus backoff. Each idle reconnect attempt also runs the full `openRetry` policy, and when that policy exhausts its retries, idle recovery ends.
- **Serverless auto-pause**: For [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview), raise `ConnectTimeout` to at least 60 seconds. The first `Open()` can fail with error `40613` while the database resumes, and `openRetry` retries it. A client-side timeout surfaces as error `-2`, which isn't retried, so each connect attempt must outlast the resume. For more information, see [Auto-pause and auto-resume](/azure/azure-sql/database/serverless-tier-auto-pause-resume).
- **`commandRetry`**: Attach it to <xref:Microsoft.Data.SqlClient.SqlCommand.RetryLogicProvider%2A?displayProperty=nameWithType> only when replaying the command is safe: reads, `MERGE` guarded by a natural key, upserts through a stored procedure, and other idempotent operations. It skips retry inside a transaction, so application code must retry multi-statement transactions. `ExecuteReader` and `ExecuteScalar` retry only errors returned before the first result set starts. `ExecuteNonQuery` retries the whole call.
- **Transient error list**: Setting `TransientErrors` replaces the built-in list. To extend it instead, use <xref:Microsoft.Data.SqlClient.SqlConfigurableRetryFactory.BaselineTransientErrors%2A?displayProperty=nameWithType> (Microsoft.Data.SqlClient 7.0 and later).
- **`Retrying` event**: <xref:Microsoft.Data.SqlClient.SqlRetryLogicBaseProvider.Retrying> fires before each retry with the retry count, the next delay, and the exceptions so far. Route it to <xref:Microsoft.Extensions.Logging.ILogger> or your telemetry pipeline.
- **Exceptions**: The method doesn't catch exceptions. An exhausted provider throws <xref:System.AggregateException> with the exception from each attempt. A nontransient error propagates as the original exception. The caller logs it once with the rest of the request context.
- **Cancellation**: The <xref:System.Threading.CancellationToken> stops the provider's wait between retries. It doesn't interrupt the driver's `ConnectRetryInterval` wait inside a single `Open` attempt. For more information, see [Asynchronous programming](asynchronous-programming.md).
- **Provider lifetime**: The snippet creates the retry providers on every call to stay self-contained. In an application, create them once and reuse them.

For more information about each part of this configuration, see:

- [Connection strings](connection-strings.md)
- [Microsoft Entra authentication](sql/azure-active-directory-authentication.md)
- [Encryption and certificate validation](encryption-and-certificate-validation.md)
- [Configurable retry logic](configurable-retry-logic.md)
- [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md)

## Learn the core data-access tasks

After you connect, use these articles in order:

1. [Execute a command](execute-command.md) to choose a result shape and run Transact-SQL (T-SQL).
1. [Configure parameters](configure-parameters.md) to match the database contract.
1. [Retrieve data with SqlDataReader](retrieve-data-by-datareader.md) to process rows and multiple results.
1. [Modify data with stored procedures](modify-data-with-stored-procedures.md) to use output parameters and return values.
1. [Use asynchronous programming and cancellation](asynchronous-programming.md) to add caller cancellation and bounded timeouts.
1. [Handle errors and messages](handle-errors-messages.md) before you add retries or production diagnostics.

## Key features

- **Modern .NET support**: Runs on current .NET and .NET Framework versions. For the per-version breakdown, see [Support lifecycle](sqlclient-driver-support-lifecycle.md).
- **Encrypted by default**: TLS-encrypted connections with `Encrypt=true` as the default. Set `Encrypt=Strict` for TDS 8.0 encryption.
- **Microsoft Entra ID authentication**: Passwordless connections with managed identity, service principal, interactive, integrated, default credential chain, and access-token flows.
- **Kerberos and NTLM**: Integrated Windows authentication for on-premises Active Directory and legacy scenarios.
- **Always Encrypted**: Client-side encryption for sensitive columns, with optional secure enclaves for in-place operations.
- **Bulk copy**: High-throughput inserts with <xref:Microsoft.Data.SqlClient.SqlBulkCopy>.
- **Connection pooling**: On by default. An opt-in [V2 pool](appcontext-switches.md#enable-the-v2-connection-pool) is in evaluation.
- **Connection resiliency**: Built-in connection retries (`ConnectRetryCount` and `ConnectRetryInterval`) plus opt-in configurable retry logic for connections and commands.
- **Rich SQL Server data types**: `datetimeoffset`, `sql_variant`, JSON, vector, spatial, XML, and table-valued parameters.
- **Diagnostics**: Event source tracing, diagnostic counters, provider statistics, and a dedicated troubleshooting guide.
- **Application identity**: Set `SqlConnection.RegisteredApplication` to identify your library or tool to SQL Server (Microsoft.Data.SqlClient 7.1 and later).

## Get started

| Article | Description |
| --- | --- |
| [Get started with the SqlClient driver](get-started-sqlclient-driver.md) | Set up a project, create a database, connect, query, and add connection resiliency. |
| [Install, update, and deploy Microsoft.Data.SqlClient](download-microsoft-sqlclient-data-provider.md) | Install NuGet packages, choose a release, update the driver, and prepare deployment output. |
| [Support lifecycle](sqlclient-driver-support-lifecycle.md) | Review supported driver versions and support dates. |
| [Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient](migrate-system-data-sql-client-to-microsoft-data-sql-client.md) | Update package references, namespaces, configuration, and changed driver behavior. |
| [Microsoft.Data.SqlClient namespace and compatibility](introduction-microsoft-data-sqlclient-namespace.md) | Understand how SqlClient implements ADO.NET and its compatibility with System.Data.SqlClient, .NET, and SQL Server. |
| [What's new in Microsoft.Data.SqlClient](microsoft-data-sql-client-release-notes.md) | Find current releases, major upgrade changes, and upstream release notes. |

## Configure and connect

| Article | Description |
| --- | --- |
| [Connect to a data source](connecting-to-data-source.md) | Open and manage connections, and respond to connection state changes. |
| [Connection strings](connection-strings.md) | Configure server, database, authentication, encryption, and connection behavior. |
| [Encryption and certificate validation](encryption-and-certificate-validation.md) | Configure encrypted connections and server certificate validation. |
| [SQL Server connection pooling](sql-server-connection-pooling.md) | Reuse physical connections efficiently. |
| [Handle errors and messages](handle-errors-messages.md) | Catch errors and receive informational messages. |

## Authenticate and secure

| Article | Description |
| --- | --- |
| [Security best practices for Microsoft.Data.SqlClient](sql/application-security-scenarios-sql-server.md) | Review authentication, authorization, and application security guidance. |
| [Microsoft Entra authentication](sql/azure-active-directory-authentication.md) | Connect with managed identity, service principal, password, and interactive flows. |
| [Protect connection information](protecting-connection-information.md) | Keep credentials and connection settings out of application code. |
| [Always Encrypted](sql/sqlclient-support-always-encrypted.md) | Protect sensitive column values from the database system. |
| [Always Encrypted with secure enclaves](sql/tutorial-always-encrypted-enclaves-develop-net-apps.md) | Run rich operations on encrypted data with a secure enclave. |

## Retrieve and update data

| Article | Description |
| --- | --- |
| [Execute a command](execute-command.md) | Execute parameterized SQL statements and stored procedures. |
| [DataAdapters and DataReaders](dataadapters-datareaders.md) | Stream result sets or populate disconnected data structures. |
| [Execute multiple commands with SqlBatch](execute-multiple-commands-sql-batch.md) | Send an ordered collection of commands and process their results. |
| [Stream data](sqlclient-streaming-support.md) | Transfer large binary, text, XML, and JSON values without buffering the complete value. |
| [Transactions and concurrency](transactions-and-concurrency.md) | Use local and distributed transactions and concurrency controls. |
| [Retrieve database schema information](retrieving-database-schema-information.md) | Discover schema collections and restrictions. |
| [Bulk copy operations](sql/bulk-copy-operations-sql-server.md) | Load large data sets efficiently with <xref:Microsoft.Data.SqlClient.SqlBulkCopy>. |
| [Table-valued parameters](sql/table-valued-parameters.md) | Send multiple rows to a parameterized statement or stored procedure. |
| [Multiple Active Result Sets (MARS)](sql/multiple-active-result-sets-mars.md) | Interleave multiple batches on one connection. |

## Data types

| Article | Description |
| --- | --- |
| [SQL Server data type mappings](sql-server-data-type-mappings.md) | Map SQL Server types to .NET and <xref:System.Data.SqlTypes> types. |
| [JSON data](sql/json-data-sql-server.md) | Send and retrieve the SQL Server **json** data type. |
| [Vector data](sql/vector-data-sql-server.md) | Send and retrieve vector values. |
| [XML data](sql/xml-data-sql-server.md) | Read, write, and parameterize XML values. |
| [Binary and large-value data](sql/sql-server-binary-large-value-data.md) | Stream and update binary, FILESTREAM, and large-value data. |

## Reliability and diagnostics

| Article | Description |
| --- | --- |
| [Configurable retry logic](configurable-retry-logic.md) | Retry transient connection and command failures with bounded policies. |
| [High availability and disaster recovery](sql/sqlclient-support-high-availability-disaster-recovery.md) | Connect to availability group listeners and failover partners. |
| [Event counters in SqlClient](event-counters.md) | Monitor active connections, pooled connections, and other driver metrics. |
| [Enable event source tracing](enable-eventsource-tracing.md) | Capture detailed driver events for diagnosis. |
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
| [Microsoft.Data.SqlClient GitHub repository](https://github.com/dotnet/SqlClient) | Read the driver source code, release notes, and samples. |
| [Microsoft.Data.SqlClient samples](https://github.com/dotnet/SqlClient/tree/main/doc/samples) | Browse C# samples for driver APIs. |
| [.NET runtime GitHub repository](https://github.com/dotnet/runtime) | Read the source for the .NET runtime libraries. |

## Request a feature

To request a feature, open an issue in the [Microsoft.Data.SqlClient GitHub repository](https://github.com/dotnet/SqlClient/issues/new/choose).

## Related content

- [Microsoft.Data.SqlClient on GitHub](https://github.com/dotnet/SqlClient)
- [Microsoft.Data.SqlClient on NuGet](https://www.nuget.org/packages/Microsoft.Data.SqlClient)
- [Release notes](https://github.com/dotnet/SqlClient/blob/main/release-notes/README.md)
- [ADO.NET overview](/dotnet/framework/data/adonet/)
- [Microsoft SQL drivers and frameworks](../sql-connection-libraries.md)
