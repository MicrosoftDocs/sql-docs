---
title: Extended events for monitoring PREDICT statements - SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# Extended events for monitoring PREDICT statements

This article describes the extended events provided in SQL Server that you can use to monitor and analyze jobs that use [PREDICT](https://docs.microsoft.com/sql/t-sql/queries/predict-transact-sql) to perform real-time scoring in SQL Server.

Real-time scoring generates scores from a machine learning model that has been stored in SQL Server. The PREDICT function does not require an external run-time such as R or Python, only a model that has been created using a specific binary format. For more information, see [Realtime scoring](https://docs.microsoft.com/sql/advanced-analytics/real-time-scoring).

## Prerequisites

For general information about extended events (sometimes called XEvents), and how to track events in a session, see these articles:

+ [Extended Events concepts and architecture](https://docs.microsoft.com/sql/relational-databases/extended-events/extended-events)
+ [Set up event capture in SSMS](https://docs.microsoft.com/sql/relational-databases/extended-events/quick-start-extended-events-in-sql-server)
+ [Manage event sessions in the Object Explorer](https://docs.microsoft.com/sql/relational-databases/extended-events/manage-event-sessions-in-the-object-explorer)

## Table of extended events

The following extended events are available in all versions of SQL Server that support the [T-SQL PREDICT](https://docs.microsoft.com/sql/t-sql/queries/predict-transact-sql) statement, including SQL Server on Linux, and Azure SQL Database. 

The T-SQL PREDICT statement was introduced in SQL Server 2017. 

|name |object_type|description| 
|----|----|----|
|predict_function_completed	|event	|Builtin execution time breakdown|
|predict_model_cache_hit |event|Occurs when a model is retrieved from the PREDICT function model cache. Use this event along with other predict_model_cache_* events to troubleshoot issues caused by the PREDICT function model cache.|
|predict_model_cache_insert	|event	|	Occurs when a model is insert into the PREDICT function model cache. Use this event along with other predict_model_cache_* events to troubleshoot issues caused by the PREDICT function model cache.	|
|predict_model_cache_miss	|event|Occurs when a model is not found in the PREDICT function model cache. Frequent occurrences of this event could indicate that SQL Server needs more memory. Use this event along with other predict_model_cache_* events to troubleshoot issues caused by the PREDICT function model cache.|
|predict_model_cache_remove	|event| Occurs when a model is removed from model cache for PREDICT function. Use this event along with other predict_model_cache_* events to troubleshoot issues caused by the PREDICT function model cache.|

## Query for related events

To view a list of all columns returned for these events, run the following query in SQL Server Management Studio:

```sql
SELECT * FROM sys.dm_xe_object_columns WHERE object_name LIKE `predict%'
```

## Examples

To capture information about performance of a scoring session using PREDICT:

1. Create a new extended event session, using Management Studio or another supported [tool](https://docs.microsoft.com/sql/relational-databases/extended-events/extended-events-tools).
2. Add the events `predict_function_completed` and `predict_model_cache_hit` to the session.
3. Start the extended event session.
4. Run the query that uses PREDICT.

In the results, review these columns:

+ The value for `predict_function_completed` shows how much time the query spent on loading the model and scoring.
+ The boolean value for `predict_model_cache_hit` indicates whether the query used a cached model or not. 

### Native scoring model cache

In addition to the events specific to PREDICT, you can use the following queries to get more information about the cached model and cache usage:

View the native scoring model cache:

```sql
SELECT *
FROM sys.dm_os_memory_clerks
WHERE type = 'CACHESTORE_NATIVESCORING';
```

View the objects in the model cache:

```sql
SELECT *
FROM sys.dm_os_memory_objects
WHERE TYPE = 'MEMOBJ_NATIVESCORING';
```

