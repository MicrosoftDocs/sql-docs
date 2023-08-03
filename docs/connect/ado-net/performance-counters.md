---
title: "Performance counters in SqlClient"
description: Use Microsoft SqlClient Data Provider for SQL Server performance counters to monitor your application status and its connection resources by using Windows Performance Monitor or programmatically in .NET Framework.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "12/04/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Performance counters in SqlClient

[!INCLUDE[appliesto-netfx-xxxx-xxxx-md](../../includes/appliesto-netfx-xxxx-xxxx-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

You can use <xref:Microsoft.Data.SqlClient> performance counters to monitor the status of your application and the connection resources that it uses. Performance counters can be monitored by using Windows Performance Monitor or can be accessed programmatically using the <xref:System.Diagnostics.PerformanceCounter> class in the <xref:System.Diagnostics> namespace.

## Available performance counters

Currently there are 14 different performance counters available for <xref:Microsoft.Data.SqlClient> as described in the following table.

|Performance counter|Description|  
|-------------------------|-----------------|  
|`HardConnectsPerSecond`|The number of connections per second that are being made to a database server.|  
|`HardDisconnectsPerSecond`|The number of disconnects per second that are being made to a database server.|  
|`NumberOfActiveConnectionPoolGroups`|The number of unique connection pool groups that are active. This counter is controlled by the number of unique connection strings that are found in the AppDomain.|  
|`NumberOfActiveConnectionPools`|The total number of connection pools.|  
|`NumberOfActiveConnections`|The number of active connections that are currently in use. **Note:**  This performance counter is not enabled by default. To enable this performance counter, see [Activate off-by-default counters](#ActivatingOffByDefault).|  
|`NumberOfFreeConnections`|The number of connections available for use in the connection pools. **Note:**  This performance counter is not enabled by default. To enable this performance counter, see [Activate off-by-default counters](#ActivatingOffByDefault).|  
|`NumberOfInactiveConnectionPoolGroups`|The number of unique connection pool groups that are marked for pruning. This counter is controlled by the number of unique connection strings that are found in the AppDomain.|  
|`NumberOfInactiveConnectionPools`|The number of inactive connection pools that have not had any recent activity and are waiting to be disposed.|  
|`NumberOfNonPooledConnections`|The number of active connections that are not pooled.|  
|`NumberOfPooledConnections`|The number of active connections that are being managed by the connection pooling infrastructure.|  
|`NumberOfReclaimedConnections`|The number of connections that have been reclaimed through garbage collection where `Close` or `Dispose` was not called by the application. **Note** Not explicitly closing or disposing connections hurts performance.|  
|`NumberOfStasisConnections`|The number of connections currently awaiting completion of an action and which are therefore unavailable for use by your application.|  
|`SoftConnectsPerSecond`|The number of active connections being pulled from the connection pool. **Note:**  This performance counter is not enabled by default. To enable this performance counter, see [Activate off-by-default counters](#ActivatingOffByDefault).|  
|`SoftDisconnectsPerSecond`|The number of active connections that are being returned to the connection pool. **Note:**  This performance counter is not enabled by default. To enable this performance counter, see [Activate off-by-default counters](#ActivatingOffByDefault).|  

<a name="ActivatingOffByDefault"></a>

### Activate off-by-default counters

The performance counters `NumberOfFreeConnections`, `NumberOfActiveConnections`, `SoftDisconnectsPerSecond`, and `SoftConnectsPerSecond` are off by default. Add the following information to the application's configuration file to enable them:

```xml  
<system.diagnostics>  
  <switches>  
    <add name="ConnectionPoolPerformanceCounterDetail" value="4"/>  
    <!-- A value of 4 corresponds to System.Diagnostics.TraceLevel.Verbose -->
  </switches>  
</system.diagnostics>  
```  

## Retrieve performance counter values

The following console application shows how to retrieve performance counter values in your application. Connections must be open and active for information to be returned for all of the Microsoft SqlClient Data Provider for SQL Server performance counters.

> [!NOTE]
> This example uses the sample [**AdventureWorks** database](../../samples/adventureworks-install-configure.md). The connection strings provided in the sample code assume that the database is installed and available on the local computer, and that you have created logins that match those supplied in the connection strings. You may need to enable SQL Server logins if your server is configured using the default security settings which allow only Windows Authentication. Modify the connection strings as necessary to suit your environment.

### Example

[!code-csharp[SqlClient_PerformanceCounter#1](~/../sqlclient/doc/samples/SqlClient_PerformanceCounter.cs#1)]

## See also

- [Event counters in SqlClient](event-counters.md)
- [Connecting to a data source](connecting-to-data-source.md)
- [Runtime profiling](/dotnet/framework/debug-trace-profile/runtime-profiling)
- [Introduction to monitoring performance thresholds](/previous-versions/visualstudio/visual-studio-2008/bd20x32d(v=vs.90))
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
