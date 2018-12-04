---
title: "Database Properties (Query Store Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.databaseproperties.querystore.f1"
ms.assetid: da47d75e-291a-4305-acef-4b0aaf5215da
author: stevestein
ms.author: sstein
manager: craigg
---
# Database Properties (Query Store Page)
  Access this page from the principal database, and use it to configure and to modify the properties of the database query store. These options can also be configure by using the [ALTER DATABASE SET options](/sql/t-sql/statements/alter-database-transact-sql-set-options). For information about the query store, see [Monitoring Performance By Using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md).  
  
||  
|-|  
|**Applies to**: [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].|  
  
## Options  
 Enable  
 Enables and disables the query store.  
  
 Operation Mode  
 Valid values are READ_ONLY and READ_WRITE. In READ_WRITE mode, the query store collects and persists query plan and runtime execution statistics information. In READ_ONLY mode, information can be read from the query store, but new information is not added. If the maximum allocated space of the query store has been exhausted, the query store will change its operation mode to READ_ONLY.  
  
 Operation Mode (Actual)  
 Gets the actual operation mode of the query store.  
  
 Operation Mode (Requested)  
 Gets and sets the desired operation mode of the query store.  
  
 Data Flush Interval (Minutes)  
 Determines the frequency at which data written to the query store is persisted to disk. To optimize for performance, data collected by the query store is asynchronously written to the disk. The frequency at which this asynchronous transfer occurs is configured.  
  
 Statistics Collection Interval  
 Gets and sets the statistics collection interval value.  
  
 Max Size (MB)  
 Gets and sets the total space allocated to the query store.  
  
 Stale Query Threshold (Days)  
 Gets and sets the stale query threshold. Configure the STALE_QUERY_THRESHOLD_DAYS argument to specify the number of days to retain data in the query store.  
  
 Purge Query Data  
 Removes the contents of the query store.  
  
 Pie Charts  
 The left chart shows the total database file consumption on the disk, and the portion of the file which is filled with the query store data.  
  
 The right chart shows the portion of the query store quota which is currently used up. Note that the quota is not shown in the left chart. The quota may exceed the current size of the database.  
  
## Remarks  
 The query store feature provides DBAs with insight on query plan choice and performance. It simplifies performance troubleshooting by enabling you to quickly find performance differences caused by changes in query plans. The feature automatically captures a history of queries, plans, and runtime statistics, and retains these for your review. It separates data by time windows, allowing you to see database usage patterns and understand when query plan changes happened on the server. The query store can be configured by using this query store database property page, or by using the [ALTER DATABASE SET](/sql/t-sql/statements/alter-database-transact-sql-set-options) option. The query store presents information by using a [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] dialog box. For more information about query store, see [Monitoring Performance By Using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md).  
  
## See Also  
 [Query Store Stored Procedures &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql)   
 [Query Store Catalog Views &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/query-store-catalog-views-transact-sql)  
  
  
