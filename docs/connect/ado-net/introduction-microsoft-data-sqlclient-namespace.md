---
title: "Introduction to Microsoft.Data.SqlClient namespace"
description: "Learn about the Microsoft.Data.SqlClient namespace and how it's the preferred way to connect to SQL for .NET applications."
ms.date: "11/19/2020"
ms.assetid: c18b1fb1-2af1-4de7-80a4-95e56fd976cb
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-jizho2
---
# Introduction to Microsoft.Data.SqlClient namespace

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

## Release notes for Microsoft.Data.SqlClient 2.1

Release notes are also available in the GitHub Repository: [2.1 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/2.1).

### New features

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

`Server=<server>.database.windows.net; Authentication=Active Directory Device Code Flow; Database=Northwind;`

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
"Server={serverURL}; Authentication=Active Directory MSI; Initial Catalog={db};"

// For System Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory Managed Identity; Initial Catalog={db};"

// For User Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory MSI; User Id={ObjectIdOfManagedIdentity}; Initial Catalog={db};"

// For User Assigned Managed Identity
"Server={serverURL}; Authentication=Active Directory Managed Identity; User Id={ObjectIdOfManagedIdentity}; Initial Catalog={db};"
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

`"Server={serverURL}; Initial Catalog={db}; Integrated Security=true; Command Timeout=60"`

### Removal of symbols from Native SNI
With Microsoft.Data.SqlClient v2.1, we've removed the symbols introduced in [v2.0.0](https://www.nuget.org/packages/Microsoft.Data.SqlClient.SNI/2.0.0) from [Microsoft.Data.SqlClient.SNI.runtime](https://www.nuget.org/packages/Microsoft.Data.SqlClient.SNI.runtime) NuGet starting with [v2.1.1](https://www.nuget.org/packages/Microsoft.Data.SqlClient.SNI.runtime/2.1.1). The public symbols are now published to Microsoft Symbols Server for tools like BinSkim that require access to public symbols.

### Source-Linking of Microsoft.Data.SqlClient symbols
Starting with Microsoft.Data.SqlClient v2.1, Microsoft.Data.SqlClient symbols are source-linked and published to the Microsoft Symbols Server for an enhanced debugging experience without the need to download source code.


## Release notes for Microsoft.Data.SqlClient 2.0

Release notes are also available in the GitHub Repository: [2.0 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/2.0).

### Breaking changes

- The access modifier for the enclave provider interface `SqlColumnEncryptionEnclaveProvider` has been changed from `public` to `internal`.

- Constants in the `SqlClientMetaDataCollectionNames` class have been updated to reflect changes in SQL Server.

- The driver will now perform Server Certificate validation when the target SQL Server enforces TLS encryption, which is the default for Azure connections.

- `SqlDataReader.GetSchemaTable()` now returns an empty `DataTable` instead `null`.

- The driver now performs decimal scale rounding to match SQL Server behavior. For backwards compatibility, the previous behavior of truncation can be enabled using an AppContext switch.

- For .NET Framework applications consuming **Microsoft.Data.SqlClient**, the SNI.dll files previously downloaded to the `bin\x64` and `bin\x86` folders are now named `Microsoft.Data.SqlClient.SNI.x64.dll` and` Microsoft.Data.SqlClient.SNI.x86.dll` and will be downloaded to the `bin` directory.

- New connection string property synonyms will replace old properties when fetching connection string from `SqlConnectionStringBuilder` for consistency. [Read More](#new-connection-string-property-synonyms)

### New features

#### DNS failure resiliency

The driver will now cache IP addresses from every successful connection to a SQL Server endpoint that supports the feature. If a DNS resolution failure occurs during a connection attempt, the driver will try establishing a connection using a cached IP address for that server, if any exists.

#### EventSource tracing

This release introduces support for capturing event trace logs for debugging applications. To capture these events, client applications must listen for events from SqlClient's EventSource implementation:

```
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

```
"Server=<server name>; Database=<db name>; Authentication=Active Directory Interactive; User Id=<username>;"
```

#### Order hints for SqlBulkCopy

Order hints can be provided to improve performance for bulk copy operations on tables with clustered indexes. For more information, see the [bulk copy operations](sql/bulk-copy-order-hints.md) section.

#### SNI dependency changes

Microsoft.Data.SqlClient (.NET Core and .NET Standard) on Windows is now dependent on **Microsoft.Data.SqlClient.SNI.runtime**, replacing the previous dependency on **runtime.native.System.Data.SqlClient.SNI**. The new dependency adds support for the ARM platform along with the already supported platforms ARM64, x64, and x86 on Windows.

## Release notes for Microsoft.Data.SqlClient 1.1.0

Release notes are also available in the GitHub Repository: [1.1 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/1.1).

### New features

#### Always Encrypted with secure enclaves

Always Encrypted is available starting in Microsoft SQL Server 2016. Secure enclaves are available starting in Microsoft SQL Server 2019. To use the enclave feature, connection strings should include the required attestation protocol and attestation URL. For example:

```
Attestation Protocol=HGS;Enclave Attestation Url=<attestation_url_for_HGS>
```

For more information, see:

- [SqlClient support for Always Encrypted](sql/sqlclient-support-always-encrypted.md)
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](sql/tutorial-always-encrypted-enclaves-develop-net-apps.md)

## Release notes for Microsoft.Data.SqlClient 1.0

The initial release for the Microsoft.Data.SqlClient namespace offers additional functionality over the existing System.Data.SqlClient namespace.
Release notes are also available on the GitHub Repository: [1.0 Release Notes](https://github.com/dotnet/SqlClient/tree/master/release-notes/1.0).

### New features

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

UTF-8 support doesn't require any application code changes. These SqlClient changes optimize client-server communication when the server supports UTF-8 and the underlying column collation is UTF-8. See the UTF-8 section under [What's new in SQL Server 2019](../../sql-server/what-s-new-in-sql-server-ver15.md).

### Always encrypted with enclaves

In general, existing documentation that uses System.Data.SqlClient on .NET Framework **and built-in column master key store providers** should now work with .NET Core, too.

 [Develop using Always Encrypted with .NET Framework Data Provider](../../relational-databases/security/encryption/develop-using-always-encrypted-with-net-framework-data-provider.md)

 [Always Encrypted: Protect sensitive data and store encryption keys in the Windows certificate store](/azure/sql-database/sql-database-always-encrypted)

### Authentication

Different authentication modes can be specified by using the _Authentication_ connection string option. For more information, see the [documentation for SqlAuthenticationMethod](/dotnet/api/system.data.sqlclient.sqlauthenticationmethod?view=netframework-4.7.2&preserve-view=true).

> [!NOTE]
> Custom key store providers, like the Azure Key Vault provider, will need to be updated to support Microsoft.Data.SqlClient. Similarly, enclave providers will also need to be updated to support Microsoft.Data.SqlClient.
> Always Encrypted is only supported against .NET Framework and .NET Core targets. It is not supported against .NET Standard since .NET Standard is missing certain encryption dependencies.