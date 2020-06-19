---
title: "Enabling event tracing in SqlClient"
description: "Describes how to enable event tracing in SqlClient by implementing an event listener and how to access the event data."
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
# Enabling event tracing in SqlClient

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

[Event Tracing for Windows (ETW)](https://docs.microsoft.com/windows/win32/etw/event-tracing-portal) is an efficient kernel-level tracing facility that lets you log driver-defined events for debugging and testing purposes. SqlClient supports capturing ETW events at different informational levels. To begin capturing event traces, client applications should listen for events from SqlClient's EventSource implementation:

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
|||

## Example
The following example enables event tracing for a data operation on the **AdventureWorks** sample database and displays the events in the console window.

[!code-csharp [SqlClientEventSource#1](~/../sqlclient/doc/samples/SqlClientEventSource.cs#1)]

## External resources  
For more information, see the following resources.  
  
|Resource|Description|  
|--------------|-----------------|  
|[EventSource Class](https://docs.microsoft.com/dotnet/api/system.diagnostics.tracing.eventsource)|Provides the ability to create ETW events.| 
|[EventListener Class](https://docs.microsoft.com/dotnet/api/system.diagnostics.tracing.eventlistener)|Provides methods for enabling and disabling events from event sources.| 
