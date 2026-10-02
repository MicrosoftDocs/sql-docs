---
title: AppContext Switches in SqlClient
description: Learn about the AppContext switches available in SqlClient and how to use them to modify some default behaviors.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra, randolphwest
ms.date: 09/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ms.custom:
  - sfi-ropc-nochange
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# AppContext switches in SqlClient

[!INCLUDE [dotnet-all](../../includes/products/applies-full/dotnet-all.md)]

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The AppContext class allows SqlClient to provide new functionality while continuing to support callers who depend on the previous behavior. Users can opt out of a change in behavior by setting specific AppContext switches.

SqlClient reads and caches each switch the first time it uses that switch. Set switches at application startup, before you use any SqlClient types. Changing a switch after SqlClient has cached its value has no effect.

## Enable MultiSubnetFailover by default

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

(Available starting with version 7.0)

To set `MultiSubnetFailover=true` globally without modifying individual connection strings, set the AppContext switch `Switch.Microsoft.Data.SqlClient.EnableMultiSubnetFailoverByDefault` to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.EnableMultiSubnetFailoverByDefault", true);
```

You can also enable this switch in your App.Config:

```xml
<runtime>
  <AppContextSwitchOverrides value="Switch.Microsoft.Data.SqlClient.EnableMultiSubnetFailoverByDefault=true" />
</runtime>
```

When enabled, all connections behave as if `MultiSubnetFailover=true` is set in the connection string. This switch is disabled by default.

## Enable packet multiplexing for async reads

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

(Available starting with version 7.0)

Packet multiplexing improves performance for large async read operations such as `ExecuteReaderAsync` with big result sets, streaming scenarios, or bulk data retrieval. This feature is controlled by two opt-in AppContext switches. Setting both switches to `false` enables the new async processing path:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseCompatibilityAsyncBehaviour", false);
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseCompatibilityProcessSni", false);
```

By default, both switches are `true`, which preserves the existing (compatible) behavior.

## Enable decimal truncation behavior

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting with Microsoft.Data.SqlClient 2.0, decimal data is rounded by default, as is done by SQL Server. To enable the previous behavior of truncation, you can set the AppContext switch `Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal` to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal", true);
```

## Enable managed networking on Windows

[!INCLUDE [dotnet-modern](../../includes/products/applies-plain/dotnet-modern.md)]

(Available starting with version 2.0)

On Windows, SqlClient uses a native implementation of the SNI network interface by default. To enable the use of a managed SNI implementation, set the AppContext switch `Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows` to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows", true);
```

This switch toggles the driver's behavior to use a managed networking implementation in .NET Core 2.1+ and .NET Standard 2.0+ projects on Windows, eliminating all dependencies on native libraries for the Microsoft.Data.SqlClient library. It is for testing and debugging purposes only.

> [!NOTE]  
> There are some known differences when compared to the native implementation. For example, the managed implementation doesn't support non-domain Windows Authentication.

<a id="disabling-transparent-network-ip-resolution"></a>

## Disable Transparent Network IP Resolution

[!INCLUDE [dotnet-framework-only](../../includes/products/applies-plain/dotnet-framework-only.md)]

Transparent Network IP Resolution (TNIR) is a revision of the existing MultiSubnetFailover feature. TNIR affects the connection sequence of the driver in the case where the first resolved IP of the hostname doesn't respond and there are multiple IPs associated with the hostname. The combination of `TransparentNetworkIPResolution` and `MultiSubnetFailover` selects the connection sequence:

| TransparentNetworkIPResolution | MultiSubnetFailover | Connection sequence |
| --- | --- | --- |
| True | True | `TransparentNetworkIPResolution` is ignored. The driver attempts the DNS-resolved IP addresses in parallel and completes the authentication with the first responder. |
| True | False | The driver runs multiple connect rounds across the DNS-resolved IP addresses, with a 500-millisecond minimum on the first attempt and progressively larger per-attempt timeouts, until a connection succeeds or the overall `Connect Timeout` is reached. |
| False | True | The driver attempts the DNS-resolved IP addresses in parallel and completes the authentication with the first responder. |
| False | False | The driver attempts each DNS-resolved IP address sequentially until one succeeds or `Connect Timeout` is reached. |

`TransparentNetworkIPResolution` is enabled by default on .NET Framework, and `MultiSubnetFailover` is disabled by default. On .NET 5 and later versions, `TransparentNetworkIPResolution` isn't a recognized connection-string keyword and setting it (with any value) throws `ArgumentException` (`KeywordNotSupported`). Those versions honor `MultiSubnetFailover` only. The rest of this section (the automatic override, the failure modes in the following warning, and the AppContext switch) applies to .NET Framework.

> [!TIP]  
> Set `MultiSubnetFailover=True` on every connection string, regardless of .NET version or whether the target is Azure SQL or on-premises SQL Server. `MultiSubnetFailover=True` selects a parallel-connect code path that finds the first responsive replica quickly. On .NET Framework, it also bypasses TNIR's sequential per-IP retry loop, which is a common cause of long connect delays and pre-authentication handshake timeouts.

On .NET Framework, when `TransparentNetworkIPResolution` isn't specified in the connection string, the driver automatically disables TNIR when the data source is a recognized Azure SQL endpoint, when the `Authentication` key is set to any Microsoft Entra ID method (`Active Directory Password`, `Active Directory Integrated`, `Active Directory Interactive`, `Active Directory Service Principal`, `Active Directory Device Code Flow`, `Active Directory Managed Identity`, `Active Directory MSI`, `Active Directory Default`, or `Active Directory Workload Identity`), or when the [`SqlConnection.AccessToken`](/dotnet/api/microsoft.data.sqlclient.sqlconnection.accesstoken) property is set. For the endpoint suffixes the driver recognizes, see the `TransparentNetworkIPResolution` entry in [SqlConnection.ConnectionString](/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring).

An explicit `TransparentNetworkIPResolution` value bypasses this automatic behavior: `True` enables TNIR, and `False` disables TNIR unconditionally. To restore the automatic behavior, remove the keyword from the connection string. The automatic override also doesn't apply when the connection string points at Azure SQL through a custom CNAME or vanity DNS name whose suffix isn't recognized as an Azure SQL endpoint. The automatic override targets Azure SQL specifically; it doesn't fire for on-premises SQL Server, so TNIR is on by default there.

### Long connect delays on .NET Framework

On .NET Framework, `TransparentNetworkIPResolution=True` (the default) can cause long connect delays and pre-authentication handshake timeouts whenever the target DNS name resolves to multiple IPs and one of the earlier IPs is unhealthy, stale, or unreachable. TNIR tries the resolved IPs sequentially and increases the per-attempt timeout each round until the overall `Connect Timeout` is reached. You typically observe an unexpectedly long connect delay that ends in this error:

```output
Connection Timeout Expired.  The timeout period elapsed while attempting to consume the pre-authentication handshake acknowledgement.  This could be because the pre-authentication handshake failed or the server was unable to respond back in time.
```

The pattern shows up in several topologies:

- **Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric.** The Azure SQL gateway routes each authentication to a backend replica. When a routed connection fails, TNIR retries the routed backend without returning to the gateway to be rerouted, which extends the delay during a backend failover.
- **On-premises SQL Server behind an Always On availability group listener** whose DNS name resolves to multiple replica IPs. A stale DNS entry or an unhealthy replica IP is tried sequentially before TNIR reaches a working replica.
- **Failover cluster instances with a multi-subnet cluster listener**, or any other configuration where the target DNS name has multiple `A`/`AAAA` records (such as DNS round-robin).

To avoid this behavior, set `MultiSubnetFailover=True` in the connection string:

```text
MultiSubnetFailover=True
```

This recommendation works on every .NET version and covers both Azure SQL and on-premises SQL Server. When `MultiSubnetFailover=True`, the driver ignores `TransparentNetworkIPResolution`, attempts the DNS-resolved IP addresses in parallel, and completes authentication with the first responsive replica. Despite the name, `MultiSubnetFailover` applies to any listener whose DNS name resolves to multiple target IPs, regardless of whether those IPs are in different subnets, and it's safe on stand-alone servers whose DNS resolves to a single IP.

For process-wide control without editing every connection string, use the [Enable MultiSubnetFailover by default](#enable-multisubnetfailover-by-default) AppContext switch.

### Disable TNIR with an AppContext switch

To flip the default value of `TransparentNetworkIPResolution` from `true` to `false` on .NET Framework, set the AppContext switch `Switch.Microsoft.Data.SqlClient.DisableTNIRByDefaultInConnectionString` to `true` at application startup. This switch only changes the default value when `TransparentNetworkIPResolution` isn't in the connection string; it doesn't override an explicit value.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.DisableTNIRByDefaultInConnectionString", true);
```

For more information about setting these properties, see the documentation for [SqlConnection.ConnectionString Property](/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring).

## Disable the minimum timeout during login

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

By default, SqlClient enforces a one-second minimum when calculating the time available for a login attempt. This behavior prevents a login attempt from waiting indefinitely when the calculated timeout rounds down to zero.

To restore the legacy behavior, set the AppContext switch `Switch.Microsoft.Data.SqlClient.UseOneSecFloorInTimeoutCalculationDuringLogin` to `false` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseOneSecFloorInTimeoutCalculationDuringLogin", false);
```

## Disable blocking behavior of ReadAsync

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 3.0, `ReadAsync` runs asynchronously. Previous versions run `ReadAsync` synchronously and block the calling thread on .NET Framework. To control this blocking behavior, set the AppContext switch `Switch.Microsoft.Data.SqlClient.MakeReadAsyncBlocking` to `true` or `false` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.MakeReadAsyncBlocking", false);
```

## Enable rowversion null behavior

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 3.0, when a **rowversion** has a null value, `SqlDataReader` returns a `DBNull` value instead of an empty `byte[]`. To enable the legacy behavior of returning an empty `byte[]`, enable the AppContext switch `Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior` on application startup.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior", true);
```

## Suppress insecure TLS warning

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

(Available starting with version 4.0.1)

When using `Encrypt=false` in the connection string, the console outputs a security warning if the TLS version is 1.2 or lower. Suppress this warning by enabling the following AppContext switch on application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.SuppressInsecureTLSWarning", true);
```

## Ignore Server Provided Failover Partner

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

(Available starting with versions 5.1.8, 6.0.4, and 6.1.3)

Upon failover, failover partner information provided by the server is preferred over failover partner information provided in the connection string. To ignore failover partner information provided by the server and only consider failover partner information provided in the connection string, enable this AppContext switch on application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.IgnoreServerProvidedFailoverPartner", true);
```

## Enforce the connection idle timeout

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 7.1.0, the `Connection Idle Timeout` connection string keyword and `SqlConnectionStringBuilder.IdleTimeout` property configure idle-expiration checks and background cleanup for pooled connections. The default is 300 seconds. Negative values throw an <xref:System.ArgumentException>.

The switch and connection string setting affect each pool implementation differently:

- **V1 pool.** When the switch is `true`, V1 uses its historical randomized two-to-four-minute cleanup cadence and evicts idle connections regardless of `Connection Idle Timeout`. When the switch is `false` and `Connection Idle Timeout` is nonzero, V1 uses half the configured timeout as its cleanup cadence and evicts idle connections after one or two cleanup cycles. When the switch is `false` and `Connection Idle Timeout=0`, V1 disables idle eviction but continues minimum-pool-size maintenance at the historical randomized two-to-four-minute interval.
- **V2 pool.** When the switch is `true`, V2 doesn't perform per-connection idle-age checks, but a nonzero `Connection Idle Timeout` enables and configures background pruning. When the switch is `false` and `Connection Idle Timeout` is nonzero, V2 performs per-connection idle-age checks and configures background pruning from the timeout. When the switch is `false` and `Connection Idle Timeout=0`, V2 doesn't perform per-connection idle-age checks or background pruning. V2 doesn't perform background pruning when `Min Pool Size` is greater than or equal to `Max Pool Size`.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseLegacyIdleTimeoutBehavior", false);
```

For configuration examples, see [Limit connection idle time](sql-server-connection-pooling.md#limit-connection-idle-time).

## Enable the V2 connection pool

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 7.1, SqlClient includes an alternative connection pool implementation (V2). The V1 pool remains the default, and the switch defaults to `false`.

To opt in to V2, enable the AppContext switch `Switch.Microsoft.Data.SqlClient.UseConnectionPoolV2` when the application starts:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseConnectionPoolV2", true);
```

The connection string pooling controls apply to both implementations. For more information, see [SQL Server connection pooling](sql-server-connection-pooling.md).

## Use one connect timeout for pool waits and network connections

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 7.1.0, time spent waiting for a connection from the pool can count against the caller's `Connect Timeout` budget, so the pool wait and network connection attempt share one overall timeout. Enable `Switch.Microsoft.Data.SqlClient.UseOverallConnectTimeoutForPoolWait` to use the overall timeout with either connection pool implementation:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseOverallConnectTimeoutForPoolWait", true);
```

The switch defaults to `false`. This default preserves the legacy connect-timeout behavior, where the pool operation receives a full `Connect Timeout` and the network connection attempt receives another full timeout. As a result, `Open` or `OpenAsync` can take longer than the configured `Connect Timeout`.

## Revert to legacy failover alternation on login errors

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 7.1.0, when connecting with failover configured, SqlClient no longer alternates to the failover partner for SQL errors returned during the login phase. To revert to the legacy alternation behavior, enable the AppContext switch `Switch.Microsoft.Data.SqlClient.UseLegacyFailoverAlternationOnLoginSqlErrors` at application startup. The switch defaults to `false`.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseLegacyFailoverAlternationOnLoginSqlErrors", true);
```

## Honor an explicit zero scale on vartime parameters

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

By default, SqlClient sends a scale of 7 when you explicitly set the scale to 0 for **datetime2**, **datetimeoffset**, or **time** parameters. In version 6.0 or later, set `Switch.Microsoft.Data.SqlClient.LegacyVarTimeZeroScaleBehaviour` to `false` at application startup to preserve the explicit scale of 0. The switch defaults to `true`.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.LegacyVarTimeZeroScaleBehaviour", false);
```

## Related content

- [AppContext Class](/dotnet/api/system.appcontext?view=netcore-3.1&preserve-view=true)
