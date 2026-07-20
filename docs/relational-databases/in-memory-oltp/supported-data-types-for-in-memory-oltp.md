---
title: Supported Data Types for In-Memory OLTP
description: Learn about data types that are unsupported for the In-Memory OLTP features memory-optimized tables and natively compiled T-SQL modules.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 10/27/2025
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: reference
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Supported data types for In-Memory OLTP

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article lists the data types that are unsupported for the In-Memory OLTP features of:

- Memory-optimized tables
- Natively compiled Transact-SQL (T-SQL) modules

## Unsupported data types

The following data types aren't supported:

- [datetimeoffset](../../t-sql/data-types/datetimeoffset-transact-sql.md)
- [geography](../../t-sql/spatial-geography/spatial-types-geography.md)
- [geometry](../../t-sql/spatial-geometry/spatial-types-geometry-transact-sql.md)
- [hierarchyid](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
- [json](../../t-sql/data-types/json-data-type.md)
- [rowversion](../../t-sql/data-types/rowversion-transact-sql.md)
- [sql_variant](../../t-sql/data-types/sql-variant-transact-sql.md)
- [vector](../../t-sql/data-types/vector-data-type.md)
- [xml](../../t-sql/xml/xml-transact-sql.md)
- User-defined types

## Notable supported data types

In-Memory OLTP features support most data types. The following list is worth noting explicitly:

| String and binary types | For more information |
| --- | --- |
| **binary** and **varbinary** | [binary and varbinary](../../t-sql/data-types/binary-and-varbinary-transact-sql.md) |
| **char** and **varchar** | [char and varchar](../../t-sql/data-types/char-and-varchar-transact-sql.md) |
| **nchar** and **nvarchar** | [nchar and nvarchar](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md) |

For the preceding string and binary data types, starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]:

- An individual memory-optimized table can also have several long columns such as **nvarchar(4000)**, even though their lengths would add to more than the physical row size of 8,060 bytes.

- A memory-optimized table can have max length string and binary columns of data types such as **varchar(max)**.

### Identify LOBs and other columns that are off-row

In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, memory-optimized tables support [off-row columns](table-and-row-size-in-memory-optimized-tables.md), which allow a single table row to be larger than 8,060 bytes. The following Transact-SQL `SELECT` statement reports all columns that are off-row, for memory-optimized tables:

- All index key columns are stored in-row.
  - Nonunique index keys can include nullable columns on memory-optimized tables.
  - Indexes can be declared as `UNIQUE` on a memory-optimized table.
- All LOB columns are stored off-row.
- A `max_length` of `-1` indicates a large object (LOB) column.

```sql
SELECT OBJECT_NAME(m.object_id) AS [table],
       c.name AS [column],
       c.max_length
FROM sys.memory_optimized_tables_internal_attributes AS m
     INNER JOIN sys.columns AS c
         ON m.object_id = c.object_id
        AND m.minor_id = c.column_id
WHERE m.type = 5;
```

### Other data types

| Other types | For more information |
| --- | --- |
| **table** types | [Faster temp table and table variable by using memory optimization](faster-temp-table-and-table-variable-by-using-memory-optimization.md) |

## Related content

- [Transact-SQL Support for In-Memory OLTP](transact-sql-support-for-in-memory-oltp.md)
- [Implementing SQL_VARIANT in a Memory-Optimized Table](implementing-sql-variant-in-a-memory-optimized-table.md)
- [Table and row size in memory-optimized tables](table-and-row-size-in-memory-optimized-tables.md)
