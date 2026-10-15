---
title: JSON Data Type
description: The built-in JSON data type provides advantages for storing JSON data over varchar or nvarchar. Learn more about the JSON data type.
author: uc-msft
ms.author: umajay
ms.reviewer: jovanpop
ms.date: 10/13/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "JSON data type"
monikerRange: "=sql-server-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---
# JSON data type

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The **json** data type stores JSON documents in a binary format.

The **json** type provides high-fidelity storage of JSON documents optimized for querying and manipulation. It offers the following benefits over storing JSON data as **varchar** or **nvarchar**:

- More efficient reads, as the document is already parsed.
- More efficient writes, as the query can update individual values without accessing the entire document.
- More efficient storage, optimized for compression.
- No change in compatibility with existing code.

The **json** type internally stores strings using UTF-8 encoding (`Latin1_General_100_BIN2_UTF8`). Other data types are stored as native data types. This behavior matches the JSON specification.

For more information on querying JSON data, see [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md).

## Sample syntax

The usage syntax for the **json** type is similar to all other SQL Server data types in a table.

```syntaxsql
column_name JSON [ NOT NULL | NULL ] [CHECK ( constraint_expression ) ] [ DEFAULT ( default_expression ) ]
```

Use the **json** data type in a column definition contained in a `CREATE TABLE` statement. For example:

```sql
CREATE TABLE Orders
(
    order_id INT,
    order_details JSON NOT NULL
);
```

Specify constraints as part of the column definition. For example:

```sql
CREATE TABLE Orders
(
    order_id INT,
    order_details JSON NOT NULL
        CHECK (JSON_PATH_EXISTS(order_details, '$.basket') = 1)
);
```

## Feature availability

Support for JSON functions was introduced in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. The built-in **json** data type is available in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

For more information about JSON support in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], see [Key JSON capabilities in SQL Server 2025](../../relational-databases/json/json-data-sql-server.md#key-json-capabilities).

The **json** data type is available under all database compatibility levels.

[!INCLUDE [json-data-supportability](../../includes/json-data-supportability.md)]

<a id="modify-method"></a>

## The modify method

The **json** data type supports the `modify` method. Use `modify` to change JSON documents stored in a column. The `modify` method includes optimizations that perform in-place modifications to the data when possible. It's the preferred way to modify a JSON document in a **json** column.

For JSON strings, if the new value is the same length or shorter than the existing value, the method can modify the data in place.

For JSON numbers, if the new value is the same type or fits within the range of the existing value, the method can modify the data in place.

```sql
DROP TABLE IF EXISTS JsonTable;

CREATE TABLE JsonTable
(
    id INT PRIMARY KEY,
    d JSON
);

INSERT INTO JsonTable
(
    id,
    d
)
VALUES (1, '{"a":1, "b":"abc", "c":true}');

UPDATE JsonTable
SET d.modify('$.a', 14859)
WHERE id = 1;

UPDATE JsonTable
SET d.modify('$.b', 'def')
WHERE id = 1;
```

## Valid data

Input to the **json** data type must be a JSON object or a JSON array. Scalars, booleans, and `NULL` values aren't supported. The JSON data type conforms to IETF RFC 4627 which allows only a JSON object or array.

Use the [ISJSON](../functions/isjson-transact-sql.md) function to test whether a string contains valid JSON. Without a type constraint, `ISJSON` accepts only a JSON object or array at the top level, the same as the **json** data type. The `VALUE` type constraint accepts any JSON value at the top level, including a scalar, as IETF RFC 8259 allows. The `ARRAY`, `OBJECT`, and `SCALAR` type constraints accept only that type.

For example:

```sql
DECLARE @true AS JSON = 'true'; -- invalid
DECLARE @false AS JSON = 'false'; -- invalid
DECLARE @number AS JSON = '1234.56'; -- invalid
DECLARE @string AS JSON = '"contoso"'; -- invalid
DECLARE @null AS JSON = 'null'; -- invalid

DECLARE @null AS JSON = NULL; -- valid
DECLARE @object AS JSON = '{}'; -- valid
DECLARE @array AS JSON = '[]'; -- valid
```

## Function support

All JSON functions support the **json** data type without requiring code changes.

For a complete list of JSON functions, see [JSON functions](../functions/json-functions-transact-sql.md).

## Indexes

A **json** column can be an included column in an index definition, or appear in the `WHERE` clause of a filtered index. A **json** column can't be used as a key column in a standard index.

For more information, see [CREATE INDEX](../statements/create-index-transact-sql.md).

In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], you can create a [JSON index](../../relational-databases/json/index-json-data.md) to optimize queries that access values within JSON documents.

For more information, see [CREATE JSON INDEX](../statements/create-json-index-transact-sql.md).

## Conversion

Conversion from **json** to **char**, **nchar**, **varchar**, and **nvarchar** must be explicit. Use `CAST` or `CONVERT`.

Conversion from **char**, **nchar**, **varchar**, **nvarchar**, and **vector** to **json** can be implicit or explicit. Conversion from **json** to **vector** can also be implicit or explicit.

Conversion between **json** and all other data types isn't allowed.

A value that you convert to **json** must be a JSON object or a JSON array. A JSON scalar, such as `1`, `"abc"`, `true`, or `null`, causes error 13609. An empty string also causes error 13609. If an object has duplicate keys, **json** keeps the first value and discards the other values. No error occurs.

Conversion from **json** to a string type returns the JSON text without insignificant white space. If the target length is too short for the JSON text, error 13639 occurs. The value isn't truncated.

The **json** data type can't be used with the **sql_variant** type or assigned to a **sql_variant** variable or column. This restriction is similar to **varchar(max)**, **varbinary(max)**, **nvarchar(max)**, **xml**, and CLR-based data types.

You can convert existing columns like **varchar(max)** to **json** using `ALTER TABLE`. Similar to the **xml** data type, you can't convert a **json** column to any of the string or binary types using `ALTER TABLE`.

For more information, see [Data type conversion (Database Engine)](data-type-conversion-database-engine.md).

### Numeric precision

When you convert to **json** from **char**, **nchar**, **varchar**, or **nvarchar**, JSON text containing a JSON number can be rounded based on the number format.

If you need to retain precision, specify the JSON number as a decimal value. Similarly, if you need to preserve the textual representation of the JSON document, store the JSON document as a character string data type.

## Compatibility

You can use the **json** data type as a parameter or return type in a user-defined function, or as the parameter of a stored procedure. The **json** type is compatible with triggers and views.

Currently, the [bcp](../../tools/bcp/bcp-utility.md) tool's native format represents **json** data as **varchar** or **nvarchar**. You must specify a format file to designate a **json** column.

You can't create an alias type for the **json** data type with `CREATE TYPE`. This behavior is the same as the **xml** data type.

Using `SELECT ... INTO` with the **json** data type creates a table with the **json** type.

## Limitations

`CAST` and `CONVERT` return the **json** data type as expected.

### Driver limitations

The [sp_describe_first_result_set](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) system stored procedure returns the **json** data type correctly if the driver supports the `JSONSUPPORT` TDS feature extension. Drivers that don't support this extension see **varchar** or **nvarchar** instead.

SqlClient (>= 6.0) and JDBC (>= 13.2.0) support `JSONSUPPORT`. For example, the latest version of SQL Server Management Studio (SSMS) uses the latest SqlClient driver and correctly reports the **json** type in result set metadata.

- Without `JSONSUPPORT`, TDS >= 7.4 (with UTF-8) reports **varchar(max)** with `Latin1_General_100_BIN2_UTF8`.
- Without `JSONSUPPORT`, TDS < 7.4 reports **nvarchar(max)** with database collation.

### Size limitations

Size limits of JSON data in storage:

| Field | Limitation |
| --- | --- |
| JSON data type size (binary) | Up to 2 GB |
| Number of unique keys | Up to 32,000 |
| Per key string size | 7,998 bytes |
| Per string value size | 536,870,911 bytes |
| Number of properties in one object | Up to 65,535 |
| Number of elements in one array | Up to 65,535 |
| Number of nested levels in JSON document | 128 |

## Related content

- [Store JSON documents](../../relational-databases/json/store-json-documents-in-sql-tables.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
