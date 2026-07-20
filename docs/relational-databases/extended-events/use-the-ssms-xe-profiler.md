---
title: Use the SSMS XEvent Profiler
description: The XEvent Profiler displays a live viewer of extended events. Learn why to use this profiler, key features, and how to get started viewing extended events.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: mathoma
ms.date: 12/09/2025
ms.service: sql
ms.subservice: xevents
ms.topic: tutorial
helpviewer_keywords:
  - "extended events [SQL Server], system health session"
  - "extended events [SQL Server], system_health session"
  - "system_health session [SQL Server extended events]"
  - "system health session [SQL Server extended events]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Use the SSMS XEvent Profiler

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

The XEvent Profiler is a SQL Server Management Studio (SSMS) feature that displays a live viewer window of Extended Events. This overview describes the reasons for using this profiler, key features, and instructions to get started viewing Extended Events.

## Why would I use the XEvent Profiler?

Unlike SQL Profiler, XEvent profiler is directly integrated into SSMS and is built on top of the scalable Extended Events technology in the SQL engine. This feature enables quick access to a live streaming view of diagnostics events on the SQL Server. This view can be customized and those customizations can be shared with other SSMS users as a .viewsettings file. The session created by XE Profiler is less intrusive to the running SQL Server than a similar SQL trace would be when using SQL Profiler. This session can be customized by the user as well, using the existing XE session properties UI or by Transact-SQL.

## Prerequisites

This feature is only available on SQL Server Management Studio (SSMS) v17.3 or later. Install the latest version of [SQL Server Management Studio (SSMS)](/ssms/install/install).

<a id="getting-started"></a>

## Getting started

To access the XEvent Profiler, follow these steps:

1. Open **SQL Server Management Studio**.

1. Connect to an instance of the SQL Server Database Engine or localhost.

1. In Object Explorer, find the XE Profiler menu item and expand it by selecting the '+' sign.

   :::image type="content" source="media/use-the-ssms-xe-profiler/xevents-xe-profiler-menu.png" alt-text="Screenshot of the XEProfiler Menu.":::

1. Double-click **Standard** if you want to view all events in this session. Select **T-SQL** if you want to view the logged SQL statements. If a session is not already created, a session is created for you.

   :::image type="content" source="media/use-the-ssms-xe-profiler/xevents-xe-profiler-start-session.png" alt-text="Screenshot of the XEProfiler Session." lightbox="media/use-the-ssms-xe-profiler/xevents-xe-profiler-start-session.png":::

1. You can now view events captured by the session.

   :::image type="content" source="media/use-the-ssms-xe-profiler/xevents-xe-profiler-start-viewer.png" alt-text="Screenshot of the XEProfiler Viewer." lightbox="media/use-the-ssms-xe-profiler/xevents-xe-profiler-start-viewer.png":::

## Stop and start the session

To start the session, either select **Start data feed** from the **Extended Events** menu in the Live Data Viewer or use the green arrow in the tool bar:

:::image type="content" source="media/use-the-ssms-xe-profiler/start-feed.png" alt-text="Screenshot of the start data feed option in the extended events menu in SSMS.":::

Likewise, after a session is started, to stop a session select **Stop data feed** from the **Extended Events** menu in the Live Data Viewer or use the red square in the tool bar.

## Customize the session

While XEvent Profiler provides the preconfigured **Standard** and **T-SQL** sessions, you can further customize the session to meet your needs by doing the following:

- **Add or remove columns**: Right-click on any column header in the Live Data viewer and select **Choose Columns...** to add or remove columns to control the display of additional information.
- **Filter events**: Right-click on any field in the Live Data viewer and select **Filter by this value** to apply criteria to the captured events, such as to focus on a specific application, user, or event type. Alternatively, you can select **Filters...** from the top navigation bar to open the filter dialog box.

## Export data

To save the data for later analysis, you can export the data feed to a table, or either an `XEL` or `CSV` file. To export the feed, select **Export data...** from the **Extended Events** menu.

## Related content

- [Extended Events overview](extended-events.md)
- [Extended Events Tools](extended-events-tools.md)
