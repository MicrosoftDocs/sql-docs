---
title: "Parse and transform JSON data with OPENJSON"
description: OPENJSON converts JSON into a set of rows and columns. Use it to run any SQL query on the returned data, or insert it into a SQL Server table.
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: jroth, randolphwest
ms.date: 05/06/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "OPENJSON"
  - "JSON, importing"
  - "importing JSON"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Parse and transform JSON data with OPENJSON

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

The `OPENJSON` rowset function converts JSON text into a set of rows and columns. After you transform a JSON collection into a rowset with `OPENJSON`, you can run any SQL query on the returned data or insert it into a SQL Server table. For more information about working with JSON data in the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)], see [JSON data in SQL Server](json-data-sql-server.md).

The `OPENJSON` function takes a single JSON object or a collection of JSON objects and transforms them into one or more rows. By default, the `OPENJSON` function returns the following data:

- From a JSON object, the function returns all the key/value pairs that it finds at the first level.
- From a JSON array, the function returns all the elements of the array with their indexes.

You can add an optional `WITH` clause to provide a schema that explicitly defines the structure of the output.

## OPENJSON with the default output

When you use the `OPENJSON` function without providing an explicit schema for the results - that is, without a `WITH` clause after `OPENJSON` - the function returns a table with the following three columns:

1. The `name` of the property in the input object (or the index of the element in the input array).
1. The `value` of the property or the array element.
1. The `type` (for example, string, number, boolean, array, or object).

`OPENJSON` returns each property of the JSON object, or each element of the array, as a separate row.

The following example uses `OPENJSON` with the default schema - that is, without the optional `WITH` clause - and returns one row for each property of the JSON object.

```sql
DECLARE @json NVARCHAR(MAX);

SET @json='{ "name": "John", "surname": "Doe", "age": 45, "skills": [ "SQL", "C#", "MVC" ]}';

SELECT *
FROM OPENJSON(@json);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| key | value | type |
| --- | --- | --- |
| `name` | `John` | `1` |
| `surname` | `Doe` | `1` |
| `age` | `45` | `2` |
| `skills` | `[ "SQL" ,"C#" ,"MVC" ]` | `4` |

For more info and examples, see [Use OPENJSON with the Default Schema](use-openjson-with-the-default-schema-sql-server.md).

For syntax and usage, see [OPENJSON](../../t-sql/functions/openjson-transact-sql.md).

## OPENJSON output with an explicit structure

When you specify a schema for the results by using the `WITH` clause of the `OPENJSON` function, the function returns a table with only the columns that you define in the `WITH` clause. In the optional `WITH` clause, you specify a set of output columns, their types, and the paths of the JSON source properties for each output value. `OPENJSON` iterates through the array of JSON objects, reads the value on the specified path for each column, and converts the value to the specified type.

The following example uses `OPENJSON` with a schema for the output that you explicitly specify in the `WITH` clause.

```sql
DECLARE @json NVARCHAR(MAX);

SET @json = N'[
    {
        "Order": {
            "Number": "SO43659",
            "Date": "2024-05-31T00:00:00"
        },
        "AccountNumber": "AW29825",
        "Item": {
            "Price": 2024.9940,
            "Quantity": 1
        }
    },
    {
        "Order": {
            "Number": "SO43661",
            "Date": "2024-06-01T00:00:00"
        },
        "AccountNumber": "AW73565",
        "Item": {
            "Price": 2024.9940,
            "Quantity": 3
        }
    }
]';

SELECT *
FROM OPENJSON(@json) WITH (
    Number VARCHAR(200) '$.Order.Number',
    DATE DATETIME '$.Order.Date',
    Customer VARCHAR(200) '$.AccountNumber',
    Quantity INT '$.Item.Quantity'
);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

| Number | Date | Customer | Quantity |
| --- | --- | --- | --- |
| `SO43659` | `2024-05-31T00:00:00` | `AW29825` | `1` |
| `SO43661` | `2024-06-01T00:00:00` | `AW73565` | `3` |

This function returns and formats the elements of a JSON array.

- For each element in the JSON array, `OPENJSON` generates a new row in the output table. The two elements in the JSON array are converted into two rows in the returned table.

- For each column, specified by using the `colName type json_path` syntax, `OPENJSON` converts the value found in each array element on the specified path to the specified type. In this example, values for the `Date` column are taken from each element on the path `$.Order.Date` and converted to datetime values.

For more info and examples, see [Use OPENJSON with an Explicit Schema (SQL Server)](use-openjson-with-an-explicit-schema-sql-server.md).

For syntax and usage, see [OPENJSON](../../t-sql/functions/openjson-transact-sql.md).

## OPENJSON requires compatibility level 130

The `OPENJSON` function is available only under compatibility level `130` and greater. If your [database compatibility level](../../database-engine/install-windows/compatibility-certification.md) is lower than `130`, SQL Server can't find and run the `OPENJSON` function. Other built-in JSON functions are available at all compatibility levels.

You can check compatibility level in the `sys.databases` view or in database properties, and change the compatibility level of a database by using the following command:

```sql
ALTER DATABASE <DatabaseName> SET COMPATIBILITY_LEVEL = 130;
```

## Related content

- [JSON as a bridge between NoSQL and relational worlds](/shows/datadriven-sqlserver2016/json-as-bridge-betwen-nosql-relational-worlds)
- [OPENJSON (Transact-SQL)](../../t-sql/functions/openjson-transact-sql.md)
