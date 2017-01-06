---
title: "Wildcard Character(s) to Match (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5107da2f-3b9f-4aea-9fcf-57b5cf00c806
caps.latest.revision: 15
author: BarbKess
---
# Wildcard Character(s) to Match (SQL Server PDW)
The % character matches any string of zero or more characters. The % character can be used as either a prefix or a suffix.  
  
> [!NOTE]  
> For information about using % as the arithmetic modulo operator, see [Modulo &#40;SQL Server PDW&#41;](../sqlpdw/modulo-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Examples  
The following example returns all the names of people in the `dimEmployee` table where the first name starts with `D`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName  
FROM dimEmployee  
WHERE FirstName LIKE 'D%';  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[LIKE &#40;SQL Server PDW&#41;](../sqlpdw/like-sql-server-pdw.md)  
  
