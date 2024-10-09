---
title: "Database Properties (Query Store Page)"
description: "Learn how to use the Query Store tab in the Database Properties dialog box to configure Query Store modes, intervals, thresholds, and other properties."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "01/25/2021"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.databaseproperties.querystore.f1"
---
# Database Properties (Query Store Page)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)], [!INCLUDE[sssds](../../includes/sssds-md.md)]

  Access this page from the principal database, and use it to configure and to modify the properties of the database Query Store. These options can also be configure by using the [ALTER DATABASE SET options](../../t-sql/statements/alter-database-transact-sql-set-options.md). For information about the Query Store, see [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md).  
  
## Options  
 Operation Mode  
 Valid values are OFF, READ_ONLY, and READ_WRITE. OFF disables the Query Store. In READ_WRITE mode, the Query Store collects and persists query plan and runtime execution statistics information. In READ_ONLY mode, information can be read from the Query Store, but new information is not added. If the maximum allocated space of the Query Store has been exhausted, the Query Store will change its operation mode to READ_ONLY.  
  
 Operation Mode (Actual)  
 Gets the actual operation mode of the Query Store.  
  
 Operation Mode (Requested)  
 Gets and sets the desired operation mode of the Query Store.  
  
 Data Flush Interval (Minutes)  
 Determines the frequency at which data written to the Query Store is persisted to disk. To optimize for performance, data collected by the Query Store is asynchronously written to the disk. The frequency at which this asynchronous transfer occurs is configured.  
  
 Statistics Collection Interval  
 Gets and sets the statistics collection interval value.  
  
 Max Size (MB)  
 Gets and sets the total space allocated to the Query Store.  
  
 Query Store Capture Mode  
 -   None does not capture new queries.  
  
-   All captures all queries.  
  
-   Auto captures queries based on resource consumption.  
  
 Stale Query Threshold (Days)  
 Gets and sets the stale query threshold. Configure the STALE_QUERY_THRESHOLD_DAYS argument to specify the number of days to retain data in the Query Store.  
  
 Purge Query Data  
 Removes the contents of the Query Store.  
  
 Pie Charts  
 The left chart shows the total database file consumption on the disk, and the portion of the file which is filled with the Query Store data.  
  
 The right chart shows the portion of the Query Store quota which is currently used up. Note that the quota is not shown in the left chart. The quota may exceed the current size of the database.  
  
## Remarks  
 The Query Store feature provides DBAs with insight on query plan choice and performance. It simplifies performance troubleshooting by enabling you to quickly find performance differences caused by changes in query plans. The feature automatically captures a history of queries, plans, and runtime statistics, and retains these for your review. It separates data by time windows, allowing you to see database usage patterns and understand when query plan changes happened on the server. The Query Store can be configured by using this Query Store database property page, or by using the [ALTER DATABASE SET](../../t-sql/statements/alter-database-transact-sql-set-options.md) option. The Query Store presents information by using a [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] dialog box. For more information about Query Store, see [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md).  
  
## See Also  
 [Query Store Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)   
 [Query Store Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)
