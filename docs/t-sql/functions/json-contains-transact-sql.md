---
title: JSON_CONTAINS (Transact-SQL)
description: The JSON_CONTAINS function searches for a SQL value in a path in a JSON document.
author: uc-msft
ms.author: umajay
ms.reviewer: randolphwest
ms.date: 10/27/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: language-reference
ms.custom:
  - build-2025
monikerRange: ">=sql-server-2017"
---
# JSON_CONTAINS (Transact-SQL)

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

Searches for a SQL value in a path in a JSON document.

> [!NOTE]  
> The `JSON_CONTAINS` function is currently in preview and only available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
JSON_CONTAINS( target_expression , search_value_expression [ , path_expression ]  [ , search_mode ] )
```

## Arguments

#### *target_expression*

An expression that returns a target JSON document to search. The value can be a **json** type or character string value that contains a JSON document.

#### *search_value_expression*

An expression that returns a SQL scalar value or **json** type value to search in the specified SQL/JSON document.

#### *path*

A SQL/JSON path that specifies the search target in the JSON document. This parameter is optional.

You can provide a variable as the value of *path*. The JSON path can specify lax or strict mode for parsing. If you don't specify the parsing mode, lax mode is the default. For more info, see [JSON Path Expressions in the SQL Database Engine](../../relational-databases/json/json-path-expressions-sql-server.md).

The default value for *path* is `$`. As a result, if you don't provide a value for *path*, `JSON_CONTAINS` searches for the value in the entire JSON document.

If the format of *path* isn't valid, `JSON_CONTAINS` returns an error.

#### *search_mode*

Indicates if the search mode for the value should use an equality or LIKE predicate semantics. This parameter only applies when the *search_value_expression* is a character string value. The default value for *search_mode* is 0, which indicates equality predicate semantics. If the *search_mode* is 1, then it indicates that LIKE predicate semantics should be used.

## Return value

Returns an **int** value of `0`, `1`, or `NULL`. A value of `1` indicates that the specified search value was contained within the target JSON document, or `0` otherwise. The `JSON_CONTAINS` function returns `NULL` if any of the arguments is `NULL`, or if the specified SQL/JSON path isn't found in the JSON document.

## Remarks

The `JSON_CONTAINS` function follows these rules for searching if a value is contained in a JSON document:

- A scalar search value is contained in a target scalar if and only if they're comparable and are equal. Since **json** types have only JSON number or string or true/false value, the possible SQL scalar types that can be specified as search value are limited to the SQL numeric types, character string types, and the **bit** type.

- The SQL type of the scalar search value is used to perform the comparison with the **json** type value in the specified path. This is different from `JSON_VALUE`-based predicate where the `JSON_VALUE` function always returns a character string value.

- A JSON array search value is contained in a target array if and only if every element in the search array is contained in some element of the target array.

- A scalar search value is contained in a target array if and only if the search value is contained in some element of the target array.

- A JSON object search value is contained in a target object if and only if each key/value in the search object is found in the target object.

## Limitations

Using the `JSON_CONTAINS` function has the following limitations:

- The **json** type isn't supported as search value.
- The JSON object or array returned from `JSON_QUERY` isn't supported as search value.
- The path parameter is currently required.
- If the SQL/JSON path points to an array then wildcard is required in the SQL/JSON path expression. Automatic array unwrapping is currently only at the first level.

JSON index support includes the `JSON_CONTAINS` predicate and the following operators:

- Comparison operators (`=`)
- `IS [NOT] NULL` predicate (not currently supported)

## Examples

### A. Search for a SQL integer value in a JSON path

The following example shows how to search for a SQL **int** value in a JSON array in a JSON path.

```sql
DECLARE @j AS JSON = '{"a": 1, "b": 2, "c": {"d": 4, "ce":["dd"]}, "d": [1, 3, {"df": [89]}, false], "e":null, "f":true}';

SELECT json_contains(@j, 1, '$.a') AS is_value_found;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
is_value_found
--------
1
```

### B. Search for a SQL character string value in a JSON path

The following example shows how to search for a SQL character string value in a JSON array in a JSON path.

```sql
DECLARE @j AS JSON = '{"a": 1, "b": 2, "c": {"d": 4, "ce":["dd"]}, "d": [1, 3, {"df": [89]}, false], "e":null, "f":true}';

SELECT json_contains(@j, 'dd', '$.c.ce[*]') AS is_value_found;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
is_value_found
--------
1
```

### C. Search for a SQL bit value in a JSON array in a JSON path

The following example shows how to search for a **sql** bit value in a JSON array in a JSON path.

```sql
DECLARE @j AS JSON = '{"a": 1, "b": 2, "c": {"d": 4, "ce":["dd"]}, "d": [1, 3, {"df": [89]}, false], "e":null, "f":true}';

SELECT json_contains(@j, CAST (0 AS BIT), '$.d[*]') AS is_value_found;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
is_value_found
--------
1
```

### D. Search for a SQL integer value contained within a nested JSON array

The following example shows how to search for a SQL **int** value contained within a nested JSON array in a JSON path.

```sql
DECLARE @j AS JSON = '{"a": 1, "b": 2, "c": {"d": 4, "ce":["dd"]}, "d": [1, 3, {"df": [89]}, false], "e":null, "f":true}';

SELECT json_contains(@j, 89, '$.d[*].df[*]') AS is_value_found;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
is_value_found
--------
1
```

### E. Search for a SQL integer value contained within a JSON object in a JSON array

The following example shows how to search for a SQL **int** value contained within a JSON object in a JSON array in a JSON path.

```sql
DECLARE @j AS JSON = '[{"a": 1}, {"b": 2}, {"c": 3}, {"a": 56}]';

SELECT json_contains(@j, 56, '$[*].a') AS is_value_found;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
is_value_found
--------
1
```

### F. Search for a SQL character string value in a JSON path using a wildcard pattern

The following example shows how to search for a SQL character string value using a pattern in a JSON array in a JSON path.

```sql
DECLARE @j AS JSON = '{"a": 1, "b": 2, "c": {"d": 4, "ce":["dd"]}, "d": [1, 3, {"df": [89]}, false], "e":null, "f":true}';

SELECT json_contains(@j, 'dd', '$.c.ce[*]') AS is_value_found;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
is_value_found
--------
1
```

## Related content

- [JSON path expressions in the SQL Database Engine](../../relational-databases/json/json-path-expressions-sql-server.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
