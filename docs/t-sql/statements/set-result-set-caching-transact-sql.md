---
title: "SET RESULT SET CACHING  (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/03/2019"
ms.prod: sql
ms.prod_service: "sql-data-warehouse"
ms.reviewer: "jrasnick"
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
dev_langs:
  - "TSQL"
helpviewer_keywords: 
author: XiaoyuL-Preview
ms.author: xiaoyul
manager: craigg
monikerRange: "=azure-sqldw-latest || = sqlallproducts-allversions"
---
# SET RESULT SET CACHING (Transact-SQL)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Causes Azure SQL Data Warehouse to cache query result sets.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax

```
SET RESULT_SET_CACHING { ON | OFF };
```  
  
## Remarks  
  
This command must be run while connected to the master database.  Change to this database setting takes effect immediately.  Storage costs are incurred by caching query result sets. After disabling result caching for a database, previously persisted result cache will immediately be deleted from Azure SQL Data Warehouse storage. A new column called is_result_set_caching_on is introduced in [sys.databases](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql?view=azure-sqldw-latest
) to show the result caching setting for a database.  

**ON**
Specifies that query result sets returned from this database will be cached in Azure SQL Data Warehouse storage.

**OFF**
Specifies that query result sets returned from this database will not be cached in Azure SQL Data Warehouse storage.

Users can tell if a query was executed with a result cache hit or miss by querying [sys.pdw_request_steps](/sql/relational-databases/system-dynamic-management-views/sys-dm-pdw-request-steps-transact-sql?view=azure-sqldw-latest) with a specific request_id. If there is a cache hit, the query result will have a single step with following details:

|**Column name**|**Operator**|**Value**|
|----|----|----|
|operation_type|=|ReturnOperation|
|step_index|=|0|
|location_type|=|Control|
|command|Like|%DWResultCacheDb%|
||||
  
## Permissions

Requires these permissions:

- Server-level principal login (the one created by the provisioning process)
  or
- Member of the dbmanager database role.

The owner of the database cannot alter the database unless the owner is a member of the dbmanager role.
  
## Examples

### Enable result set caching for a database

```sql
ALTER DATABASE myTestDW  
SET RESULT_SET_CACHING ON;
```

### Disable result set caching for a database

```sql
ALTER DATABASE myTestDW  
SET RESULT_SET_CACHING OFF;
```

### Check result set caching setting for a database

```sql
SELECT name, is_result_set_caching  
FROM sys.databases
```

### Check for number of queries with result set cache hit and cache miss

```sql
SELECT  
Queries=CacheHits+CacheMisses,
CacheHits,
CacheMisses,
CacheHitPct=CacheHits*1.0/(CacheHits+CacheMisses)
FROM  
(SELECT  
CacheHits=count(distinct case when s.command like '%DWResultCacheDb%' and
r.resource_class IS NULL and s.operation_type = 'ReturnOperation' and  
s.step_index = 0 then s.request_id else null end) ,
CacheMisses=count(distinct case when r.resource_class IS NOT NULL then  
s.request_id else null end)
     FROM sys.dm_pdw_request_steps s  
     JOIN sys.dm_pdw_exec_requests r  
     ON s.request_id = r.request_id) A
```

### Check for result set cache hit or cache miss for a query

```sql
If
(SELECT step_index  
FROM sys.dm_pdw_request_steps  
WHERE request_id = 'QID58286'
      and operation_type = 'ReturnOperation'
      and command like '%DWResultCacheDb%') = 0
SELECT 1 as is_cache_hit  
ELSE
SELECT 0 as is_cache_hit
```

### Check for all queries with result set cache hits

```sql
SELECT *  
FROM sys.dm_pdw_request_steps  
WHERE command like '%DWResultCacheDb%' and step_index = 0
```

## See Also

[SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)</br>
[ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql?view=azure-sqldw-latest)