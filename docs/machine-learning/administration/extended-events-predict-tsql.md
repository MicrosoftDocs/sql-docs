---
title: Monitor T-SQL with extended events
description: Learn how to use extended events to monitor and troubleshooting PREDICT T-SQL statements in SQL Server Machine Learning Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 09/24/2019
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Monitor PREDICT T-SQL statements with extended events in SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

Learn how to use extended events to monitor and troubleshooting [PREDICT](../../t-sql/queries/predict-transact-sql.md) T-SQL statements in SQL Server Machine Learning Services.

## Table of extended events

The following extended events are available in all versions of SQL Server that support the [PREDICT](../../t-sql/queries/predict-transact-sql.md) T-SQL statement. 

| name                       | object_type | description |
|----------------------------|-------------|-------------|
| predict_function_completed | event       | Builtin execution time breakdown|
| predict_model_cache_hit    | event       | Occurs when a model is retrieved from the PREDICT function model cache. Use this event along with other predict_model_cache_* events to troubleshoot issues caused by the PREDICT function model cache.|
| predict_model_cache_insert | event       | Occurs when a model is insert into the PREDICT function model cache. Use this event along with other predict_model_cache_* events to troubleshoot issues caused by the PREDICT function model cache.	|
| predict_model_cache_miss   | event       | Occurs when a model is not found in the PREDICT function model cache. Frequent occurrences of this event could indicate that SQL Server needs more memory. Use this event along with other predict_model_cache_* events to troubleshoot issues caused by the PREDICT function model cache.|
| predict_model_cache_remove | event       | Occurs when a model is removed from model cache for PREDICT function. Use this event along with other predict_model_cache_* events to troubleshoot issues caused by the PREDICT function model cache.|

## Query for related events

To view a list of all columns returned for these events, run the following query in SQL Server Management Studio:

```sql
SELECT *
FROM sys.dm_xe_object_columns
WHERE object_name LIKE 'predict%'
```

## Examples

To capture information about performance of a scoring session using PREDICT:

1. Create a new extended event session, using Management Studio or another supported [tool](../../relational-databases/extended-events/extended-events-tools.md).
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

## Next steps

For more information about extended events (sometimes called XEvents), and how to track events in a session, see these articles:

+ [Monitor Python and R scripts with extended events in SQL Server Machine Learning Services](extended-events.md)
+ [Extended Events concepts and architecture](../../relational-databases/extended-events/extended-events.md)
+ [Set up event capture in SSMS](../../relational-databases/extended-events/quick-start-extended-events-in-sql-server.md)
+ [Manage event sessions in the Object Explorer](../../relational-databases/extended-events/manage-event-sessions-in-the-object-explorer.md)