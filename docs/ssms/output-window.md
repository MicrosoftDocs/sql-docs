---
title: "SSMS Output Window | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Output Window [SQL Server Management Studio]"
  - "Activity Monitor [SQL Server Management Studio]"
  - "Object Explorer [SQL Server Management Studio]"  
ms.assetid: a2ce1a07-b4e2-471c-87d2-b8de5e6c6864
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Output Window in SQL Server Management Studio
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
The Output Window can be opened from the View menu or by using the key combination Ctrl+Alt+O. There are multiple channels of output available.

The following table gives an overview of the types of messages associated with each output channel.

|Channel|Description|
|-----------|---------------|  
|**Telemetry**|Telemetry is the stream of [anonymous feature usage data](sql-server-management-studio-ssms.md) collected by Microsoft. These events could be useful for your own record keeping of SSMS usage. It can help you identify what Object Explorer nodes you expanded and what commands you ran during your SSMS session while the Output window was open.|
|**Object Explorer**|This channel outputs the query text and elapsed times of SQL queries needed to expand nodes in Object Explorer. Each query logs a Begin Query and an End Query event. Each event has a time stamp and the URN associated with the entity being queried. The [URN](https://technet.microsoft.com/library/microsoft.sqlserver.management.smo.urn(v=sql.90).aspx) refers to the underlying SQL Management Object and consists of an XPath-style hierarchy. For example, the URN for a table named "Table1" in database "Db" on server "MyServer" would be "Server[@Name='MyServer']/Database[@Name='Db']/Table[/@Name='Table1']". Expanding one node in Object Explorer could perform multiple such queries with different parameters. The End Query event will contain the elapsed time of the query along with the TSQL text. You may find this query data useful for server performance analysis in cases where Object Explorer seems unusually slow to expand a particular node. **Note**- not every node in Object Explorer provides this level of query detail when expanding.|
|**Activity Monitor**|This channel starts when [Activity Monitor opens](https://docs.microsoft.com/sql/relational-databases/performance-monitor/activity-monitor) for a server. This stream contains events showing part of the query text and timestamp of each query, error messages, and notifications of the monitor being paused due to connectivity problems. If Activity Monitor seems to be idle or otherwise failing to update, check this output channel for more information.|





