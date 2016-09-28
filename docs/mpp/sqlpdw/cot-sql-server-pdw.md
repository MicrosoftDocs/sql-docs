---
title: "COT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b976f87c-eec7-4606-a7d0-6a27723fb87f
caps.latest.revision: 5
author: BarbKess
---
# COT (SQL Server PDW)
A mathematical function that returns the trigonometric cotangent of the specified angle, in radians, in the specified **float** expression.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
COT (float_expression )  
```  
  
## Arguments  
*float_expression*  
Is an expression of type **float** or of a type that can be implicitly converted to **float**.  
  
## Return Types  
**float**  
  
## Examples  
The following example returns the COT for the specific angle.  
  
```  
DECLARE @angle float;  
SET @angle = 124.1332;  
SELECT 'The COT of the angle is: ' + CONVERT(varchar,COT(@angle));  
GO  
```  
  
Here is the result set.  
  
```  
The COT of the angle is: -0.040312                
  
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
