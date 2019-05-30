---
title: "Pushdown computations in PolyBase | Microsoft Docs"
ms.custom: ""
ms.date: 04/23/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: polybase
ms.topic: conceptual
author: rothja
ms.author: jroth
manager: craigg
monikerRange: ">= sql-server-2016 || =sqlallproducts-allversions"
---
# Pushdown computations in PolyBase

## DMV

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Pushdown computation improves the performance of queries on your Hadoop cluster.

## Enable pushdown

The steps for enabling pushdown are covered in the following article:

[Enable pushdown computation in Hadoop](polybase-configure-hadoop.md#pushdown)

## Select a subset of rows

Use predicate pushdown to improve performance for a query that selects a subset of rows from an external table.

In this example, SQL Server 2016 initiates a map-reduce job to retrieve the rows that match the predicate `customer.account_balance < 200000` on Hadoop. Because the query can complete successfully without scanning all of the rows in the table, only the rows that meet the predicate criteria are copied to SQL Server. This saves significant time and requires less temporary storage space when the number of customer balances < 200000 is small in comparison with the number of customers with account balances >= 200000.

```sql
SELECT * FROM customer WHERE customer.account_balance < 200000
SELECT * FROM SensorData WHERE Speed > 65;  
```

### Select a subset of columns

Use predicate pushdown to improve performance for a query that selects a subset of columns from an external table.

In this query, SQL Server initiates a map-reduce job to pre-process the Hadoop delimited-text file so that only the data for the two columns, customer.name and customer.zip_code, will be copied to SQL Server PDW.

```sql
SELECT customer.name, customer.zip_code FROM customer WHERE customer.account_balance < 200000
```

### Pushdown for basic expressions and operators

SQL Server allows the following basic expressions and operators for predicate pushdown.

+ Binary comparison operators ( \<, >, =, !=, <>, >=, <= ) for numeric, date, and time values.

+ Arithmetic operators ( +, -, *, /, % ).

+ Logical operators (AND, OR).

+ Unary operators (NOT, IS NULL, IS NOT NULL).

The operators BETWEEN, NOT, IN, and LIKE might be pushed-down. The actual behavior depends on how the query optimizer rewrites the operator expressions as a series of statements that use basic relational operators.

The query in this example has multiple predicates that can be pushed down to Hadoop. SQL Server can push map-reduce jobs to Hadoop to perform the predicate `customer.account_balance <= 200000`. The expression `BETWEEN 92656 and 92677` is also composed of binary and logical operations that can be pushed to Hadoop. The logical **AND** in `customer.account_balance and customer.zipcode` is a final expression.

Given this combination of predicates, the map-reduce jobs can perform all of the WHERE clause. Only the data that meets the SELECT criteria is copied back to SQL Server PDW.

```sql
SELECT * FROM customer WHERE customer.account_balance <= 200000 AND customer.zipcode BETWEEN 92656 AND 92677
```

## Force pushdown

```sql
SELECT * FROM [dbo].[SensorData]
WHERE Speed > 65
OPTION (FORCE EXTERNALPUSHDOWN);
```

## Disable pushdown

```sql
SELECT * FROM [dbo].[SensorData]
WHERE Speed > 65
OPTION (DISABLE EXTERNALPUSHDOWN);
```

## Next steps

For more information about PolyBase, see [What is PolyBase?](polybase-guide.md).
