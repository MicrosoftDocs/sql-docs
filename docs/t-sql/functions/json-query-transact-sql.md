---
title: "JSON_QUERY (Transact-SQL)"
description: JSON_QUERY extracts an object or an array from a JSON string.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, umajay, randolphwest
ms.date: 10/27/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "JSON_QUERY"
  - "JSON_QUERY_TSQL"
helpviewer_keywords:
  - "JSON, extracting"
  - "JSON, querying"
  - "JSON_QUERY function"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# JSON_QUERY (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

The `JSON_QUERY` syntax extracts an object or an array from a JSON string.

To extract a scalar value from a JSON string instead of an object or an array, see [JSON_VALUE](json-value-transact-sql.md). For info about the differences between `JSON_VALUE` and `JSON_QUERY`, see [Compare JSON_VALUE and JSON_QUERY](../../relational-databases/json/validate-query-and-change-json-data-with-built-in-functions-sql-server.md#JSONCompare).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
JSON_QUERY ( expression [ , path ] [ WITH ARRAY WRAPPER ] )
```

## Arguments

#### *expression*

An expression. Typically the name of a variable or a column that contains JSON text.

If `JSON_QUERY` finds JSON that isn't valid in *expression* before it finds the value identified by *path*, the function returns an error. If `JSON_QUERY` doesn't find the value identified by *path*, it scans the entire text and returns an error if it finds JSON that isn't valid anywhere in *expression*.

#### *path*

A JSON path that specifies the object or the array to extract.

In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], you can provide a variable as the value of *path*.

The JSON path can specify lax or strict mode for parsing. If you don't specify the parsing mode, lax mode is the default. For more info, see [JSON Path Expressions in the SQL Database Engine](../../relational-databases/json/json-path-expressions-sql-server.md).

The default value for *path* is `$`. As a result, if you don't provide a value for *path*, `JSON_QUERY` returns the input *expression*.

If the format of *path* isn't valid, `JSON_QUERY` returns an error.

#### *WITH ARRAY WRAPPER*

> [!NOTE]  
> `WITH ARRAY WRAPPER` is currently in preview and only available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

The ANSI SQL `JSON_QUERY` function is currently used to return a JSON object or array in a specified path. With the support for [array wildcards](../../relational-databases/json/json-path-expressions-sql-server.md#array-wildcard-and-range-support) in SQL/JSON path expression introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], `JSON_QUERY` can be used to return specified properties of elements in a JSON array where each element is a JSON object. Since wildcard searches can return multiple values, specify the `WITH ARRAY WRAPPER` clause in a JSON query expression along with a SQL/JSON path expression with wildcard or range or list to return the values as a JSON array. `WITH ARRAY WRAPPER` clause is supported only if the input is a **json** type.

Consider the following JSON document:

```sql
DECLARE @j AS JSON = '{
    "id": 2,
    "first_name": "Mamie",
    "last_name": "Baudassi",
    "email": "mbaudassi1@example.com",
    "gender": "Female",
    "ip_address": "148.199.129.123",
    "credit_cards": [
        {
            "type": "jcb",
            "card#": "3545138777072343",
            "currency": "Koruna"
        },
        {
            "type": "diners-club-carte-blanche",
            "card#": "30282304348533",
            "currency": "Dong"
        },
        {
            "type": "jcb",
            "card#": "3585303288595361",
            "currency": "Yuan Renminbi"
        },
        {
            "type": "maestro",
            "card#": "675984450768756054",
            "currency": "Rupiah"
        },
        {
            "type": "instapayment",
            "card#": "6397068371771473",
            "currency": "Euro"
        }
    ]
}';
```

The path `$.credit_cards` points to a JSON array where each element is a valid JSON object. Now, the `JSON_QUERY` function can be used with array wildcard support to return all or specific values of the `type` property like:

```sql
SELECT JSON_QUERY(@j, '$.credit_cards[*].type' WITH ARRAY WRAPPER);
```

The following table shows various examples of SQL/JSON path expression with wildcard and the return value using `JSON_QUERY WITH ARRAY WRAPPER`.

| Path | Return value |
| --- | --- |
| `$.credit_cards[0].type` | `["jcb"]` |
| `$.credit_cards[*].type` | `["jcb","diners-club-carte-blanche","jcb","maestro","instapayment"]` |
| `$.credit_cards[0, 2].type` | `["jcb","jcb"]` |
| `$.credit_cards[1 to 3].type` | `["diners-club-carte-blanche","jcb","maestro"]` |
| `$.credit_cards[last].type` | `["instapayment"]` |
| `$.credit_cards[last, 0].type` | `["instapayment","jcb"]` |
| `$.credit_cards[last, last].type` | `["instapayment","instapayment"]` |
| `$.credit_cards[ 0, 2, 4].type` | `["jcb","jcb","instapayment"]` |

## Return value

Returns a JSON fragment of type **nvarchar(max)**. The collation of the returned value is the same as the collation of the input expression.

If the value isn't an object or an array:

- In lax mode, `JSON_QUERY` returns null.

- In strict mode, `JSON_QUERY` returns an error.

## Remarks

### Lax mode and strict mode

Consider the following JSON text:

```json
{
   "info": {
      "type": 1,
      "address": {
         "town": "Cheltenham",
         "county": "Gloucestershire",
         "country": "England"
      },
      "tags": ["Sport", "Water polo"]
   },
   "type": "Basic"
}
```

The following table compares the behavior of `JSON_QUERY` in lax mode and in strict mode. For more info about the optional path mode specification (lax or strict), see [JSON Path Expressions in the SQL Database Engine](../../relational-databases/json/json-path-expressions-sql-server.md).

| Path | Return value in lax mode | Return value in strict mode | More info |
| --- | --- | --- | --- |
| `$` | Returns the entire JSON text. | Returns the entire JSON text. | |
| `$.info.type` | `NULL` | Error | Not an object or array.<br /><br />Use `JSON_VALUE` instead. |
| `$.info.address.town` | `NULL` | Error | Not an object or array.<br /><br />Use `JSON_VALUE` instead. |
| `$.info."address"` | `N'{ "town":"Cheltenham", "county":"Gloucestershire", "country":"England" }'` | `N'{ "town":"Cheltenham", "county":"Gloucestershire", "country":"England" }'` | |
| `$.info.tags` | `N'[ "Sport", "Water polo"]'` | `N'[ "Sport", "Water polo"]'` | |
| `$.info.type[0]` | `NULL` | Error | Not an array. |
| `$.info.none` | `NULL` | Error | Property doesn't exist. |

### Use JSON_QUERY with FOR JSON

`JSON_QUERY` returns a valid JSON fragment. As a result, `FOR JSON` doesn't escape special characters in the `JSON_QUERY` return value.

If you're returning results with FOR JSON, and you're including data that's already in JSON format (in a column or as the result of an expression), wrap the JSON data with `JSON_QUERY` without the *path* parameter.

## Examples

### A. Return a JSON fragment

The following example shows how to return a JSON fragment from a `CustomFields` column in query results.

```sql
SELECT PersonID,
       FullName,
       JSON_QUERY(CustomFields, '$.OtherLanguages') AS Languages
FROM Application.People;
```

### B. Include JSON fragments in FOR JSON output

The following example shows how to include JSON fragments in the output of the FOR JSON clause.

```sql
SELECT StockItemID,
       StockItemName,
       JSON_QUERY(Tags) AS Tags,
       JSON_QUERY(CONCAT('["', ValidFrom, '","', ValidTo, '"]')) AS ValidityPeriod
FROM Warehouse.StockItems
FOR JSON PATH;
```

### C. Use WITH ARRAY WRAPPER with JSON_QUERY function

The following example shows the use of `WITH ARRAY WRAPPER` with the `JSON_QUERY` function to return multiple elements from a JSON array:

```sql
DECLARE @j JSON = '
{"id":2, "first_name":"Mamie", "last_name":"Baudassi", "email":"mbaudassi1@example.com", "gender":"Female", "ip_address":"148.199.129.123", "credit_cards":[ {"type":"jcb", "card#":"3545138777072343", "currency":"Koruna"}, {"type":"diners-club-carte-blanche", "card#":"30282304348533", "currency":"Dong"}, {"type":"jcb", "card#":"3585303288595361", "currency":"Yuan Renminbi"}, {"type":"maestro", "card#":"675984450768756054", "currency":"Rupiah"}, {"type":"instapayment", "card#":"6397068371771473", "currency":"Euro"}]}
';
SELECT JSON_QUERY(@j, '$.credit_cards[*].type' WITH ARRAY WRAPPER ) as credit_card_types;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
credit_card_types
--------
["jcb","diners-club-carte-blanche","jcb","maestro","instapayment"]
```

## Related content

- [JSON Path Expressions in the SQL Database Engine](../../relational-databases/json/json-path-expressions-sql-server.md)
- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
