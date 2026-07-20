---
title: "JSON_VALUE (Transact-SQL)"
description: JSON_VALUE extracts a scalar value from a JSON string.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, umajay, randolphwest
ms.date: 02/05/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "JSON_VALUE"
  - "JSON_VALUE_TSQL"
helpviewer_keywords:
  - "JSON_VALUE function"
  - "JSON, extracting"
  - "JSON, querying"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# JSON_VALUE (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

Use the `JSON_VALUE` syntax to extract a scalar value from a JSON string.

To extract an object or an array from a JSON string instead of a scalar value, see [JSON_QUERY](json-query-transact-sql.md). For info about the differences between `JSON_VALUE` and `JSON_QUERY`, see [Compare JSON_VALUE and JSON_QUERY](../../relational-databases/json/validate-query-and-change-json-data-with-built-in-functions-sql-server.md#JSONCompare).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

::: moniker range="<=sql-server-linux-ver16 || <=sql-server-ver16"

Syntax for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions.

```syntaxsql
JSON_VALUE ( expression , path )
```

::: moniker-end

::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

Syntax for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

```syntaxsql
JSON_VALUE ( expression , path [ RETURNING data_type ] )
```

> [!NOTE]  
> In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, [RETURNING isn't included](?view=sql-server-ver16&preserve-view=true#syntax).

::: moniker-end

## Arguments

#### *expression*

An expression that's typically the name of a variable or a column containing JSON text.

If `JSON_VALUE` finds JSON that isn't valid in *expression* before it finds the value identified by *path*, the function returns an error. If `JSON_VALUE` doesn't find the value identified by *path*, it scans the entire text and returns an error if it finds JSON that isn't valid anywhere in *expression*.

#### *path*

`A JSON` path that specifies the property to extract. For more info, see [JSON path expressions in the SQL Database Engine](../../relational-databases/json/json-path-expressions-sql-server.md).

In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], you can provide a variable as the value of *path*.

If the format of *path* isn't valid, `JSON_VALUE` returns an error.

#### *data_type*

The data type that you want to use for the return value. This type is only supported if the input is a JSON type. The supported data types are: **tinyint**, **smallint**, **int**, **bigint**, **decimal**, **numeric**, **float**, **real**, **char**, **varchar**, **varchar(max)**, **nchar**, **nvarchar**, **nvarchar(max)**, **date**, **time**, **datetime2**, and **datetimeoffset**.

## Return value

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

If you don't include `RETURNING`:

- Returns a single text value of type **nvarchar(4000)**. The collation of the returned value matches the collation of the input expression.

- If the value is greater than 4,000 characters:

  - In lax mode, `JSON_VALUE` returns `NULL`.
  - In strict mode, `JSON_VALUE` returns an error.

  If you need to return scalar values greater than 4,000 characters, use `OPENJSON` instead of `JSON_VALUE`. For more information, see [OPENJSON](openjson-transact-sql.md).

If you include `RETURNING`:

Returns the value specified in *data_type*. The supported data types are: **tinyint**, **smallint**, **int**, **bigint**, **decimal**, **numeric**, **float**, **real**, **char**, **varchar**, **varchar(max)**, **nchar**, **nvarchar**, **nvarchar(max)**, **date**, **time**, **datetime2**, and **datetimeoffset**.

JSON functions work the same way whether the JSON document is stored in **varchar**, **nvarchar**, or the native **json** data type.

## Remarks

### Lax mode and strict mode

Consider the following JSON text:

```sql
DECLARE @jsonInfo AS NVARCHAR (MAX);

SET @jsonInfo = N'{
     "info":{
       "type":1,
       "address":{
         "town":"Bristol",
         "county":"Avon",
         "country/region":"England"
       },
       "tags":["Sport", "Water polo"]
    },
    "type":"Basic"
 }';
```

The following table compares the behavior of `JSON_VALUE` in lax mode and in strict mode. For more info about the optional path mode specification (lax or strict), see [JSON path expressions in the SQL Database Engine](../../relational-databases/json/json-path-expressions-sql-server.md).

| Path | Return value in lax mode | Return value in strict mode | More info |
| --- | --- | --- | --- |
| $ | `NULL` | Error | Not a scalar value.<br /><br />Use `JSON_QUERY` instead. |
| $.info.type | N'1' | N'1' | N/a |
| $.info.address.town | N'Bristol' | N'Bristol' | N/a |
| $.info."address" | `NULL` | Error | Not a scalar value.<br /><br />Use `JSON_QUERY` instead. |
| $.info.tags | `NULL` | Error | Not a scalar value.<br /><br />Use `JSON_QUERY` instead. |
| $.info.type[0] | `NULL` | Error | Not an array. |
| $.info.none | `NULL` | Error | Property doesn't exist. |

## Examples

### Example 1

The following example uses the values of the JSON properties `town` and `state` in query results. Since `JSON_VALUE` preserves the collation of the source, the sort order of the results depends on the collation of the `jsonInfo` column.

> [!NOTE]  
> This example assumes that a table named `Person.Person` contains a `jsonInfo` column of JSON text, and that this column has the structure shown previously in the discussion of lax mode and strict mode. In the `AdventureWorks` sample database, the `Person` table doesn't contain a `jsonInfo` column.

```sql
SELECT FirstName,
       LastName,
       JSON_VALUE(jsonInfo, '$.info.address.town') AS Town
FROM Person.Person
WHERE JSON_VALUE(jsonInfo, '$.info.address.state') LIKE 'US%'
ORDER BY JSON_VALUE(jsonInfo, '$.info.address.town');
```

### Example 2

The following example extracts the value of the JSON property `town` into a local variable.

```sql
DECLARE @jsonInfo AS NVARCHAR (MAX);
DECLARE @town AS NVARCHAR (32);

SET @jsonInfo = N'{"info":{"address":[{"town":"Paris"},{"town":"London"}]}}';
SET @town = JSON_VALUE(@jsonInfo, '$.info.address[0].town'); -- Paris
SET @town = JSON_VALUE(@jsonInfo, '$.info.address[1].town'); -- London
```

### Example 3

The following example creates computed columns based on the values of JSON properties.

```sql
CREATE TABLE dbo.Store
(
    StoreID INT IDENTITY (1, 1) NOT NULL,
    Address VARCHAR (500),
    jsonContent NVARCHAR (4000),
    Longitude AS JSON_VALUE(jsonContent, '$.address[0].longitude'),
    Latitude AS JSON_VALUE(jsonContent, '$.address[0].latitude')
);
```

### Example 4

The following example extracts a value from JSON array using `JSON_VALUE` and returns the value as a date type value.

```sql
DECLARE @j AS JSON = '[1, 1.3333, true, "a", "1", "2025-01-01"]';

SELECT JSON_VALUE(@j, '$[5]' RETURNING date) AS date_value;
```

```output
date_value
--------
2025-01-01
```

## Related content

- [JSON path expressions in the SQL Database Engine](../../relational-databases/json/json-path-expressions-sql-server.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
