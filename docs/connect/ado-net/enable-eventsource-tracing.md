---
title: Enable event tracing in SqlClient
description: Describes how to enable event tracing or logging in SqlClient by implementing an event listener and how to access the event data.
author: David-Engel
ms.author: davidengel
ms.reviewer: davidengel
ms.date: 09/27/2024
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

<a id="keywords"></a>The current implementation supports the following Event Keywords:

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

With **Microsoft.Data.SqlClient** v2.1, event tracing needs to be enabled by configuring the `EventCommand` with an event source listener. The valid `EventCommand` values applicable to Native SNI are:

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

## Use Xperf to collect traces

1. Start tracing using the following command.

   ```powershell
   xperf -start trace -f myTrace.etl -on *Microsoft.Data.SqlClient.EventSource
   ```

2. Run the native SNI tracing example to connect to SQL Server.

3. Stop tracing using the following command line.

   ```powershell
   xperf -stop trace
   ```

4. Use [PerfView](https://github.com/microsoft/perfview) to open the myTrace.etl file specified in Step 1. The SNI tracing log can be found with `Microsoft.Data.SqlClient.EventSource/SNIScope` and `Microsoft.Data.SqlClient.EventSource/SNITrace` event names.

   ![Use PerfView to view SNI trace file](media/view-event-trace-native-sni.png)

## Use PerfView to collect traces

1. Start [PerfView](https://github.com/microsoft/perfview) and run `Collect > Collect` from the menu bar.

2. Configure the trace file name, output path, and provider name.

   ![Configure Perfview before collection](media/collect-event-trace-native-sni.png)

3. Start collection.

4. Run the native SNI tracing example to connect to SQL Server.

5. Stop collection from PerfView. It takes a while to generate the PerfViewData.etl file according to the configuration in Step 2.

6. Open the `etl` file in PerfView. The SNI tracing log can be found with `Microsoft.Data.SqlClient.EventSource/SNIScope` and `Microsoft.Data.SqlClient.EventSource/SNITrace` event names.

## Use dotnet-trace to collect traces

On Linux, macOS, or Windows, dotnet-trace can be used to capture traces. The dotnet-trace tool is used to collect traces for .NET applications. For more information about dotnet-trace, see the [dotnet-trace performance analysis utility](/dotnet/core/diagnostics/dotnet-trace) The traces created by dotnet-trace can be viewed in [PerfView](https://github.com/microsoft/perfview).

1. If not already installed, [install the .NET SDK](/dotnet/core/install/) on the client machine.

1. [Install dotnet-trace](/dotnet/core/diagnostics/dotnet-trace#install).

1. Run dotnet-trace. The `--providers` parameter requires the provider name and keywords to be specified for traces from Microsoft.Data.SqlClient. The keywords option is a sum of the keyword values in the [event keywords table](#keywords) converted to hexadecimal. To collect all events at the verbose level of `MyApplication` from the start of the application, the sum of keywords is 8191 and `1FFF` in hexadecimal. The verbose level is specified in this command by `5`.

   ```bash
   dotnet-trace collect --providers Microsoft.Data.SqlClient.EventSource:1FFF:5 -- dotnet MyApplication.dll
   ```

   The output is:

   ```bash

   Provider Name                           Keywords            Level               Enabled By
   Microsoft.Data.SqlClient.EventSource    0x0000000000001FFF  Verbose(5)          --providers

   Launching: dotnet MyApplication.dll
   Process        : /usr/lib/dotnet/dotnet
   Output File    : /home/appuser/dotnet_20240927_102506.nettrace

   [00:00:00:00]   Recording trace 0.00     (B)
   Press <Enter> or <Ctrl+C> to exit...

   Trace completed.
   Process exited with code '1'.
   ```

   To collect all events at the information level on a running application, first find the process ID of the application. Then run dotnet-trace on the process. The information level is specified by `4`.

   ```bash
   dotnet-trace ps
   8734  MyApplication  /home/appuser/MyApplication/MyApplication

   dotnet-trace collect -–process-id 8734 --providers Microsoft.Data.SqlClient.EventSource:1FFF:4
   ```

   Run the application separately and let it run as long as needed to reproduce the issue. If it's a high CPU issue, 5-10 seconds is usually enough.

   ```bash
   Provider Name                           Keywords            Level               Enabled By
   Microsoft.Data.SqlClient.EventSource    0x0000000000001FFF  LogAlways(0)        --providers

   Process        : /usr/lib/dotnet/dotnet
   Output File    : /home/appuser/dotnet_20240927_104154.nettrace

   [00:00:00:10]   Recording trace 4.096    (KB)
   Press <Enter> or <Ctrl+C> to exit...
   Stopping the trace. This may take several minutes depending on the application being traced.

   Trace completed.
   ```

   The trace file name ends in `.nettrace`. If not tracing on Windows, copy the file to a Windows system. View the trace file in [PerfView](https://github.com/microsoft/perfview).

## External resources

For another set of examples on how to trace Microsoft.Data.SqlClient cross-platform, see the [CSS SQL Networking Tools wiki](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/Collect-a-.NET-Core-SQL-Driver-Trace).

For more information about event tracing, see the following resources.

|Resource|Description|
|--------------|-----------------|
|[EventSource Class](/dotnet/api/system.diagnostics.tracing.eventsource)|Used to create ETW events.|
|[EventListener Class](/dotnet/api/system.diagnostics.tracing.eventlistener)|Provides methods for enabling and disabling events from event sources.|
