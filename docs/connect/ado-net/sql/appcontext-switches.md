---
title: "App Context Switches in Sqlclient"
description: "Describes how to use AppContext switches that are available in SqlClient."
ms.date: "06/15/2020"
dev_langs: 
  - "csharp"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: johnnypham
ms.author: v-jopha
ms.reviewer: 
---
# App Context Switches in Sqlclient

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

The AppContext class allows SqlClient to provide new functionality while continuing to support callers who depend on the previous behavior. Users can opt out of a change in behaviour by setting specific AppContext switches.

SqlClient currently supports two AppContext switches.

## Enabling decimal truncation behavior

Starting with Microsoft.Data.SqlClient 2.0, decimal data will be rounded by default, as is done by SQL Server. To enable the previous behaviour of truncation, you can set the AppContext switch **"Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal"** to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.TruncateScaledDecimal", true);
```

## Enabling Managed networking on Windows

On Windows, SqlClient uses a native implementation of the SNI network interface by default. To enable the use of a managed SNI implementation, you can set the AppContext switch **"Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows"** to `true` at application startup:

```csharp
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows", true);
```

This switch will toggle the driver's behavior to use a managed networking implementation in .NET Core 2.1+ and .NET Standard 2.0+ projects on Windows. It is intended for testing and debugging purposes only.

> [!NOTE]
> There are some known differences when compared to the native implementation. For example, the managed implementation does not support non-domain Windows Authentication.

## External resources  
For more information, see the following resources.  
  
|Resource|Description|  
|--------------|-----------------|  
|[AppContext Class](https://docs.microsoft.com/en-us/dotnet/api/system.appcontext?view=netcore-3.1)|Provides members for setting and retrieving data about an application's context.| 
