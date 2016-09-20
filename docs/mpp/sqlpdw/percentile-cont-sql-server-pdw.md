---
title: "PERCENTILE_CONT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6d9fda69-cb98-4bd2-abbe-b84b533588f6
caps.latest.revision: 28
author: BarbKess
---
# PERCENTILE_CONT (SQL Server PDW)
PERCENTILE_CONT calculates the percentile value based on a continuous distribution of the column values; the result is interpolated and might not be equal to any of the specific column values.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
PERCENTILE_CONT  ( literal ) WITHIN GROUP   
     ( ORDER BY order_by_expression [ASC | DESC ] )  
    OVER  ( [ PARTITION BY partition_by_expression ] )  
```  
  
## Arguments  
*literal*  
The percentile to compute. The literal is between 0.0 and 1.0.  
  
WITHIN GROUP  ( ORDER BY *order_by_expression* [**ASC** | DESC ])  
Specifies a list of numeric values to use as input to the function. The *order_by_expression* can contain column references. The *order_by_expression* must evaluate to one of the following data types:  
  
-   Exact numeric (bigint, numeric, bit, smallint, decimal, smallmoney, int, tinyint, money)  
  
-   Approximate numeric (float, real)  
  
For more information, see [ORDER BY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/order-by-sql-server-pdw.md).  
  
OVER ( [ PARTITION BY *partition_by_expression* ] )  
Determines the partitioning of the rowset before the analytic function is applied. The PERCENTILE_CONT function is used within a SELECT statement, and is applied to the results of the FROM clause. The percentile function is applied to each partition. For more information about the *partition_by_expression* syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/over-clause-sql-server-pdw.md).  
  
## Return Types  
The return type is always FLOAT(53) and is not determined by the *order_by_expression* type. The result can be NULL.  
  
## General Remarks  
PERCENTILE_CONT ignores NULL values in the results of the FROM clause.  
  
## Limitations and Restrictions  
The PERCENTILE_CONT function is computed by using approximate numerics; running the function on a different ordering of the data, or on different hardware can produce different results.  
  
## Examples  
  
### A. Basic syntax example  
The following example uses PERCENTILE_CONT and PERCENTILE_DISC to find the median employee salary in each department. Note that these functions may not return the same value. This is because PERCENTILE_CONT interpolates the appropriate value, whether or not it exists in the data set, while PERCENTILE_DISC always returns an actual value from the set.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT DISTINCT DepartmentName  
,PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY BaseRate)  
    OVER (PARTITION BY DepartmentName) AS MedianCont  
,PERCENTILE_DISC(0.5) WITHIN GROUP (ORDER BY BaseRate)  
    OVER (PARTITION BY DepartmentName) AS MedianDisc  
FROM dbo.DimEmployee;  
```  
  
Here is a partial result set.  
  
<pre>DepartmentName        MedianCont    MedianDisc  
--------------------   ----------   ----------  
Document Control       16.826900    16.8269  
Engineering            34.375000    32.6923  
Human Resources        17.427850    16.5865  
Shipping and Receiving 9.250000      9.0000</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
