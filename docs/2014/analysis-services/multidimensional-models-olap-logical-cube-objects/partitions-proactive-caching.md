---
title: "Proactive Caching (Partitions) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "hybrid OLAP"
  - "partitions [Analysis Services], proactive caching"
  - "relational OLAP"
  - "multidimensional OLAP"
  - "MOLAP"
  - "proactive caching [Analysis Services]"
  - "ROLAP"
  - "cache [Analysis Services]"
ms.assetid: 422660b2-4d80-4165-b1c9-3963bcde556b
author: minewiskan
ms.author: owend
manager: craigg
---
# Proactive Caching (Partitions)
  Proactive caching provides automatic MOLAP cache creation and management for OLAP objects. The cubes immediately incorporate changes that are made to the data in the database, based upon notifications received from the database. The goal of proactive caching is to provide the performance of traditional MOLAP, while retaining the immediacy and ease of management offered by ROLAP.  
  
 A simple <xref:Microsoft.AnalysisServices.ProactiveCaching> object is composed of: timing specification, and table notification. The timing specification defines the timeframe for updating the cache after a change notification has been received. The table notification defines the notification schema between the data table and the <xref:Microsoft.AnalysisServices.ProactiveCaching> object.  
  
 Multidimensional OLAP (MOLAP) storage provides the best query response, but with a penalty of some data latency. Real-time relational OLAP (ROLAP) storage lets users immediately browse the most recent changes in a data source, but at the penalty of significantly poorer performance than multidimensional OLAP (MOLAP) storage because of the absence of precalculated summaries of data and because relational storage is not optimized for OLAP-style queries. If you have applications in which your users need to see recent data and you also want the performance advantages of MOLAP storage, SQL Server Analysis Services offers the option of proactive caching to address this scenario, particularly in combination with the use of partitions. Proactive caching is set on a per partition and per dimension basis. Proactive caching options can provide a balance between the enhanced performance of MOLAP storage and the immediacy of ROLAP storage, and provide automatic partition processing when underlying data changes or on a set schedule.  
  
## Proactive Caching Configuration Options  
 SQL Server Analysis Services provides several proactive caching configuration options that enable you to maximize performance, minimize latency, and schedule processing. Proactive caching features simplify the process of managing data obsolescence. The proactive caching settings determine how frequently the multidimensional OLAP structure, also called the MOLAP cache, is rebuilt, whether the outdated MOLAP storage is queried while the cache is rebuilt or the underlying ROLAP data source, and whether the cache is rebuilt on a schedule or based on changes in the database.  
  
### Minimizing Latency  
 With proactive caching set to minimize latency, user queries against an OLAP object are made against either ROLAP storage or MOLAP storage, depending whether recent changes have occurred to the data and how proactive caching is configured. The query engine directs queries against source data in MOLAP storage until changes occur in the data source. To minimize latency, after changes occur in a data source, cached MOLAP objects can be dropped and querying switched to ROLAP storage while the MOLAP objects are rebuilt in cache. After the MOLAP objects are rebuilt and processed, queries are automatically switched to the MOLAP storage. The cache refresh can occur extremely quickly for a small partition, such as the current partition - which can be as small as the current day.  
  
### Maximizing Performance  
 To maximize performance while also reducing latency, caching can also be used without dropping the current MOLAP objects. Queries then continue against the MOLAP objects while data is read into and processed in a new cache. This method provides better performance but may result in queries returning old data while the new cache is being built.  
  
## See Also  
 [Dimension Storage](../multidimensional-models-olap-logical-dimension-objects/dimensions-storage.md)   
 [Set Partition Storage &#40;Analysis Services - Multidimensional&#41;](../multidimensional-models/set-partition-storage-analysis-services-multidimensional.md)  
  
  
