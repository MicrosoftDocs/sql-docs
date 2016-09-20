---
title: "ASIN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d2664138-00d2-438c-98da-1135b04d0af6
caps.latest.revision: 20
author: BarbKess
---
# ASIN (SQL Server PDW)
Returns the angle, in radians, whose sine is the specified **float** expression in SQL Server PDW. This is also referred to as the arcsine.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ASIN (float_expression )  
```  
  
## Arguments  
*float_expression*  
An expression of the type **float** or of a type that can be implicitly converted to **float**, with a value from -1 through 1. Values outside this range return NULL and report a domain error.  
  
## Return Types  
**float**  
  
## Examples  
The following example returns the arcsine of 1.00.  
  
```  
SELECT ASIN(1.00) AS asinCalc;  
```  
  
The following example returns an error, because it requests the arcsine for a value outside the allowed range.  
  
```  
SELECT ASIN(1.1472738) AS asinCalc;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/expressions-sql-server-pdw.md)  
[SIN &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sin-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/data-types-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
  
