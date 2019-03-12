---
title: "Optimize JSON processing with in-memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: d9c5adb1-3209-4186-bc10-8e41a26f5e57
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Optimize JSON processing with in-memory OLTP
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

SQL Server and Azure SQL Database let you work with text formatted as JSON. To increase the performance of queries that process JSON data, you can store JSON documents in memory-optimized tables using standard string columns (NVARCHAR type). Storing JSON data in memory-optimized tables increases query performance by leveraging lock-free, in-memory data access.

## Store JSON in memory-optimized tables
The following example shows a memory-optimized `Product` table with two JSON columns, `Tags` and `Data`:

```sql
CREATE SCHEMA xtp;
GO
CREATE TABLE xtp.Product(
	ProductID int PRIMARY KEY NONCLUSTERED, --standard column
	Name nvarchar(400) NOT NULL, --standard column
	Price float, --standard column

	Tags nvarchar(400),--json stored in string column
	Data nvarchar(4000) --json stored in string column

) WITH (MEMORY_OPTIMIZED=ON);
```

## Optimize JSON processing with additional in-memory features
Features that are available in SQL Server and Azure SQL Database let you fully integrate JSON functionality with existing in-memory OLTP technologies. For example, you can do the following things:
 - [Validate the structure of JSON documents](#validate) stored in memory-optimized tables by using natively compiled CHECK constraints.
 - [Expose and strongly type values](#computedcol) stored in JSON documents by using computed columns.
 - [Index values](#index) in JSON documents by using memory-optimized indexes.
 - [Natively compile SQL queries](#compile) that use values from JSON documents or that format results as JSON text.

## <a name="validate"></a> Validate JSON columns
SQL Server and Azure SQL Database let you add natively compiled CHECK constraints that validate the content of JSON documents stored in a string column. With natively compiled JSON CHECK constraints, you can ensure that JSON text stored in your memory-optimized tables is properly formatted.

The following example creates a `Product` table with a JSON column `Tags`. The `Tags` column has a CHECK constraint that uses the `ISJSON` function to validate the JSON text in the column.

```sql
DROP TABLE IF EXISTS xtp.Product;
GO
CREATE TABLE xtp.Product(
	ProductID int PRIMARY KEY NONCLUSTERED,
	Name nvarchar(400) NOT NULL,
	Price float,

	Tags nvarchar(400)
        	CONSTRAINT [Tags should be formatted as JSON]
			    CHECK (ISJSON(Tags)=1),
	Data nvarchar(4000)

) WITH (MEMORY_OPTIMIZED=ON);
```

You can also add the natively compiled CHECK constraint to an existing table that contains JSON columns.

```sql
ALTER TABLE xtp.Product
    ADD CONSTRAINT [Data should be JSON]
        CHECK (ISJSON(Data)=1)
```

## <a name="computedcol"></a> Expose JSON values using computed columns
Computed columns let you expose values from JSON text and access those values without fetching the value from the JSON text again and without parsing the JSON structure again. Values exposted in this way are strongly typed and physically persisted in the computed columns. Accessing JSON values using persisted computed columns is faster than accessing values in the JSON document directly.

The following example shows how to expose the following two values from the JSON `Data` column:
-   The country where a product is made.
-   The product manufacturing cost.

In this example, the computed columns `MadeIn` and `Cost` are updated every time the JSON document stored in the `Data` column changes.

```sql
DROP TABLE IF EXISTS xtp.Product;
GO
CREATE TABLE xtp.Product(
	ProductID int PRIMARY KEY NONCLUSTERED,
	Name nvarchar(400) NOT NULL,
	Price float,

	Data nvarchar(4000),

	MadeIn AS CAST(JSON_VALUE(Data, '$.MadeIn') as NVARCHAR(50)) PERSISTED,
	Cost   AS CAST(JSON_VALUE(Data, '$.ManufacturingCost') as float)

) WITH (MEMORY_OPTIMIZED=ON);
```

## <a name="index"></a> Index values in JSON columns
SQL Server and Azure SQL Database let you index values in JSON columns by using memory-optimized indexes. JSON values that are indexed must be exposed and strongly typed by using computed columns, as described in the preceding example.

Values in JSON columns can be indexed by using both standard NONCLUSTERED and HASH indexes.
-   NONCLUSTERED indexes optimize queries that select ranges of rows by some JSON value or sort results by JSON values.
-   HASH indexes optimize queries that select a single row or a few rows by specifying an exact value to find.

The following example builds a table that exposes JSON values by using two computed columns. The example creates a NONCLUSTERED index on one JSON value and a HASH index on the other.

```sql
DROP TABLE IF EXISTS xtp.Product;
GO
CREATE TABLE xtp.Product(
	ProductID int PRIMARY KEY NONCLUSTERED,
	Name nvarchar(400) NOT NULL,
	Price float,

	Data nvarchar(4000),

	MadeIn AS CAST(JSON_VALUE(Data, '$.MadeIn') as NVARCHAR(50)) PERSISTED,
	Cost   AS CAST(JSON_VALUE(Data, '$.ManufacturingCost') as float) PERSISTED,

    INDEX [idx_Product_MadeIn] NONCLUSTERED (MadeIn)

) WITH (MEMORY_OPTIMIZED=ON)

ALTER TABLE Product
    ADD INDEX [idx_Product_Cost] NONCLUSTERED HASH(Cost)
        WITH (BUCKET_COUNT=20000)
```

## <a name="compile"></a> Native compilation of JSON queries
If your procedures, functions, and triggers contain queries that use the built-in JSON functions, native compilation increases the performance of these queries and reduces the CPU cycles required to run them.

The following example shows a natively compiled procedure that uses several JSON functions - **JSON_VALUE**, **OPENJSON**, and **JSON_MODIFY**.

```sql
CREATE PROCEDURE xtp.ProductList(@ProductIds nvarchar(100))
WITH SCHEMABINDING, NATIVE_COMPILATION
AS BEGIN
	ATOMIC WITH (transaction isolation level = snapshot,  language = N'English')

	SELECT ProductID,Name,Price,Data,Tags, JSON_VALUE(data,'$.MadeIn') AS MadeIn
	FROM xtp.Product
		JOIN OPENJSON(@ProductIds)
			ON ProductID = value

END;

CREATE PROCEDURE xtp.UpdateProductData(@ProductId int, @Property nvarchar(100), @Value nvarchar(100))
WITH SCHEMABINDING, NATIVE_COMPILATION
AS BEGIN
	ATOMIC WITH (transaction isolation level = snapshot,  language = N'English')

	UPDATE xtp.Product
	SET Data = JSON_MODIFY(Data, @Property, @Value)
	WHERE ProductID = @ProductId;

END
```

## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [SQL Server 2016 and JSON Support](https://channel9.msdn.com/Shows/Data-Exposed/SQL-Server-2016-and-JSON-Support)

-   [Using JSON in SQL Server 2016 and Azure SQL Database](https://channel9.msdn.com/Shows/Data-Exposed/Using-JSON-in-SQL-Server-2016-and-Azure-SQL-Database)

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven/SQLServer2016/JSON-as-a-bridge-betwen-NoSQL-and-relational-worlds)
