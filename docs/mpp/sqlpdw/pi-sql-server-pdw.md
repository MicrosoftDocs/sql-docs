---
title: "PI (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a0ad65d6-441a-4c7d-a64d-d590541a9321
caps.latest.revision: 4
author: BarbKess
---
# PI (SQL Server PDW)
Returns the constant value of PI.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
PI ()  
```  
  
## Return Types  
**float**  
  
## Examples  
The following example returns the value of `PI`.  
  
```  
SELECT PI();  
GO  
```  
  
Here is the result set.  
  
```  
------------------------  
3.14159265358979  
  
(1 row(s) affected)  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
