---
title: JSON data type (preview)
description: The native JSON data type provides advantages for storing JSON data over varchar or nvarchar. Learn more about the JSON data type.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest, jovanpop
ms.date: 05/21/2024
ms.service: sql
ms.topic: quickstart
ms.custom:
  - intro-quickstart
helpviewer_keywords:
  - "JSON data type"
monikerRange: "=azuresqldb-current"
---
# JSON data type (preview)

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

The new native **JSON** data type that stores JSON documents in a native binary format.

The **JSON** type provides a high-fidelity storage of JSON documents optimized for easy querying and manipulation, and provides the following benefits over storing JSON data in **varchar**/**nvarchar**:

- More efficient reads, as the document is already parsed
- More efficient writes, as the query can update individual values without accessing the entire document
- More efficient storage, optimized for compression
- No change in compatibility with existing code

The **JSON** type internally stores data using UTF-8 encoding, `Latin1_General_100_BIN2_UTF8`. This behavior matches the JSON specification.

For more information on querying JSON data, see [Work with JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md).

## Sample syntax

The usage syntax for the **JSON** type is similar to all other SQL Server data types in a table.

```syntaxsql
column_name JSON [NOT NULL | NULL] [CHECK(constraint_expression)] [DEFAULT(default_expression)] 
```

The **JSON** type can be used in column definition contained in a `CREATE TABLE` statement, for example:

```sql
CREATE TABLE Orders (order_id int, order_details JSON NOT NULL); 
```

Constraints can be specified as part of the column definition, for example:

```sql
CREATE TABLE Orders (order_id int, order_details JSON NOT NULL 
   CHECK (JSON_PATH_EXISTS(order_details, '$.basket') = 1
); 
```

## Feature availability

JSON support was first introduced in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], mostly in the form of JSON functions. The new native **JSON** type was introduced in 2024, first on Azure SQL platforms.

**JSON** is available under all database compatibility levels.

> [!NOTE]
> Currently, the **JSON** type is available in preview on Azure SQL Database.

## Function support

All JSON functions support the **JSON** type with no code changes or usage difference necessary.

For a complete list of JSON functions, see [JSON Functions](../../t-sql/functions/json-functions-transact-sql.md).

## Indexes

There are no special index types for JSON data.

The **JSON** type cannot be used as key column in a `CREATE INDEX` statement. However, a **JSON** column can be specified as an included column in an index definition. Additionally, a  **JSON** column can appear in the `WHERE` clause of a filtered index.

## Conversion

Explicit conversion using `CAST` or `CONVERT` from the **JSON** type can be done to  **char**/**nchar**/**varchar**/**nvarchar** types. All implicit conversions are not allowed, similar to the behavior of **xml**. Similarly, only **char**/**varchar**/**nchar**/**nvarchar** can be explicitly converted to the **JSON** type.

The **JSON** type cannot be used with the **sql_variant** type or assigned to a **sql_variant** variable or column. This restriction similar to **varchar(max)**, **varbinary(max)**, **nvarchar(max)**, **xml**, and CLR-based data types.

For more information, see [Data type conversion](data-type-conversion-database-engine.md).

## Compatibility

The **JSON** type can be used as a parameter or return type in a user-defined function, or the parameter of a stored procedure. The **JSON** type is compatible with triggers and views.

Currently, the [bcp](../../tools/bcp-utility.md) tool's native format contains the **JSON** document as **varchar** or **nvarchar**. You must specify a format file to designate a **JSON** data type column.

Creation of alias type using `CREATE TYPE` for the **JSON** type is not allowed. This is same behavior as **xml** type.

Using `SELECT ... INTO` with the JSON type will create a table with the JSON type.

## Limitations

- The behavior of `CAST ( ... AS JSON)` returns a **JSON** type, but the [sp_describe_first_result_set](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) system stored procedure does not correct return the **JSON** data type. Therefore, many data access clients and driver will see a **varchar** or **nvarchar** data type.
    - Currently, TDS >= 7.4 (with UTF-8) support will see *varchar(max)* with **Latin_General_100_bin2_utf8**.
    - Currently, TDS < 7.4 support will see **nvarchar(max)** with database collation.

- Currently, the `OPENJSON()` function does not accept the **JSON** type, currently that is an implicit conversion. Explicitly convert to **nvarchar(max)** first.

## Related content

- [Store JSON documents in SQL Server or SQL Database](../../relational-databases/json/store-json-documents-in-sql-tables.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
