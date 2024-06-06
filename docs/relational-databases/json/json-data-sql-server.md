---
title: Work with JSON data in SQL Server
description: Combine NoSQL and relational concepts in the same database with JSON data in SQL Server
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest, jovanpop
ms.date: 05/21/2024
ms.service: sql
ms.topic: quickstart
ms.custom:
  - intro-quickstart
  - build-2024
helpviewer_keywords:
  - "JSON"
  - "JSON, built-in support"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# JSON data in SQL Server

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

JSON is a popular textual data format that's used for exchanging data in modern web and mobile applications. JSON is also used for storing unstructured data in log files or NoSQL databases such as Microsoft Azure Cosmos DB. Many REST web services return results that are formatted as JSON text or accept data that's formatted as JSON. For example, most Azure services, such as Azure Search, Azure Storage, and Azure Cosmos DB, have REST endpoints that return or consume JSON. JSON is also the main format for exchanging data between webpages and web servers by using AJAX calls.

JSON functions, first introduced in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], enable you to combine NoSQL and relational concepts in the same database. You can combine classic relational columns with columns that contain documents formatted as JSON text in the same table, parse and import JSON documents in relational structures, or format relational data to JSON text.

> [!NOTE]  
> JSON support requires [database compatibility level](../databases/view-or-change-the-compatibility-level-of-a-database.md) 130 or higher.

Here's an example of JSON text:

```json
[
    {
        "name": "John",
        "skills": [ "SQL", "C#", "Azure" ]
    },
    {
        "name": "Jane",
        "surname": "Doe"
    }
]
```

By using [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] built-in functions and operators, you can do the following things with JSON text:

- Parse JSON text and read or modify values.
- Transform arrays of JSON objects into table format.
- Run any Transact-SQL query on the converted JSON objects.
- Format the results of Transact-SQL queries in JSON format.

:::image type="content" source="media/json-data-sql-server/json-slides-overview.png" alt-text="Diagram showing the overview of built-in JSON support.":::

## Key JSON capabilities of SQL Server and SQL Database

The next sections discuss the key capabilities that [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] provides with its built-in JSON support.

### JSON data type

The new **json** data type that stores JSON documents in a native binary format that provides the following benefits over storing JSON data in **varchar**/**nvarchar**:

- More efficient reads, as the document is already parsed
- More efficient writes, as the query can update individual values without accessing the entire document
- More efficient storage, optimized for compression
- No change in compatibility with existing code

> [!NOTE]
> - Currently, the [JSON data type](../../t-sql/data-types/json-data-type.md) is available in Azure SQL Database.

Using the JSON same functions described in this article remain the most efficient way to query the **json** data type. For more information on the native **json** data type, see [JSON data type](../../t-sql/data-types/json-data-type.md).

### Extract values from JSON text and use them in queries

If you have JSON text that's stored in database tables, you can read or modify values in the JSON text by using the following built-in functions:

- [ISJSON (Transact-SQL)](../../t-sql/functions/isjson-transact-sql.md) tests whether a string contains valid JSON.
- [JSON_VALUE (Transact-SQL)](../../t-sql/functions/json-value-transact-sql.md) extracts a scalar value from a JSON string.
- [JSON_QUERY (Transact-SQL)](../../t-sql/functions/json-query-transact-sql.md) extracts an object or an array from a JSON string.
- [JSON_MODIFY (Transact-SQL)](../../t-sql/functions/json-modify-transact-sql.md) changes a value in a JSON string.

**Example**

In the following example, the query uses both relational and JSON data (stored in a column named `jsonCol`) from a table called `People`:

```sql
SELECT Name,
    Surname,
    JSON_VALUE(jsonCol, '$.info.address.PostCode') AS PostCode,
    JSON_VALUE(jsonCol, '$.info.address."Address Line 1"')
        + ' ' + JSON_VALUE(jsonCol, '$.info.address."Address Line 2"') AS Address,
    JSON_QUERY(jsonCol, '$.info.skills') AS Skills
FROM People
WHERE ISJSON(jsonCol) > 0
    AND JSON_VALUE(jsonCol, '$.info.address.Town') = 'Belgrade'
    AND STATUS = 'Active'
ORDER BY JSON_VALUE(jsonCol, '$.info.address.PostCode');
```

Applications and tools see no difference between the values taken from scalar table columns and the values taken from JSON columns. You can use values from JSON text in any part of a Transact-SQL query (including WHERE, ORDER BY, or GROUP BY clauses, window aggregates, and so on). JSON functions use JavaScript-like syntax for referencing values inside JSON text.

For more information, see [Validate, Query, and Change JSON Data with Built-in Functions (SQL Server)](validate-query-and-change-json-data-with-built-in-functions-sql-server.md), [JSON_VALUE (Transact-SQL)](../../t-sql/functions/json-value-transact-sql.md), and [JSON_QUERY (Transact-SQL)](../../t-sql/functions/json-query-transact-sql.md).

### Change JSON values

If you must modify parts of JSON text, you can use the [JSON_MODIFY (Transact-SQL)](../../t-sql/functions/json-modify-transact-sql.md) function to update the value of a property in a JSON string and return the updated JSON string. The following example updates the value of a property in a variable that contains JSON:

```sql
DECLARE @json NVARCHAR(MAX);

SET @json = '{"info": {"address": [{"town": "Belgrade"}, {"town": "Paris"}, {"town":"Madrid"}]}}';
SET @json = JSON_MODIFY(@json, '$.info.address[1].town', 'London');

SELECT modifiedJson = @json;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```json
{"info":{"address":[{"town":"Belgrade"},{"town":"London"},{"town":"Madrid"}]}}
```

### Convert JSON collections to a rowset

You don't need a custom query language to query JSON in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. To query JSON data, you can use standard T-SQL. If you must create a query or report on JSON data, you can easily convert JSON data to rows and columns by calling the `OPENJSON` rowset function. For more information, see [Parse and Transform JSON Data with OPENJSON](convert-json-data-to-rows-and-columns-with-openjson-sql-server.md).

The following example calls `OPENJSON` and transforms the array of objects that is stored in the `@json` variable to a rowset that can be queried with a standard Transact-SQL `SELECT` statement:

```sql
DECLARE @json NVARCHAR(MAX);

SET @json = N'[
  {"id": 2, "info": {"name": "John", "surname": "Smith"}, "age": 25},
  {"id": 5, "info": {"name": "Jane", "surname": "Smith"}, "dob": "2005-11-04T12:00:00"}
]';

SELECT *
FROM OPENJSON(@json) WITH (
    id INT 'strict $.id',
    firstName NVARCHAR(50) '$.info.name',
    lastName NVARCHAR(50) '$.info.surname',
    age INT,
    dateOfBirth DATETIME2 '$.dob'
);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| ID | firstName | lastName | age | dateOfBirth |
| --- | --- | --- | --- | --- |
| 2 | John | Smith | 25 | |
| 5 | Jane | Smith | | 2005-11-04T12:00:00 |

`OPENJSON` transforms the array of JSON objects into a table in which each object is represented as one row, and key/value pairs are returned as cells. The output observes the following rules:

- `OPENJSON` converts JSON values to the types that are specified in the `WITH` clause.
- `OPENJSON` can handle both flat key/value pairs and nested, hierarchically organized objects.
- You don't have to return all the fields that are contained in the JSON text.
- If JSON values don't exist, `OPENJSON` returns `NULL` values.
- You can optionally specify a path after the type specification to reference a nested property or to reference a property by a different name.
- The optional `strict` prefix in the path specifies that values for the specified properties must exist in the JSON text.

For more information, see [Parse and Transform JSON Data with OPENJSON](convert-json-data-to-rows-and-columns-with-openjson-sql-server.md) and [OPENJSON (Transact-SQL)](../../t-sql/functions/openjson-transact-sql.md).

JSON documents might have sub-elements and hierarchical data that can't be directly mapped into the standard relational columns. In this case, you can flatten JSON hierarchy by joining parent entity with sub-arrays.

In the following example, the second object in the array has sub-array representing person skills. Every sub-object can be parsed using additional `OPENJSON` function call:

```sql
DECLARE @json NVARCHAR(MAX);

SET @json = N'[
  {"id": 2, "info": {"name": "John", "surname": "Smith"}, "age": 25},
  {"id": 5, "info": {"name": "Jane", "surname": "Smith", "skills": ["SQL", "C#", "Azure"]}, "dob": "2005-11-04T12:00:00"}
]';

SELECT id,
    firstName,
    lastName,
    age,
    dateOfBirth,
    skill
FROM OPENJSON(@json) WITH (
    id INT 'strict $.id',
    firstName NVARCHAR(50) '$.info.name',
    lastName NVARCHAR(50) '$.info.surname',
    age INT,
    dateOfBirth DATETIME2 '$.dob',
    skills NVARCHAR(MAX) '$.info.skills' AS JSON
)
OUTER APPLY OPENJSON(skills) WITH (skill NVARCHAR(8) '$');
```

The `skills` array is returned in the first `OPENJSON` as original JSON text fragment and passed to another `OPENJSON` function using `APPLY` operator. The second `OPENJSON` function parses JSON array and return string values as single column rowset that will be joined with the result of the first `OPENJSON`.

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| ID | firstName | lastName | age | dateOfBirth | skill |
| --- | --- | --- | --- | --- | --- |
| 2 | John | Smith | 25 | | |
| 5 | Jane | Smith | | 2005-11-04T12:00:00 | SQL |
| 5 | Jane | Smith | | 2005-11-04T12:00:00 | C# |
| 5 | Jane | Smith | | 2005-11-04T12:00:00 | Azure |

`OUTER APPLY OPENJSON` joins first-level entity with sub-array and return flatten resultset. Due to JOIN, the second row is repeated for every skill.

### Convert SQL Server data to JSON or export JSON

> [!NOTE]  
> Converting Azure Synapse Analytics data to JSON or exporting JSON is not supported.

Format [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] data or the results of SQL queries as JSON by adding the `FOR JSON` clause to a `SELECT` statement. Use `FOR JSON` to delegate the formatting of JSON output from your client applications to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. For more information, see [Format query results as JSON with FOR JSON](format-query-results-as-json-with-for-json-sql-server.md).

The following example uses PATH mode with the `FOR JSON` clause:

```sql
SELECT id,
    firstName AS "info.name",
    lastName AS "info.surname",
    age,
    dateOfBirth AS dob
FROM People
FOR JSON PATH;
```

The `FOR JSON` clause formats SQL results as JSON text that can be provided to any app that understands JSON. The PATH option uses dot-separated aliases in the SELECT clause to nest objects in the query results.

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```json
[
  {
    "id": 2,
    "info": {
      "name": "John",
      "surname": "Smith"
    },
    "age": 25
  },
  {
    "id": 5,
    "info": {
      "name": "Jane",
      "surname": "Smith"
    },
    "dob": "2005-11-04T12:00:00"
  }
]
```

For more information, see [Format query results as JSON with FOR JSON](format-query-results-as-json-with-for-json-sql-server.md) and [FOR Clause (Transact-SQL)](../../t-sql/queries/select-for-clause-transact-sql.md).

### JSON data from aggregates

JSON aggregate functions enable construction of JSON objects or arrays based on an aggregate from SQL data.

- [JSON_OBJECTAGG](../../t-sql/functions/json-objectagg-transact-sql.md) constructs a JSON **object** from an aggregation of SQL data or columns.
- [JSON_ARRAYAGG](../../t-sql/functions/json-arrayagg-transact-sql.md) constructs a JSON **array** from an aggregation of SQL data or columns.

> [!NOTE]
> Currently, both **json** aggregate functions `JSON_OBJECTAGG` and `JSON_ARRAYAGG` are available in preview for Azure SQL Database.

## Use cases for JSON data in SQL Server

JSON support in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and Azure SQL Database lets you combine relational and NoSQL concepts. You can easily transform relational to semi-structured data and vice-versa. JSON isn't a replacement for existing relational models, however. Here are some specific use cases that benefit from the JSON support in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and in SQL Database.

### Simplify complex data models

Consider denormalizing your data model with JSON fields in place of multiple child tables.

### Store retail and e-commerce data

Store info about products with a wide range of variable attributes in a denormalized model for flexibility.

### Process log and telemetry data

Load, query, and analyze log data stored as JSON files with all the power of the Transact-SQL language.

### Store semi-structured IoT data

When you need real-time analysis of IoT data, load the incoming data directly into the database instead of staging it in a storage location.

### Simplify REST API development

Transform relational data from your database easily into the JSON format used by the REST APIs that support your web site.

## Combine relational and JSON data

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] provides a hybrid model for storing and processing both relational and JSON data by using standard Transact-SQL language. You can organize collections of your JSON documents in tables, establish relationships between them, combine strongly typed scalar columns stored in tables with flexible key/value pairs stored in JSON columns, and query both scalar and JSON values in one or more tables by using full Transact-SQL.

JSON text is stored in `VARCHAR` or `NVARCHAR` columns and is indexed as plain text. Any [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] feature or component that supports text supports JSON, so there are almost no constraints on interaction between JSON and other [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] features. You can store JSON in In-memory or Temporal tables, apply Row-Level Security predicates on JSON text, and so on.

Here are some use cases that show how you can use the built-in JSON support in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Store and index JSON data in SQL Server

JSON is a textual format so the JSON documents can be stored in `NVARCHAR` columns in a SQL Database. Since `NVARCHAR` type is supported in all [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] subsystems you can put JSON documents in tables with clustered columnstore indexes, memory optimized tables, or external files that can be read using OPENROWSET or PolyBase.

To learn more about your options for storing, indexing, and optimizing JSON data in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see the following articles:

- [Store JSON documents in SQL Server or SQL Database](store-json-documents-in-sql-tables.md)
- [Index JSON data](index-json-data.md)
- [Optimize JSON processing with in-memory OLTP](optimize-json-processing-with-in-memory-oltp.md)

### Load JSON files into SQL Server

You can format information that's stored in files as standard JSON or line-delimited JSON. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] can import the contents of JSON files, parse it by using the `OPENJSON` or `JSON_VALUE` functions, and load it into tables.

- If your JSON documents are stored in local files, on shared network drives, or in Azure Files locations that can be accessed by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you can use bulk import to load your JSON data into [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

- If your line-delimited JSON files are stored in Azure Blob storage or the Hadoop file system, you can use PolyBase to load JSON text, parse it in Transact-SQL code, and load it into tables.

### Import JSON data into SQL Server tables

If you must load JSON data from an external service into [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you can use `OPENJSON` to import the data into [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instead of parsing the data in the application layer.

In supported platforms, use the native **json** data type instead of **nvarchar(max)** for improved performance and more efficient storage.

```sql
DECLARE @jsonVariable NVARCHAR(MAX);

SET @jsonVariable = N'[
  {
    "Order": {
      "Number":"SO43659",
      "Date":"2011-05-31T00:00:00"
    },
    "AccountNumber":"AW29825",
    "Item": {
      "Price":2024.9940,
      "Quantity":1
    }
  },
  {
    "Order": {
      "Number":"SO43661",
      "Date":"2011-06-01T00:00:00"
    },
    "AccountNumber":"AW73565",
    "Item": {
      "Price":2024.9940,
      "Quantity":3
    }
  }
]';

-- INSERT INTO <sampleTable>
SELECT SalesOrderJsonData.*
FROM OPENJSON(@jsonVariable, N'$') WITH (
    Number VARCHAR(200) N'$.Order.Number',
    Date DATETIME N'$.Order.Date',
    Customer VARCHAR(200) N'$.AccountNumber',
    Quantity INT N'$.Item.Quantity'
) AS SalesOrderJsonData;
```

You can provide the content of the JSON variable by an external REST service, send it as a parameter from a client-side JavaScript framework, or load it from external files. You can easily insert, update, or merge results from JSON text into a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] table.

## Analyze JSON data with SQL queries

If you must filter or aggregate JSON data for reporting purposes, you can use `OPENJSON` to transform JSON to relational format. You can then use standard [!INCLUDE [tsql](../../includes/tsql-md.md)] and built-in functions to prepare the reports.

```sql
SELECT Tab.Id,
    SalesOrderJsonData.Customer,
    SalesOrderJsonData.Date
FROM SalesOrderRecord AS Tab
CROSS APPLY OPENJSON(Tab.json, N'$.Orders.OrdersArray') WITH (
    Number VARCHAR(200) N'$.Order.Number',
    Date DATETIME N'$.Order.Date',
    Customer VARCHAR(200) N'$.AccountNumber',
    Quantity INT N'$.Item.Quantity'
) AS SalesOrderJsonData
WHERE JSON_VALUE(Tab.json, '$.Status') = N'Closed'
ORDER BY JSON_VALUE(Tab.json, '$.Group'),
    Tab.DateModified;
```

You can use both standard table columns and values from JSON text in the same query. You can add indexes on the `JSON_VALUE(Tab.json, '$.Status')` expression to improve the performance of the query. For more information, see [Index JSON data](index-json-data.md).

## Return data from a SQL Server table formatted as JSON

If you have a web service that takes data from the database layer and returns it in JSON format, or if you have JavaScript frameworks or libraries that accept data formatted as JSON, you can format JSON output directly in a SQL query. Instead of writing code or including a library to convert tabular query results and then serialize objects to JSON format, you can use `FOR JSON` to delegate the JSON formatting to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

For example, you might want to generate JSON output that's compliant with the OData specification. The web service expects a request and response in the following format:

- Request: `/Northwind/Northwind.svc/Products(1)?$select=ProductID,ProductName`

- Response: `{"@odata.context": "https://services.odata.org/V4/Northwind/Northwind.svc/$metadata#Products(ProductID,ProductName)/$entity", "ProductID": 1, "ProductName": "Chai"}`

This OData URL represents a request for the ProductID and ProductName columns for the product with `ID` 1. You can use `FOR JSON` to format the output as expected in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

```sql
SELECT 'https://services.odata.org/V4/Northwind/Northwind.svc/$metadata#Products(ProductID,ProductName)/$entity' AS '@odata.context',
  ProductID,
  Name as ProductName
FROM Production.Product
WHERE ProductID = 1
FOR JSON AUTO;
```

The output of this query is JSON text that's fully compliant with the OData spec. Formatting and escaping are handled by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] can also format query results in any format, such as OData JSON or GeoJSON.

## Test drive built-in JSON support with the AdventureWorks sample database

To get the AdventureWorks sample database, download at least the database file and the samples and scripts file from [GitHub](https://github.com/microsoft/sql-server-samples/releases/tag/adventureworks).

After you restore the sample database to an instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], extract the samples file, and then open the `JSON Sample Queries procedures views and indexes.sql` file from the JSON folder. Run the scripts in this file to reformat some existing data as JSON data, test sample queries and reports over the JSON data, index the JSON data, and import and export JSON.

Here's what you can do with the scripts that are included in the file:

- Denormalize the existing schema to create columns of JSON data.

  - Store information from `SalesReasons`, `SalesOrderDetails`, `SalesPerson`, `Customer`, and other tables that contain information related to sales order into JSON columns in the `SalesOrder_json` table.

  - Store information from `EmailAddresses` and `PersonPhone` tables in the `Person_json` table as arrays of JSON objects.

- Create procedures and views that query JSON data.

- Index JSON data. Create indexes on JSON properties and full-text indexes.

- Import and export JSON. Create and run procedures that export the content of the `Person` and the `SalesOrder` tables as JSON results, and import and update the `Person` and the `SalesOrder` tables by using JSON input.

- Run query examples. Run some queries that call the stored procedures and views that you created in steps 2 and 4.

- Clean up scripts. Don't run this part if you want to keep the stored procedures and views that you created in steps 2 and 4.

## Related content

- [SELECT - FOR Clause (Transact-SQL)](../../t-sql/queries/select-for-clause-transact-sql.md)
- [OPENJSON (Transact-SQL)](../../t-sql/functions/openjson-transact-sql.md)
- [JSON Functions (Transact-SQL)](../../t-sql/functions/json-functions-transact-sql.md)
- [ISJSON (Transact-SQL)](../../t-sql/functions/isjson-transact-sql.md)
- [JSON_VALUE (Transact-SQL)](../../t-sql/functions/json-value-transact-sql.md)
- [JSON_OBJECT (Transact-SQL)](../../t-sql/functions/json-object-transact-sql.md)
- [JSON_ARRAY (Transact-SQL)](../../t-sql/functions/json-array-transact-sql.md)
- [JSON_QUERY (Transact-SQL)](../../t-sql/functions/json-query-transact-sql.md)
- [JSON_MODIFY (Transact-SQL)](../../t-sql/functions/json-modify-transact-sql.md)
