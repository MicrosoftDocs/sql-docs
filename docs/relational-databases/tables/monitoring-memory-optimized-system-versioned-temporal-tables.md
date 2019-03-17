---
title: "Monitoring Memory-Optimized System-Versioned Temporal Tables | Microsoft Docs"
ms.custom: ""
ms.date: "03/28/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
ms.assetid: 7a06785d-dbcb-44de-b95c-26b131471bee
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# Monitoring Memory-Optimized System-Versioned Temporal Tables
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  You can use existing views to track detailed and summarized memory consumption for every system-versioned memory-optimized table.  
  
 Detailed memory consumption (split per main system-versioned and internal history staging table):  
  
```  
--Details of memory consumption   
WITH InMemoryTemporalTables   
AS   
(   
   SELECT SCHEMA_NAME ( T1.schema_id ) AS TemporalTableSchema  
      , T1.object_id AS TemporalTableObjectId  
      , IT.object_id AS InternalTableObjectId  
      , OBJECT_NAME ( IT.parent_object_id ) AS TemporalTableName  
      , IT.Name AS InternalHistoryStagingName   
   FROM sys.internal_tables IT    
   JOIN sys.tables T1 ON IT.parent_object_id = T1.object_id   
   WHERE T1.is_memory_optimized  = 1 AND T1.temporal_type = 2    
)   
  
SELECT   
     TemporalTableSchema  
   , T.TemporalTableName  
   , T.InternalHistoryStagingName,    
     CASE   
        WHEN C.object_id = T.TemporalTableObjectId   
        THEN 'Temporal Table Consumption'   
         ELSE 'Internal Table Consumption'   
         END ConsumedBy  
     , C.*   
   FROM sys.dm_db_xtp_memory_consumers C   
   JOIN InMemoryTemporalTables T   
      ON C.object_id = T.TemporalTableObjectId OR C.object_id = T.InternalTableObjectId   
   WHERE T.TemporalTableSchema = 'dbo' AND  T.TemporalTableName = 'FXCurrencyPairs' ;  
  
```  
  
 Summary of memory consumption (total for a system-versioned memory-optimized table):  
  
```  
--Summary of memory consumption   
WITH InMemoryTemporalTables   
AS   
(   
   SELECT SCHEMA_NAME ( T1.schema_id ) AS TemporalTableSchema  
     , T1.object_id AS TemporalTableObjectId  
     , IT.object_id AS InternalTableObjectId  
     , OBJECT_NAME ( IT.parent_object_id ) AS TemporalTableName  
     , IT.Name AS InternalHistoryStagingName   
   FROM sys.internal_tables IT    
   JOIN sys.tables T1 ON IT.parent_object_id = T1.object_id   
   WHERE T1.is_memory_optimized  = 1 AND T1.temporal_type = 2    
)   
, DetailedConsumption   
AS   
(   
   SELECT TemporalTableSchema  
      , T.TemporalTableName  
      , T.InternalHistoryStagingName  
      , CASE   
           WHEN C.object_id = T.TemporalTableObjectId   
           THEN 'Temporal Table Consumption'   
           ELSE 'Internal Table Consumption'   
           END ConsumedBy  
      , C.*   
    FROM sys.dm_db_xtp_memory_consumers C   
   JOIN InMemoryTemporalTables T   
      ON C.object_id = T.TemporalTableObjectId OR C.object_id = T.InternalTableObjectId   
)   
  
SELECT TemporalTableSchema  
     TemporalTableName  
   , sum ( allocated_bytes ) AS allocated_bytes  
   , sum ( used_bytes ) AS used_bytes   
FROM DetailedConsumption   
WHERE TemporalTableSchema = 'dbo' AND  TemporalTableName = 'FXCurrencyPairs'   
GROUP BY TemporalTableSchema, TemporalTableName ;  
  
```  
  
## See Also  
 [System-Versioned Temporal Tables with Memory-Optimized Tables](../../relational-databases/tables/system-versioned-temporal-tables-with-memory-optimized-tables.md)   
 [Creating a Memory-Optimized System-Versioned Temporal Table](../../relational-databases/tables/creating-a-memory-optimized-system-versioned-temporal-table.md)   
 [Working with Memory-Optimized System-Versioned Temporal Tables](../../relational-databases/tables/working-with-memory-optimized-system-versioned-temporal-tables.md)   
 [Performance Considerations with Memory-Optimized System-Versioned Temporal Tables](../../relational-databases/tables/memory-optimized-system-versioned-temporal-tables-performance.md)   
 [Temporal Tables](../../relational-databases/tables/temporal-tables.md)   
 [Temporal Table System Consistency Checks](../../relational-databases/tables/temporal-table-system-consistency-checks.md)   
 [Manage Retention of Historical Data in System-Versioned Temporal Tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)   
 [Temporal Table Metadata Views and Functions](../../relational-databases/tables/temporal-table-metadata-views-and-functions.md)  
  
  
