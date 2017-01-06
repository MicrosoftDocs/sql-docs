---
title: "AND (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e68f81c9-7e81-45e1-a5a4-fa277dcf62a2
caps.latest.revision: 13
author: BarbKess
---
# AND (SQL Server PDW)
Combines two Boolean expressions and returns TRUE when both expressions are true. When more than one logical operator is used in a statement, the AND operators are evaluated first. You can change the order of evaluation by using parentheses.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
boolean_expression AND boolean_expression  
```  
  
## Arguments  
*boolean_expression*  
Is any valid expression that returns a Boolean value: TRUE, FALSE, or UNKNOWN.  
  
## Result Types  
**Boolean**  
  
## Result Value  
Returns TRUE when both expressions are true.  
  
## Remarks  
The following chart shows the outcomes when you compare TRUE and FALSE values by using the AND operator.  
  
|Expression1|Expression2|Result|  
|---------------|---------------|----------|  
|TRUE|TRUE|TRUE|  
|TRUE|FALSE|FALSE|  
|TRUE|UNKNOWN|UNKNOWN|  
|FALSE|FALSE|FALSE|  
|FALSE|UNKNOWN|FALSE|  
|UNKNOWN|UNKNOWN|UNKNOWN|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Operators &#40;SQL Server PDW&#41;](../sqlpdw/operators-sql-server-pdw.md)  
[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../sqlpdw/where-sql-server-pdw.md)  
  
