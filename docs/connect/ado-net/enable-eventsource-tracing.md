---
title: Enable event tracing in SqlClient
description: Describes how to enable event tracing or logging in SqlClient by implementing an event listener and how to access the event data.
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
# Enable event tracing in SqlClient

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

[Event Tracing for Windows (ETW)](/windows/win32/etw/event-tracing-portal) is an efficient, kernel-level, tracing facility that lets you log driver-defined events for debugging and testing purposes. SqlClient supports capturing ETW events at different informational levels. To begin capturing event traces, client applications should listen for events from SqlClient's EventSource implementation:

```
Microsoft.Data.SqlClient.EventSource
```

The current implementation supports the following Event Keywords:

| Keyword name | Value | Description |
| ------------ | ----- | ----------- |
| ExecutionTrace | 1 | Turns on capturing Start/Stop events before and after command execution. |
| Trace | 2 | Turns on capturing basic application flow trace events. |
| Scope | 4 | Turns on capturing enter and exit events |
| NotificationTrace | 8 | Turns on capturing `SqlNotification` trace events |
| NotificationScope | 16 | Turns on capturing `SqlNotification` scope enter and exit events |
| PoolerTrace | 32 | Turns on capturing connection pooling flow trace events. |
| PoolerScope | 64 | Turns on capturing connection pooling scope trace events. |
| AdvancedTrace | 128 | Turns on capturing advanced flow trace events. |
| AdvancedTraceBin  | 256 | Turns on capturing advanced flow trace events with additional information. |
| CorrelationTrace | 512 | Turns on capturing correlation flow trace events. |
| StateDump | 1024 | Turns on capturing full state dump of `SqlConnection` |
| SNITrace | 2048 | Turns on capturing flow trace events from Managed Networking implementation (only applicable in .NET Core) |
| SNIScope | 4096 | Turns on capturing scope events from Managed Networking implementation (only applicable in .NET Core) |

## Example

The following example enables event tracing for a data operation on the **AdventureWorks** sample database and displays the events in the console window.

[!code-csharp [SqlClientEventSource#1](~/../sqlclient/doc/samples/SqlClientEventSource.cs#1)]

## Event tracing support in Native SNI

**Microsoft.Data.SqlClient** provides event tracing support in **Microsoft.Data.SqlClient.SNI** and **Microsoft.Data.SqlClient.SNI.runtime** starting with v2.1. Events can be collected from the native DLLs using the [Xperf](/windows-hardware/test/wpt/) and [PerfView](https://github.com/microsoft/perfview) tools.

Starting with **Microsoft.Data.SqlClient** v3.0, event tracing can be enabled without any modifications in the client application using event collection tools.

With **Microsoft.Data.SqlClient** v2.1, event tracing needs to be enabled by configuring the `EventCommand` with an event source listener. The valid `EventCommand` values applicable to Native SNI are listed as below:

```cs

// Enables trace events:
EventSource.SendCommand(eventSource, (EventCommand)8192, null);

// Enables flow events:
EventSource.SendCommand(eventSource, (EventCommand)16384, null);

// Enables both trace and flow events:
EventSource.SendCommand(eventSource, (EventCommand)(8192 | 16384), null);
```

The following example enables event tracing in native SNI DLLs.

```cs
// Native SNI tracing example
using System;
using System.Diagnostics.Tracing;
using Microsoft.Data.SqlClient;

public class SqlClientListener : EventListener
{
    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        if (eventSource.Name.Equals("Microsoft.Data.SqlClient.EventSource"))
        {
            // Enables both trace and flow events
            EventSource.SendCommand(eventSource, (EventCommand)(8192 | 16384), null);
        }
    }
}

class Program
{
    static string connectionString = @"Data Source = localhost; Initial Catalog = AdventureWorks;Integrated Security=true;";

    static void Main(string[] args)
    {
        // Event source listener configuration is not required in v3.0 onwards.
        using (SqlClientListener listener = new SqlClientListener())
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
        }
    }
}
```

### Use Xperf to collect trace log

1. Start tracing using the following command line.

   ```
   xperf -start trace -f myTrace.etl -on *Microsoft.Data.SqlClient.EventSource
   ```

2. Run the native SNI tracing example to connect to SQL Server.

3. Stop tracing using the following command line.

   ```
   xperf -stop trace
   ```

4. Use PerfView to open the myTrace.etl file specified in Step 1. The SNI tracing log can be found with `Microsoft.Data.SqlClient.EventSource/SNIScope` and `Microsoft.Data.SqlClient.EventSource/SNITrace` event names.

   ![Use PerfView to view SNI trace file](media/view-event-trace-native-sni.png)

### Use PerfView to collect trace log

1. Start PerfView and run `Collect > Collect` from menu bar.

2. Configure trace file name, output path, and provider name.

   ![Configure Prefview before collection](media/collect-event-trace-native-sni.png)

3. Start collection.

4. Run the native SNI tracing example to connect to SQL Server.

5. Stop collection from PerfView. It will take a while to generate PerfViewData.etl file according to configuration in Step 2.

6. Open the `etl` file in PerfView. The SNI tracing log can be found with `Microsoft.Data.SqlClient.EventSource/SNIScope` and `Microsoft.Data.SqlClient.EventSource/SNITrace` event names.

## External resources

For more information, see the following resources.

|Resource|Description|
|--------------|-----------------|
|[EventSource Class](/dotnet/api/system.diagnostics.tracing.eventsource)|Used to create ETW events.|
|[EventListener Class](/dotnet/api/system.diagnostics.tracing.eventlistener)|Provides methods for enabling and disabling events from event sources.|
