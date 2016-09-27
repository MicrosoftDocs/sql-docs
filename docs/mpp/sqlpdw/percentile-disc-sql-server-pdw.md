---
title: "PERCENTILE_DISC (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6944896e-50cb-4556-9604-6ca111cb85c9
caps.latest.revision: 10
author: BarbKess
---
# PERCENTILE_DISC (SQL Server PDW)
PERCENTILE_DISC computes a specific percentile for sorted values in an entire rowset or within distinct partitions of a rowset in SQL Server PDW. For example, PERCENTILE_DISC (0.5) will compute the 50th percentile (that is, the median) of an expression. PERCENTILE_DISC calculates the percentile based on a discrete distribution of the column values; the result is equal to a specific value in the column.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
PERCENTILE_DISC ( literal) WITHIN GROUP   
    ( ORDER BY order_by_expression [ASC | DESC ] )  
    OVER ( [ <partition_by_clause> ] )  
```  
  
## Arguments  
*literal*  
The percentile to compute. The value must range between 0.0 and 1.0.  
  
WITHIN GROUP **(** ORDER BY *order_by_expression* [**ASC** | DESC ]**)**  
Specifies a list of values to sort and compute the percentile over. Only one *order_by_expression* is allowed. The default sort order is ascending. The list of values can be of any of the data types that are valid for the sort operation.  
  
OVER  
Determines the partitioning of the rowset before the analytic function is applied.  
  
<partition_by_clause>  
Divides the result set produced by the [FROM](../sqlpdw/from-sql-server-pdw.md) clause into partitions to which the percentile function is applied. For the PARTITION BY syntax, see [OVER Clause &#40;SQL Server PDW&#41;](../sqlpdw/over-clause-sql-server-pdw.md).  
  
## Return Types  
The return type is determined by the *order_by_expression* type.  
  
|*order_by_expression* Data Type|Return Value Data Type|  
|-------------------------------------|--------------------------|  
|**bigint**|**bigint**|  
|**binary** or **varbinary**|error|  
|**bit**|error|  
|**char** or **varchar**|error|  
|**datetime**|**datetime**|  
|**datetime2**|**datetime2**|  
|**datetimeoffset**|**datetimeoffset**|  
|**decimal**|**decimal**|  
|**float** or **real**|**float** or **real**|  
|**int**|**int**|  
|**money**|**money**|  
|**smallmoney**|**smallmoney**|  
|**smalldatetime**|**smalldatetime**|  
|**smallint**|**smallint**|  
|**time**|**time**|  
|**tinyint**|**tinyint**|  
  
## General Remarks  
Any nulls in the data set are ignored.  
  
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
Shipping and Receiving  9.250000     9.0000</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
