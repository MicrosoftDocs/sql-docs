---
title: "Event counters in SqlClient"
description: Use Microsoft SqlClient Data Provider for SQL Server event counters to monitor your application status and its connection resources in .NET Core and .NET Standard.
ms.date: "05/31/2021"
dev_langs: 
  - "csharp"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-deshtehari
---
# Event counters in SqlClient

[!INCLUDE[appliesto-xxxx-netcore-netst-md](../../includes/appliesto-xxxx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

> [!IMPORTANT]
> Event counters are applied to **.NET Core 3.1**, **.NET Standard 2.1** and above. And this feature is available since `Microsoft.Data.SqlClient` **version 3.0.0**.

You can use <xref:Microsoft.Data.SqlClient> event counters to monitor the status of your application and the connection resources that it uses. Event counters can be monitored by `.NET CLI global tools` and `perfView` or can be accessed programmatically using the <xref:System.Diagnostics.Tracing.EventListener> class in the <xref:System.Diagnostics.Tracing> namespace.

## Available event counters

Currently there are 16 different event counters available for <xref:Microsoft.Data.SqlClient> as described in the following table:

|Name|Display Name|Description|  
|-------------------------|-----------------|-----------------|  
|**active-hard-connections**|Actual active connections are made to servers|The number of connections that are being made to database servers.|  
|**hard-connects**|Actual connections are made to servers|The number of connections per second that are being made to database servers.|  
|**hard-disconnects**|Actual disconnections are made to servers|The number of disconnects per second that are being made to database servers.|  
|**active-soft-connects**|Active connections got from connection pool|The number of active connections being pulled from the connection pool.|  
|**soft-connects**|Connections got from connection pool|The number of connections per second that are being pulled from the connection pool.|  
|**soft-disconnects**|Connections returned to the connection pool|The number of connections per second that are being returned to the connection pool.|  
|**number-of-non-pooled-connections**|Number of connections aren't using connection pooling|The number of active connections that aren't pooled.|  
|**number-of-pooled-connections**|Number of connections are managed by connection pooler|The number of active connections that are being managed by the connection pooling infrastructure.|  
|**number-of-active-connection-pool-groups**|Number of active unique connection strings|The number of unique connection pool groups that are active. This counter is controlled by the number of unique connection strings that are found in the AppDomain.|  
|**number-of-inactive-connection-pool-groups**|Number of unique connection strings waiting for pruning|The number of unique connection pool groups that are marked for pruning. This counter is controlled by the number of unique connection strings that are found in the AppDomain.|  
|**number-of-active-connection-pools**|Number of active connection pools|The total number of connection pools.|  
|**number-of-inactive-connection-pools**|Number of inactive connection pools|The number of inactive connection pools that haven't had any recent activity and are waiting to be disposed.|  
|**number-of-active-connections**|Number of active connections|The number of active connections that are currently in use.|  
|**number-of-free-connections**|Number of free-ready connections|The number of connections available for use in the connection pools.|  
|**number-of-stasis-connections**|Number of connections currently waiting to be ready|The number of connections currently awaiting completion of an action and which are unavailable for use by your application.|  
|**number-of-reclaimed-connections**|Number of reclaimed connections from GC|The number of connections that have been reclaimed through garbage collection where `Close` or `Dispose` wasn't called by the application. **Note** Not explicitly closing or disposing connections hurts performance.|  

## Retrieve event counter values

There are two primary ways of consuming EventCounters, either in-proc, or out-of-proc. For more information, see [Consume EventCounters](/dotnet/core/diagnostics/event-counters).

### Consume out-of-proc

In Windows, you can use [PerfView](https://github.com/microsoft/perfview) and [Xperf](/windows-hardware/test/wpt/) to collect event counters data. For more information, see [Enable event tracing in SqlClient](enable-eventsource-tracing.md).
You can use [dotnet-counters](/dotnet/core/diagnostics/dotnet-counters) and [dotnet-trace](/dotnet/core/diagnostics/dotnet-trace), which are cross platform **.NET** tools to monitor and collect event counters data.

#### Out-of-proc example

The following command, runs, and collects SqlClient event counters values once every second. Replacing `EventCounterIntervalSec=1` with a higher value allows collection of a smaller trace with less granularity in the counter data.

```Console
PerfView /onlyProviders=*Microsoft.Data.SqlClient.EventSource:EventCounterIntervalSec=1 run "<application-Path>"
```

The following command, collects SqlClient event counters values once every second.

```Console
dotnet-trace collect --process-id <pid> --providers Microsoft.Data.SqlClient.EventSource:0:1:EventCounterIntervalSec=1
```

The following command, monitor SqlClient event counters values once every three seconds.

```Console
dotnet-counters monitor Microsoft.Data.SqlClient.EventSource -p <process-id> --refresh-interval 3
```

The following command, monitor selected SqlClient event counters values once every second.

```Console
dotnet-counters monitor Microsoft.Data.SqlClient.EventSource[hard-connects,hard-disconnects] -p <process-id>
```

### Consume in-proc

You can consume the counter values via the [EventListener](/dotnet/api/system.diagnostics.tracing.eventlistener) API. An `EventListener` is an in-proc way of consuming any events written by all instances of an [EventSource](/dotnet/api/system.diagnostics.tracing.eventsource) in your application. For more information, see [EventListener](/dotnet/api/system.diagnostics.tracing.eventlistener).

#### In-proc example

The following sample code, enables events with `EventCounterIntervalSec=1` if event source was `Microsoft.Data.SqlClient.EventSource`. And it writes the counter name with its value that has `Mean` property on each event counter update.

> [!NOTE]
> It's required to specify the `EventCounterIntervalSec` property value while enabling event.

[!code-csharp[SqlClientDiagnosticCounter#1](~/../sqlclient/doc/samples/SqlClientDiagnosticCounter.cs#1)]

``` Output
Actual active connections are made to servers           0
Active connections got from connection pool             26
Number of connections are not using connection pooling          0
Number of connections are managed by connection pooler          26
Number of active unique connection strings              1
Number of unique connection strings waiting for pruning         0
Number of active connection pools               1
Number of inactive connection pools             0
Number of active connections            26
Number of free-ready connections                0
Number of connections currently waiting to be ready             0
...
```

## See also

- [Performance counters in SqlClient](performance-counters.md)
- [dotnet-counters](/dotnet/core/diagnostics/dotnet-counters)
- [dotnet-trace](/dotnet/core/diagnostics/dotnet-trace)
- [Enable event tracing in SqlClient](enable-eventsource-tracing.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
