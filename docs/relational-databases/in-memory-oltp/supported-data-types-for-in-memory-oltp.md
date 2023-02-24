---
title: "Supported Data Types for In-Memory OLTP"
description: Learn about data types that are unsupported for the In-Memory OLTP features memory-optimized tables and natively compiled T-SQL modules.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "06/19/2017"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: a7380ef0-c9d7-49e4-b6de-fad34752b9f3
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Supported Data Types for In-Memory OLTP
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  This article lists the data types that are unsupported for the In-Memory OLTP features of:  
  
-   Memory-optimized tables  
  
-   Natively compiled T-SQL modules  
  
## Unsupported Data Types  
 The following data types are not supported:  

:::row:::
    :::column:::
        [datetimeoffset &#40;Transact-SQL&#41;](../../t-sql/data-types/datetimeoffset-transact-sql.md)

        [hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)

        [sql_variant &#40;Transact-SQL&#41;](../../t-sql/data-types/sql-variant-transact-sql.md)
    :::column-end:::
    :::column:::
        [geography &#40;Transact-SQL&#41;](../../t-sql/spatial-geography/spatial-types-geography.md)

        [rowversion &#40;Transact-SQL&#41;](../../t-sql/data-types/rowversion-transact-sql.md)

        User-Defined Types
    :::column-end:::
    :::column:::
        [geometry &#40;Transact-SQL&#41;](../../t-sql/spatial-geometry/spatial-types-geometry-transact-sql.md)

        [xml &#40;Transact-SQL&#41;](../../t-sql/xml/xml-transact-sql.md)
    :::column-end:::
:::row-end:::
  
## Notable Supported Data Types  
 Most data types are supported by the features of In-Memory OLTP. The following few are worth noting explicitly:  
  
|String and Binary Types|For more information|  
|-----------------------------|--------------------------|  
|binary and varbinary*|[binary and varbinary &#40;Transact-SQL&#41;](../../t-sql/data-types/binary-and-varbinary-transact-sql.md)|  
|char and varchar*|[char and varchar &#40;Transact-SQL&#41;](../../t-sql/data-types/char-and-varchar-transact-sql.md)|  
|nchar and nvarchar*|[nchar and nvarchar &#40;Transact-SQL&#41;](../../t-sql/data-types/nchar-and-nvarchar-transact-sql.md)|  
  
For the preceding string and binary data types, starting with SQL Server 2016:  
  
- An individual memory-optimized table can also have several long columns such as `nvarchar(4000)`, even though their lengths would add to more than the physical row size of 8060 bytes.  
  
- A memory-optimized table can have max length string and binary columns of data types such as `varchar(max)`.  


### Identify LOBs and other columns that are off-row

Starting with SQL Server 2016, memory-optimized tables [support off-row columns](../../relational-databases/in-memory-oltp/table-and-row-size-in-memory-optimized-tables.md), which allow a single table row to be larger than 8060 bytes. The following Transact-SQL SELECT statement reports all columns that are off-row, for memory-optimized tables. Note that:

- All index key columns are stored in-row.
  - Nonunique index keys can now include NULLable columns, on memory-optimized tables.
  - Indexes can be declared as UNIQUE on a memory-optimized table.
- All LOB columns are stored off-row.
- A max_length of -1 indicates a large object (LOB) column.


```sql
SELECT
        OBJECT_NAME(m.object_id) as [table],
        c.name                   as [column],
        c.max_length
    FROM
             sys.memory_optimized_tables_internal_attributes AS m
        JOIN sys.columns                                     AS c
                ON  m.object_id = c.object_id
                AND m.minor_id  = c.column_id
    WHERE
        m.type = 5;
```


### Other Data Types


|Other Types|For more information|  
|-----------------|--------------------------|  
|table types|[Memory-Optimized Table Variables](../../relational-databases/in-memory-oltp/faster-temp-table-and-table-variable-by-using-memory-optimization.md)|  
  
## See Also  
 [Transact-SQL Support for In-Memory OLTP](../../relational-databases/in-memory-oltp/transact-sql-support-for-in-memory-oltp.md)   
 [Implementing SQL_VARIANT in a Memory-Optimized Table](../../relational-databases/in-memory-oltp/implementing-sql-variant-in-a-memory-optimized-table.md)  
 [Table and Row size in Memory-Optimized Table](../../relational-databases/in-memory-oltp/table-and-row-size-in-memory-optimized-tables.md)  
  
  
