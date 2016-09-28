---
title: "ACOS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8cefd6fa-f279-4c0d-80d2-5f78e1be2955
caps.latest.revision: 6
author: BarbKess
---
# ACOS (SQL Server PDW)
A mathematical function that returns the angle, in radians, whose cosine is the specified **float** expression; also called arccosine.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ACOS ( float_expression )  
```  
  
## Arguments  
*float_expression*  
Is an expression of the type **float** or of a type that can be implicitly converted to **float**, with a value from -1 through 1. Values outside this range return NULL and report a domain error.  
  
## Return Types  
**float**  
  
## Examples  
The following example returns the ACOS of the specified number.  
  
```  
DECLARE @cos float;  
SET @cos = -1.0;  
SELECT 'The ACOS of the number is: ' + CONVERT(varchar, ACOS(@cos));  
```  
  
Here is the result set.  
  
```  
---------------------------------   
The ACOS of the number is: 3.14159   
  
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
