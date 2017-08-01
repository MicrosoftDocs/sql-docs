---
title: "COUNT_BIG (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "COUNT_BIG_TSQL"
  - "COUNT_BIG"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "totals [SQL Server], COUNT_BIG function"
  - "counting items in group"
  - "groups [SQL Server], number of items in"
  - "number of group items"
  - "COUNT_BIG function"
ms.assetid: f2e3601f-487e-4917-bb01-47b1047908cd
caps.latest.revision: 44
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# COUNT_BIG (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the number of items in a group. COUNT_BIG works like the COUNT function. The only difference between the two functions is their return values. COUNT_BIG always returns a **bigint** data type value. COUNT always returns an **int** data type value.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
-- Syntax for SQL Server and Azure SQL Database  
  
COUNT_BIG ( { [ ALL | DISTINCT ] expression } | * )  
   [ OVER ( [ partition_by_clause ] [ order_by_clause ] ) ]  
```  
  
```sql
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  

-- Aggregation Function Syntax  
COUNT_BIG ( { [ [ ALL | DISTINCT ] expression ] | * } )  
  
-- Analytic Function Syntax  
COUNT_BIG ( { expression | * } ) OVER ( [ <partition_by_clause> ] )  
```  
  
## Arguments  
ALL  
Applies the aggregate function to all values. ALL is the default.
  
DISTINCT  
Specifies that COUNT_BIG returns the number of unique nonnull values.
  
*expression*  
Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type. Aggregate functions and subqueries are not permitted.
  
*\**  
Specifies that all rows should be counted to return the total number of rows in a table. COUNT_BIG(*\**) takes no parameters and cannot be used with DISTINCT. COUNT_BIG(*\**) does not require an *expression* parameter because, by definition, it does not use information about any particular column. COUNT_BIG(*\**) returns the number of rows in a specified table without getting rid of duplicates. It counts each row separately. This includes rows that contain null values.
  
ALL  
Applies the aggregate function to all values. ALL is the default.
  
DISTINCT  
Specifies that AVG be performed only on each unique instance of a value, regardless of how many times the value occurs.
  
*expression*  
Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the exact numeric or approximate numeric data type category, except for the **bit** data type. Aggregate functions and subqueries are not permitted.
  
OVER **(** [ *partition_by_clause* ] [ *order_by_clause* ] **)**  
*partition_by_clause* divides the result set produced by the FROM clause into partitions to which the function is applied. If not specified, the function treats all rows of the query result set as a single group. *order_by_clause* determines the logical order in which the operation is performed. For more information, see [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md).
  
## Return types
**bigint**
  
## Remarks  
COUNT_BIG(*) returns the number of items in a group. This includes NULL values and duplicates.
  
COUNT_BIG (ALL *expression*) evaluates *expression* for each row in a group and returns the number of nonnull values.
  
COUNT_BIG (DISTINCT *expression*) evaluates *expression* for each row in a group and returns the number of unique, nonnull values.
  
COUNT_BIG is a deterministic function when used without the OVER and ORDER BY clauses. It is nondeterministic when specified with the OVER and ORDER BY clauses. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).
  
## Examples  
For examples, see [COUNT &#40;Transact-SQL&#41;](../../t-sql/functions/count-transact-sql.md).
  
## See also
[Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md)  
[COUNT &#40;Transact-SQL&#41;](../../t-sql/functions/count-transact-sql.md)  
[int, bigint, smallint, and tinyint &#40;Transact-SQL&#41;](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)  
[OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md)
  
  
