---
title: JSON Path Expressions
description: Learn how to use JSON Path expressions to reference the properties of JSON objects.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, umajay, randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "JSON, path expressions"
  - "path expressions (JSON)"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# JSON path expressions in the SQL Database Engine

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-serverless-pool-only-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-serverless-pool-only-fabricsqldb.md)]

Use JSON path expressions to reference the properties of JSON objects.

You have to provide a path expression when you call the following functions.

- When you call [OPENJSON](../../t-sql/functions/openjson-transact-sql.md) to create a relational view of JSON data.
- When you call [JSON_VALUE](../../t-sql/functions/json-value-transact-sql.md) to extract a value from JSON text.
- When you call [JSON_QUERY](../../t-sql/functions/json-query-transact-sql.md) to extract a JSON object or an array.
- When you call [JSON_MODIFY](../../t-sql/functions/json-modify-transact-sql.md) to update the value of a property in a JSON string.

## Parts of a path expression

A path expression has two components.

1. The optional [path mode](#path-mode), with a value of `lax` or `strict`.

1. The [path](#path) itself.

<a id="PATHMODE"></a>

## Path mode

At the beginning of the path expression, optionally declare the path mode by specifying the keyword `lax` or `strict`. The default is `lax`.

- In `lax` mode, the function returns empty values if the path expression contains an error. For example, if you request the value `$.name`, and the JSON text doesn't contain a `name` key, the function returns null, but doesn't raise an error.

- In `strict` mode, the function raises an error if the path expression contains an error.

The following query explicitly specifies `lax` mode in the path expression.

```sql
DECLARE @json AS NVARCHAR (MAX);

SET @json = N'{ ... }';

SELECT *
FROM OPENJSON (@json, N'lax $.info');
```

<a id="PATH"></a>

## Path

After the optional path mode declaration, specify the path itself.

- The dollar sign (`$`) represents the context item.

- The property path is a set of path steps. Path steps can contain the following elements and operators.

  - Key names. For example, `$.name` and `$."first name"`. If the key name starts with a dollar sign or contains special characters such as spaces or dot operators(`.`), surround it with quotes.

  - Array elements. For example, `$.product[3]`. Arrays are zero-based.

  - The dot operator (`.`) indicates a member of an object. For example, in `$.people[1].surname`, `surname` is a child of `people`.

  - Array wildcard and range searches are also supported if the input is a JSON type value.

<a id="ARRAY_PATH"></a>

## Array wildcard and range support

> [!NOTE]  
> Array wildcard and range support is currently in preview and only available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] expands ANSI SQL/JSON path expression to support an array wildcard. The Array wildcard allows you to specify all elements, range of elements, list of elements or the special token "last" to indicate the last value in a JSON array. SQL/JSON arrays use zero-based index. SQL/JSON path with wildcards can be used in [JSON_QUERY](../../t-sql/functions/json-query-transact-sql.md), [JSON_PATH_EXISTS](../../t-sql/functions/json-path-exists-transact-sql.md), and [JSON_CONTAINS](../../t-sql/functions/json-contains-transact-sql.md).

While `JSON_VALUE` function supports the SQL/JSON path expression, the return value of a `JSON_VALUE` function is a SQL scalar and hence the function always returns `NULL` for any SQL/JSON path that points to a JSON object or array. Array wildcards are supported only if the input is a **json** type.

The following syntax shows how the wildcard, range, and special token `last` can be used:

```syntaxsql
path[elements ]

elements ::= {
*
| number
| number to number
| last
| {number...[, number] }
}
```

The special token `last` can be used in lieu of number value. If a range is specified, then the range needs to be specified in increasing order.

Examples of some valid SQL/JSON path expressions:

| Path | Description |
| --- | --- |
| `$[*]` | All elements |
| `$[0]` | First element |
| `$[0 to 2]` | First three elements |
| `$[last]` | Last element |
| `$[last, 0]` | *Invalid* |
| `$[last, 2, 0, last]` | *Invalid* |
| `$.creditcards[0].type` | Returns the type property value of first element in `creditcards` array |
| `$.credit_cards[*].type` | Returns the type property value of all elements in `creditcards` array |
| `$.credit_cards[0, 2].type` | Returns the type property value of first and third element in `creditcards` array |
| `$.credit_cards[1 to 3].type` | Returns the type property value of second to fourth element in `creditcards` array |
| `$.credit_cards[last].type` | Returns the type property value of last element in `creditcards` array |
| `$.credit_cards[last, 0].type` | Returns the type property value of last and first element in `creditcards` array |

## Examples

The examples in this section reference the following JSON text.

```json
{
    "people": [{
        "name": "John",
        "surname": "Doe"
    }, {
        "name": "Jane",
        "surname": null,
        "active": true
    }]
}
```

The following table shows some examples of path expressions.

| Path expression | Value |
| --- | --- |
| `$.people[0].name` | `John` |
| `$.people[1]` | `{ "name": "Jane", "surname": null, "active": true }` |
| `$.people[1].surname` | `NULL` |
| `$` | `{ "people": [ { "name": "John", "surname": "Doe" },{ "name": "Jane", "surname": null, "active": true } ] }` |
| `$.people[last].name` | `["Jane"]` |
| `$.people[0 to 1].name` | `["John","Jane"]` |
| `$.people[0, 1].name` | `["John","Jane"]` |

## How built-in functions handle duplicate paths

If the JSON text contains duplicate properties - for example, two keys with the same name on the same level - the `JSON_VALUE` and `JSON_QUERY` functions return only the first value that matches the path. To parse a JSON object that contains duplicate keys and return all values, use `OPENJSON`, as shown in the following example.

```sql
DECLARE @json AS NVARCHAR (MAX);

SET @json = N'{"person":{"info":{"name":"John", "name":"Jack"}}}';

SELECT value
FROM OPENJSON (@json, '$.person.info');
```

## Learn more about JSON

For a visual introduction to the built-in JSON support, see the following video:

- [JSON as a bridge between NoSQL and relational worlds](/shows/datadriven-sqlserver2016/json-as-bridge-betwen-nosql-relational-worlds)

## Related content

- [OPENJSON (Transact-SQL)](../../t-sql/functions/openjson-transact-sql.md)
- [JSON_VALUE (Transact-SQL)](../../t-sql/functions/json-value-transact-sql.md)
- [JSON_QUERY (Transact-SQL)](../../t-sql/functions/json-query-transact-sql.md)
