<img width="100" height="19" alt="image" src="https://github.com/user-attachments/assets/97427a9d-b373-460a-a4d7-1bd32ca6fb42" />---
title: "JSON Data Type"
description: The native JSON data type provides advantages for storing JSON data over varchar or nvarchar. Learn more about the JSON data type.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest, jovanpop, umajay
ms.date: 09/16/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - build-2024
  - build-2025
helpviewer_keywords:
  - "JSON data type"
monikerRange: "=sql-server-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric"
---
# JSON data type

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The **json** data type stores JSON documents in a native binary format.

The **json** type provides a high-fidelity storage of JSON documents optimized for easy querying and manipulation, and provides the following benefits over storing JSON data as **varchar** or **nvarchar**:

- More efficient reads, as the document is already parsed
- More efficient writes, as the query can update individual values without accessing the entire document
- More efficient storage, optimized for compression
- No change in compatibility with existing code

The **json** type internally stores data using UTF-8 encoding, `Latin1_General_100_BIN2_UTF8`. This behavior matches the JSON specification.

For more information on querying JSON data, see [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md).

## Sample syntax

The usage syntax for the **json** type is similar to all other SQL Server data types in a table.

```syntaxsql
column_name JSON [ NOT NULL | NULL ] [CHECK ( constraint_expression ) ] [ DEFAULT ( default_expression ) ]
```

The **json** data type can be used in column definition contained in a `CREATE TABLE` statement. For example:

```sql
CREATE TABLE Orders
(
    order_id INT,
    order_details JSON NOT NULL
);
```

Constraints can be specified as part of the column definition. For example:

```sql
CREATE TABLE Orders
(
    order_id INT,
    order_details JSON NOT NULL
        CHECK (JSON_PATH_EXISTS(order_details, '$.basket') = 1)
);
```

## Feature availability

JSON function support was first introduced in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. The native **json** type was introduced in Azure SQL Database and Azure SQL Managed Instance, and is also available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

The **json** data type is available under all database compatibility levels.

[!INCLUDE [json-data-supportability](../../includes/json-data-supportability.md)]

<a id="modify-method"></a>

## The modify method

> [!NOTE]  
> The `modify` method is currently in preview and only available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

The **json** data type supports the `modify` method. Use `modify` to modify JSON documents stored in a column. The `modify` method has optimizations to perform in-place modifications to the data where possible, and is the preferred way to modify a JSON document in a **json** column.

For JSON strings, if the new value is less than or equal to the existing value, then in-place modification is possible.

For JSON numbers, if the new value is of the same type, or within the range of the existing value, then in-place modification is possible.

```sql
DROP TABLE IF EXISTS JsonTable;

CREATE TABLE JsonTable
(
    id INT PRIMARY KEY,
    d JSON
);

INSERT INTO JsonTable (id, d)
VALUES (1, '{"a":1, "b":"abc", "c":true}');

UPDATE JsonTable
SET d.modify('$.a', 14859)
WHERE id = 1;

UPDATE JsonTable
SET d.modify('$.b', 'def')
WHERE id = 1;
```

## Function support

All JSON functions support the **json** data type with no code changes or usage difference necessary.

- `OPENJSON` currently doesn't support the **json** data type on some platforms. For more information, see [Limitations](#limitations).

For a complete list of JSON functions, see [JSON functions](../functions/json-functions-transact-sql.md).

## Indexes

There are no special index types for JSON data.

The **json** type can't be used as key column in a `CREATE INDEX` statement. However, a **json** column can be specified as an included column in an index definition. Additionally, a **json** column can appear in the `WHERE` clause of a filtered index.

## Conversion

Explicit conversion using `CAST` or `CONVERT` from the **json** type can be done to **char**, **nchar**, **varchar**, and **nvarchar** types. All implicit conversions aren't allowed, similar to the behavior of **xml**. Similarly, only **char**, **nchar**, **varchar**, and **nvarchar** can be explicitly converted to the **json** data type.

The **json** data type can't be used with the **sql_variant** type or assigned to a **sql_variant** variable or column. This restriction similar to **varchar(max)**, **varbinary(max)**, **nvarchar(max)**, **xml**, and CLR-based data types.

You can convert existing columns like **varchar(max)** to **json** using `ALTER TABLE`. Similar to the **xml** data type, you can't convert a **json** column to any of the string or binary types using `ALTER TABLE`.

For more information, see [Data type conversion (Database Engine)](data-type-conversion-database-engine.md).

## Compatibility

The **json** data type can be used as a parameter or return type in a user-defined function, or the parameter of a stored procedure. The **json** type is compatible with triggers and views.

Currently, the [bcp](../../tools/bcp-utility.md) tool's native format contains the **json** document as **varchar** or **nvarchar**. You must specify a format file to designate a **json** column.

Creation of alias type using `CREATE TYPE` for the **json** data type isn't allowed. This behavior is the same as the **xml** data type.

Using `SELECT ... INTO` with the **json** data type creates a table with the **json** type.

## Limitations

The behavior of `CAST ( ... AS JSON)` returns a **json** data type, but the [sp_describe_first_result_set](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) system stored procedure doesn't correctly return the **json** data type. Therefore, many data access clients and driver see a **varchar** or **nvarchar** data type.

- Currently, TDS >= 7.4 (with UTF-8) sees **varchar(max)** with `Latin_General_100_bin2_utf8`.
- Currently, TDS < 7.4 sees **nvarchar(max)** with database collation.

Currently, the `OPENJSON()` function doesn't accept the **json** data type in some platforms. Currently, it's an implicit conversion. Explicitly convert to **nvarchar(max)** first.

- In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], the `OPENJSON()` function does support **json**. For more information, see [Key JSON capabilities in SQL Server 2025](../../relational-databases/json/json-data-sql-server.md#key-json-capabilities).

### Size limit for Items in one object/array
The **JSON type** has a size limit of **65,535 items** per object or array — this corresponds to the maximum value of a 16-bit unsigned integer.
If you attempt to cast text into the JSON type and it contains more than 65,535 elements, the following error will be thrown:
`Msg 13647, Level 16, State 1, Line 10
Number of items in one object/array exceeds limit 65535 in JSON type.`

The `TRY_CAST` function also throws the same error when this limit is exceeded.


## Related content

- [Store JSON documents](../../relational-databases/json/store-json-documents-in-sql-tables.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
