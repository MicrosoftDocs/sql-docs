---
title: Validate, query, and change JSON data with built-in functions
description: "Validate, query, and change JSON data with built-in functions (SQL Server)."
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: jroth, randolphwest
ms.date: 05/21/2024
ms.service: sql
ms.topic: conceptual
ms.custom:
  - build-2024
helpviewer_keywords:
  - "JSON, built-in functions"
  - "functions (JSON)"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Validate, query, and change JSON data with built-in functions (SQL Server)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

The built-in support for JSON includes the following built-in functions described briefly in this article.

- [ISJSON](#ISJSON) tests whether a string contains valid JSON.
- [JSON_VALUE](#VALUE) extracts a scalar value from a JSON string.
- [JSON_QUERY](#QUERY) extracts an object or an array from a JSON string.
- [JSON_MODIFY](#MODIFY) updates the value of a property in a JSON string and returns the updated JSON string.

For all JSON functions, review [JSON functions](../../t-sql/functions/json-functions-transact-sql.md).

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

## JSON text for the examples on this page

The examples on this page use the JSON text similar to the content shown in the following example:

```json
{
    "id": "DesaiFamily",
    "parents": [
        { "familyName": "Desai", "givenName": "Prashanth" },
        { "familyName": "Miller", "givenName": "Helen" }
    ],
    "children": [
        {
            "familyName": "Desai",
            "givenName": "Jesse",
            "gender": "female",
            "grade": 1,
            "pets": [
                { "givenName": "Goofy" },
                { "givenName": "Shadow" }
            ]
        },
        {
            "familyName": "Desai",
            "givenName": "Lisa",
            "gender": "female",
            "grade": 8
        }
    ],
    "address": {
        "state": "NY",
        "county": "Manhattan",
        "city": "NY"
    },
    "creationDate": 1431620462,
    "isRegistered": false
}
```

This JSON document, which contains nested complex elements, is stored in the following sample table:

```sql
CREATE TABLE Families (
    id INT identity CONSTRAINT PK_JSON_ID PRIMARY KEY,
    [doc] NVARCHAR(MAX)
);
```

JSON functions work the same whether the JSON document is stored in **varchar**/**nvarchar** or the native **JSON** data type.

## <a id="ISJSON"></a> Validate JSON text by using the ISJSON function

The `ISJSON` function tests whether a string contains valid JSON.

The following example returns rows in which the JSON column contains valid JSON text. Without explicit JSON constraint, you can enter any text in the **nvarchar** column:

```sql
SELECT *
FROM Families
WHERE ISJSON(doc) > 0;
```

For more information, see [ISJSON](../../t-sql/functions/isjson-transact-sql.md).

## <a id="VALUE"></a> Extract a value from JSON text by using the JSON_VALUE function

The `JSON_VALUE` function extracts a scalar value from a JSON string. The following query returns the documents where the `id` JSON field matches the value `DesaiFamily`, ordered by `city` and `state` JSON fields:

```sql
SELECT JSON_VALUE(f.doc, '$.id') AS Name,
    JSON_VALUE(f.doc, '$.address.city') AS City,
    JSON_VALUE(f.doc, '$.address.county') AS County
FROM Families f
WHERE JSON_VALUE(f.doc, '$.id') = N'DesaiFamily'
ORDER BY JSON_VALUE(f.doc, '$.address.city') DESC,
    JSON_VALUE(f.doc, '$.address.state') ASC
```

The results of this query are shown in the following table:

| Name | City | County |
| --- | --- | --- |
| `DesaiFamily` | `NY` | `Manhattan` |

For more information, see [JSON_VALUE](../../t-sql/functions/json-value-transact-sql.md).

## <a id="QUERY"></a> Extract an object or an array from JSON text by using the JSON_QUERY function

The `JSON_QUERY` function extracts an object or an array from a JSON string. The following example shows how to return a JSON fragment in query results.

```sql
SELECT JSON_QUERY(f.doc, '$.address') AS Address,
    JSON_QUERY(f.doc, '$.parents') AS Parents,
    JSON_QUERY(f.doc, '$.parents[0]') AS Parent0
FROM Families f
WHERE JSON_VALUE(f.doc, '$.id') = N'DesaiFamily';
```

The results of this query are shown in the following table:

| Address | Parents | Parent0 |
| --- | --- | --- |
| `{ "state": "NY", "county": "Manhattan", "city": "NY" }` | `[ { "familyName": "Desai", "givenName": "Prashanth" }, { "familyName": "Miller", "givenName": "Helen" } ]` | `{ "familyName": "Desai", "givenName": "Prashanth" }` |

For more information, see [JSON_QUERY](../../t-sql/functions/json-query-transact-sql.md).

## Parse nested JSON collections

`OPENJSON` function enables you to transform JSON subarray into the rowset and then join it with the parent element. As an example, you can return all family documents, and "join" them with their `children` objects that are stored as an inner JSON array:

```sql
SELECT JSON_VALUE(f.doc, '$.id') AS Name,
    JSON_VALUE(f.doc, '$.address.city') AS City,
    c.givenName,
    c.grade
FROM Families f
CROSS APPLY OPENJSON(f.doc, '$.children') WITH (
    grade INT,
    givenName NVARCHAR(100)
) c
```

The results of this query are shown in the following table:

| Name | City | givenName | grade |
| --- | --- | --- | --- |
| `DesaiFamily` | `NY` | `Jesse` | `1` |
| `DesaiFamily` | `NY` | `Lisa` | `8` |

Two rows are returned, because one parent row is joined with two child rows produced by parsing two elements of the children subarray. `OPENJSON` function parses `children` fragment from the `doc` column and returns `grade` and `givenName` from each element as a set of rows. This rowset can be joined with the parent document.

## Query nested hierarchical JSON subarrays

You can apply multiple `CROSS APPLY OPENJSON` calls in order to query nested JSON structures. The JSON document used in this example has a nested array called `children`, where each child has nested array of `pets`. The following query parses children from each document, return each array object as row, and then parse `pets` array:

```sql
SELECT c.familyName,
    c.givenName AS childGivenName,
    p.givenName AS petName
FROM Families f
CROSS APPLY OPENJSON(f.doc) WITH (
    familyName NVARCHAR(100),
    children NVARCHAR(MAX) AS JSON
) AS a
CROSS APPLY OPENJSON(children) WITH (
    familyName NVARCHAR(100),
    givenName NVARCHAR(100),
    pets NVARCHAR(max) AS JSON
) AS c
OUTER APPLY OPENJSON(pets) WITH (givenName NVARCHAR(100)) AS p;
```

The first `OPENJSON` call returns fragment of `children` array using AS JSON clause. This array fragment is provided to the second `OPENJSON` function that returns `givenName`, `firstName` of each child, as well as the array of `pets`. The array of `pets` is provided to the third `OPENJSON` function that returns the `givenName` of the pet.

The results of this query are shown in the following table:

| familyName | childGivenName | petName |
| --- | --- | --- | --- |
| `Desai` | `Jesse` | `Goofy` |
| `Desai` | `Jesse` | `Shadow` |
| `Desai` | `Lisa` | `NULL` |

The root document is joined with two `children` rows returned by first `OPENJSON(children)` call making two rows (or tuples). Then each row is joined with the new rows generated by `OPENJSON(pets)` using `OUTER APPLY` operator. Jesse has two pets, so `(Desai, Jesse)` is joined with two rows generated for `Goofy` and `Shadow`. Lisa doesn't have the pets, so there are no rows returned by `OPENJSON(pets)` for this tuple. However, since we use `OUTER APPLY`, we get `NULL` in the column. If we put `CROSS APPLY` instead of `OUTER APPLY`, Lisa wouldn't be returned in the result because there are no pets rows that could be joined with this tuple.

## <a id="JSONCompare"></a> Compare JSON_VALUE and JSON_QUERY

The key difference between `JSON_VALUE` and `JSON_QUERY` is that `JSON_VALUE` returns a scalar value, while `JSON_QUERY` returns an object or an array.

Consider the following sample JSON text.

```json
{
    "a": "[1,2]",
    "b": [1, 2],
    "c": "hi"
}
```

In this sample JSON text, data members "a" and "c" are string values, while data member "b" is an array. `JSON_VALUE` and `JSON_QUERY` return the following results:

| Path | `JSON_VALUE` returns | `JSON_QUERY` returns |
| --- | --- | --- |
| `$` | `NULL` or error | `{ "a": "[1,2]", "b": [1, 2], "c": "hi" }` |
| `$.a` | `[1,2]` | `NULL` or error |
| `$.b` | `NULL` or error | `[1,2]` |
| `$.b[0]` | `1` | `NULL` or error |
| `$.c` | `hi` | `NULL` or error |

## Test JSON_VALUE and JSON_QUERY with the AdventureWorks sample database

Test the built-in functions described in this article by running the following examples with the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database. For more information about how to add JSON data for testing by running a script, see [Test drive built-in JSON support](json-data-sql-server.md#test-drive-built-in-json-support-with-the-adventureworks-sample-database).

In the following examples, the `Info` column in the `SalesOrder_json` table contains JSON text.

### Example 1 - Return both standard columns and JSON data

The following query returns values from both standard relational columns and from a JSON column.

```sql
SELECT SalesOrderNumber,
    OrderDate,
    Status,
    ShipDate,
    AccountNumber,
    TotalDue,
    JSON_QUERY(Info, '$.ShippingInfo') ShippingInfo,
    JSON_QUERY(Info, '$.BillingInfo') BillingInfo,
    JSON_VALUE(Info, '$.SalesPerson.Name') SalesPerson,
    JSON_VALUE(Info, '$.ShippingInfo.City') City,
    JSON_VALUE(Info, '$.Customer.Name') Customer,
    JSON_QUERY(OrderItems, '$') OrderItems
FROM Sales.SalesOrder_json
WHERE ISJSON(Info) > 0;
```

### Example 2- Aggregate and filter JSON values

The following query aggregates subtotals by customer name (stored in JSON) and status (stored in an ordinary column). Then it filters the results by city (stored in JSON) and OrderDate (stored in an ordinary column).

```sql
DECLARE @territoryid INT;
DECLARE @city NVARCHAR(32);

SET @territoryid = 3;
SET @city = N'Seattle';

SELECT JSON_VALUE(Info, '$.Customer.Name') AS Customer,
    Status,
    SUM(SubTotal) AS Total
FROM Sales.SalesOrder_json
WHERE TerritoryID = @territoryid
    AND JSON_VALUE(Info, '$.ShippingInfo.City') = @city
    AND OrderDate > '1/1/2015'
GROUP BY JSON_VALUE(Info, '$.Customer.Name'),
    Status
HAVING SUM(SubTotal) > 1000;
```

## <a id="MODIFY"></a> Update property values in JSON text by using the JSON_MODIFY function

The `JSON_MODIFY` function updates the value of a property in a JSON string and returns the updated JSON string.

The following example updates the value of a JSON property in a variable that contains JSON.

```sql
SET @info = JSON_MODIFY(@jsonInfo, '$.info.address[0].town', 'London');
```

For more information, see [JSON_MODIFY](../../t-sql/functions/json-modify-transact-sql.md).

## Related content

- [ISJSON (Transact-SQL)](../../t-sql/functions/isjson-transact-sql.md)
- [JSON_VALUE (Transact-SQL)](../../t-sql/functions/json-value-transact-sql.md)
- [JSON_QUERY (Transact-SQL)](../../t-sql/functions/json-query-transact-sql.md)
- [JSON_MODIFY (Transact-SQL)](../../t-sql/functions/json-modify-transact-sql.md)
- [JSON Path Expressions (SQL Server)](json-path-expressions-sql-server.md)
- [JSON data type](../../t-sql/data-types/json-data-type.md)
