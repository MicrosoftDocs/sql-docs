---
title: AppContext switches in SqlClient
description: Learn about the AppContext switches available in SqlClient and how to use them to modify some default behaviors.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: 06/01/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# AppContext switches in Sqlclient

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The AppContext class allows SqlClient to provide new functionality while continuing to support callers who depend on the previous behavior. Users can opt out of a change in behavior by setting specific AppContext switches.

## Force use of operating system encryption protocols

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

Starting with Microsoft.Data.SqlClient 4.0, TLS 1.3 isn't supported by the driver and has been removed from the supported protocols list by default. Users can switch back to forcing use of the operating system's client protocols, by setting the AppContext switch **"Switch.Microsoft.Data.SqlClient.UseSystemDefaultSecureProtocols"** to true:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseSystemDefaultSecureProtocols", true);
```

Starting with version 5.0, TLS 1.3 is supported in TDS 8 connections without having to use the above switch. TDS 8 is enabled when `Encrypt` is set to `Strict`.

## Enabling decimal truncation behavior

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

Starting with Microsoft.Data.SqlClient 2.0, decimal data will be rounded by default, as is done by SQL Server. To enable the previous behavior of truncation, you can set the AppContext switch **"Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal"** to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal", true);
```

## Enabling managed networking on Windows

[!INCLUDE [appliesto-xxxx-netcore-netst-md](../../includes/appliesto-xxxx-netcore-netst-md.md)]

(Available starting with version 2.0)

On Windows, SqlClient uses a native implementation of the SNI network interface by default. To enable the use of a managed SNI implementation, you can set the AppContext switch **"Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows"** to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows", true);
```

This switch will toggle the driver's behavior to use a managed networking implementation in .NET Core 2.1+ and .NET Standard 2.0+ projects on Windows, eliminating all dependencies on native libraries for the Microsoft.Data.SqlClient library. It's intended for testing and debugging purposes only.

> [!NOTE]
> There are some known differences when compared to the native implementation. For example, the managed implementation does not support non-domain Windows Authentication.

## Disabling Transparent Network IP Resolution

[!INCLUDE [appliesto-netfx-xxxx-xxxx-md](../../includes/appliesto-netfx-xxxx-xxxx-md.md)]

Transparent Network IP Resolution (TNIR) is a revision of the existing MultiSubnetFailover feature. TNIR affects the connection sequence of the driver in the case where the first resolved IP of the hostname doesn't respond and there are multiple IPs associated with the hostname. TNIR interacts with MultiSubnetFailover to provide the following three connection sequences:

* 0: One IP is attempted, followed by all IPs in parallel
* 1: All IPs are attempted in parallel
* 2: All IPs are attempted one after another

|TransparentNetworkIPResolution|MultiSubnetFailover|Behavior|
|--------|--------|--------|
|True|True|1|
|True|False|0|
|False|True|1|
|False|False|2|

TransparentNetworkIPResolution is enabled by default. MultiSubnetFailover is disabled by default. To disable TNIR, you can set the AppContext switch **"Switch.Microsoft.Data.SqlClient.DisableTNIRByDefaultInConnectionString"** to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.DisableTNIRByDefaultInConnectionString", true);
```

For more information about setting these properties, see the documentation for [SqlConnection.ConnectionString Property](/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring).

## Enable a minimum timeout during login

[!INCLUDE [appliesto-netfx-xxxx-xxxx-md](../../includes/appliesto-netfx-xxxx-xxxx-md.md)]

To prevent a login attempt from waiting indefinitely, you can set the AppContext switch **Switch.Microsoft.Data.SqlClient.UseOneSecFloorInTimeoutCalculationDuringLogin** to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseOneSecFloorInTimeoutCalculationDuringLogin", false);
```

## Disable blocking behavior of ReadAsync

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

By default, ReadAsync runs synchronously and blocks the calling thread on .NET Framework. To disable this blocking behavior, you can set the AppContext switch **Switch.Microsoft.Data.SqlClient.MakeReadAsyncBlocking** to `false` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.MakeReadAsyncBlocking", false);
```

## Enable configurable retry logic

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

(Available starting with version 3.0)

By default, configurable retry logic is disabled. To enable this feature, set the AppContext switch **Switch.Microsoft.Data.SqlClient.EnableRetryLogic** to `true` at application startup. This switch is required, even if a retry provider is assigned to a connection or command.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.EnableRetryLogic", true);
```

* For information on how to enable the switch by using a configuration file see [Enable safety switch](configurable-retry-logic-config-file-sqlclient.md#enable-safety-switch).

> [!NOTE]
> Starting from Microsoft.Data.SqlClient v4.0, the App Context switch "Switch.Microsoft.Data.SqlClient.EnableRetryLogic" will no longer be required to use the configurable retry logic feature. The feature is now supported in production. The default behavior of the feature will continue to be a non-retry policy, which will need to be overridden by client applications to enable retries.

## Enabling rowversion null behavior

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]
Starting in version 3.0, when a rowversion has a value of null, `SqlDataReader` returns a `DBNull` value instead of an empty `byte[]`. To enable the legacy behavior of returning an empty `byte[]`, enable the AppContext switch **Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior** on application startup.

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior", true);
```

## Suppress insecure TLS warning

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

(Available starting with version 4.0.1)

When using `Encrypt=false` in the connection string, a security warning is output to the console if the TLS version is 1.2 or lower. This warning can be suppressed by enabling the following AppContext switch on application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.SuppressInsecureTLSWarning", true);
```

## See also

[AppContext Class](/dotnet/api/system.appcontext?view=netcore-3.1&preserve-view=true)
