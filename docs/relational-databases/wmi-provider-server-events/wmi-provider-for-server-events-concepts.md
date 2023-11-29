---
title: "WMI Provider for Server Events concepts"
description: Use these resources to learn about how WMI Provider for Server Events uses Windows Management Instrumentation to monitor events in an instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 10/30/2023
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "server events [WMI]"
  - "SQL Server WMI Provider"
  - "WMI Provider for Server Events"
  - "monitoring events [WMI]"
  - "events [WMI]"
---
# WMI Provider for Server Events concepts

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The WMI Provider for Server Events lets you use Windows Management Instrumentation (WMI) to monitor events in an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## In this section

- [Understand the WMI Provider for Server Events](understanding-the-wmi-provider-for-server-events.md)

  Provides an overview of the provider architecture and how SQL Server Agent can be programmed to work with it.

- [Work with the WMI Provider for Server Events](working-with-the-wmi-provider-for-server-events.md)

  Explains the factors that you need to consider before programming with the provider.

- [Use WQL with the WMI Provider for Server Events](using-wql-with-the-wmi-provider-for-server-events.md)

  Explains the WMI Query Language (WQL) syntax and how to use it when you program against the provider.

- [Sample: Create a SQL Server Agent Alert with the WMI Provider](sample-creating-a-sql-server-agent-alert-with-the-wmi-provider.md)

  Provides an example of using the WMI Provider to return trace event information on which to create a SQL Server Agent alert.

- [Sample: Use the WMI Event Provider with the .NET Framework](sample-using-the-wmi-event-provider-with-the-net-framework.md)

  Provides an example of using the WMI Provider to return event data in a C# application.

- [WMI Provider for Server Events classes and properties](wmi-provider-for-server-events-classes-and-properties.md)

  Introduces the event classes and properties that make up the programming mode of the provider.
