---
title: "DEGREES (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 188d2ced-9106-4eb1-9c52-9b776079a516
caps.latest.revision: 5
author: BarbKess
---
# DEGREES (SQL Server PDW)
Returns the corresponding angle in degrees for an angle specified in radians.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DEGREES (numeric_expression )  
```  
  
## Arguments  
*numeric_expression*  
Is an expression of the exact numeric or approximate numeric data type category, except for the **bit** data type.  
  
## Return Code Values  
Returns the same type as *numeric_expression*.  
  
## Examples  
The following example returns the number of degrees in an angle of PI/2 radians.  
  
```  
SELECT 'The number of degrees in PI/2 radians is: ' +   
CONVERT(varchar, DEGREES((PI()/2)));  
GO  
```  
  
Here is the result set.  
  
```  
The number of degrees in PI/2 radians is 90         
  
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
