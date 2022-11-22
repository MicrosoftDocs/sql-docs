---
title: "GROUPING (Transact-SQL)"
description: "GROUPING (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "12/03/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "GROUPING"
  - "GROUPING_TSQL"
helpviewer_keywords:
  - "null values [SQL Server], GROUPING function"
  - "grouping columns"
  - "ROLLUP operator"
  - "GROUP BY clause, GROUPING function"
  - "GROUPING function"
  - "CUBE operator"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqledge-current || = azure-sqldw-latest"
---
# GROUPING (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  Indicates whether a specified column expression in a GROUP BY list is aggregated or not. GROUPING returns 1 for aggregated or 0 for not aggregated in the result set. GROUPING can be used only in the SELECT \<select> list, HAVING, and ORDER BY clauses when GROUP BY is specified.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
GROUPING ( <column_expression> )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 \<column_expression>  
 Is a column or an expression that contains a column in a [GROUP BY](../../t-sql/queries/select-group-by-transact-sql.md) clause.  
  
## Return Types  
 **tinyint**  
  
## Remarks  
 GROUPING is used to distinguish the null values that are returned by ROLLUP, CUBE or GROUPING SETS from standard null values. The NULL returned as the result of a ROLLUP, CUBE or GROUPING SETS operation is a special use of NULL. This acts as a column placeholder in the result set and means all.  
  
## Examples  
 The following example groups `SalesQuota` and aggregates `SaleYTD` amounts in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. The `GROUPING` function is applied to the `SalesQuota` column.  
  
```sql 
SELECT SalesQuota, SUM(SalesYTD) 'TotalSalesYTD', GROUPING(SalesQuota) AS 'Grouping'  
FROM Sales.SalesPerson  
GROUP BY SalesQuota WITH ROLLUP;  
GO  
```  
  
 The result set shows two null values under `SalesQuota`. The first `NULL` represents the group of null values from this column in the table. The second `NULL` is in the summary row added by the ROLLUP operation. The summary row shows the `TotalSalesYTD` amounts for all `SalesQuota` groups and is indicated by `1` in the `Grouping` column.  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 SalesQuota     TotalSalesYTD       Grouping  
------------   -----------------   --------  
NULL           1533087.5999          0  
250000.00      33461260.59           0  
300000.00      9299677.9445          0  
NULL           44294026.1344         1  

(4 row(s) affected)
```  
  
## See Also  
 [GROUPING_ID &#40;Transact-SQL&#41;](../../t-sql/functions/grouping-id-transact-sql.md)   
 [GROUP BY &#40;Transact-SQL&#41;](../../t-sql/queries/select-group-by-transact-sql.md)  
  
  
