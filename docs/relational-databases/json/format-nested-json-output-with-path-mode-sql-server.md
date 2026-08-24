---
title: "Format Nested JSON Output with PATH Mode"
description: "To maintain full control over the output of the FOR JSON clause, specify the PATH option."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, umajay, randolphwest
ms.date: 01/28/2026
ms.service: sql
ms.topic: how-to
ms.custom:
  - ignite-2025
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Format nested JSON output with PATH mode

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-serverless-pool-only-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-svrless-only-fabricse-fabricdw-fabricsqldb.md)]

To maintain full control over the output of the `FOR JSON` clause, specify the `PATH` option.

`PATH` mode lets you create wrapper objects and nest complex properties. The results are formatted as an array of JSON objects.

The alternative is to use the `AUTO` option to format the output automatically based on the structure of the `SELECT` statement.

- For more info about the `AUTO` option, see [Format JSON output automatically with AUTO mode](format-json-output-automatically-with-auto-mode-sql-server.md).
- For an overview of both options, see [Format query results as JSON with FOR JSON](format-query-results-as-json-with-for-json-sql-server.md).

The following examples show how to use the `FOR JSON` clause with the `PATH` option. Format nested results by using dot-separated column names or by using nested queries, as shown in the examples. By default, null values aren't included in `FOR JSON` output.

> [!NOTE]  
> The [MSSQL extension for Visual Studio Code](../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) can auto-format the JSON results (as seen in this article) instead of displaying an unformatted string.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Dot-separated column names

The following query formats the first five rows from the AdventureWorks `Person` table as JSON.

The `FOR JSON PATH` clause uses the column alias or column name to determine the key name in the JSON output. If an alias contains dots, the `PATH` option creates nested objects.

```sql
SELECT TOP 5 BusinessEntityID AS Id,
             FirstName,
             LastName,
             Title AS 'Info.Title',
             MiddleName AS 'Info.MiddleName'
FROM Person.Person
FOR JSON PATH;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```json
[{
    "Id": 1,
    "FirstName": "Ken",
    "LastName": "Sanchez",
    "Info": {
        "MiddleName": "J"
    }
}, {
    "Id": 2,
    "FirstName": "Terri",
    "LastName": "Duffy",
    "Info": {
        "MiddleName": "Lee"
    }
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
    "Info": {
        "Title": "Ms.",
        "MiddleName": "A"
    }
}]
```

### B. Multiple tables

If you reference more than one table in a query, `FOR JSON PATH` nests each column using its alias. The following query creates one JSON object for each `(OrderHeader, OrderDetails)` pair that the query joins.

```sql
SELECT TOP 2 H.SalesOrderNumber AS 'Order.Number',
             H.OrderDate AS 'Order.Date',
             D.UnitPrice AS 'Product.Price',
             D.OrderQty AS 'Product.Quantity'
FROM Sales.SalesOrderHeader AS H
     INNER JOIN Sales.SalesOrderDetail AS D
         ON H.SalesOrderID = D.SalesOrderID
FOR JSON PATH;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```json
[{
    "Order": {
        "Number": "SO43659",
        "Date": "2011-05-31T00:00:00"
    },
    "Product": {
        "Price": 2024.9940,
        "Quantity": 1
    }
}, {
    "Order": {
        "Number": "SO43659"
    },
    "Product": {
        "Price": 2024.9940
    }
}]
```

## Learn more about JSON in the SQL Database Engine

For a visual introduction to the built-in JSON support, see [JSON as a bridge between NoSQL and relational worlds](/events/datadriven-sqlserver2016/json-as-bridge-betwen-nosql-relational-worlds).

## Related content

- [SELECT - FOR clause (Transact-SQL)](../../t-sql/queries/select-for-clause-transact-sql.md)
