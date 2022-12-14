---
description: "sp_query_store_clear_message_queues (Transact-SQL)"
title: "sp_query_store_clear_message_queues (Transact-SQL)"
ms.custom:
- event-tier1-build-2022
ms.date: "04/26/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_query_store_clear_message_queues_TSQL"
  - "sp_query_store_clear_message_queues"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_query_store_clear_message_queues"
  - "sys.sp_query_store_clear_message_queues"
ms.assetid: 899df1ff-e871-44df-9361-f3b87ac3ea31
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16"
---
# sp_query_store_clear_message_queues (Transact-SQL)
[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

Clears all queued (non-persisted) Query Store messages pending for the replica against which the command is executed. The `sp_query_store_clear_message_queues` is used when [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) has been enabled. 

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
sp_query_store_clear_message_queues [;]  
```  
  
## Return Code Values  

0 (success) or 1 (failure)  
  
## Permissions  

Requires **ALTER** permission on the database.
  
## Examples  

The following example clears all queued (non-persisted) Query Store messages pending. The action applies to the replica against which the command is executed.
  
```sql
EXEC sp_query_store_clear_message_queues;  
```  
  
## Next steps

Learn more about Query Store in the following articles:

- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md)
- [sys.database_query_store_internal_state (Transact-SQL)](../system-catalog-views/sys-database-query-store-internal-state-transact-sql.md)
- [Query Store catalog views (Transact-SQL)](../system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Query Store stored procedures (Transact-SQL)](query-store-stored-procedures-transact-sql.md)
