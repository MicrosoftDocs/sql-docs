---
title: SSMS Options page - Output Window
description: "Output Window in SQL Server Management Studio"
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 11/13/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "VS.ToolsOptionsPages.Query_Execution.Sql_Server.General"
helpviewer_keywords:
  - "Output Window [SQL Server Management Studio]"
  - "Activity Monitor [SQL Server Management Studio]"
  - "Object Explorer [SQL Server Management Studio]"
dev_langs:
  - TSQL
---

# Options (Output Window - General)

[!INCLUDE[SQL Server Azure SQL Database PDW](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

Use this page to set the default behavior of the output window.

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

The Output Window can be opened from the View menu. To access the settings, on the **Tools** menu, select **Options**, and expand the **Output Window** folder or use the key combination Ctrl+Alt+O.

There are multiple logging channels available to provide more information when using SQL Server Management Studio.

The following table gives an overview of the types of messages associated with each output channel.

| Channel | Definition |
| ------- | ---------- |
| **Object Explorer** | This channel outputs the query text and elapsed times of SQL queries needed to expand nodes in Object Explorer. Each query logs a Begin Query and an End Query event. Each event has a timestamp and the URN associated with the entity being queried. The URN refers to the underlying SQL Management Object and consists of an XPath-style hierarchy. For example, the URN for a table named "Table1" in database "Db" on server "MyServer" would be "Server[@Name='MyServer']/Database[@Name='Db']/Table[/@Name='Table1']". Expanding one node in Object Explorer can require multiple such queries with different parameters. The End Query event contains the elapsed time of the query along with the T-SQL text. This query data is useful for server performance analysis in cases where Object Explorer seems unusually slow to expand a particular node. Note: only some nodes in Object Explorer provide this query detail when expanding. Enabled by default. |
| **Telemetry** | Telemetry is the stream of anonymous feature usage data collected by Microsoft. These events could be useful for your tracking of SSMS usage. It can help you identify what Object Explorer nodes you expand and what commands run during your SSMS session while the Output window is open. Enabled by default. |
| **SQL Client** | This channel displays information from the Microsoft.Data.SqlClient data provider. This option requires a restart of SSMS for output to appear. |
| **Azure Active Directory Interactive authentication** | For cases where Microsoft Entra authentication is used, this channel outputs information specific to the authentication process. |
| **Azure REST API** | This channel starts after authentication with Microsoft Entra credentials occurs. This channel outputs information specific to Azure REST API calls when Azure-connected features are used and more troubleshooting information is needed. |
| **Activity Monitor** | This channel starts when [Activity Monitor](../../relational-databases/performance-monitor/activity-monitor.md) opens for a server. This stream contains events showing part of the query text and timestamp of each query, error messages, and notifications of the monitor being paused due to connectivity problems. If Activity Monitor is idle or otherwise failing to update, check this output channel for more information. |
