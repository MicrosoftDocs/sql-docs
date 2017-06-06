---
title: "Optimize JSON processing with in-memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "02/03/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-json"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d9c5adb1-3209-4186-bc10-8e41a26f5e57
caps.latest.revision: 3
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Optimize JSON processing with in-memory OLTP
[!INCLUDE[tsql-appliesto-ssvNxt-asdb-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-asdb-xxxx-xxx.md)]

SQL Server and Azure SQL Database let you work with text formatted as JSON. In order to increase performance of your OLTP queries that process JSON data, you can store JSON documents in memory-optimized tables using standard string columns (NVARCHAR type).

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
Storing JSON data in memory-optimized tables increases query performance by leveraging lock-free, in-memory data access.

## Optimize JSON with additional in-memory features
New features that are available in SQL Server and Azure SQL Database let you fully integrate JSON functionalities with existing in-memory OLTP technologies. For example, you can do the following things:
 - Validate the structure of JSON documents stored in memory-optimized tables using natively compiled CHECK constraints.
 - Expose and strongly type values stored in JSON documents using computed columns.
 - Index values in JSON documents using memory-optimized indexes.
 - Natively compile SQL queries that use values from JSON documents or format results as JSON text.

## Validate JSON columns
SQL Server and Azure SQL Database let you add natively compiled CHECK constraints that validate the content of JSON documents stored in a string column, as shown in the following example.

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

The natively compiled CHECK constraint can be added on existing tables that contain JSON columns:

```sql
ALTER TABLE xtp.Product
    ADD CONSTRAINT [Data should be JSON]
        CHECK (ISJSON(Data)=1)
```

With natively compiled JSON CHECK constraints, you can ensure that JSON text stored in your memory-optimized tables is properly formatted.

## Expose JSON values using computed columns
Computed columns let you expose values from JSON text and access those values without re-evaluating the expressions that fetch a value from the JSON text and without re-parsing the JSON structure. Exposed values are strongly typed and physically persisted in the computed columns. Accessing JSON values using persisted computed columns is faster than accessing values in the JSON document.

The following example shows how to expose the following two values from the JSON `Data` column:
-   The country where a product is made.
-   The product manufacturing cost.

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

The computed columns `MadeIn` and `Cost` are updated every time the JSON document stored in the `Data` column changes.

## Index values in JSON columns
SQL Server and Azure SQL Database let you index values in JSON columns using memory optimized indexes. JSON values that are indexed must be exposed and strongly typed using computed columns, as shown in the following example.

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
Values in JSON columns can be indexed using both standard NONCLUSTERED and HASH indexes.
-   NONCLUSTERED indexes optimize queries that select ranges of rows by some JSON value or sort results by JSON values.
-   HASH indexes give you optimal performance when a single row or a few rows are fetched by specifying the exact value to find.

## Native compilation of JSON queries
Finally, native compilation of Transact-SQL procedures, functions, and triggers that contain queries with JSON functions increases performance of queries and reduces CPU cycles required to execute the procedures. The following example shows a natively compiled procedure that uses several JSON functions - JSON_VALUE, OPENJSON, and JSON_MODIFY.

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

## Next steps
JSON in in-memory OLTP native modules provide a performance improvement for the built-in JSON functionality that's available in SQL Server and Azure SQL Database.

To learn more about core scenarios for using JSON, check out some of these resources:

-   [TechNet Blog](https://blogs.technet.microsoft.com/dataplatforminsider/2016/01/05/json-in-sql-server-2016-part-1-of-4/)
-   [MSDN documentation](https://msdn.microsoft.com/library/dn921897.aspx)
-   [Channel 9 video](https://channel9.msdn.com/Shows/Data-Exposed/SQL-Server-2016-and-JSON-Support)

To learn about various scenarios for integrating JSON into your application, check out the following resources:
-   See the demos in this [Channel 9 video](https://channel9.msdn.com/Events/DataDriven/SQLServer2016/JSON-as-a-bridge-betwen-NoSQL-and-relational-worlds).
-   Find a scenario that matches your use case in [JSON Blog posts](http://blogs.msdn.com/b/sqlserverstorageengine/archive/tags/json/).
-   Find examples in our [GitHub repository](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/json/).

