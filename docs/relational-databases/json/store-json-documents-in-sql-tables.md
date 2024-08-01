---
title: "Store JSON documents"
description: "Learn why and how to store and index JSON documents, and how to optimize queries over the JSON documents."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: umajay 
ms.date: 08/01/2024
ms.service: sql
ms.custom:
  - build-2024
ms.topic: conceptual
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Store JSON documents
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

The SQL Database Engine provides native JSON functions that enable you to parse JSON documents using standard SQL language. You can store JSON documents in SQL Server or SQL Database and query JSON data as in a NoSQL database. This article describes the options for storing JSON documents.

## JSON storage format

The first storage design decision is how to store JSON documents in the tables. There are two available options:
- **LOB storage** - JSON documents can be stored as-is in columns with the data type **json** or **nvarchar**. This is the best way for quick data load and ingestion because the loading speed matches the loading speed of string columns. This approach might introduce an additional performance penalty on query/analysis time if indexing on JSON values is not performed, because the raw JSON documents must be parsed while the queries are running. 
- **Relational storage** - JSON documents can be parsed while they are inserted in the table using `OPENJSON`, `JSON_VALUE` or `JSON_QUERY` functions. Fragments from the input JSON documents can be stored in the columns containing JSON sub-elements with data types **json** or **nvarchar**. This approach increases the load time because JSON parsing is done during load; however, queries match the performance of classic queries on the relational data.

- Currently, the [JSON data type](../../t-sql/data-types/json-data-type.md) is available in Azure SQL Database.
- Currently in SQL Server, JSON is not a built-in data type.

## Classic tables

The simplest way to store JSON documents in SQL Server or Azure SQL Database is to create a two-column table that contains the ID of the document and the content of the document. For example:

```sql
create table WebSite.Logs (
    [_id] bigint primary key identity,
    [log] nvarchar(max)
);
```

Or, where supported:

```sql
create table WebSite.Logs (
    [_id] bigint primary key identity,
    [log] json
);
```

This structure is equivalent to the collections that you can find in classic document databases. The primary key `_id` is an auto-incrementing value that provides a unique identifier for every document and enables fast lookups. This structure is a good choice for the classic NoSQL scenarios where you want to retrieve a document by ID or update a stored document by ID.

- Use the native **json** data type where available to store JSON documents.
- The **nvarchar(max)** data type lets you store JSON documents that are up to 2 GB in size. If you're sure that your JSON documents aren't greater than 8 KB, however, we recommend that you use **nvarchar(4000)** instead of **nvarchar(max)** for performance reasons.

The sample table created in the preceding example assumes that valid JSON documents are stored in the `log` column. If you want to be sure that valid JSON is saved in the `log` column, you can add a CHECK constraint on the column. For example:

```sql
ALTER TABLE WebSite.Logs
    ADD CONSTRAINT [Log record should be formatted as JSON]
                   CHECK (ISJSON([log])=1)
```

Every time someone inserts or updates a document in the table, this constraint verifies that the JSON document is properly formatted. Without the constraint, the table is optimized for inserts, because any JSON document is added directly to the column without any processing.

When you store your JSON documents in the table, you can use standard Transact-SQL language to query the documents. For example:

```sql
SELECT TOP 100 JSON_VALUE([log], '$.severity'), AVG( CAST( JSON_VALUE([log],'$.duration') as float))
 FROM WebSite.Logs
 WHERE CAST( JSON_VALUE([log],'$.date') as datetime) > @datetime
 GROUP BY JSON_VALUE([log], '$.severity')
 HAVING AVG( CAST( JSON_VALUE([log],'$.duration') as float) ) > 100
 ORDER BY AVG( CAST( JSON_VALUE([log],'$.duration') as float) ) DESC
```

It's a powerful advantage that you can use *any* T-SQL function and query clause to query JSON documents. SQL Server and SQL Database don't introduce any constraints in the queries that you can use to analyze JSON documents. You can extract values from a JSON document with the `JSON_VALUE` function and use it in the query like any other value.

This ability to use rich T-SQL query syntax is the key difference between SQL Server and SQL Database and classic NoSQL databases - in Transact-SQL you probably have any function that you need to process JSON data.

## Indexes

If you find out that your queries frequently search documents by some property (for example, a `severity` property in a JSON document), you can add a rowstore nonclustered index on the property to speed up the queries.

You can create a computed column that exposes JSON values from the JSON columns on the specified path (that is, on the path `$.severity`) and create a standard index on this computed column. For example:

```sql
create table WebSite.Logs (
    [_id] bigint primary key identity,
    [log] nvarchar(max),
    [severity] AS JSON_VALUE([log], '$.severity'),
    index ix_severity (severity)
);
```

The computed column used in this example is a non-persisted or virtual column that doesn't add additional space to the table. It is used by the index `ix_severity` to improve performance of the queries like the following example:

```sql
SELECT [log]
FROM Website.Logs
WHERE JSON_VALUE([log], '$.severity') = 'P4'
```

One important characteristic of this index is that it is collation-aware. If your original **nvarchar** column has a `COLLATION` property (for example, case-sensitivity or Japanese language), the index is organized according to the language rules or the case sensitivity rules associated with the **nvarchar** column. This collation awareness might be an important feature if you are developing applications for global markets that need to use custom language rules when processing JSON documents.

## Large tables & columnstore format

If you expect to have a large number of JSON documents in your collection, we recommend adding a clustered columnstore index on the collection, as shown in the following example:

```sql
create sequence WebSite.LogID as bigint;
go
create table WebSite.Logs (
    [_id] bigint default(next value for WebSite.LogID),
    [log] nvarchar(max),
    INDEX cci CLUSTERED COLUMNSTORE
);
```

A clustered columnstore index provides high data compression (up to 25x) that can significantly reduce your storage space requirements, lower the cost of storage, and increase the I/O performance of your workload. Also, clustered columnstore indexes are optimized for table scans and analytics on your JSON documents, so this type of index might be the best option for log analytics.

The preceding example uses a sequence object to assign values to the `_id` column. Both sequences and identities are valid options for the ID column.

## Frequently changing documents & memory-optimized tables

If you expect a large number of update, insert, and delete operations in your collections, you can store your JSON documents in memory-optimized tables. Memory-optimized JSON collections always keep data in-memory, so there is no storage I/O overhead. Additionally, memory optimized JSON collections are completely lock-free - that is, actions on documents do not block any other operation.

The only thing that you have to do convert a classic collection to a memory-optimized collection is to specify the `WITH (MEMORY_OPTIMIZED=ON)` option after the table definition, as shown in the following example. Then you have a memory-optimized version of the JSON collection.

```sql
CREATE TABLE WebSite.Logs (
  [_id] bigint IDENTITY PRIMARY KEY NONCLUSTERED,
  [log] nvarchar(max)
) WITH (MEMORY_OPTIMIZED=ON)
```

A memory-optimized table is the best option for frequently changing documents. When you are considering memory-optimized tables, also consider performance. Use the **nvarchar(4000)** data type instead of **nvarchar(max)** for JSON documents in your memory-optimized collections, if possible, because it might drastically improve performance. The **json** data type is not supported with memory-optimized tables.

As with classic tables, you can add indexes on the fields that you are exposing in memory-optimized tables by using computed columns. For example:

```sql
CREATE TABLE WebSite.Logs (

  [_id] bigint IDENTITY PRIMARY KEY NONCLUSTERED,
  [log] nvarchar(max),

  [severity] AS cast(JSON_VALUE([log], '$.severity') as tinyint) persisted,
  INDEX ix_severity (severity)

) WITH (MEMORY_OPTIMIZED=ON)
```

To maximize performance, cast the JSON value to the smallest possible type that can be used to hold the value of the property. In the preceding example, **tinyint** is used.

You can also put SQL queries that update JSON documents in stored procedures to get the benefit of native compilation. For example:

```sql
CREATE PROCEDURE WebSite.UpdateData(@Id int, @Property nvarchar(100), @Value nvarchar(100))
WITH SCHEMABINDING, NATIVE_COMPILATION

AS BEGIN
    ATOMIC WITH (transaction isolation level = snapshot,  language = N'English')

    UPDATE WebSite.Logs
    SET [log] = JSON_MODIFY([log], @Property, @Value)
    WHERE _id = @Id;

END
```

This natively compiled procedure takes the query and creates .DLL code that runs the query. A natively compiled procedure is the faster approach for querying and updating data.

## Conclusion

Native JSON functions in SQL Server and SQL Database enable you to process JSON documents just like in NoSQL databases. Every database - relational or NoSQL - has some pros and cons for JSON data processing. The key benefit of storing JSON documents in SQL Server or SQL Database is full SQL language support. You can use the rich Transact-SQL language to process data and to configure a variety of storage options, from columnstore indexes for high compression and fast analytics, to memory-optimized tables for lock-free processing. At the same time, you get the benefit of mature security and internationalization features which you can easily reuse in your NoSQL scenario. The reasons described in this article are excellent reasons to consider storing JSON documents in SQL Server or SQL Database.

## Learn more about JSON in SQL Server and Azure SQL Database
  
For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

- [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven-SQLServer2016/JSON-as-bridge-betwen-NoSQL-relational-worlds)

## Related content

- [JSON data in SQL Server](json-data-sql-server.md)
- [JSON data type](../../t-sql/data-types/json-data-type.md)
