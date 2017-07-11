---
title: "SQL Server, Catalog Metadata Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SQLServer:Catalog Metadata"
ms.assetid: 665e63e6-4bd2-4091-92a5-327364db2f8d
caps.latest.revision: 4
author: "dagiro"
ms.author: "v-dagir"
manager: "jhubbard"
---
# SQL Server, Catalog Metadata Object
The **SQLServer:Catalog Metadata** performance object provides counters for catalog metadata for SQL Server.

This following table describes the SQL Server **Catalog Metadata** performance objects.


|**SQL Server Catalog Metadata counters**|Description|  
|-------------|-----------------|  
|**Cache Entries Count**|Number of entries in the catalog metadata cache.|
|**Cache Entries Pinned Count**|Number of catalog metadata cache entries that are pinned.|
|**Cache Hit Ratio**|Ratio between catalog metadata cache hits and lookups.|
|**Cache Hit Ratio Base**|For internal use only.|

There is one instance of the counter for each database.

## See Also  
[Monitor Resource Usage (System Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)