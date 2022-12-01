---
title: "APPROX_COUNT_DISTINCT (Transact-SQL)"
description: "APPROX_COUNT_DISTINCT (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "11/12/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "APPROX_COUNT_DISTINCT"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# APPROX_COUNT_DISTINCT (Transact-SQL)

[!INCLUDE [sqlserver2019-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi-asa.md)]

This function returns the approximate number of unique non-null values in a group. 
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
APPROX_COUNT_DISTINCT ( expression )   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type, except **image**, **sql_variant**, **ntext**, or **text**. 

## Return types
 **bigint**  
  
## Remarks  
`APPROX_COUNT_DISTINCT( expression )` evaluates an expression for each row in a group, and returns the approximate number of unique non-null values in a group. This function is designed to provide aggregations across large data sets where responsiveness is more critical than absolute precision.  

`APPROX_COUNT_DISTINCT` is designed for use in big data scenarios and is optimized for the following conditions:
- Access of data sets that are millions of rows or higher *and*
- Aggregation of a column or columns that have many distinct values

The function implementation guarantees up to a 2% error rate within a 97% probability. 

`APPROX_COUNT_DISTINCT` requires less memory than an exhaustive COUNT DISTINCT operation.  Given the smaller memory footprint, `APPROX_COUNT_DISTINCT` is less likely to spill memory to disk compared to a precise COUNT DISTINCT operation. To learn more about the algorithm used to achieve this, see [HyperLogLog](https://en.wikipedia.org/wiki/HyperLogLog).

> [!NOTE]
> With collation sensitive strings, APPROX_COUNT_DISTINCT uses a binary match and provides results that would have been generated in the presence of BIN collations and not BIN2. 
  
## Examples  
  
### A. Using APPROX_COUNT_DISTINCT 
This example returns the approximate number of different order keys from the orders table.
  
```sql
SELECT APPROX_COUNT_DISTINCT(O_OrderKey) AS Approx_Distinct_OrderKey
FROM dbo.Orders;
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
Approx_Distinct_OrderKey
------------------------
15164704
```
  
### B. Using APPROX_COUNT_DISTINCT with GROUP BY 
This example returns the approximate number of different order keys by order status from the orders table. 
  
```sql
SELECT O_OrderStatus, APPROX_COUNT_DISTINCT(O_OrderKey) AS Approx_Distinct_OrderKey
FROM dbo.Orders
GROUP BY O_OrderStatus
ORDER BY O_OrderStatus; 
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
O_OrderStatus                                                    Approx_Distinct_OrderKey
---------------------------------------------------------------- ------------------------
F                                                                7397838
O                                                                7387803
P                                                                388036
```
    
## See also
[Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md)  
[COUNT &#40;Transact-SQL&#41;](../../t-sql/functions/count-transact-sql.md) 
