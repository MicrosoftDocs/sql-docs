---
title: "COUNT_BIG (Transact-SQL)"
description: "COUNT_BIG (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "COUNT_BIG_TSQL"
  - "COUNT_BIG"
helpviewer_keywords:
  - "totals [SQL Server], COUNT_BIG function"
  - "counting items in group"
  - "groups [SQL Server], number of items in"
  - "number of group items"
  - "COUNT_BIG function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# COUNT_BIG (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns the number of items found in a group. `COUNT_BIG` operates like the [COUNT](../../t-sql/functions/count-transact-sql.md) function. These functions differ only in the data types of their return values. `COUNT_BIG` always returns a **bigint** data type value. `COUNT` always returns an **int** data type value.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql

-- Aggregation Function Syntax  
COUNT_BIG ( { [ [ ALL | DISTINCT ] expression ] | * } )  
  
-- Analytic Function Syntax  
COUNT_BIG ( [ ALL ] { expression | * } ) OVER ( [ <partition_by_clause> ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
ALL  
Applies the aggregate function to all values. ALL serves as the default.
  
DISTINCT  
Specifies that `COUNT_BIG` returns the number of unique nonnull values.
  
*expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type. `COUNT_BIG` does not support aggregate functions or subqueries in an expression.
  
*\**  
Specifies that `COUNT_BIG` should count all rows to determine the total table row count to return. `COUNT_BIG(*)` takes no parameters and does not support the use of DISTINCT. `COUNT_BIG(*)` does not require an *expression* parameter because by definition, it does not use information about any particular column. `COUNT_BIG(*)` returns the number of rows in a specified table, and it preserves duplicate rows. It counts each row separately, including rows that contain null values.
  
OVER **(** [ *partition_by_clause* ] [ *order_by_clause* ] **)**  
The *partition_by_clause* divides the result set produced by the `FROM` clause into partitions to which the `COUNT_BIG` function is applied. If not specified, the function treats all rows of the query result set as a single group. The *order_by_clause* determines the logical order of the operation. See [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md) for more information.
  
## Return types
**bigint**
  
## Remarks  
COUNT_BIG(\*) returns the number of items in a group. This includes NULL values and duplicates.
  
COUNT_BIG (ALL *expression*) evaluates *expression* for each row in a group, and returns the number of nonnull values.
  
COUNT_BIG (DISTINCT *expression*) evaluates *expression* for each row in a group, and returns the number of unique, nonnull values.
  
COUNT_BIG is a deterministic function when used **_without_** the OVER and ORDER BY clauses. COUNT_BIG is nondeterministic when used **_with_** the OVER and ORDER BY clauses. See [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md) for more information.
  
## Examples  
See [COUNT &#40;Transact-SQL&#41;](../../t-sql/functions/count-transact-sql.md) for examples.
  
## See also
[Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md)  
[COUNT &#40;Transact-SQL&#41;](../../t-sql/functions/count-transact-sql.md)  
[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)  
[OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md)
  
  
