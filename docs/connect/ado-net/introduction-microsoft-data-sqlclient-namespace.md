---
title: Introduction to Microsoft.Data.SqlClient namespace
description: Learn about the Microsoft.Data.SqlClient namespace and how it's the preferred way to connect to SQL for .NET applications.
author: David-Engel
ms.author: v-davidengel
ms.date: 07/26/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Introduction to Microsoft.Data.SqlClient namespace

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The Microsoft.Data.SqlClient namespace is essentially a new version of the System.Data.SqlClient namespace. Microsoft.Data.SqlClient generally maintains the same API and backwards compatibility with System.Data.SqlClient. To migrate from System.Data.SqlClient to Microsoft.Data.SqlClient, for most applications, it's simple. Add a NuGet dependency on Microsoft.Data.SqlClient and update references and `using` statements to Microsoft.Data.SqlClient.

There are a few differences in less-used APIs compared to System.Data.SqlClient that may affect some applications. For those differences, see this useful [porting cheat sheet](https://github.com/dotnet/SqlClient/blob/main/porting-cheat-sheet.md).

## API reference

The Microsoft.Data.SqlClient API details can be found in the [.NET API Browser](/dotnet/api/microsoft.data.sqlclient).

## Release notes for Microsoft.Data.SqlClient 5.0

### Breaking changes in 5.0

- Dropped support for .NET Framework 4.6.1 [#1574](https://github.com/dotnet/SqlClient/pull/1574)
- Added a dependency on the [Microsoft.SqlServer.Server](https://github.com/dotnet/SqlClient/tree/main/src/Microsoft.SqlServer.Server) package. This new dependency may cause namespace conflicts if your application references that namespace and still has package references (direct or indirect) to System.Data.SqlClient from .NET Core.
- Dropped classes from the `Microsoft.Data.SqlClient.Server` namespace and replaced them with supported types from the [Microsoft.SqlServer.Server](https://github.com/dotnet/SqlClient/tree/main/src/Microsoft.SqlServer.Server) package.[#1585](https://github.com/dotnet/SqlClient/pull/1585).
The affected classes and enums are:
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

- Added support for `TDS8`. To use TDS 8, users should specify Encrypt=Strict in the connection string. [#1608](https://github.com/dotnet/SqlClient/pull/1608) [Read more](#tds-8-enhanced-security)
- Added support for specifying Server SPN and Failover Server SPN on the connection. [#1607](https://github.com/dotnet/SqlClient/pull/1607) [Read more](#server-spn)
- Added support for aliases when targeting .NET Core on Windows. [#1588](https://github.com/dotnet/SqlClient/pull/1588) [Read more](#support-for-sql-aliases)
- Added SqlDataSourceEnumerator. [#1430](https://github.com/dotnet/SqlClient/pull/1430), [Read more](#sql-data-source-enumerator-support)
- Added a new AppContext switch to suppress insecure TLS warnings. [#1457](https://github.com/dotnet/SqlClient/pull/1457), [Read more](#suppress-insecure-tls-warnings)

### TDS 8 enhanced security

To use TDS 8, specify Encrypt=Strict in the connection string. Strict mode disables TrustServerCertificate (always treated as False in Strict mode). HostNameInCertificate has been added to help some Strict mode scenarios. TDS 8 begins and continues all server communication inside a secure, encrypted TLS connection.

New Encrypt values have been added to clarify connection encryption behavior. `Encrypt=Mandatory` is equivalent to `Encrypt=True` and encrypts connections during the TDS connection negotiation. `Encrypt=Optional` is equivalent to `Encrypt=False` and only encrypts the connection if the server tells the client that encryption is required during the TDS connection negotiation.

`HostNameInCertificate` can be specified in the connection string when using aliases to connect with encryption to a server that has a server certificate with a different name or alternate subject name than the name used by the client to identify the server (DNS aliases, for example). Example usage: `HostNameInCertificate=MyDnsAliasName`

### Server SPN

When connecting in an environment that has unique domain/forest topography, you might have specific requirements for Server SPNs. The ServerSPN/Server SPN and FailoverServerSPN/Failover Server SPN connection string settings can be used to override the auto-generated server SPNs used during integrated authentication in a domain environment

### Support for SQL aliases

Users can configure Aliases by using the SQL Server Configuration Manager. These aliases are stored in the Windows registry and are already supported when targeting .NET Framework. This release brings support for aliases when targeting .NET or .NET Core on Windows.

### SQL Data Source Enumerator support

Provides a mechanism for enumerating all available instances of SQL Server within the local network.

```cs
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

### Suppress insecure TLS warnings

A security warning is output on the console if the TLS version less than 1.2 is used to negotiate with the server. This warning could be suppressed on SQL connection while `Encrypt = false` by enabling the following AppContext switch on the application startup:

```cs
Switch.Microsoft.Data.SqlClient.SuppressInsecureTLSWarning
```

## 5.0 Target platform support

- .NET Framework 4.6.2+ (Windows x86, Windows x64)
- .NET Core 3.1+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)

Full release notes, including dependencies, are available in the GitHub Repository: [5.0 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/5.0).

## Release notes for Microsoft.Data.SqlClient 4.1

Full release notes, including dependencies, are available in the GitHub Repository: [4.1 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/4.1).

### New features in 4.1

### Introduce Attestation Protocol None

A new attestation protocol called `None` will be allowed in the connection string. This protocol will allow users to forgo enclave attestation for `VBS` enclaves. When this protocol is set, the enclave attestation URL property is optional.  

Connection string example:

```cs
//Attestation protocol NONE with no URL
"Data Source = {server}; Initial Catalog = {db}; Column Encryption Setting = Enabled; Attestation Protocol = None;"

```

### 4.1 Target Platform Support

- .NET Framework 4.6.1+ (Windows x86, Windows x64)
- .NET Core 3.1+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)

## Release notes for Microsoft.Data.SqlClient 4.0

Full release notes, including dependencies, are available in the GitHub Repository: [4.0 Release Notes](https://github.com/dotnet/SqlClient/tree/main/release-notes/4.0).

### Breaking changes in 4.0

- Changed `Encrypt` connection string property to be `true` by default. [#1210](https://github.com/dotnet/SqlClient/pull/1210) [Read more](#encrypt-default-value-set-to-true)
- The driver now throws `SqlException` replacing `AggregateException` for active directory authentication modes. [#1213](https://github.com/dotnet/SqlClient/pull/1213)
- Dropped obsolete `Asynchronous Processing` connection property from .NET Framework. [#1148](https://github.com/dotnet/SqlClient/pull/1148)
- Removed `Configurable Retry Logic` safety switch. [#1254](https://github.com/dotnet/SqlClient/pull/1254) [Read more](#remove-configurable-retry-logic-safety-switch)
- Dropped support for .NET Core 2.1 [#1272](https://github.com/dotnet/SqlClient/pull/1272)
- [.NET Framework] Exception won't be thrown if a User ID is provided in the connection string when using `Active Directory Integrated` authentication [#1359](https://github.com/dotnet/SqlClient/pull/1359)

### New features in 4.0

### Encrypt default value set to true

The default value of the `Encrypt` connection setting has been changed from `false` to `true`. With the growing use of cloud databases and the need to ensure those connections are secure, it's time for this backwards-compatibility-breaking change.

### Ensure connections fail when encryption is required

In scenarios where client encryption libraries were disabled or unavailable, it was possible for unencrypted connections to be made when Encrypt was set to true or the server required encryption.

### App Context Switch for using System default protocols

TLS 1.3 isn't supported by the driver; therefore, it has been removed from the supported protocols list by default. Users can switch back to forcing use of the Operating System's client protocols, by enabling the App Context switch below:

 `Switch.Microsoft.Data.SqlClient.UseSystemDefaultSecureProtocols`

### Enable optimized parameter binding

Microsoft.Data.SqlClient introduces a new `SqlCommand` API, `EnableOptimizedParameterBinding` to improve performance of queries with large number of parameters. This property is disabled by default. When set to `true`, parameter names won't be sent to the SQL server when the command is executed.

```cs
public class SqlCommand
{
    public bool EnableOptimizedParameterBinding { get; set; }
}
```

### Remove configurable retry logic safety switch

The App Context switch "Switch.Microsoft.Data.SqlClient.EnableRetryLogic" will no longer be required to use the configurable retry logic feature. The feature is now supported in production. The default behavior of the feature will continue to be a non-retry policy, which will need to be overridden by client applications to enable retries.

### SqlLocalDb shared instance support

SqlLocalDb shared instances are now supported when using Managed SNI.

- Possible scenarios:
  - `(localdb)\.` (connects to default instance of SqlLocalDb)
  - `(localdb)\<named instance>`
  - `(localdb)\.\<shared instance name>` (*newly added support)

### `GetFieldValueAsync<T>` and `GetFieldValue<T>` support for `XmlReader`, `TextReader`, `Stream` types

`XmlReader`, `TextReader`, `Stream` types are now supported when using `GetFieldValueAsync<T>` and `GetFieldValue<T>`.

Example usage:

```cs
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

### 4.0 Target Platform Support

- .NET Framework 4.6.1+ (Windows x86, Windows x64)
- .NET Core 3.1+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)

## Release notes for Microsoft.Data.SqlClient 3.0

Full release notes, including dependencies, are available in the GitHub Repository: [3.0 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/3.0).

### Breaking changes in 3.0

- The minimum supported .NET Framework version has been increased to v4.6.1. .NET Framework v4.6.0 is no longer supported. [#899](https://github.com/dotnet/SqlClient/pull/899)
- `User Id` connection property now requires `Client Id` instead of `Object Id` for **User-Assigned Managed Identity** [#1010](https://github.com/dotnet/SqlClient/pull/1010) [Read more](#azure-identity-dependency-introduction)
- `SqlDataReader` now returns a `DBNull` value instead of an empty `byte[]`. Legacy behavior can be enabled by setting `AppContext` switch **Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior** [#998](https://github.com/dotnet/SqlClient/pull/998) [Read more](#enabling-row-version-null-behavior)

### New features in 3.0

### Configurable Retry Logic

This new feature introduces configurable support for client applications to retry on "transient" or "retriable" errors. Configuration can be done through code or app config files and retry operations can be applied to opening a connection or executing a command. This feature is disabled by default and is currently in preview. To enable this support, client applications must turn on the following safety switch:

`AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.EnableRetryLogic", true);`

Once the .NET AppContext switch is enabled, a retry logic policy can be defined for `SqlConnection` and `SqlCommand` independently, or together using various customization options.

New public APIs are introduced in `SqlConnection` and `SqlCommand` for registering a custom `SqlRetryLogicBaseProvider` implementation:

```cs
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

A simple example of using the new configuration sections in configuration files is below:

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

### Event Counters

The following counters are now available for applications targeting .NET Core 3.1+ and .NET Standard 2.1+:

|Name|Display name|Description|  
|-------------------------|-----------------|-----------------|  
|**active-hard-connections**|Actual active connections currently made to servers|The number of connections that are currently open to database servers.|
|**hard-connects**|Actual connection rate to servers|The number of connections per second that are being opened to database servers.|
|**hard-disconnects**|Actual disconnection rate from servers|The number of disconnects per second that are being made to database servers.|
|**active-soft-connects**|Active connections retrieved from the connection pool|The number of already-open connections being consumed from the connection pool.|
|**soft-connects**|Rate of connections retrieved from the connection pool|The number of connections per second that are being consumed from the connection pool.|
|**soft-disconnects**|Rate of connections returned to the connection pool|The number of connections per second that are being returned to the connection pool.|
|**number-of-non-pooled-connections**|Number of connections not using connection pooling|The number of active connections that aren't pooled.|
|**number-of-pooled-connections**|Number of connections managed by the connection pool|The number of active connections that are being managed by the connection pooling infrastructure.|
|**number-of-active-connection-pool-groups**|Number of active unique connection strings|The number of unique connection pool groups that are active. This counter is controlled by the number of unique connection strings that are found in the AppDomain.|
|**number-of-inactive-connection-pool-groups**|Number of unique connection strings waiting for pruning|The number of unique connection pool groups that are marked for pruning. This counter is controlled by the number of unique connection strings that are found in the AppDomain.|
|**number-of-active-connection-pools**|Number of active connection pools|The total number of connection pools.|
|**number-of-inactive-connection-pools**|Number of inactive connection pools|The number of inactive connection pools that haven't had any recent activity and are waiting to be disposed.|
|**number-of-active-connections**|Number of active connections|The number of active connections that are currently in use.|
|**number-of-free-connections**|Number of ready connections in the connection pool|The number of open connections available for use in the connection pools.|
|**number-of-stasis-connections**|Number of connections currently waiting to be ready|The number of connections currently awaiting completion of an action and which are unavailable for use by the application.|
|**number-of-reclaimed-connections**|Number of reclaimed connections from GC|The number of connections that have been reclaimed through garbage collection where `Close` or `Dispose` wasn't called by the application. **Note** Not explicitly closing or disposing connections hurts performance.|

These counters can be used with .NET Core global CLI tools: `dotnet-counters` and `dotnet-trace` in Windows or Linux and PerfView in Windows, using `Microsoft.Data.SqlClient.EventSource` as the provider name. For more information, see [Retrieve event counter values](event-counters.md#retrieve-event-counter-values).

```cmd
dotnet-counters monitor Microsoft.Data.SqlClient.EventSource -p
PerfView /onlyProviders=*Microsoft.Data.SqlClient.EventSource:EventCounterIntervalSec=1 collect
```

### Azure Identity dependency introduction

**Microsoft.Data.SqlClient** now depends on the **Azure.Identity** library to acquire tokens for "Active Directory Managed Identity/MSI" and "Active Directory Service Principal" authentication modes. This change brings the following changes to the public surface area:

- **Breaking Change**  
  The "User Id" connection property now requires "Client Id" instead of "Object Id" for "User-Assigned Managed Identity".  
- **Public API**  
  New read-only public property: `SqlAuthenticationParameters.ConnectionTimeout`
- **Dependency**  
  Azure.Identity v1.3.0

### Event tracing improvements in SNI.dll

`Microsoft.Data.SqlClient.SNI` (.NET Framework dependency) and `Microsoft.Data.SqlClient.SNI.runtime` (.NET Core/Standard dependency) versions have been updated to `v3.0.0-preview1.21104.2`. Event tracing in SNI.dll will no longer be enabled through a client application. Subscribing a session to the **Microsoft.Data.SqlClient.EventSource** provider through tools like `xperf` or `perfview` will be sufficient. For more information, see [Event tracing support in Native SNI](enable-eventsource-tracing.md#event-tracing-support-in-native-sni).

### Enabling row version null behavior

`SqlDataReader` returns a `DBNull` value instead of an empty `byte[]`. To enable the legacy behavior, you must enable the following AppContext switch on application startup:
**"Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior"**

### Active Directory Default authentication support

This PR introduces a new SQL Authentication method, **Active Directory Default**. This authentication mode widens the possibilities of user authentication, extending login solutions to the client environment, Visual Studio Code, Visual Studio, Azure CLI etc.

With this authentication mode, the driver acquires a token by passing "[DefaultAzureCredential](/dotnet/api/azure.identity.defaultazurecredential)" from the Azure Identity library to acquire an access token. This mode attempts to use these credential types to acquire an access token in the following order:

- **EnvironmentCredential**
  - Enables authentication to Azure Active Directory using client and secret, or username and password, details configured in the following environment variables: AZURE_TENANT_ID, AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, AZURE_CLIENT_CERTIFICATE_PATH, AZURE_USERNAME, AZURE_PASSWORD ([More details](/dotnet/api/azure.identity.environmentcredential))
- **ManagedIdentityCredential**
  - Attempts authentication to Azure Active Directory using a managed identity that has been assigned to the deployment environment. **"Client Id" of "User Assigned Managed Identity"** is read from the **"User Id" connection property**.
- **SharedTokenCacheCredential**
  - Authenticates using tokens in the local cache shared between Microsoft applications.
- **VisualStudioCredential**
  - Enables authentication to Azure Active Directory using data from Visual Studio
- **VisualStudioCodeCredential**
  - Enables authentication to Azure Active Directory using data from Visual Studio Code.
- **AzureCliCredential**
  - Enables authentication to Azure Active Directory using Azure CLI to obtain an access token.

> InteractiveBrowserCredential is disabled in the driver implementation of "Active Directory Default", and "Active Directory Interactive" is the only option available to acquire a token using MFA/Interactive authentication.*

> Further customization options are not available at the moment.

### Custom master key store provider registration enhancements

Microsoft.Data.SqlClient now offers more control of where master key store providers are accessible in an application to better support multi-tenant applications and their use of column encryption/decryption. The following APIs are introduced to allow registration of custom master key store providers on instances of `SqlConnection` and `SqlCommand`:

```cs
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

#### Column master key store provider registration precedence

The built-in column master key store providers that are available for the Windows Certificate Store, CNG Store and CSP are pre-registered. No providers should be registered on the connection or command instances if one of the built-in column master key store providers is needed.

Custom master key store providers can be registered with the driver at three different layers. The global level is as it currently is. The new per-connection and per-command level registrations will be empty initially and can be set more than once.

The precedences of the three registrations are as follows:

- The per-command registration will be checked if it isn't empty.
- If the per-command registration is empty, the per-connection registration will be checked if it isn't empty.
- If the per-connection registration is empty, the global registration will be checked.

Once any key store provider is found at a registration level, the driver will **NOT** fall back to the other registrations to search for a provider. If providers are registered but the proper provider isn't found at a level, an exception will be thrown containing only the registered providers in the registration that was checked.

#### Column encryption key cache precedence

The column encryption keys (CEKs) for custom key store providers registered using the new instance-level APIs won't be cached by the driver. The key store providers need to implement their own cache to gain performance. This local cache of column encryption keys implemented by custom key store providers will be disabled by the driver if the key store provider instance is registered in the driver at the global level.

A new API has also been introduced on the `SqlColumnEncryptionKeyStoreProvider` base class to set the cache time to live:

```cs
public abstract class SqlColumnEncryptionKeyStoreProvider
{
    // The default value of Column Encryption Key Cache Time to Live is 0.
    // Provider's local cache is disabled for globally registered providers.
    // Custom key store provider implementation must include column encryption key cache to provide caching support to locally registered providers.
    public virtual TimeSpan? ColumnEncryptionKeyCacheTtl { get; set; } = new TimeSpan(0);
}
```

### IP Address preference

A new connection property `IPAddressPreference` is introduced to specify the IP address family preference to the driver when establishing TCP connections. If `Transparent Network IP Resolution` (in .NET Framework) or `Multi Subnet Failover` is set to `true`, this setting has no effect. Below are the three accepted values for this property:

- **IPv4First**
  - This value is the default. The driver will use resolved IPv4 addresses first. If none of them can be connected to successfully, it will try resolved IPv6 addresses.

- **IPv6First**
  - The driver will use resolved IPv6 addresses first. If none of them can be connected to successfully, it will try resolved IPv4 addresses.

- **UsePlatformDefault**
  - The driver will try IP addresses in the order received from the DNS resolution response.

### 3.0 Target Platform Support

- .NET Framework 4.6.1+ (Windows x86, Windows x64)
- .NET Core 2.1+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)

## Release notes for Microsoft.Data.SqlClient 2.1

Full release notes, including dependencies, are available in the GitHub Repository: [2.1 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/2.1).

### New features in 2.1

### Cross-Platform support for Always Encrypted

Microsoft.Data.SqlClient v2.1 extends support for Always Encrypted on the following platforms:

| Support Always Encrypted | Support Always Encrypted with Secure Enclave  | Target Framework | Microsoft.Data.SqlClient Version | Operating System |
|:--|:--|:--|:--:|:--:|
| Yes | Yes | .NET Framework 4.6+ | 1.1.0+ | Windows |
| Yes | Yes | .NET Core 2.1+ | 2.1.0+<sup>1</sup> | Windows, Linux, macOS |
| Yes | No<sup>2</sup> | .NET Standard 2.0 | 2.1.0+ | Windows, Linux, macOS |
| Yes | Yes | .NET Standard 2.1+ | 2.1.0+ | Windows, Linux, macOS |

> [!NOTE]
> <sup>1</sup> Before Microsoft.Data.SqlClient version v2.1, Always Encrypted is only supported on Windows.
> <sup>2</sup> Always Encrypted with secure enclaves is not supported on .NET Standard 2.0.

### Azure Active Directory Device Code Flow authentication

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

### Azure Active Directory Managed Identity authentication

Microsoft.Data.SqlClient v2.1 introduces support for Azure Active Directory authentication using [managed identities](/azure/active-directory/managed-identities-azure-resources/overview).

The following authentication mode keywords are supported:

- Active Directory Managed Identity
- Active Directory MSI (for cross MS SQL drivers compatibility)

Connection string examples:

```cs
// For System Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory MSI; Encrypt=True; Initial Catalog={db};"

// For System Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory Managed Identity; Initial Catalog={db};"

// For User Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory MSI; Encrypt=True; User Id={ObjectIdOfManagedIdentity}; Initial Catalog={db};"

// For User Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory Managed Identity; Encrypt=True; User Id={ObjectIdOfManagedIdentity}; Initial Catalog={db};"
```

### Azure Active Directory Interactive authentication enhancements

Microsoft.Data.SqlClient v2.1 adds the following APIs to customize the "Active Directory Interactive" authentication experience:

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

### `SqlClientAuthenticationProviders` configuration section

Microsoft.Data.SqlClient v2.1 introduces a new configuration section, `SqlClientAuthenticationProviders` (a clone of the existing `SqlAuthenticationProviders`). The existing configuration section, `SqlAuthenticationProviders`, is still supported for backwards compatibility when the appropriate type is defined.

The new section allows application config files to contain both a SqlAuthenticationProviders section for System.Data.SqlClient and a SqlClientAuthenticationProviders section for Microsoft.Data.SqlClient.

### Azure Active Directory authentication using an application client ID

Microsoft.Data.SqlClient v2.1 introduces support for passing a user-defined application client ID to the Microsoft Authentication Library. Application Client ID is used when authenticating with Azure Active Directory.

The following new APIs are introduced:

1. A new constructor has been introduced in ActiveDirectoryAuthenticationProvider:\
_[Applies to all .NET Platforms (.NET Framework, .NET Core, and .NET Standard)]_

    ```csharp
    public ActiveDirectoryAuthenticationProvider(string applicationClientId)
    ```

    Usage:

    ```csharp
    string APP_CLIENT_ID = "<GUID>";
    SqlAuthenticationProvider customAuthProvider = new ActiveDirectoryAuthenticationProvider(APP_CLIENT_ID);
    SqlAuthenticationProvider.SetProvider(SqlAuthenticationMethod.ActiveDirectoryInteractive, customAuthProvider);
    
    using (SqlConnection sqlConnection = new SqlConnection("<connection_string>")
    {
        sqlConnection.Open();
    }
    ```

2. A new configuration property has been introduced under `SqlAuthenticationProviderConfigurationSection` and `SqlClientAuthenticationProviderConfigurationSection`:\
_[Applies to .NET Framework and .NET Core]_

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

### Data Classification v2 support

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

### Server Process ID for an active `SqlConnection`

Microsoft.Data.SqlClient v2.1 introduces a new `SqlConnection` property, `ServerProcessId`, on an active connection.

```csharp
public class SqlConnection
{
    // Returns the server process Id (SPID) of the active connection.
    public int ServerProcessId;
}
```

### Trace Logging support in Native SNI

Microsoft.Data.SqlClient v2.1 extends the existing `SqlClientEventSource` implementation to enable event tracing in SNI.dll. Events must be captured using a tool like Xperf.

Tracing can be enabled by sending a command to `SqlClientEventSource` as illustrated below:

```csharp
// Enables trace events:
EventSource.SendCommand(eventSource, (EventCommand)8192, null);

// Enables flow events:
EventSource.SendCommand(eventSource, (EventCommand)16384, null);

// Enables both trace and flow events:
EventSource.SendCommand(eventSource, (EventCommand)(8192 | 16384), null);
```

### "Command Timeout" connection string property

Microsoft.Data.SqlClient v2.1 introduces the "Command Timeout" connection string property to override the default of 30 seconds. The timeout for individual commands can be overridden using the `CommandTimeout` property on the SqlCommand.

Connection string examples:

`"Server={serverURL}; Initial Catalog={db}; Encrypt=True; Integrated Security=true; Command Timeout=60"`

### Removal of symbols from Native SNI

With Microsoft.Data.SqlClient v2.1, we've removed the symbols introduced in [v2.0.0](https://www.nuget.org/packages/Microsoft.Data.SqlClient.SNI/2.0.0) from [Microsoft.Data.SqlClient.SNI.runtime](https://www.nuget.org/packages/Microsoft.Data.SqlClient.SNI.runtime) NuGet starting with [v2.1.1](https://www.nuget.org/packages/Microsoft.Data.SqlClient.SNI.runtime/2.1.1). The public symbols are now published to Microsoft Symbols Server for tools like BinSkim that require access to public symbols.

### Source-Linking of Microsoft.Data.SqlClient symbols

Starting with Microsoft.Data.SqlClient v2.1, Microsoft.Data.SqlClient symbols are source-linked and published to the Microsoft Symbols Server for an enhanced debugging experience without the need to download source code.

### 2.1 Target Platform Support

- .NET Framework 4.6+ (Windows x86, Windows x64)
- .NET Core 2.1+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)

## Release notes for Microsoft.Data.SqlClient 2.0

Full release notes, including dependencies, are available in the GitHub Repository: [2.0 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/2.0).

### Breaking changes in 2.0

- The access modifier for the enclave provider interface `SqlColumnEncryptionEnclaveProvider` has been changed from `public` to `internal`.
- Constants in the `SqlClientMetaDataCollectionNames` class have been updated to reflect changes in SQL Server.
- The driver will now perform Server Certificate validation when the target SQL Server enforces TLS encryption, which is the default for Azure connections.
- `SqlDataReader.GetSchemaTable()` now returns an empty `DataTable` instead `null`.
- The driver now performs decimal scale rounding to match SQL Server behavior. For backwards compatibility, the previous behavior of truncation can be enabled using an AppContext switch.
- For .NET Framework applications consuming **Microsoft.Data.SqlClient**, the SNI.dll files previously downloaded to the `bin\x64` and `bin\x86` folders are now named `Microsoft.Data.SqlClient.SNI.x64.dll` and `Microsoft.Data.SqlClient.SNI.x86.dll` and will be downloaded to the `bin` directory.
- New connection string property synonyms will replace old properties when fetching connection string from `SqlConnectionStringBuilder` for consistency. [Read More](#new-connection-string-property-synonyms)

### New features in 2.0

The following new features have been introduced in Microsoft.Data.SqlClient 2.0.

#### DNS failure resiliency

The driver will now cache IP addresses from every successful connection to a SQL Server endpoint that supports the feature. If a DNS resolution failure occurs during a connection attempt, the driver will try establishing a connection using a cached IP address for that server, if any exists.

#### EventSource tracing

This release introduces support for capturing event trace logs for debugging applications. To capture these events, client applications must listen for events from SqlClient's EventSource implementation:

```csharp
Microsoft.Data.SqlClient.EventSource
```

For more information, see how to [Enable event tracing in SqlClient](enable-eventsource-tracing.md).

#### Enabling managed networking on Windows

A new AppContext switch, **"Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows"**, enables the use of a managed SNI implementation on Windows for testing and debugging purposes. This switch will toggle the driver's behavior to use a managed SNI in .NET Core 2.1+ and .NET Standard 2.0+ projects on Windows, eliminating all dependencies on native libraries for the Microsoft.Data.SqlClient library.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows", true);
```

See [AppContext Switches in SqlClient](appcontext-switches.md) for a full list of available switches in the driver.

#### Enabling decimal truncation behavior

The decimal data scale will be rounded by the driver by default as is done by SQL Server. For backwards compatibility, you can set the AppContext switch **"Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal"** to **true**.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal", true);
```

#### New connection string property synonyms

New synonyms have been added for the following existing connection string properties to avoid spacing confusion around properties with more than one word. Old property names will continue to be supported for backwards compatibility but the new connection string properties will now be included when fetching connection string from [SqlConnectionStringBuilder](/dotnet/api/microsoft.data.sqlclient.sqlconnectionstringbuilder).

|Existing connection string property|New Synonym|
|-----------------------------------|-----------|
| ApplicationIntent | Application Intent |
| ConnectRetryCount | Connect Retry Count |
| ConnectRetryInterval | Connect Retry Interval |
| PoolBlockingPeriod | Pool Blocking Period |
| MultipleActiveResultSets | Multiple Active Result Sets |
| MultiSubnetFailover | Multiple Subnet Failover |
| TransparentNetworkIPResolution | Transparent Network IP Resolution |
| TrustServerCertificate | Trust Server Certificate |

#### SqlBulkCopy RowsCopied property

The RowsCopied property provides read-only access to the number of rows that have been processed in the ongoing bulk copy operation. This value may not necessarily be equal to the final number of rows added to the destination table.

#### Connection open overrides

The default behavior of SqlConnection.Open() can be overridden to disable the ten-second delay and automatic connection retries triggered by transient errors.

```csharp
using SqlConnection sqlConnection = new SqlConnection("Data Source=(local);Integrated Security=true;Initial Catalog=AdventureWorks;");
sqlConnection.Open(SqlConnectionOverrides.OpenWithoutRetry);
```

> [!NOTE]
> Note that this override can only be applied to SqlConnection.Open() and not SqlConnection.OpenAsync().

#### Username support for Active Directory Interactive mode

A username can be specified in the connection string when using Azure Active Directory Interactive authentication mode for both .NET Framework and .NET Core

Set a username using the **User ID** or **UID** connection string property:

```csharp
"Server=<server name>; Database=<db name>; Authentication=Active Directory Interactive; User Id=<username>;Encrypt=True;"
```

#### Order hints for SqlBulkCopy

Order hints can be provided to improve performance for bulk copy operations on tables with clustered indexes. For more information, see the [bulk copy operations](sql/bulk-copy-order-hints.md) section.

#### SNI dependency changes

Microsoft.Data.SqlClient (.NET Core and .NET Standard) on Windows is now dependent on **Microsoft.Data.SqlClient.SNI.runtime**, replacing the previous dependency on **runtime.native.System.Data.SqlClient.SNI**. The new dependency adds support for the ARM platform along with the already supported platforms ARM64, x64, and x86 on Windows.

### 2.0 Target Platform Support

- .NET Framework 4.6+ (Windows x86, Windows x64)
- .NET Core 2.1+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Windows ARM64, Windows ARM, Linux, macOS)

## Release notes for Microsoft.Data.SqlClient 1.1.0

Full release notes, including dependencies, are available in the GitHub Repository: [1.1 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/1.1).

### New features in 1.1

#### Always Encrypted with secure enclaves

Always Encrypted is available starting in Microsoft SQL Server 2016. Secure enclaves are available starting in Microsoft SQL Server 2019. To use the enclave feature, connection strings should include the required attestation protocol and attestation URL. For example:

```csharp
"Attestation Protocol=HGS;Enclave Attestation Url=<attestation_url_for_HGS>"
```

For more information, see:

- [SqlClient support for Always Encrypted](sql/sqlclient-support-always-encrypted.md)
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](sql/tutorial-always-encrypted-enclaves-develop-net-apps.md)

### 1.1 Target Platform Support

- .NET Framework 4.6+ (Windows x86, Windows x64)
- .NET Core 2.1+ (Windows x86, Windows x64, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Linux, macOS)

## Release notes for Microsoft.Data.SqlClient 1.0

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

### Data Classification

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

### UTF-8 support

UTF-8 support doesn't require any application code changes. These SqlClient changes optimize client-server communication when the server supports UTF-8 and the underlying column collation is UTF-8. See the UTF-8 section under [What's new in SQL Server 2019](../../sql-server/what-s-new-in-sql-server-2019.md).

### Always encrypted with secure enclaves

In general, existing documentation that uses System.Data.SqlClient on .NET Framework **and built-in column master key store providers** should now work with .NET Core, too.

 [Develop using Always Encrypted with .NET Framework Data Provider](../../relational-databases/security/encryption/develop-using-always-encrypted-with-net-framework-data-provider.md)

 [Always Encrypted: Protect sensitive data and store encryption keys in the Windows certificate store](/azure/sql-database/sql-database-always-encrypted)

### Authentication

Different authentication modes can be specified by using the _Authentication_ connection string option. For more information, see the [documentation for SqlAuthenticationMethod](/dotnet/api/system.data.sqlclient.sqlauthenticationmethod?view=netframework-4.7.2&preserve-view=true).

> [!NOTE]
> Custom key store providers, like the Azure Key Vault provider, will need to be updated to support Microsoft.Data.SqlClient. Similarly, enclave providers will also need to be updated to support Microsoft.Data.SqlClient.
> Always Encrypted is only supported against .NET Framework and .NET Core targets. It is not supported against .NET Standard since .NET Standard is missing certain encryption dependencies.

### 1.0 Target Platform Support

- .NET Framework 4.6+ (Windows x86, Windows x64)
- .NET Core 2.1+ (Windows x86, Windows x64, Linux, macOS)
- .NET Standard 2.0+ (Windows x86, Windows x64, Linux, macOS)