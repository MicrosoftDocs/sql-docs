---
title: "Optimize JSON processing with in-memory OLTP"
description: "Optimize JSON processing with in-memory OLTP"
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: jroth, randolphwest
ms.date: 12/21/2022
ms.service: sql
ms.topic: conceptual
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Optimize JSON processing with in-memory OLTP

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

SQL Server and Azure SQL Database let you work with text formatted as JSON. To increase the performance of queries that process JSON data, you can store JSON documents in memory-optimized tables using standard string columns (**nvarchar** type). Storing JSON data in memory-optimized tables increases query performance by using lock-free, in-memory data access.

## Store JSON in memory-optimized tables

The following example shows a memory-optimized `Product` table with two JSON columns, `Tags` and `Data`:

```sql
CREATE SCHEMA xtp;
GO

CREATE TABLE xtp.Product (
    ProductID INT PRIMARY KEY NONCLUSTERED, --standard column
    Name NVARCHAR(400) NOT NULL, --standard column
    Price FLOAT, --standard column
    Tags NVARCHAR(400), --JSON stored in string column
    Data NVARCHAR(4000) --JSON stored in string column
)
WITH (MEMORY_OPTIMIZED = ON);
GO
```

## Optimize JSON processing with additional in-memory features

You can fully integrate JSON functionality with existing in-memory OLTP technologies. For example, you can do the following things:

- [Validate the structure of JSON documents](#validate) stored in memory-optimized tables by using natively compiled CHECK constraints.
- [Expose and strongly type values](#computedcol) stored in JSON documents by using computed columns.
- [Index values](#index) in JSON documents by using memory-optimized indexes.
- [Natively compile SQL queries](#compile) that use values from JSON documents or that format results as JSON text.

## <a id="validate"></a> Validate JSON columns

You can add natively compiled CHECK constraints that validate the content of JSON documents stored in a string column, to ensure that JSON text stored in your memory-optimized tables is properly formatted.

The following example creates a `Product` table with a JSON column `Tags`. The `Tags` column has a CHECK constraint that uses the `ISJSON` function to validate the JSON text in the column.

```sql
DROP TABLE IF EXISTS xtp.Product;
GO
CREATE TABLE xtp.Product (
    ProductID INT PRIMARY KEY NONCLUSTERED,
    Name NVARCHAR(400) NOT NULL,
    Price FLOAT,
    Tags NVARCHAR(400)
        CONSTRAINT [Tags should be formatted as JSON] CHECK (ISJSON(Tags) = 1),
    Data NVARCHAR(4000)
)
WITH (MEMORY_OPTIMIZED = ON);
GO
```

You can also add the natively compiled CHECK constraint to an existing table that contains JSON columns.

```sql
ALTER TABLE xtp.Product
    ADD CONSTRAINT [Data should be JSON]
        CHECK (ISJSON(Data)=1);
```

## <a id="computedcol"></a> Expose JSON values using computed columns

Computed columns let you expose values from JSON text and access those values without fetching the value from the JSON text again and without parsing the JSON structure again. Values exposed in this way are strongly typed and physically persisted in the computed columns. Accessing JSON values using persisted computed columns is faster than accessing values in the JSON document directly.

The following example shows how to expose the following two values from the JSON `Data` column:

- The country/region where a product is made.
- The product manufacturing cost.

In this example, the computed columns `MadeIn` and `Cost` are updated every time the JSON document stored in the `Data` column changes.

```sql
DROP TABLE IF EXISTS xtp.Product;
GO
CREATE TABLE xtp.Product (
    ProductID INT PRIMARY KEY NONCLUSTERED,
    Name NVARCHAR(400) NOT NULL,
    Price FLOAT,
    Data NVARCHAR(4000),
    MadeIn AS CAST(JSON_VALUE(Data, '$.MadeIn') AS NVARCHAR(50)) PERSISTED,
    Cost AS CAST(JSON_VALUE(Data, '$.ManufacturingCost') AS FLOAT) PERSISTED
)
WITH (MEMORY_OPTIMIZED = ON);
GO
```

## <a id="index"></a> Index values in JSON columns

You can index values in JSON columns by using memory-optimized indexes. JSON values that are indexed must be exposed and strongly typed by using computed columns, as described in the preceding example.

Values in JSON columns can be indexed by using both standard NONCLUSTERED and HASH indexes.

- NONCLUSTERED indexes optimize queries that select ranges of rows by some JSON value or sort results by JSON values.
- HASH indexes optimize queries that select a single row or a few rows by specifying an exact value to find.

The following example builds a table that exposes JSON values by using two computed columns. The example creates a NONCLUSTERED index on one JSON value and a HASH index on the other.

```sql
DROP TABLE IF EXISTS xtp.Product;
GO
CREATE TABLE xtp.Product (
    ProductID INT PRIMARY KEY NONCLUSTERED,
    Name NVARCHAR(400) NOT NULL,
    Price FLOAT,
    Data NVARCHAR(4000),
    MadeIn AS CAST(JSON_VALUE(Data, '$.MadeIn') AS NVARCHAR(50)) PERSISTED,
    Cost AS CAST(JSON_VALUE(Data, '$.ManufacturingCost') AS FLOAT) PERSISTED,
    INDEX [idx_Product_MadeIn] NONCLUSTERED (MadeIn)
)
WITH (MEMORY_OPTIMIZED = ON);
GO

ALTER TABLE Product ADD INDEX [idx_Product_Cost] NONCLUSTERED HASH (Cost)
    WITH (BUCKET_COUNT = 20000);
```

## <a id="compile"></a> Native compilation of JSON queries

If your procedures, functions, and triggers contain queries that use the built-in JSON functions, native compilation increases the performance of these queries and reduces the CPU cycles required to run them.

The following example shows a natively compiled procedure that uses several JSON functions: `JSON_VALUE`, `OPENJSON`, and `JSON_MODIFY`.

```sql
CREATE PROCEDURE xtp.ProductList (@ProductIds NVARCHAR(100))
WITH SCHEMABINDING, NATIVE_COMPILATION
AS BEGIN
    ATOMIC WITH (TRANSACTION ISOLATION LEVEL = snapshot, LANGUAGE = N'English')

    SELECT ProductID,
        Name,
        Price,
        Data,
        Tags,
        JSON_VALUE(data, '$.MadeIn') AS MadeIn
    FROM xtp.Product
    INNER JOIN OPENJSON(@ProductIds)
        ON ProductID = value
END;
GO

CREATE PROCEDURE xtp.UpdateProductData (
    @ProductId INT,
    @Property NVARCHAR(100),
    @Value NVARCHAR(100)
)
WITH SCHEMABINDING, NATIVE_COMPILATION
AS BEGIN
    ATOMIC WITH (TRANSACTION ISOLATION LEVEL = snapshot, LANGUAGE = N'English')

    UPDATE xtp.Product
    SET Data = JSON_MODIFY(Data, @Property, @Value)
    WHERE ProductID = @ProductId;
END
GO
```

## Next steps

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

- [JSON as a bridge between NoSQL and relational worlds](/events/datadriven-sqlserver2016/json-as-bridge-betwen-nosql-relational-worlds)
