---
title: "Remove Square Brackets from JSON - WITHOUT_ARRAY_WRAPPER Option"
description: "To remove the square brackets that surround the JSON output of the FOR JSON clause by default, specify the WITHOUT_ARRAY_WRAPPER option."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, umajay
ms.date: 07/23/2025
ms.service: sql
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "WITHOUT_ARRAY_WRAPPER"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Remove Square Brackets from JSON - WITHOUT_ARRAY_WRAPPER Option

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-serverless-pool-only-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-svrless-only-fabricse-fabricdw-fabricsqldb.md)]

To remove the square brackets that surround the JSON output of the `FOR JSON` clause by default, specify the `WITHOUT_ARRAY_WRAPPER` option. Use this option with a single-row result to generate a single JSON object as output instead of an array with a single element.

If you use this option with a multiple-row result, the resulting output is not valid JSON because of the multiple elements and the missing square brackets.  

## Example (single-row result)

The following example shows the output of the `FOR JSON` clause with and without the `WITHOUT_ARRAY_WRAPPER` option.  

 **Query**  

```sql
SELECT 2015 as year, 12 as month, 15 as day  
FOR JSON PATH, WITHOUT_ARRAY_WRAPPER 
```  

 **Result** with the `WITHOUT_ARRAY_WRAPPER` option  

```json
{
    "year": 2015,
    "month": 12,
    "day": 15
} 
```  

 **Result** (default) without the `WITHOUT_ARRAY_WRAPPER` option  

```json
[{
    "year": 2015,
    "month": 12,
    "day": 15
}]
```  

## Example (multiple-row result)

Here's another example of a `FOR JSON` clause with and without the `WITHOUT_ARRAY_WRAPPER` option. This example produces a multiple-row result. The output is not valid JSON because of the multiple elements and the missing square brackets.

 **Query**  

```sql
SELECT TOP 3 SalesOrderNumber, OrderDate, Status  
FROM Sales.SalesOrderHeader  
ORDER BY ModifiedDate  
FOR JSON PATH, WITHOUT_ARRAY_WRAPPER 
```  

 **Result** with the `WITHOUT_ARRAY_WRAPPER` option  

```json
{
    "SalesOrderNumber": "SO43662",
    "OrderDate": "2011-05-31T00:00:00",
    "Status": 5
}, {
    "SalesOrderNumber": "SO43661",
    "OrderDate": "2011-05-31T00:00:00",
    "Status": 5
}, {
    "SalesOrderNumber": "SO43660",
    "OrderDate": "2011-05-31T00:00:00",
    "Status": 5
} 
```  

 **Result** (default) without the `WITHOUT_ARRAY_WRAPPER` option  

```json
[{
    "SalesOrderNumber": "SO43662",
    "OrderDate": "2011-05-31T00:00:00",
    "Status": 5
}, {
    "SalesOrderNumber": "SO43661",
    "OrderDate": "2011-05-31T00:00:00",
    "Status": 5
}, {
    "SalesOrderNumber": "SO43660",
    "OrderDate": "2011-05-31T00:00:00",
    "Status": 5
}]
```  

## Learn more about JSON in the SQL Database Engine

For a visual introduction to the built-in JSON support, see the following videos:

- [JSON as a bridge between NoSQL and relational worlds](/events/datadriven-sqlserver2016/json-as-bridge-betwen-nosql-relational-worlds)

## Related content

- [SELECT - FOR Clause (Transact-SQL)](../../t-sql/queries/select-for-clause-transact-sql.md)
