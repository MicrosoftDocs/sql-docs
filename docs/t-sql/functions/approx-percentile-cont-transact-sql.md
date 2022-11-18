---
title: APPROX_PERCENTILE_CONT (Transact-SQL)
description: A function that returns an approximate interpolated value from the set of values in a group based on percentile value and sort specification.
author: blakhani-msft
ms.author: blakhani
ms.reviewer: "maghan"
ms.date: 07/25/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "PERCENTILE_CONT_TSQL"
  - "PERCENTILE_CONT"
helpviewer_keywords:
  - "analytic functions, PERCENTILE_CONT"
  - "PERCENTILE_CONT function"
dev_langs:
  - "TSQL"
monikerRange: "azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqledge-current"
---

# APPROX_PERCENTILE_CONT (Transact-SQL)

[!INCLUDE[SQL Server 2022 Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

This function returns an approximate interpolated value from the set of values in a group based on percentile value and sort specification. Since this is an approximate function, the output would be within rank based error bound with certain confidence. The percentile value returned by this function is based on a continuous distribution of the column values and the result would be interpolated. Due to this, the output might not be one of values in the data set. One of the common use cases for this function is to avoid the data outliers. This function can be used as an alternative to PERCENTILE_CONT for large datasets where negligible error with faster response is acceptable as compared to accurate percentile value with slow response time.

[Transact-SQL Syntax Conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
APPROX_PERCENTILE_CONT (numeric_literal)
WITHIN GROUP (ORDER BY order_by_expression [ASC|DESC]) 
```

## Argument

*numeric_literal*

The percentile to compute. The value must range between 0.0 and 1.0.

*order_by_expression*

Specifies a list of numeric values to sort and compute the percentile
over. Only one *order_by_expression* is allowed. The default sort order
is ascending (ASC).  The expression must evaluate to an exact or
approximate numeric type, with no other data types allowed. Exact
numeric types are int, bigint, smallint, tinyint, numeric, bit, decimal,
smallmoney, and money. Approximate numeric types are float and real.

## Return type

float(53)

## Remarks

Any nulls in the data set are ignored.

Approximate percentile functions use KLL sketch. The sketch is built by reading the stream of data. Due to the algorithm used, this function requires less memory than its non-approximate counterpart ([PERCENTILE_CONT](./percentile-cont-transact-sql.md)).

This function provides rank-based error guarantees not value based. The function implementation guarantees up to a 1.33% error.

## Known behaviors

- The output of the function may not be the same in all executions. The algorithm used for these functions is [KLL sketch](https://arxiv.org/pdf/1603.05346v2.pdf) which is a randomized algorithm. Every time the sketch is built, random values are picked. These functions provide rank-based error guarantees not value based.
- The function implementation guarantees up to a 1.33% error bounds within a 99% confidence.

## Compatibility support

Under compatibility level 110 and higher, WITHIN GROUP is a reserved keyword. For more information, see [ALTER DATABASE Compatibility Level (Transact-SQL).](../statements/alter-database-transact-sql-compatibility-level.md)

## Examples

The following example creates a table, populates it, and executes a sample query.

```sql
SET NOCOUNT ON
GO
DROP TABLE IF EXISTS tblEmployee;
GO
CREATE TABLE tblEmployee (
EmplId INT IDENTITY(1,1) PRIMARY KEY CLUSTERED,
DeptId INT,
Salary int);
GO
INSERT INTO tblEmployee
VALUES (1, 31),(1, 33), (1, 18), (2, 25),(2, 35),(2, 10), (2, 10),(3,
1), (3,NULL), (4,NULL), (4,NULL);
GO
SELECT DeptId,
APPROX_PERCENTILE_CONT(0.10) WITHIN GROUP(ORDER BY Salary) AS 'P10',
APPROX_PERCENTILE_CONT(0.90) WITHIN GROUP(ORDER BY Salary) AS 'P90'
FROM tblEmployee
GROUP BY DeptId;
```
  
## See also

[PERCENTILE_CONT](../../t-sql/functions/percentile-cont-transact-sql.md)