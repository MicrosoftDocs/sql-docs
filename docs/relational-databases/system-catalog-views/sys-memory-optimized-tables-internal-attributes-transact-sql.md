---
title: "sys.memory_optimized_tables_internal_attributes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.memory_optimized_tables_internal_attributes"
  - "sys.memory_optimized_tables_internal_attributes_TSQL"
  - "memory_optimized_tables_internal_attributes"
  - "memory_optimized_tables_internal_attributes_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.memory_optimized_tables_internal_attributes catalog view"
ms.assetid: 78ef5807-0504-4de8-9a01-ede6c03c7ff1
author: "jodebrui"
ms.author: "jodebrui"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.memory_optimized_tables_internal_attributes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

Contains a row for each internal memory-optimized table used for storing user memory-optimized tables. Each user table corresponds to one or more internal tables. A single table is used for the core data storage. Additional internal tables are used to support features such as temporal, columnstore index and off-row (LOB) storage for memory-optimized tables.
 
| Column name  | Data type  | Description |
| :------ |:----------| :-----|
|object_id  |**int**|       ID of the user table. Internal memory-optimized tables that exist to support a user table (such as off-row storage or deleted rows in case of Hk/Columnstore combinations) have the same object_id as their parent. |
|xtp_object_id  |**bigint**|    In-Memory OLTP object ID corresponding to the internal memory-optimized table that is used to support the user table. It is unique within the database and it can change over the lifetime of the object. 
|type|  **int** |   Type of internal table.<br/><br/> 0 => DELETED_ROWS_TABLE <br/> 1 => USER_TABLE <br/> 2 => DICTIONARIES_TABLE<br/>3 => SEGMENTS_TABLE<br/>4 => ROW_GROUPS_INFO_TABLE<br/>5 => INTERNAL OFF-ROW DATA TABLE<br/>252 => INTERNAL_TEMPORAL_HISTORY_TABLE | 
|type_desc| **nvarchar(60)**|   Description of the type<br/><br/>DELETED_ROWS_TABLE -> Internal table tracking deleted rows for a columnstore index<br/>USER_TABLE -> Table containing the in-row user data<br/>DICTIONARIES_TABLE -> Dictionaries for a columnstore index<br/>SEGMENTS_TABLE -> Compressed segments for a columnstore index<br/>ROW_GROUPS_INFO_TABLE -> Metadata about compressed row groups of a columnstore index<br/>INTERNAL OFF-ROW DATA TABLE -> Internal table used for storage of an off-row column. In this case, minor_id reflects the column_id.<br/>INTERNAL_TEMPORAL_HISTORY_TABLE -> Hot tail of the disk-based history table. Rows inserted into the history are inserted into this internal memory-optimized table first. There is a background task that asynchronously moves rows from this internal table to the disk-based history table. |
|minor_id|  **int**|    0 indicates a user or internal table<br/><br/>Non-0 indicates the ID of a column stored off-row. Joins with column_id in sys.columns.<br/><br/>Each column stored off-row has a corresponding row in this system view.|

## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
  
### A. Returning all columns that are stored off-row

The following T-SQL script illustrates a table with multiple large non-LOB columns and a single LOB column:

```Transact-SQL
CREATE TABLE dbo.LargeTableSample
(
      Id   int IDENTITY PRIMARY KEY NONCLUSTERED,
      C1   nvarchar(4000),
      C2   nvarchar(4000),
      C3   nvarchar(4000),
      C4   nvarchar(4000),
      Misc nvarchar(max)
) WITH (MEMORY_OPTIMIZED = ON);
GO
```

The following query shows all columns that are stored off-row, along with their sizes. A size of -1 indicates a LOB column. All LOB columns are stored off-row.

```Transact-SQL
SELECT 
  QUOTENAME(SCHEMA_NAME(o.schema_id)) + N'.' + QUOTENAME(OBJECT_NAME(moa.object_id)) AS 'table', 
  c.name AS 'column', 
  c.max_length
FROM sys.memory_optimized_tables_internal_attributes moa
     JOIN sys.columns c ON moa.object_id = c.object_id AND moa.minor_id=c.column_id
     JOIN sys.objects o on moa.object_id=o.object_id 
WHERE moa.type=5;
```

### B. Returning memory consumption of all columns that are stored off-row

To get more details about the memory consumption of off-row columns you can use the following query, which shows the memory consumption of all internal tables and their indexes that are used to store the off-row columns:

```Transact-SQL
SELECT
  QUOTENAME(SCHEMA_NAME(o.schema_id)) + N'.' + QUOTENAME(OBJECT_NAME(moa.object_id)) AS 'table',
  c.name AS 'column',
  c.max_length,
  mc.memory_consumer_desc,
  mc.index_id,
  mc.allocated_bytes,
  mc.used_bytes
FROM sys.memory_optimized_tables_internal_attributes moa
   JOIN sys.columns c ON moa.object_id = c.object_id AND moa.minor_id=c.column_id
   JOIN sys.dm_db_xtp_memory_consumers mc ON moa.xtp_object_id=mc.xtp_object_id
   JOIN sys.objects o on moa.object_id=o.object_id 
WHERE moa.type=5;
```

### C. Returning memory consumption of columnstore indexes on memory-optimized tables

Use the following query to show the memory consumption of columnstore indexes on memory-optimized tables:

```Transact-SQL
SELECT
  QUOTENAME(SCHEMA_NAME(o.schema_id)) + N'.' + QUOTENAME(OBJECT_NAME(moa.object_id)) AS 'table',
  i.name AS 'columnstore index',
  SUM(mc.allocated_bytes) / 1024 as [allocated_kb],
  SUM(mc.used_bytes) / 1024 as [used_kb]
FROM sys.memory_optimized_tables_internal_attributes moa
   JOIN sys.indexes i ON moa.object_id = i.object_id AND i.type in (5,6)
   JOIN sys.dm_db_xtp_memory_consumers mc ON moa.xtp_object_id=mc.xtp_object_id
   JOIN sys.objects o on moa.object_id=o.object_id
WHERE moa.type IN (0, 2, 3, 4)
GROUP BY o.schema_id, moa.object_id, i.name;
```

Use the following query break down the memory consumption across internal structures used for columnstore indexes on memory-optimized tables:

```Transact-SQL
SELECT
  QUOTENAME(SCHEMA_NAME(o.schema_id)) + N'.' + QUOTENAME(OBJECT_NAME(moa.object_id)) AS 'table',
  i.name AS 'columnstore index',
  moa.type_desc AS 'internal table',
  mc.index_id AS 'index',
  mc.memory_consumer_desc,
  mc.allocated_bytes / 1024 as [allocated_kb],
  mc.used_bytes / 1024 as [used_kb]
FROM sys.memory_optimized_tables_internal_attributes moa
   JOIN sys.indexes i ON moa.object_id = i.object_id AND i.type in (5,6)
   JOIN sys.dm_db_xtp_memory_consumers mc ON moa.xtp_object_id=mc.xtp_object_id
   JOIN sys.objects o on moa.object_id=o.object_id
WHERE moa.type IN (0, 2, 3, 4)
```


