---
title: "Format JSON Output Automatically with AUTO Mode"
description: "To format the output of the FOR JSON clause automatically based on the structure of the SELECT statement, specify the AUTO option."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, umajay, randolphwest
ms.date: 01/28/2026
ms.service: sql
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "FOR JSON AUTO"
ai-usage: ai-assisted
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Format JSON output automatically with AUTO mode

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-serverless-pool-only-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-svrless-only-fabricse-fabricdw-fabricsqldb.md)]

To automatically format the output of the `FOR JSON` clause based on the structure of the `SELECT` statement, specify the `AUTO` option.

When you specify the `AUTO` option, the format of the JSON output is automatically determined based on the order of columns in the SELECT list and their source tables. You can't change this format.

Use the `PATH` option if you want to control the output.

- For more info about the `PATH` option, see [Format nested JSON output with PATH mode](format-nested-json-output-with-path-mode-sql-server.md).
- For an overview of both options, see [Format query results as JSON with FOR JSON](format-query-results-as-json-with-for-json-sql-server.md).

A query that uses the `FOR JSON AUTO` option must have a `FROM` clause.

Here are some examples of the `FOR JSON` clause with the `AUTO` option.

> [!NOTE]  
> The [MSSQL extension for Visual Studio Code](../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) can auto-format the JSON results (as seen in this article) instead of displaying an unformatted string.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Format JSON from a single table

When a query references only one table, the results of the `FOR JSON AUTO` clause are similar to the results of `FOR JSON PATH`. In this case, `FOR JSON AUTO` doesn't create nested objects. The only difference is that `FOR JSON AUTO` outputs dot-separated aliases (for example, `Info.MiddleName` in the following example) as keys with dots, not as nested objects.

```sql
SELECT TOP 5 BusinessEntityID AS Id,
             FirstName,
             LastName,
             Title AS 'Info.Title',
             MiddleName AS 'Info.MiddleName'
FROM Person.Person
FOR JSON AUTO;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```json
[{
    "Id": 1,
    "FirstName": "Ken",
    "LastName": "Sánchez",
    "Info.MiddleName": "J"
}, {
    "Id": 2,
    "FirstName": "Terri",
    "LastName": "Duffy",
    "Info.MiddleName": "Lee"
}, {
    "Id": 3,
    "FirstName": "Roberto",
    "LastName": "Tamburello"
}, {
    "Id": 4,
    "FirstName": "Rob",
    "LastName": "Walters"
}, {
    "Id": 5,
    "FirstName": "Gail",
    "LastName": "Erickson",
    "Info.Title": "Ms.",
    "Info.MiddleName": "A"
}]
```

### B. Format JSON for joined tables

When you join tables, columns in the first table are generated as properties of the root object. Columns in the second table are generated as properties of a nested object. The table name or alias of the second table (for example, `D` in the following example) is used as the name of the nested array.

```sql
SELECT TOP 2 SalesOrderNumber,
             OrderDate,
             UnitPrice,
             OrderQty
FROM Sales.SalesOrderHeader AS H
     INNER JOIN Sales.SalesOrderDetail AS D
         ON H.SalesOrderID = D.SalesOrderID
FOR JSON AUTO;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```json
[{
    "SalesOrderNumber": "SO43659",
    "OrderDate": "2011-05-31T00:00:00",
    "D": [{
        "UnitPrice": 24.99,
        "OrderQty": 1
    }]
}, {
    "SalesOrderNumber": "SO43659",
    "D": [{
        "UnitPrice": 34.40
    }, {
        "UnitPrice": 134.24,
        "OrderQty": 5
    }]
}]
```

### C. Use FOR JSON PATH to match AUTO output

Instead of using `FOR JSON AUTO`, you can nest a `FOR JSON PATH` subquery in the `SELECT` statement, as shown in the following example. This example outputs the same result as the preceding example.

```sql
SELECT TOP 2 SalesOrderNumber,
             OrderDate,
             (SELECT UnitPrice,
                     OrderQty
              FROM Sales.SalesOrderDetail AS D
              WHERE H.SalesOrderID = D.SalesOrderID
              FOR JSON PATH) AS D
FROM Sales.SalesOrderHeader AS H
FOR JSON PATH;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```json
[{
    "SalesOrderNumber": "SO43659",
    "OrderDate": "2011-05-31T00:00:00",
    "D": [{
        "UnitPrice": 24.99,
        "OrderQty": 1
    }]
}, {
    "SalesOrderNumber": "SO4390",
    "D": [{
        "UnitPrice": 24.99
    }]
}]
```

## Learn more about JSON in the SQL Database Engine

For a visual introduction to the built-in JSON support, see [JSON as a bridge between NoSQL and relational worlds](/events/datadriven-sqlserver2016/json-as-bridge-betwen-nosql-relational-worlds).

## Related content

- [SELECT - FOR clause (Transact-SQL)](../../t-sql/queries/select-for-clause-transact-sql.md)
