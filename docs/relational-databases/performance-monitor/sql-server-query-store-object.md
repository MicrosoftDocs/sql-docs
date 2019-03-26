---
title: "SQL Server, Query Store Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Store object"
  - "SQL Server:Query Store"
ms.assetid: b4a04acd-0b66-44a5-b72d-1a45b49e13e6
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, Query Store Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  The Query Store object provides counters to monitor resource utilization of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to store query texts, execution plans and runtime stats for objects such as stored procedures, ad hoc and prepared [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, and triggers.  
  
 This table describes are the **SQLServer:Query Store**counters.  
  
|SQL Server Query Store counters|Description|  
|-------------------------------------|-----------------|  
|**Query Store CPU usage**|Indicates Query Stores usage of the CPU.|  
|**Query Store logical reads**|Indicates the number of logical reads made by the Query Store.|  
|**Query Store logical writes**|Indicates how much data is being queued to be flushed from the Query Store. The frequency and delay of adding items (that represent runtime stats) to the queue is controlled by Data Flush Interval setting.|  
|**Query Store physical reads**|Indicates the number of physical reads made by the Query Store.|  
  
 Each counter in the object contains the following instances:  
  
|Query Store instance|Description|  
|--------------------------|-----------------|  
|**_Total**|Information for the Query Store for this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|\<database name>|Query Store information for this database.|  
  
## See Also  
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)   
 [Query Store Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)   
 [Query Store Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
