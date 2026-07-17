---
title: "Introduction to Microsoft.Data.SqlClient Namespace"
description: Learn about the Microsoft.Data.SqlClient namespace and how it's the preferred way to connect to SQL for .NET applications.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, paulmedynski, cmalhotra
ms.date: 03/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: whats-new
ms.custom:
  - sfi-ropc-nochange
  - ignite-2025
---

# Introduction to Microsoft.Data.SqlClient namespace

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The Microsoft.Data.SqlClient namespace is essentially a new version of the System.Data.SqlClient namespace. Microsoft.Data.SqlClient generally maintains the same API and backward compatibility with System.Data.SqlClient. To migrate from System.Data.SqlClient to Microsoft.Data.SqlClient, for most applications, it's simple. Add a NuGet dependency on Microsoft.Data.SqlClient and update references and `using` statements to Microsoft.Data.SqlClient.

There are a few differences in less-used APIs compared to System.Data.SqlClient that might affect some applications. For those differences, refer to the useful [porting cheat sheet](https://github.com/dotnet/SqlClient/blob/main/porting-cheat-sheet.md).

## API reference

The Microsoft.Data.SqlClient API details can be found in the [.NET API Browser](/dotnet/api/microsoft.data.sqlclient).

## Release notes for 7.0

This is the general availability release of **Microsoft.Data.SqlClient 7.0**, a major milestone for the .NET data provider for SQL Server. This release addresses the most upvoted issue in the repository's history — extracting Azure dependencies from the core package — introduces pluggable SSPI authentication, adds enhanced routing for Azure, and delivers async read performance improvements.  

Also released as part of this milestone:

- Released Microsoft.Data.SqlClient.Extensions.Abstractions 1.0.0. See [release notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/Extensions/Abstractions/1.0/1.0.0.md).
- Released Microsoft.Data.SqlClient.Extensions.Azure 1.0.0. See [release notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/Extensions/Azure/1.0/1.0.0.md).
- Released Microsoft.Data.SqlClient.Internal.Logging 1.0.0. See [release notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/Internal/Logging/1.0/1.0.0.md).
- Released Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider 7.0.0. See [release notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/add-ons/AzureKeyVaultProvider/7.0/7.0.0.md).

### Breaking changes in 7.0

#### Azure dependencies removed from core package

*What Changed:*

- The core `Microsoft.Data.SqlClient` package no longer depends on `Azure.Core`, `Azure.Identity`, or their transitive dependencies (e.g., `Microsoft.Identity.Client`, `Microsoft.Web.WebView2`). Azure Active Directory / Entra ID authentication functionality (`ActiveDirectoryAuthenticationProvider` and related types) has been extracted into a new `Microsoft.Data.SqlClient.Extensions.Azure` package.
  ([#1108](https://github.com/dotnet/SqlClient/issues/1108),
   [#3680](https://github.com/dotnet/SqlClient/pull/3680),
   [#3902](https://github.com/dotnet/SqlClient/pull/3902),
   [#3904](https://github.com/dotnet/SqlClient/pull/3904),
   [#3908](https://github.com/dotnet/SqlClient/pull/3908),
   [#3917](https://github.com/dotnet/SqlClient/pull/3917),
   [#3982](https://github.com/dotnet/SqlClient/pull/3982),
   [#3978](https://github.com/dotnet/SqlClient/pull/3978),
   [#3986](https://github.com/dotnet/SqlClient/pull/3986))
- Two additional packages are introduced to support this separation: `Microsoft.Data.SqlClient.Extensions.Abstractions` (shared types between the core driver and extensions) and `Microsoft.Data.SqlClient.Internal.Logging` (shared ETW tracing infrastructure).  
  ([#3626](https://github.com/dotnet/SqlClient/pull/3626),
   [#3628](https://github.com/dotnet/SqlClient/pull/3628),
   [#3967](https://github.com/dotnet/SqlClient/pull/3967),
   [#4038](https://github.com/dotnet/SqlClient/pull/4038))

*Who Benefits:*

- All users benefit from a significantly lighter core package. Previously, the Azure dependency chain pulled in numerous assemblies even for applications that only needed basic SQL Server connectivity. This was the [most upvoted open issue](https://github.com/dotnet/SqlClient/issues/1108) in the repository.
- Users who do not use Entra ID authentication no longer carry Azure-related assemblies in their build output.
- Users who do use Entra ID authentication can now manage Azure dependency versions independently from the core driver.

*Impact:*

- Applications using Entra ID authentication (e.g., `ActiveDirectoryInteractive`, `ActiveDirectoryDefault`, `ActiveDirectoryManagedIdentity`, etc.) must now install the `Microsoft.Data.SqlClient.Extensions.Azure` NuGet package separately:

```console
dotnet add package Microsoft.Data.SqlClient.Extensions.Azure
```

- No code changes are required beyond adding the package reference.
- If an Entra ID authentication method is used without the Azure package installed, the driver now provides an actionable error message guiding users to install the correct package.

#### Other breaking changes in 7.0

- Reverted public visibility of internal interop enums (`IoControlCodeAccess` and `IoControlTransferType`) that were accidentally made public during the project merge.
  ([#3900](https://github.com/dotnet/SqlClient/pull/3900))

### New Features in 7.0

#### Pluggable authentication with SspiContextProvider  

*What Changed:*

- Added a public `SspiContextProvider` property on `SqlConnection`, completing the SSPI extensibility work begun in 6.1.0. Applications can now supply a custom SSPI context provider for integrated authentication, enabling custom Kerberos ticket negotiation and NTLM username/password authentication scenarios.
  ([#2253](https://github.com/dotnet/SqlClient/issues/2253),
   [#2494](https://github.com/dotnet/SqlClient/pull/2494))

*Who Benefits:*

- Users authenticating across untrusted domains, non-domain-joined machines, or cross-platform environments where configuring integrated authentication is difficult.
- Users running in containers who need manual Kerberos negotiation without deploying sidecars or external ticket-refresh mechanisms.
- Users who need NTLM username/password authentication to SQL Server, which the driver does not provide natively.

*Impact:*

- Applications can set a custom `SspiContextProvider` on `SqlConnection` before opening the connection:

```c#
var connection = new SqlConnection(connectionString);
connection.SspiContextProvider = new MyKerberosProvider();
connection.Open();
```

- The provider handles the authentication token exchange during integrated authentication. Existing authentication behavior is unchanged when no custom provider is set. See [SspiContextProvider_CustomProvider.cs](https://github.com/dotnet/SqlClient/tree/main/doc/samples/SspiContextProvider_CustomProvider.cs) for a sample implementation.
- **Note:** The `SspiContextProvider` is part of the connection pool key. Ensure the implementation returns a consistent identity per resource.  

#### Async read performance: Packet multiplexing (preview)

*What Changed:*

- Continued refinement of packet multiplexing with bug fixes and stability improvements since 6.1.0, plus new app context switches for opt-in control.
  ([#3534](https://github.com/dotnet/SqlClient/pull/3534),
   [#3537](https://github.com/dotnet/SqlClient/pull/3537),
   [#3605](https://github.com/dotnet/SqlClient/pull/3605))

*Who Benefits:*

- Applications performing large async reads (`ExecuteReaderAsync` with big result sets, streaming scenarios, or bulk data retrieval).

*Impact:*

- Packet multiplexing ships behind two opt-in feature switches:

```c#
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseCompatibilityAsyncBehaviour", false);
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseCompatibilityProcessSni", false);
```

- Setting both switches to `false` enables the new async processing path. By default, the driver uses the existing (compatible) behavior.

#### Enhanced Routing Support

*What Changed:*

- Added support for enhanced routing, a TDS feature that allows the server to redirect connections to a specific server *and* database during login.
  ([#3641](https://github.com/dotnet/SqlClient/issues/3641),
   [#3969](https://github.com/dotnet/SqlClient/pull/3969),
   [#3970](https://github.com/dotnet/SqlClient/pull/3970),
   [#3973](https://github.com/dotnet/SqlClient/pull/3973))

*Who Benefits:*

- Users connecting to Azure environments that use named read replicas and gateway-based load balancing.

*Impact:*

- Enhanced routing is negotiated automatically during login when the server supports it. No application code changes are required.

#### Support for .NET 10

*What Changed:*

- Updated pipelines and test suites to compile the driver using the .NET 10 SDK.
  ([#3686](https://github.com/dotnet/SqlClient/pull/3686))

*Who Benefits:*

- Developers targeting .NET 10 on day one.

*Impact:*

- SqlClient 7.0 compiles and tests against .NET 10, ensuring compatibility.

#### Strongly-typed diagnostic events on .NET Framework

*What Changed:*

- Enabled `SqlClientDiagnosticListener` for `SqlCommand` on .NET Framework, closing a long-standing observability gap where diagnostic events were previously only emitted on .NET Core.
  ([#3658](https://github.com/dotnet/SqlClient/pull/3658))

- Brought the 15 strongly-typed diagnostic event classes in the `Microsoft.Data.SqlClient.Diagnostics` namespace — originally introduced for .NET Core in 6.0 — to .NET Framework as part of the codebase merge. Both platforms now use the same strongly-typed event model. The types cover command, connection, and transaction lifecycle events:
  - `SqlClientCommandBefore`, `SqlClientCommandAfter`, `SqlClientCommandError`
  - `SqlClientConnectionOpenBefore`, `SqlClientConnectionOpenAfter`, `SqlClientConnectionOpenError`
  - `SqlClientConnectionCloseBefore`, `SqlClientConnectionCloseAfter`, `SqlClientConnectionCloseError`
  - `SqlClientTransactionCommitBefore`, `SqlClientTransactionCommitAfter`, `SqlClientTransactionCommitError`
  - `SqlClientTransactionRollbackBefore`, `SqlClientTransactionRollbackAfter`, `SqlClientTransactionRollbackError`

  ([#3493](https://github.com/dotnet/SqlClient/pull/3493))

*Who Benefits:*

- .NET Framework users subscribing to `SqlClientDiagnosticListener` events for observability, distributed tracing, or custom telemetry. These users now have parity with .NET Core, gaining IntelliSense, compile-time safety, and eliminating the need to access diagnostic payloads via reflection or dictionary lookups.

*Impact:*

- On .NET Framework, `SqlCommand` now emits the same diagnostic events that were previously only available on .NET Core. Subscribers to `DiagnosticListener` events (e.g., `Microsoft.Data.SqlClient.WriteCommandBefore`) receive the strongly-typed objects:

```c#
listener.Subscribe(new Observer<KeyValuePair<string, object?>>(kvp =>
{
    if (kvp.Value is SqlClientCommandBefore before)
    {
        Console.WriteLine($"Executing: {before.Command.CommandText}");
    }
}));
```

- The types implement `IReadOnlyList<KeyValuePair<string, object>>` for backward compatibility with code that iterates properties generically.

#### Other additions in 7.0

- Added `SqlConfigurableRetryFactory.BaselineTransientErrors` static property exposing the default transient error codes list as a `ReadOnlyCollection<int>`, making it easier to extend the default list with application-specific error codes.
  ([#3903](https://github.com/dotnet/SqlClient/pull/3903))

- Added app context switch `Switch.Microsoft.Data.SqlClient.EnableMultiSubnetFailoverByDefault` to set `MultiSubnetFailover=true` globally without modifying connection strings.
  ([#3841](https://github.com/dotnet/SqlClient/pull/3841))

- Added app context switch `Switch.Microsoft.Data.SqlClient.IgnoreServerProvidedFailoverPartner` to let the client ignore server-provided failover partner info in Basic Availability Groups.
  ([#3625](https://github.com/dotnet/SqlClient/pull/3625))

- Enabled User Agent Feature Extension (opt-in via `Switch.Microsoft.Data.SqlClient.EnableUserAgent`).
  ([#3606](https://github.com/dotnet/SqlClient/pull/3606))

### Changes in 7.0

#### Deprecation of `SqlAuthenticationMethod.ActiveDirectoryPassword`

*What Changed:*

- `SqlAuthenticationMethod.ActiveDirectoryPassword` (the ROPC flow) is now marked `[Obsolete]` and will generate compiler warnings. This aligns with Microsoft's move toward [mandatory multifactor authentication](/entra/identity/authentication/concept-mandatory-multifactor-authentication).
  ([#3671](https://github.com/dotnet/SqlClient/pull/3671))

*Who Benefits:*

- Teams moving toward stronger, passwordless or MFA-compliant authentication.

*Impact:*

- If you use `Authentication=Active Directory Password`, migrate to a supported alternative:

| Scenario | Recommended Authentication |
|----------|---------------------------|
| Interactive / desktop apps | `Active Directory Interactive` |
| Service-to-service | `Active Directory Service Principal` |
| Azure-hosted workloads | `Active Directory Managed Identity` |
| Developer / CI environments | `Active Directory Default` |

- See [Connect to Azure SQL with Microsoft Entra authentication](sql/azure-active-directory-authentication.md) for more information.

### 7.0 Target Platform Support

- .NET Framework 4.6.2+ (Windows x86, Windows x64, Windows ARM64)
- .NET 8.0+ (Windows x86, Windows x64, Windows ARM, Windows ARM64, Linux, macOS)

Full release notes, including dependencies, are available in the GitHub Repository: [7.0 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/7.0).

## Release notes for 6.1

### New features in 6.1

#### Added dedicated SQL Server vector data type support

*What changed:*

- Optimized vector communications between MDS and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], employing a custom binary format over the TDS protocol ([#3433](https://github.com/dotnet/SqlClient/pull/3433), [#3443](https://github.com/dotnet/SqlClient/pull/3443)).

- Reduced processing load compared to existing JSON-based vector support.

- Initial support for 32-bit single-precision floating point vectors.

*Who benefits:*

- Applications moving large vector data sets see beneficial improvements to processing times and memory requirements.
- Vector-specific APIs are ready to support future numeric representations with a consistent look-and-feel.

*Effect:*

- Reduced transmission and processing times for vector operations versus JSON using [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]:

  - Reads: 50x improvement
  - Writes: 3.3x improvement
  - Bulk Copy: 19x improvement
  - (Observed with vector column of max 1998 size, and 10,000 records for each operation.)

- Improved memory footprint due to the elimination of JSON serialization/deserialization and string representation bloat.

- For backward compatibility with earlier SQL Server Vector implementations, applications might continue to use JSON strings to send/receive vector data, although they don't see any of the performance improvements noted previously.

#### Revived .NET Standard 2.0 target support

*What changed:*

- Support for targeting .NET Standard 2.0 has returned ([#3381](https://github.com/dotnet/SqlClient/pull/3381))
- Support had previously been removed in the 6.0 release, with the [community voicing concerns](https://github.com/dotnet/SqlClient/discussions/3115).

*Who benefits:*

- Libraries that depend on MDS might seamlessly target any of the following frameworks:
  - .NET Standard 2.0
  - .NET Framework 4.6.2 and above
  - .NET 8.0
  - .NET 9.0
- Applications should continue to target runtimes.
  - The MDS .NET Standard 2.0 target framework support doesn't include an actual implementation, and can't be used with a runtime.
  - An application's build/publish process should always pick the appropriate MDS .NET/.NET Framework runtime implementation.
  - Custom build/publish actions that incorrectly try to deploy the MDS .NET Standard 2.0 reference DLL at runtime aren't supported.

*Effect:*

- Libraries targeting .NET Standard 2.0 no longer receive warnings such as:

  ```output
  warning NU1701: Package 'Microsoft.Data.SqlClient 6.0.2' was restored using '.NETFramework,Version=v4.6.1, .NETFramework,Version=v4.6.2, .NETFramework,Version=v4.7, .NETFramework,Version=v4.7.1, .NETFramework,Version=v4.7.2, .NETFramework,Version=v4.8, .NETFramework,Version=v4.8.1' instead of the project target framework '.NETStandard,Version=v2.0'. This package may not be fully compatible with your project.
  ```

#### Other additions

- Added support for special casing with Fabric endpoints.
  ([#3084](https://github.com/dotnet/SqlClient/pull/3084))

### 6.1 Target platform support

- .NET Framework 4.6.2+ (Windows Arm64, Windows x86, Windows x64)
- .NET 8.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)

Full release notes, including dependencies, are available in the GitHub Repository: [6.1 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/6.1).

## Release notes for 6.0

### Breaking changes in 6.0

- Dropped support for .NET Standard. [#2386](https://github.com/dotnet/SqlClient/pull/2386)
- Dropped support for .NET 6 [#2927](https://github.com/dotnet/SqlClient/pull/2927)
- Dropped UWP (UAP) references. [#2483](https://github.com/dotnet/SqlClient/pull/2483)
- Dropped SQL Server 2000 client-side debugging support for .NET Framework [#2981](https://github.com/dotnet/SqlClient/pull/2981), [#2940](https://github.com/dotnet/SqlClient/pull/2940)

### New features in 6.0

- Added support for JSON data type [#2916](https://github.com/dotnet/SqlClient/pull/2916), [#2892](https://github.com/dotnet/SqlClient/pull/2892), [#2891](https://github.com/dotnet/SqlClient/pull/2891), [#2880](https://github.com/dotnet/SqlClient/pull/2880), [#2882](https://github.com/dotnet/SqlClient/pull/2882), [#2829](https://github.com/dotnet/SqlClient/pull/2829), [#2830](https://github.com/dotnet/SqlClient/pull/2830)
- Added support for .NET 9 [#2946](https://github.com/dotnet/SqlClient/pull/2946)
- Added `Microsoft.Data.SqlClient.Diagnostics.SqlClientDiagnostic` type in .NET. [#2226](https://github.com/dotnet/SqlClient/pull/2226)
- Added `DateOnly` and `TimeOnly` support to `DataTable` as a structured parameter. [#2258](https://github.com/dotnet/SqlClient/pull/2258)
- Added support for `SqlConnectionOverrides` in `OpenAsync()` API [#2433](https://github.com/dotnet/SqlClient/pull/2433)
- Added localization in Czech, Polish, and Turkish [#2987](https://github.com/dotnet/SqlClient/pull/2987)
- Added `TokenCredential` object to take advantage of token caching in `ActiveDirectoryAuthenticationProvider`. [#2380](https://github.com/dotnet/SqlClient/pull/2380)
- Added readme to NuGet package [#2826](https://github.com/dotnet/SqlClient/pull/2826)
- Enabled NuGet package auditing via NuGet.org audit source [#3024](https://github.com/dotnet/SqlClient/pull/3024)
- Added missing SqlCommand_BeginExecuteReader code sample [#3009](https://github.com/dotnet/SqlClient/pull/3009)
- Added scope trace for `GenerateSspiClientContext`. [#2497](https://github.com/dotnet/SqlClient/pull/2497), [#2725](https://github.com/dotnet/SqlClient/pull/2725)

### JSON data type support

JSON data type support is now available in Microsoft.Data.SqlClient v6.0. This release introduces the `SqlJson` type available as an extension to `System.Data.SqlDbTypes`:

```csharp
using System;
using System.Data.SqlTypes;
using System.Text.Json;

namespace Microsoft.Data.SqlTypes
{
    /// <summary>
    /// Represents the Json data type in SQL Server.
    /// </summary>
    public class SqlJson : INullable
    {
        /// <summary>
        /// Parameterless constructor. Initializes a new instance of the SqlJson class which
        /// represents a null JSON value.
        /// </summary>
        public SqlJson() { }

        /// <summary>
        /// Takes a <see cref="string"/> as input and initializes a new instance of the SqlJson class.
        /// </summary>
        /// <param name="jsonString"></param>
        public SqlJson(string jsonString) { }

        /// <summary>
        /// Takes a <see cref="JsonDocument"/> as input and initializes a new instance of the SqlJson class.
        /// </summary>
        /// <param name="jsonDoc"></param>
        public SqlJson(JsonDocument jsonDoc) { }

        /// <inheritdoc/>
        public bool IsNull => throw null;

        /// <summary>
        /// Represents a null instance of the <see cref="SqlJson"/> type.
        /// </summary>
        public static SqlJson Null { get { throw null; } }

        /// <summary>
        /// Gets the string representation of the JSON content of this <see cref="SqlJson" /> instance.
        /// </summary>
        public string Value { get ; }
    }
}
```

The JSON data type supports reading, writing, streaming, and bulk copy operations.

### Introducing SqlClientDiagnostics

New types are available in the `Microsoft.Data.SqlClient.Diagnostics` namespace that provide a strongly typed collection of key-value pairs. These types can be captured by consuming applications for gathering diagnostic events emitted by the driver. This release introduces the following types:

- `SqlClientCommandBefore`
- `SqlClientCommandAfter`
- `SqlClientCommandError`
- `SqlClientConnectionOpenBefore`
- `SqlClientConnectionOpenAfter`
- `SqlClientConnectionOpenError`
- `SqlClientConnectionCloseBefore`
- `SqlClientConnectionCloseAfter`
- `SqlClientConnectionCloseError`
- `SqlClientTransactionCommitBefore`
- `SqlClientTransactionCommitAfter`
- `SqlClientTransactionCommitError`
- `SqlClientTransactionRollbackBefore`
- `SqlClientTransactionRollbackAfter`
- `SqlClientTransactionRollbackError`

### Added support for Connection Overrides in OpenAsync() API

The default behavior of `SqlConnection.OpenAsync()` can be overridden to disable the ten-second delay and automatic connection retries triggered by transient errors.

```csharp
using(SqlConnection sqlConnection = new SqlConnection("Data Source=(local);Integrated Security=true;Initial Catalog=AdventureWorks;"))
{
    await sqlConnection.OpenAsync(SqlConnectionOverrides.OpenWithoutRetry, cancellationToken);
}
```

### 6.0 Target platform support

- .NET Framework 4.6.2+ (Windows x86, Windows x64)
- .NET 8.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)

Full release notes, including dependencies, are available in the GitHub Repository: [6.0 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/6.0).

## Release notes for 5.2

### New features in 5.2

- Added support of `SqlDiagnosticListener` on **.NET Standard**. [#1931](https://github.com/dotnet/SqlClient/pull/1931)
- Added new property `RowsCopied64` to `SqlBulkCopy`. [#2004](https://github.com/dotnet/SqlClient/pull/2004) [Read more](#added-new-property-rowscopied64-to-sqlbulkcopy)
- Added a new `AccessTokenCallBack` API to `SqlConnection`. [#1260](https://github.com/dotnet/SqlClient/pull/1260) [Read more](#added-new-property-accesstokencallback-to-sqlconnection)
- Added support for the `SuperSocketNetLib` registry option for Encrypt on .NET on Windows. [#2047](https://github.com/dotnet/SqlClient/pull/2047)
- Added `SqlBatch` support on .NET 6+ [#1825](https://github.com/dotnet/SqlClient/pull/1825), [#2223](https://github.com/dotnet/SqlClient/pull/2223) [Read more](#sqlbatch-api)
- Added Workload Identity authentication support [#2159](https://github.com/dotnet/SqlClient/pull/2159), [#2264](https://github.com/dotnet/SqlClient/pull/2264)
- Added Localization support on .NET [#2210](https://github.com/dotnet/SqlClient/pull/2110)
- Added support for Georgian collation [#2194](https://github.com/dotnet/SqlClient/pull/2194)
- Added support for Big Endian systems [#2170](https://github.com/dotnet/SqlClient/pull/2170)
- Added .NET 8 support [#2230](https://github.com/dotnet/SqlClient/pull/2230)
- Added explicit version for major .NET version dependencies on System.Runtime.Caching 8.0.0, System.Configuration.ConfigurationManager 8.0.0, and System.Diagnostics.DiagnosticSource 8.0.0 [#2303](https://github.com/dotnet/SqlClient/pull/2303)
- Added the ability to generate debugging symbols in a separate package file [#2137](https://github.com/dotnet/SqlClient/pull/2137)

#### Added new property `RowsCopied64` to SqlBulkCopy

SqlBulkCopy has a new property `RowsCopied64` which supports `long` value types.

**The existing `SqlBulkCopy.RowsCopied` behavior is unchanged. When the value exceeds `int.MaxValue`, `RowsCopied` can return a negative number.**

Example usage:

```csharp
    using (SqlConnection srcConn = new SqlConnection(srcConstr))
    using (SqlCommand srcCmd = new SqlCommand("select top 5 * from employees", srcConn))
    {
        srcConn.Open();
        using (DbDataReader reader = srcCmd.ExecuteReader())
        {
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(dstConn))
            {
                bulkcopy.DestinationTableName = dstTable;
                SqlBulkCopyColumnMappingCollection ColumnMappings = bulkcopy.ColumnMappings;

                ColumnMappings.Add("EmployeeID", "col1");
                ColumnMappings.Add("LastName", "col2");
                ColumnMappings.Add("FirstName", "col3");

                bulkcopy.WriteToServer(reader);
                long rowsCopied = bulkcopy.RowsCopied64;
            }
        }
    }
```

#### Added new property `AccessTokenCallBack` to SqlConnection

SqlConnection supports `TokenCredential` authentication by introducing a new `AccessTokenCallBack` property as a `Func<SqlAuthenticationParameters, CancellationToken,Task<SqlAuthenticationToken>>` delegate to return a federated authentication access token.

Example usage:

```csharp
using Microsoft.Data.SqlClient;
using Azure.Identity;

const string defaultScopeSuffix = "/.default";
string connectionString = GetConnectionString();
DefaultAzureCredential credential = new();
using SqlConnection connection = new(connectionString);

connection.AccessTokenCallback = async (authParams, cancellationToken) =>
{
    string scope = authParams.Resource.EndsWith(defaultScopeSuffix)
        ? authParams.Resource
        : $"{authParams.Resource}{defaultScopeSuffix}";
    AccessToken token = await credential.GetTokenAsync(
        new TokenRequestContext([scope]),
        cancellationToken);

    return new SqlAuthenticationToken(token.Token, token.ExpiresOn);
}

connection.Open();
Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
Console.WriteLine("State: {0}", connection.State);
```

#### SqlBatch API

Example usage:

```csharp
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        string str = "Data Source=(local);Initial Catalog=Northwind;"
        + "Integrated Security=SSPI;Encrypt=False";
        RunBatch(str);
    }

    static void RunBatch(string connString)
    {
        using var connection = new SqlConnection(connString);
        connection.Open();

        var batch = new SqlBatch(connection);

        const int count = 10;
        const string parameterName = "parameter";
        for (int i = 0; i < count; i++)
        {
            var batchCommand = new SqlBatchCommand($"SELECT @{parameterName} as value");
            batchCommand.Parameters.Add(new SqlParameter(parameterName, i));
            batch.BatchCommands.Add(batchCommand);
        }

        // Optionally Prepare
        batch.Prepare();

        var results = new List<int>(count);
        using (SqlDataReader reader = batch.ExecuteReader())
        {
            do
            {
                while (reader.Read())
                {
                    results.Add(reader.GetFieldValue<int>(0));
                }
            } while (reader.NextResult());
        }
        Console.WriteLine(string.Join(", ", results));
    }
}
```

### 5.2 Target platform support

- .NET Framework 4.6.2+ (Windows x86, Windows x64)
- .NET 6.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)

Full release notes, including dependencies, are available in the GitHub Repository: [5.2 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/5.2).

## Release notes for 5.1

### Breaking changes in 5.1

- Dropped support for .NET Core 3.1. [#1704](https://github.com/dotnet/SqlClient/pull/1704) [#1823](https://github.com/dotnet/SqlClient/pull/1823)

### New features in 5.1

- Added support for `DateOnly` and `TimeOnly` for `SqlParameter` value and `GetFieldValue`. [#1813](https://github.com/dotnet/SqlClient/pull/1813)
- Added support for TLS 1.3 for .NET Core and SNI Native. [#1821](https://github.com/dotnet/SqlClient/pull/1821)
- Added `ServerCertificate` setting for `Encrypt=Mandatory` or `Encrypt=Strict`. [#1822](https://github.com/dotnet/SqlClient/pull/1822) [Read more](#server-certificate)
- Added Windows Arm64 support when targeting .NET Framework. [#1828](https://github.com/dotnet/SqlClient/pull/1828)

#### Server certificate

The default value of the `ServerCertificate` connection setting is an empty string. When `Encrypt` is set to `Mandatory` or `Strict`, `ServerCertificate` can be used to specify a path on the file system to a certificate file to match against the server's TLS/SSL certificate. The certificate specified must be an exact match to be valid. The accepted certificate formats are `PEM`, `DER`, and `CER`. Here's a usage example:

```csharp
"Data Source=...;Encrypt=Strict;ServerCertificate=C:\\certificates\\server.cer"
```

### 5.1 Target platform support

- .NET Framework 4.6.2+ (Windows x86, Windows x64)
- .NET 6.0+ (Windows x86, Windows x64, Windows Arm64, Azure Resource Manager, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)

Full release notes, including dependencies, are available in the GitHub Repository: [5.1 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/5.1).

## Release notes for 5.0

### Breaking changes in 5.0

- Dropped support for .NET Framework 4.6.1 [#1574](https://github.com/dotnet/SqlClient/pull/1574)
- Added a dependency on the [Microsoft.SqlServer.Server](https://github.com/dotnet/SqlClient/tree/main/src/Microsoft.SqlServer.Server) package. This new dependency might cause namespace conflicts if your application references that namespace and still has package references (direct or indirect) to System.Data.SqlClient from .NET Core.
- Dropped classes from the `Microsoft.Data.SqlClient.Server` namespace and replaced them with supported types from the [Microsoft.SqlServer.Server](https://github.com/dotnet/SqlClient/tree/main/src/Microsoft.SqlServer.Server) package.[#1585](https://github.com/dotnet/SqlClient/pull/1585). The affected classes and enums are:
  - Microsoft.Data.SqlClient.Server.IBinarySerialize -> Microsoft.SqlServer.Server.IBinarySerialize
  - Microsoft.Data.SqlClient.Server.InvalidUdtException -> Microsoft.SqlServer.Server.InvalidUdtException
  - Microsoft.Data.SqlClient.Server.SqlFacetAttribute -> Microsoft.SqlServer.Server.SqlFacetAttribute
  - Microsoft.Data.SqlClient.Server.SqlFunctionAttribute -> Microsoft.SqlServer.Server.SqlFunctionAttribute
  - Microsoft.Data.SqlClient.Server.SqlMethodAttribute -> Microsoft.SqlServer.Server.SqlMethodAttribute
  - Microsoft.Data.SqlClient.Server.SqlUserDefinedAggregateAttribute -> Microsoft.SqlServer.Server.SqlUserDefinedAggregateAttribute
  - Microsoft.Data.SqlClient.Server.SqlUserDefinedTypeAttribute -> Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute
  - (enum) Microsoft.Data.SqlClient.Server.DataAccessKind -> Microsoft.SqlServer.Server.DataAccessKind
  - (enum) Microsoft.Data.SqlClient.Server.Format -> Microsoft.SqlServer.Server.Format
  - (enum) Microsoft.Data.SqlClient.Server.SystemDataAccessKind -> Microsoft.SqlServer.Server.SystemDataAccessKind

### New features in 5.0

- Added support for `TDS8`. To use TDS 8, users should specify Encrypt=Strict in the connection string. [#1608](https://github.com/dotnet/SqlClient/pull/1608) [Read more](#tds-80-enhanced-security)
- Added support for specifying Server SPN and Failover Server SPN on the connection. [#1607](https://github.com/dotnet/SqlClient/pull/1607) [Read more](#server-spn)
- Added support for aliases when targeting .NET Core on Windows. [#1588](https://github.com/dotnet/SqlClient/pull/1588) [Read more](#support-for-sql-aliases)
- Added SqlDataSourceEnumerator. [#1430](https://github.com/dotnet/SqlClient/pull/1430), [Read more](#sql-data-source-enumerator-support)
- Added a new AppContext switch to suppress insecure TLS warnings. [#1457](https://github.com/dotnet/SqlClient/pull/1457), [Read more](#suppress-insecure-tls-warnings)

#### TDS 8.0 enhanced security

To use TDS 8.0, specify Encrypt=Strict in the connection string. Strict mode disables TrustServerCertificate (always treated as False in Strict mode). HostNameInCertificate has been added to help some Strict mode scenarios. TDS 8.0 begins and continues all server communication inside a secure, encrypted TLS connection.

New Encrypt values have been added to clarify connection encryption behavior. `Encrypt=Mandatory` is equivalent to `Encrypt=True` and encrypts connections during the TDS connection negotiation. `Encrypt=Optional` is equivalent to `Encrypt=False` and only encrypts the connection if the server tells the client that encryption is required during the TDS connection negotiation.

For more information on encrypting connections to the server, see [Encryption and certificate validation in Microsoft.Data.SqlClient](encryption-and-certificate-validation.md).

`HostNameInCertificate` can be specified in the connection string when using aliases to connect with encryption to a server that has a server certificate with a different name or alternate subject name than the name used by the client to identify the server (DNS aliases, for example). Example usage: `HostNameInCertificate=MyDnsAliasName`

#### Server SPN

When connecting in an environment that has unique domain/forest topography, you might have specific requirements for Server SPNs. The ServerSPN/Server SPN and FailoverServerSPN/Failover Server SPN connection string settings can be used to override the autogenerated server SPNs used during integrated authentication in a domain environment

#### Support for SQL aliases

Users can configure Aliases by using the SQL Server Configuration Manager. These aliases are stored in the Windows registry and are already supported when targeting .NET Framework. This release brings support for aliases when targeting .NET or .NET Core on Windows.

#### SQL data source enumerator support

Provides a mechanism for enumerating all available instances of SQL Server within the local network.

```csharp
using Microsoft.Data.Sql;
static void Main()
  {
    // Retrieve the enumerator instance and then the data.
    SqlDataSourceEnumerator instance =
      SqlDataSourceEnumerator.Instance;
    System.Data.DataTable table = instance.GetDataSources();

    // Display the contents of the table.
    DisplayData(table);

    Console.WriteLine("Press any key to continue.");
    Console.ReadKey();
  }

  private static void DisplayData(System.Data.DataTable table)
  {
    foreach (System.Data.DataRow row in table.Rows)
    {
      foreach (System.Data.DataColumn col in table.Columns)
      {
        Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
      }
      Console.WriteLine("============================");
    }
  }
```

#### Suppress insecure TLS warnings

A security warning is output on the console if the TLS version less than 1.2 is used to negotiate with the server. This warning could be suppressed on SQL connection while `Encrypt = false` by enabling the following AppContext switch on the application startup:

```csharp
Switch.Microsoft.Data.SqlClient.SuppressInsecureTLSWarning
```

### 5.0 Target platform support

- .NET Framework 4.6.2+ (Windows x86, Windows x64)
- .NET Core 3.1+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)

Full release notes, including dependencies, are available in the GitHub Repository: [5.0 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/5.0).

## Release notes for 4.1

Full release notes, including dependencies, are available in the GitHub Repository: [4.1 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/4.1).

### New features in 4.1

#### Introduce attestation protocol None

A new attestation protocol called `None` is allowed in the connection string. This protocol allows users to forgo enclave attestation for `VBS` enclaves. When this protocol is set, the enclave attestation URL property is optional.

Connection string example:

```csharp
//Attestation protocol NONE with no URL
"Data Source = {server}; Initial Catalog = {db}; Column Encryption Setting = Enabled; Attestation Protocol = None;"
```

### 4.1 Target platform support

- .NET Framework 4.6.1+ (Windows x86, Windows x64)
- .NET Core 3.1+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)

## Release notes for 4.0

Full release notes, including dependencies, are available in the GitHub Repository: [4.0 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/4.0).

### Breaking changes in 4.0

- Changed `Encrypt` connection string property to be `true` by default. [#1210](https://github.com/dotnet/SqlClient/pull/1210) [Read more](#encrypt-default-value-set-to-true)
- The driver now throws `SqlException` replacing `AggregateException` for active directory authentication modes. [#1213](https://github.com/dotnet/SqlClient/pull/1213)
- Dropped obsolete `Asynchronous Processing` connection property from .NET Framework. [#1148](https://github.com/dotnet/SqlClient/pull/1148)
- Removed `Configurable Retry Logic` safety switch. [#1254](https://github.com/dotnet/SqlClient/pull/1254) [Read more](#remove-configurable-retry-logic-safety-switch)
- Dropped support for .NET Core 2.1 [#1272](https://github.com/dotnet/SqlClient/pull/1272)
- [.NET Framework] Exception isn't thrown if a User ID is provided in the connection string when using `Active Directory Integrated` authentication [#1359](https://github.com/dotnet/SqlClient/pull/1359)

### New features in 4.0

#### Encrypt default value set to true

The default value of the `Encrypt` connection setting has been changed from `false` to `true`. With the growing use of cloud databases and the need to ensure those connections are secure, it's time for this backward-compatibility-breaking change.

#### Ensure connections fail when encryption is required

In scenarios where client encryption libraries were disabled or unavailable, it was possible for unencrypted connections to be made when Encrypt was set to true or the server required encryption.

#### App Context Switch for using System default protocols

TLS 1.3 isn't supported by the driver; therefore, it has been removed from the supported protocols list by default. Users can switch back to forcing use of the Operating System's client protocols, by enabling the following App Context switch:

`Switch.Microsoft.Data.SqlClient.UseSystemDefaultSecureProtocols`

#### Enable optimized parameter binding

Microsoft.Data.SqlClient introduces a new `SqlCommand` API, `EnableOptimizedParameterBinding` to improve performance of queries with large number of parameters. This property is disabled by default. When set to `true`, parameter names aren't sent to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance when the command is executed.

```csharp
public class SqlCommand
{
    public bool EnableOptimizedParameterBinding { get; set; }
}
```

#### Remove configurable retry logic safety switch

The App Context switch "Switch.Microsoft.Data.SqlClient.EnableRetryLogic" is no longer required to use the configurable retry logic feature. The feature is now supported in production. The default behavior of the feature continues to be a non-retry policy, which client applications need to override to enable retries.

#### SqlLocalDb shared instance support

SqlLocalDb shared instances are now supported when using Managed SNI.

- Possible scenarios:
  - `(localdb)\.` (connects to default instance of SqlLocalDb)
  - `(localdb)\<named instance>`
  - `(localdb)\.\<shared instance name>` (*newly added support)

#### `GetFieldValueAsync<T>` and `GetFieldValue<T>` support for `XmlReader`, `TextReader`, `Stream` types

`XmlReader`, `TextReader`, `Stream` types are now supported when using `GetFieldValueAsync<T>` and `GetFieldValue<T>`.

Example usage:

```csharp
using (SqlConnection connection = new SqlConnection(connectionString))
{
    using (SqlCommand command = new SqlCommand(query, connection))
    {
        connection.Open();
        using (SqlDataReader reader = await command.ExecuteReaderAsync())
        {
            if (await reader.ReadAsync())
            {
                using (Stream stream = await reader.GetFieldValueAsync<Stream>(1))
                {
                    // Continue to read from stream
                }
            }
        }
    }
}
```

### 4.0 Target platform support

- .NET Framework 4.6.1+ (Windows x86, Windows x64)
- .NET Core 3.1+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)

## Release notes for 3.1

Full release notes, including dependencies, are available in the GitHub Repository: [3.1 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/3.1).

### New features in 3.1

#### Introduced Attestation Protocol 'None'

A new attestation protocol called `None` will be allowed in the connection string. This protocol will allow users to forgo enclave attestation for `VBS` enclaves. When this protocol is set, the enclave attestation URL property is optional.  

Connection string example:

```cs
//Attestation protocol NONE with no URL
"Data Source = {server}; Initial Catalog = {db}; Column Encryption Setting = Enabled; Attestation Protocol = None;"
```

## Target Platform Support

- .NET Framework 4.6.1+ (Windows x86, Windows x64)
- .NET Core 3.1+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)

## Release notes for 3.0

Full release notes, including dependencies, are available in the GitHub Repository: [3.0 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/3.0).

### Breaking changes in 3.0

- The minimum supported .NET Framework version has been increased to v4.6.1. .NET Framework v4.6.0 is no longer supported. [#899](https://github.com/dotnet/SqlClient/pull/899)
- `User Id` connection property now requires `Client Id` instead of `Object Id` for **User-Assigned Managed Identity** [#1010](https://github.com/dotnet/SqlClient/pull/1010) [Read more](#azure-identity-dependency-introduction)
- `SqlDataReader` now returns a `DBNull` value instead of an empty `byte[]`. Legacy behavior can be enabled by setting `AppContext` switch **Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior** [#998](https://github.com/dotnet/SqlClient/pull/998) [Read more](#enable-row-version-null-behavior).

### New features in 3.0

#### Configurable retry logic

This new feature introduces configurable support for client applications to retry on "transient" or "retriable" errors. Configuration can be done through code or app config files and retry operations can be applied to opening a connection or executing a command. This feature is disabled by default and is currently in preview. To enable this support, client applications must turn on the following safety switch:

`AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.EnableRetryLogic", true);`

Once the .NET AppContext switch is enabled, a retry logic policy can be defined for `SqlConnection` and `SqlCommand` independently, or together using various customization options.

New public APIs are introduced in `SqlConnection` and `SqlCommand` for registering a custom `SqlRetryLogicBaseProvider` implementation:

```csharp
public SqlConnection
{
    public SqlRetryLogicBaseProvider RetryLogicProvider;
}

public SqlCommand
{
    public SqlRetryLogicBaseProvider RetryLogicProvider;
}
```

API Usage examples can be found here:
[!code-csharp [SqlConnection retry sample1](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_OpenConnection.cs#1)]
[!code-csharp [SqlCommand retry sample](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_SqlCommand.cs#1)]
[!code-csharp [Sample for retry logic options](~/../sqlclient/doc/samples/SqlConfigurableRetryLogic_SqlRetryLogicOptions.cs#1)]

New configuration sections have also been introduced to do the same registration from configuration files, without having to modify existing code:

```xml
<section name="SqlConfigurableRetryLogicConnection"
            type="Microsoft.Data.SqlClient.SqlConfigurableRetryConnectionSection, Microsoft.Data.SqlClient"/>

<section name="SqlConfigurableRetryLogicCommand"
            type="Microsoft.Data.SqlClient.SqlConfigurableRetryCommandSection, Microsoft.Data.SqlClient"/>
```

Here's a simple example of using the new configuration sections in configuration files:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="SqlConfigurableRetryLogicConnection"
             type="Microsoft.Data.SqlClient.SqlConfigurableRetryConnectionSection, Microsoft.Data.SqlClient"/>
    <section name="SqlConfigurableRetryLogicCommand"
             type="Microsoft.Data.SqlClient.SqlConfigurableRetryCommandSection, Microsoft.Data.SqlClient"/>

    <section name="AppContextSwitchOverrides"
             type="Microsoft.Data.SqlClient.AppContextSwitchOverridesSection, Microsoft.Data.SqlClient"/>
  </configSections>

  <!--Enable safety switch in .NET Core-->
  <AppContextSwitchOverrides value="Switch.Microsoft.Data.SqlClient.EnableRetryLogic=true"/>

  <!--Retry method for SqlConnection-->
  <SqlConfigurableRetryLogicConnection retryMethod ="CreateFixedRetryProvider" numberOfTries ="3" deltaTime ="00:00:10" maxTime ="00:00:30"
                                    transientErrors="40615" />

  <!--Retry method for SqlCommand containing SELECT queries-->
  <SqlConfigurableRetryLogicCommand retryMethod ="CreateIncrementalRetryProvider" numberOfTries ="5" deltaTime ="00:00:10" maxTime ="00:01:10"
                                    authorizedSqlCondition="\b(SELECT)\b" transientErrors="102, 4060, 0"/>
</configuration>
```

Alternatively, applications can implement their own provider of the `SqlRetryLogicBaseProvider` base class, and register it with `SqlConnection`/`SqlCommand`.

#### Event counters

The following counters are now available for applications targeting .NET Core 3.1+ and .NET Standard 2.1+:

| Name | Display name | Description |
| --- | --- | --- |
| **active-hard-connections** | Actual active connections currently made to servers | The number of connections currently open to database servers. |
| **hard-connects** | Actual connection rate to servers | The number of connections per second being opened to database servers. |
| **hard-disconnects** | Actual disconnection rate from servers | The number of disconnects per second being made to database servers. |
| **active-soft-connects** | Active connections retrieved from the connection pool | The number of already-open connections being consumed from the connection pool. |
| **soft-connects** | Rate of connections retrieved from the connection pool | The number of connections per second being consumed from the connection pool. |
| **soft-disconnects** | Rate of connections returned to the connection pool | The number of connections per second being returned to the connection pool. |
| **number-of-non-pooled-connections** | Number of connections not using connection pooling | The number of active connections that aren't pooled. |
| **number-of-pooled-connections** | Number of connections managed by the connection pool | The number of active connections managed the connection pooling infrastructure. |
| **number-of-active-connection-pool-groups** | Number of active unique connection strings | The number of active, unique connection pool groups. This counter is based on the number of unique connection strings found in the AppDomain. |
| **number-of-inactive-connection-pool-groups** | Number of unique connection strings waiting for pruning | The number of unique connection pool groups marked for pruning. This counter is based on the number of unique connection strings found in the AppDomain. |
| **number-of-active-connection-pools** | Number of active connection pools | The total number of connection pools. |
| **number-of-inactive-connection-pools** | Number of inactive connection pools | The number of inactive connection pools with no recent activity and waiting to be disposed. |
| **number-of-active-connections** | Number of active connections | The number of active connections currently in use. |
| **number-of-free-connections** | Number of ready connections in the connection pool | The number of open connections available for use in the connection pools. |
| **number-of-stasis-connections** | Number of connections currently waiting to be ready | The number of connections currently awaiting completion of an action and which are unavailable for use by the application. |
| **number-of-reclaimed-connections** | Number of reclaimed connections from GC | The number of connections reclaimed through garbage collection where `Close` or `Dispose` wasn't called by the application. **Note** Not explicitly closing or disposing connections hurts performance. |

These counters can be used with .NET Core global CLI tools: `dotnet-counters` and `dotnet-trace` in Windows or Linux and PerfView in Windows, using `Microsoft.Data.SqlClient.EventSource` as the provider name. For more information, see [Retrieve event counter values](event-counters.md#retrieve-event-counter-values).

```console
dotnet-counters monitor Microsoft.Data.SqlClient.EventSource -p
PerfView /onlyProviders=*Microsoft.Data.SqlClient.EventSource:EventCounterIntervalSec=1 collect
```

#### Azure Identity dependency introduction

**Microsoft.Data.SqlClient** now depends on the **Azure.Identity** library to acquire tokens for "Active Directory Managed Identity/MSI" and "Active Directory Service Principal" authentication modes. This change brings the following changes to the public surface area:

- **Breaking change**

  The "User ID" connection property now requires "Client ID" instead of "Object ID" for "User-Assigned Managed Identity".

- **Public API**

  New read-only public property: `SqlAuthenticationParameters.ConnectionTimeout`

- **Dependency**

  Azure.Identity v1.3.0

#### Event tracing improvements in SNI.dll

`Microsoft.Data.SqlClient.SNI` (.NET Framework dependency) and `Microsoft.Data.SqlClient.SNI.runtime` (.NET Core/Standard dependency) versions have been updated to `v3.0.0-preview1.21104.2`. Event tracing in SNI.dll is no longer enabled through a client application. Subscribing a session to the **Microsoft.Data.SqlClient.EventSource** provider through tools like `xperf` or `perfview` is sufficient. For more information, see [Event tracing support in Native SNI](enable-eventsource-tracing.md#event-tracing-support-in-native-sni).

#### Enable row version null behavior

`SqlDataReader` returns a `DBNull` value instead of an empty `byte[]`. To enable the legacy behavior, you must enable the following AppContext switch on application startup:
**"Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior"**

#### Microsoft Entra default authentication support

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

This PR introduces a new SQL Authentication method, **Active Directory Default**. This authentication mode widens the possibilities of user authentication with Microsoft Entra ID, extending login solutions to the client environment, Visual Studio Code, Visual Studio, Azure CLI, etc.

With this authentication mode, the driver acquires a token by passing "[DefaultAzureCredential](/dotnet/api/azure.identity.defaultazurecredential)" from the Azure Identity library to acquire an access token. This mode attempts to use these credential types to acquire an access token in the following order:

- **EnvironmentCredential**
  - Enables authentication with Microsoft Entra ID using client and secret, or username and password, details configured in the following environment variables: AZURE_TENANT_ID, AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, AZURE_CLIENT_CERTIFICATE_PATH, AZURE_USERNAME, AZURE_PASSWORD ([More details](/dotnet/api/azure.identity.environmentcredential))
- **ManagedIdentityCredential**
  - Attempts authentication with Microsoft Entra ID using a managed identity that has been assigned to the deployment environment. **The "Client Id" of a "user-assigned managed identity"** is read from the **"User Id" connection property**.
- **SharedTokenCacheCredential**
  - Authenticates using tokens in the local cache shared between Microsoft applications.
- **VisualStudioCredential**
  - Enables authentication with Microsoft Entra ID using data from Visual Studio
- **VisualStudioCodeCredential**
  - Enables authentication with Microsoft Entra ID using data from Visual Studio Code.
- **AzureCliCredential**
  - Enables authentication with Microsoft Entra ID using Azure CLI to obtain an access token.

> InteractiveBrowserCredential is disabled in the driver implementation of "Active Directory Default", and "Active Directory Interactive" is the only option available to acquire a token using MFA/Interactive authentication.

> Further customization options aren't available at the moment.

#### Custom master key store provider registration enhancements

Microsoft.Data.SqlClient now offers more control of where master key store providers are accessible in an application to better support multitenant applications and their use of column encryption/decryption. The following APIs are introduced to allow registration of custom master key store providers on instances of `SqlConnection` and `SqlCommand`:

```csharp
public class SqlConnection
{
    public void RegisterColumnEncryptionKeyStoreProvidersOnConnection(IDictionary<string, SqlColumnEncryptionKeyStoreProvider> customProviders)
}
public class SqlCommand
{
    public void RegisterColumnEncryptionKeyStoreProvidersOnCommand(IDictionary<string, SqlColumnEncryptionKeyStoreProvider> customProviders)
}
```

The static API on `SqlConnection`, `SqlConnection.RegisterColumnEncryptionKeyStoreProviders`, used to register custom master key store providers globally, continues to be supported. The column encryption key cache maintained globally only applies to globally registered providers.

##### Column master key store provider registration precedence

The built-in column master key store providers that are available for the Windows Certificate Store, CNG Store, and CSP are preregistered. No providers should be registered on the connection or command instances if one of the built-in column master key store providers is needed.

Custom master key store providers can be registered with the driver at three different layers. The global level is as it currently is. The new per-connection and per-command level registrations are empty initially and can be set more than once.

The precedences of the three registrations are as follows:

- The per-command registration is checked if it isn't empty.
- If the per-command registration is empty, the per-connection registration is checked if it isn't empty.
- If the per-connection registration is empty, the global registration is checked.

Once any key store provider is found at a registration level, the driver **doesn't** fall back to the other registrations to search for a provider. If providers are registered but the proper provider isn't found at a level, an exception is thrown containing only the registered providers in the registration checked.

#### Column encryption key cache precedence

The driver doesn't cache the column encryption keys (CEKs) for custom key store providers registered using the new instance-level APIs. The key store providers need to implement their own cache to gain performance. The driver disables the local cache of column encryption keys implemented by custom key store providers if the key store provider instance is registered in the driver at the global level.

A new API has also been introduced on the `SqlColumnEncryptionKeyStoreProvider` base class to set the cache time to live:

```csharp
public abstract class SqlColumnEncryptionKeyStoreProvider
{
    // The default value of Column Encryption Key Cache Time to Live is 0.
    // Provider's local cache is disabled for globally registered providers.
    // Custom key store provider implementation must include column encryption key cache to provide caching support to locally registered providers.
    public virtual TimeSpan? ColumnEncryptionKeyCacheTtl { get; set; } = new TimeSpan(0);
}
```

#### IP address preference

A new connection property `IPAddressPreference` is introduced to specify the IP address family preference to the driver when establishing TCP connections. If `Transparent Network IP Resolution` (in .NET Framework) or `Multi Subnet Failover` is set to `true`, this setting has no effect. There are the three accepted values for this property:

- **IPv4First**
  - This value is the default. The driver uses resolved IPv4 addresses first. If none of them can be connected to successfully, it tries resolved IPv6 addresses.

- **IPv6First**
  - The driver uses resolved IPv6 addresses first. If none of them can be connected to successfully, it tries resolved IPv4 addresses.

- **UsePlatformDefault**
  - The driver tries IP addresses in the order received from the DNS resolution response.

### 3.0 Target platform support

- .NET Framework 4.6.1+ (Windows x86, Windows x64)
- .NET Core 2.1+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)

## Release notes for 2.1

Full release notes, including dependencies, are available in the GitHub Repository: [2.1 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/2.1).

### New features in 2.1

#### Cross-platform support for Always Encrypted

Microsoft.Data.SqlClient v2.1 extends support for Always Encrypted on the following platforms:

| Support Always Encrypted | Support Always Encrypted with Secure Enclave | Target framework | Microsoft.Data.SqlClient Version | Operating system |
| --- | --- | --- | ---: | ---: |
| Yes | Yes | .NET Framework 4.6+ | 1.1.0+ | Windows |
| Yes | Yes | .NET Core 2.1+ | 2.1.0+<sup>1</sup> | Windows, Linux, macOS |
| Yes | No<sup>2</sup> | .NET Standard 2.0 | 2.1.0+ | Windows, Linux, macOS |
| Yes | Yes | .NET Standard 2.1+ | 2.1.0+ | Windows, Linux, macOS |

<sup>1</sup> Before Microsoft.Data.SqlClient version v2.1, Always Encrypted is only supported on Windows.  
<sup>2</sup> Always Encrypted with secure enclaves isn't supported on .NET Standard 2.0.

<a id="azure-active-directory-device-code-flow-authentication"></a>

#### Microsoft Entra Device Code Flow authentication

Microsoft.Data.SqlClient v2.1 provides support for "Device Code Flow" authentication with MSAL.NET.
Reference documentation: [OAuth2.0 Device Authorization Grant flow](/azure/active-directory/develop/v2-oauth2-device-code)

Connection string example:

`Server=<server>.database.windows.net; Authentication=Active Directory Device Code Flow; Database=Northwind;Encrypt=True`

The following API enables customization of the Device Code Flow callback mechanism:

```csharp
public class ActiveDirectoryAuthenticationProvider
{
    // For .NET Framework, .NET Core and .NET Standard targeted applications
    public void SetDeviceCodeFlowCallback(Func<DeviceCodeResult, Task> deviceCodeFlowCallbackMethod)
}
```

<a id="azure-active-directory-managed-identity-authentication"></a>

#### Microsoft Entra managed identity authentication

Microsoft.Data.SqlClient v2.1 introduces support for Microsoft Entra authentication using [managed identities](/azure/active-directory/managed-identities-azure-resources/overview).

The following authentication mode keywords are supported:

- Active Directory Managed Identity
- Active Directory MSI (for cross MS SQL drivers compatibility)

Connection string examples:

```csharp
// For System Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory MSI; Encrypt=True; Initial Catalog={db};"

// For System Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory Managed Identity; Initial Catalog={db};"

// For User Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory MSI; Encrypt=True; User Id={ObjectIdOfManagedIdentity}; Initial Catalog={db};"

// For User Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory Managed Identity; Encrypt=True; User Id={ObjectIdOfManagedIdentity}; Initial Catalog={db};"
```

<a id="azure-active-directory-interactive-authentication-enhancements"></a>

#### Microsoft Entra Interactive authentication enhancements

Microsoft.Data.SqlClient v2.1 adds the following APIs to customize the **Microsoft Entra Interactive** authentication experience:

```csharp
public class ActiveDirectoryAuthenticationProvider
{
    // For .NET Framework targeted applications only
    public void SetIWin32WindowFunc(Func<IWin32Window> iWin32WindowFunc);

    // For .NET Standard targeted applications only
    public void SetParentActivityOrWindowFunc(Func<object> parentActivityOrWindowFunc);

    // For .NET Framework, .NET Core and .NET Standard targeted applications
    public void SetAcquireAuthorizationCodeAsyncCallback(Func<Uri, Uri, CancellationToken, Task<Uri>> acquireAuthorizationCodeAsyncCallback);

    // For .NET Framework, .NET Core and .NET Standard targeted applications
    public void ClearUserTokenCache();
}
```

#### `SqlClientAuthenticationProviders` configuration section

Microsoft.Data.SqlClient v2.1 introduces a new configuration section, `SqlClientAuthenticationProviders` (a clone of the existing `SqlAuthenticationProviders`). The existing configuration section, `SqlAuthenticationProviders`, is still supported for backward compatibility when the appropriate type is defined.

The new section allows application config files to contain both a SqlAuthenticationProviders section for System.Data.SqlClient and a SqlClientAuthenticationProviders section for Microsoft.Data.SqlClient.

<a id="azure-active-directory-authentication-using-an-application-client-id"></a>

#### Microsoft Entra authentication using an application client ID

Microsoft.Data.SqlClient v2.1 introduces support for passing a user-defined application client ID to the Microsoft Authentication Library. Application Client ID is used when authenticating with Microsoft Entra ID.

The following new APIs are introduced:

1. A new constructor has been introduced in ActiveDirectoryAuthenticationProvider:

   **Applies to**: *All .NET Platforms (.NET Framework, .NET Core, and .NET Standard)*

   ```csharp
   public ActiveDirectoryAuthenticationProvider(string applicationClientId)
   ```

   Usage:

   ```csharp
   string APP_CLIENT_ID = "<GUID>";
   SqlAuthenticationProvider customAuthProvider = new ActiveDirectoryAuthenticationProvider(APP_CLIENT_ID);
   SqlAuthenticationProvider.SetProvider(SqlAuthenticationMethod.ActiveDirectoryInteractive, customAuthProvider);

   using (SqlConnection sqlConnection = new SqlConnection("<connection_string>"))
   {
       sqlConnection.Open();
   }
   ```

1. A new configuration property has been introduced under `SqlAuthenticationProviderConfigurationSection` and `SqlClientAuthenticationProviderConfigurationSection`:

   **Applies to**: *.NET Framework and .NET Core*

   ```csharp
   internal class SqlAuthenticationProviderConfigurationSection : ConfigurationSection
   {
       ...
       [ConfigurationProperty("applicationClientId", IsRequired = false)]
       public string ApplicationClientId => this["applicationClientId"] as string;
   }

   // Inheritance
   internal class SqlClientAuthenticationProviderConfigurationSection : SqlAuthenticationProviderConfigurationSection
   { ... }
   ```

   Usage:

   ```xml
   <configuration>
       <configSections>
           <section name="SqlClientAuthenticationProviders"
                            type="Microsoft.Data.SqlClient.SqlClientAuthenticationProviderConfigurationSection, Microsoft.Data.SqlClient" />
       </configSections>
       <SqlClientAuthenticationProviders applicationClientId ="<GUID>" />
   </configuration>

   <!--or-->

   <configuration>
       <configSections>
           <section name="SqlAuthenticationProviders"
                            type="Microsoft.Data.SqlClient.SqlAuthenticationProviderConfigurationSection, Microsoft.Data.SqlClient" />
       </configSections>
       <SqlAuthenticationProviders applicationClientId ="<GUID>" />
   </configuration>
   ```

#### Data Classification v2 support

Microsoft.Data.SqlClient v2.1 introduces support for Data Classification's "Sensitivity Rank" information. The following new APIs are now available:

```csharp
public class SensitivityClassification
{
    public SensitivityRank SensitivityRank;
}

public class SensitivityProperty
{
    public SensitivityRank SensitivityRank;
}

public enum SensitivityRank
{
    NOT_DEFINED = -1,
    NONE = 0,
    LOW = 10,
    MEDIUM = 20,
    HIGH = 30,
    CRITICAL = 40
}
```

#### Session ID for an active `SqlConnection`

Microsoft.Data.SqlClient v2.1 introduces a new `SqlConnection` property, `ServerProcessId`, on an active connection.

```csharp
public class SqlConnection
{
    // Returns the session ID (SPID) of the active connection.
    public int ServerProcessId;
}
```

#### Trace logging support in native SNI

Microsoft.Data.SqlClient v2.1 extends the existing `SqlClientEventSource` implementation to enable event tracing in SNI.dll. Events must be captured using a tool like Xperf.

Tracing can be enabled by sending a command to `SqlClientEventSource` as illustrated:

```csharp
// Enables trace events:
EventSource.SendCommand(eventSource, (EventCommand)8192, null);

// Enables flow events:
EventSource.SendCommand(eventSource, (EventCommand)16384, null);

// Enables both trace and flow events:
EventSource.SendCommand(eventSource, (EventCommand)(8192 | 16384), null);
```

#### "Command Timeout" connection string property

Microsoft.Data.SqlClient v2.1 introduces the "Command Timeout" connection string property to override the default of 30 seconds. The timeout for individual commands can be overridden using the `CommandTimeout` property on the SqlCommand.

Connection string examples:

`"Server={serverURL}; Initial Catalog={db}; Encrypt=True; Integrated Security=true; Command Timeout=60"`

#### Removal of symbols from native SNI

With Microsoft.Data.SqlClient v2.1, we've removed the symbols introduced in [v2.0.0](https://www.nuget.org/packages/Microsoft.Data.SqlClient.SNI/2.0.0) from [Microsoft.Data.SqlClient.SNI.runtime](https://www.nuget.org/packages/Microsoft.Data.SqlClient.SNI.runtime) NuGet starting with [v2.1.1](https://www.nuget.org/packages/Microsoft.Data.SqlClient.SNI.runtime/2.1.1). The public symbols are now published to Microsoft Symbols Server for tools like BinSkim that require access to public symbols.

#### Source-linking of Microsoft.Data.SqlClient symbols

Starting with Microsoft.Data.SqlClient v2.1, Microsoft.Data.SqlClient symbols are source-linked and published to the Microsoft Symbols Server for an enhanced debugging experience without the need to download source code.

### 2.1 Target platform support

- .NET Framework 4.6+ (Windows x86, Windows x64)
- .NET Core 2.1+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)

## Release notes for 2.0

Full release notes, including dependencies, are available in the GitHub Repository: [2.0 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/2.0).

### Breaking changes in 2.0

- The access modifier for the enclave provider interface `SqlColumnEncryptionEnclaveProvider` has been changed from `public` to `internal`.
- Constants in the `SqlClientMetaDataCollectionNames` class have been updated to reflect changes in SQL Server.
- The driver now performs Server Certificate validation when the target SQL Server enforces TLS encryption, which is the default for Azure connections.
- `SqlDataReader.GetSchemaTable()` now returns an empty `DataTable` instead `null`.
- The driver now performs decimal scale rounding to match SQL Server behavior. For backward compatibility, the previous behavior of truncation can be enabled using an AppContext switch.
- For .NET Framework applications consuming **Microsoft.Data.SqlClient**, the SNI.dll files previously downloaded to the `bin\x64` and `bin\x86` folders are now named `Microsoft.Data.SqlClient.SNI.x64.dll` and `Microsoft.Data.SqlClient.SNI.x86.dll` and are downloaded to the `bin` directory.
- New connection string property synonyms replace old properties when fetching connection string from `SqlConnectionStringBuilder` for consistency. [Read More](#new-connection-string-property-synonyms)

### New features in 2.0

The following new features have been introduced in Microsoft.Data.SqlClient 2.0.

#### DNS failure resiliency

The driver now caches IP addresses from every successful connection to a SQL Server endpoint that supports the feature. If a DNS resolution failure occurs during a connection attempt, the driver tries establishing a connection using a cached IP address for that server, if any exists.

#### EventSource tracing

This release introduces support for capturing event trace logs for debugging applications. To capture these events, client applications must listen for events from SqlClient's EventSource implementation:

```csharp
Microsoft.Data.SqlClient.EventSource
```

For more information, see how to [Enable event tracing in SqlClient](enable-eventsource-tracing.md).

#### Enable managed networking on Windows

A new AppContext switch, **"Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows"**, enables the use of a managed SNI implementation on Windows for testing and debugging purposes. This switch toggles the driver's behavior to use a managed SNI in .NET Core 2.1+ and .NET Standard 2.0+ projects on Windows, eliminating all dependencies on native libraries for the Microsoft.Data.SqlClient library.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows", true);
```

See [AppContext switches in SqlClient](appcontext-switches.md) for a full list of available switches in the driver.

#### Enable decimal truncation behavior

The driver rounds the decimal data scale, by default, as is done by SQL Server. For backward compatibility, you can set the AppContext switch **"Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal"** to **true**.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal", true);
```

#### New connection string property synonyms

New synonyms have been added for the following existing connection string properties to avoid spacing confusion around properties with more than one word. Old property names continue to be supported for backward compatibility. But the new connection string properties are now included when fetching the connection string from [SqlConnectionStringBuilder](/dotnet/api/microsoft.data.sqlclient.sqlconnectionstringbuilder).

| Existing connection string property | New synonym |
| --- | --- |
| ApplicationIntent | Application Intent |
| ConnectRetryCount | Connect Retry Count |
| ConnectRetryInterval | Connect Retry Interval |
| PoolBlockingPeriod | Pool Blocking Period |
| MultipleActiveResultSets | Multiple Active Result Sets |
| MultiSubnetFailover | Multiple Subnet Failover |
| TransparentNetworkIPResolution | Transparent Network IP Resolution |
| TrustServerCertificate | Trust Server Certificate |

#### SqlBulkCopy RowsCopied property

The RowsCopied property provides read-only access to the number of rows that have been processed in the ongoing bulk copy operation. This value might not necessarily be equal to the final number of rows added to the destination table.

#### Connection open overrides

The default behavior of SqlConnection.Open() can be overridden to disable the ten-second delay and automatic connection retries triggered by transient errors.

```csharp
using(SqlConnection sqlConnection = new SqlConnection("Data Source=(local);Integrated Security=true;Initial Catalog=AdventureWorks;"))
{
    sqlConnection.Open(SqlConnectionOverrides.OpenWithoutRetry);
}
```

> [!NOTE]  
> This override can be applied to `SqlConnection.OpenAsync()` starting with Microsoft.Data.SqlClient v6.0.0.

#### Username support for Active Directory Interactive mode

A username can be specified in the connection string when using Microsoft Entra Interactive authentication mode for both .NET Framework and .NET Core

Set a username using the **User ID** or **UID** connection string property:

```csharp
"Server=<server name>; Database=<db name>; Authentication=Active Directory Interactive; User Id=<username>;Encrypt=True;"
```

#### Order hints for SqlBulkCopy

Order hints can be provided to improve performance for bulk copy operations on tables with clustered indexes. For more information, see the [bulk copy operations](sql/bulk-copy-order-hints.md) section.

#### SNI dependency changes

Microsoft.Data.SqlClient (.NET Core and .NET Standard) on Windows is now dependent on **Microsoft.Data.SqlClient.SNI.runtime**, replacing the previous dependency on **runtime.native.System.Data.SqlClient.SNI**. The new dependency adds support for the ARM platform along with the already supported platforms Arm64, x64, and x86 on Windows.

### 2.0 Target platform support

- .NET Framework 4.6+ (Windows x86, Windows x64)
- .NET Core 2.1+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows Arm64, Windows ARM, Linux, macOS)

## Release notes for 1.1

Full release notes, including dependencies, are available in the GitHub Repository: [1.1 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/1.1).

### New features in 1.1

#### Always Encrypted with secure enclaves

Always Encrypted is available starting in Microsoft SQL Server 2016. Secure enclaves are available starting in Microsoft SQL Server 2019. To use the enclave feature, connection strings should include the required attestation protocol and attestation URL. For example:

```csharp
"Attestation Protocol=HGS;Enclave Attestation Url=<attestation_url_for_HGS>"
```

For more information, see:

- [Using Always Encrypted with the Microsoft .NET Data Provider for SQL Server](sql/sqlclient-support-always-encrypted.md)
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](sql/tutorial-always-encrypted-enclaves-develop-net-apps.md)

### 1.1 Target platform support

- .NET Framework 4.6+ (Windows x86, Windows x64)
- .NET Core 2.1+ (Windows x86, Windows x64, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Linux, macOS)

## Release notes for 1.0

The initial release for the Microsoft.Data.SqlClient namespace offers more functionality over the existing System.Data.SqlClient namespace.

Full release notes, including dependencies, are available in the GitHub Repository: [1.0 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/1.0).

### New features in 1.0

#### New features over .NET Framework 4.7.2 System.Data.SqlClient

- **Data Classification** - Available in Azure SQL Database and Microsoft SQL Server 2019.

- **UTF-8 support** - Available in Microsoft SQL Server 2019.

#### New features over .NET Core 2.2 System.Data.SqlClient

- **Data Classification** - Available in Azure SQL Database and Microsoft SQL Server 2019.

- **UTF-8 support** - Available in Microsoft SQL Server 2019.

- **Authentication** - Active Directory Password authentication mode.

#### Data Classification

Data Classification brings a new set of APIs exposing read-only Data Sensitivity and Classification information about objects retrieved via SqlDataReader when the underlying source supports the feature and contains metadata about [data sensitivity and classification](../../relational-databases/security/sql-data-discovery-and-classification.md). See the sample application at [Data Discovery and Classification in SqlClient](https://github.com/dotnet/SqlClient/tree/master/release-notes/1.1).

```csharp
public class SqlDataReader
{
    public Microsoft.Data.SqlClient.DataClassification.SensitivityClassification SensitivityClassification
}

namespace Microsoft.Data.SqlClient.DataClassification
{
    public class ColumnSensitivity
    {
        public System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.Data.SqlClient.DataClassification.SensitivityProperty> SensitivityProperties
    }
    public class InformationType
    {
        public string Id
        public string Name
    }
    public class Label
    {
        public string Id
        public string Name
    }
    public class SensitivityClassification
    {
        public System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.Data.SqlClient.DataClassification.ColumnSensitivity> ColumnSensitivities
        public System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.Data.SqlClient.DataClassification.InformationType> InformationTypes
        public System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.Data.SqlClient.DataClassification.Label> Labels
    }
    public class SensitivityProperty
    {
        public Microsoft.Data.SqlClient.DataClassification.InformationType InformationType
        public Microsoft.Data.SqlClient.DataClassification.Label Label
    }
}
```

#### UTF-8 support

UTF-8 support doesn't require any application code changes. These SqlClient changes optimize client-server communication when the server supports UTF-8 and the underlying column collation is UTF-8. See the UTF-8 section under [What's new in SQL Server 2019](../../sql-server/what-s-new-in-sql-server-2019.md).

#### Always Encrypted with secure enclaves

In general, existing documentation that uses System.Data.SqlClient on .NET Framework **and built-in column master key store providers** should now work with .NET Core, too.

- [Using Always Encrypted with the .NET Framework Data Provider for SQL Server](../../relational-databases/security/encryption/develop-using-always-encrypted-with-net-framework-data-provider.md)
- [Always Encrypted: Protect sensitive data and store encryption keys in the Windows certificate store](/azure/sql-database/sql-database-always-encrypted)

#### Authentication

Different authentication modes can be specified by using the **Authentication** connection string option. For more information, see the [documentation for SqlAuthenticationMethod](/dotnet/api/system.data.sqlclient.sqlauthenticationmethod?view=netframework-4.7.2&preserve-view=true).

Custom key store providers, like the Azure Key Vault provider, need to be updated to support Microsoft.Data.SqlClient. Similarly, enclave providers also need to be updated to support Microsoft.Data.SqlClient.

Always Encrypted is only supported against .NET Framework and .NET Core targets. It isn't supported against .NET Standard since .NET Standard is missing certain encryption dependencies.

### 1.0 Target platform support

- .NET Framework 4.6+ (Windows x86, Windows x64)
- .NET Core 2.1+ (Windows x86, Windows x64, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Linux, macOS)
