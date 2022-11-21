---
title: "sys.dm_db_xtp_table_memory_stats (Transact-SQL)"
description: sys.dm_db_xtp_table_memory_stats returns memory usage statistics for each In-Memory OLTP table (user and system) in the current database.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_xtp_table_memory_stats_TSQL"
  - "dm_db_xtp_table_memory_stats"
  - "sys.dm_db_xtp_table_memory_stats"
  - "dm_db_xtp_table_memory_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_db_xtp_table_memory_stats"
  - "dm_db_xtp_table_memory_stats"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_xtp_table_memory_stats (Transact-SQL)
[!INCLUDE[sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns memory usage statistics for each [!INCLUDE[inmemory](../../includes/inmemory-md.md)] table (user and system) in the current database. The system tables have negative object IDs and are used to store run-time information for the [!INCLUDE[inmemory](../../includes/inmemory-md.md)] engine. Unlike user objects, system tables are internal and only exist in-memory, therefore, they are not visible through catalog views. System tables are used to store information such as meta-data for all data/delta files in storage, merge requests, watermarks for delta files to filter rows, dropped tables, and relevant information for recovery and backups. Given that the [!INCLUDE[inmemory](../../includes/inmemory-md.md)] engine can have up to 8,192 data and delta file pairs, for large in-memory databases, the memory taken by system tables can be a few megabytes.  
  
 For more information, see [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|The object ID of the table. `NULL` for [!INCLUDE[inmemory](../../includes/inmemory-md.md)] system tables.|  
|memory_allocated_for_table_kb|**bigint**|Memory allocated for this table.|  
|memory_used_by_table_kb|**bigint**|Memory used by table, including row versions.|  
|memory_allocated_for_indexes_kb|**bigint**|Memory allocated for indexes on this table.|  
|memory_used_by_indexes_kb|**bigint**|Memory consumed for indexes on this table.|  
  
## Permissions  
 All rows are returned if you have VIEW DATABASE STATE permission on the current database. Otherwise, an empty rowset is returned.  
  
 If you do not have VIEW DATABASE permission, all columns will be returned for rows in tables that you have SELECT permission on.  
  
 System tables are returned only for users with VIEW DATABASE STATE permission.  
  
## Examples  
 You can query the following DMV to get the memory allocated for the tables and indexes within the database:  
  
```sql  
-- finding memory for objects  
SELECT OBJECT_NAME(object_id), *   
FROM sys.dm_db_xtp_table_memory_stats;  
```  
  
 To find memory for all objects within the database:  
  
```sql  
SELECT SUM( memory_allocated_for_indexes_kb + memory_allocated_for_table_kb) AS  
 memoryallocated_objects_in_kb   
FROM sys.dm_db_xtp_table_memory_stats;  
```  
  
## User Scenario  
 
 First, set the max server memory to 4GB as a safety measure. You may want to consider a different value for your environment. 
  
```sql  
-- set max server memory to 4 GB  
EXEC sp_configure 'max server memory (MB)', 4048  
go  
  
RECONFIGURE  
go  
```

Create a resource pool for the database that contains the memory-optimized objects.

```sql  
-- create a resource pool for the database with memory-optimized objects  
CREATE RESOURCE POOL PoolHkDb1 WITH (MAX_MEMORY_PERCENT = 50);  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
go  
```

Bind the resource pool 'PoolHkdb1' to the database 'HkDb1'. This requires taking the database offline/online to associate the pool.

```sql
--bind the pool to the database  
EXEC sp_xtp_bind_db_resource_pool 'HkDb1', 'PoolHkdb1'  
go  
  
-- take database offline/online to associate the pool  
use master  
go  
  
alter database HkDb1 set offline  
go  
alter database HkDb1 set online  
go  
```

Create the following tables in a database called `HkDb1`. 

```sql
USE HkDb1  
GO
  
CREATE TABLE dbo.t1 (  
       c1 int NOT NULL,  
       c2 char(40) NOT NULL,  
       c3 char(8000) NOT NULL,  
  
       CONSTRAINT [pk_t1_c1] PRIMARY KEY NONCLUSTERED HASH (c1) WITH (BUCKET_COUNT = 100000)  
) WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA);
GO
  
CREATE TABLE dbo.t2 (  
       c1 int NOT NULL,  
       c2 char(40) NOT NULL,  
       c3 char(8000) NOT NULL,  
  
       CONSTRAINT [pk_t2_c1] PRIMARY KEY NONCLUSTERED HASH (c1) WITH (BUCKET_COUNT = 100000)  
) WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA);
GO  
  
CREATE TABLE dbo.t3 (  
       c1 int NOT NULL,  
       c2 char(40) NOT NULL,  
       c3 char(8000) NOT NULL,  
  
       CONSTRAINT [pk_t3_c1] PRIMARY KEY NONCLUSTERED HASH (c1) WITH (BUCKET_COUNT = 1000000)  
) WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA)  
GO
```

Load data into the table.

```sql  
-- load 150K rows  
DECLARE @i int = 0  
WHILE (@i <= 150000)  
BEGIN  
       insert t1 values (@i, 'a', replicate ('b', 8000))  
       set @i += 1;  
END  
GO  
```  
  
 When data is loaded into a table, you can see user defined tables and how much storage it is using. For example, each row of a table could be approximately 8070 bytes (allocation size is 8K (8192 bytes)). You can see indexes per table and how much storage the index uses. For example, 1MB is 100K entries rounded to the next power of 2 (2**17) = 131072 of 8 bytes each. A table may not have an index, in which case it will show memory allocation for the index. Other rows may represent system tables  
  
```sql  
select convert(char(10), object_name(object_id)) as Name,*   
from sys.dm_db_xtp_table_memory_stats;
```  
  
 Here is the output, in two parts:  
  
```output
Name       object_id   memory_allocated_for_table_kb memory_used_by_table_kb  
---------- ----------- ----------------------------- -----------------------  
t3         629577281   0                             0  
t1         565577053   1372928                       1202351  
t2         597577167   0                             0  
NULL       -6          0                             0  
NULL       -5          0                             0  
NULL       -4          0                             0  
NULL       -3          0                             0  
NULL       -2          192                           25  
  
memory_allocated_for_indexes_kb memory_used_by_indexes_kb  
------------------------------- -------------------------  
8192                            8192  
1024                            1024  
8192                            8192  
2                               2  
24                              24  
2                               2  
2                               2  
16                              16  
```  
  
 The output of,  
  
```sql  
select  sum(allocated_bytes)/(1024*1024) as total_allocated_MB,   
       sum(used_bytes)/(1024*1024) as total_used_MB  
from sys.dm_db_xtp_memory_consumers;
```  
  
 is:
  
```output  
total_allocated_MB   total_used_MB  
-------------------- --------------------  
1357                 1191  
```  
  
 Next, let's look at the output from the resource pool. Note, that memory used from the pool is 1356 MB. 
  
```sql  
select pool_id,convert(char(10), name) as Name, min_memory_percent, max_memory_percent,   
   max_memory_kb/1024 as max_memory_mb  
from sys.dm_resource_governor_resource_pools; 
  
select used_memory_kb/1024 as used_memory_mb ,target_memory_kb/1024 as target_memory_mb  
from sys.dm_resource_governor_resource_pools;
```  
  
 The output:  
  
```output  
pool_id     Name       min_memory_percent max_memory_percent max_memory_mb  
----------- ---------- ------------------ ------------------ --------------------  
1           internal   0                  100                3845  
2           default    0                  100                3845  
259         PoolHkDb1  0                  100                3845  
  
used_memory_mb       target_memory_mb  
-------------------- --------------------  
125                  3845  
32                   3845  
1356                 3845  
```  
  
## See also

- [Introduction to Memory-Optimized Tables](../in-memory-oltp/introduction-to-memory-optimized-tables.md)
- [Memory-Optimized Table Dynamic Management Views](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)

## Next steps 

- [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] Overview and Usage Scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
- [Optimize performance by using in-memory technologies in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/in-memory-oltp-overview)
