---
title: AppContext switches in SqlClient
description: Learn about the AppContext switches available in SqlClient and how to use them to modify some default behaviors.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 08/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
dev_langs:
  - "csharp"
ms.custom: sfi-ropc-nochange
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

To set `MultiSubnetFailover=true` globally without modifying individual connection strings, you can set the AppContext switch **"Switch.Microsoft.Data.SqlClient.EnableMultiSubnetFailoverByDefault"** to `true` at application startup:  

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

## Enable User Agent feature extension

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

(Available starting with version 7.0)

When the AppContext switch **"Switch.Microsoft.Data.SqlClient.EnableUserAgent"** is enabled, the driver sends user agent details to the server as part of the connection. This information assists with troubleshooting and quantifying driver usage by version and operating system. This switch is disabled by default. To enable it, set the AppContext switch to `true` at application startup:  

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.EnableUserAgent", true);
```

## Enabling decimal truncation behavior

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting with Microsoft.Data.SqlClient 2.0, decimal data is rounded by default, as is done by SQL Server. To enable the previous behavior of truncation, you can set the AppContext switch **"Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal"** to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal", true);
```

## Enabling managed networking on Windows

[!INCLUDE [dotnet-modern](../../includes/products/applies-plain/dotnet-modern.md)]

(Available starting with version 2.0)

On Windows, SqlClient uses a native implementation of the SNI network interface by default. To enable the use of a managed SNI implementation, you can set the AppContext switch **"Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows"** to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows", true);
```

This switch toggles the driver's behavior to use a managed networking implementation in .NET Core 2.1+ and .NET Standard 2.0+ projects on Windows, eliminating all dependencies on native libraries for the Microsoft.Data.SqlClient library. It is for testing and debugging purposes only.

> [!NOTE]
> There are some known differences when compared to the native implementation. For example, the managed implementation does not support non-domain Windows Authentication.

## Disabling Transparent Network IP Resolution

[!INCLUDE [dotnet-framework-only](../../includes/products/applies-plain/dotnet-framework-only.md)]

Transparent Network IP Resolution (TNIR) is a revision of the existing MultiSubnetFailover feature. TNIR affects the connection sequence of the driver in the case where the first resolved IP of the hostname doesn't respond and there are multiple IPs associated with the hostname. The combination of `TransparentNetworkIPResolution` and `MultiSubnetFailover` selects the connection sequence:

|TransparentNetworkIPResolution|MultiSubnetFailover|Connection sequence|
|--------|--------|--------|
|True|True|`TransparentNetworkIPResolution` is ignored. The driver attempts the DNS-resolved IP addresses in parallel and completes the authentication with the first responder.|
|True|False|The driver runs multiple connect rounds across the DNS-resolved IP addresses, with a 500-millisecond minimum on the first attempt and progressively larger per-attempt timeouts, until a connection succeeds or the overall `Connect Timeout` is reached.|
|False|True|The driver attempts the DNS-resolved IP addresses in parallel and completes the authentication with the first responder.|
|False|False|The driver attempts each DNS-resolved IP address sequentially until one succeeds or `Connect Timeout` is reached.|

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

## Enable a minimum timeout during login

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

To prevent a login attempt from waiting indefinitely, you can set the AppContext switch **Switch.Microsoft.Data.SqlClient.UseOneSecFloorInTimeoutCalculationDuringLogin** to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseOneSecFloorInTimeoutCalculationDuringLogin", false);
```

## Disable blocking behavior of ReadAsync

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 3.0, ReadAsync runs asynchronously. Previous versions run ReadAsync synchronously and block the calling thread on .NET Framework. To control this blocking behavior, you can set the AppContext switch **Switch.Microsoft.Data.SqlClient.MakeReadAsyncBlocking** to `true` or `false` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.MakeReadAsyncBlocking", false);
```

## Enabling rowversion null behavior

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 3.0, when a rowversion has a value of null, `SqlDataReader` returns a `DBNull` value instead of an empty `byte[]`. To enable the legacy behavior of returning an empty `byte[]`, enable the AppContext switch **Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior** on application startup.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior", true);
```

## Suppress insecure TLS warning

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

(Available starting with version 4.0.1)

When using `Encrypt=false` in the connection string, a security warning is output to the console if the TLS version is 1.2 or lower. This warning can be suppressed by enabling the following AppContext switch on application startup:

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

Starting in version 7.1.0-preview2, the `Connection Idle Timeout` connection string keyword configures the idle duration, in seconds, after which a pooled connection becomes eligible for eviction (default 300; a value of 0 disables idle expiration). An eligible connection is discarded on a later retrieval or maintenance pass, so the exact timing can vary by pool implementation and maintenance cadence. The keyword is only enforced when the legacy idle-timeout behavior is disabled. With the switch at its default value of `true`, the pool preserves the historical behavior and the keyword has no effect.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseLegacyIdleTimeoutBehavior", false);
```

## Enable the V2 connection pool

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 6.1, SqlClient includes an alternative, experimental connection pool implementation (V2). The V1 pool remains the default (the switch defaults to `false`). To opt in to the V2 pool, enable the AppContext switch `Switch.Microsoft.Data.SqlClient.UseConnectionPoolV2` at application startup.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseConnectionPoolV2", true);
```

## Count pool waits against the connect timeout

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 7.1.0-preview2, time spent waiting for a connection from the pool can be counted against the caller's `Connect Timeout` budget, so pool waits and the network connection attempt share one overall timeout. When the switch is at its default value of `false`, pool operations receive a full `Connect Timeout` and the network connection attempt receives a further full budget.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseOverallConnectTimeoutForPoolWait", true);
```

## Revert to legacy failover alternation on login errors

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

Starting in version 7.1.0-preview2, when connecting with failover configured, SqlClient no longer alternates to the failover partner for SQL errors returned during the login phase. To revert to the legacy alternation behavior, enable the AppContext switch `Switch.Microsoft.Data.SqlClient.UseLegacyFailoverAlternationOnLoginSqlErrors` at application startup. The switch defaults to `false`.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseLegacyFailoverAlternationOnLoginSqlErrors", true);
```

## Honor an explicit zero scale on vartime parameters

[!INCLUDE [dotnet-all](../../includes/products/applies-plain/dotnet-all.md)]

By default, SqlClient sends a scale of 7 when you explicitly set the scale to 0 for **datetime2**, **datetimeoffset**, or **time** parameters. With version 6.0 and later versions, set `Switch.Microsoft.Data.SqlClient.LegacyVarTimeZeroScaleBehaviour` to `false` at application startup to preserve the explicit scale of 0. The switch defaults to `true`.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.LegacyVarTimeZeroScaleBehaviour", false);
```

## See also

[AppContext Class](/dotnet/api/system.appcontext?view=netcore-3.1&preserve-view=true)
